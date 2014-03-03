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
using com.Sconit.Entity.MasterData;
using com.Sconit.Control;
using com.Sconit.Service.Distribution;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using System.Collections.Generic;
using com.Sconit.Utility;


public partial class Warehouse_InProcessLocation_Edit : EditModuleBase
{
    public string IpNo
    {
        get { return (string)ViewState["IpNo"]; }
        set { ViewState["IpNo"] = value; }
    }

    public string Action
    {
        get { return (string)ViewState["Action"]; }
        set { ViewState["Action"] = value; }
    }

    public string AsnType
    {
        get { return (string)ViewState["AsnType"]; }
        set { ViewState["AsnType"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    public void InitPageParameter(string ipNo)
    {
        this.ODS_InProcessLocation.SelectParameters["code"].DefaultValue = ipNo;
        this.IpNo = ipNo;
        this.FV_InProcessLocation.DataBind();
    }

    public void UpdateView()
    {
        InProcessLocation ip = TheInProcessLocationMgr.LoadInProcessLocation(this.IpNo);
        Controls_TextBox tbCurrentOperation = (Controls_TextBox)this.FV_InProcessLocation.FindControl("tbCurrentOperation");
        TextBox tbCurrentActivity = (TextBox)this.FV_InProcessLocation.FindControl("tbCurrentActivity");
        tbCurrentOperation.Text = ip.CurrentOperation.ToString();
        tbCurrentActivity.Text = ip.CurrentActivity;

    }
    protected void FV_Flow_DataBound(object sender, EventArgs e)
    {

        if (this.Action == "Close")
        {
            this.FV_InProcessLocation.FindControl("tbDisposition").Visible = true;
            this.FV_InProcessLocation.FindControl("tbReferenceOrderNo").Visible = true;

            this.FV_InProcessLocation.FindControl("lbDisposition").Visible = false;
            this.FV_InProcessLocation.FindControl("lbReferenceOrderNo").Visible = false;

        }

    }

    public void UpdateInProcessLocation()
    {

        InProcessLocation ip = TheInProcessLocationMgr.LoadInProcessLocation(this.IpNo);
        Controls_TextBox tbCurrentOperation = (Controls_TextBox)this.FV_InProcessLocation.FindControl("tbCurrentOperation");
        if (tbCurrentOperation.Text.Trim() != string.Empty)
        {
            int op = int.Parse(tbCurrentOperation.Text.Trim());
            int currentOp = ip.CurrentOperation == null ? 0 : (int)ip.CurrentOperation;
            if (op > currentOp)
            {
                try
                {

                    TheInProcessLocationMgr.UpdateInProcessLocation(ip, op, this.CurrentUser);
                    UpdateView();

                }
                catch (BusinessErrorException ex)
                {
                    ShowErrorMessage(ex);
                }
            }
        }
    }

    public void CloseInProcessLocation()
    {

        RequiredFieldValidator rfvDisposition = (RequiredFieldValidator)this.FV_InProcessLocation.FindControl("rfvDisposition");
        RequiredFieldValidator rfvReferenceOrderNo = (RequiredFieldValidator)this.FV_InProcessLocation.FindControl("rfvReferenceOrderNo");
        if (rfvDisposition.IsValid && rfvReferenceOrderNo.IsValid)
        {
            InProcessLocation ip = TheInProcessLocationMgr.LoadInProcessLocation(this.IpNo);

            try
            {
                ip.Disposition = ((TextBox)this.FV_InProcessLocation.FindControl("tbDisposition")).Text.Trim();
                ip.ReferenceOrderNo = ((TextBox)this.FV_InProcessLocation.FindControl("tbReferenceOrderNo")).Text.Trim();
                if (ip.Type == BusinessConstants.CODE_MASTER_INPROCESS_LOCATION_TYPE_VALUE_GAP)
                {
                    //gap直接关闭，不需要处理差异
                    TheInProcessLocationMgr.CloseInProcessLocation(ip, this.CurrentUser, false);
                }
                else
                {
                    TheInProcessLocationMgr.CloseInProcessLocation(ip, this.CurrentUser, true);
                    TheTransportationOrderMgr.TryCompleteTransportationOrder(ip, this.CurrentUser);
                }
                UpdateView();

            }
            catch (BusinessErrorException ex)
            {
                throw ex;
            }
        }
    }

    public void AdjustInProcessLocationFrom()
    {
        InProcessLocation ip = TheInProcessLocationMgr.LoadInProcessLocation(this.IpNo);

        try
        {
            if (this.AsnType == BusinessConstants.CODE_MASTER_INPROCESS_LOCATION_TYPE_VALUE_GAP)
            {
                TheInProcessLocationMgr.ResolveInPorcessLocationGap(ip, BusinessConstants.CODE_MASTER_GR_GAP_TO_GI, this.CurrentUser);
            }
            else if (this.AsnType == BusinessConstants.CODE_MASTER_INPROCESS_LOCATION_TYPE_VALUE_NORMAL)
            {
                TheInProcessLocationMgr.ResolveInPorcessLocationNormal(ip, BusinessConstants.CODE_MASTER_GR_GAP_TO_GI, this.CurrentUser);
            }
        }
        catch (BusinessErrorException ex)
        {
            throw (ex);
        }
    }

    public string AdjustInProcessLocationTo()
    {

        try
        {
            InProcessLocation ip = TheInProcessLocationMgr.LoadInProcessLocation(this.IpNo, true);
            Receipt receipt = new Receipt();
            InProcessLocation nmlIp = CloneHelper.DeepClone(ip);
            IList<InProcessLocationDetail> nmlReceiptDetailList = new List<InProcessLocationDetail>();
            foreach (InProcessLocationDetail ipdet in ip.InProcessLocationDetails)
            {
                #region 对应的ipdet
                InProcessLocationDetail nmlInProcessLocationDetail = new InProcessLocationDetail();
                nmlInProcessLocationDetail.Qty = 0 - ipdet.Qty;
                nmlInProcessLocationDetail.OrderLocationTransaction = TheOrderLocationTransactionMgr.GetOrderLocationTransaction(ipdet.OrderLocationTransaction.OrderDetail, BusinessConstants.IO_TYPE_IN)[0];
                nmlInProcessLocationDetail.IsConsignment = ipdet.IsConsignment;
                nmlInProcessLocationDetail.PlannedBill = ipdet.PlannedBill;
                nmlReceiptDetailList.Add(nmlInProcessLocationDetail);
                #endregion

                #region 收货单明细
                ReceiptDetail receiptDetail = new ReceiptDetail();
                receiptDetail.Receipt = receipt;
                receiptDetail.ReceivedQty = nmlInProcessLocationDetail.Qty;
                receiptDetail.ShippedQty = nmlInProcessLocationDetail.Qty;
                receiptDetail.OrderLocationTransaction = nmlInProcessLocationDetail.OrderLocationTransaction;
                receiptDetail.IsConsignment = ipdet.IsConsignment;
                nmlInProcessLocationDetail.PlannedBill = ipdet.PlannedBill;

                OrderDetail orderDetail = receiptDetail.OrderLocationTransaction.OrderDetail;

                OrderHead orderHead = orderDetail.OrderHead;

                if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
                {
                    PriceListDetail priceListDetail = ThePriceListDetailMgr.GetLastestPriceListDetail(orderDetail.DefaultPriceListFrom, orderDetail.Item, DateTime.Now, orderDetail.OrderHead.Currency);
                    if (priceListDetail == null)
                    {
                        throw new BusinessErrorException("Order.Error.NoPriceListReceipt", new string[] { orderDetail.Item.Code });
                    }
                    receiptDetail.PlannedAmount = priceListDetail.UnitPrice * (decimal)receiptDetail.ReceivedQty;
                }
                else if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
                {
                    PriceListDetail priceListDetail = ThePriceListDetailMgr.GetLastestPriceListDetail(orderDetail.DefaultPriceListTo, orderDetail.Item, DateTime.Now, orderDetail.OrderHead.Currency);
                    if (priceListDetail == null)
                    {
                        throw new BusinessErrorException("Order.Error.NoPriceListReceipt", new string[] { orderDetail.Item.Code });
                    }
                    receiptDetail.PlannedAmount = priceListDetail.UnitPrice * (decimal)receiptDetail.ReceivedQty;
                }

                receipt.AddReceiptDetail(receiptDetail);
                #endregion
            }


            nmlIp.InProcessLocationDetails = nmlReceiptDetailList;
            if (receipt.InProcessLocations == null)
            {
                receipt.InProcessLocations = new List<InProcessLocation>();
            }
            receipt.InProcessLocations.Add(nmlIp);

            TheReceiptMgr.CreateReceipt(receipt, this.CurrentUser);
            return receipt.ReceiptNo;
        }
        catch (BusinessErrorException ex)
        {
            throw (ex);
        }
    }

}
