using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity.View;
using com.Sconit.Entity;
using com.Sconit.Utility;

public partial class Reports_InvTurn_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler ExportEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbLocation.ServiceParameter = "string:" + this.CurrentUser.Code;
        this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTimeHelper.GetStartTime(BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_YEAR, DateTime.Today).ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTimeHelper.GetEndTime(BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_YEAR, DateTime.Today).ToString("yyyy-MM-dd");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ExportEvent != null)
        {
            object param = this.CollectParam();
            if (param != null)
                ExportEvent(param, null);
        }
    }

    protected override void DoSearch()
    {
        if (SearchEvent != null)
        {
            object criteriaParam = CollectParam();
            SearchEvent(criteriaParam, null);
        }
    }

    private CriteriaParam CollectParam()
    {
        CriteriaParam criteriaParam = new CriteriaParam();
        criteriaParam.UserCode = this.CurrentUser.Code;
        criteriaParam.Location = this.tbLocation.Text.Trim() != string.Empty ? new string[] { this.tbLocation.Text.Trim() } : null;
        criteriaParam.Party = this.tbRegion.Text.Trim() != string.Empty ? new string[] { this.tbRegion.Text.Trim() } : null;
        if (this.tbStartDate.Text.Trim() != string.Empty)
            criteriaParam.StartDate = DateTime.Parse(this.tbStartDate.Text);
        if (this.tbEndDate.Text.Trim() != string.Empty)
            criteriaParam.EndDate = DateTime.Parse(this.tbEndDate.Text);
        criteriaParam.Item = this.tbItem.Text.Trim() != string.Empty ? this.tbItem.Text.Trim() : null;

        criteriaParam.ClassifiedParty = this.cbClassifiedParty.Checked;
        criteriaParam.ClassifiedLocation = this.cbClassifiedLocation.Checked;

        return criteriaParam;
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Region"))
        {
            this.tbRegion.Text = actionParameter["Region"];
        }
        if (actionParameter.ContainsKey("Location"))
        {
            this.tbLocation.Text = actionParameter["Location"];
        }
        if (actionParameter.ContainsKey("StartDate"))
        {
            this.tbStartDate.Text = actionParameter["StartDate"];
        }
        if (actionParameter.ContainsKey("EndDate"))
        {
            this.tbEndDate.Text = actionParameter["EndDate"];
        }
        if (actionParameter.ContainsKey("Item"))
        {
            this.tbItem.Text = actionParameter["Item"];
        }
    }
}
