using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;

public partial class Finance_Bill_NewBatchSearch : ModuleBase
{
    public event EventHandler SearchEvent;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageCleanUp();
           
        }
        if (this.ModuleType == BusinessConstants.BILL_TRANS_TYPE_SO)
        {
            this.ltlPartyCode.Text = "${MasterData.ActingBill.Customer}:";
            this.ltlReceiver.Text = "${MasterData.ActingBill.ExternalReceiptNo}:";

            this.tbPartyCode.ServicePath = "CustomerMgr.service";
            this.tbPartyCode.ServiceMethod = "GetAllCustomer";
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        string partyCode = this.tbPartyCode.Text != string.Empty ? this.tbPartyCode.Text.Trim() : string.Empty;
        string receiver = this.tbReceiver.Text != string.Empty ? this.tbReceiver.Text.Trim() : string.Empty;
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

        //重新计价
        bool needRecalculate = bool.Parse(TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_RECALCULATE_WHEN_BILL).Value);
        if (needRecalculate)
        {
            IList<ActingBill> allactingBillList = TheActingBillMgr.GetActingBill(partyCode, receiver, effDateFrom, effDateTo, itemCode, currency, this.ModuleType,null, true);

            TheActingBillMgr.RecalculatePrice(allactingBillList, this.CurrentUser);
        }

        IList<ActingBill> actingBillList = TheActingBillMgr.GetActingBill(partyCode, receiver, effDateFrom, effDateTo, itemCode, currency, this.ModuleType, null);
        
       

        if (actingBillList != null && actingBillList.Count > 0)
        {

            foreach (ActingBill actingBill in actingBillList)
            {
                actingBill.CurrentBillQty = actingBill.BillQty - actingBill.BilledQty;
                decimal orgAmount = actingBill.UnitPrice * actingBill.CurrentBillQty;
                actingBill.CurrentDiscount = orgAmount - (actingBill.BillAmount - actingBill.BilledAmount);
            }

            IList<Bill> billList = this.TheBillMgr.CreateBill(actingBillList, this.CurrentUser, (this.IsRelease.Checked ? BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT : BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
            ;

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(Bill));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(Bill))
                .SetProjection(Projections.Count("BillNo"));

            selectCriteria.Add(Expression.Eq("CreateDate", billList[0].CreateDate));
            selectCriteria.Add(Expression.Eq("CreateUser.Code", this.CurrentUser.Code));
            selectCountCriteria.Add(Expression.Eq("CreateDate", billList[0].CreateDate));
            selectCountCriteria.Add(Expression.Eq("CreateUser.Code", this.CurrentUser.Code));

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);

            this.ShowSuccessMessage("MasterData.Bill.BatchCreateSuccessfully");
        }
        else
        {
            this.ShowErrorMessage("Bill.Error.EmptyBillDetail");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        BackEvent(this, null);
        this.PageCleanUp();
    }

    private void PageCleanUp()
    {
        this.tbStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
        this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        this.tbPartyCode.Text = string.Empty;
        this.tbReceiver.Text = string.Empty;
        this.tbItemCode.Text = string.Empty;
        this.tbCurrency.Text = string.Empty;
    }
}
