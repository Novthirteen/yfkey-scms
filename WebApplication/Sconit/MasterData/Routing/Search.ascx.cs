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
using NHibernate.Expression;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;

public partial class MasterData_Routing_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        tbRegion.ServiceParameter = "string:" + this.CurrentUser.Code;
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
        string code = this.tbCode.Text.Trim();
        string region = this.tbRegion.Text.Trim();

        if (SearchEvent != null)
        {
            #region DetachedCriteria
            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(Routing));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(Routing))
                .SetProjection(Projections.Count("Code"));

            if (code != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Code", code, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Code", code, MatchMode.Start));
            }
            if (region != string.Empty)
            {
                selectCriteria.Add(Expression.Like("Region.Code", region, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("Region.Code", region, MatchMode.Start));
            }
            DetachedCriteria[] regionCrieteria = SecurityHelper.GetRegionPermissionCriteria(this.CurrentUser.Code);
            selectCriteria.Add(
                Expression.Or(
                  Expression.Or(
                      Subqueries.PropertyIn("Region.Code", regionCrieteria[0]),
                      Subqueries.PropertyIn("Region.Code", regionCrieteria[1])
                                ),
                      Expression.IsNull("Region.Code")
                              )
                );

            selectCountCriteria.Add(
                Expression.Or(
                  Expression.Or(
                      Subqueries.PropertyIn("Region.Code", regionCrieteria[0]),
                      Subqueries.PropertyIn("Region.Code", regionCrieteria[1])
                                ),
                      Expression.IsNull("Region.Code")
                              )
                );
            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion

        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Code"))
        {
            this.tbCode.Text = actionParameter["Code"];
        }
        if (actionParameter.ContainsKey("RegionCode"))
        {
            this.tbRegion.Text = actionParameter["RegionCode"];
        }
    }
}
