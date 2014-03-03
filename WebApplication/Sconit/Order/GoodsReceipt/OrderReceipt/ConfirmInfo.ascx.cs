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

public partial class Order_GoodsReceipt_OrderReceipt_ConfirmInfo : ModuleBase
{

    public event EventHandler ConfirmEvent;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Visible = false;
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        this.Visible = false;
        ConfirmEvent(this.cbIsOddCreateHu.Checked, e);
    }

    public void InitPageParameter(OrderHead orderHead, bool hasOdd, bool isOddCreateHu)
    {
        IList<OrderDetail> orderDetailList = new List<OrderDetail>();
        foreach (OrderDetail orderDetail in orderHead.OrderDetails)
        {
            if (orderDetail.CurrentReceiveQty != 0 || orderDetail.CurrentRejectQty != 0 || orderDetail.CurrentScrapQty != 0)
            {
                orderDetailList.Add(orderDetail);
            }
        }
        this.GV_List.DataSource = orderDetailList;
        this.GV_List.DataBind();

        if (hasOdd)
        {
            this.lblIsOddCreateHu.Visible = true;
            this.cbIsOddCreateHu.Visible = true;
            this.cbIsOddCreateHu.Checked = isOddCreateHu;
        }

    }

}
