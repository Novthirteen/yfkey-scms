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
using com.Sconit.Entity.Distribution;
using com.Sconit.Service.Distribution;

public partial class Order_OrderHead_New : NewModuleBase
{
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;
    public event EventHandler QuickCreateEvent;

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
        this.ucList.PageCleanup();
        this.ucHuList.PageCleanup();
        this.ucList.Visible = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucList.SaveEvent += new System.EventHandler(this.SaveRender);
        this.ucHuList.SaveEvent += new System.EventHandler(this.SaveRender);
        List<Flow> flowList = new List<Flow>();
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:false,bool:false,bool:true,bool:false,bool:false,string:"+BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH;

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

            this.ucShift.Date = DateTime.Today;

            this.ucList.NewItem = this.NewItem;

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
            this.ucList.Visible = false;
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

                    this.ucList.InitPageParameter(orderHead);
                    this.ucList.Visible = true;
                    this.ucList.CleanPrice();
                    this.ucHuList.Visible = false;
                    
                    this.hfLeadTime.Value = currentFlow.LeadTime.ToString();
                    this.hfEmTime.Value = currentFlow.EmTime.ToString();
                    
                  //  InitDetailParamater(orderHead);

                    this.BindShift();
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

    protected void tbWinTime_TextChanged(object sender, EventArgs e)
    {
        this.BindShift();
    }

    private void BindShift()
    {
        Flow currentFlow = TheFlowMgr.LoadFlow(tbFlow.Text, true);
        string regionCode = currentFlow != null ?currentFlow.PartyFrom.Code:string.Empty;
        DateTime dateTime = this.tbStartTime.Text.Trim() == string.Empty ? DateTime.Today : DateTime.Parse(this.tbStartTime.Text);
        this.ucShift.BindList(dateTime, regionCode);
    }

    private void InitDetailParamater(OrderHead orderHead)
    {
        Flow currentFlow = this.TheFlowMgr.LoadFlow(orderHead.Flow);
        bool isScanHu = currentFlow.IsShipScanHu || currentFlow.IsReceiptScanHu || (currentFlow.CreateHuOption == BusinessConstants.CODE_MASTER_CREATE_HU_OPTION_VALUE_GI) || currentFlow.CreateHuOption == BusinessConstants.CODE_MASTER_CREATE_HU_OPTION_VALUE_GR;

        if (this.IsQuick && isScanHu)
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
            this.ucList.InitPageParameter(orderHead);
            this.ucList.Visible = true;
            this.ucHuList.Visible = false;
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {

        if (this.ucShift.ShiftCode == string.Empty)
        {
            ShowErrorMessage("MasterData.Order.Shift.Empty");
            return;
        }
        //Flow currentFlow = TheFlowMgr.LoadFlow(this.tbFlow.Text);
        //bool isScanHu = currentFlow.IsShipScanHu || currentFlow.IsReceiptScanHu || (currentFlow.CreateHuOption == BusinessConstants.CODE_MASTER_CREATE_HU_OPTION_VALUE_GI) || currentFlow.CreateHuOption == BusinessConstants.CODE_MASTER_CREATE_HU_OPTION_VALUE_GR;

        //if (this.IsQuick && isScanHu)
        //{
        //    this.ucHuList.SaveCallBack();
        //}
        //else
        //{
            this.ucList.SaveCallBack();
        //}
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
        if (this.cvStartTime.IsValid)
        {
            IList<OrderDetail> resultOrderDetailList = new List<OrderDetail>();
            OrderHead orderHead = CloneHelper.DeepClone<OrderHead>((OrderHead)sender);  //Clone：避免修改List Page的TheOrder，导致出错

            if (orderHead != null && orderHead.OrderDetails != null && orderHead.OrderDetails.Count > 0)
            {
                foreach (OrderDetail orderDetail in orderHead.OrderDetails)
                {
                    if (orderDetail.OrderedQty != 0)
                    {
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
                DateTime winTime = DateTime.Parse(this.tbWinTime.Text);
                DateTime startTime = DateTime.Parse(this.tbWinTime.Text);
                string shiftCode = this.ucShift.ShiftCode;

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
                if (this.ucShift.ShiftCode != string.Empty)
                {
                    orderHead.Shift = TheShiftMgr.LoadShift(this.ucShift.ShiftCode);
                }
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

                if (this.IsQuick)
                {
                    orderHead.IsAutoRelease = true;
                    orderHead.IsAutoStart = true;
                    orderHead.IsAutoShip = true;
                    orderHead.IsAutoReceive = true;
                    orderHead.StartLatency = 0;
                    orderHead.CompleteLatency = 0;
                }
                if (orderHead.ReferenceOrderNo != null && orderHead.ReferenceOrderNo != string.Empty)
                {
                    orderHead.IsAdditional = true;
                }
            }

            //创建订单
            try
            {
                //if (this.IsQuick)
                //{
                //    Receipt receipt = TheOrderMgr.QuickReceiveOrder(orderHead.Flow, orderHead.OrderDetails, this.CurrentUser, this.ModuleSubType, orderHead.WindowTime, orderHead.StartTime, this.cbIsUrgent.Checked, orderHead.ReferenceOrderNo, orderHead.ExternalOrderNo);

                //    if (receipt.ReceiptDetails != null && receipt.ReceiptDetails.Count > 0)
                //    {
                //        orderHead = receipt.ReceiptDetails[0].OrderLocationTransaction.OrderDetail.OrderHead;
                //        if (this.cbPrintOrder.Checked)
                //        {
                //            IList<OrderDetail> orderDetails = orderHead.OrderDetails;
                //            IList<object> list = new List<object>();
                //            list.Add(orderHead);
                //            list.Add(orderDetails);

                //            IList<OrderLocationTransaction> orderLocationTransactions = TheOrderLocationTransactionMgr.GetOrderLocationTransaction(orderHead.OrderNo);
                //            list.Add(orderLocationTransactions);

                //              //TheReportProductionMgr.FillValues(orderHead.OrderTemplate, list);
                //              string printUrl = TheReportMgr.WriteToFile(orderHead.OrderTemplate, list);

                //            Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + printUrl + "'); </script>");
                //        }
                //    }
                //    this.ShowSuccessMessage("Receipt.Receive.Successfully", receipt.ReceiptNo);
                //    this.PageCleanup();
                //    if (!this.cbContinuousCreate.Checked)
                //    {
                //        if (QuickCreateEvent != null)
                //        {
                //            QuickCreateEvent(new Object[] { receipt, orderHead.NeedPrintReceipt }, e);
                //        }

                //    }
                //}
                //else
                //{
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
                    this.ucList.InitPageParameter(orderHead);
                }

                //}
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
        DateTime winTime = DateTime.Parse(this.tbWinTime.Text);
        DateTime startTime = DateTime.Parse(this.tbWinTime.Text);
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
}
