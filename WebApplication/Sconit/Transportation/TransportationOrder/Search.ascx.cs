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
using com.Sconit.Entity.Transportation;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Web;
using com.Sconit.Entity;
using NHibernate.Expression;
using System.Collections.Generic;
using com.Sconit.Utility;

public partial class Transportation_TransportationOrder_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        
    }

    protected override void DoSearch()
    {
        string orderNo = this.tbOrderNo.Text.Trim() != string.Empty ? this.tbOrderNo.Text.Trim() : string.Empty;
        string routeNo = this.tbRoute.Text.Trim() != string.Empty ? this.tbRoute.Text.Trim() : string.Empty;
        string shipFrom = this.tbShipFrom.Text.Trim() != string.Empty ? this.tbShipFrom.Text.Trim() : string.Empty;
        string shipTo = this.tbShipTo.Text.Trim() != string.Empty ? this.tbShipTo.Text.Trim() : string.Empty;
        string startDate = this.tbStartDate.Text.Trim() != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
        string endDate = this.tbEndDate.Text.Trim() != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;

        if (SearchEvent != null)
        {
            #region DetachedCriteria

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(TransportationOrder));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(TransportationOrder))
                .SetProjection(Projections.Count("OrderNo"));

            selectCriteria.CreateAlias("TransportationRoute", "r", NHibernate.SqlCommand.JoinType.LeftOuterJoin);
            selectCriteria.CreateAlias("r.ShipFrom", "f", NHibernate.SqlCommand.JoinType.LeftOuterJoin);
            selectCriteria.CreateAlias("r.ShipTo", "t", NHibernate.SqlCommand.JoinType.LeftOuterJoin);


            selectCountCriteria.CreateAlias("TransportationRoute", "r", NHibernate.SqlCommand.JoinType.LeftOuterJoin);
            selectCountCriteria.CreateAlias("r.ShipFrom", "f", NHibernate.SqlCommand.JoinType.LeftOuterJoin);
            selectCountCriteria.CreateAlias("r.ShipTo", "t", NHibernate.SqlCommand.JoinType.LeftOuterJoin);


            if (orderNo != string.Empty)
            {
                selectCriteria.Add(Expression.Like("OrderNo", orderNo, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("OrderNo", orderNo, MatchMode.Start));
            }

            if (routeNo != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("r.Code", routeNo));
                selectCountCriteria.Add(Expression.Eq("r.Code", routeNo));
            }
            if (shipFrom != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("f.Id", Int32.Parse(shipFrom)));
                selectCountCriteria.Add(Expression.Eq("f.Id", Int32.Parse(shipFrom)));
            }
            if (shipTo != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("t.Id", Int32.Parse(shipTo)));
                selectCountCriteria.Add(Expression.Eq("t.Id", Int32.Parse(shipTo)));
            }
            if (startDate != string.Empty)
            {
                selectCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
                selectCountCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
            }
            if (endDate != string.Empty)
            {
                selectCriteria.Add(Expression.Le("CreateDate", DateTime.Parse(endDate).AddDays(1)));
                selectCountCriteria.Add(Expression.Le("CreateDate", DateTime.Parse(endDate).AddDays(1)));
            }

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }
}
