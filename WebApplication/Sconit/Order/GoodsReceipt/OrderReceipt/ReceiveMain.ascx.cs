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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;
using com.Sconit.Entity.Production;
using com.Sconit.Entity.Procurement;
using com.Sconit.Service.Production;
using com.Sconit.Service.Distribution;

public partial class Order_GoodsReceipt_OrderReceipt_ReceiveMain : MainModuleBase
{
    public event EventHandler RefreshListEvent;
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

    public string InOutType
    {
        get
        {
            return (string)ViewState["InOutType"];
        }
        set
        {
            ViewState["InOutType"] = value;
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
    public bool IsOddCreateHu
    {
        get
        {
            return (bool)ViewState["IsOddCreateHu"];
        }
        set
        {
            ViewState["IsOddCreateHu"] = value;
        }
    }
    public bool IsGRCreateHu
    {
        get
        {
            return (bool)ViewState["IsGRCreateHu"];
        }
        set
        {
            ViewState["IsGRCreateHu"] = value;
        }
    }

    public bool IsNewItem
    {
        get
        {
            return (bool)ViewState["IsNewItem"];
        }
        set
        {
            ViewState["IsNewItem"] = value;
        }
    }

    public void InitPageParameter(string orderNo)
    {
        this.OrderNo = orderNo;
        OrderHead orderHead = TheOrderHeadMgr.LoadOrderHead(orderNo, true, false, true);
        this.IsOddCreateHu = orderHead.IsOddCreateHu;
        this.IsGRCreateHu = orderHead.CreateHuOption == BusinessConstants.CODE_MASTER_CREATE_HU_OPTION_VALUE_GR;
        this.ModuleSubType = orderHead.SubType;
        this.ucTabNavigator.Visible = true;
        this.ucDetailList.Visible = true;
        this.ucInLocTransList.Visible = false;
        this.ucInLocTransList.InLocTransList = null;

        this.ucOutLocTransList.Visible = false;
        this.IsNewItem = orderHead.IsNewItem;
        this.ucDetailList.InitPageParameter(orderNo);
        this.ucTabNavigator.IsNewItem = orderHead.IsNewItem;

        this.ucTabNavigator.UpdateView();
        this.ucNewItemInLocTransList.UpdateView();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucDetailList.ReceiveEvent += new System.EventHandler(this.ReceiveRender);
        this.ucConfirmInfo.ConfirmEvent += new System.EventHandler(this.ReceiveRender);
        this.ucTabNavigator.lbDetailClickEvent += new System.EventHandler(this.TabDetailClick_Render);
        this.ucTabNavigator.lbInLocTransEvent += new System.EventHandler(this.TabInLocTransClick_Render);
        this.ucTabNavigator.lbOutLocTransEvent += new System.EventHandler(this.TabOutLocTransClick_Render);
        this.ucTabNavigator.lbNewItemInLocTransEvent += new System.EventHandler(this.TabNewItemInLocTransClick_Render);


        if (!IsPostBack)
        {
            this.ucDetailList.ModuleType = this.ModuleType;
            this.ucInLocTransList.ModuleType = this.ModuleType;
            this.ucOutLocTransList.ModuleType = this.ModuleType;
        }
    }

    protected void btnReceive_Click(object sender, EventArgs e)
    {
        #region 更新投入的值
        if (this.ucInLocTransList.InLocTransList == null)
        {
            this.InOutType = BusinessConstants.IO_TYPE_IN;
            InitOrderLocTrans();
        }

        this.ucInLocTransList.InLocTransCallBack();

        this.ucInLocTransList.Visible = false;
        this.ucOutLocTransList.Visible = false;
        #endregion

        #region 检查明细是否为空及是否超单包装,每次只收一个成品
        OrderHead orderHead = this.ucDetailList.PopulateOrderHead();
        int detailCount = 0;
        bool allowExceedUC = bool.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_ALLOW_EXCEED_UC).Value);

        foreach (OrderDetail orderDetail in orderHead.OrderDetails)
        {
            if (orderDetail.CurrentReceiveQty != 0 || orderDetail.CurrentRejectQty != 0 || orderDetail.CurrentScrapQty != 0)
            {
                if (!allowExceedUC && orderDetail.UnitCount < orderDetail.CurrentReceiveQty)
                {
                    ShowErrorMessage("OrderDetail.Error.OrderDetailReceiveQtyExceedUC", orderDetail.Sequence.ToString());
                    return;
                }
                else
                {
                    detailCount++;
                }
            }

        }
        if (detailCount == 0)
        {
            ShowErrorMessage("OrderDetail.Error.OrderDetailReceiveEmpty");
            return;
        }

        bool isReceiptOneItem = bool.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_IS_RECEIPT_ONE_ITEM).Value);
        if (isReceiptOneItem && detailCount > 1)
        {
            ShowErrorMessage("OrderDetail.Error.OrderDetailReceiveItem");
            return;
        }
        #endregion

        #region 处理零头
        bool hasOdd = false;
        if (this.IsGRCreateHu)
        {
            foreach (OrderDetail orderDetail in orderHead.OrderDetails)
            {
                if (orderDetail.CurrentReceiveQty % orderDetail.UnitCount != 0)
                {
                    hasOdd = true;
                    break;
                }
            }
        }
        #endregion

        this.ucConfirmInfo.InitPageParameter(orderHead, hasOdd, this.IsOddCreateHu);
        this.ucConfirmInfo.Visible = true;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        BackEvent(sender, e);
    }

    private void ReceiveRender(object sender, EventArgs e)
    {
        bool isOddCreateHu = (bool)sender;

        IList<ReceiptDetail> receiptDetailList = this.ucDetailList.PopulateReceiptDetailList();
        bool isReceiptOneItem = bool.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_IS_RECEIPT_ONE_ITEM).Value);
        if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION && this.ModuleSubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_ADJ && isReceiptOneItem && receiptDetailList.Count > 1)
        {
            ShowErrorMessage("MasterData.Receipt.One.Item");
            return;
        }
        IList<OrderLocationTransaction> inLocTransList = new List<OrderLocationTransaction>();

        foreach (ReceiptDetail receiptDetail in receiptDetailList)
        {
            OrderLocationTransaction ol = receiptDetail.OrderLocationTransaction;
            ol.CurrentReceiveQty = ol.UnitQty * (receiptDetail.ReceivedQty.HasValue ? (decimal)receiptDetail.ReceivedQty : 0);
            ol.CurrentRejectQty = ol.UnitQty * (receiptDetail.RejectedQty.HasValue ? (decimal)receiptDetail.RejectedQty : 0);
            ol.CurrentScrapQty = ol.UnitQty * (receiptDetail.ScrapQty.HasValue ? (decimal)receiptDetail.ScrapQty : 0);
            inLocTransList.Add(ol);
        }

        IList<OrderLocationTransaction> orderLocTransList = this.ucInLocTransList.InLocTransList;  //投入原材料List
        IList<OrderLocationTransaction> outLocTransList = new List<OrderLocationTransaction>();     //非零原材料List
        IList<OrderLocationTransaction> rwoLocTransList = new List<OrderLocationTransaction>();    //返工成品List


        foreach (OrderLocationTransaction inLocTrans in this.ucInLocTransList.InLocTransList)
        {
            if (inLocTrans.BomDetail == null)
            {
                rwoLocTransList.Add(inLocTrans);
            }
            else if (inLocTrans.CurrentReceiveQty != 0)
            {
                outLocTransList.Add(inLocTrans);
            }
        }

        try
        {

            #region 正常生产收货
            if (outLocTransList.Count > 0)
            {
                foreach (ReceiptDetail receiptDetail in receiptDetailList)
                {
                    foreach (OrderLocationTransaction orderLocationTransaction in outLocTransList)
                    {
                        MaterialFlushBack materialFlushBack = new MaterialFlushBack();
                        materialFlushBack.RawMaterial = orderLocationTransaction.Item;
                        materialFlushBack.Uom = orderLocationTransaction.OrderDetail.Uom;
                        materialFlushBack.Operation = orderLocationTransaction.Operation;
                        materialFlushBack.Qty = orderLocationTransaction.CurrentReceiveQty;
                        IList<MaterialFlushBack> materialFlushBackList = TheMaterialFlushBackMgr.AssignMaterialFlushBack(materialFlushBack, inLocTransList);
                        foreach (MaterialFlushBack m in materialFlushBackList)
                        {
                            if (m.OrderLocationTransaction.OrderDetail.Id == receiptDetail.OrderLocationTransaction.OrderDetail.Id)
                            {
                                if (m.OrderLocationTransaction.UnitQty != 0)
                                {
                                    m.Qty = m.Qty / m.OrderLocationTransaction.UnitQty;
                                }
                                materialFlushBack = m;
                                receiptDetail.AddMaterialFlushBack(materialFlushBack);
                                break;
                            }
                        }
                    }
                }
            }
            #endregion


            #region 新品收货
            if (this.IsNewItem)
            {
                IList<TransformerDetail> transformerDetailList = this.ucNewItemInLocTransList.NewItemInLocTransList; //新品原材料List
                if (transformerDetailList.Count > 0)
                {
                    foreach (ReceiptDetail receiptDetail in receiptDetailList)
                    {
                        foreach (TransformerDetail transformerDetail in transformerDetailList)
                        {

                            MaterialFlushBack materialFlushBack = new MaterialFlushBack();
                            LocationLotDetail newItemLocationLotDetail = TheLocationLotDetailMgr.LoadLocationLotDetail(transformerDetail.LocationLotDetId);
                            materialFlushBack.RawMaterial = newItemLocationLotDetail.Item;
                            materialFlushBack.Uom = newItemLocationLotDetail.Hu.Uom;
                            materialFlushBack.Qty = newItemLocationLotDetail.Qty;
                            materialFlushBack.HuId = newItemLocationLotDetail.Hu.HuId;

                            IList<MaterialFlushBack> materialFlushBackList = TheMaterialFlushBackMgr.AssignMaterialFlushBack(materialFlushBack, inLocTransList);
                            foreach (MaterialFlushBack m in materialFlushBackList)
                            {
                                if (m.OrderLocationTransaction.OrderDetail.Id == receiptDetail.OrderLocationTransaction.OrderDetail.Id)
                                {
                                    if (m.OrderLocationTransaction.UnitQty != 0)
                                    {
                                        m.Qty = m.Qty / m.OrderLocationTransaction.UnitQty;
                                    }
                                    materialFlushBack = m;
                                    receiptDetail.AddMaterialFlushBack(materialFlushBack);
                                    break;
                                }
                            }

                        }
                    }
                }
            }
            #endregion

            #region 返工收货
            if (rwoLocTransList.Count > 0)
            {
                foreach (OrderLocationTransaction rwoOrderLocTran in rwoLocTransList)
                {
                    foreach (ReceiptDetail receiptDetail in receiptDetailList)
                    {

                        if (receiptDetail.OrderLocationTransaction.Item.Code == rwoOrderLocTran.Item.Code)
                        {
                            MaterialFlushBack materialFlushBack = new MaterialFlushBack();
                            materialFlushBack.RawMaterial = rwoOrderLocTran.Item;
                            materialFlushBack.Uom = rwoOrderLocTran.OrderDetail.Uom;
                            materialFlushBack.Operation = rwoOrderLocTran.Operation;
                            materialFlushBack.Qty = rwoOrderLocTran.CurrentReceiveQty;
                            IList<MaterialFlushBack> materialFlushBackList = TheMaterialFlushBackMgr.AssignMaterialFlushBack(materialFlushBack, inLocTransList);
                            foreach (MaterialFlushBack m in materialFlushBackList)
                            {
                                if (m.OrderLocationTransaction.OrderDetail.Id == receiptDetail.OrderLocationTransaction.OrderDetail.Id)
                                {
                                    if (m.OrderLocationTransaction.UnitQty != 0)
                                    {
                                        m.Qty = m.Qty / m.OrderLocationTransaction.UnitQty;
                                    }
                                    materialFlushBack = m;
                                    receiptDetail.AddMaterialFlushBack(materialFlushBack);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            #endregion


            Receipt receipt = TheOrderMgr.ReceiveOrder(receiptDetailList, this.CurrentUser, null, null, null, true, isOddCreateHu);
            OrderHead orderHead = TheOrderHeadMgr.LoadOrderHead(this.OrderNo);

            if (RefreshListEvent != null)
            {
                this.RefreshListEvent(new object[] { receipt.ReceiptNo, orderHead.NeedPrintReceipt }, e);
            }


        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }

    }

    private void InitOrderLocTrans()
    {
        bool isChanged = this.ucDetailList.GetChanged();
        OrderHead orderHead = this.ucDetailList.PopulateOrderHead();
        if (this.InOutType == BusinessConstants.IO_TYPE_IN)
        {
            this.ucInLocTransList.Visible = true;
            this.ucInLocTransList.InitPageParameter(orderHead, isChanged);
        }
        else
        {
            this.ucOutLocTransList.Visible = true;
            this.ucOutLocTransList.InitPageParameter(orderHead);
        }
    }

    private void TabDetailClick_Render(object sender, EventArgs e)
    {
        this.ucInLocTransList.InLocTransCallBack();
        this.ucDetailList.Visible = true;
        this.ucInLocTransList.Visible = false;
        this.ucNewItemInLocTransList.Visible = false;
        this.ucOutLocTransList.Visible = false;
        this.ucDetailList.SetChanged(false);
    }


    private void TabInLocTransClick_Render(object sender, EventArgs e)
    {

        this.ucInLocTransList.InLocTransCallBack();
        this.InOutType = BusinessConstants.IO_TYPE_IN;
        InitOrderLocTrans();
        this.ucDetailList.Visible = false;
        this.ucInLocTransList.Visible = true;
        this.ucNewItemInLocTransList.Visible = false;
        this.ucOutLocTransList.Visible = false;
        this.ucDetailList.SetChanged(false);
    }

    private void TabOutLocTransClick_Render(object sender, EventArgs e)
    {

        this.ucInLocTransList.InLocTransCallBack();
        this.InOutType = BusinessConstants.IO_TYPE_OUT;
        InitOrderLocTrans();
        this.ucDetailList.Visible = false;
        this.ucInLocTransList.Visible = false;
        this.ucNewItemInLocTransList.Visible = false;
        this.ucOutLocTransList.Visible = true;
        this.ucDetailList.SetChanged(false);
    }

    private void TabNewItemInLocTransClick_Render(object sender, EventArgs e)
    {
        this.ucDetailList.Visible = false;
        this.ucInLocTransList.Visible = false;
        this.ucOutLocTransList.Visible = false;
        this.ucNewItemInLocTransList.Visible = true;

    }
}
