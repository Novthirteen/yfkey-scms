using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;

public partial class Order_GoodsReceipt_AdjustReceipt_AdjustMain : MainModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler AdjustEvent;

    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }
    private string ReceiptNo
    {
        get { return (string)ViewState["ReceiptNo"]; }
        set { ViewState["ReceiptNo"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ucList.ModuleType = this.ModuleType;
        }
    }


    public void InitPageParameter(string receiptNo)
    {
        Receipt receipt = TheReceiptMgr.LoadReceipt(receiptNo, true);
        this.ReceiptNo = receipt.ReceiptNo;
        this.ucEdit.InitPageParameter(receipt);
        this.ucList.InitPageParameter(receipt);
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            IList<TransformerDetail> transformerDetailList = this.ucList.PopulateTransformerDetailList();
            IList<OrderDetail> orderDetailList = new List<OrderDetail>();
            string currentFlow = string.Empty;

            foreach (TransformerDetail transformerDetail in transformerDetailList)
            {
                if (transformerDetail.CurrentQty != transformerDetail.AdjustQty)
                {
                    OrderLocationTransaction orderLocTrans = TheOrderLocationTransactionMgr.LoadOrderLocationTransaction(transformerDetail.OrderLocTransId);
                    if (currentFlow == string.Empty)
                    {
                        currentFlow = orderLocTrans.OrderDetail.OrderHead.Flow;
                    }
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.Item = TheItemMgr.LoadItem(transformerDetail.ItemCode);
                    orderDetail.Uom = TheUomMgr.LoadUom(transformerDetail.UomCode);
                    if (transformerDetail.HuId != null && transformerDetail.HuId.Trim() != string.Empty)
                    {
                        Hu hu = this.TheHuMgr.CheckAndLoadHu(transformerDetail.HuId.Trim());
                        orderDetail.OrderedQty = transformerDetail.AdjustQty - hu.Qty;
                    }
                    else
                    {
                        orderDetail.OrderedQty = transformerDetail.AdjustQty - transformerDetail.CurrentQty;
                    }
                    orderDetail.UnitCount = transformerDetail.UnitCount;
                    orderDetail.HuId = transformerDetail.HuId;
                    orderDetail.HuLotNo = transformerDetail.LotNo;
                    if (transformerDetail.LocationFromCode != null)
                    {
                        orderDetail.LocationFrom = TheLocationMgr.LoadLocation(transformerDetail.LocationFromCode);
                    }
                    if (transformerDetail.LocationToCode != null)
                    {
                        orderDetail.LocationTo = TheLocationMgr.LoadLocation(transformerDetail.LocationToCode);

                    }

                    orderDetailList.Add(orderDetail);
                }
            }
            if (orderDetailList.Count > 0)
            {
                Receipt receipt = TheOrderMgr.QuickReceiveOrder(currentFlow, orderDetailList, this.CurrentUser.Code, BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_ADJ, DateTime.Now, DateTime.Now, false, this.ReceiptNo, null);
                this.Visible = false;
                ShowSuccessMessage("MasterData.Receipt.Adjust.Successfully", this.ReceiptNo);
                if (AdjustEvent != null)
                {
                    AdjustEvent(receipt.ReceiptNo, e);
                }
            }
            else
            {
                ShowSuccessMessage("MasterData.Receipt.NoDetail.Adjust", this.ReceiptNo);
            }
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Visible = false;
        if (BackEvent != null)
        {
            BackEvent(this, null);
        }
    }

}
