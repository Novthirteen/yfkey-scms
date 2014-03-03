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

public partial class MasterData_Flow_Strategy : EditModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler UpdateViewEvent;

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

    private string[] EditFields = new string[]
    {
        "FlowStrategy",
        "LotGroup",
        "StartLatency",
        "CompleteLatency",
        "LeadTime",
        "EmTime",
        "WeekInterval",
        "NextOrderTime",
        "NextWinTime",
        "WinTime1",
        "WinTime2",
        "WinTime3",
        "WinTime4",
        "WinTime5",
        "WinTime6",
        "WinTime7",
        "LastModifyUser",
        "LastModifyDate",
        "Interval"
    };

    public void InitPageParameter(string flowCode)
    {
        this.FlowCode = flowCode;
        this.ODS_Strategy.SelectParameters["code"].DefaultValue = FlowCode;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            FlowCode = null;
            BackEvent(this, e);
        }
    }

    protected void FV_Strategy_DataBound(object sender, EventArgs e)
    {
        ((HtmlGenericControl)this.FV_Strategy.FindControl("lStrategy")).InnerText = FlowHelper.GetFlowStrategyLabel(this.ModuleType) ;
        if (FlowCode != null && FlowCode != string.Empty)
        {
            Flow flow = (Flow)((FormView)sender).DataItem;

            com.Sconit.Control.CodeMstrDropDownList ddlStrategy = (com.Sconit.Control.CodeMstrDropDownList)this.FV_Strategy.FindControl("ddlStrategy");
            Literal lblFlowStrategy = (Literal)this.FV_Strategy.FindControl("lblFlowStrategy");
            lblFlowStrategy.Text = FlowHelper.GetFlowStrategyLabel(this.ModuleType) + ":";
            if (flow.FlowStrategy != null)
            {
                ddlStrategy.Text = flow.FlowStrategy;
            }
            else
            {
                ddlStrategy.Text = string.Empty;
            }

        }
    }

    protected void ODS_Strategy_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        Flow flow = (Flow)e.InputParameters[0];
        Flow oldFlow = TheFlowMgr.LoadFlow(FlowCode);
        CloneHelper.CopyProperty(oldFlow, flow, EditFields, true);

        com.Sconit.Control.CodeMstrDropDownList ddlStrategy = (com.Sconit.Control.CodeMstrDropDownList)this.FV_Strategy.FindControl("ddlStrategy");

        flow.FlowStrategy = ddlStrategy.SelectedValue;
        flow.LastModifyUser = this.CurrentUser;
        flow.LastModifyDate = DateTime.Now;
        flow.Version += 1;
    }

    protected void ODS_Strategy_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        UpdateViewEvent(this.FlowCode, e);
        ShowSuccessMessage("MasterData.Flow.UpdateStrategy.Successfully", this.FlowCode);
    }
}
