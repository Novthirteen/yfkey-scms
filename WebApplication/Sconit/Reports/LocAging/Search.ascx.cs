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

public partial class Reports_LocAging_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler ExportEvent;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbLocation.ServiceParameter = "string:" + this.CurrentUser.Code;
        this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
        if (!IsPostBack)
        {
            this.tbDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            this.tbDays.Text = "30";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch();
    }

    protected override void DoSearch()
    {
        if (SearchEvent != null)
        {

            object[] param = this.CollectParam();
            if (param != null)
            {
                SearchEvent(param, null);
            }
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ExportEvent != null)
        {
            object[] param = this.CollectParam();
            if (param != null)
            {

                ExportEvent(param, null);
            }
        }
    }
    private object[] CollectParam()
    {
        CriteriaParam criteriaParam = new CriteriaParam();
        criteriaParam.Location = this.tbLocation.Text.Trim() != string.Empty ? new string[] { this.tbLocation.Text.Trim() } : null;
        criteriaParam.Party = this.tbRegion.Text.Trim() != string.Empty ? new string[] { this.tbRegion.Text.Trim() } : null;
        if (this.tbDate.Text.Trim() != string.Empty)
            criteriaParam.EndDate = DateTime.Parse(this.tbDate.Text);
        criteriaParam.Item = this.tbItem.Text.Trim() != string.Empty ? this.tbItem.Text.Trim() : null;

        criteriaParam.ClassifiedParty = this.cbClassifiedParty.Checked;
        criteriaParam.ClassifiedLocation = this.cbClassifiedLocation.Checked;

        int days = this.tbDays.Text.Trim() != string.Empty ? int.Parse(this.tbDays.Text.Trim()) : 30;
        return new object[] { criteriaParam, days };
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
        if (actionParameter.ContainsKey("Date"))
        {
            this.tbDate.Text = actionParameter["Date"];
        }
        if (actionParameter.ContainsKey("Item"))
        {
            this.tbItem.Text = actionParameter["Item"];
        }
    }
}
