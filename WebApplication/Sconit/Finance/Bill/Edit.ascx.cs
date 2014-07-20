using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;

public partial class Finance_Bill_Edit : EditModuleBase
{
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

    private int DecimalLength
    {
        get
        {
            return (int)ViewState["DecimalLength"];
        }
        set
        {
            ViewState["DecimalLength"] = value;
        }
    }

    public string PartyCode
    {
        get
        {
            return (string)ViewState["PartyCode"];
        }
        set
        {
            ViewState["PartyCode"] = value;
        }
    }

    public string BillNo
    {
        get
        {
            return (string)ViewState["BillNo"];
        }
        set
        {
            ViewState["BillNo"] = value;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Bill bill = this.TheBillMgr.LoadBill(this.BillNo, true);
        IList<object> list = new List<object>();
        if (bill != null)
        {
            list.Add(bill);
            list.Add(bill.BillDetails);
        }
        string barCodeUrl = "";
        if (bill.TransactionType == BusinessConstants.BILL_TRANS_TYPE_PO)
        {
            barCodeUrl = TheReportMgr.WriteToFile("Bill.xls", list);
        }
        else
        {
            barCodeUrl = TheReportMgr.WriteToFile("BillMarket.xls", list);
        }

        Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + barCodeUrl + "'); </script>");
        this.ShowSuccessMessage("MasterData.Bill.Print.Successful");
    }

    public void InitPageParameter(string billNo)
    {
        this.BillNo = billNo;
        this.ODS_Bill.SelectParameters["billNo"].DefaultValue = billNo;
        this.ODS_Bill.SelectParameters["includeDetail"].DefaultValue = true.ToString();

        if (this.ModuleType == BusinessConstants.BILL_TRANS_TYPE_PO)
        {
            this.Gv_List.Columns[2].Visible = false;
        }
        else if (this.ModuleType == BusinessConstants.BILL_TRANS_TYPE_SO)
        {
            Literal lblParty = this.FV_Bill.FindControl("lblParty") as Literal;
            lblParty.Text = "${MasterData.Bill.Customer}:";

            this.Gv_List.Columns[1].Visible = false;
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucNewSearch.BackEvent += new EventHandler(AddBack_Render);

        if (!IsPostBack)
        {
            this.ucNewSearch.ModuleType = this.ModuleType;
        }
    }

    protected void FV_Bill_DataBound(object sender, EventArgs e)
    {
        Bill bill = (Bill)((FormView)(sender)).DataItem;
        UpdateView(bill);
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            BillDetail billDetail = (BillDetail)e.Row.DataItem;

            TextBox tbAmount = (TextBox)e.Row.FindControl("tbAmount");
            tbAmount.Attributes["oldValue"] = tbAmount.Text;

            TextBox tbQty = (TextBox)e.Row.FindControl("tbQty");
            tbQty.Attributes["oldValue"] = tbQty.Text;

            TextBox tbDiscountRate = (TextBox)e.Row.FindControl("tbDiscountRate");
            TextBox tbDiscount = (TextBox)e.Row.FindControl("tbDiscount");
            if (billDetail.Bill.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
            {
                tbQty.ReadOnly = true;
                tbDiscountRate.ReadOnly = true;
                tbDiscount.ReadOnly = true;
            }
        }
    }

    protected void lbRefBillNo_Click(object sender, EventArgs e)
    {
        string refBillNo = ((LinkButton)(sender)).CommandArgument;
        InitPageParameter(refBillNo);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            IList<BillDetail> billDetailList = PopulateData(false);
            Bill bill = this.TheBillMgr.LoadBill(this.BillNo);
            TextBox tbExternalBillNo = this.FV_Bill.FindControl("tbExternalBillNo") as TextBox;
            TextBox tbMemo = this.FV_Bill.FindControl("tbMemo") as TextBox;

            if (tbMemo.Text.Trim() != string.Empty)
            {
                bill.Memo = tbMemo.Text.Trim();
            }
            else
                bill.Memo = string.Empty;
            if (tbExternalBillNo.Text.Trim() != string.Empty)
            {
                bill.ExternalBillNo = tbExternalBillNo.Text.Trim();
            }
            else
            {
                bill.ExternalBillNo = null;
            }

            if (this.tbTotalDiscount.Text.Trim() != string.Empty)
            {
                bill.Discount = decimal.Parse(this.tbTotalDiscount.Text.Trim());
            }
            else
            {
                bill.Discount = null;
            }
            bill.BillDetails = billDetailList;
            this.TheBillMgr.UpdateBill(bill, this.CurrentUser);
            
            this.ShowSuccessMessage("MasterData.Bill.UpdateSuccessfully", this.BillNo);
            this.FV_Bill.DataBind();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnTongji_Click(object sender, EventArgs e)
    {
        IList<BillDetail> result = TheBillMgr.LoadBill(this.BillNo, true).BillDetails;
        var gp = (from i in result group i by new { i.ActingBill.Item.Code,i.ActingBill.ReferenceItemCode, i.ActingBill.Item.Description } into g select new { g.Key.Code,g.Key.ReferenceItemCode, g.Key.Description, Amount = g.Sum(i => i.BilledQty) }).ToList();
        HZ.DataSource = gp;
        HZ.DataBind();
        HZ.Visible = true;
        field.Visible = true;
        mx.Visible = true;
        tbshowTotal.Text = "零件总数:" + gp.Sum(i => i.Amount).ToString("0");
        itemInList.DataSource = gp.Select(i => i.Code);
        itemInList.DataBind();
        itemMX.Visible = false;
        btnClearMX.Visible = false;
    }



    protected void btnQuery_Click(object sender, EventArgs e)
    {
        btnClearMX.Visible = true;

        itemMX.Visible = true;
        HZ.Visible = false;
        IList<BillDetail> result = TheBillMgr.LoadBill(this.BillNo, true).BillDetails;
        IList<BillDetail> ds = (from i in result where i.ActingBill.Item.Code == itemInList.SelectedValue select i).ToList();
        itemMX.DataSource = ds;
        itemMX.DataBind();
        tbshowTotal.Text = "零件总数:" + ds.Sum(i => i.BilledQty).ToString("0");
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        HZ.DataSource = null;
        HZ.DataBind();
        HZ.Visible = false;
        field.Visible = false;
        mx.Visible = false;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            this.TheBillMgr.ReleaseBill(this.BillNo, this.CurrentUser);
            this.ShowSuccessMessage("MasterData.Bill.ReleaseSuccessfully", this.BillNo);
            this.FV_Bill.DataBind();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            this.TheBillMgr.DeleteBill(this.BillNo, this.CurrentUser);
            this.ShowSuccessMessage("MasterData.Bill.DeleteSuccessfully", this.BillNo);
            this.BackEvent(this, e);
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            this.TheBillMgr.CloseBill(this.BillNo, this.CurrentUser);
            this.ShowSuccessMessage("MasterData.Bill.CloseSuccessfully", this.BillNo);
            this.FV_Bill.DataBind();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.TheBillMgr.CancelBill(this.BillNo, this.CurrentUser);
            this.ShowSuccessMessage("MasterData.Bill.CancelSuccessfully", this.BillNo);
            this.FV_Bill.DataBind();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnVoid_Click(object sender, EventArgs e)
    {
        try
        {
            this.TheBillMgr.VoidBill(this.BillNo, this.CurrentUser);
            this.ShowSuccessMessage("MasterData.Bill.VoidSuccessfully", this.BillNo);
            this.FV_Bill.DataBind();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (this.BackEvent != null)
        {
            this.BackEvent(this, e);
        }
    }

    protected void btnAddDetail_Click(object sender, EventArgs e)
    {
        IDictionary<string, string> actionParameter = new Dictionary<string, string>();
        actionParameter.Add("PartyCode", this.PartyCode);
        this.ucNewSearch.QuickSearch(actionParameter);
        this.ucNewSearch.Visible = true;
    }

    protected void btnDeleteDetail_Click(object sender, EventArgs e)
    {
        try
        {
            IList<BillDetail> billDetailList = PopulateData(true);
            this.TheBillMgr.DeleteBillDetail(billDetailList, this.CurrentUser);
            this.ShowSuccessMessage("MasterData.Bill.DeleteBillDetailSuccessfully");
            this.FV_Bill.DataBind();
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnRecalculate_Click(object sender, EventArgs e)
    {
        try
        {
           
                TheBillMgr.RecalculatePrice(this.BillNo, this.CurrentUser);
                this.ShowSuccessMessage("MasterData.ActingBill.Recalculate.Successfully");
            
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void AddBack_Render(object sender, EventArgs e)
    {
        this.ucNewSearch.Visible = false;
        this.FV_Bill.DataBind();
    }

    private void UpdateView(Bill bill)
    {
        #region 根据状态显示按钮
        if (bill.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
        {
            this.btnSave.Visible = true;
            this.btnSubmit.Visible = true;
            this.btnDelete.Visible = true;
            this.btnClose.Visible = false;
            this.btnCancel.Visible = false;
            this.btnVoid.Visible = false;
            this.btnAddDetail.Visible = true;
            this.btnDeleteDetail.Visible = true;
            this.btnPrint.Visible = false;
        }
        else if (bill.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT)
        {
            this.btnSave.Visible = false;
            this.btnSubmit.Visible = false;
            this.btnDelete.Visible = false;
            this.btnClose.Visible = true;
            if (bill.IsExport)
            {
                this.btnCancel.Visible = false;
            }
            else
            {
                this.btnCancel.Visible = true;
            }
            this.btnVoid.Visible = false;
            this.btnAddDetail.Visible = false;
            this.btnDeleteDetail.Visible = false;
            this.btnPrint.Visible = true;
        }
        else if (bill.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL)
        {
            this.btnSave.Visible = false;
            this.btnSubmit.Visible = false;
            this.btnDelete.Visible = false;
            this.btnClose.Visible = false;
            this.btnCancel.Visible = false;
            this.btnVoid.Visible = false;
            this.btnAddDetail.Visible = false;
            this.btnDeleteDetail.Visible = false;
            this.btnPrint.Visible = false;
        }
        else if (bill.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE)
        {
            this.btnSave.Visible = false;
            this.btnSubmit.Visible = false;
            this.btnDelete.Visible = false;
            this.btnClose.Visible = false;
            this.btnCancel.Visible = false;
            this.btnPrint.Visible = false;
            if (bill.BillType == BusinessConstants.CODE_MASTER_BILL_TYPE_VALUE_CANCEL)
            {
                this.btnVoid.Visible = false; ;
            }
            else
            {
                this.btnVoid.Visible = true;
            }
            this.btnAddDetail.Visible = false;
            this.btnDeleteDetail.Visible = false;
        }
        else if (bill.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_VOID)
        {
            this.btnSave.Visible = false;
            this.btnSubmit.Visible = false;
            this.btnDelete.Visible = false;
            this.btnClose.Visible = false;
            this.btnCancel.Visible = false;
            this.btnVoid.Visible = false;
            this.btnAddDetail.Visible = false;
            this.btnDeleteDetail.Visible = false;
            this.btnPrint.Visible = false;
        }

        this.btnRecalculate.Visible = bill.HasProvEst ? true : false; //暂估可以重新计价
        #endregion

        #region 根据状态隐藏/显示字段
        if (bill.Status != BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
        {
            this.Gv_List.Columns[0].Visible = false;
            this.tbTotalDiscountRate.ReadOnly = true;
            this.tbTotalDiscount.ReadOnly = true;

            TextBox tbExternalBillNo = this.FV_Bill.FindControl("tbExternalBillNo") as TextBox;
            tbExternalBillNo.ReadOnly = true;
        }
        else
        {
            this.Gv_List.Columns[0].Visible = true;
            this.tbTotalDiscountRate.ReadOnly = false;
            this.tbTotalDiscount.ReadOnly = false;

            TextBox tbExternalBillNo = this.FV_Bill.FindControl("tbExternalBillNo") as TextBox;
            tbExternalBillNo.ReadOnly = false;
        }
        #endregion

        #region 给总金额和折扣赋值
        this.tbTotalDetailAmount.Text = bill.TotalBillDetailAmount.ToString("F4");
        this.tbTotalAmount.Text = bill.TotalBillAmount.ToString("F4");
        this.tbTotalQty.Text = bill.TotalQty.ToString("F4");
        this.tbTotalDiscount.Text = (bill.Discount.HasValue ? bill.Discount.Value : 0).ToString("F4");
        this.tbTotalDiscountRate.Text = bill.TotalBillDiscountRate.ToString("F4");
        #endregion

        #region 初始化弹出窗口
        this.PartyCode = bill.BillAddress.Party.Code;
        this.ucNewSearch.InitPageParameter(true, bill);
        #endregion

        UpdateDetailView(bill.BillDetails);
    }

    private void UpdateDetailView(IList<BillDetail> billDetailList)
    {
        this.Gv_List.DataSource = billDetailList;
        this.Gv_List.DataBind();
    }

    private IList<BillDetail> PopulateData(bool isChecked)
    {
        if (this.Gv_List.Rows != null && this.Gv_List.Rows.Count > 0)
        {
            IList<BillDetail> billDetailList = new List<BillDetail>();
            foreach (GridViewRow row in this.Gv_List.Rows)
            {
                CheckBox checkBoxGroup = row.FindControl("CheckBoxGroup") as CheckBox;
                if (checkBoxGroup.Checked || !isChecked)
                {
                    HiddenField hfId = row.FindControl("hfId") as HiddenField;
                    TextBox tbQty = row.FindControl("tbQty") as TextBox;
                    TextBox tbDiscount = row.FindControl("tbDiscount") as TextBox;

                    BillDetail billDetail = new BillDetail();
                    billDetail.Id = int.Parse(hfId.Value);
                    billDetail.BilledQty = decimal.Parse(tbQty.Text);
                    billDetail.Discount = decimal.Parse(tbDiscount.Text);

                    billDetailList.Add(billDetail);
                }
            }
            return billDetailList;
        }

        return null;
    }
}
