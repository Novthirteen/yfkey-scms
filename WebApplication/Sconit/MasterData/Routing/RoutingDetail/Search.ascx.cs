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

public partial class MasterData_Routing_RoutingDetail_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    private string code;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void DoSearch()
    {

        if (SearchEvent != null)
        {
            #region DetachedCriteria
            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(RoutingDetail));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(RoutingDetail))
                .SetProjection(Projections.Count("Id"));

            if (code != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Routing.Code", code));
                selectCountCriteria.Add(Expression.Eq("Routing.Code", code));
            }

            #endregion

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("RoutingCode"))
        {
            this.code = actionParameter["RoutingCode"];
        }
    }
}
