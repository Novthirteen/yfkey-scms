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
using com.Sconit.Web;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Utility;
using com.Sconit.Entity.MasterData;

public partial class MRP_PlanSchedule_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler ReleaseEvent;
    public event EventHandler RunEvent;
    public event EventHandler SaveEvent;

    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ShowHideActionButton(false);
        }

        this.InitialUI();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void DoSearch()
    {
        if (SearchEvent != null)
        {
            string party = this.tbRegion.Text.Trim() != string.Empty ? this.tbRegion.Text.Trim() : string.Empty;
            string timePeriodType = this.ucDateSelect.TimePeriodType;
            DateTime startTime = this.ucDateSelect.Date;
            DateTime endTime = this.ucDateSelect.Date.AddDays(7);
            string flowCode = this.tbFlow.Text.Trim() != string.Empty ? this.tbFlow.Text.Trim() : string.Empty;
            string itemCode = this.tbItemCode.Text.Trim() != string.Empty ? this.tbItemCode.Text.Trim() : string.Empty;

            SearchEvent(new object[] { party, timePeriodType, startTime, endTime, flowCode, itemCode }, null);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Party"))
        {
            this.tbRegion.Text = actionParameter["Party"];
        }
        if (actionParameter.ContainsKey("TimePeriodType"))
        {
            this.ucDateSelect.TimePeriodType = actionParameter["TimePeriodType"];
        }
        if (actionParameter.ContainsKey("StartTime"))
        {
            this.ucDateSelect.Date = DateTime.Parse(actionParameter["StartTime"]);
        }
        if (actionParameter.ContainsKey("Flow"))
        {
            this.tbFlow.Text = actionParameter["Flow"];
        }
        if (actionParameter.ContainsKey("ItemCode"))
        {
            this.tbItemCode.Text = actionParameter["ItemCode"];
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveEvent != null)
        {
            SaveEvent(sender, null);
        }
    }

    protected void btnRelease_Click(object sender, EventArgs e)
    {
        if (ReleaseEvent != null)
        {
            ReleaseEvent(sender, null);
        }
    }

    protected void btnRun_Click(object sender, EventArgs e)
    {
        if (RunEvent != null)
        {
            RunEvent(sender, null);
        }
    }

    private void InitialUI()
    {
        if (ModuleType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_DMDSCHEDULE)
        {
            this.lblRegion.Text = "${Common.Business.Customer}:";
            this.lblFlow.Text = "${MasterData.Flow.Flow.Distribution}:";

            this.tbRegion.ServiceMethod = "GetCustomer";
            this.tbRegion.ServicePath = "CustomerMgr.service";
            this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
            this.tbFlow.ServiceMethod = "GetDistributionFlow";
            this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
        }
        else if (ModuleType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MRP)
        {
            this.lblRegion.Text = "${Common.Business.Supplier}:";
            this.lblFlow.Text = "${MasterData.Flow.Flow.Procurement}:";

            this.tbRegion.ServiceMethod = "GetSupplier";
            this.tbRegion.ServicePath = "SupplierMgr.service";
            this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
            this.tbFlow.ServiceMethod = "GetProcurementFlow";
            this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
        }
        else
        {
            this.lblFlow.Text = "${MasterData.Flow.Flow.Production}:";

            this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
            this.tbFlow.ServiceMethod = "GetProductionFlow";
            this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
        }
    }

    public void ShowHideActionButton(bool show)
    {
        if (show)
        {
            this.btnSave.Visible = true;
            this.btnRelease.Visible = true;
            if (ModuleType != BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_DMDSCHEDULE)
                this.btnRun.Visible = true;
        }
        else
        {
            this.btnSave.Visible = false;
            this.btnRelease.Visible = false;
            this.btnRun.Visible = false;
        }
    }
}
