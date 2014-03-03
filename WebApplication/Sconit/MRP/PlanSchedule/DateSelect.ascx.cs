using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Web;
using com.Sconit.Utility;

public partial class MRP_PlanSchedule_DateSelect : ModuleBase
{
    public DateTime Date
    {
        get { return DateTime.Parse(this.tbDate.Text); }
        set { this.tbDate.Text = value.ToString("yyyy-MM-dd"); }
    }

    public string TimePeriodType
    {
        get { return this.ddlTimePeriodType.SelectedValue; }
        set { this.ddlTimePeriodType.SelectedValue = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            this.ddlTimePeriodType.DataSource = this.GetTimePeriodType();
            this.ddlTimePeriodType.DataBind();

            this.UpdateInfo();
        }
    }

    protected void ddlTimePeriodType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.UpdateInfo();
    }

    protected void tbDate_TextChanged(object sender, EventArgs e)
    {
        this.UpdateInfo();
    }

    private IList<CodeMaster> GetTimePeriodType()
    {
        IList<CodeMaster> statusGroup = new List<CodeMaster>();

        statusGroup.Add(GetTimePeriodType(BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY));
        statusGroup.Add(GetTimePeriodType(BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK));
        statusGroup.Add(GetTimePeriodType(BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_MONTH));

        return statusGroup;
    }
    private CodeMaster GetTimePeriodType(string statusValue)
    {
        return TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE, statusValue);
    }

    private void UpdateInfo()
    {
        if (this.TimePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_MONTH)
        {
            string year = this.Date.Year.ToString();
            string month = this.Date.Month.ToString();
            this.lblInfo.Text = TheLanguageMgr.TranslateMessage("MRP.MonthNo", this.CurrentUser, new string[] { year, month });
        }
        else
        {
            string year = this.Date.Year.ToString();
            string week = DateTimeHelper.GetWeekIndex(this.Date).ToString();
            this.lblInfo.Text = TheLanguageMgr.TranslateMessage("MRP.WeekNo", this.CurrentUser, new string[] { year, week });
        }

        this.tbDate.Text = DateTimeHelper.GetStartTime(this.TimePeriodType, this.Date).ToString("yyyy-MM-dd");
    }
}
