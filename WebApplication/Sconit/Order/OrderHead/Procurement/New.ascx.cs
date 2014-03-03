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
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Utility;
using com.Sconit.Control;

public partial class Order_OrderHead_New : NewModuleBase
{
    public event EventHandler CreateEvent;
    public event EventHandler QuickCreateEvent;
    public event EventHandler BackEvent;

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

    public bool IsQuick
    {
        get
        {
            return (bool)ViewState["IsQuick"];
        }
        set
        {
            ViewState["IsQuick"] = value;
        }
    }

    //不合格品退货
    public bool IsReject
    {
        get
        {
            return (bool)ViewState["IsReject"];
        }
        set
        {
            ViewState["IsReject"] = value;
        }
    }

    //新品
    public bool NewItem
    {
        get
        {
            return (bool)ViewState["NewItem"];
        }
        set
        {
            ViewState["NewItem"] = value;
        }
    }
    private string CurrentFlowCode
    {
        get
        {
            return (string)ViewState["CurrentFlowCode"];
        }
        set
        {
            ViewState["CurrentFlowCode"] = value;
        }
    }

    public void PageCleanup()
    {
        this.tbFlow.Text = string.Empty;
        this.tbRefOrderNo.Text = string.Empty;
        this.tbExtOrderNo.Text = string.Empty;
        this.tbWinTime.Text = string.Empty;
        this.cbIsUrgent.Checked = false;
        this.CurrentFlowCode = null;
        this.tbStartTime.Text = string.Empty;
        this.lbCurrency.Text = string.Empty;
        this.ucList.PageCleanup();
        this.ucHuList.PageCleanup();
        this.ucList.Visible = false;
        this.ucHuList.Visible = false;
        if (this.IsReject)
        {
            lblWinDate.Visible = false;
            tbWinTime.Visible = false;
            cbIsUrgent.Visible = false;
            lblExtOrderNo.Visible = false;
            tbExtOrderNo.Visible = false;
            lblStartTime.Visible = false;
            tbStartTime.Visible = false;
            lblRefOrderNo.Visible = false;
            tbRefOrderNo.Visible = false;
            cbPrintOrder.Visible = false;
            cbReleaseOrder.Visible = false;
            cbContinuousCreate.Visible = false;
        }
        if (this.NewItem)
        {
            //供货只看目的区域权限
            this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:false,bool:true,bool:false,bool:true,bool:true,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;
            this.tbFlow.DataBind();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucList.SaveEvent += new System.EventHandler(this.SaveRender);
        this.ucHuList.SaveEvent += new System.EventHandler(this.SaveRender);
        List<Flow> flowList = new List<Flow>();
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:false,bool:true,bool:false,bool:true,bool:true,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;

        string userLanguage = this.CurrentUser.UserLanguage;
        this.tbWinTime.Attributes.Add("onclick", "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm',lang:'" + this.CurrentUser.UserLanguage + "'})");
        this.tbWinTime.Attributes["onchange"] += "setStartTime();";
        this.cbIsUrgent.Attributes["onchange"] += "setStartTime();";

        if (this.BackEvent != null)
        {
            this.btnBack.Visible = true;
        }
        else
        {
            this.btnBack.Visible = false;
        }

        if (!IsPostBack)
        {
            this.ucList.ModuleType = this.ModuleType;
            this.ucList.ModuleSubType = this.ModuleSubType;
            this.ucHuList.ModuleType = this.ModuleType;
            this.ucHuList.ModuleSubType = this.ModuleSubType;
            this.ucList.NewItem = this.NewItem;
            this.ucList.IsReject = this.IsReject;
            this.ucHuList.IsReject = this.IsReject;

            if (IsQuick)
            {
                this.cbReleaseOrder.Visible = false;
            }

        }
    }

    protected void tbFlow_TextChanged(Object sender, EventArgs e)
    {
        try
        {
            this.tbWinTime.Text = string.Empty;
            this.tbStartTime.Text = string.Empty;
            if (this.CurrentFlowCode == null || this.CurrentFlowCode != this.tbFlow.Text)
            {
                Flow currentFlow = TheFlowMgr.LoadFlow(tbFlow.Text, this.CurrentUser.Code, true, this.ModuleType);
                if (currentFlow != null)
                {
                    this.CurrentFlowCode = currentFlow.Code;

                    this.cbReleaseOrder.Checked = currentFlow.IsAutoRelease;
                    this.cbPrintOrder.Checked = currentFlow.NeedPrintOrder;

                    OrderHead orderHead = TheOrderMgr.TransferFlow2Order(currentFlow);
                    orderHead.SubType = this.ModuleSubType;
                    orderHead.WindowTime = DateTime.Now;

                    this.hfLeadTime.Value = currentFlow.LeadTime.ToString();
                    this.hfEmTime.Value = currentFlow.EmTime.ToString();


                    InitDetailParamater(orderHead);
                }
                else
                {
                    this.tbFlow.Text = string.Empty;
                    this.ucList.Visible = false;
                }
            }
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }
    private void InitDetailParamater(OrderHead orderHead)
    {
        Flow currentFlow = this.TheFlowMgr.LoadFlow(orderHead.Flow);
        bool isScanHu = currentFlow.IsShipScanHu || currentFlow.IsReceiptScanHu || (currentFlow.CreateHuOption == BusinessConstants.CODE_MASTER_CREATE_HU_OPTION_VALUE_GI) || currentFlow.CreateHuOption == BusinessConstants.CODE_MASTER_CREATE_HU_OPTION_VALUE_GR;

        if (this.IsQuick && isScanHu && !this.IsReject)
        {
            this.ucHuList.InitPageParameter(orderHead);
            this.ucList.Visible = false;
            this.ucHuList.Visible = true;
        }
        else
        {
            if (!currentFlow.IsListDetail)
            {
                orderHead.OrderDetails = new List<OrderDetail>();
            }

            this.ucList.CleanPrice();
            this.ucList.IsReject = this.IsReject;
            this.ucList.NewItem = this.NewItem;
            this.ucList.InitPageParameter(orderHead);
            this.ucList.Visible = true;
            this.ucHuList.Visible = false;
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {

        Flow currentFlow = TheFlowMgr.LoadFlow(this.tbFlow.Text);
        if (currentFlow != null)
        {
            bool isScanHu = currentFlow.IsShipScanHu || currentFlow.IsReceiptScanHu || (currentFlow.CreateHuOption == BusinessConstants.CODE_MASTER_CREATE_HU_OPTION_VALUE_GI) || currentFlow.CreateHuOption == BusinessConstants.CODE_MASTER_CREATE_HU_OPTION_VALUE_GR;

            if (this.IsQuick && isScanHu && !this.IsReject)
            {
                this.ucHuList.SaveCallBack();
            }
            else
            {
                this.ucList.SaveCallBack();
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    private void SaveRender(object sender, EventArgs e)
    {
        if (this.cvStartTime.IsValid || this.IsReject)
        {
            IList<OrderDetail> resultOrderDetailList = new List<OrderDetail>();
            OrderHead orderHead = CloneHelper.DeepClone<OrderHead>((OrderHead)sender);  //Clone：避免修改List Page的TheOrder，导致出错

            if (orderHead != null && orderHead.OrderDetails != null && orderHead.OrderDetails.Count > 0)
            {
                foreach (OrderDetail orderDetail in orderHead.OrderDetails)
                {
                    if (orderDetail.OrderedQty != 0)
                    {
                        if (orderDetail.Item.Type == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_K)
                        {
                            IList<Item> newItemList = new List<Item>(); //填充套件子件
                            decimal? convertRate = null;
                            IList<ItemKit> itemKitList = null;

                            var maxSequence = orderHead.OrderDetails.Max(o => o.Sequence);
                            itemKitList = this.TheItemKitMgr.GetChildItemKit(orderDetail.Item);
                            for (int i = 0; i < itemKitList.Count; i++)
                            {
                                Item item = itemKitList[i].ChildItem;
                                if (!convertRate.HasValue)
                                {
                                    if (itemKitList[i].ParentItem.Uom.Code != orderDetail.Item.Uom.Code)
                                    {
                                        convertRate = this.TheUomConversionMgr.ConvertUomQty(orderDetail.Item, orderDetail.Item.Uom, 1, itemKitList[i].ParentItem.Uom);
                                    }
                                    else
                                    {
                                        convertRate = 1;
                                    }
                                }
                                OrderDetail newOrderDetail = new OrderDetail();

                                newOrderDetail.OrderHead = orderDetail.OrderHead;
                                newOrderDetail.Sequence = maxSequence + (i + 1);
                                newOrderDetail.IsBlankDetail = false;
                                newOrderDetail.Item = item;



                                newOrderDetail.Uom = item.Uom;
                                newOrderDetail.UnitCount = orderDetail.Item.UnitCount * itemKitList[i].Qty * convertRate.Value;
                                newOrderDetail.OrderedQty = orderDetail.OrderedQty * itemKitList[i].Qty * convertRate.Value;
                                newOrderDetail.PackageType = orderDetail.PackageType;

                                #region 价格字段

                                if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
                                {
                                    if (orderDetail.PriceListFrom != null && orderDetail.PriceListFrom.Code != string.Empty)
                                    {
                                        newOrderDetail.PriceListFrom = ThePurchasePriceListMgr.LoadPurchasePriceList(orderDetail.PriceListFrom.Code);

                                        if (newOrderDetail.PriceListFrom != null)
                                        {
                                            newOrderDetail.PriceListDetailFrom = this.ThePriceListDetailMgr.GetLastestPriceListDetail(newOrderDetail.PriceListFrom, item, DateTime.Now, newOrderDetail.OrderHead.Currency, item.Uom);
                                        }
                                    }
                                }
                                else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
                                {
                                    if (orderDetail.PriceListTo != null && orderDetail.PriceListTo.Code != string.Empty)
                                    {
                                        newOrderDetail.PriceListTo = TheSalesPriceListMgr.LoadSalesPriceList(orderDetail.PriceListTo.Code);
                                    }
                                    if (newOrderDetail.PriceListTo != null)
                                    {
                                        newOrderDetail.PriceListDetailTo = this.ThePriceListDetailMgr.GetLastestPriceListDetail(newOrderDetail.PriceListTo, item, DateTime.Now, newOrderDetail.OrderHead.Currency, item.Uom);
                                    }
                                }

                                #endregion
                                resultOrderDetailList.Add(newOrderDetail);

                            }

                        }
                        else
                            resultOrderDetailList.Add(orderDetail);

                    }
                }
            }
            if (resultOrderDetailList.Count == 0)
            {
                this.ShowErrorMessage("MasterData.Order.OrderHead.OrderDetail.Required");
                return;
            }
            else
            {
                Flow currentFlow = TheFlowMgr.LoadFlow(CurrentFlowCode, true);

                DateTime winTime = this.tbWinTime.Text.Trim() == string.Empty ? DateTime.Now : DateTime.Parse(this.tbWinTime.Text);
                DateTime startTime = winTime;

                if (this.tbStartTime.Text != string.Empty)
                {
                    startTime = DateTime.Parse(this.tbStartTime.Text.Trim());
                }
                else
                {
                    double leadTime = this.hfLeadTime.Value == string.Empty ? 0 : double.Parse(this.hfLeadTime.Value);
                    double emTime = this.hfEmTime.Value == string.Empty ? 0 : double.Parse(this.hfEmTime.Value);
                    double lTime = this.cbIsUrgent.Checked ? emTime : leadTime;
                    startTime = winTime.AddHours(0 - lTime);
                }

                orderHead.OrderDetails = resultOrderDetailList;
                orderHead.WindowTime = winTime;
                orderHead.StartTime = startTime;
                orderHead.IsAutoRelease = this.cbReleaseOrder.Checked;

                orderHead.IsNewItem = this.NewItem;
                if (this.cbIsUrgent.Checked)
                {
                    orderHead.Priority = BusinessConstants.CODE_MASTER_ORDER_PRIORITY_VALUE_URGENT;
                }
                else
                {
                    orderHead.Priority = BusinessConstants.CODE_MASTER_ORDER_PRIORITY_VALUE_NORMAL;
                }
                if (this.tbRefOrderNo.Text.Trim() != string.Empty)
                {
                    orderHead.ReferenceOrderNo = this.tbRefOrderNo.Text.Trim();
                }
                if (this.tbExtOrderNo.Text.Trim() != string.Empty)
                {
                    orderHead.ExternalOrderNo = this.tbExtOrderNo.Text.Trim();
                }
            }

            //创建订单
            try
            {
                if (this.IsQuick)
                {
                    Receipt receipt = TheOrderMgr.QuickReceiveOrder(orderHead.Flow, orderHead.OrderDetails, this.CurrentUser.Code, this.ModuleSubType, orderHead.WindowTime, orderHead.StartTime, this.cbIsUrgent.Checked, orderHead.ReferenceOrderNo, orderHead.ExternalOrderNo);
                    if (receipt.ReceiptDetails != null && receipt.ReceiptDetails.Count > 0)
                    {
                        orderHead = receipt.ReceiptDetails[0].OrderLocationTransaction.OrderDetail.OrderHead;
                        if (this.cbPrintOrder.Checked)
                        {
                            IList<OrderDetail> orderDetails = orderHead.OrderDetails;
                            IList<object> list = new List<object>();
                            list.Add(orderHead);
                            list.Add(orderDetails);

                            IList<OrderLocationTransaction> orderLocationTransactions = TheOrderLocationTransactionMgr.GetOrderLocationTransaction(orderHead.OrderNo);
                            list.Add(orderLocationTransactions);

                            //TheReportProductionMgr.FillValues(orderHead.OrderTemplate, list);
                            string printUrl = TheReportMgr.WriteToFile(orderHead.OrderTemplate, list);

                            Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + printUrl + "'); </script>");
                        }
                    }
                    this.ShowSuccessMessage("Receipt.Receive.Successfully", receipt.ReceiptNo);

                    if (!this.cbContinuousCreate.Checked)
                    {
                        this.PageCleanup();
                        if (QuickCreateEvent != null)
                        {
                            QuickCreateEvent(new Object[] { receipt, orderHead.NeedPrintReceipt }, e);
                        }

                    }
                    else
                    {
                        orderHead = TheOrderMgr.TransferFlow2Order(this.tbFlow.Text.Trim());
                        InitDetailParamater(orderHead);
                    }
                }
                else
                {
                    TheOrderMgr.CreateOrder(orderHead, this.CurrentUser);
                    if (this.cbPrintOrder.Checked)
                    {

                        IList<OrderDetail> orderDetails = orderHead.OrderDetails;
                        IList<object> list = new List<object>();
                        list.Add(orderHead);
                        list.Add(orderDetails);

                        IList<OrderLocationTransaction> orderLocationTransactions = TheOrderLocationTransactionMgr.GetOrderLocationTransaction(orderHead.OrderNo);
                        list.Add(orderLocationTransactions);

                        //TheReportProductionMgr.FillValues(orderHead.OrderTemplate, list);
                        string printUrl = TheReportMgr.WriteToFile(orderHead.OrderTemplate, list);

                        Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + printUrl + "'); </script>");
                    }
                    this.ShowSuccessMessage("MasterData.Order.OrderHead.AddOrder.Successfully", orderHead.OrderNo);
                    if (!this.cbContinuousCreate.Checked)
                    {
                        this.PageCleanup();
                        if (CreateEvent != null)
                        {
                            CreateEvent(orderHead.OrderNo, e);
                        }
                    }
                    else
                    {
                        orderHead = TheOrderMgr.TransferFlow2Order(this.tbFlow.Text.Trim());
                        InitDetailParamater(orderHead);
                    }

                }

            }
            catch (BusinessErrorException ex)
            {
                this.ShowErrorMessage(ex);
                return;
            }

        }
    }

    protected void CheckStartTime(object source, ServerValidateEventArgs args)
    {
        DateTime winTime = this.tbWinTime.Text.Trim() == string.Empty ? DateTime.Now : DateTime.Parse(this.tbWinTime.Text);
        DateTime startTime = this.tbStartTime.Text.Trim() == string.Empty ? DateTime.Now : DateTime.Parse(this.tbWinTime.Text);
        double leadTime = this.hfLeadTime.Value == string.Empty ? 0 : double.Parse(this.hfLeadTime.Value);
        double emTime = this.hfEmTime.Value == string.Empty ? 0 : double.Parse(this.hfEmTime.Value);
        if (this.tbStartTime.Text != string.Empty)
        {
            startTime = DateTime.Parse(this.tbStartTime.Text.Trim());
        }
        else
        {
            double lTime = this.cbIsUrgent.Checked ? emTime : leadTime;
            startTime = winTime.AddHours(0 - lTime);
        }
        if (startTime < DateTime.Now)
        {
            args.IsValid = false;
        }
    }

    protected void tbWinTime_TextChanged(object sender, EventArgs e)
    {
        if (tbWinTime.Text.Trim() != string.Empty && tbFlow.Text.Trim() != string.Empty)
        {
            OrderHead orderHead = TheOrderMgr.TransferFlow2Order(this.tbFlow.Text.Trim());
            orderHead.SubType = this.ModuleSubType;
            orderHead.WindowTime = DateTime.Parse(tbWinTime.Text.Trim());
            InitDetailParamater(orderHead);
        }
    }
}
