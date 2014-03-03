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
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Utility;

public partial class MasterData_Flow_ViewStrategy : EditModuleBase
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

    protected string FlowCode
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



    public void InitPageParameter(string flowCode)
    {
        this.FlowCode = flowCode;
        this.ODS_Strategy.SelectParameters["code"].DefaultValue = FlowCode;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void FV_Strategy_DataBound(object sender, EventArgs e)
    {
        ((HtmlGenericControl)this.FV_Strategy.FindControl("lStrategy")).InnerText = FlowHelper.GetFlowStrategyLabel(this.ModuleType);
        Literal lblFlowStrategy = (Literal)this.FV_Strategy.FindControl("lblFlowStrategy");
        lblFlowStrategy.Text = FlowHelper.GetFlowStrategyLabel(this.ModuleType) + ":";
    }

}
