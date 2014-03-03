using System;
using System.Collections.Generic;
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

public partial class Finance_Bill_NewSearch : SearchModuleBase
{
    public event EventHandler BackEvent;
    public event EventHandler CreateEvent;

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
      


       IList<ActingBill> actingBillList = TheActingBillMgr.GetActingBill(partyCode, receiver, effDateFrom, effDateTo, itemCode, currency, this.ModuleType, this.billNo);

      
     

        this.ucNewList.BindDataSource(actingBillList != null && actingBillList.Count > 0 ? actingBillList : null);
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

    protected void btnAddDetail_Click(object sender, EventArgs e)
    {
        try
        {
            IList<ActingBill> actingBillList = this.ucNewList.PopulateSelectedData();
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
}
