using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using com.Sconit.Entity;

public partial class MasterData_FlowBinding_Edit : ModuleBase
{
    public event EventHandler BackEvent;

    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }

    public int FlowBindingId
    {
        get
        {
            if (ViewState["FlowBindingId"] == null)
            {
                return 0;
            }
            else
            {
                return (Int32)ViewState["FlowBindingId"];
            }
        }
        set
        {
            ViewState["FlowBindingId"] = value;
        }
    }


    public void InitPageParameter(Int32 id)
    {
        this.FlowBindingId = id;
        this.ODS_FlowBinding.SelectParameters["id"].DefaultValue = FlowBindingId.ToString();
        this.ODS_FlowBinding.DeleteParameters["id"].DefaultValue = FlowBindingId.ToString();

    }

    protected void FV_FlowBinding_DataBound(object sender, EventArgs e)
    {

        FlowBinding flowBinding = TheFlowBindingMgr.LoadFlowBinding(FlowBindingId);

        Label lblMasterFlowTypeValue = (Label)this.FV_FlowBinding.FindControl("lblMasterFlowTypeValue");
        Label lblMasterFlowValue = (Label)this.FV_FlowBinding.FindControl("lblMasterFlowValue");
        Controls_TextBox tbSlaveFlow = (Controls_TextBox)this.FV_FlowBinding.FindControl("tbSlaveFlow");
        com.Sconit.Control.CodeMstrDropDownList ddlBindingType = (com.Sconit.Control.CodeMstrDropDownList)this.FV_FlowBinding.FindControl("ddlBindingType");

        tbSlaveFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
        tbSlaveFlow.DataBind();

        lblMasterFlowTypeValue.Text = flowBinding.MasterFlow.Type;
        lblMasterFlowValue.Text = flowBinding.MasterFlow.Code;

        tbSlaveFlow.Text = flowBinding.SlaveFlow.Code;
        ddlBindingType.Text = flowBinding.BindingType;

    }

    protected void ODS_FlowBinding_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        FlowBinding flowBinding = (FlowBinding)e.InputParameters[0];
        FlowBinding oldFlowBinding = TheFlowBindingMgr.LoadFlowBinding(FlowBindingId);
        flowBinding.MasterFlow = oldFlowBinding.MasterFlow;

        Controls_TextBox tbSlaveFlow = (Controls_TextBox)this.FV_FlowBinding.FindControl("tbSlaveFlow");
        com.Sconit.Control.CodeMstrDropDownList ddlBindingType = (com.Sconit.Control.CodeMstrDropDownList)this.FV_FlowBinding.FindControl("ddlBindingType");

        if (tbSlaveFlow != null && tbSlaveFlow.Text.Trim() != string.Empty)
        {
            flowBinding.SlaveFlow = TheFlowMgr.LoadFlow(tbSlaveFlow.Text);
        }

        if (ddlBindingType.SelectedIndex != -1)
        {
            flowBinding.BindingType = ddlBindingType.SelectedValue;
        }
    }

    protected void ODS_FlowBinding_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        String slaveFlowCode = ((Controls_TextBox)this.FV_FlowBinding.FindControl("tbSlaveFlow")).Text;
        ShowSuccessMessage("MasterData.Flow.Binding.UpdateBinding.Successfully", slaveFlowCode);
    }

    protected void ODS_FlowBinding_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        btnBack_Click(this, e);
        String slaveFlowCode = ((Controls_TextBox)this.FV_FlowBinding.FindControl("tbSlaveFlow")).Text;
        ShowSuccessMessage("MasterData.Flow.Binding.DeleteBinding.Successfully", slaveFlowCode);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
            this.FlowBindingId = 0;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }


    protected void CheckSlaveFlow(object source, ServerValidateEventArgs args)
    {

        String masterFlowCode = ((Label)this.FV_FlowBinding.FindControl("lblMasterFlowValue")).Text;
        String slaveFlowCode = ((Controls_TextBox)this.FV_FlowBinding.FindControl("tbSlaveFlow")).Text;
        CustomValidator cvSlaveFlowCheck = ((CustomValidator)this.FV_FlowBinding.FindControl("cvSlaveFlowCheck"));

        FlowBinding flowBinding = TheFlowBindingMgr.LoadFlowBinding(this.FlowBindingId);
        if (flowBinding.SlaveFlow.Code != slaveFlowCode)
        {
            if (TheFlowBindingMgr.LoadFlowBinding(masterFlowCode, slaveFlowCode) != null)
            {
                args.IsValid = false;
                ShowErrorMessage("MasterData.Flow.Binding.SlaveFlow.Exists");
            }

            if (CheckLoopBind(masterFlowCode, slaveFlowCode))
            {
                args.IsValid = false;
                ShowErrorMessage("MasterData.Flow.Binding.SlaveFlow.LoopBind");
            }
        }
    }
    private bool CheckLoopBind(string masterFlowCode, string slaveFlowCode)
    {
        bool isLoop = false;
        while (slaveFlowCode != string.Empty)
        {
            if (masterFlowCode == slaveFlowCode)
            {
                isLoop = true;
                break;
            }
            else
            {

                List<FlowBinding> flowBindingList = (List<FlowBinding>)TheFlowBindingMgr.GetFlowBinding(slaveFlowCode);

                foreach (FlowBinding flowBinding in flowBindingList)
                {
                    slaveFlowCode = flowBinding.SlaveFlow != null ? flowBinding.SlaveFlow.Code : string.Empty;
                    isLoop = CheckLoopBind(masterFlowCode, slaveFlowCode);
                }
                slaveFlowCode = string.Empty;
            }

        }
        return isLoop;
    }
}
