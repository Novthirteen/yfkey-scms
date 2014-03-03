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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Entity.Transportation;
using LeanEngine.Entity;
using com.Sconit.Entity.Exception;
using Button = System.Web.UI.WebControls.Button;
using Label = System.Web.UI.WebControls.Label;
using Microsoft.ApplicationBlocks.Data;
using System.Xml;
using com.Sconit.Control;
using DropDownList = System.Web.UI.WebControls.DropDownList;

public partial class Transportation_TransportationOrder_Edit : EditModuleBase
{
    public event EventHandler BackEvent;
    private DataSet ds_torder = new DataSet();//tordermstr
    private DataSet ds_ip = new DataSet();//ipmstr
    public string ips = string.Empty;
    // public string connstring = string.Empty;
    private string connstring = string.Empty;
    public bool onlyWSNFlag = false;
    protected string OrderNo
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

    protected void Page_Load(object sender, EventArgs e)
    {
        onlyWSNFlag = false;
        
    }

    protected override void OnUnload(EventArgs e)
    {
        if (OrderNo != null) { UpdateView(); UpdateViewButton(); }
        base.OnUnload(e);
    }

    public void InitPageParameter(string orderNo)
    {
        this.OrderNo = orderNo;
        this.ODS_Order.SelectParameters["orderNo"].DefaultValue = this.OrderNo;

        this.ucList.InitPageParameter(orderNo, true);
        this.ucList.Visible = true;

        UpdateViewButton();
    }

    protected void FV_Order_DataBound(object sender, EventArgs e)
    {
        TransportationOrder order = (TransportationOrder)((FormView)sender).DataItem;

        ((Label)(this.FV_Order.FindControl("tbShipFrom"))).Text = order.TransportationRoute != null ? order.TransportationRoute.ShipFrom.FullAddress : string.Empty;
        ((Label)(this.FV_Order.FindControl("tbShipTo"))).Text = order.TransportationRoute != null ? order.TransportationRoute.ShipTo.FullAddress : string.Empty;

        Controls_TextBox tbCarrier = (Controls_TextBox)(this.FV_Order.FindControl("tbCarrier"));
        Controls_TextBox tbCarrierBillAddress = (Controls_TextBox)(this.FV_Order.FindControl("tbCarrierBillAddress"));
        tbCarrier.ServiceParameter = "string:" + this.CurrentUser.Code;
        tbCarrier.DataBind();
        if (order.Carrier != null)
        {
            tbCarrier.Text = order.Carrier.Code;
        }
        if (order.CarrierBillAddress != null)
        {
            tbCarrierBillAddress.Text = order.CarrierBillAddress.Code;
        }

        Controls_TextBox tbVehicle = (Controls_TextBox)(this.FV_Order.FindControl("tbVehicle"));
        if (order.Vehicle != null)
        {
            tbVehicle.Text = order.Vehicle;
        }

        Controls_TextBox tbExpense = (Controls_TextBox)(this.FV_Order.FindControl("tbExpense"));
        if (order.Expense != null)
        {
            tbExpense.Text = order.Expense.Code;
        }



        com.Sconit.Control.CodeMstrDropDownList ddlPricingMethod = (com.Sconit.Control.CodeMstrDropDownList)(this.FV_Order.FindControl("ddlPricingMethod"));
        if (order.PricingMethod != null)
        {
            ddlPricingMethod.SelectedValue = order.PricingMethod;
            ddlPricingMethod_SelectedIndexChanged(null, null);
        }
        else
        {
            ddlPricingMethod.SelectedIndex = 0;
        }

        DropDownList ddlType = (DropDownList)(this.FV_Order.FindControl("ddlType"));
        if (order.VehicleType != null)
        {
            ddlType.SelectedValue = order.VehicleType;

        }

        UpdateView();
        UpdateViewButton();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            TheTransportationOrderMgr.CancelTransportationOrder(this.OrderNo, this.CurrentUser);
            TransportationOrder order = TheTransportationOrderMgr.LoadTransportationOrder(this.OrderNo);
            order.Remark = ((TextBox)(this.FV_Order.FindControl("tbRemark"))).Text.Trim();
            TheTransportationOrderMgr.UpdateTransportationOrder(order);
            ShowSuccessMessage("Transportation.TransportationOrder.CancelTransportationOrder.Successfully", this.OrderNo);

            UpdateViewButton();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        RequiredFieldValidator rfvCarrier = (RequiredFieldValidator)(this.FV_Order.FindControl("rfvCarrier"));
        RequiredFieldValidator rfvCarrierBillAddress = (RequiredFieldValidator)(this.FV_Order.FindControl("rfvCarrierBillAddress"));
        if (!rfvCarrier.IsValid || !rfvCarrierBillAddress.IsValid)
        {
            return;
        }
        try
        {
            TheTransportationOrderMgr.UpdateTransportationOrder(PrepareOrder());

            ShowSuccessMessage("Transportation.TransportationOrder.UpdateTransportationOrder.Successfully", this.OrderNo);
            UpdateView();
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }

    protected void btnStart_Click(object sender, EventArgs e)
    {
        try
        {
            RequiredFieldValidator rfvCarrier = (RequiredFieldValidator)(this.FV_Order.FindControl("rfvCarrier"));
            RequiredFieldValidator rfvCarrierBillAddress = (RequiredFieldValidator)(this.FV_Order.FindControl("rfvCarrierBillAddress"));
            if (!rfvCarrier.IsValid || !rfvCarrierBillAddress.IsValid)
            {
                return;
            }
            TransportationOrder order = PrepareOrder();
            order.Status = "Create";

            TheTransportationOrderMgr.UpdateTransportationOrder(order);
            TheTransportationOrderMgr.StartTransportationOrder(this.OrderNo, this.CurrentUser);

            ShowSuccessMessage("Transportation.TransportationOrder.StartTransportationOrder.Successfully", this.OrderNo);

            UpdateView();
            this.FV_Order.DataBind();
            UpdateViewButton();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnCheck_Click(object sender, EventArgs e)
    {

        try
        {
            RequiredFieldValidator rfvCarrier = (RequiredFieldValidator)(this.FV_Order.FindControl("rfvCarrier"));
            RequiredFieldValidator rfvCarrierBillAddress = (RequiredFieldValidator)(this.FV_Order.FindControl("rfvCarrierBillAddress"));
            RequiredFieldValidator rfvType = (RequiredFieldValidator)(this.FV_Order.FindControl("rfvType"));

            if (!rfvCarrier.IsValid || !rfvCarrierBillAddress.IsValid || !rfvType.IsValid)
            {
                return;
            }
            btnSave_Click(sender, e);
            ShowSuccessMessage("Transportation.TransportationOrder.Checked.Successfully", this.OrderNo);
            TransportationOrder order = TheTransportationOrderMgr.LoadTransportationOrder(this.OrderNo);
            order.Status = "Checked";
            order.Remark = ((TextBox)(this.FV_Order.FindControl("tbRemark"))).Text.Trim();
            TheTransportationOrderMgr.UpdateTransportationOrder(order);
            UpdateView();
            this.FV_Order.DataBind();
            UpdateViewButton();
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            const string template = "SHP.xls";
            string printUrl = TheReportMgr.WriteToFile(template, this.OrderNo);
            Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + printUrl + "'); </script>");
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }

    protected void btnComplete_Click(object sender, EventArgs e)
    {
        try
        {
            ds = needCaluate();
            if (onlyWSNFlag == false)
            {
                TransportationOrder to = TheTransportationOrderMgr.LoadTransportationOrder(OrderNo);
                to.Status = "In-Process";
                to.Remark = ((TextBox)(this.FV_Order.FindControl("tbRemark"))).Text.Trim();
                TheTransportationOrderMgr.UpdateTransportationOrder(to);
                TheTransportationOrderMgr.CompleteTransportationOrder(this.OrderNo, this.CurrentUser);
            }
            else
            {
                createTBill();
                TransportationOrder to = TheTransportationOrderMgr.LoadTransportationOrder(OrderNo);
                to.Status = "Complete";
                to.Remark = ((TextBox)(this.FV_Order.FindControl("tbRemark"))).Text.Trim();
                TheTransportationOrderMgr.UpdateTransportationOrder(to);
            }
            if (ds != null)//djin
                Caluate();
            ShowSuccessMessage("Transportation.TransportationOrder.CompleteTransportationOrder.Successfully", this.OrderNo);
            if (ds != null)
                Restore(ds);
            UpdateViewButton();
        }
        catch (BusinessErrorException ex)
        {
            Restore(ds);
            ShowErrorMessage(ex);
        }
    }

    protected void tbVehicle_TextChanged(object sender, EventArgs e)
    {
        Controls_TextBox tbVehicle = (Controls_TextBox)(this.FV_Order.FindControl("tbVehicle"));
        string vehicleCode = tbVehicle.Text.Trim();
        if (vehicleCode != String.Empty)
        {
            Vehicle vehicle = TheVehicleMgr.LoadVehicle(vehicleCode);
            if (vehicle != null)
            {
                Controls_TextBox tbCarrier = (Controls_TextBox)(this.FV_Order.FindControl("tbCarrier"));
                if (vehicle.Carrier != null)
                {
                    tbCarrier.Text = vehicle.Carrier.Code;
                    Controls_TextBox tbCarrierBillAddress = (Controls_TextBox)(this.FV_Order.FindControl("tbCarrierBillAddress"));
                    BillAddress carrierBillAddress = TheBillAddressMgr.GetDefaultBillAddress(vehicle.Carrier.Code);
                    if (carrierBillAddress != null)
                    {
                        tbCarrierBillAddress.Text = carrierBillAddress.Code;
                    }
                }

                if (vehicle.Carrier != null)
                {
                    tbCarrier.Text = vehicle.Carrier.Code;
                }

                DropDownList ddlType = (DropDownList)(this.FV_Order.FindControl("ddlType"));
                if (vehicle.Type != null)
                {
                    ddlType.SelectedValue = vehicle.Type;
                }

                TextBox tbDriver = (TextBox)(this.FV_Order.FindControl("tbDriver"));
                if (vehicle.Driver != null)
                {
                    tbDriver.Text = vehicle.Driver;
                }
            }
        }
    }

    protected void btnValuate_Click(object sender, EventArgs e)
    {
        try
        {

            ds = needCaluate();
            if (onlyWSNFlag == false)
                TheTransportationOrderMgr.ValuateTransportationOrder(this.OrderNo, this.CurrentUser);
            else
            {
                createTBill();
            }
            if (ds != null)//djin
                Caluate();
            ShowSuccessMessage("Transportation.TransportationOrder.ValuateTransportationOrder.Successfully", this.OrderNo);
            if (ds != null)
                Restore(ds);
            UpdateView();
            UpdateViewButton();
            TransportationOrder to = TheTransportationOrderMgr.LoadTransportationOrder(OrderNo);
            to.Status = "Close";
            to.Remark = ((TextBox)(this.FV_Order.FindControl("tbRemark"))).Text.Trim();
            TheTransportationOrderMgr.UpdateTransportationOrder(to);
        }
        //BusinessErrorException
        catch (BusinessErrorException ex)
        {
            Restore(ds);
            ShowErrorMessage(ex.Message);
        }
    }

    #region  djin 2012-4-1
    protected void tbExpense_TextChanged(object sender, EventArgs e)
    {
        Controls_TextBox tbExpense = (Controls_TextBox)(this.FV_Order.FindControl("tbExpense"));
        Controls_TextBox tbCarrier = (Controls_TextBox)(this.FV_Order.FindControl("tbCarrier"));
        TextBox tbCarrier1 = (TextBox)(this.FV_Order.FindControl("tbCarrier1"));
        string expenseCode = tbExpense.Text.Trim();
        if (expenseCode != String.Empty)
        {
            Expense expense = TheExpenseMgr.LoadExpense(expenseCode);
            if (expense != null)
            {

                if (expense.Carrier != null)
                {
                    tbCarrier.Text = expense.Carrier.Code;
                    Controls_TextBox tbCarrierBillAddress = (Controls_TextBox)(this.FV_Order.FindControl("tbCarrierBillAddress"));
                    BillAddress carrierBillAddress = TheBillAddressMgr.GetDefaultBillAddress(expense.Carrier.Code);
                    if (carrierBillAddress != null)
                    {
                        tbCarrierBillAddress.Text = carrierBillAddress.Code;
                    }
                    tbCarrier1.Text = expense.Carrier.Code;
                }

                tbCarrier1.Visible = true;
                tbCarrier.Visible = false;
            }
            else
            {
                tbCarrier1.Visible = false;
                tbCarrier.Visible = true;
            }
        }
        else
        {
            tbCarrier1.Visible = false;
            tbCarrier.Visible = true;
        }
    }
    public DataSet ds = null;
    protected bool Caluate()
    {
        if (onlyWSNFlag == false)
        {
            string carrier = ((Controls_TextBox)(this.FV_Order.FindControl("tbCarrier"))).Text;
            string shipid = ((Label)(this.FV_Order.FindControl("tbOrderNo"))).Text;
            string pricingmethod = ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_Order.FindControl("ddlPricingMethod"))).SelectedValue;
            string vchtype = ((DropDownList)(this.FV_Order.FindControl("ddlType"))).SelectedValue;
            string shipfrom = ((Label)(this.FV_Order.FindControl("tbShipFrom"))).Text;
            string shipto = ((Label)(this.FV_Order.FindControl("tbShipTo"))).Text;
            if (pricingmethod == "SHIPT" || pricingmethod == "KG")
                return false;
            string s = "select unitprice,minvolume from WF_SpordicTranView where  ";
            s += "code='" + carrier + "' and [from]='" + shipfrom + "' and [to]='" + shipto + "' and [vehicleType]='" + vchtype + "'";
            DataSet stview = SqlHelper.ExecuteDataset(connstring, CommandType.Text, s);
            string vol_sql = "select * from tactbill where orderno='" + shipid + "'";
            DataSet actbill = SqlHelper.ExecuteDataset(connstring, CommandType.Text, vol_sql);
            double scmsvol = Convert.ToDouble(actbill.Tables[0].Rows[0]["BillQty"]);
            double price = 0;
            double wfvolume = 0;
            DataSet wfvol = SqlHelper.ExecuteDataset(connstring, CommandType.Text, "select * from ipmstr where ipno in(" + ips.TrimEnd(new char[] { ',' }) + ")");
            foreach (DataRow dr in wfvol.Tables[0].Rows)
            {
                wfvolume += Convert.ToDouble(dr["CompleteLatency"]);
            }
            if (scmsvol >= Convert.ToDouble(stview.Tables[0].Rows[0]["minvolume"]))//判断是否满足最小运量
                price = (wfvolume + scmsvol) * Convert.ToDouble(stview.Tables[0].Rows[0]["unitprice"]);
            else if (scmsvol + wfvolume >= Convert.ToDouble(stview.Tables[0].Rows[0]["minvolume"]))
                price = (scmsvol + wfvolume) * Convert.ToDouble(stview.Tables[0].Rows[0]["unitprice"]);
            else if (scmsvol + wfvolume < Convert.ToDouble(stview.Tables[0].Rows[0]["minvolume"]))
                price = Convert.ToDouble(stview.Tables[0].Rows[0]["minvolume"]) * Convert.ToDouble(stview.Tables[0].Rows[0]["unitprice"]);
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, "update tactbill set billqty=" + (double)(scmsvol + wfvolume) + ",billamount=" + price + " where orderno='" + shipid + "'");//更新到tbillact
            return true;
        }
        else
        { return true; }
    }
    protected bool Restore(DataSet ds)
    {
        if (ds != null)
        {

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, "insert into torderdet(orderno,ipno) values('" + dr["orderno"].ToString() + "','" + dr["ipno"].ToString() + "')");


                }

            }
        }
        return true;
    }
    protected void createTBill()
    {
        if ((TheTransportationOrderMgr.LoadTransportationOrder(OrderNo)).Status == "In-Process")
        {
        TransportationActBill tb = new TransportationActBill();
        tb.BillAddress=TheBillAddressMgr.LoadBillAddress(((Controls_TextBox)(this.FV_Order.FindControl("tbCarrierBillAddress"))).Text);
        tb.Currency =TheCurrencyMgr.LoadCurrency("RMB");
        tb.Status="Create";
        tb.IsIncludeTax = false;
        tb.OrderNo = this.OrderNo;
        tb.TransType = "Transportation";
        tb.EffectiveDate = DateTime.Now;
        tb.CreateDate = DateTime.Now;
        tb.CreateUser = CurrentUser;
        tb.LastModifyUser = CurrentUser;
        tb.LastModifyDate = DateTime.Now;
        tb.IsProvisionalEstimate = false;
        string carrier=string.Empty;
        tb.PricingMethod = ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_Order.FindControl("ddlPricingMethod"))).SelectedValue;
        carrier=((Controls_TextBox)(this.FV_Order.FindControl("tbCarrier"))).Text + "WL";
         string vchtype = ((DropDownList)(this.FV_Order.FindControl("ddlType"))).SelectedValue;
        int shipto=(TheTransportationOrderMgr.LoadTransportationOrder(this.OrderNo)).TransportationRoute.ShipTo.Id;
        int shipfrom=(TheTransportationOrderMgr.LoadTransportationOrder(this.OrderNo)).TransportationRoute.ShipFrom.Id;
        DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.Text, "select id from TPriceListDet where Tpricelist='" + carrier + "' and startdate<'" + DateTime.Now.ToString() + "' and enddate >'" + DateTime.Now.ToString() + "' and currency='RMB' and pricingmethod='" + tb.PricingMethod + "' and vehicletype='" + vchtype + "' and shipto='" + shipto + "' and shipfrom='" + shipfrom + "' ");
        if (ds.Tables[0].Rows.Count == 0)
        {
            throw new BusinessErrorException("没有找到该类型的价格单");
             
        }
        int _id = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        tb.PriceListDetail=TheTransportPriceListDetailMgr.LoadTransportPriceListDetail(_id);
        tb.PriceList = TheTransportPriceListMgr.LoadTransportPriceList(carrier);
        tb.UnitPrice = tb.PriceListDetail.UnitPrice;
        tb.ShipFrom = tb.PriceListDetail.ShipFrom;
        tb.ShipTo = tb.PriceListDetail.ShipTo;
        tb.VehicleType = tb.PriceListDetail.VehicleType;
        if (tb.PricingMethod != "SHIPT")
        {
            decimal qty = 0;
            foreach (DataRow dr in ds_ip.Tables[0].Rows)
            {
                qty += Convert.ToDecimal((TheInProcessLocationMgr.LoadInProcessLocation(dr["ipno"].ToString())).CompleteLatency);
            }
            tb.BillQty = qty;
            tb.BilledQty = 0;
            if (qty < tb.PriceListDetail.MinVolume)
                tb.BillAmount = tb.PriceListDetail.MinVolume * tb.UnitPrice;
            else
                tb.BillAmount = qty * tb.UnitPrice;
            tb.BilledAmount = 0;
        }
        else
        {
            tb.BillQty = 1;
            tb.BilledQty = 0;
            tb.BillAmount=tb.PriceListDetail.UnitPrice;
            tb.BilledAmount=0;
        }
        TheTransportationActBillMgr.CreateTransportationActBill(tb);
        }
        else
            return ;
    
    }
    protected DataSet needCaluate()
    {
        XmlTextReader reader = new XmlTextReader(Server.MapPath("Config/properties.config"));
        XmlDocument doc = new XmlDocument();
        doc.Load(reader);//  
        reader.Close();//
        connstring = doc.SelectSingleNode("/configuration/properties/connectionString").InnerText.Trim();
        ds_torder = SqlHelper.ExecuteDataset(connstring, CommandType.Text, "select * from torderdet where orderno='" + this.OrderNo + "'");
        if (ds_torder.Tables[0].Rows.Count == 0)
            return null;
        int init = ds_torder.Tables[0].Rows.Count;
        int cur = 0;
        string shipid = ((Label)(this.FV_Order.FindControl("tbOrderNo"))).Text;
        for (int i = 0; i < ds_torder.Tables[0].Rows.Count; i++)
        {
            if (ds_torder.Tables[0].Rows[i]["ipno"].ToString().IndexOf("WSN") >= 0)
            {
                ips += "'" + ds_torder.Tables[0].Rows[i]["ipno"].ToString() + "',";
                cur++;
            }
        }
        if (ips == "")
            return null;
        if (cur == init)
            onlyWSNFlag = true;
        ds_ip = SqlHelper.ExecuteDataset(connstring, CommandType.Text, "select * from torderdet where ipno in(" + ips.TrimEnd(new char[] { ',' }) + ") and orderno='" + shipid + "'");
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, "delete from torderdet where ipno in(" + ips.TrimEnd(new char[] { ',' }) + ") and orderno='" + shipid + "'");

        return ds_ip;

    }
    #endregion

    private TransportationOrder PrepareOrder()
    {
        TransportationOrder order = TheTransportationOrderMgr.LoadTransportationOrder(this.OrderNo);

        Controls_TextBox tbCarrier = (Controls_TextBox)(this.FV_Order.FindControl("tbCarrier"));

        if (tbCarrier.Text.Trim() != string.Empty)
        {
            order.Carrier = TheCarrierMgr.LoadCarrier(tbCarrier.Text.Trim());
        }

        Controls_TextBox tbCarrierBillAddress = (Controls_TextBox)(this.FV_Order.FindControl("tbCarrierBillAddress"));
        if (tbCarrierBillAddress.Text.Trim() != string.Empty)
        {
            order.CarrierBillAddress = TheBillAddressMgr.LoadBillAddress(tbCarrierBillAddress.Text.Trim());
        }
        else
        {
            order.CarrierBillAddress = TheBillAddressMgr.GetDefaultBillAddress(order.Carrier.Code);
        }


        Controls_TextBox tbVehicle = (Controls_TextBox)(this.FV_Order.FindControl("tbVehicle"));
        order.Vehicle = tbVehicle.Text.Trim();

        Controls_TextBox tbExpense = (Controls_TextBox)(this.FV_Order.FindControl("tbExpense"));
        order.Expense = TheExpenseMgr.LoadExpense(tbExpense.Text.Trim());

        DropDownList ddlType = (DropDownList)(this.FV_Order.FindControl("ddlType"));
        if (ddlType.SelectedValue != string.Empty)
        {
            order.VehicleType = ddlType.SelectedValue;
        }

        com.Sconit.Control.CodeMstrDropDownList ddlPricingMethod = (com.Sconit.Control.CodeMstrDropDownList)(this.FV_Order.FindControl("ddlPricingMethod"));
        if (ddlPricingMethod.SelectedValue != string.Empty)
        {
            order.PricingMethod = ddlPricingMethod.SelectedValue;
        }
        order.VehicleDriver = ((TextBox)(this.FV_Order.FindControl("tbDriver"))).Text.Trim();
        order.PallentCount = Int32.Parse(((TextBox)(this.FV_Order.FindControl("tbPallentCount"))).Text.Trim());
        order.LastModifyDate = DateTime.Now;
        order.LastModifyUser = this.CurrentUser;
        order.Remark = ((TextBox)(this.FV_Order.FindControl("tbRemark"))).Text.Trim();
        //added by williamlu@esteering.cn
        order.ReferencePallentCount = ((CheckBox)(this.FV_Order.FindControl("IsExcess"))).Checked ? 1 : 0;
        //added end
        return order;
    }

    private void UpdateView()
    {
        TransportationOrder order = TheTransportationOrderMgr.LoadTransportationOrder(this.OrderNo);

        bool showInputTxt = false;
        User currentUser = this.Page.Session["Current_User"] as User;

        if (order.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
        {
            showInputTxt = true;
        }
        if (currentUser.HasPermission("btnIPSave"))
        {
            showInputTxt = true;
        }
        else
        {
            showInputTxt = false;
        }
        if (order.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE) showInputTxt = false;
        ((Controls_TextBox)(this.FV_Order.FindControl("tbVehicle"))).Visible = showInputTxt;
        ((com.Sconit.Control.ReadonlyTextBox)(this.FV_Order.FindControl("tbVehicle1"))).Visible = !showInputTxt;
        ((Controls_TextBox)(this.FV_Order.FindControl("tbExpense"))).Visible = showInputTxt;
        ((com.Sconit.Control.ReadonlyTextBox)(this.FV_Order.FindControl("tbExpense1"))).Visible = !showInputTxt;
        ((Controls_TextBox)(this.FV_Order.FindControl("tbCarrier"))).Visible = showInputTxt && order.Expense == null;
        ((TextBox)(this.FV_Order.FindControl("tbCarrier1"))).Visible = !showInputTxt || order.Expense != null;
        ((Controls_TextBox)(this.FV_Order.FindControl("tbCarrierBillAddress"))).Visible = showInputTxt;
        ((TextBox)(this.FV_Order.FindControl("tbCarrierBillAddress1"))).Visible = !showInputTxt;
        ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_Order.FindControl("ddlPricingMethod"))).Enabled = showInputTxt;
        ((DropDownList)(this.FV_Order.FindControl("ddlType"))).Enabled = showInputTxt;
        ((TextBox)(this.FV_Order.FindControl("tbDriver"))).ReadOnly = !showInputTxt;
        ((TextBox)(this.FV_Order.FindControl("tbPallentCount"))).ReadOnly = !showInputTxt;
        ((CheckBox)(this.FV_Order.FindControl("IsExcess"))).Checked= order.ReferencePallentCount==1?true:false;
        if (order.Status == "Cancel" || order.Status == "Complete")
        { ((TextBox)(this.FV_Order.FindControl("tbRemark"))).ReadOnly = true;
        ((CheckBox)(this.FV_Order.FindControl("IsExcess"))).Enabled = false;
        }
    }

    private void UpdateViewButton()
    {
        TransportationOrder order = TheTransportationOrderMgr.LoadTransportationOrder(this.OrderNo);
        User currentUser = this.Page.Session["Current_User"] as User;

        Button btnSave = ((Button)(this.FV_Order.FindControl("btnSave")));
        Button btnStart = ((Button)(this.FV_Order.FindControl("btnStart")));
        Button btnCancel = ((Button)(this.FV_Order.FindControl("btnCancel")));
        Button btnPrint = ((Button)(this.FV_Order.FindControl("btnPrint")));
        Button btnComplete = ((Button)(this.FV_Order.FindControl("btnComplete")));
        Button btnValuate = ((Button)(this.FV_Order.FindControl("btnValuate")));
        Button btnCheck = ((Button)(this.FV_Order.FindControl("btnCheck")));
        btnSave.Visible = false;
        btnStart.Visible = false;
        btnCancel.Visible = false;
        btnPrint.Visible = false;
        btnComplete.Visible = false;
        btnCheck.Visible = false;
        btnValuate.Visible = !order.IsValuated && (order.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE
                                                    && order.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL);

        if (order.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
        {
            btnSave.Visible = true;
            //btnStart.Visible = true;
            // if (currentUser.HasPermission("btnIPCreate"))
            btnCancel.Visible = true;
            btnCheck.Visible = true;
        }
        else if (order.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT)
        {
            btnCancel.Visible = true;
            btnPrint.Visible = true;
        }
        else if (order.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
        {
            btnCancel.Visible = true;
            btnPrint.Visible = true;
            if (currentUser.HasPermission("btnIPStart"))
                btnComplete.Visible = true;
        }
        else if (order.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_COMPLETE)
        {
            btnPrint.Visible = true;
            btnValuate.Visible = false;
            btnCancel.Visible = false;
        }
        else if (order.Status == "Checked")//已审核
        {
            btnStart.Visible = true;
            btnValuate.Visible = false;
            btnCancel.Visible = true;
        }
        else if (order.Status == "Close")
        {
            btnComplete.Visible = true;
            btnPrint.Visible = true;

        }

    }

    protected void ddlPricingMethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        com.Sconit.Control.CodeMstrDropDownList ddlPricingMethod = ((com.Sconit.Control.CodeMstrDropDownList)(this.FV_Order.FindControl("ddlPricingMethod")));
        DropDownList ddlType = (DropDownList)(this.FV_Order.FindControl("ddlType"));

        ddlType.DataSource = GetVehicleTypeGroup(ddlPricingMethod.SelectedValue);
        ddlType.DataBind();
    }

    private IList<CodeMaster> GetVehicleTypeGroup(string pricingMethod)
    {
        IList<CodeMaster> vehicleTypeGroup = new List<CodeMaster>();
        if (pricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_SHIPT)
        {
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_2T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_5T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_8T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_10T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_12T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_20FOOT));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_40FOOT));
        }
        else if (pricingMethod == BusinessConstants.TRANSPORTATION_PRICING_METHOD_M3)
        {
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_SCATTERED));
        }
        else
        {
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_2T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_5T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_8T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_10T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_12T));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_20FOOT));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_40FOOT));
            vehicleTypeGroup.Add(GetVehicleType(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE_SCATTERED));
        }

        return vehicleTypeGroup;
    }

    private CodeMaster GetVehicleType(string vehicleType)
    {
        return TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.TRANSPORTATION_VEHICLE_TYPE, vehicleType);
    }

}
