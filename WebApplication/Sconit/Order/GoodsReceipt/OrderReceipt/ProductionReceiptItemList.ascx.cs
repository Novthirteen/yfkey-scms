using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using com.Sconit.Entity.Procurement;
using System.Web.UI;

public partial class Order_GoodsReceipt_OrderReceipt_ProductionReceiptItemList : ModuleBase
{
    public event EventHandler ReceiveEvent;
    public event EventHandler BackEvent;

    public bool IsShowPrice
    {
        get
        {
            return (bool)ViewState["IsShowPrice"];
        }
        set
        {
            ViewState["IsShowPrice"] = value;
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

    protected string OrderNo
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
    public void InitPageParameter(string orderNo)
    {
        this.OrderNo = orderNo;

        OrderHead oH = TheOrderHeadMgr.LoadOrderHead(OrderNo, true);
        bool isSupportHu = false;
        if (!isSupportHu)
        {
            this.ltlHuScan.Visible = false;
            this.tbHuScan.Visible = false;
            this.btnHuScan.Visible = false;
            this.btnScanHu.Visible = false;
        }
        else
        {
            this.ltlHuScan.Visible = true;
            this.tbHuScan.Visible = true;
            this.btnHuScan.Visible = true;
            this.btnScanHu.Visible = true;
        }


        this.lblRefNo.Visible = false;
        this.tbRefNo.Visible = false;


        this.ModuleSubType = oH.SubType;
        RefreshGv_List();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucHuList.HuSaveEvent += new System.EventHandler(this.HuSave_Render);

    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSeq = (Label)e.Row.FindControl("lblSeq");
            Label lblItemCode = (Label)e.Row.FindControl("lblItemCode");
            Label lblItemDescription = (Label)e.Row.FindControl("lblItemDescription");
            Label lblUom = (Label)e.Row.FindControl("lblUom");
            Label lblUnitCount = (Label)e.Row.FindControl("lblUnitCount");
            Label lblLocFrom = (Label)e.Row.FindControl("lblLocFrom");
            Label lblLocTo = (Label)e.Row.FindControl("lblLocTo");
            Label lblPrice = (Label)e.Row.FindControl("lblPrice");
            Label lblDiscount = (Label)e.Row.FindControl("lblDiscount");
            Label lblDiscountRate = (Label)e.Row.FindControl("lblDiscountRate");
            Label lblUnitPrice = (Label)e.Row.FindControl("lblUnitPrice");
            Label lblReferenceItemCode = (Label)e.Row.FindControl("lblReferenceItemCode");
            Label lblOrderQty = (Label)e.Row.FindControl("tbOrderQty");
            Label lblReceivedQty = (Label)e.Row.FindControl("lblReceivedQty");
            HiddenField hfId = (HiddenField)e.Row.FindControl("hfId");
            TextBox tbReceiveQty = (TextBox)e.Row.FindControl("tbReceiveQty");
            HiddenField hfHuOpt = (HiddenField)e.Row.FindControl("hfHuOpt");
            TextBox tbHuId = (TextBox)e.Row.FindControl("tbHuId");
            TextBox tbHuReceiveQty = (TextBox)e.Row.FindControl("tbHuReceiveQty");


            RangeValidator rvReceiveQty = (RangeValidator)e.Row.FindControl("rvReceiveQty");




            if (this.ModuleType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {

                #region 颜色
                decimal OrderQty = lblOrderQty.Text == string.Empty ? 0M : Convert.ToDecimal(lblOrderQty.Text);
                decimal ReceivedQty = lblReceivedQty.Text == string.Empty ? 0M : Convert.ToDecimal(lblReceivedQty.Text);
                decimal ReceiveQty = tbReceiveQty.Text == string.Empty ? 0M : Convert.ToDecimal(tbReceiveQty.Text);
                if (ReceiveQty < OrderQty - ReceivedQty)
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Attributes.Add("class", "GVRow");
                    }
                }
                else
                {
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Attributes.Add("class", "GVAlternatingRow");
                    }
                }
                #endregion
            }
            else
            {
                string defaultReceiptOpt = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_RECEIPT_OPTION).Value;
                OrderLocationTransaction orderLocTrans = (OrderLocationTransaction)e.Row.DataItem;
                tbReceiveQty.Text = OrderHelper.GetDefaultReceiptQty(orderLocTrans.OrderDetail, defaultReceiptOpt).ToString();
            }



            #region 收货数控制

            if (this.ModuleSubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RTN)
            {
                rvReceiveQty.MaximumValue = "0";
                rvReceiveQty.MinimumValue = "-999999999";
            }
            #endregion
        }
    }

    private void RefreshGv_List()
    {

        OrderHead orderHead = TheOrderHeadMgr.LoadOrderHead(this.OrderNo, true);

        if (orderHead != null)
        {
            IList<OrderLocationTransaction> orderLocTransList = new List<OrderLocationTransaction>();
            orderLocTransList = TheOrderLocationTransactionMgr.GetOrderLocationTransaction(orderHead, BusinessConstants.IO_TYPE_IN);
            this.GV_List.DataSource = orderLocTransList;
            this.GV_List.DataBind();
            UpdateView(orderHead);
        }
        else
        {
            ShowErrorMessage("MasterData.Order.NotExists", this.OrderNo);
        }

    }

    private void UpdateView(OrderHead orderHead)
    {
        this.GV_List.Columns[17].Visible = false;
    }

    public void EnterReceiveHuId(List<int> huDetIdList)
    {


    }

    protected void btnScanHu_Click(object sender, EventArgs e)
    {
        this.ucHuList.Visible = true;
    }

    private void HuSave_Render(object sender, EventArgs e)
    {
        EnterReceiveHuId((List<int>)sender);
    }

    protected void tbHuScan_TextChanged(object sender, EventArgs e)
    {

    }



    public void ReceiveCallBack()
    {
        if (ReceiveEvent != null)
        {
            ReceiveEvent(false, null);
        }
    }

    public void SetChanged(bool isChanged)
    {
        this.hfIsChanged.Value = isChanged.ToString();
    }

    public bool GetChanged()
    {
        return bool.Parse(this.hfIsChanged.Value);
    }

    public IList<ReceiptDetail> PopulateReceiptDetailList()
    {
        IList<ReceiptDetail> receiptDetailList = new List<ReceiptDetail>();
        for (int i = 0; i < this.GV_List.Rows.Count; i++)
        {
            GridViewRow row = this.GV_List.Rows[i];

            HiddenField hfId = (HiddenField)row.FindControl("hfId");
            HiddenField hfOrderDetailId = (HiddenField)row.FindControl("hfOrderDetailId");
            TextBox tbHuId = (TextBox)row.FindControl("tbHuId");
            TextBox tbHuReceiveQty = (TextBox)row.FindControl("tbHuReceiveQty");
            TextBox tbReceiveQty = (TextBox)row.FindControl("tbReceiveQty");
            TextBox tbRejectQty = (TextBox)row.FindControl("tbRejectQty");
            TextBox tbScrapQty = (TextBox)row.FindControl("tbScrapQty");
            decimal reveiveQty = tbHuId.Text != string.Empty ? (tbHuReceiveQty.Text.Trim() != string.Empty ? Decimal.Parse(tbHuReceiveQty.Text.Trim()) : 0) : (tbReceiveQty.Text.Trim() != string.Empty ? Decimal.Parse(tbReceiveQty.Text.Trim()) : 0);
            decimal rejectQty = tbRejectQty.Text.Trim() == string.Empty ? 0 : Decimal.Parse(tbRejectQty.Text.Trim());
            decimal scrapQty = tbScrapQty.Text.Trim() == string.Empty ? 0 : Decimal.Parse(tbScrapQty.Text.Trim());
            if (reveiveQty != 0 || rejectQty != 0 || scrapQty != 0)
            {
                ReceiptDetail receiptDetail = new ReceiptDetail();
                receiptDetail.OrderLocationTransaction = TheOrderLocationTransactionMgr.LoadOrderLocationTransaction(Int32.Parse(hfId.Value));

                receiptDetail.ReceivedQty = reveiveQty;
                receiptDetail.RejectedQty = rejectQty;
                receiptDetail.ScrapQty = scrapQty;
                receiptDetail.HuId = tbHuId.Text.Trim() == string.Empty ? null : tbHuId.Text.Trim();
                receiptDetailList.Add(receiptDetail);
            }
        }
        return receiptDetailList;
    }

    public OrderHead PopulateOrderHead()
    {
        OrderHead orderHead = TheOrderHeadMgr.LoadOrderHead(this.OrderNo, true, false, true);
        if (orderHead != null)
        {
            if (orderHead.OrderDetails != null)
            {
                for (int i = 0; i < this.GV_List.Rows.Count; i++)
                {
                    GridViewRow row = this.GV_List.Rows[i];

                    HiddenField hfId = (HiddenField)row.FindControl("hfOrderDetailId");
                    TextBox tbReceiveQty = (TextBox)row.FindControl("tbReceiveQty");
                    TextBox tbRejectQty = (TextBox)row.FindControl("tbRejectQty");
                    TextBox tbScrapQty = (TextBox)row.FindControl("tbScrapQty");

                    OrderDetail orderDetail = orderHead.GetOrderDetailById(int.Parse(hfId.Value));
                    decimal receiveQty = 0;
                    if (tbReceiveQty != null && tbReceiveQty.Text.Trim() != string.Empty)
                    {
                        receiveQty = Decimal.Parse(tbReceiveQty.Text.Trim());
                    }
                    decimal rejectQty = 0;

                    if (tbRejectQty != null && tbRejectQty.Text.Trim() != string.Empty)
                    {
                        rejectQty = Decimal.Parse(tbRejectQty.Text.Trim());
                    }
                    decimal scrapQty = 0;
                    if (tbScrapQty != null && tbScrapQty.Text.Trim() != string.Empty)
                    {
                        scrapQty = Decimal.Parse(tbScrapQty.Text.Trim());
                    }
                    decimal receivedQty = orderDetail.ReceivedQty != null ? orderDetail.ReceivedQty.Value : 0;

                    orderDetail.CurrentReceiveQty = receiveQty;
                    orderDetail.CurrentRejectQty = rejectQty;
                    orderDetail.CurrentScrapQty = scrapQty;

                }


            }
        }
        return orderHead;
    }
}