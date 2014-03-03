using System;
using System.Collections;
using System.Collections.Generic;
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
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Exception;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Service.Distribution;
using com.Sconit.Utility;

public partial class Distribution_OrderIssue_SupplierList : ModuleBase
{
    public EventHandler EditEvent;


    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }
    public string ModuleSubType
    {
        get { return (string)ViewState["ModuleSubType"]; }
        set { ViewState["ModuleSubType"] = value; }
    }
    public string OrderType
    {
        get { return (string)ViewState["OrderType"]; }
        set { ViewState["OrderType"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void InitPageParameter(string flowCode, string orderSubType)
    {
        this.ModuleSubType = orderSubType;

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(OrderHead));
        selectCriteria.Add(Expression.Eq("Flow", flowCode))
            .Add(Expression.Or(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS),
                Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT)))
            .Add(Expression.Eq("SubType", this.ModuleSubType))
            .AddOrder(Order.Asc("WindowTime")).AddOrder(Order.Asc("StartTime"));
        IList<OrderHead> orderList = new List<OrderHead>();
        IList<OrderHead> orderHeadList = TheCriteriaMgr.FindAll<OrderHead>(selectCriteria);
        foreach (OrderHead orderHead in orderHeadList)
        {
            IList<OrderDetail> orderDetailList = orderHead.OrderDetails;
            if (orderHead.OrderDetails.Count > 0)
            {
                foreach (OrderDetail orderDetail in orderHead.OrderDetails)
                {
                    if (orderDetail.RemainShippedQty > 0)
                    {
                        orderList.Add(orderHead);
                        break;
                    }
                }
            }
        }

        if (orderList.Count > 0)
        {
            this.OrderType = orderList[0].Type;


            this.GV_List.DataSource = orderList;
            this.GV_List.DataBind();
        }
    }

    protected void lbtnOrder_Click(object sender, EventArgs e)
    {

        try
        {
            string orderNo = ((LinkButton)sender).CommandArgument;
            OrderHead orderHead = TheOrderMgr.LoadOrder(orderNo, this.CurrentUser);
            if (orderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT)
            {
                TheOrderMgr.StartOrder(orderNo, this.CurrentUser);
            }
            List<string> orderNoList = new List<string>();
            orderNoList.Add(orderNo);


            if (EditEvent != null)
            {
                EditEvent(new Object[] { orderNoList }, e);

            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            OrderHead orderHead = (OrderHead)e.Row.DataItem;

            Label lblWinDate = (Label)e.Row.FindControl("lblWinDate");
            Label lblWinTime = (Label)e.Row.FindControl("lblWinTime");
            if (orderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE
               || orderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT
               || orderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
            {
                lblWinDate.ForeColor = OrderHelper.GetWinTimeColor(orderHead.StartTime, orderHead.WindowTime);
                lblWinTime.ForeColor = OrderHelper.GetWinTimeColor(orderHead.StartTime, orderHead.WindowTime);
            }

            if (orderHead.Priority == BusinessConstants.CODE_MASTER_ORDER_PRIORITY_VALUE_URGENT)
            {
                Label lblPriority = (Label)e.Row.FindControl("lblPriority");
                lblPriority.ForeColor = System.Drawing.Color.Red;
            }

            if (!orderHead.IsConfirmed)
            {
                Label lblIsConfirmed = (Label)e.Row.FindControl("lblIsConfirmed");
                lblIsConfirmed.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    private List<string> CollectOrderNoList()
    {
        List<string> orderNoList = new List<string>();
        foreach (GridViewRow gvr in GV_List.Rows)
        {
            CheckBox cbCheckBoxGroup = (CheckBox)gvr.FindControl("CheckBoxGroup");
            if (cbCheckBoxGroup.Checked)
            {
                string orderNo = ((Literal)gvr.FindControl("ltlOrderNo")).Text;
                orderNoList.Add(orderNo);
            }
        }
        return orderNoList;
    }


}
