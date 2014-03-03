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

public partial class Order_GoodsReceipt_OrderReceipt_OutLocTransList : ListModuleBase
{
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


    public override void UpdateView()
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void InitPageParameter(OrderHead orderHead)
    {
        IList<OrderLocationTransaction> outLocTransList = new List<OrderLocationTransaction>();

        foreach (OrderDetail orderDetail in orderHead.OrderDetails)
        {
            decimal currentReceiveQty = orderDetail.CurrentReceiveQty == null ? 0 : (decimal)orderDetail.CurrentReceiveQty;
            decimal currentRejectQty = orderDetail.CurrentRejectQty == null ? 0 : (decimal)orderDetail.CurrentRejectQty;
            decimal currentScrapQty = orderDetail.CurrentScrapQty == null ? 0 : (decimal)orderDetail.CurrentScrapQty;
            decimal backoutQty = currentReceiveQty + currentRejectQty + currentScrapQty;
            foreach (OrderLocationTransaction orderLocTrans in orderDetail.OrderLocationTransactions)
            {
                if (orderLocTrans.IOType == BusinessConstants.IO_TYPE_IN)
                {
                    orderLocTrans.CurrentReceiveQty = orderLocTrans.UnitQty * currentReceiveQty;
                    orderLocTrans.CurrentRejectQty = orderLocTrans.UnitQty * currentRejectQty;
                    orderLocTrans.CurrentScrapQty = orderLocTrans.UnitQty * currentScrapQty;
                    outLocTransList.Add(orderLocTrans);

                }
            }
        }
        this.GV_List.DataSource = outLocTransList;
        this.GV_List.DataBind();
    }

}
