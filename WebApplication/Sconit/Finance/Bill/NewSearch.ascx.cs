using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;
using com.Sconit.Utility;
using System.IO;
public partial class Finance_Bill_NewSearch : SearchModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler CreateEvent;
    //  public event EventHandler SearchEvent;
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

    public void InitPageParameter(bool isPopup, Bill bill)
    {
        if (isPopup)
        {
            this.billNo = bill.BillNo;
            this.tbPartyCode.Visible = false;
            this.ltlParty.Text = bill.BillAddress.Party.Name;
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
            this.ucNewList.ModuleType = this.ModuleType;
            this.PageCleanUp();
        }

        if (this.ModuleType == BusinessConstants.BILL_TRANS_TYPE_SO)
        {
            this.ltlPartyCode.Text = "${MasterData.ActingBill.Customer}:";
            this.ltlReceiver.Text = "${MasterData.ActingBill.ExternalReceiptNo}:";

            this.tbPartyCode.ServicePath = "CustomerMgr.service";
            this.tbPartyCode.ServiceMethod = "GetAllCustomer";
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void DoSearch()
    {
        string partyCode = this.tbPartyCode.Text != string.Empty ? this.tbPartyCode.Text.Trim() : string.Empty;
        string receiver = this.tbReceiver.Text != string.Empty ? this.tbReceiver.Text.Trim() : string.Empty;
        string startDate = this.tbStartDate.Text != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
        string endDate = this.tbEndDate.Text != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;
        string itemCode = this.tbItemCode.Text != string.Empty ? this.tbItemCode.Text.Trim() : string.Empty;
        string currency = this.tbCurrency.Text != string.Empty ? this.tbCurrency.Text.Trim() : string.Empty;
        string externalRece = ExternalReceiptNo.SelectedValue;

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

        bool needRecalculate = bool.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_RECALCULATE_WHEN_BILL).Value);
        if (needRecalculate)
        {
            IList<ActingBill> allactingBillList = TheActingBillMgr.GetActingBill(partyCode, receiver, effDateFrom, effDateTo, itemCode, currency, this.ModuleType, this.billNo, true);

            TheActingBillMgr.RecalculatePrice(allactingBillList, this.CurrentUser);
        }
        //djin 2013-3-20 内部客户
        if (partyCode != string.Empty)
        {
            var IsIntern = TheCustomerMgr.LoadCustomer(partyCode).IsIntern;
            this.tbIsIntern.Value = IsIntern.ToString();
        }


        IList<ActingBill> actingBillList = TheActingBillMgr.GetActingBill(partyCode, receiver, effDateFrom, effDateTo, itemCode, currency, this.ModuleType, this.billNo);

        //djin 2013-3-20 客户回单
        var hd = (from b in actingBillList
                  where b.ExternalReceiptNo != null
                  select b.ExternalReceiptNo.Substring(0, 2)).Distinct();

        ExternalReceiptNo.Items.Clear();
        ExternalReceiptNo.Items.Add(new ListItem("ALL", "ALL"));
        foreach (var i in hd)
        {
            ListItem item = new ListItem(i);
            ExternalReceiptNo.Items.Add(item);
        }
        if (externalRece != "ALL")
        {
            var afterF = from a in actingBillList where (a.ExternalReceiptNo != null && a.ExternalReceiptNo.Substring(0, 2) == externalRece) select a;

            this.ucNewList.BindDataSource(afterF.ToList());
        }
        else
        {
            this.ucNewList.BindDataSource(actingBillList != null && actingBillList.Count > 0 ? actingBillList : null);

        }

        //this.ucNewList.BindDataSource(actingBillList != null && actingBillList.Count > 0 ? actingBillList : null);
        this.ucNewList.Visible = true;
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            IList<ActingBill> actingBillList = this.ucNewList.PopulateSelectedData();
            IList<Bill> billList = TheBillMgr.CreateBill(actingBillList, this.CurrentUser);
            this.ShowSuccessMessage("MasterData.Bill.CreateSuccessfully", billList[0].BillNo);

            if (this.IsRelease.Checked)
            {
                TheBillMgr.ReleaseBill(billList[0].BillNo, this.CurrentUser);
                this.ShowSuccessMessage("MasterData.Bill.ReleaseSuccessfully", billList[0].BillNo);
            }
            this.PageCleanUp();
            CreateEvent(billList[0].BillNo, null);
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

    protected void btnImport_Click(object sender, EventArgs e)
    {
        fs01.Visible = true;
    }

    protected void btnAddDetail_Click(object sender, EventArgs e)
    {
        try
        {
            IList<ActingBill> actingBillList = this.ucNewList.PopulateSelectedData();
            //if (tbIsIntern.Value == "False")//内部客户结算方式
            //{
            //    string o = ((TextBox)(this.Parent.FindControl("ucEdit").FindControl("tbTotalDetailAmount"))).Text;
            //    decimal amount = decimal.Parse(o);
            //    foreach (ActingBill a in actingBillList)
            //    {
            //        ActingBill x = TheActingBillMgr.LoadActingBill(a.Id);
            //        if (amount + x.UnitPrice * a.CurrentBillQty > 1000000)
            //        {
            //            ShowErrorMessage("MasterData.Bill.AddBillDetailOverLimit");
            //            BackEvent(this, null);
            //        }
            //        else
            //        {
            //            amount += x.UnitPrice * a.CurrentBillQty;
            //        }
            //    }
            //}
            this.TheBillMgr.AddBillDetail(this.billNo, actingBillList, this.CurrentUser);
            this.ShowSuccessMessage("MasterData.Bill.AddBillDetailSuccessfully");
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
        if (this.tbReceiver.Text != string.Empty)
        {
            actionParameter.Add("Receiver", this.tbReceiver.Text);
        }
        if (this.tbItemCode.Text != string.Empty)
        {
            actionParameter.Add("ItemCode", this.tbItemCode.Text);
        }
        if (this.tbCurrency.Text != string.Empty)
        {
            actionParameter.Add("Currency", this.tbCurrency.Text);
        }

        //this.SaveNamedQuery(this.tbNamedQuery.Text, actionParameter);
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
        if (actionParameter.ContainsKey("Receiver"))
        {
            this.tbReceiver.Text = actionParameter["Receiver"];
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
        this.tbReceiver.Text = string.Empty;
        this.tbItemCode.Text = string.Empty;
        this.tbCurrency.Text = string.Empty;

        this.ucNewList.Visible = false;
    }

    public string _createdBillNo = string.Empty;

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        HSSFWorkbook excel = new HSSFWorkbook(fileUpload.PostedFile.InputStream);
        Sheet sheet = excel.GetSheetAt(0);
        IEnumerator rows = sheet.GetRowEnumerator();
        Row custrow = sheet.GetRow(2);
        string cust = custrow.GetCell(1).StringCellValue;//客户代码
        Customer customer = TheCustomerMgr.LoadCustomer(cust);
        Row row_startdate = sheet.GetRow(3);
        string startdate = row_startdate.GetCell(1).StringCellValue;//开始日期
        Row row_enddate = sheet.GetRow(4);
        string enddate = row_enddate.GetCell(1).StringCellValue;//结束日期
        startdate = startdate == string.Empty ? DateTime.Now.AddMonths(-1).ToShortDateString() : startdate;
        enddate = enddate == string.Empty ? DateTime.Now.ToShortDateString() : enddate;
        IList<ActingBill> actingBillList = TheActingBillMgr.GetActingBill(cust, "", DateTime.Parse(startdate), DateTime.Parse(enddate), "", "", this.ModuleType, this.billNo);

        var actbill = (from i in actingBillList//按ASN ITME聚合 得到总的可用数量
                       group i by new
                       {
                           i.IpNo,
                           i.Item.Code
                       } into k
                       select new
                       {
                           Item = k.Key.Code,
                           IpNo = k.Key.IpNo,
                           Amt = k.Sum(i => i.BillQty - i.BilledQty)
                       }).ToList();

        ImportHelper.JumpRows(rows, 7);
        IList<ActingBill> createBillList = new List<ActingBill>();
        IList<Bill> createdBill = new List<Bill>();

        while (rows.MoveNext())
        {
            Row curow = (HSSFRow)rows.Current;
            string asn = curow.GetCell(0).StringCellValue;
            string item = curow.GetCell(1).NumericCellValue.ToString();

            decimal qty = decimal.Parse(curow.GetCell(2).NumericCellValue.ToString());
            if (asn != string.Empty)
            {
                var temp_qty = actbill.Where(i => i.Item == item && i.IpNo == asn).ToList();
                if (temp_qty.Count > 0)
                {
                    if (Math.Abs(temp_qty[0].Amt) - Math.Abs(qty) >= 0)
                    {
                        IList<ActingBill> result = actingBillList.Where(i => i.Item.Code == item && i.IpNo == asn).ToList();
                        foreach (ActingBill _actbill in result)
                        {
                            if (qty == 0) break;//扣减完了
                            if (_actbill.BillQty - _actbill.BilledQty - _actbill.CurrentBillQty == 0) continue;//actbill可用数量用完了
                            if (_actbill.BillQty - _actbill.BilledQty - _actbill.CurrentBillQty - qty >= 0)
                            {
                                _actbill.CurrentBillQty = _actbill.CurrentBillQty + qty;
                                qty = 0;
                            }
                            else
                            {
                                _actbill.CurrentBillQty = _actbill.BillQty - _actbill.BilledQty - _actbill.CurrentBillQty;
                                qty = qty - _actbill.BillQty - _actbill.BilledQty - _actbill.CurrentBillQty;
                            }
                            createBillList.Add(_actbill);

                        }
                    }
                    else
                    {
                        ShowErrorMessage("行" + (curow.RowNum + 1).ToString() + "数量大于剩余开票数量！");
                        return;
                    }
                }
                else
                {
                    ShowErrorMessage("行" + (curow.RowNum + 1).ToString() + " ASN或零件不存在！请查询对应记录后再导入！");
                    return;
                }
            }
        }
        int cnt = 0;
        try
        {
            while (0 < createBillList.Count)
            {
                cnt++;
                var t = from i in createBillList
                        group i by new
                        {
                            i.Item.Code
                        } into k
                        select new
                        {
                            Item = k.Key.Code,
                            Amt = k.Sum(i => i.CurrentBillQty * i.UnitPrice)
                        };

                List<string> abM = new List<string>();
                List<string> zeroM = new List<string>();
                foreach (var i in t)
                {
                    if (i.Amt > 1000000)
                        abM.Add(i.Item);
                    if (i.Amt == 0 && cnt == 1)
                        zeroM.Add(i.Item);
                }

                #region 超过100w的
                foreach (string s in abM)
                {
                    IList<ActingBill> tempList = createBillList.Where(i => i.Item.Code == s).OrderByDescending(i => i.UnitPrice * i.CurrentBillQty).ToList();
                    IList<ActingBill> amtList = new List<ActingBill>();
                    decimal amount = 0;
                    foreach (ActingBill act in tempList)
                    {

                        if (amount + act.CurrentBillQty * act.UnitPrice <= 1000000)
                        {
                            amtList.Add(act);
                            amount += act.CurrentBillQty * act.UnitPrice;
                        }
                        else if (act.CurrentBillQty * act.UnitPrice > 1000000)
                        {
                            decimal qty = Math.Round(1000000 / act.UnitPrice, 0);
                            act.CurrentBillQty = qty;
                            amtList.Add(act);
                        }
                        else
                            continue;
                    }

                    if (amtList.Count > 0)
                    {
                        IList<Bill> billList = TheBillMgr.CreateBill(amtList, this.CurrentUser);
                        createdBill.Add(billList[0]);
                    }
                    foreach (ActingBill act in amtList)
                    {
                        if (Math.Round(1000000 / act.UnitPrice, 0) > act.CurrentBillQty)
                        {
                            createBillList.Remove(act);

                        }
                        else
                            act.CurrentBillQty = act.BillQty - act.BilledQty - Math.Round(1000000 / act.UnitPrice, 0) * cnt;
                    }

                }

                #endregion
                #region 未超过100w的
                List<string> normal = new List<string>();
                var bAm = (from i in createBillList
                           group i by new
                           {
                               i.Item.Code
                           } into k
                           select new
                           {
                               Item = k.Key.Code,
                               Amt = k.Sum(i => i.CurrentBillQty * i.UnitPrice)
                           }).Where(i => i.Amt < 1000000 && i.Amt != 0).OrderByDescending(i => i.Amt).ToList();
                while (bAm.Count > 0)
                {
                    decimal tempAmt = 0;
                    IList<string> tempGroup = new List<string>();
                    foreach (var i in bAm)
                    {
                        if (i.Amt + tempAmt <= 1000000)
                        {
                            tempGroup.Add(i.Item);
                            tempAmt += i.Amt;
                        }
                        else
                            continue;
                    }
                    List<ActingBill> tempAct = new List<ActingBill>();
                    foreach (string item in tempGroup)
                    {
                        List<ActingBill> _tempAct = (createBillList.Where(i => i.Item.Code == item)).ToList();
                        foreach (ActingBill bill in _tempAct)
                        {
                            tempAct.Add(bill);
                        }
                    }
                    for (int i = bAm.Count; i > 0; i--)
                    {
                        if (tempGroup.Contains(bAm[i - 1].Item))
                        {
                            bAm.Remove(bAm[i - 1]);
                            i = bAm.Count + 1;
                        }
                    }
                    if (tempAct.Count > 0)
                    {
                        IList<Bill> billList = TheBillMgr.CreateBill(tempAct, this.CurrentUser);
                        createdBill.Add(billList[0]);
                    }
                    foreach (ActingBill bill in tempAct)
                    {
                        createBillList.Remove(bill);
                    }
                }


                #endregion
                if (zeroM.Count > 0 && cnt == 1)
                {
                    foreach (string code in zeroM)
                    {
                        IList<ActingBill> tempList = createBillList.Where(i => i.Item.Code == code).OrderByDescending(i => i.UnitPrice * i.CurrentBillQty).ToList();
                        if (tempList.Count > 0)
                        {
                            if (createdBill.Count > 0)
                            {

                                TheBillMgr.AddBillDetail(createdBill[0], tempList, this.CurrentUser);

                            }
                            else
                            {
                                IList<Bill> billList = TheBillMgr.CreateBill(tempList, this.CurrentUser);
                                createdBill.Add(billList[0]);
                            }
                        }
                        foreach (ActingBill actb in tempList)
                        {
                            createBillList.Remove(actb);
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
        _createdBillNo += " 已创建以下账单：<br/>";
        foreach (Bill b in createdBill)
        {
            _createdBillNo += b.BillNo + "<br />";


        }


        #region output result xls
        if (createdBill != null && createdBill.Count > 0)
        {
            this.btnBack_Click(sender, e);

            ExportResult(createdBill);
        }
        #endregion


    }

    protected void ExportResult(IList<Bill> billList)
    {
        HSSFWorkbook excel = new HSSFWorkbook();
        NPOI.SS.UserModel.Sheet sheet = excel.CreateSheet("BILL");
        NPOI.SS.UserModel.Row row = sheet.CreateRow(0);
        row.CreateCell(0).SetCellValue("BILL NO");
        row.CreateCell(1).SetCellValue("ASN");
        row.CreateCell(2).SetCellValue("零件");
        row.CreateCell(3).SetCellValue("单价");
        row.CreateCell(4).SetCellValue("开票数");
        row.CreateCell(5).SetCellValue("金额");
        int rowNum = 1;
        foreach (Bill bill in billList)
        {
            foreach (BillDetail bd in bill.BillDetails)
            {
                NPOI.SS.UserModel.Row _row = sheet.CreateRow(rowNum);
                _row.CreateCell(0).SetCellValue(bd.Bill.BillNo);
                _row.CreateCell(1).SetCellValue(bd.IpNo);
                _row.CreateCell(2).SetCellValue(bd.ActingBill.Item.Code);
                _row.CreateCell(3).SetCellValue((double)bd.UnitPrice);
                _row.CreateCell(4).SetCellValue((double)bd.BilledQty);
                _row.CreateCell(5).SetCellValue((double)bd.Amount);
                rowNum++;
            }
        }
        MemoryStream ms = new MemoryStream();
        excel.Write(ms);
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename=BillResult.xls"));
        Response.BinaryWrite(ms.ToArray());

        excel = null;
        ms.Close();
        ms.Dispose();

    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        fs01.Visible = false;
    }
}
