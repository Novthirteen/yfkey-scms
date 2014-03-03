using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity.Procurement;
using com.Sconit.Utility;
using com.Sconit.Entity.MasterData;

public partial class Order_OrderDetail_OrderTracer : ListModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
            {
                OrderDetail orderDetail = ((OrderTracer)e.Row.DataItem).OrderDetail;
                IList<OrderDetail> list = new List<OrderDetail>();
                list.Add(orderDetail);
                this.FV_FormView.DataSource = list;
                this.FV_FormView.DataBind();
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Visible = false;
    }

    public void InitPageParameter(int orderDetailId)
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(OrderTracer));
        selectCriteria.Add(Expression.Eq("OrderDetail.Id", orderDetailId));

        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(OrderTracer));
        selectCountCriteria.Add(Expression.Eq("OrderDetail.Id", orderDetailId));
        selectCountCriteria.SetProjection(Projections.Count("Id"));

        this.SetSearchCriteria(selectCriteria, selectCountCriteria);
        this.UpdateView();
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
        this.FV_FormView.Visible = (this.GV_List.Rows.Count > 0);
    }
}
