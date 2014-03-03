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
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Exception;

public partial class Order_GoodsReceipt_OrderReceipt_InLocTransList : ListModuleBase
{
    public EventHandler InLocTransEvent;
    public string ModuleType
    {
        get
        {
            return (string)ViewState["ModuleType"];
        }
        set
        {
            ViewState["ModuleType"] = value;
        }
    }

    public string ModuleSubType
    {
        get
        {
            return (string)ViewState["ModuleSubType"];
        }
        set
        {
            ViewState["ModuleSubType"] = value;
        }
    }

    public string OrderNo
    {
        get
        {
            return (string)ViewState["OrderNo"];
        }
        set
        {
            ViewState["OrderNo"] = value;
        }
    }

    public IList<OrderLocationTransaction> InLocTransList
    {
        get
        {
            return (IList<OrderLocationTransaction>)ViewState["InLocTransList"];
        }
        set
        {
            ViewState["InLocTransList"] = value;
        }
    }

    public override void UpdateView()
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(OrderHead orderHead, bool isChanged)
    {
        if (InLocTransList == null || InLocTransList.Count == 0 || isChanged)
        {
            InLocTransList = new List<OrderLocationTransaction>();
            IDictionary<string, int> detailDic = new Dictionary<string, int>();
            foreach (OrderDetail orderDetail in orderHead.OrderDetails)
            {
                decimal currentReceiveQty = orderDetail.CurrentReceiveQty == null ? 0 : (decimal)orderDetail.CurrentReceiveQty;
                decimal currentRejectQty = orderDetail.CurrentRejectQty == null ? 0 : (decimal)orderDetail.CurrentRejectQty;
                decimal currentScrapQty = orderDetail.CurrentScrapQty == null ? 0 : (decimal)orderDetail.CurrentScrapQty;
                decimal backoutQty = currentReceiveQty + currentRejectQty + currentScrapQty;
                foreach (OrderLocationTransaction orderLocTrans in orderDetail.OrderLocationTransactions)
                {
                    if (orderLocTrans.IOType == BusinessConstants.IO_TYPE_OUT)
                    {
                        if (detailDic.ContainsKey(orderLocTrans.Item.Code + "-" + orderLocTrans.Operation))
                        {
                            InLocTransList[detailDic[orderLocTrans.Item.Code + "-" + orderLocTrans.Operation]].CurrentReceiveQty += orderLocTrans.UnitQty * backoutQty;
                            InLocTransList[detailDic[orderLocTrans.Item.Code + "-" + orderLocTrans.Operation]].OrderedQty += orderLocTrans.OrderedQty;
                            if (InLocTransList[detailDic[orderLocTrans.Item.Code + "-" + orderLocTrans.Operation]].UnitQty != 0 && backoutQty != 0)
                            {
                                InLocTransList[detailDic[orderLocTrans.Item.Code + "-" + orderLocTrans.Operation]].UnitQty = InLocTransList[detailDic[orderLocTrans.Item.Code + "-" + orderLocTrans.Operation]].CurrentReceiveQty / ((InLocTransList[detailDic[orderLocTrans.Item.Code + "-" + orderLocTrans.Operation]].CurrentReceiveQty - orderLocTrans.UnitQty * backoutQty) / InLocTransList[detailDic[orderLocTrans.Item.Code + "-" + orderLocTrans.Operation]].UnitQty + backoutQty);
                            }
                        }
                        else
                        {
                            detailDic.Add(orderLocTrans.Item.Code + "-" + orderLocTrans.Operation, InLocTransList.Count);
                            orderLocTrans.CurrentReceiveQty = orderLocTrans.UnitQty * backoutQty;
                            InLocTransList.Add(orderLocTrans);
                        }
                    }
                }
            }
        }
        this.GV_List.DataSource = InLocTransList;
        this.GV_List.DataBind();
    }

    public void InLocTransCallBack()
    {

        if (InLocTransList != null && InLocTransList.Count > 0)
        {
            for (int i = 0; i < this.GV_List.Rows.Count; i++)
            {
                GridViewRow row = this.GV_List.Rows[i];

                TextBox tbReceiveQty = (TextBox)row.FindControl("tbCurrentReceiveQty");
                Label lbOperation = (Label)row.FindControl("lbOperation");
                Label lbItem = (Label)row.FindControl("lbItem");

                foreach (OrderLocationTransaction orderLocTrans in InLocTransList)
                {
                    if (orderLocTrans.Item.Code == lbItem.Text && orderLocTrans.Operation == int.Parse(lbOperation.Text))
                    {
                        orderLocTrans.CurrentReceiveQty = tbReceiveQty.Text.Trim() == string.Empty ? 0 : decimal.Parse(tbReceiveQty.Text.Trim());
                        break;
                    }
                }

            }
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (!(this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION))
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox tbCurrentReceiveQty = (TextBox)e.Row.FindControl("tbCurrentReceiveQty");
                tbCurrentReceiveQty.ReadOnly = true;
            }
        }
    }

}
