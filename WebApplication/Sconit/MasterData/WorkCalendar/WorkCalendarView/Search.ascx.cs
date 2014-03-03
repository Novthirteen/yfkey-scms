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
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using System.Collections.Generic;

public partial class MasterData_WorkCalendar_WorkCalendarView_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbStartTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.tbEndTime.Text = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
        }
        ((Controls_TextBox)(this.tbRegion)).ServiceParameter = "string:" + this.CurrentUser.Code;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void DoSearch()
    {
        if (SearchEvent != null)
        {
            string region = this.tbRegion.Text.Trim() != string.Empty ? this.tbRegion.Text.Trim() : string.Empty;
            string workcenter = this.tbWorkCenter.Text.Trim() != string.Empty ? this.tbWorkCenter.Text.Trim() : string.Empty;
            string para_starttime = this.tbStartTime.Text.Trim() != string.Empty ? this.tbStartTime.Text.Trim() : string.Empty;
            string para_endtime = this.tbEndTime.Text.Trim() != string.Empty ? this.tbEndTime.Text.Trim() : string.Empty;

            SearchEvent(new object[] { region, workcenter, para_starttime, para_endtime }, null);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Region"))
        {
            this.tbRegion.Text = actionParameter["Region"];
        }
        if (actionParameter.ContainsKey("WorkCenter"))
        {
            this.tbWorkCenter.Text = actionParameter["WorkCenter"];
        }
    }
}
