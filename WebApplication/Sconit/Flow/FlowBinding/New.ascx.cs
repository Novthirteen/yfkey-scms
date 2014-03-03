using System;
using System.Collections;
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
using System.Collections.Generic;

public partial class MasterData_FlowBinding_New : ModuleBase
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

    public string FlowCode
    {
        get
        {
            return (string)ViewState["FlowCode"];
        }
        set
        {
            ViewState["FlowCode"] = value;
        }
    }


    private FlowBinding flowBinding;

    public void PageCleanup()
    {
        Flow flow = TheFlowMgr.LoadFlow(this.FlowCode);
        Label lblMasterFlowTypeValue = (Label)this.FV_FlowBinding.FindControl("lblMasterFlowTypeValue");
        Label lblMasterFlowValue = (Label)this.FV_FlowBinding.FindControl("lblMasterFlowValue");
        Controls_TextBox tbSlaveFlow = (Controls_TextBox)this.FV_FlowBinding.FindControl("tbSlaveFlow");
    

        lblMasterFlowTypeValue.Text = flow.Type;

        lblMasterFlowValue.Text = flow.Code;
        lblMasterFlowValue.ToolTip = flow.Description;


       // tbSlaveFlow.ServiceMethod = FlowHelper.GetFlowServiceMethod(this.ModuleType);
        tbSlaveFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
        tbSlaveFlow.DataBind();

        tbSlaveFlow.Text = string.Empty;

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void ODS_FlowBinding_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        flowBinding = (FlowBinding)e.InputParameters[0];
        flowBinding.MasterFlow = TheFlowMgr.LoadFlow(FlowCode);

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

    protected void ODS_FlowBinding_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
            String slaveFlowCode = ((Controls_TextBox)this.FV_FlowBinding.FindControl("tbSlaveFlow")).Text;
            ShowSuccessMessage("MasterData.Flow.Binding.AddBinding.Successfully", slaveFlowCode);
        }
    }

    protected void CheckSlaveFlow(object source, ServerValidateEventArgs args)
    {

        String masterFlowCode = ((Label)this.FV_FlowBinding.FindControl("lblMasterFlowValue")).Text;
        String slaveFlowCode = ((Controls_TextBox)this.FV_FlowBinding.FindControl("tbSlaveFlow")).Text;
        CustomValidator cvSlaveFlowCheck = ((CustomValidator)this.FV_FlowBinding.FindControl("cvSlaveFlowCheck"));

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
