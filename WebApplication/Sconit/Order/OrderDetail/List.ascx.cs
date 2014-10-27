using System;
using System.Collections;
using System.Linq;
using System.Web.UI.WebControls;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Utility;
using com.Sconit.Web;

public partial class Order_OrderDetail_List : ModuleBase
{
    public event EventHandler SaveEvent;
    public event EventHandler ShipEvent;
    public event EventHandler ReceiveEvent;
    public event EventHandler UpdateLocTransAndActBillEvent;

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

    private string NewOrEdit   //记录是新建订单还是编辑订单
    {
        get
        {
            return (string)ViewState["NewOrEdit"];
        }
        set
        {
            ViewState["NewOrEdit"] = value;
        }
    }

    private bool IsShowPrice   //记录是否显示价格
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

    private string OrderNo
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

    private int CurrentSeq
    {
        get
        {
            return (int)ViewState["CurrentSeq"];
        }
        set
        {
            ViewState["CurrentSeq"] = value;
        }
    }


    protected string PartyFromCode
    {
        get
        {
            return (string)ViewState["PartyFromCode"];
        }
        set
        {
            ViewState["PartyFromCode"] = value;
        }
    }
    protected string PartyToCode
    {
        get
        {
            return (string)ViewState["PartyToCode"];
        }
        set
        {
            ViewState["PartyToCode"] = value;
        }
    }
    protected string FlowCode
    {
        get
        {
            return (string)ViewState["FlowCode"];
        }
        set
        {
            ViewState["FlowCode"] = value;
        }
    }

    protected DateTime StartTime
    {
        get
        {
            return (DateTime)ViewState["StartTime"];
        }
        set
        {
            ViewState["StartTime"] = value;
        }
    }

    private string OrderStatus
    {
        get
        {
            return (string)ViewState["OrderStatus"];
        }
        set
        {
            ViewState["OrderStatus"] = value;
        }
    }


    protected string currencyCode = string.Empty;

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

    //供新增订单使用，由于还订单还没有保存，所以需要在ViewState中缓存OrderHead
    public void InitPageParameter(OrderHead orderHead)
    {
        this.NewOrEdit = "New";
        this.TheOrder = orderHead;
        this.FlowCode = orderHead.Flow;
        this.StartTime = orderHead.StartTime > DateTime.Now ? orderHead.StartTime : DateTime.Now;
        this.PartyFromCode = orderHead.PartyFrom.Code;
        this.PartyToCode = orderHead.PartyTo.Code;
        if (orderHead.Currency != null)
        {
            this.currencyCode = orderHead.Currency.Code;
        }
        int seqInterval = int.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_SEQ_INTERVAL).Value);

        IList<OrderDetail> orderDetailList = new List<OrderDetail>();
        IList<OrderDetail> oldrderDetailList = new List<OrderDetail>();
        if (orderHead.OrderDetails != null && orderHead.OrderDetails.Count > 0)
        {
            foreach (OrderDetail orderDetail in orderHead.OrderDetails)
            {
                if (OrderHelper.IsOrderDetailValid(orderDetail, orderHead.WindowTime))
                {
                    orderDetailList.Add(orderDetail);
                    oldrderDetailList.Add(orderDetail);
                }
            }
        }

        //经过有效期校验，明细会改变
        TheOrder.OrderDetails = oldrderDetailList;

        if (this.IsShowPrice && !orderHead.IsShowPrice)
        {
            this.IsShowPrice = false;
        }
        if (orderHead.AllowCreateDetail)
        {
            OrderDetail blankOrderDetail = new OrderDetail();
            int seq = int.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_SEQ_INTERVAL).Value);
            if (this.TheOrder.OrderDetails == null || this.TheOrder.OrderDetails.Count == 0)
            {
                blankOrderDetail.Sequence = seqInterval;
            }
            else
            {
                CurrentSeq = this.TheOrder.OrderDetails.Last<OrderDetail>().Sequence + seqInterval;
                blankOrderDetail.Sequence = CurrentSeq;
            }
            blankOrderDetail.IsBlankDetail = true;
            orderDetailList.Add(blankOrderDetail);
        }

        this.GV_List.DataSource = orderDetailList;
        this.GV_List.DataBind();

        UpdateView(this.TheOrder);
    }

    //供编辑订单使用，由于订单已经保存，为了减小ViewState大小，所以只在ViewState中缓存OrderNo
    public void InitPageParameter(string orderNo)
    {
        this.NewOrEdit = "Edit";
        this.OrderNo = orderNo;

        OrderHead orderHead = TheOrderHeadMgr.LoadOrderHead(this.OrderNo);
        this.ModuleSubType = orderHead.SubType;
        if (this.IsShowPrice && !orderHead.IsShowPrice)
        {
            this.IsShowPrice = false;
        }
        RefreshGv_List(orderHead.AllowCreateDetail);

    }

    //保存
    public void SaveCallBack()
    {
        if (this.NewOrEdit == "New")
        {
            if (SaveEvent != null)
            {
                if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
                {
                    this.TheOrder.DiscountFrom = tbOrderDiscount.Text.Trim() == string.Empty ? 0 : decimal.Parse(tbOrderDiscount.Text.Trim());
                }
                else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
                {
                    this.TheOrder.DiscountTo = tbOrderDiscount.Text.Trim() == string.Empty ? 0 : decimal.Parse(tbOrderDiscount.Text.Trim());
                }
                if (this.TheOrder != null && this.TheOrder.OrderDetails != null)
                {
                    for (int i = 0; i < this.TheOrder.OrderDetails.Count; i++)
                    {
                        OrderDetail orderDetail = (OrderDetail)this.TheOrder.OrderDetails[i];
                        GridViewRow row = this.GV_List.Rows[i];

                        TextBox tbOrderQty = (TextBox)row.FindControl("tbOrderQty");

                        if (tbOrderQty.Text.Trim() != string.Empty)
                        {
                            decimal d;

                            if (!decimal.TryParse(tbOrderQty.Text.Trim(), out d))
                            {
                                this.ShowErrorMessage("Common.Validator.Valid.Number");
                                return;
                            }

                            decimal orderQty = decimal.Parse(tbOrderQty.Text.Trim());
                            if (this.ModuleSubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML || this.ModuleSubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RWO)
                            {

                                if (orderQty < 0)
                                {
                                    this.ShowErrorMessage("MasterData.Order.OrderDetail.OrderQty.NML");
                                    return;
                                }
                            }
                            else if (this.ModuleSubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RTN)
                            {
                                if (orderQty > 0)
                                {
                                    this.ShowErrorMessage("MasterData.Order.OrderDetail.OrderQty.RTN");
                                    return;
                                }
                            }

                            orderDetail.RequiredQty = orderQty;
                            orderDetail.OrderedQty = orderQty;
                        }

                        TextBox tbDetailDiscount = (TextBox)row.FindControl("tbDiscount");
                        if (tbDetailDiscount.Text.Trim() != string.Empty)
                        {
                            if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
                            {
                                orderDetail.DiscountFrom = decimal.Parse(tbDetailDiscount.Text.Trim());
                            }
                            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
                            {
                                orderDetail.DiscountTo = decimal.Parse(tbDetailDiscount.Text.Trim());
                            }
                        }

                        if (this.IsReject)
                        {
                            orderDetail.LocationTo = TheLocationMgr.GetRejectLocation();
                        }
                        else
                        {
                            DropDownList ddlLocTo = (DropDownList)row.FindControl("ddlLocTo");
                            if (ddlLocTo.SelectedIndex != -1)
                            {
                                orderDetail.LocationTo = TheLocationMgr.LoadLocation(ddlLocTo.SelectedValue);
                            }
                        }

                        if (this.NewItem)
                        {
                            TextBox tbItemVersion = (TextBox)row.FindControl("tbItemVersion");
                            if (this.ModuleType == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION && tbItemVersion.Text.Trim() == string.Empty && orderDetail.OrderedQty != 0)
                            {
                                this.ShowErrorMessage("MasterData.Order.OrderDetail.ItemVersion.Empty");
                                return;
                            }
                            orderDetail.ItemVersion = tbItemVersion.Text.Trim();
                        }
                    }

                    SaveEvent(this.TheOrder, null);
                }
            }
        }
        else if (this.NewOrEdit == "Edit")
        {
            if (SaveEvent != null)
            {
                OrderHead orderHead = TheOrderHeadMgr.LoadOrderHead(this.OrderNo, true);
                if (orderHead != null)
                {
                    if (orderHead.OrderDetails != null)
                    {
                        for (int i = 0; i < orderHead.OrderDetails.Count; i++)
                        {
                            GridViewRow row = this.GV_List.Rows[i];

                            HiddenField hfId = (HiddenField)row.FindControl("hfId");
                            TextBox tbOrderQty = (TextBox)row.FindControl("tbOrderQty");
                            OrderDetail orderDetail = orderHead.GetOrderDetailById(int.Parse(hfId.Value));
                            decimal orderQty = Decimal.Parse(tbOrderQty.Text.Trim());

                            if (orderQty != 0)
                            {
                                orderDetail.RequiredQty = orderQty;
                                orderDetail.OrderedQty = orderQty;
                            }

                        }

                        SaveEvent(orderHead, null);
                    }
                }
                else
                {
                    ShowErrorMessage("MasterData.Order.NotExists", this.OrderNo);
                }
            }
        }
    }

    //发货
    public void ShipCallBack()
    {
        if (ShipEvent != null)
        {
            OrderHead orderHead = TheOrderHeadMgr.LoadOrderHead(this.OrderNo, true);
            if (orderHead != null)
            {
                if (orderHead.OrderDetails != null)
                {
                    for (int i = 0; i < this.GV_List.Rows.Count; i++)
                    {
                        GridViewRow row = this.GV_List.Rows[i];
                        HiddenField hfId = (HiddenField)row.FindControl("hfId");
                        TextBox tbShipQty = (TextBox)row.FindControl("tbShipQty");
                        OrderDetail orderDetail = orderHead.GetOrderDetailById(int.Parse(hfId.Value));
                        decimal shipQty = Decimal.Parse(tbShipQty.Text.Trim());
                        orderDetail.CurrentShipQty = shipQty;
                    }

                    ShipEvent(orderHead, null);
                }
            }
            else
            {
                ShowErrorMessage("MasterData.Order.NotExists", this.OrderNo);
            }
        }
    }

    //收货
    public void ReceiveCallBack()
    {
        if (ReceiveEvent != null)
        {

            ReceiveEvent(false, null);
        }
    }


    public OrderHead PopulateReceiveOrder()
    {
        OrderHead orderHead = TheOrderHeadMgr.LoadOrderHead(this.OrderNo, true);
        if (orderHead != null)
        {
            if (orderHead.OrderDetails != null)
            {
                for (int i = 0; i < this.GV_List.Rows.Count; i++)
                {
                    GridViewRow row = this.GV_List.Rows[i];
                    HiddenField hfId = (HiddenField)row.FindControl("hfId");
                    TextBox tbReceiveQty = (TextBox)row.FindControl("tbReceiveQty");
                    TextBox tbRejectQty = (TextBox)row.FindControl("tbRejectQty");
                    TextBox tbScrapQty = (TextBox)row.FindControl("tbScrapQty");

                    OrderDetail orderDetail = orderHead.GetOrderDetailById(int.Parse(hfId.Value));
                    decimal receiveQty = 0;
                    if (tbReceiveQty.Text.Trim() != string.Empty)
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

    public void PageCleanup()
    {
        //清空ViewState缓存
        this.TheOrder = null;
        this.OrderNo = null;
        this.OrderStatus = null;
        this.GV_List.DataSource = null;
        this.GV_List.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ucView.ModuleType = this.ModuleType;
            this.ucView.ModuleSubType = this.ModuleSubType;
            //默认取企业选项里控制价格
            this.IsShowPrice = bool.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_IS_SHOW_PRICE).Value);
            this.StartTime = DateTime.Now;

            if (this.ModuleType != BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT && this.ModuleType != BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_TRANSFER)
            {
                this.IsReject = false;
            }
            if (this.ModuleType != BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT && this.ModuleType != BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION && this.ModuleType != BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION)
            {
                this.NewItem = false;
            }
        }

    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        TextBox tbPrice = (TextBox)e.Row.FindControl("tbPrice");
        TextBox tbDiscount = (TextBox)e.Row.FindControl("tbDiscount");
        TextBox tbDiscountRate = (TextBox)e.Row.FindControl("tbDiscountRate");
        TextBox tbUnitPrice = ((TextBox)e.Row.FindControl("tbUnitPrice"));
        TextBox tbReceiveQty = ((TextBox)e.Row.FindControl("tbReceiveQty"));
        Label lblReferenceItemCode = ((Label)e.Row.FindControl("lblReferenceItemCode"));
        TextBox tbOrderQty = (TextBox)e.Row.FindControl("tbOrderQty");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            OrderDetail orderDetail = (OrderDetail)e.Row.DataItem;

            #region 处理新增行
            if (orderDetail.IsBlankDetail)
            {
                ((Label)e.Row.FindControl("lblSeq")).Visible = false;
                ((TextBox)e.Row.FindControl("tbSeq")).Visible = true;

                ((Label)e.Row.FindControl("lblItemDescription")).Visible = false;
                ((TextBox)e.Row.FindControl("tbItemDescription")).Visible = true;

                ((Label)e.Row.FindControl("lblItemCode")).Visible = false;

                Controls_TextBox tbItemCode = (Controls_TextBox)e.Row.FindControl("tbItemCode");
                tbItemCode.Visible = true;
                tbItemCode.SuggestTextBox.Attributes.Add("onchange", "GenerateFlowDetail(this);");

                ((Controls_TextBox)e.Row.FindControl("tbItemCode")).Visible = true;
                ((Label)e.Row.FindControl("lblReferenceItemCode")).Visible = false;

                ((Label)e.Row.FindControl("lblUom")).Visible = false;
                Controls_TextBox tbUom = (Controls_TextBox)e.Row.FindControl("tbUom");
                tbUom.Visible = true;
                tbUom.SuggestTextBox.Attributes.Add("onchange", "GetUnitPriceByUom(this);");

                ((Label)e.Row.FindControl("lblUnitCount")).Visible = false;
                ((TextBox)e.Row.FindControl("tbUnitCount")).Visible = true;

                ((Label)e.Row.FindControl("lblPackageType")).Visible = false;
                ((com.Sconit.Control.CodeMstrDropDownList)e.Row.FindControl("ddlPackageType")).Visible = true;

                Controls_TextBox tbRefItemCode = (Controls_TextBox)e.Row.FindControl("tbRefItemCode");
                tbRefItemCode.Visible = true;

                tbRefItemCode.ServiceParameter = "string:" + this.PartyFromCode + ",string:" + this.PartyToCode;

                tbRefItemCode.DataBind();
                tbRefItemCode.SuggestTextBox.Attributes.Add("onchange", "GenerateFlowDetailProxyByReferenceItem(this);");

                ((RequiredFieldValidator)e.Row.FindControl("rfvItemCode")).Enabled = true;
                ((RequiredFieldValidator)e.Row.FindControl("rfvUom")).Enabled = true;
                ((RequiredFieldValidator)e.Row.FindControl("rfvUC")).Enabled = true;
                ((RangeValidator)e.Row.FindControl("rvUC")).Enabled = true;

                ((LinkButton)e.Row.FindControl("lbtnAdd")).Visible = true;
                ((LinkButton)e.Row.FindControl("lbtnView")).Visible = false;
                ((LinkButton)e.Row.FindControl("lbtnDelete")).Visible = false;

                e.Row.FindControl("lblItemVersion").Visible = false;
                e.Row.FindControl("tbItemVersion").Visible = true;
            }
            #endregion

            #region 订单数量
            //只有状态为空或者Create时能够修改OrderQty
            if (!(this.OrderStatus == null || this.OrderStatus == string.Empty || this.OrderStatus == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE))
            {
                tbOrderQty.ReadOnly = true;
            }
            //orderqty默认值
            if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT || this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
            {
                ((TextBox)e.Row.FindControl("tbReceiveQty")).Text = orderDetail.RemainReceivedQty.ToString("0.########");
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                decimal shippedQty = orderDetail.ShippedQty == null ? 0 : (decimal)orderDetail.ShippedQty;
                ((TextBox)e.Row.FindControl("tbShipQty")).Text = (orderDetail.OrderedQty - shippedQty).ToString("0.########");
                if (orderDetail.ShippedQty != null && orderDetail.ShippedQty >= orderDetail.OrderedQty)
                {
                    ((TextBox)e.Row.FindControl("tbShipQty")).ReadOnly = true;
                }
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                string defaultReceiptOpt = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_DEFAULT_RECEIPT_OPTION).Value;

                tbReceiveQty.Text = OrderHelper.GetDefaultReceiptQty(orderDetail, defaultReceiptOpt).ToString();
            }

            #endregion

            #region 价格，折扣
            if (this.IsShowPrice)
            {
                if (orderDetail.IsBlankDetail)
                {
                    if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
                    {
                        tbDiscountRate.Text = orderDetail.DiscountRateFrom.ToString("F2");
                        decimal discountFrom = 0;
                        decimal unitPriceFrom = 0;
                        if (orderDetail.DiscountFrom != null)
                        {
                            discountFrom = (decimal)orderDetail.DiscountFrom;
                            tbDiscount.Text = discountFrom.ToString("F2");
                        }
                        if (orderDetail.PriceListDetailFrom != null && orderDetail.PriceListDetailFrom.UnitPrice != null)
                        {
                            unitPriceFrom = (decimal)orderDetail.PriceListDetailFrom.UnitPrice;
                            tbUnitPrice.Text = unitPriceFrom.ToString("F2");

                            tbPrice.Text = (unitPriceFrom * orderDetail.OrderedQty - discountFrom).ToString("F2");
                            tbPrice.Attributes["oldValue"] = tbPrice.Text;

                        }
                        if (orderDetail.ReferenceItemCode == null && orderDetail.ReferenceItemCode == string.Empty)
                        {
                            lblReferenceItemCode.Text = TheItemReferenceMgr.GetItemReferenceByItem(orderDetail.Item.Code, this.PartyFromCode, this.PartyToCode);
                        }
                    }
                    else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
                    {

                        tbDiscountRate.Text = orderDetail.DiscountRateTo.ToString("F2");
                        decimal discountTo = 0;
                        decimal unitPriceTo = 0;
                        if (orderDetail.DiscountTo != null)
                        {
                            discountTo = (decimal)orderDetail.DiscountTo;
                            tbDiscount.Text = discountTo.ToString("F2");

                        }
                        if (orderDetail.PriceListDetailTo != null && orderDetail.PriceListDetailTo.UnitPrice != null)
                        {
                            unitPriceTo = (decimal)orderDetail.PriceListDetailTo.UnitPrice;
                            tbUnitPrice.Text = unitPriceTo.ToString("F2");
                            tbPrice.Text = (unitPriceTo * orderDetail.OrderedQty - discountTo).ToString("F2");
                            tbPrice.Attributes["oldValue"] = tbPrice.Text;

                        }
                        if (orderDetail.ReferenceItemCode == null && orderDetail.ReferenceItemCode == string.Empty)
                        {
                            lblReferenceItemCode.Text = TheItemReferenceMgr.GetItemReferenceByItem(orderDetail.Item.Code, this.PartyToCode, this.PartyFromCode);
                        }
                    }

                }


                tbDiscount.Attributes.Add("onchange", "discountChanged(this," + orderDetail.IsBlankDetail.ToString() + ");");
                tbDiscountRate.Attributes.Add("onchange", "discountRateChanged(this," + orderDetail.IsBlankDetail.ToString().ToLower() + ");");
                tbOrderQty.Attributes.Add("onchange", "calcPrice(this," + orderDetail.IsBlankDetail.ToString().ToLower() + ");");

                if (!AllowEditPrice() || (this.OrderStatus != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE && this.OrderStatus != null && this.OrderStatus != string.Empty))
                {
                    tbDiscount.Attributes.Add("onfocus", "this.blur();");
                    tbDiscountRate.Attributes.Add("onfocus", "this.blur();");

                }
            }
            #endregion

            #region orderqty输入值的校验
            if (this.ModuleSubType != null && this.ModuleSubType != string.Empty)
            {
                RegularExpressionValidator revOrderQty = (RegularExpressionValidator)e.Row.FindControl("revOrderQty");
                revOrderQty.Enabled = true;
                if (this.ModuleSubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_NML || this.ModuleSubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RWO)
                {
                    revOrderQty.ValidationExpression = "^(0|([1-9]\\d*))(\\.\\d+)?$";
                    revOrderQty.ErrorMessage = "${MasterData.Order.OrderDetail.OrderQty.NML}";
                }
                else if (this.ModuleSubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RTN)
                {
                    revOrderQty.ValidationExpression = "^(-)(0|([1-9]\\d*))(\\.\\d+)?|0(\\.[0]*)?$";
                    revOrderQty.ErrorMessage = "${MasterData.Order.OrderDetail.OrderQty.RTN}";
                    tbReceiveQty.ReadOnly = true;

                }
                else if (this.ModuleSubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_ADJ)
                {
                    revOrderQty.ValidationExpression = "^(-)?(0|([1-9]\\d*))(\\.\\d+)?$";
                    revOrderQty.ErrorMessage = "${MasterData.Order.OrderDetail.OrderQty.ADJ}";
                }
            }
            #endregion

            #region 退货
            if (this.OrderStatus == null || this.OrderStatus == string.Empty || this.OrderStatus == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
            {
                if (!orderDetail.IsBlankDetail && this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT && this.ModuleSubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RTN)
                {
                    DropDownList ddlLocationTo = (DropDownList)e.Row.FindControl("ddlLocTo");
                    Label lblLocTo = (Label)e.Row.FindControl("lblLocTo");

                    if (this.IsReject)
                    {
                        ddlLocationTo.Visible = false;
                        lblLocTo.Visible = true;
                        lblLocTo.Text = TheLocationMgr.GetRejectLocation().Name;
                    }

                    else
                    {
                        ddlLocationTo.Visible = true;
                        lblLocTo.Visible = false;
                        ddlLocationTo.DataSource = GetReturnLocationTo(orderDetail);
                        ddlLocationTo.DataBind();
                        ddlLocationTo.SelectedValue = orderDetail.DefaultLocationTo.Code;
                    }
                }
            }
            #endregion

            #region 新品

            if (!(this.OrderStatus == null || this.OrderStatus == string.Empty))
            {
                e.Row.FindControl("lblItemVersion").Visible = true;
                e.Row.FindControl("tbItemVersion").Visible = false;
            }
            else
            {
                e.Row.FindControl("lblItemVersion").Visible = false;
                e.Row.FindControl("tbItemVersion").Visible = true;
            }
            #endregion
        }
    }

    private IList<Location> GetReturnLocationTo(OrderDetail orderDetail)
    {
        IList<Location> locationList = new List<Location>();
        if (orderDetail.OrderHead.LocationTo != null)
        {
            locationList.Add(orderDetail.OrderHead.LocationTo);
        }
        if (orderDetail.LocationTo != null)
        {
            if (locationList.Count == 0 || locationList[0].Code != orderDetail.LocationTo.Code)
            {
                locationList.Add(orderDetail.LocationTo);
            }
        }

        return locationList;
    }

    //添加
    protected void lbtnAdd_Click(object sender, EventArgs e)
    {
        if (CheckItemExists())
        {
            return;
        }
        IList<OrderDetail> orderDetailList = GetOrderDetailList();
        int newRowId = orderDetailList != null ? orderDetailList.Count : 0;
        GridViewRow newRow = this.GV_List.Rows[newRowId];
        RequiredFieldValidator rfvItemCode = (RequiredFieldValidator)newRow.FindControl("rfvItemCode");
        RequiredFieldValidator rfvUom = (RequiredFieldValidator)newRow.FindControl("rfvUom");
        CustomValidator cvItemCheck = (CustomValidator)newRow.FindControl("cvItemCheck");
        if (rfvItemCode.IsValid && rfvUom.IsValid)
        {
            //将上面输入的数量和折扣值保存
            if (orderDetailList != null)
            {
                UpdateSavedOrderHead();

                #region 新增明细
                int currentRow = orderDetailList == null ? 0 : orderDetailList.Count;
                GridViewRow newOrderDetailRow = this.GV_List.Rows[currentRow];
                TextBox tbNewSeq = (TextBox)newOrderDetailRow.FindControl("tbSeq");
                Controls_TextBox tbNewItemCode = (Controls_TextBox)newOrderDetailRow.FindControl("tbItemCode");
                Controls_TextBox tbNewUom = (Controls_TextBox)newOrderDetailRow.FindControl("tbUom");
                com.Sconit.Control.CodeMstrDropDownList ddlPackageType = (com.Sconit.Control.CodeMstrDropDownList)newOrderDetailRow.FindControl("ddlPackageType");
                TextBox tbNewUnitCount = (TextBox)newOrderDetailRow.FindControl("tbUnitCount");
                TextBox tbHuLotSize = (TextBox)newOrderDetailRow.FindControl("tbHuLotSize");
                TextBox tbNewOrderQty = (TextBox)newOrderDetailRow.FindControl("tbOrderQty");
                TextBox tbNewUnitPrice = (TextBox)newOrderDetailRow.FindControl("tbUnitPrice");
                TextBox tbNewDiscount = (TextBox)newOrderDetailRow.FindControl("tbDiscount");
                HiddenField hfPriceListCode = (HiddenField)newOrderDetailRow.FindControl("hfPriceListCode");
                HiddenField hfPriceListDetailId = (HiddenField)newOrderDetailRow.FindControl("hfPriceListDetailId");
                HiddenField tbPackageVolumn = (HiddenField)newOrderDetailRow.FindControl("hfPackageVolumn");

                int seqInterval = int.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_SEQ_INTERVAL).Value);
                decimal orderQty = tbNewOrderQty.Text.Trim() == string.Empty ? 0 : decimal.Parse(tbNewOrderQty.Text.Trim());
                decimal unitPrice = tbNewUnitPrice.Text.Trim() == string.Empty ? 0 : decimal.Parse(tbNewUnitPrice.Text.Trim());
                decimal discount = tbNewDiscount.Text.Trim() == string.Empty ? 0 : decimal.Parse(tbNewDiscount.Text.Trim());
                decimal orderDetailPrice = this.tbOrderDetailPrice.Text.Trim() == string.Empty ? 0 : decimal.Parse(this.tbOrderDetailPrice.Text.Trim());
                int? huLotSize = null;
                if (tbHuLotSize.Text.Trim() != string.Empty)
                {
                    huLotSize = int.Parse(tbHuLotSize.Text.Trim());
                }
                else if (tbNewUnitCount.Text.Trim() != string.Empty)
                {
                    huLotSize = Convert.ToInt32(decimal.Parse(tbNewUnitCount.Text.Trim()));
                }
                decimal? packageVolumn = null;
                if (tbPackageVolumn.Value != string.Empty)
                {
                    packageVolumn = decimal.Parse(tbPackageVolumn.Value);
                }

                IList<Item> newItemList = new List<Item>(); //待新增明细列表
                Item newItem = this.TheItemMgr.LoadItem(tbNewItemCode.Text.Trim());
                Uom newUom = this.TheUomMgr.LoadUom(tbNewUom.Text.Trim());
                decimal? convertRate = null;
                IList<ItemKit> itemKitList = null;
                if (newItem.Type == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_K)
                {
                    itemKitList = this.TheItemKitMgr.GetChildItemKit(newItem);
                    foreach (ItemKit itemKit in itemKitList)
                    {
                        if (!convertRate.HasValue)
                        {
                            if (itemKit.ParentItem.Uom.Code != newUom.Code)
                            {
                                convertRate = this.TheUomConversionMgr.ConvertUomQty(newItem, newUom, 1, itemKit.ParentItem.Uom);
                            }
                            else
                            {
                                convertRate = 1;
                            }
                        }

                        newItemList.Add(itemKit.ChildItem);
                    }
                }
                else
                {
                    newItemList.Add(newItem);
                }

                for (int i = 0; i < newItemList.Count; i++)
                {
                    Item item = newItemList[i];
                    OrderDetail newOrderDetail = new OrderDetail();

                    newOrderDetail.OrderHead = GetOrderHead();
                    newOrderDetail.Sequence = (tbNewSeq.Text.Trim() == string.Empty ? this.CurrentSeq : int.Parse(tbNewSeq.Text.Trim()) + i * seqInterval);
                    newOrderDetail.IsBlankDetail = false;
                    newOrderDetail.Item = item;
                    newOrderDetail.PackageVolumn = packageVolumn;

                    if (newOrderDetail.Item.Code != newItem.Code)
                    {
                        //套件
                        ItemKit thisItemKit = null;
                        foreach (ItemKit itemKit in itemKitList)
                        {
                            if (itemKit.ChildItem.Code == item.Code)
                            {
                                thisItemKit = itemKit;
                                break;
                            }
                        }

                        newOrderDetail.Uom = item.Uom;
                        newOrderDetail.UnitCount = item.UnitCount * thisItemKit.Qty * convertRate.Value;
                        newOrderDetail.HuLotSize = Convert.ToInt32(newOrderDetail.UnitCount);
                        newOrderDetail.OrderedQty = orderQty * thisItemKit.Qty * convertRate.Value;
                        newOrderDetail.PackageType = ddlPackageType.SelectedValue;

                        #region 价格字段
                        if (this.IsShowPrice)
                        {
                            if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
                            {
                                if (hfPriceListCode.Value != string.Empty)
                                {
                                    newOrderDetail.PriceListFrom = ThePurchasePriceListMgr.LoadPurchasePriceList(hfPriceListCode.Value);
                                }
                                if (newOrderDetail.PriceListFrom != null)
                                {
                                    newOrderDetail.PriceListDetailFrom = this.ThePriceListDetailMgr.GetLastestPriceListDetail(newOrderDetail.PriceListFrom, item, DateTime.Now, newOrderDetail.OrderHead.Currency, item.Uom);
                                }
                            }
                            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
                            {
                                if (hfPriceListCode.Value != string.Empty)
                                {
                                    newOrderDetail.PriceListTo = TheSalesPriceListMgr.LoadSalesPriceList(hfPriceListCode.Value);
                                }
                                if (newOrderDetail.PriceListTo != null)
                                {
                                    newOrderDetail.PriceListDetailTo = this.ThePriceListDetailMgr.GetLastestPriceListDetail(newOrderDetail.PriceListTo, item, DateTime.Now, newOrderDetail.OrderHead.Currency, item.Uom);
                                }
                            }

                        }
                        #endregion
                    }
                    else
                    {
                        newOrderDetail.Uom = newUom;
                        newOrderDetail.UnitCount = tbNewUnitCount.Text.Trim() == string.Empty ? 0 : decimal.Parse(tbNewUnitCount.Text.Trim());
                        newOrderDetail.HuLotSize = huLotSize;
                        newOrderDetail.OrderedQty = orderQty;
                        newOrderDetail.PackageType = ddlPackageType.SelectedValue;

                        #region 价格字段
                        if (this.IsShowPrice)
                        {
                            if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
                            {
                                newOrderDetail.DiscountFrom = discount;
                                if (hfPriceListCode.Value != string.Empty)
                                {
                                    newOrderDetail.PriceListFrom = ThePurchasePriceListMgr.LoadPurchasePriceList(hfPriceListCode.Value);
                                }
                                if (hfPriceListDetailId.Value != string.Empty)
                                {
                                    newOrderDetail.PriceListDetailFrom = ThePriceListDetailMgr.LoadPriceListDetail(Int32.Parse(hfPriceListDetailId.Value));
                                }

                            }
                            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
                            {
                                newOrderDetail.DiscountTo = discount;
                                if (hfPriceListCode.Value != string.Empty)
                                {
                                    newOrderDetail.PriceListTo = TheSalesPriceListMgr.LoadSalesPriceList(hfPriceListCode.Value);
                                }
                                if (hfPriceListDetailId.Value != string.Empty)
                                {
                                    newOrderDetail.PriceListDetailTo = ThePriceListDetailMgr.LoadPriceListDetail(Int32.Parse(hfPriceListDetailId.Value));
                                }
                            }

                            //更新总价                        
                            this.tbOrderDetailPrice.Text = (orderDetailPrice + unitPrice * orderQty - discount).ToString();
                        }
                        #endregion
                    }

                    #region 新品
                    if (this.NewItem)
                    {
                        TextBox tbItemVersion = (TextBox)newOrderDetailRow.FindControl("tbItemVersion");
                        newOrderDetail.ItemVersion = tbItemVersion.Text.Trim();
                    }
                    #endregion

                    if (this.IsReject)
                    {
                        newOrderDetail.LocationTo = TheLocationMgr.GetRejectLocation();
                    }

                    if (this.NewOrEdit == "New")
                    {
                        this.TheOrder.AddOrderDetail(newOrderDetail);
                        InitPageParameter(this.TheOrder);
                    }
                    else if (this.NewOrEdit == "Edit")
                    {
                        TheOrderMgr.AddOrderDetail(newOrderDetail, this.CurrentUser);
                        InitPageParameter(this.OrderNo);


                    }
                }
                #endregion
            }
        }
    }

    private void UpdateSavedOrderHead()
    {
        IList<OrderDetail> orderDetailList = GetOrderDetailList();
        if (orderDetailList != null)
        {
            for (int i = 0; i < orderDetailList.Count; i++)
            {
                OrderDetail orderDetail = orderDetailList[i];
                GridViewRow row = this.GV_List.Rows[i];

                TextBox tbOrderQty = (TextBox)row.FindControl("tbOrderQty");
                TextBox tbDiscount = (TextBox)row.FindControl("tbDiscount");


                if (tbOrderQty.Text != string.Empty)
                {
                    orderDetail.RequiredQty = Decimal.Parse(tbOrderQty.Text.Trim());
                    orderDetail.OrderedQty = Decimal.Parse(tbOrderQty.Text.Trim());
                }
                if (tbDiscount.Text != string.Empty)
                {
                    if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
                    {
                        orderDetail.DiscountFrom = Decimal.Parse(tbDiscount.Text.Trim());
                    }
                    else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
                    {
                        orderDetail.DiscountTo = Decimal.Parse(tbDiscount.Text.Trim());
                    }
                }
                if ((this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT || this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER) && this.ModuleSubType == BusinessConstants.CODE_MASTER_ORDER_SUB_TYPE_VALUE_RTN)
                {
                    if (this.IsReject)
                    {
                        orderDetail.LocationTo = TheLocationMgr.GetRejectLocation();
                    }
                    else
                    {

                        DropDownList ddlLocTo = (DropDownList)row.FindControl("ddlLocTo");
                        if (ddlLocTo.SelectedIndex != -1)
                        {
                            orderDetail.LocationTo = TheLocationMgr.LoadLocation(ddlLocTo.SelectedValue);
                        }
                    }
                }
                if (this.NewItem)
                {
                    TextBox tbItemVersion = (TextBox)row.FindControl("tbItemVersion");
                    orderDetail.ItemVersion = tbItemVersion.Text.Trim();
                }

            }
        }

    }

    protected void lbtnView_Click(object sender, EventArgs e)
    {

        if (this.NewOrEdit == "New")
        {
            int rowIndex = ((GridViewRow)(((DataControlFieldCell)(((LinkButton)(sender)).Parent)).Parent)).RowIndex;
            this.ucView.Visible = true;
            this.ucView.InitPageParameterForView(TheOrder.OrderDetails[rowIndex]);
        }
        else if (this.NewOrEdit == "Edit")
        {
            int orderDetailId = int.Parse(((LinkButton)sender).CommandArgument);
            this.ucView.Visible = true;
            this.ucView.InitPageParameterForView(TheOrderDetailMgr.LoadOrderDetail(orderDetailId));
        }
    }

    //删除
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {

        if (this.NewOrEdit == "New")
        {
            int rowIndex = ((GridViewRow)(((DataControlFieldCell)(((LinkButton)(sender)).Parent)).Parent)).RowIndex;
            TheOrder.RemoveOrderDetailByRowIndex(rowIndex);
            UpdateSavedOrderHead();
            if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT || this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                GridViewRow newOrderDetailRow = this.GV_List.Rows[rowIndex];
                TextBox tbPrice = (TextBox)newOrderDetailRow.FindControl("tbPrice");
                decimal totalPrice = tbOrderDetailPrice.Text.Trim() == string.Empty ? 0 : decimal.Parse(tbOrderDetailPrice.Text.Trim());
                decimal price = tbPrice.Text.Trim() == string.Empty ? 0 : decimal.Parse(tbPrice.Text.Trim());
                tbOrderDetailPrice.Text = (totalPrice - price).ToString();
            }
            InitPageParameter(this.TheOrder);
        }
        else if (this.NewOrEdit == "Edit")
        {
            int orderDetailId = int.Parse(((LinkButton)sender).CommandArgument);
            try
            {
                TheOrderMgr.DeleteOrderDetail(orderDetailId, this.CurrentUser);
                this.ShowSuccessMessage("MasterData.Order.OrderDetail.DeleteOrderDetail.Successfully");
                RefreshGv_List(true);
                UpdateLocTransAndActBillEvent(this.OrderNo, e);
            }
            catch (BusinessErrorException ex)
            {
                this.ShowErrorMessage(ex);
            }
        }
    }

    private void RefreshGv_List(bool showNewDetail)
    {
        if (this.NewOrEdit == "Edit")
        {
            OrderHead orderHead = TheOrderHeadMgr.LoadOrderHead(this.OrderNo, true);
            this.StartTime = orderHead.StartTime;
            this.FlowCode = orderHead.Flow;
            if (orderHead.Currency != null)
            {
                this.currencyCode = orderHead.Currency.Code;
            }
            this.PartyFromCode = orderHead.PartyFrom.Code;
            this.PartyToCode = orderHead.PartyTo.Code;
            if (orderHead != null)
            {
                this.OrderStatus = orderHead.Status;
                if (orderHead.OrderDetails != null)
                {
                    IList<OrderDetail> orderDetailList = orderHead.OrderDetails;
                    if (this.OrderStatus == null || this.OrderStatus == string.Empty || this.OrderStatus == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
                    {
                        if (showNewDetail)
                        {
                            OrderDetail blankOrderDetail = new OrderDetail();
                            blankOrderDetail.IsBlankDetail = true;
                            int seq = int.Parse(TheEntityPreferenceMgr.LoadEntityPreference("SeqInterval").Value);
                            if (orderHead.OrderDetails == null || orderHead.OrderDetails.Count == 0)
                            {
                                blankOrderDetail.Sequence = seq;
                            }
                            else
                            {
                                CurrentSeq = orderHead.OrderDetails.Last<OrderDetail>().Sequence + seq;
                                blankOrderDetail.Sequence = CurrentSeq;
                            }
                            orderDetailList.Add(blankOrderDetail);
                        }
                    }
                    this.GV_List.DataSource = orderDetailList;
                    this.GV_List.DataBind();
                }
                UpdateView(orderHead);
            }
            else
            {
                ShowErrorMessage("MasterData.Order.NotExists", this.OrderNo);
            }
        }
    }

    private void UpdateView(OrderHead orderHead)
    {
        HiddenPriceColumn(orderHead);
        //根据订单类型隐藏相应列
        HiddenGVColumn(orderHead);
        if (orderHead == null || orderHead.OrderDetails == null || orderHead.OrderDetails.Count == 0)
        {
            this.tabPrice.Visible = false;
        }
    }

    //控制价格选项
    private void HiddenPriceColumn(OrderHead orderHead)
    {

        this.tabPrice.Visible = false;              //订单折扣，总价
        this.GV_List.Columns[20].Visible = false;  //单价
        this.GV_List.Columns[21].Visible = false;  //折扣率
        this.GV_List.Columns[22].Visible = false;  //折扣金额
        this.GV_List.Columns[23].Visible = false;  //总金额

        if (!AllowEditPrice() || (this.OrderStatus != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE && this.OrderStatus != null && this.OrderStatus != string.Empty))
        {

            this.tbOrderDiscount.Attributes.Add("onfocus", "this.blur();");
            this.tbOrderDiscountRate.Attributes.Add("onfocus", "this.blur();");
        }

        //企业选项和Flow选项
        if (IsShowPrice && GetOrderHead().IsShowPrice)
        {
            //控制价格字段
            if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION || this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
            {
                this.tabPrice.Visible = true;              //订单折扣，总价
                this.GV_List.Columns[20].Visible = true;  //单价
                this.GV_List.Columns[21].Visible = true;  //折扣率
                this.GV_List.Columns[22].Visible = true;  //折扣金额
                this.GV_List.Columns[23].Visible = true;  //总金额

                if (this.NewOrEdit == "Edit")
                {
                    if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
                    {
                        tbOrderDetailPrice.Text = orderHead.TotalOrderDetailAmountFrom.ToString("F2");
                        if (orderHead.DiscountFrom != null && orderHead.DiscountRateFrom != null)
                        {
                            tbOrderDiscount.Text = ((decimal)orderHead.DiscountFrom).ToString("F2");
                            tbOrderDiscountRate.Text = ((decimal)orderHead.DiscountRateFrom).ToString("F2");
                        }
                        tbOrderPrice.Text = orderHead.OrderAmountFrom.ToString("F2");
                    }
                    else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
                    {
                        tbOrderDetailPrice.Text = orderHead.TotalOrderDetailAmountTo.ToString("F2");
                        if (orderHead.DiscountTo != null && orderHead.DiscountRateTo != null)
                        {
                            tbOrderDiscount.Text = ((decimal)orderHead.DiscountTo).ToString("F2");
                            tbOrderDiscountRate.Text = ((decimal)orderHead.DiscountRateTo).ToString("F2");
                        }
                        tbOrderPrice.Text = orderHead.OrderAmountTo.ToString("F2");
                    }
                }

            }
        }
    }

    //根据订单类型隐藏相应列
    private void HiddenGVColumn(OrderHead orderHead)
    {
        this.GV_List.Columns[7].Visible = true;  //外包装
        
        #region 按flowtype显示
        if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            this.GV_List.Columns[7].Visible = true;  //外包装
            this.GV_List.Columns[9].Visible = false;  //来源库位
            this.GV_List.Columns[11].Visible = false;  //物料清单
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            this.GV_List.Columns[10].Visible = false;  //目的库位
            this.GV_List.Columns[11].Visible = false;  //物料清单
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        {
            this.GV_List.Columns[9].Visible = false;  //来源库位
        }

        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
        {
            this.GV_List.Columns[11].Visible = false;  //物料清单
        }

        #endregion

        #region 数量字段
        if (orderHead.Status == null ||
            orderHead.Status == string.Empty ||
            orderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
        {
            this.GV_List.Columns[14].Visible = false;  //已发数量
            this.GV_List.Columns[15].Visible = false;  //发货数量
            this.GV_List.Columns[16].Visible = false;  //已收数量
            this.GV_List.Columns[17].Visible = false;  //收货数量
            this.GV_List.Columns[18].Visible = false;  //次品数量
            this.GV_List.Columns[19].Visible = false;  //废品数量
            this.GV_List.Columns[24].Visible = true;   //操作按钮
        }
        else if (orderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT)
        {
            this.GV_List.Columns[14].Visible = false;  //已发数量
            this.GV_List.Columns[15].Visible = false;  //发货数量
            this.GV_List.Columns[16].Visible = false;  //已收数量
            this.GV_List.Columns[17].Visible = false;  //收货数量
            this.GV_List.Columns[18].Visible = false;  //次品数量
            this.GV_List.Columns[19].Visible = false;  //废品数量
            this.GV_List.Columns[24].Visible = false;   //操作按钮
        }
        else if (orderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL)
        {
            this.GV_List.Columns[14].Visible = false;  //已发数量
            this.GV_List.Columns[15].Visible = false;  //发货数量
            this.GV_List.Columns[16].Visible = false;  //已收数量
            this.GV_List.Columns[17].Visible = false;  //收货数量
            this.GV_List.Columns[18].Visible = false;  //次品数量
            this.GV_List.Columns[19].Visible = false;  //废品数量
            this.GV_List.Columns[24].Visible = false;   //操作按钮
        }
        else if (orderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
        {
            if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
            {
                this.GV_List.Columns[14].Visible = true;  //已发数量
                this.GV_List.Columns[15].Visible = false;  //发货数量
                this.GV_List.Columns[16].Visible = true;   //已收数量
                this.GV_List.Columns[17].Visible = true;   //收货数量
                this.GV_List.Columns[18].Visible = false;  //次品数量
                this.GV_List.Columns[19].Visible = false;  //废品数量
                this.GV_List.Columns[24].Visible = false;   //操作按钮
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                this.GV_List.Columns[14].Visible = true;   //已发数量
                this.GV_List.Columns[15].Visible = true;   //发货数量
                this.GV_List.Columns[16].Visible = false;  //已收数量
                this.GV_List.Columns[17].Visible = false;  //收货数量
                this.GV_List.Columns[18].Visible = false;  //次品数量
                this.GV_List.Columns[19].Visible = false;  //废品数量
                this.GV_List.Columns[24].Visible = false;   //操作按钮
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                this.GV_List.Columns[14].Visible = false;  //已发数量
                this.GV_List.Columns[15].Visible = false;  //发货数量
                this.GV_List.Columns[16].Visible = true;  //已收数量
                this.GV_List.Columns[17].Visible = true;  //收货数量
                this.GV_List.Columns[18].Visible = true;  //次品数量
                this.GV_List.Columns[19].Visible = true;  //废品数量
                this.GV_List.Columns[24].Visible = false;   //操作按钮
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
            {
                this.GV_List.Columns[14].Visible = false;  //已发数量
                this.GV_List.Columns[15].Visible = false;  //发货数量
                this.GV_List.Columns[16].Visible = false;  //已收数量
                this.GV_List.Columns[17].Visible = true;   //收货数量
                this.GV_List.Columns[18].Visible = false;  //次品数量
                this.GV_List.Columns[19].Visible = false;  //废品数量
                this.GV_List.Columns[24].Visible = false;   //操作按钮
            }
        }
        else if (orderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE)
        {
            if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
            {
                this.GV_List.Columns[14].Visible = false;  //已发数量
                this.GV_List.Columns[15].Visible = false;  //发货数量
                this.GV_List.Columns[16].Visible = true;   //已收数量
                this.GV_List.Columns[17].Visible = false;  //收货数量
                this.GV_List.Columns[18].Visible = false;  //次品数量
                this.GV_List.Columns[19].Visible = false;  //废品数量
                this.GV_List.Columns[24].Visible = false;   //操作按钮
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                this.GV_List.Columns[14].Visible = true;   //已发数量
                this.GV_List.Columns[15].Visible = false;  //发货数量
                this.GV_List.Columns[16].Visible = false;  //已收数量
                this.GV_List.Columns[17].Visible = false;  //收货数量
                this.GV_List.Columns[18].Visible = false;  //次品数量
                this.GV_List.Columns[19].Visible = false;  //废品数量
                this.GV_List.Columns[24].Visible = false;   //操作按钮
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                this.GV_List.Columns[14].Visible = false;  //已发数量
                this.GV_List.Columns[15].Visible = false;  //发货数量
                this.GV_List.Columns[16].Visible = true;   //已收数量
                this.GV_List.Columns[17].Visible = false;  //收货数量
                this.GV_List.Columns[18].Visible = false;  //次品数量
                this.GV_List.Columns[19].Visible = false;  //废品数量
                this.GV_List.Columns[24].Visible = false;   //操作按钮
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
            {
                this.GV_List.Columns[14].Visible = false;  //已发数量
                this.GV_List.Columns[15].Visible = false;  //发货数量
                this.GV_List.Columns[16].Visible = true;   //已收数量
                this.GV_List.Columns[17].Visible = false;  //收货数量
                this.GV_List.Columns[18].Visible = false;  //次品数量
                this.GV_List.Columns[19].Visible = false;  //废品数量
                this.GV_List.Columns[24].Visible = false;   //操作按钮
            }
        }
        else if (orderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE)
        {
            if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
            {
                this.GV_List.Columns[14].Visible = false;  //已发数量
                this.GV_List.Columns[15].Visible = false;  //发货数量
                this.GV_List.Columns[16].Visible = true;   //已收数量
                this.GV_List.Columns[17].Visible = false;  //收货数量
                this.GV_List.Columns[18].Visible = false;  //次品数量
                this.GV_List.Columns[19].Visible = false;  //废品数量
                this.GV_List.Columns[24].Visible = false;   //操作按钮
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                this.GV_List.Columns[14].Visible = true;   //已发数量
                this.GV_List.Columns[15].Visible = false;  //发货数量
                this.GV_List.Columns[16].Visible = false;  //已收数量
                this.GV_List.Columns[17].Visible = false;  //收货数量
                this.GV_List.Columns[18].Visible = false;  //次品数量
                this.GV_List.Columns[19].Visible = false;  //废品数量
                this.GV_List.Columns[24].Visible = false;   //操作按钮
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {
                this.GV_List.Columns[14].Visible = false;  //已发数量
                this.GV_List.Columns[15].Visible = false;  //发货数量
                this.GV_List.Columns[16].Visible = true;   //已收数量
                this.GV_List.Columns[17].Visible = false;  //收货数量
                this.GV_List.Columns[18].Visible = false;  //次品数量
                this.GV_List.Columns[19].Visible = false;  //废品数量
                this.GV_List.Columns[24].Visible = false;   //操作按钮
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
            {
                this.GV_List.Columns[14].Visible = false;  //已发数量
                this.GV_List.Columns[15].Visible = false;  //发货数量
                this.GV_List.Columns[16].Visible = true;   //已收数量
                this.GV_List.Columns[17].Visible = false;  //收货数量
                this.GV_List.Columns[18].Visible = false;  //次品数量
                this.GV_List.Columns[19].Visible = false;  //废品数量
                this.GV_List.Columns[24].Visible = false;   //操作按钮
            }
        }
        #endregion

        #region 新品字段
        if (this.NewItem)
        {
            this.GV_List.Columns[2].Visible = true;
        }

        #endregion

        if (orderHead.IsShipByOrder &&
            orderHead.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS && orderHead.Type != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        {
            this.GV_List.Columns[15].Visible = true;
        }
        else
        {
            this.GV_List.Columns[15].Visible = false;
        }

    }

    //检查零件是否存在
    protected bool CheckItemExists()
    {
        IList<OrderDetail> orderDetailList = GetOrderDetailList();
        int newRowId = orderDetailList != null ? orderDetailList.Count : 0;
        OrderHead orderHead = GetOrderHead();
        GridViewRow newRow = this.GV_List.Rows[newRowId];
        Controls_TextBox tbItemCode = (Controls_TextBox)newRow.FindControl("tbItemCode");
        Controls_TextBox tbUom = (Controls_TextBox)newRow.FindControl("tbUom");
        TextBox tbUnitCount = (TextBox)newRow.FindControl("tbUnitCount");
        if (orderDetailList != null)
        {
            IList<Item> itemList = new List<Item>();
            Item item = this.TheItemMgr.LoadItem(tbItemCode.Text.Trim());
            item.Uom = this.TheUomMgr.LoadUom(tbUom.Text.Trim());
            item.UnitCount = decimal.Parse(tbUnitCount.Text.Trim());
            if (item.Type == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_K)
            {
                IList<ItemKit> itemKitList = TheItemKitMgr.GetChildItemKit(item);

                if (itemKitList != null && itemKitList.Count > 0)
                {
                    foreach (ItemKit itemKit in itemKitList)
                    {
                        itemList.Add(itemKit.ChildItem);
                    }
                }
                else
                {
                    ShowErrorMessage("ItemKit.Error.NotFoundForParentItem", item.Code);
                    return true;
                }
            }
            else
            {
                itemList.Add(item);
            }

            foreach (Item checkItem in itemList)
            {
                for (int i = 0; i < orderDetailList.Count; i++)
                {

                    string oLocFrom = orderHead.LocationFrom == null ? null : orderHead.LocationFrom.Code;
                    string oLocTo = orderHead.LocationTo == null ? null : orderHead.LocationTo.Code;
                    string dLocFrom = orderDetailList[i].DefaultLocationFrom == null ? null : orderDetailList[i].DefaultLocationFrom.Code;
                    string dLocTo = orderDetailList[i].DefaultLocationTo == null ? null : orderDetailList[i].DefaultLocationTo.Code;

                    if (orderDetailList[i].Item.Type == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_K)
                    {
                        IList<ItemKit> itemKitList = TheItemKitMgr.GetChildItemKit(orderDetailList[i].Item);

                        if (itemKitList != null && itemKitList.Count > 0)
                        {
                            foreach (ItemKit itemKit in itemKitList)
                            {
                                if (itemKit.ChildItem.Code == checkItem.Code && itemKit.ChildItem.Uom.Code == checkItem.Uom.Code && oLocFrom == dLocFrom && oLocTo == dLocTo && itemKit.ChildItem.UnitCount == checkItem.UnitCount)
                                {
                                    ShowErrorMessage("MasterData.Order.OrderDetail.ItemCode.Exists");
                                    return true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (orderDetailList[i].Item.Code == checkItem.Code && orderDetailList[i].Item.Uom.Code == checkItem.Uom.Code && oLocFrom == dLocFrom && oLocTo == dLocTo && orderDetailList[i].UnitCount == checkItem.UnitCount)
                        {
                            if (checkItem.Code == item.Code && item.Uom.Code == checkItem.Uom.Code && oLocFrom == dLocFrom && oLocTo == dLocTo && checkItem.UnitCount == item.UnitCount)
                            {
                                ShowErrorMessage("MasterData.Order.OrderDetail.ItemCode.Exists");
                            }
                            else//由于新增物料为套件展开后造成物料重复,ErrorMessage显示套件代码
                            {
                                ShowErrorMessage("MasterData.Order.OrderDetail.ItemCode.Exists2", item.Code);
                            }
                            return true;
                        }
                    }

                }
            }

        }
        return false;
    }

    //清空价格字段
    public void CleanPrice()
    {
        tbOrderDetailPrice.Text = string.Empty;
        tbOrderDiscount.Text = string.Empty;
        tbOrderDiscountRate.Text = string.Empty;
        tbOrderPrice.Text = string.Empty;
    }

    //返回订单明细
    private IList<OrderDetail> GetOrderDetailList()
    {
        if (this.NewOrEdit == "New")
        {
            return this.TheOrder.OrderDetails;
        }
        else if (this.NewOrEdit == "Edit")
        {
            return TheOrderHeadMgr.LoadOrderHead(this.OrderNo, true).OrderDetails;
        }
        return null;
    }

    //返回订单头
    private OrderHead GetOrderHead()
    {

        if (this.NewOrEdit == "New")
        {
            return this.TheOrder;
        }
        else if (this.NewOrEdit == "Edit")
        {
            return TheOrderHeadMgr.LoadOrderHead(this.OrderNo, true);
        }
        return null;
    }

    //外部调用，返回订单头
    public OrderHead PopulateOrderHead()
    {
        UpdateSavedOrderHead();
        return GetOrderHead();
    }

    //返回是否允许编辑价格
    private bool AllowEditPrice()
    {
        bool allowEditPrice = false;
        if (this.CurrentUser.PagePermission != null && this.CurrentUser.PagePermission.Count > 0)
        {
            foreach (Permission permission in this.CurrentUser.PagePermission)
            {
                if (permission.Code == BusinessConstants.ORDER_OPERATION_EDIT_ORDER_PRICE)
                {
                    allowEditPrice = true;
                    break;
                }
            }
        }
        return allowEditPrice;
    }

    protected void lbReqQty_Click(object sender, EventArgs e)
    {
        int orderDetailId = int.Parse(((LinkButton)sender).CommandArgument);
        this.ucOrderTracer.Visible = true;
        this.ucOrderTracer.InitPageParameter(orderDetailId);
    }

}
