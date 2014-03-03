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
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Exception;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using System.Collections.Generic;

public partial class Order_OrderView_LocTransList : ModuleBase
{
    public EventHandler EditEvent;
    public EventHandler UpdateRoutingEvent;

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

    public string IOType
    {
        get
        {
            return (string)ViewState["IOType"];
        }
        set
        {
            ViewState["IOType"] = value;
        }
    }

    public string LocationCode
    {
        get
        {
            return (string)ViewState["LocationCode"];
        }
        set
        {
            ViewState["LocationCode"] = value;
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

    public IList<Hu> huList
    {
        get
        {
            return (IList<Hu>)ViewState["huList"];
        }
        set
        {
            ViewState["huList"] = value;
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

    //报废
    public bool IsScrap
    {
        get
        {
            return (bool)ViewState["IsScrap"];
        }
        set
        {
            ViewState["IsScrap"] = value;
        }
    }

    //原材料回用
    public bool IsReuse
    {
        get
        {
            return (bool)ViewState["IsReuse"];
        }
        set
        {
            ViewState["IsReuse"] = value;
        }
    }

    protected string PartyFromCode = string.Empty;
    protected string OrderStatus = string.Empty;
    protected IList<string> KitItemList = new List<string>();
    protected IDictionary<string, int> KitItemDic = new Dictionary<string, int>();

    public void UpdateView()
    {

        OrderHead orderHead = TheOrderHeadMgr.LoadOrderHead(this.OrderNo);
        this.OrderStatus = orderHead.Status;
        this.PartyFromCode = orderHead.PartyFrom.Code;

        DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderLocationTransaction));
        criteria.CreateAlias("OrderDetail", "od");
        criteria.CreateAlias("od.OrderHead", "oh");

        criteria.Add(Expression.Eq("oh.OrderNo", this.OrderNo));
        criteria.Add(Expression.Eq("IOType", this.IOType));

        criteria.AddOrder(Order.Asc("od.Sequence"));
        criteria.AddOrder(Order.Asc("Operation"));
        criteria.AddOrder(Order.Asc("IOType"));

        IList<OrderLocationTransaction> orderLocTransList = TheCriteriaMgr.FindAll<OrderLocationTransaction>(criteria);

        //新增物料
        if (this.OrderStatus == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE && this.IOType == BusinessConstants.IO_TYPE_OUT
            && this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION && !this.IsReuse)
        {
            GetKitItemList(orderLocTransList);
            OrderLocationTransaction newOrderLocTrans = new OrderLocationTransaction();
            newOrderLocTrans.IOType = this.IOType;
            newOrderLocTrans.IsBlank = true;
            orderLocTransList.Add(newOrderLocTrans);
            this.GV_List.DataBind();
        }

        this.GV_List.DataSource = orderLocTransList;
        this.GV_List.DataBind();
        SaveAllDetail();

        this.GV_List.Columns[4].Visible = this.NewItem;

        if (this.IsReuse && this.IOType == BusinessConstants.IO_TYPE_OUT)
        {
            if (orderHead.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
            {
                this.divMessage.Visible = false;
            }
            else
            {
                this.divMessage.Visible = true;
            }
            this.LocationCode = null;
            InitialHuScan();

            this.huList = new List<Hu>();
            if (this.GV_HuList != null && this.huList.Count > 0)
            {
                this.fdHuList.Visible = true;
                this.lTitle.Visible = true;
            }
            else
            {
                this.fdHuList.Visible = false;
                this.lTitle.Visible = false;
            }

        }
        else
        {
            this.divMessage.Visible = false;
            this.GV_HuList.Visible = false;
        }



    }

    private void GetKitItemList(IList<OrderLocationTransaction> orderLocTransList)
    {

        foreach (OrderLocationTransaction orderLocTrans in orderLocTransList)
        {
            if (!KitItemList.Contains(orderLocTrans.OrderDetail.Item.Code))
            {
                KitItemList.Add(orderLocTrans.OrderDetail.Item.Code);
                KitItemDic.Add(orderLocTrans.OrderDetail.Item.Code, orderLocTrans.OrderDetail.Id);
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.huList = new List<Hu>();
            this.LocationCode = null;
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                // ((DataControlFieldCell)(e.Row.Cells[8])).ContainingField.HeaderText = "${MasterData.Order.LocTrans.AccumulateRejectQty.Production}";
                // ((DataControlFieldCell)(e.Row.Cells[11])).ContainingField.HeaderText = "${MasterData.Order.LocTrans.ScrapLocation}";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                OrderLocationTransaction ordeLocTrans = (OrderLocationTransaction)e.Row.DataItem;

                if (this.OrderStatus == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
                {
                    if (ordeLocTrans.IOType == BusinessConstants.IO_TYPE_IN)
                    {
                        this.GV_List.Columns[1].Visible = false; //成品
                        this.GV_List.Columns[5].Visible = false; //选装
                        this.GV_List.Columns[10].Visible = true;  //废品数
                        this.GV_List.Columns[11].Visible = true;  //次品数
                        this.GV_List.Columns[13].Visible = true; //次品库位

                        if (this.IsScrap)
                        {
                            this.GV_List.Columns[10].Visible = false;
                            this.GV_List.Columns[11].Visible = false;
                            this.GV_List.Columns[12].Visible = false;
                            this.GV_List.Columns[13].Visible = false;
                            this.GV_List.Columns[14].Visible = false;
                            this.GV_List.Columns[15].Visible = false;
                        }
                    }
                    if (ordeLocTrans.IOType == BusinessConstants.IO_TYPE_OUT)
                    {
                        this.GV_List.Columns[5].Visible = true; //选装
                        this.GV_List.Columns[15].Visible = true;  //操作按钮

                        ((RequiredFieldValidator)e.Row.FindControl("rfvUnitQty")).Enabled = true;
                        ((RequiredFieldValidator)e.Row.FindControl("rfvOrderdQty")).Enabled = true;
                        ((RangeValidator)e.Row.FindControl("rvUnitQty")).Enabled = true;
                        ((RangeValidator)e.Row.FindControl("rvOrderdQty")).Enabled = true;

                        ((CheckBox)e.Row.FindControl("cbNeedPrint")).Enabled = true;

                        e.Row.FindControl("lbOrderdQty").Visible = false;
                        TextBox tbOrderdQty = (TextBox)e.Row.FindControl("tbOrderdQty");
                        tbOrderdQty.Visible = true;
                        Label lbUnitQty = (Label)e.Row.FindControl("lbUnitQty");
                        lbUnitQty.Visible = false;
                        e.Row.FindControl("lbUnitQty").Visible = false;
                        e.Row.FindControl("tbUnitQty").Visible = true;

                        e.Row.FindControl("lbItemVersion").Visible = false;
                        e.Row.FindControl("tbItemVersion").Visible = true;

                        Label lbLocation = (Label)e.Row.FindControl("lbLocation");
                        lbLocation.Visible = false;
                        Controls_TextBox tbLocation = (Controls_TextBox)e.Row.FindControl("tbLocation");
                        tbLocation.Visible = true;
                        tbLocation.ServiceParameter = "string:" + this.PartyFromCode;
                        tbLocation.DataBind();
                        tbLocation.Text = ordeLocTrans.Location == null ? string.Empty : ordeLocTrans.Location.Code;

                        TextBox tbUnitQty = (TextBox)e.Row.FindControl("tbUnitQty");
                        tbUnitQty.Attributes.Add("onchange", "UnitQtyChanged(this);");

                        if (!ordeLocTrans.IsBlank)
                        {
                            #region exist row
                            if (ordeLocTrans.BomDetail != null && ordeLocTrans.BomDetail.StructureType == BusinessConstants.CODE_MASTER_BOM_DETAIL_TYPE_VALUE_O)
                            {
                                e.Row.FindControl("cbIsAssemble").Visible = true;    //选装件
                            }
                            if (ordeLocTrans.Item.Type == BusinessConstants.CODE_MASTER_ITEM_TYPE_VALUE_A)
                            {
                                e.Row.FindControl("lbItem").Visible = false;
                                e.Row.FindControl("lbtnEdit").Visible = true;
                            }
                            if (this.IsScrap)
                            {

                                tbLocation.Visible = false;
                                lbLocation.Visible = true;
                                if (this.IsReuse)
                                {
                                    if (ordeLocTrans.Item.Code == ordeLocTrans.OrderDetail.Item.Code)
                                    {
                                        tbOrderdQty.Text = ordeLocTrans.OrderedQty.ToString("0.########");
                                        tbOrderdQty.ReadOnly = true;

                                        tbLocation.ServiceParameter = "string:" + this.PartyFromCode + ",bool:false,bool:true";
                                        tbLocation.DataBind();

                                        tbLocation.Visible = true;
                                        lbLocation.Visible = false;
                                    }
                                    else
                                    {
                                        tbOrderdQty.Text = (0 - ordeLocTrans.OrderedQty).ToString("0.########");
                                        RangeValidator rvOrderQty = (RangeValidator)e.Row.FindControl("rvOrderdQty");
                                        rvOrderQty.MaximumValue = "0";
                                        rvOrderQty.MinimumValue = "-999999999";

                                    }
                                }
                                else
                                {
                                    //  tbOrderdQty.Text = "0";
                                }
                                lbUnitQty.Visible = true;
                                tbUnitQty.Visible = false;

                                this.GV_List.Columns[5].Visible = false;
                                this.GV_List.Columns[9].Visible = false;
                                this.GV_List.Columns[10].Visible = false;
                                this.GV_List.Columns[11].Visible = false;
                                this.GV_List.Columns[12].Visible = true;
                                this.GV_List.Columns[13].Visible = false;
                                this.GV_List.Columns[14].Visible = false;
                                this.GV_List.Columns[15].Visible = false;
                            }
                            #endregion
                        }
                        else
                        {
                            #region newrow
                            e.Row.FindControl("lbOperation").Visible = false;
                            e.Row.FindControl("lbKitItem").Visible = false;
                            e.Row.FindControl("lbItem").Visible = false;

                            e.Row.FindControl("tbOperation").Visible = true;
                            DropDownList tbKitItem = (DropDownList)e.Row.FindControl("tbKitItem");
                            tbKitItem.Visible = true;

                            tbKitItem.DataSource = KitItemDic.ToList();
                            tbKitItem.DataBind();


                            Controls_TextBox tbItem = (Controls_TextBox)e.Row.FindControl("tbItem");
                            tbItem.Visible = true;
                            tbItem.SuggestTextBox.Attributes.Add("onchange", "GetItemUom(this);");



                            ((RangeValidator)e.Row.FindControl("rvOperation")).Enabled = true;
                            ((RequiredFieldValidator)e.Row.FindControl("rfvOperation")).Enabled = true;
                            ((RequiredFieldValidator)e.Row.FindControl("rfvItem")).Enabled = true;
                            ((RequiredFieldValidator)e.Row.FindControl("rfvLocation")).Enabled = !(IsScrap && !IsReuse);
                            ((Controls_TextBox)e.Row.FindControl("tbLocation")).Visible = !(IsScrap && !IsReuse);
                            ((Label)e.Row.FindControl("lbLocation")).Visible = IsScrap && !IsReuse;
                            if (IsScrap && !IsReuse)
                            {
                                ((TextBox)e.Row.FindControl("tbOperation")).Text = "10";
                                ((TextBox)e.Row.FindControl("tbUnitQty")).Text = "1";

                            }
                            e.Row.FindControl("lbtnAdd").Visible = true;
                            e.Row.FindControl("lbtnDelete").Visible = false;

                            #endregion
                        }
                    }
                }
                else
                {
                    this.GV_List.Columns[5].Visible = false; //选装
                    if (ordeLocTrans.IOType == BusinessConstants.IO_TYPE_IN)
                    {
                        this.GV_List.Columns[1].Visible = false; //成品

                    }

                }
            }

        }
        else
        {
            this.GV_List.Columns[1].Visible = false; //成品
        }

    }

    protected void lbtnAdd_Click(object sender, EventArgs e)
    {


        int rowIndex = ((GridViewRow)(((DataControlFieldCell)(((LinkButton)(sender)).Parent)).Parent)).RowIndex;
        GridViewRow row = this.GV_List.Rows[rowIndex];
        RequiredFieldValidator rfvItem = (RequiredFieldValidator)(row.FindControl("rfvItem"));
        RequiredFieldValidator rfvLocation = (RequiredFieldValidator)(row.FindControl("rfvLocation"));
        if (!rfvItem.IsValid || !rfvLocation.IsValid)
        {
            return;
        }
        if (checkItemExists())
        {
            ShowErrorMessage("MasterData.Order.OrderDetail.Item.Exists");
            return;
        }
        OrderLocationTransaction orderLocTrans = new OrderLocationTransaction();



        orderLocTrans.Operation = int.Parse(((TextBox)row.FindControl("tbOperation")).Text.Trim());
        orderLocTrans.OrderDetail = TheOrderDetailMgr.LoadOrderDetail(int.Parse(((DropDownList)row.FindControl("tbKitItem")).SelectedValue));

        orderLocTrans.UnitQty = decimal.Parse(((TextBox)row.FindControl("tbUnitQty")).Text.Trim());
        Item item = TheItemMgr.LoadItem(((Controls_TextBox)row.FindControl("tbItem")).Text.Trim());
        if (item != null)
        {
            orderLocTrans.Item = item;
            orderLocTrans.Uom = item.Uom;

        }
        orderLocTrans.OrderedQty = decimal.Parse(((TextBox)row.FindControl("tbOrderdQty")).Text.Trim());

        if (orderLocTrans.OrderedQty == 0)
        {
            orderLocTrans.OrderedQty = orderLocTrans.UnitQty * orderLocTrans.OrderDetail.OrderedQty;
        }
        if (!this.IsReuse && this.IsScrap)
        {

            orderLocTrans.Location = orderLocTrans.OrderDetail.DefaultLocationFrom;
        }
        else
        {
            Controls_TextBox tbLocation = (Controls_TextBox)row.FindControl("tbLocation");
            if (tbLocation.Text.Trim() != string.Empty)
            {
                orderLocTrans.Location = TheLocationMgr.LoadLocation(tbLocation.Text.Trim());
            }
        }

        orderLocTrans.NeedPrint = ((CheckBox)row.FindControl("cbNeedPrint")).Checked;

        orderLocTrans.IsAssemble = true;
        orderLocTrans.IOType = this.IOType;
        orderLocTrans.IsBlank = false;
        orderLocTrans.TransactionType = BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO;

        try
        {
            TheOrderMgr.AddOrderLocationTransaction(orderLocTrans, this.CurrentUser);
            ShowSuccessMessage("MasterData.Order.LocTrans.Add.Successfully");
            UpdateRoutingEvent(this.OrderNo, e);
            UpdateView();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }

    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        LinkButton lbtnEdit = (LinkButton)sender;
        int id = int.Parse(((LinkButton)sender).CommandArgument);
        if (EditEvent != null)
        {
            EditEvent(new Object[] { lbtnEdit.Text, id }, e);
        }
    }

    public void SaveAllDetail()
    {
        IList<OrderLocationTransaction> orderLocTransList = new List<OrderLocationTransaction>();

        int count = this.GV_List.Rows.Count;
        if (!this.IsReuse)
        {
            count = count - 1;
        }
        for (int i = 0; i < count; i++)
        {
            GridViewRow row = this.GV_List.Rows[i];
            HiddenField hfId = (HiddenField)row.FindControl("hfId");
            OrderLocationTransaction orderLocTrans = TheOrderLocationTransactionMgr.LoadOrderLocationTransaction(int.Parse(hfId.Value));

            orderLocTrans.IsAssemble = ((CheckBox)row.FindControl("cbIsAssemble")).Checked;
            orderLocTrans.OrderedQty = decimal.Parse(((TextBox)row.FindControl("tbOrderdQty")).Text.Trim());
            orderLocTrans.UnitQty = decimal.Parse(((TextBox)row.FindControl("tbUnitQty")).Text.Trim());
            orderLocTrans.ItemVersion = ((TextBox)row.FindControl("tbUnitQty")).Text.Trim();
            Controls_TextBox tbLocation = (Controls_TextBox)row.FindControl("tbLocation");

            if (tbLocation.Text.Trim() != string.Empty)
            {
                orderLocTrans.Location = TheLocationMgr.LoadLocation(tbLocation.Text.Trim());
            }
            orderLocTrans.NeedPrint = ((CheckBox)row.FindControl("cbNeedPrint")).Checked;
            orderLocTransList.Add(orderLocTrans);
        }
        //更新orderloctrans
        foreach (OrderLocationTransaction orderLocTrans in orderLocTransList)
        {
            TheOrderLocationTransactionMgr.UpdateOrderLocationTransaction(orderLocTrans);
        }
        //  ShowSuccessMessage("MasterData.Order.LocTrans.Update.Successfully");
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        int id = int.Parse(((LinkButton)sender).CommandArgument);
        OrderLocationTransaction orderLocationTrans = TheOrderLocationTransactionMgr.LoadOrderLocationTransaction(id);
        try
        {
            TheOrderMgr.DeleteOrderLocationTransaction(id, this.CurrentUser.Code);
            ShowSuccessMessage("MasterData.Order.LocTrans.Delete.Successfully");
            UpdateRoutingEvent(this.OrderNo, e);
            UpdateView();

        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }
    protected bool checkItemExists()
    {
        bool itemChecked = false;
        int index = this.GV_List.Rows.Count;
        GridViewRow newRow = this.GV_List.Rows[index - 1];

        int operation = int.Parse(((TextBox)newRow.FindControl("tbOperation")).Text.Trim());
        string itemCode = ((Controls_TextBox)newRow.FindControl("tbItem")).Text.Trim();
        string kitItem = ((DropDownList)newRow.FindControl("tbKitItem")).SelectedItem.Text;
        for (int i = 0; i < this.GV_List.Rows.Count - 1; i++)
        {
            GridViewRow row = (GridViewRow)this.GV_List.Rows[i];
            int op = int.Parse(((Label)row.FindControl("lbOperation")).Text.Trim());
            string item = ((Label)row.FindControl("lbItem")).Text.Trim();
            string kit = ((Label)row.FindControl("lbKitItem")).Text.Trim();
            if (kit == kitItem && op == operation && item == itemCode)
            {
                itemChecked = true;
            }
        }
        return itemChecked;
    }

    protected void tbHuScan_TextChanged(object sender, EventArgs e)
    {
        string huId = this.tbHuScan.Text.Trim();
        foreach (Hu hu in this.huList)
        {
            if (hu.HuId == huId)
            {
                ShowErrorMessage("Warehouse.Error.HuNotExist", huId);
                return;
            }
        }

        IList<LocationLotDetail> locationLotDetailList = TheLocationLotDetailMgr.GetHuLocationLotDetail(huId);
        if (locationLotDetailList.Count == 0)
        {
            ShowErrorMessage("Warehouse.Error.HuNotExist", huId);
            return;
        }
        if (this.LocationCode == null)
        {
            this.LocationCode = locationLotDetailList[0].Location.Code;
        }
        if (this.LocationCode != locationLotDetailList[0].Location.Code)
        {
            ShowErrorMessage("Hu.Error.Location.NotEqual");
            return;
        }
        this.huList.Add(locationLotDetailList[0].Hu);
        this.GV_HuList.DataSource = this.huList;
        this.GV_HuList.DataBind();

        this.lTitle.Visible = true;
        this.fdHuList.Visible = true;
        InitialHuScan();
    }

    private void InitialHuScan()
    {
        this.lblMessage.Text = string.Empty;
        this.tbHuScan.Text = string.Empty;
        this.tbHuScan.Focus();
        this.GV_HuList.DataBind();
    }

}
