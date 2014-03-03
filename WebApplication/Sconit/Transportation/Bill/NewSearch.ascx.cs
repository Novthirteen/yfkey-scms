using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity.Transportation;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Collections;
using com.Sconit.Utility;
public partial class Transportation_Bill_NewSearch : SearchModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler CreateEvent;

    private string billNo
    {
        get
        {
            return (string)ViewState["billNo"];
        }
        set
        {
            ViewState["billNo"] = value;
        }
    }

    public void InitPageParameter(bool isPopup, TransportationBill transportationBill)
    {
        if (isPopup)
        {
            this.billNo = transportationBill.BillNo;
            this.tbPartyCode.Visible = false;
            this.ltlParty.Text = transportationBill.BillAddress.Party.Name;
            this.ltlParty.Visible = true;
            this.IsRelease.Visible = false;
            this.btnConfirm.Visible = false;
            this.btnBack.Visible = false;
            this.btnAddDetail.Visible = true;
            this.btnClose.Visible = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.PageCleanUp();
        }

        tbPartyCode.ServiceParameter = "string:" + this.CurrentUser.Code;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    /// <summary>
    /// 批量导入运输单 taskno:181695 
    /// djin 2013-9-2
    /// </summary>
    protected void btnImport_Click(object sender, EventArgs e)
    {
        fs01.Visible = true;
    }


    /// <summary>
    /// 文件上传 taskno:181695 
    /// djin 2013-9-2
    /// </summary>
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        HSSFWorkbook excel = new HSSFWorkbook(fileUpload.PostedFile.InputStream);
        Sheet sheet = excel.GetSheetAt(0);
        IEnumerator rows = sheet.GetRowEnumerator();
        //Row custrow = sheet.GetRow(2);
        // string sup = custrow.GetCell(1).StringCellValue;//客户代码
        //Supplier su = TheSupplierMgr.LoadSupplier(sup);
        //Row row_startdate = sheet.GetRow(3);
        //string startdate = row_startdate.GetCell(1).StringCellValue;//开始日期
        //Row row_enddate = sheet.GetRow(4);
        //string enddate = row_enddate.GetCell(1).StringCellValue;//结束日期
        //startdate = startdate == string.Empty ? DateTime.Now.AddMonths(-1).ToShortDateString() : startdate;
        //enddate = enddate == string.Empty ? DateTime.Now.ToShortDateString() : enddate;
        ImportHelper.JumpRows(rows, 1);
        IList<TransportationActBill> tactbillList = new List<TransportationActBill>();
        Hashtable th = new Hashtable();
        string supply = string.Empty;
        while (rows.MoveNext())
        {
            Row curow = (HSSFRow)rows.Current;
            string shiporder = curow.GetCell(0).StringCellValue;
            if (th.ContainsKey(shiporder)) continue;//避免重复
            // decimal cur = decimal.Parse(curow.GetCell(1).NumericCellValue.ToString());
            if (shiporder != string.Empty)
            {

                IList<TransportationActBill> tactbill = TheTransportationActBillMgr.GetTransportationActBill(shiporder);

                if (tactbill.Count > 0)
                {
                    foreach (TransportationActBill tbill in tactbill)
                    {
                        if (!string.IsNullOrEmpty(supply))
                        {
                            if (tbill.BillAddress.Party.Code != supply)
                            {
                                ShowErrorMessage("行" + curow.RowNum.ToString() + "供应商的代码不一致！");
                                return;
                            }
                        }
                        else
                            supply = tbill.BillAddress.Party.Code;
                        if (tbill.Status == "Create")
                        {

                            tbill.CurrentBillQty = tbill.BillQty - tbill.BilledQty;
                            tbill.CurrentBillAmount = tbill.CurrentBillQty * tbill.UnitPrice;
                            tactbillList.Add(tbill);

                        }

                    }
                }
                else
                {
                    ShowErrorMessage("行" + curow.RowNum.ToString() + "还没有计价！");
                    return;
                }
            }
            else
            {
                ShowErrorMessage("行" + curow.RowNum.ToString() + "无运单号！");
                return;
            }

        }
        if (tactbillList.Count > 0)
        {
            IList<TransportationBill> transportationBillList = TheTransportationBillMgr.CreateTransportationBill(tactbillList, this.CurrentUser);
            if (transportationBillList != null && transportationBillList.Count > 0)
            {
                ExportResult(transportationBillList);
                btnBack_Click(sender, e);
            }

        }
        else
        {
            ShowErrorMessage("账单创建失败！");
        }
    }

    protected void ExportResult(IList<TransportationBill> tbillList)
    {
        HSSFWorkbook excel = new HSSFWorkbook();
        NPOI.SS.UserModel.Sheet sheet = excel.CreateSheet("TransportationBill");
        NPOI.SS.UserModel.Row row = sheet.CreateRow(0);
        row.CreateCell(0).SetCellValue("BILL NO");
        row.CreateCell(1).SetCellValue("ShipOrderNo");
        row.CreateCell(2).SetCellValue("单价");
        row.CreateCell(3).SetCellValue("开票数");
        row.CreateCell(4).SetCellValue("金额");
        int rowNum = 1;
        foreach (TransportationBill bill in tbillList)
        {
            foreach (TransportationBillDetail bd in bill.TransportationBillDetails)
            {
                NPOI.SS.UserModel.Row _row = sheet.CreateRow(rowNum);
                _row.CreateCell(0).SetCellValue(bd.Bill.BillNo);
                _row.CreateCell(1).SetCellValue(bd.ActBill.OrderNo);
                _row.CreateCell(2).SetCellValue((double)bd.UnitPrice);
                _row.CreateCell(3).SetCellValue((double)bd.BilledQty);
                _row.CreateCell(4).SetCellValue((double)bd.Amount);
                rowNum++;
            }
        }
        MemoryStream ms = new MemoryStream();
        excel.Write(ms);
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=TBillResult.xls"));
        Response.BinaryWrite(ms.ToArray());

        excel = null;
        ms.Close();
        ms.Dispose();

    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        fs01.Visible = false;
    }

    protected override void DoSearch()
    {
        string partyCode = this.tbPartyCode.Text != string.Empty ? this.tbPartyCode.Text.Trim() : string.Empty;
        string expenseNo = this.tbExpenseNo.Text != string.Empty ? this.tbExpenseNo.Text.Trim() : string.Empty;
        string startDate = this.tbStartDate.Text != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
        string endDate = this.tbEndDate.Text != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;
        string itemCode = this.tbItemCode.Text != string.Empty ? this.tbItemCode.Text.Trim() : string.Empty;
        string currency = this.tbCurrency.Text != string.Empty ? this.tbCurrency.Text.Trim() : string.Empty;

        DateTime? effDateFrom = null;
        if (startDate != string.Empty)
        {
            effDateFrom = DateTime.Parse(startDate);
        }

        DateTime? effDateTo = null;
        if (endDate != string.Empty)
        {
            effDateTo = DateTime.Parse(endDate).AddDays(1).AddMilliseconds(-1);
        }

        bool needRecalculate = bool.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_RECALCULATE_WHEN_TRANSPORTATIONBILL).Value);
        if (needRecalculate)
        {
            IList<TransportationActBill> allTransportationActBillList = TheTransportationActBillMgr.GetTransportationActBill(partyCode, expenseNo, effDateFrom, effDateTo, itemCode, currency, this.billNo, true);

            TheTransportationActBillMgr.RecalculatePrice(allTransportationActBillList, this.CurrentUser);
        }

        IList<TransportationActBill> transportationActBillList = TheTransportationActBillMgr.GetTransportationActBill(partyCode, expenseNo, effDateFrom, effDateTo, itemCode, currency, this.billNo);

        this.ucNewList.BindDataSource(transportationActBillList != null && transportationActBillList.Count > 0 ? transportationActBillList : null);
        this.ucNewList.Visible = true;
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            IList<TransportationActBill> transportationActBillList = this.ucNewList.PopulateSelectedData();
            IList<TransportationBill> transportationBillList = TheTransportationBillMgr.CreateTransportationBill(transportationActBillList, this.CurrentUser);
            this.ShowSuccessMessage("Transportation.TransportationBill.CreateSuccessfully", transportationBillList[0].BillNo);

            if (this.IsRelease.Checked)
            {
                TheTransportationBillMgr.ReleaseTransportationBill(transportationBillList[0].BillNo, this.CurrentUser);
                this.ShowSuccessMessage("Transportation.TransportationBill.ReleaseSuccessfully", transportationBillList[0].BillNo);
            }
            this.PageCleanUp();
            CreateEvent(transportationBillList[0].BillNo, null);
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        BackEvent(this, null);
    }

    protected void btnAddDetail_Click(object sender, EventArgs e)
    {
        try
        {
            IList<TransportationActBill> transportationActBillList = this.ucNewList.PopulateSelectedData();
            this.TheTransportationBillMgr.AddTransportationBillDetail(this.billNo, transportationActBillList, this.CurrentUser);
            this.ShowSuccessMessage("Transportation.TransportationBill.AddTransportationBillDetailSuccessfully");
            BackEvent(this, null);
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }

    protected void btnNamedQuery_Click(object sender, EventArgs e)
    {
        IDictionary<string, string> actionParameter = new Dictionary<string, string>();
        if (this.tbStartDate.Text != string.Empty)
        {
            actionParameter.Add("StartDate", this.tbStartDate.Text);
        }
        if (this.tbEndDate.Text != string.Empty)
        {
            actionParameter.Add("EndDate", this.tbEndDate.Text);
        }
        if (this.tbPartyCode.Text != string.Empty)
        {
            actionParameter.Add("PartyCode", this.tbPartyCode.Text);
        }
        if (this.tbExpenseNo.Text != string.Empty)
        {
            actionParameter.Add("ExpenseNo", this.tbExpenseNo.Text);
        }
        if (this.tbItemCode.Text != string.Empty)
        {
            actionParameter.Add("ItemCode", this.tbItemCode.Text);
        }
        if (this.tbCurrency.Text != string.Empty)
        {
            actionParameter.Add("Currency", this.tbCurrency.Text);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("StartDate"))
        {
            this.tbStartDate.Text = actionParameter["StartDate"];
        }
        if (actionParameter.ContainsKey("EndDate"))
        {
            this.tbEndDate.Text = actionParameter["EndDate"];
        }
        if (actionParameter.ContainsKey("PartyCode"))
        {
            this.tbPartyCode.Text = actionParameter["PartyCode"];
        }
        if (actionParameter.ContainsKey("ExpenseNo"))
        {
            this.tbExpenseNo.Text = actionParameter["ExpenseNo"];
        }
        if (actionParameter.ContainsKey("ItemCode"))
        {
            this.tbItemCode.Text = actionParameter["ItemCode"];
        }
        if (actionParameter.ContainsKey("Currency"))
        {
            this.tbCurrency.Text = actionParameter["Currency"];
        }
    }

    private void PageCleanUp()
    {
        this.tbStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
        this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        this.tbPartyCode.Text = string.Empty;
        this.tbExpenseNo.Text = string.Empty;
        this.tbItemCode.Text = string.Empty;
        this.tbCurrency.Text = string.Empty;

        this.ucNewList.Visible = false;
    }
}
