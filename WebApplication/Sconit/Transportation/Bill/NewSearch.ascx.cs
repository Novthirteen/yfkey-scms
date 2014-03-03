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
