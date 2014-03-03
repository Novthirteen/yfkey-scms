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

public partial class Reports_ProdIO_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler ExportEvent;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
        this.tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ExportEvent != null)
        {
            string flow = this.tbFlow.Text.Trim() != string.Empty ? this.tbFlow.Text.Trim() : string.Empty;
            string region = this.tbRegion.Text.Trim() != string.Empty ? this.tbRegion.Text.Trim() : string.Empty;
            string startDate = this.tbStartDate.Text.Trim() != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
            string endDate = this.tbEndDate.Text.Trim() != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;
            string item = this.tbItem.Text.Trim() != string.Empty ? this.tbItem.Text.Trim() : string.Empty;
            ExportEvent((new object[] { flow, region, startDate, endDate, item }), null);
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
            string flow = this.tbFlow.Text.Trim() != string.Empty ? this.tbFlow.Text.Trim() : string.Empty;
            string region = this.tbRegion.Text.Trim() != string.Empty ? this.tbRegion.Text.Trim() : string.Empty;
            string startDate = this.tbStartDate.Text.Trim() != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
            string endDate = this.tbEndDate.Text.Trim() != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;
            string item = this.tbItem.Text.Trim() != string.Empty ? this.tbItem.Text.Trim() : string.Empty;

            SearchEvent((new object[] { flow, region, startDate, endDate, item }), null);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Flow"))
        {
            this.tbFlow.Text = actionParameter["Flow"];
        }
        if (actionParameter.ContainsKey("Region"))
        {
            this.tbRegion.Text = actionParameter["Region"];
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
