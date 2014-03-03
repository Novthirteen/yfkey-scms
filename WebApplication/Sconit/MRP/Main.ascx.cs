using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Utility;
using com.Sconit.Entity.Procurement;
using com.Sconit.Entity.View;
using com.Sconit.Entity.MRP;
using System.Reflection;
using NHibernate.Expression;
using System.Data;
using System.Xml;
using Microsoft.ApplicationBlocks.Data;
using System.Collections;



public partial class MRP_Schedule_DemandSchedule_Main : MainModuleBase
{
    private bool enableDiscon = true;

    public event EventHandler lbRunMrpClickEvent;

    #region 变量
    private bool isExport = false;
    private int seq = 1;
    private int seq_Detail = 1;
    private IList<MrpShipPlan> mrpShipPlans
    {
        get;
        set;
    }
    private IList<ExpectTransitInventory> expectTransitInventories
    {
        get;
        set;
    }
   

    protected DateTime EffDate
    {
        get
        {
            return this.ddlDate.SelectedValue != string.Empty? DateTime.Parse(this.ddlDate.SelectedValue.Trim()):DateTime.Now;
        }
    }

    private DateTime? WinDate
    {
        get
        {
            if (this.tbScheduleTime.Text.Trim() != string.Empty && isWinTime)
            {
                return DateTime.Parse(this.tbScheduleTime.Text.Trim()).Date;
            }
            else
            {
                return null;
            }
        }
    }

    private DateTime? StartDate
    {
        get
        {
            if (this.tbScheduleTime.Text.Trim() != string.Empty && !isWinTime)
            {
                return DateTime.Parse(this.tbScheduleTime.Text.Trim()).Date;
            }
            else
            {
                return null;
            }
        }
    }


    protected string flowOrLoc
    {
        get { return this.tbFlowOrLoc.Text.Trim(); }
    }

    private string itemCode
    {
        get { return this.tbItemCode.Text.Trim(); }
    }

    protected bool isWinTime
    {
        get { return this.rblDateType.SelectedIndex == 1; }
    }

    protected bool isFlow
    {
        get { return this.rblFlowOrLoc.SelectedIndex == 0; }
    }

    private string FlowCode
    {
        get { return (string)ViewState["FlowCode"]; }
        set { ViewState["FlowCode"] = value; }
    }

    private string FlowType
    {
        get { return (string)ViewState["FlowType"]; }
        set { ViewState["FlowType"] = value; }
    }

    private DateTime? ScheduleDate
    {
        get { return (DateTime)ViewState["ScheduleDate"]; }
        set { ViewState["ScheduleDate"] = value; }
    }

    private string PartyCode
    {
        get { return (string)ViewState["PartyCode"]; }
        set { ViewState["PartyCode"] = value; }
    }

    private int ColumnNum
    {
        get { return (int)ViewState["ColumnNum"]; }
        set { ViewState["ColumnNum"] = value; }
    }


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
       
        this.tbFlowOrLoc.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:true,bool:true,bool:true,bool:true,bool:true,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;
       
        this.tbFlowOrLoc.DataBind();
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:true,bool:true,bool:true,bool:true,bool:true,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;

        this.tbWinTime.Attributes.Add("onclick", "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm',lang:'" + this.CurrentUser.UserLanguage + "'})");
        this.tbWinTime.Attributes["onchange"] += "setStartTime();";
        this.cbIsUrgent.Attributes["onchange"] += "setStartTime();";
        if (!IsPostBack)
        {
            this.ucShift.Date = DateTime.Today;

            DetachedCriteria criteria = DetachedCriteria.For<MrpRunLog>();
            criteria.SetProjection(Projections.ProjectionList().Add(Projections.GroupProperty("RunDate")));
            criteria.AddOrder(Order.Desc("RunDate"));
            IList<DateTime> list = TheCriteriaMgr.FindAll<DateTime>(criteria, 0, 30);

            List<string> effDate = list.Select(l => l.ToString("yyyy-MM-dd")).ToList();

            this.ddlDate.DataSource = effDate;
            this.ddlDate.DataBind();
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch((Button)sender);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.fld_Search.Visible = true;
        this.div_OrderDetail.Visible = false;
        this.div_MRP_Detail.Visible = false;
        this.fld_Group.Visible = false;
        this.tbFlow.Text = string.Empty;
        this.tbWinTime.Text = string.Empty;
        this.tbStartTime.Text = string.Empty;
        this.cbIsUrgent.Checked = false;
        this.tbStartTime.Text = string.Empty;
        this.tbRefOrderNo.Text = string.Empty;
        this.tbExtOrderNo.Text = string.Empty;
        this.DoSearch(this.btnSearch);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
           IList<MrpShipPlan> mrpShipPlanList = new List<MrpShipPlan>();
            foreach (GridViewRow gvr in this.GV_Detail.Rows)
            {
                HiddenField hdfId = (HiddenField)gvr.FindControl("hdfId");
                int id = int.Parse(hdfId.Value);

                Label tbQty = (Label)gvr.FindControl("tbQty");
                decimal qty = decimal.Parse(tbQty.Text.Trim());

               MrpShipPlan mrpShipPlan = TheMrpShipPlanMgr.LoadMrpShipPlan(id);
               mrpShipPlan.Qty = qty;
               mrpShipPlanList.Add(mrpShipPlan);
            }

            TheMrpShipPlanMgr.UpdateMrpShipPlan(mrpShipPlanList);
            ShowSuccessMessage("MRP.Schedule.Update.Successfully");
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage("MRP.Schedule.Update.Failed");
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.tbFlow.Text == string.Empty)
            {
                ShowErrorMessage("MRP.Schedule.Import.CustomerSchedule.Result.SelectFlow");
                return;
            }
            Flow flow = TheFlowMgr.CheckAndLoadFlow(this.tbFlow.Text);

            OrderHead orderHead = this.TheOrderMgr.TransferFlow2Order(flow);

            foreach (GridViewRow row in this.GV_Order.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string item = row.Cells[1].Text;
                    string uom = row.Cells[4].Text;
                    string qtyStr = ((TextBox)row.Cells[7].FindControl("tbQty")).Text;
                    decimal? qty = null;
                    try
                    {
                        qty = decimal.Parse(qtyStr);
                    }
                    catch (Exception)
                    {
                        this.ShowErrorMessage("MasterData.MiscOrder.WarningMessage.InputQtyFormat.Error");
                        return;
                    }

                    if (qty.HasValue && qty > 0)
                    {
                        OrderDetail orderDetail = (from det in orderHead.OrderDetails
                                                   where det.Item.Code == item
                                                   select det).FirstOrDefault();

                        if (orderDetail != null)
                        {
                            orderDetail.OrderedQty = qty.Value;

                            if (orderDetail.Uom.Code != uom)
                            {
                                orderDetail.OrderedQty = this.TheUomConversionMgr.ConvertUomQty(item, uom, orderDetail.OrderedQty, orderDetail.Uom.Code);
                            }
                        }
                    }
                }
            }

            IList<OrderDetail> resultOrderDetailList = new List<OrderDetail>();

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
                                if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
                                {
                                    if (orderDetail.PriceListFrom != null && orderDetail.PriceListFrom.Code != string.Empty)
                                    {
                                        //newOrderDetail.PriceListFrom = ThePurchasePriceListMgr.l
                                        //if (newOrderDetail.PriceList != null)
                                        //{
                                        //    PriceListDetail priceListDetail = this.ThePriceListDetailMgr.GetLastestPriceListDetail(newOrderDetail.PriceList, item, DateTime.Now, newOrderDetail.OrderHead.Currency, item.Uom);
                                        //    newOrderDetail.IsProvisionalEstimate = priceListDetail == null ? true : priceListDetail.IsProvisionalEstimate;
                                        //    if (priceListDetail != null)
                                        //    {
                                        //        newOrderDetail.UnitPrice = priceListDetail.UnitPrice;
                                        //        newOrderDetail.TaxCode = priceListDetail.TaxCode;
                                        //        newOrderDetail.IsIncludeTax = priceListDetail.IsIncludeTax;
                                        //    }
                                        //}
                                    }
                                }
                                #endregion
                                resultOrderDetailList.Add(newOrderDetail);
                            }
                        }
                        else
                        {
                            resultOrderDetailList.Add(orderDetail);
                        }
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
                DateTime winTime = this.tbWinTime.Text.Trim() == string.Empty ? DateTime.Now : DateTime.Parse(this.tbWinTime.Text);
                DateTime startTime = winTime;
                if (this.tbSettleTime.Text.Trim() != string.Empty)
                {
                    //orderHead.SettleTime = DateTime.Parse(this.tbSettleTime.Text);
                }

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

                if (orderHead.Type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
                {
                    if (this.ucShift.ShiftCode == string.Empty)
                    {
                        ShowErrorMessage("MasterData.Order.Shift.Empty");
                        return;
                    }
                    orderHead.Shift = TheShiftMgr.LoadShift(this.ucShift.ShiftCode);
                }

                orderHead.OrderDetails = resultOrderDetailList;
                orderHead.WindowTime = winTime;
                orderHead.StartTime = startTime;
                orderHead.IsAutoRelease = this.cbReleaseOrder.Checked;

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


            TheOrderMgr.CreateOrder(orderHead, this.CurrentUser);
            if (this.cbPrintOrder.Checked && false)//不要打印
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
            this.ShowSuccessMessage("MasterData.Order.OrderHead.AddOrder.Successfully", orderHead.OrderNo);

            //跳转到相应的订单查询一面
            string url = null;
            if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
            {

                url = "Main.aspx?mid=Order.OrderHead.Production__mp--ModuleType-Production_ModuleSubType-Nml_StatusGroupId-4__act--ListAction";
            }
            else if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT
                || orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_SUBCONCTRACTING
                || orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_CUSTOMERGOODS)
            {
                url = "Main.aspx?mid=Order.OrderHead.Procurement__mp--ModuleType-Procurement_ModuleSubType-Nml_StatusGroupId-4__act--ListAction";

            }
            else if (orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION
                || orderHead.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER)
            {
                url = "Main.aspx?mid=Order.OrderHead.Distribution__mp--ModuleType-Distribution_ModuleSubType-Nml_StatusGroupId-4__act--ListAction";
            }
            else
            {
                return;
            }

            Page.ClientScript.RegisterStartupScript(GetType(), "method",
                " <script language='javascript' type='text/javascript'>timedMsg('" + url + "'); </script>");

        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
        catch (Exception)
        {


        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int columnCount = this.GV_List.Columns.Count;
       

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ScheduleBody body = (com.Sconit.Entity.MRP.ScheduleBody)(e.Row.DataItem);

            e.Row.Cells[0].Text = seq.ToString();
            //LinkButton lbnItem = e.Row.Cells[1].Controls[0] as LinkButton;
            Item item = this.TheItemMgr.CheckAndLoadItem(body.Item);
            e.Row.Cells[2].Text = item.Description;
            e.Row.Cells[3].Text = item.DefaultSupplier;
            string lblUom = e.Row.Cells[4].Text;
            string lblUnitCount = e.Row.Cells[5].Text;

            seq++;
            //lbnItem.Text = body.Item;

            if (!isExport)
            {
                for (int i = 6; i < columnCount; i++)
                {
                    string headerText = this.GV_List.Columns[i].SortExpression;
                    string lastHeaderText = this.GV_List.Columns[i].FooterText;
                    DateTime headerTextTime = DateTime.Parse(headerText);
                    DateTime? lastHeaderTextTime = null;
                    if (lastHeaderText != string.Empty)
                    {
                        lastHeaderTextTime = DateTime.Parse(lastHeaderText);
                    }
                  
                }
            }
            else
            {
                e.Row.Cells[1].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            }
        }
    }

    protected void GV_Order_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ScheduleBody body = (com.Sconit.Entity.MRP.ScheduleBody)(e.Row.DataItem);

            e.Row.Cells[0].Text = seq.ToString();
            Item item = this.TheItemMgr.LoadItem(body.Item);
            e.Row.Cells[2].Text = item.Description;
            e.Row.Cells[3].Text = item.DefaultSupplier;

            decimal uc = Convert.ToDecimal(e.Row.Cells[5].Text);

            decimal qty = body.Qty0;

            if (qty % uc != 0)
            {
                qty = qty - qty % uc + uc;
            }

            ((TextBox)e.Row.Cells[7].FindControl("tbQty")).Text = qty.ToString("0.##");
            seq++;
        }
    }

    protected void GV_Detail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((Label)e.Row.FindControl("lblSequence")).Text = seq_Detail.ToString();
            seq_Detail++;

            Item item = this.TheItemMgr.LoadItem(e.Row.Cells[1].Text);
            e.Row.Cells[2].Text = item.Description;
           // e.Row.Cells[3].Text = item.DefaultSupplier;

            if (isExport)
            {
                e.Row.Cells[1].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            }
        }
    }


    protected void GV_List_Sorting(Object sender, GridViewSortEventArgs e)
    {
        
        DataControlFieldCollection dcfc = ((GridView)sender).Columns;
        for (int i = 6; i < dcfc.Count; i++)
        {
            DataControlField dcf = dcfc[i];
            if (dcf.SortExpression == e.SortExpression)
            {
                ColumnNum = i - 6;

                string qty = "Qty" + (i - 6).ToString();
                string actQty = "ActQty" + (i - 6).ToString();
                string requiredQty = "RequiredQty" + (i - 6).ToString();

                this.ScheduleDate = DateTime.Parse(e.SortExpression);
                //todo wintime or starttime
                this.hfLastScheduleTime.Value = dcf.FooterText;
                DateTime? lastScheduleDate = null;
                if (this.hfLastScheduleTime.Value != string.Empty)
                {
                    lastScheduleDate = DateTime.Parse(this.hfLastScheduleTime.Value);
                }

                IList<MrpShipPlanView> mrpShipPlanViews = TheMrpShipPlanViewMgr.GetMrpShipPlanViews((isFlow ? this.flowOrLoc : null), (!isFlow ? this.flowOrLoc : null), this.itemCode, this.EffDate, null, null);

                DetachedCriteria criteria = DetachedCriteria.For<ExpectTransitInventoryView>();
                criteria.Add(Expression.Eq("EffectiveDate", this.EffDate));
                IList<ExpectTransitInventoryView> transitInventoryViews = this.TheCriteriaMgr.FindAll<ExpectTransitInventoryView>(criteria);

               
                ScheduleView scheduleView = TheMrpShipPlanViewMgr.TransferMrpShipPlanViews2ScheduleView(mrpShipPlanViews, transitInventoryViews, this.rblFlowOrLoc.SelectedValue, this.rblDateType.SelectedValue);
               
                foreach (ScheduleBody body in scheduleView.ScheduleBodys)
                {
                    PropertyInfo qtyProp = typeof(ScheduleBody).GetProperty(qty);
                    PropertyInfo actQtyProp = typeof(ScheduleBody).GetProperty(actQty);
                    PropertyInfo requiredQtyProp = typeof(ScheduleBody).GetProperty(requiredQty);

                    body.Qty0 = (decimal)qtyProp.GetValue(body, null);
                    body.ActQty0 = (decimal)actQtyProp.GetValue(body, null);
                    body.RequiredQty0 = (decimal)requiredQtyProp.GetValue(body, null);
                }

                this.GV_Order.DataSource = scheduleView.ScheduleBodys;
                this.GV_Order.DataBind();
                this.fld_Search.Visible = false;
                this.div_OrderDetail.Visible = true;
                this.div_MRP_Detail.Visible = false;
                this.fld_Group.Visible = false;
                if (isFlow)
                {
                    this.tbFlow.Text = this.flowOrLoc;
                    dotbFlow_TextChanged(this.flowOrLoc);
                }
                //this.ucShift.Date = DateTime.Today;
                break;
            }
        }
    }

    protected void CustomersGridView_Sorted(Object sender, EventArgs e)
    {
        
    }

    protected void rblFlowOrLoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (isFlow)
        {
            this.tbFlowOrLoc.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:true,bool:true,bool:true,bool:true,bool:true,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;
            this.tbFlowOrLoc.ServicePath = "FlowMgr.service";
            this.tbFlowOrLoc.ServiceMethod = "GetFlowList";
            this.tbFlowOrLoc.DescField = "Description";
        }
        else
        {
            this.tbFlowOrLoc.ServiceParameter = "string:" + this.CurrentUser.Code;
            this.tbFlowOrLoc.ServicePath = "LocationMgr.service";
            this.tbFlowOrLoc.ServiceMethod = "GetLocationByUserCode";
            this.tbFlowOrLoc.DescField = "Name";
        }
        this.tbFlowOrLoc.Text = string.Empty;
        this.tbFlowOrLoc.DataBind();
    }

    private void DoSearch(Button button)
    {

        if (this.flowOrLoc == string.Empty)
        {
            ShowErrorMessage("MRP.Schedule.Import.CustomerSchedule.Result.SelectFlow");
            return;
        }
        else if (isFlow)
        {
            Flow flow = TheFlowMgr.LoadFlow(this.flowOrLoc);
            if (flow.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                this.PartyCode = flow.PartyTo.Code;
            }
            else
            {
                this.PartyCode = flow.PartyFrom.Code;
            }
        }
        //added by williamlu@esteering.cn 
        //2012/5
        string plant = "";
        plant = ddlPlant.Text;
        string ConnString = string.Empty;
        string sqlText = string.Empty;
        DataSet dsTemp = new DataSet();
        XmlTextReader reader = new XmlTextReader(Server.MapPath("Config/properties.config"));
        XmlDocument doc = new XmlDocument();
        doc.Load(reader);//  
        reader.Close();//
        ConnString = doc.SelectSingleNode("/configuration/properties/connectionString").InnerText.Trim();
        sqlText = "select code from item where plant = '" + plant + "'";
        //tested by williamlu@esteering.cn
        //sqlText = "select code from item where code in ('3608021021624' )";
        //tested end
        dsTemp = SqlHelper.ExecuteDataset(ConnString, CommandType.Text, sqlText);
        List<string> ListItem = new List<string>();
        foreach (DataRow dr in dsTemp.Tables[0].Rows)
        {
            ListItem.Add(dr["code"].ToString());
        }
        //added end

        DetachedCriteria criteria = DetachedCriteria.For<ExpectTransitInventory>();
                
        criteria.Add(Expression.Eq("EffectiveDate", this.EffDate));
        expectTransitInventories = this.TheCriteriaMgr.FindAll<ExpectTransitInventory>(criteria);        

        if (this.rblListFormat.SelectedIndex == 1)
        {
            this.GV_List.EnableViewState = false;
            this.GV_Detail.EnableViewState = true;
            this.GV_Order.EnableViewState = false;

           // this.mrpShipPlans = TheMrpShipPlanMgr.GetMrpShipPlans((isFlow ? this.flowOrLoc : null), (!isFlow ? this.flowOrLoc : null), this.itemCode, this.EffDate, this.WinDate, this.StartDate);
            this.mrpShipPlans = TheMrpShipPlanMgr.GetMrpShipPlansBB((isFlow ? this.flowOrLoc : null), (!isFlow ? this.flowOrLoc : null),this.itemCode, ListItem, this.EffDate, this.WinDate, this.StartDate);

            this.GV_Detail_DataBind(mrpShipPlans, expectTransitInventories);
            
        }
        else
        {
            this.GV_List.EnableViewState = true;
            this.GV_Detail.EnableViewState = false;
            this.GV_Order.EnableViewState = false;
            if (button == this.btnSearch)
            {            
               //this.mrpShipPlans = TheMrpShipPlanMgr.GetMrpShipPlans((isFlow ? this.flowOrLoc : null), (!isFlow ? this.flowOrLoc : null), this.itemCode, this.EffDate, this.WinDate, this.StartDate);
                this.mrpShipPlans = TheMrpShipPlanMgr.GetMrpShipPlansBB((isFlow ? this.flowOrLoc : null), (!isFlow ? this.flowOrLoc : null), this.itemCode, ListItem, this.EffDate, this.WinDate, this.StartDate);
            }

           // IList<MrpShipPlanView> mrpShipPlanViews = TheMrpShipPlanViewMgr.GetMrpShipPlanViews((isFlow ? this.flowOrLoc : null), (!isFlow ? this.flowOrLoc : null), this.itemCode, this.EffDate, this.WinDate, this.StartDate);
            IList<MrpShipPlanView> mrpShipPlanViews = TheMrpShipPlanViewMgr.GetMrpShipPlanViewsBB((isFlow ? this.flowOrLoc : null), (!isFlow ? this.flowOrLoc : null), this.itemCode, ListItem, this.EffDate, this.WinDate, this.StartDate);

            criteria = DetachedCriteria.For<ExpectTransitInventoryView>();
            criteria.Add(Expression.Eq("EffectiveDate", this.EffDate));
            IList<ExpectTransitInventoryView> transitInventoryViews = this.TheCriteriaMgr.FindAll<ExpectTransitInventoryView>(criteria);

            ScheduleView scheduleView = TheMrpShipPlanViewMgr.TransferMrpShipPlanViews2ScheduleView(mrpShipPlanViews, transitInventoryViews,  this.rblFlowOrLoc.SelectedValue, this.rblDateType.SelectedValue);
            this.GV_List_DataBind(scheduleView);
           
        }
    }

    private void GV_List_DataBind(ScheduleView scheduleView)
    {
        for (int i = this.GV_List.Columns.Count; i > 6; i--)
        {
            this.GV_List.Columns.RemoveAt(this.GV_List.Columns.Count - 1);
        }

        this.div_MRP_Detail.Visible = false;
        this.div_OrderDetail.Visible = false;
        this.fld_Group.Visible = true;

        if (scheduleView == null) return;
        IList<ScheduleBody> scheduleBodys = scheduleView.ScheduleBodys;
        IList<ScheduleHead> scheduleHeads = scheduleView.ScheduleHeads;

        #region add qty column
        if (scheduleHeads != null && scheduleHeads.Count > 0)
        {
            int i = 0;
            foreach (ScheduleHead scheduleHead in scheduleHeads)
            {
                string qty = "Qty" + i.ToString();
                if (enableDiscon)
                {
                    qty = "DisplayQty" + i.ToString();
                }

                PropertyInfo[] scheduleBodyPropertyInfo = typeof(ScheduleBody).GetProperties();
                foreach (PropertyInfo pi in scheduleBodyPropertyInfo)
                {
                    if (pi.Name != null && StringHelper.Eq(pi.Name.ToLower(), qty))
                    {
                        BoundField bfColumn = new BoundField();
                        bfColumn.DataField = qty;
                        bfColumn.DataFormatString = "{0:#,##0.##}";
                        bfColumn.HtmlEncode = false;
                        bfColumn.HeaderText = isWinTime ? scheduleHead.DateTo.ToString("yyyy-MM-dd") : scheduleHead.DateFrom.ToString("yyyy-MM-dd");
                        bfColumn.SortExpression = isWinTime ? scheduleHead.DateTo.ToString("yyyy-MM-dd") : scheduleHead.DateFrom.ToString("yyyy-MM-dd");
                        bfColumn.FooterText = isWinTime ? (scheduleHead.LastDateTo.HasValue ? scheduleHead.LastDateTo.Value.ToString("yyyy-MM-dd") : string.Empty) : (scheduleHead.LastDateFrom.HasValue ? scheduleHead.LastDateFrom.Value.ToString("yyyy-MM-dd") : string.Empty);
                        this.GV_List.Columns.Add(bfColumn);
                        break;
                    }
                }
                i++;
            }
            this.ltl_GV_List_Result.Visible = false;
        }
        else
        {
            this.ltl_GV_List_Result.Visible = true;
        }
        #endregion
        this.GV_List.DataSource = scheduleBodys;
        this.GV_List.DataBind();
        //this.btnSave.Visible = false;
    }

    private void GV_Detail_DataBind(IList<MrpShipPlan> mrpShipPlans, IList<ExpectTransitInventory> transitList)
    {
        if (mrpShipPlans == null || mrpShipPlans.Count == 0)
        {
            this.ltl_MRP_List_Result.Visible = true;
           // this.btnSave.Visible = false;
        }
        else
        {
            if (mrpShipPlans.Count > 5000)
            {
                mrpShipPlans = mrpShipPlans.Take(5000).ToList();
                ShowWarningMessage("Common.Export.Warning.GreatThan5000", mrpShipPlans.Count.ToString());
            }
            this.ltl_MRP_List_Result.Visible = false;
          //  this.btnSave.Visible = true;
        }
        this.GV_Detail.DataSource = mrpShipPlans;
        this.GV_Detail.DataBind();

        this.div_MRP_Detail.Visible = true;
        this.div_OrderDetail.Visible = false;
        this.fld_Group.Visible = false;
    }


    protected void tbFlow_TextChanged(Object sender, EventArgs e)
    {
        dotbFlow_TextChanged(tbFlow.Text);
    }

    private void dotbFlow_TextChanged(string flowCode)
    {
        try
        {
            Flow currentFlow = TheFlowMgr.LoadFlow(flowCode, true, true);
            if (currentFlow != null)
            {
                this.FlowCode = currentFlow.Code;
                this.FlowType = currentFlow.Type;

                this.cbReleaseOrder.Checked = currentFlow.IsAutoRelease;
                this.cbPrintOrder.Checked = currentFlow.NeedPrintOrder;
                if (this.ScheduleDate.HasValue)
                {
                    if (isWinTime)
                    {
                        DateTime winTime = FlowHelper.GetWinTime(currentFlow, this.ScheduleDate.Value);
                        this.tbWinTime.Text = winTime.ToString("yyyy-MM-dd HH:mm");
                        double leadTime = currentFlow.LeadTime.HasValue ? (double)currentFlow.LeadTime.Value : 0;
                        this.tbStartTime.Text = winTime.AddHours(-leadTime).ToString("yyyy-MM-dd HH:mm");
                    }
                    else
                    {
                        double leadTime = currentFlow.LeadTime.HasValue ? (double)currentFlow.LeadTime.Value : 0;
                        DateTime winTime = FlowHelper.GetWinTime(currentFlow, this.ScheduleDate.Value.AddHours(leadTime));
                        this.tbWinTime.Text = winTime.ToString("yyyy-MM-dd HH:mm");
                        this.tbStartTime.Text = winTime.AddHours(-leadTime).ToString("yyyy-MM-dd HH:mm");
                    }
                }

                this.hfLeadTime.Value = currentFlow.LeadTime.ToString();
                this.hfEmTime.Value = currentFlow.EmTime.ToString();

                //  InitDetailParamater(orderHead);

                if (currentFlow.Type == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
                {
                    this.ltlShift.Text = "${MasterData.WorkCalendar.Shift}:";
                    this.ltlShift.Visible = true;
                    this.ucShift.Visible = true;
                    //this.tbScheduleTime.Visible = false;
                    this.BindShift(currentFlow);
                }
                else if (!enableDiscon && (currentFlow.Type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION
                   || currentFlow.Type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_SUBCONCTRACTING
                   || currentFlow.Type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT))
                {
                    this.ltlShift.Visible = true;
                    this.ltlShift.Text = "${MasterData.Order.OrderHead.SettleTime}:";
                    this.ucShift.Visible = false;
                    this.tbSettleTime.Visible = true;
                    //this.tbScheduleTime.Visible = true;
                    this.tbSettleTime.Text = this.tbWinTime.Text;
                }
                else
                {
                    this.ltlShift.Visible = false;
                    this.ucShift.Visible = false;
                    this.tbSettleTime.Visible = false;
                }

                IList<MrpShipPlanView> mrpShipPlanViews = TheMrpShipPlanViewMgr.GetMrpShipPlanViews((isFlow ? this.flowOrLoc : null), (!isFlow ? this.flowOrLoc : null), this.itemCode, this.EffDate, null, null);

                DetachedCriteria criteria = DetachedCriteria.For<ExpectTransitInventoryView>();
                criteria.Add(Expression.Eq("EffectiveDate", this.EffDate));
                IList<ExpectTransitInventoryView> transitInventoryViews = this.TheCriteriaMgr.FindAll<ExpectTransitInventoryView>(criteria);

               
                ScheduleView scheduleView = TheMrpShipPlanViewMgr.TransferMrpShipPlanViews2ScheduleView(mrpShipPlanViews, transitInventoryViews, this.rblFlowOrLoc.SelectedValue, this.rblDateType.SelectedValue);

                string qty = "Qty" + ColumnNum.ToString();
                string actQty = "ActQty" + ColumnNum.ToString();
                string requiredQty = "RequiredQty" + ColumnNum.ToString();

                IList<ScheduleBody> scheduleBodyList = new List<ScheduleBody>();
                foreach (ScheduleBody body in scheduleView.ScheduleBodys)
                {
                    if (currentFlow.FlowDetails != null && currentFlow.FlowDetails.Count > 0)
                    {
                        var p = from det in currentFlow.FlowDetails
                                where det.Item.Code == body.Item
                                select det;

                        if (p != null && p.Count() > 0)
                        {
                            PropertyInfo qtyProp = typeof(ScheduleBody).GetProperty(qty);
                            PropertyInfo actQtyProp = typeof(ScheduleBody).GetProperty(actQty);
                            PropertyInfo requiredQtyProp = typeof(ScheduleBody).GetProperty(requiredQty);

                            body.Qty0 = (decimal)qtyProp.GetValue(body, null);
                            body.ActQty0 = (decimal)actQtyProp.GetValue(body, null);
                            body.RequiredQty0 = (decimal)requiredQtyProp.GetValue(body, null);

                            scheduleBodyList.Add(body);
                        }
                    }
                }

                this.GV_Order.DataSource = scheduleBodyList;
                this.GV_Order.DataBind();
            }
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    private void BindShift(Flow currentFlow)
    {
        string regionCode = currentFlow != null ? currentFlow.PartyFrom.Code : string.Empty;
        DateTime dateTime = this.tbStartTime.Text.Trim() == string.Empty ? DateTime.Today : DateTime.Parse(this.tbStartTime.Text);
        this.ucShift.BindList(dateTime, regionCode);
    }

    protected void tbWinTime_TextChanged(object sender, EventArgs e)
    {
        if (this.FlowType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        {
            Flow currentFlow = TheFlowMgr.LoadFlow(this.FlowCode, false);
            this.BindShift(currentFlow);
        }
    }
}

