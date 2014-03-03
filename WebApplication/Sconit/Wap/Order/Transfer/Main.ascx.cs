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
using System.ComponentModel;
using System.Collections.Generic;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using System.Drawing;

public partial class Wap_Order_Transfer_Main : MainModuleBase
{
    private OrderDet orderDet;
    private List<OrderDet> orderDetList;

    private ClientMgrWS TheClientMgrWS = new ClientMgrWS();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.orderDetList = new List<OrderDet>();
        }
    }

    private void gvDetailDataBind()
    {
        this.gvTransfer.DataSource = new BindingList<OrderDet>(orderDetList);
    }
    protected void btnFlow_Click(object sender, EventArgs e)
    {
        // Flow flow = TheClientMgrWS.LoadFlow(this.tbFlow.Text.Trim(), this.CurrentUser.Code);
        Flow flow = null;
         if (flow!=null)
         {
             this.ltlFlow.Text = flow.Description;
             this.tbItem.Focus();
         }
         else
         {
             this.ltlFlow.Text = "移库路线不存在";
             this.ltlFlow.ForeColor = Color.Red;
             this.tbFlow.Focus();
             this.tbFlow.Text = string.Empty;
         }
    }
    protected void btnItem_Click(object sender, EventArgs e)
    {
        //Item item = TheItemMgr.GetItem(this.tbItem.Text.Trim());
        //if (item!=null)
        //{
        //    this.ltlItem.Text = item.Description;
        //    this.tbQty.Focus();
        //}
        //else
        //{
        //    this.ltlItem.Text = "物料不存在";
        //    this.ltlItem.ForeColor = Color.Red;
        //    this.tbItem.Focus();
        //    this.tbItem.Text = string.Empty;
        //}
    }
    protected void btnQty_Click(object sender, EventArgs e)
    {
        try
        {
            Convert.ToDecimal(this.tbQty.Text.Trim());
            

        }
        catch (Exception)
        {
            this.ltlQty.Text = "请输入数字";
            this.ltlQty.ForeColor = Color.Red;
            this.tbQty.Text = string.Empty;
            this.ltlQty.Focus();
        }
    }
}
