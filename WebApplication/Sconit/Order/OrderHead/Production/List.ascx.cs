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
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Utility;

public partial class Order_OrderHead_List : ListModuleBase
{
    public EventHandler EditEvent;
    public bool isGroup
    {
        get { return ViewState["isGroup"] == null ? true : (bool)ViewState["isGroup"]; }
        set { ViewState["isGroup"] = value; }
    }
    public bool isExport
    {
        get { return ViewState["isExport"] == null ? false : (bool)ViewState["isExport"]; }
        set { ViewState["isExport"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override void UpdateView()
    {
        if (!isExport)
        {
            if (isGroup)
            {
                this.GV_List.Execute();
                this.GV_List.Visible = true;
                this.gp.Visible = true;
                this.GV_List_Detail.Visible = false;
                this.gp_Detail.Visible = false;
            }
            else
            {
                this.GV_List_Detail.Execute();
                this.GV_List.Visible = false;
                this.GV_List_Detail.Visible = true;
                this.gp.Visible = false;
                this.gp_Detail.Visible = true;
                HiddenColumns(this.GV_List_Detail);
            }
        }
        else
        {
            string dateTime = DateTime.Now.ToString("ddhhmmss");

            if (isGroup)
            {
                if (GV_List.FindPager().RecordCount > 0)
                {
                    GV_List.Columns.RemoveAt(GV_List.Columns.Count - 1);
                }
                this.ExportXLS(GV_List, "ProductionGroup" + dateTime + ".xls");
            }
            else
            {
                this.ExportXLS(GV_List_Detail, "ProductionDetail" + dateTime + ".xls");
            }
        }
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string orderNo = ((LinkButton)sender).CommandArgument;
            EditEvent(orderNo, e);
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string orderNo = ((LinkButton)sender).CommandArgument;
        try
        {
            TheOrderMgr.DeleteOrder(orderNo, this.CurrentUser);
            ShowSuccessMessage("Order.DeleteOrder.Successfully", orderNo);
            UpdateView();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        OrderHead orderHead = (OrderHead)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!isExport)
            {
                e.Row.Cells[1].Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor=this.style.borderColor");
                e.Row.Cells[1].Attributes.Add("onmouseout", "this.style.backgroundColor=e");
                e.Row.Cells[1].Attributes.Add("title", GetDetail(orderHead));
            }
            if (orderHead.Status == null ||
                orderHead.Status == string.Empty ||
                orderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
            {
                LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
                if (lbtnDelete != null)
                {
                    lbtnDelete.Visible = true;
                }
            }
            Label lblWinTime = (Label)e.Row.FindControl("lblWinTime");
            if (orderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE
               || orderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT
               || orderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
            {
                lblWinTime.ForeColor = OrderHelper.GetWinTimeColor(orderHead.StartTime, orderHead.WindowTime);
            }
        }
    }
    protected void GV_List_Detail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (isExport && e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[10].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            e.Row.Cells[11].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        }
    }

    private string GetDetail(OrderHead orderHead)
    {
        string detail = string.Empty;
        detail += "cssbody=[obbd] cssheader=[obhd] header=[" + orderHead.OrderNo + " | " + (orderHead.Flow == null ? string.Empty : orderHead.Flow) + "] body=[<table width=100%>";
        System.Collections.Generic.IList<OrderDetail> ods = orderHead.OrderDetails;
        foreach (OrderDetail od in ods)
        {
            string ItemCode = od.Item.Code;
            string ItemDesc = od.Item.Description.Replace("[", "&#91;").Replace("]", "&#93;");
            string OrderQty = od.OrderedQty.ToString("0.########");
            string Uom = od.Uom.Code;
            string RecQty = od.ReceivedQty == null ? "0" : od.ReceivedQty.Value.ToString("0.########");
            detail += "<tr><td>" + ItemCode + "</td><td>" + ItemDesc + "</td><td>" + OrderQty + "</td><td>" + Uom + "</td><td>" + RecQty + "</td></tr>";
        }
        detail += "</table>]";
        return detail;
    }

    private void HiddenColumns(GridView gridView)
    {
        bool lblReferenceItemHasValue = false;

        foreach (GridViewRow row in gridView.Rows)
        {
            Label lblReferenceItem = (Label)row.FindControl("lblReferenceItem");
            if (lblReferenceItem.Text != string.Empty)
            {
                lblReferenceItemHasValue = true;
                break;
            }
        }
        gridView.Columns[11].Visible = lblReferenceItemHasValue;
    }
}
