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
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;

public partial class MasterData_WorkCalendar_Workday_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        ((Controls_TextBox)(this.tbRegion)).ServiceParameter = "string:" + this.CurrentUser.Code;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }

    protected override void DoSearch()
    {
        string region = this.tbRegion.Text.Trim() != string.Empty ? this.tbRegion.Text.Trim() : string.Empty;
        string workcenter = this.tbWorkCenter.Text.Trim() != string.Empty ? this.tbWorkCenter.Text.Trim() : string.Empty;

        if (SearchEvent != null)
        {
            #region DetachedCriteria
            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(Workday));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(Workday))
                .SetProjection(Projections.Count("Id"));

            if (region != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Region.Code", region, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Region.Code", region, MatchMode.Start));
            }
            else
            {
                selectCriteria.Add(Expression.IsNull("Region.Code"));
                selectCountCriteria.Add(Expression.IsNull("Region.Code"));
            }

            if (workcenter != string.Empty)
            {
                selectCriteria.Add(Expression.Like("WorkCenter.Code", workcenter, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("WorkCenter.Code", workcenter, MatchMode.Start));
            }
            else
            {
                selectCriteria.Add(Expression.IsNull("WorkCenter.Code"));
                selectCountCriteria.Add(Expression.IsNull("WorkCenter.Code"));
            }
            #endregion

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
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
