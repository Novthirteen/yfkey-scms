using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Web;

public partial class Order_OrderIssue_HuList : ModuleBase
{
    public event EventHandler SaveEvent;
    public string FLowCode
    {
        get
        {
            return (string)ViewState["FLowCode"];
        }
        set
        {
            ViewState["FLowCode"] = value;
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
    protected OrderHead TheOrder
    {
        get
        {
            return (OrderHead)ViewState["OrderHead"];
        }
        set
        {
            ViewState["OrderHead"] = value;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        ucHuList.HuSaveEvent += new EventHandler(this.HuListSave_Render);

        if (this.ModuleType != BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT && this.ModuleType != BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER)
        {
            this.IsReject = false;
        }
    }

    public void InitPageParameter(OrderHead orderHead)
    {
        this.TheOrder = orderHead;
        InitialHuScan();
        if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            this.GV_List.Columns[7].Visible = false; //来源库位
        }
        if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            this.GV_List.Columns[8].Visible = false; //目的库位
        }

    }
    public void PageCleanup()
    {
        //清空ViewState缓存
        this.TheOrder = null;
        this.GV_List.DataSource = null;
        this.GV_List.DataBind();
    }
    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void tbHuScan_TextChanged(object sender, EventArgs e)
    {
        string huId = this.tbHuScan.Text.Trim();
        this.HuScan(huId);
    }

    private void HuScan(string huId)
    {
        Hu hu = TheHuMgr.LoadHu(huId);
        HuScan(hu);
    }

    private void HuScan(Hu hu)
    {
        if (hu == null)
        {
            this.lblMessage.Text = Resources.Language.MasterDataHuNotExist;
            return;
        }
        else
        {
            if (TheOrder.OrderDetails != null)
            {
                foreach (OrderDetail orderDetail in TheOrder.OrderDetails)
                {
                    if (orderDetail.HuId == hu.HuId)
                    {
                        this.lblMessage.Text = Resources.Language.MasterDataHuExist;
                        return;
                    }
                }
            }
            Flow flow = this.TheFlowMgr.LoadFlow(TheOrder.Flow);
            if (flow != null && !flow.AllowCreateDetail)
            {
                bool isMatch = false;
                if (TheOrder.OrderDetails != null)
                {
                    foreach (OrderDetail orderDetail in TheOrder.OrderDetails)
                    {
                        if (orderDetail.Item.Code == hu.Item.Code && orderDetail.Uom.Code == hu.Uom.Code)
                        {
                            if (!orderDetail.OrderHead.FulfillUnitCount || orderDetail.UnitCount == hu.UnitCount)
                            {
                                if (orderDetail.HuId != string.Empty && orderDetail.HuId != null)
                                {
                                    OrderDetail newOrderDetail = new OrderDetail();
                                    newOrderDetail.IsScanHu = true;
                                    int seqInterval = int.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_SEQ_INTERVAL).Value);

                                    int seq = int.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_SEQ_INTERVAL).Value);
                                    if (this.TheOrder.OrderDetails == null || this.TheOrder.OrderDetails.Count == 0)
                                    {
                                        newOrderDetail.Sequence = seqInterval;
                                    }
                                    else
                                    {
                                        newOrderDetail.Sequence = this.TheOrder.OrderDetails.Last<OrderDetail>().Sequence + seqInterval;
                                    }
                                    newOrderDetail.Item = orderDetail.Item;
                                    newOrderDetail.Uom = orderDetail.Uom;
                                    newOrderDetail.HuId = hu.HuId;
                                    newOrderDetail.HuQty = hu.Qty;
                                    if ((this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION && this.ModuleSubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_ADJ)
                                        || this.ModuleSubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RTN)
                                    {
                                        newOrderDetail.OrderedQty = -hu.Qty;
                                    }
                                    else
                                    {
                                        newOrderDetail.OrderedQty = hu.Qty;
                                    }
                                    newOrderDetail.LocationFrom = orderDetail.LocationFrom;
                                    if (this.IsReject)
                                    {
                                        newOrderDetail.LocationTo = TheLocationMgr.GetRejectLocation();

                                    }
                                    else
                                    {
                                        newOrderDetail.LocationTo = orderDetail.LocationTo;
                                    }
                                    newOrderDetail.ReferenceItemCode = orderDetail.ReferenceItemCode;
                                    newOrderDetail.UnitCount = orderDetail.UnitCount;
                                    newOrderDetail.PackageType = orderDetail.PackageType;
                                    newOrderDetail.OrderHead = orderDetail.OrderHead;
                                    newOrderDetail.IsScanHu = true;
                                    TheOrder.AddOrderDetail(newOrderDetail);
                                }

                                else
                                {
                                    orderDetail.IsScanHu = true;
                                    orderDetail.HuId = hu.HuId;
                                    orderDetail.HuQty = hu.Qty;
                                    if ((this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION && this.ModuleSubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_ADJ)
                                         || this.ModuleSubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RTN)
                                    {
                                        orderDetail.OrderedQty = -hu.Qty;
                                    }
                                    else
                                    {
                                        orderDetail.OrderedQty = hu.Qty;
                                    }
                                    if (this.IsReject)
                                    {
                                        orderDetail.LocationTo = TheLocationMgr.GetRejectLocation();

                                    }
                                }
                                isMatch = true;
                                break;
                            }
                        }
                    }
                }
                if (!isMatch)
                {
                    this.lblMessage.Text = Resources.Language.MasterDataFlowNotExistHuItem;
                    return;
                }

            }
            else
            {
                OrderDetail newOrderDetail = new OrderDetail();
                newOrderDetail.IsScanHu = true;
                int seqInterval = int.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_SEQ_INTERVAL).Value);

                int seq = int.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_SEQ_INTERVAL).Value);
                if (this.TheOrder.OrderDetails == null || this.TheOrder.OrderDetails.Count == 0)
                {
                    newOrderDetail.Sequence = seqInterval;
                }
                else
                {
                    newOrderDetail.Sequence = this.TheOrder.OrderDetails.Last<OrderDetail>().Sequence + seqInterval;
                }
                newOrderDetail.Item = hu.Item;
                newOrderDetail.Uom = hu.Uom;
                newOrderDetail.HuId = hu.HuId;
                newOrderDetail.HuQty = hu.Qty;
                if ((this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION && this.ModuleSubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_ADJ)
                              || this.ModuleSubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RTN)
                {
                    newOrderDetail.OrderedQty = -hu.Qty;
                }
                else
                {
                    newOrderDetail.OrderedQty = hu.Qty;
                }
                if (this.IsReject)
                {
                    newOrderDetail.LocationFrom = TheLocationMgr.GetRejectLocation();
                }
                else
                {
                    newOrderDetail.LocationFrom = TheOrder.LocationFrom;
                }
                newOrderDetail.LocationTo = TheOrder.LocationTo;
                newOrderDetail.UnitCount = hu.UnitCount;
                TheOrder.AddOrderDetail(newOrderDetail);
            }

            IList<OrderDetail> orderDetailList = new List<OrderDetail>();
            foreach (OrderDetail od in TheOrder.OrderDetails)
            {
                if (od.IsScanHu)
                {
                    orderDetailList.Add(od);
                }
            }

            this.GV_List.DataSource = orderDetailList;
            this.GV_List.DataBind();
            InitialHuScan();
        }

    }

    private void InitialHuScan()
    {
        this.lblMessage.Text = string.Empty;
        this.tbHuScan.Text = string.Empty;
        this.tbHuScan.Focus();
        this.GV_List.DataBind();
    }


    //保存
    public void SaveCallBack()
    {
        if (SaveEvent != null)
        {
            this.lblMessage.Text = string.Empty;

            if (this.TheOrder != null && this.TheOrder.OrderDetails != null)
            {
                for (int i = 0; i < this.TheOrder.OrderDetails.Count; i++)
                {
                    OrderDetail orderDetail = (OrderDetail)this.TheOrder.OrderDetails[i];
                    foreach (GridViewRow row in this.GV_List.Rows)
                    {
                        Label tbOrderQty = (Label)row.FindControl("tbQty");
                        Label lblHuId = (Label)row.FindControl("lblHuId");
                        if (lblHuId.Text.Trim() == orderDetail.HuId)
                        {
                            decimal orderQty = decimal.Parse(tbOrderQty.Text.Trim());
                            orderDetail.RequiredQty = orderQty;
                            orderDetail.OrderedQty = orderQty;
                            break;
                        }
                    }
                }
                SaveEvent(this.TheOrder, null);
            }
        }
    }

    protected void btnScanHu_Click(object sender, EventArgs e)
    {
        this.ucHuList.Visible = true;
        this.ucHuList.InitPageParameter();
    }

    void HuListSave_Render(object sender, EventArgs e)
    {
        IList<Hu> huList = (IList<Hu>)((object[])sender)[0];
        foreach (Hu hu in huList)
        {
            this.HuScan(hu);
        }
    }
}
