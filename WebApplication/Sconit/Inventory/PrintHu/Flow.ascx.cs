using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;

public partial class Inventory_PrintHu_Flow : ModuleBase
{
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

    private string CurrentFlowCode
    {
        get
        {
            return (string)ViewState["CurrentFlowCode"];
        }
        set
        {
            ViewState["CurrentFlowCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (ModuleType == "Region")
        {
            this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:true,bool:false,bool:false,bool:false,bool:false,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_FROM;
        }
        else if (ModuleType == "Supplier")
        {
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:false,bool:false,bool:false,bool:false,bool:false,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_FROM;
        }

        if (!IsPostBack)
        {
            this.ucList.ModuleType = this.ModuleType;
        }
    }


    protected void tbFlow_TextChanged(Object sender, EventArgs e)
    {
        try
        {
            if (this.CurrentFlowCode == null || this.CurrentFlowCode != this.tbFlow.Text)
            {
                Flow currentFlow = TheFlowMgr.LoadFlow(this.tbFlow.Text);
                if (currentFlow != null)
                {
                    this.CurrentFlowCode = currentFlow.Code;

                    if (currentFlow.IsListDetail)
                    {
                        currentFlow.FlowDetails = TheFlowDetailMgr.GetFlowDetail(currentFlow);
                    }
                    else
                    {
                        currentFlow.FlowDetails = new List<FlowDetail>();
                    }

                    this.ucList.InitPageParameter(currentFlow);
                    this.ucList.Visible = true;
                }
            }
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }
}
