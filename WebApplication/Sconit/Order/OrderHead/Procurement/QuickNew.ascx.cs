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

public partial class Order_OrderHead_QuickNew : NewModuleBase
{
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
     
        this.CurrentFlowCode = null;
      
        this.ucList.PageCleanup();
        this.ucHuList.PageCleanup();
        this.ucList.Visible = false;
        this.ucHuList.Visible = false;
        
        if (this.NewItem)
        {
            this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:false,bool:false,bool:false,bool:false,bool:false,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;
            this.tbFlow.DataBind();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucList.SaveEvent += new System.EventHandler(this.SaveRender);
        this.ucHuList.SaveEvent += new System.EventHandler(this.SaveRender);
        List<Flow> flowList = new List<Flow>();
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:false,bool:true,bool:false,bool:true,bool:true,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;

        if (!IsPostBack)
        {
            this.ucList.ModuleType = this.ModuleType;
            this.ucList.ModuleSubType = this.ModuleSubType;
            this.ucHuList.ModuleType = this.ModuleType;
            this.ucHuList.ModuleSubType = this.ModuleSubType;
            this.ucList.NewItem = this.NewItem;
            this.ucList.IsReject = this.IsReject;
            this.ucHuList.IsReject = this.IsReject;
        }
    }

    protected void tbFlow_TextChanged(Object sender, EventArgs e)
    {
        try
        {
            if (this.CurrentFlowCode == null || this.CurrentFlowCode != this.tbFlow.Text)
            {
                Flow currentFlow = TheFlowMgr.LoadFlow(tbFlow.Text,this.CurrentUser.Code, true, this.ModuleType);
                if (currentFlow != null)
                {
                    this.CurrentFlowCode = currentFlow.Code;
                    this.cbPrintOrder.Checked = currentFlow.NeedPrintOrder;
                    OrderHead orderHead = TheOrderMgr.TransferFlow2Order(currentFlow);
                    orderHead.SubType = this.ModuleSubType;
                    orderHead.WindowTime = DateTime.Now;

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

    private void SaveRender(object sender, EventArgs e)
    {
        if (this.IsReject || this.IsQuick)
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

                DateTime winTime =  DateTime.Now;
                DateTime startTime = winTime;

                orderHead.OrderDetails = resultOrderDetailList;
                orderHead.WindowTime = winTime;
                orderHead.StartTime = startTime;
        
                orderHead.IsNewItem = this.NewItem;
            
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
                    Receipt receipt = TheOrderMgr.QuickReceiveOrder(orderHead.Flow, orderHead.OrderDetails, this.CurrentUser.Code, this.ModuleSubType, orderHead.WindowTime, orderHead.StartTime, false, orderHead.ReferenceOrderNo, orderHead.ExternalOrderNo);
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

                            string printUrl = TheReportMgr.WriteToFile(orderHead.OrderTemplate, list);

                            Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + printUrl + "'); </script>");
                        }
                    }
                    this.ShowSuccessMessage("Receipt.Receive.Successfully", receipt.ReceiptNo);
                    if (QuickCreateEvent != null)
                    {
                        QuickCreateEvent(new Object[] { receipt, orderHead.NeedPrintReceipt }, e);
                    }
                }
                else
                {
                    this.ShowErrorMessage("MasterData.Order.OrderHead.Not.Quick");
                }

            }
            catch (BusinessErrorException ex)
            {
                this.ShowErrorMessage(ex);
                return;
            }

        }
    }
}
