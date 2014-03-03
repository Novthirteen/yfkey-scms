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
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Transportation;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Web;
using com.Sconit.Entity;
using NHibernate.Expression;
using System.Collections.Generic;
using com.Sconit.Utility;

public partial class Transportation_ValuateOrder_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler ValuateEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("OrderNo"))
        {
            this.tbOrderNo.Text = actionParameter["OrderNo"];
        }
    }

    protected override void DoSearch()
    {
        string orderNo = this.tbOrderNo.Text != string.Empty ? this.tbOrderNo.Text.Trim() : string.Empty;
        string routeNo = this.tbRoute.Text.Trim() != string.Empty ? this.tbRoute.Text.Trim() : string.Empty;
        string startDate = this.tbStartDate.Text.Trim() != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
        string endDate = this.tbEndDate.Text.Trim() != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;

        if (SearchEvent != null)
        {
            #region DetachedCriteria

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(TransportationOrder));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(TransportationOrder))
                .SetProjection(Projections.Count("OrderNo"));

            selectCriteria.CreateAlias("TransportationRoute", "r", NHibernate.SqlCommand.JoinType.LeftOuterJoin);
            selectCountCriteria.CreateAlias("TransportationRoute", "r", NHibernate.SqlCommand.JoinType.LeftOuterJoin);

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
            if (startDate != string.Empty)
            {
                selectCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
                selectCountCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
            }
            if (endDate != string.Empty)
            {
                selectCriteria.Add(Expression.Le("CreateDate", DateTime.Parse(endDate).AddDays(1).AddMilliseconds(-1)));
                selectCountCriteria.Add(Expression.Le("CreateDate", DateTime.Parse(endDate).AddDays(1).AddMilliseconds(-1)));
            }

            selectCriteria.Add(Expression.Not(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL)));
            selectCountCriteria.Add(Expression.Not(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL)));

            selectCriteria.Add(Expression.Eq("IsValuated", false));
            selectCountCriteria.Add(Expression.Eq("IsValuated", false));

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion
        }
    }

    protected void btnValuate_Click(object sender, EventArgs e)
    {
        ValuateEvent(sender, e);
    }

    public void ValuateOrder(IList<TransportationOrder> transportationOrderList)
    {
        try
        {
            if (transportationOrderList != null && transportationOrderList.Count > 0)
            {
                TheTransportationOrderMgr.ValuateTransportationOrder(transportationOrderList, this.CurrentUser);

                DoSearch();

                ShowSuccessMessage("Transportation.TransportationOrder.ValuateTransportationOrder.Successfully");
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }
}
