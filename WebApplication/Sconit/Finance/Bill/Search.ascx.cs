using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;

public partial class Finance_Bill_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler NewEvent;

    public bool IsSupplier
    {
        get { return ViewState["IsSupplier"] != null ? (bool)ViewState["IsSupplier"] : false; }
        set { ViewState["IsSupplier"] = value; }
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ddlStatus.DataSource = this.GetAllStatus();
            this.ddlStatus.DataBind();

            this.tbStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        if (ModuleType == BusinessConstants.BILL_TRANS_TYPE_SO)
        {
            this.ltlPartyCode.Text = "${MasterData.Bill.Customer}:";
            this.tbPartyCode.ServicePath = "CustomerMgr.service";
            this.tbPartyCode.ServiceMethod = "GetAllCustomer";
        }

        if (this.IsSupplier) {
            this.tbPartyCode.ServicePath = "PartyMgr.service";
            this.tbPartyCode.ServiceMethod = "GetFromParty";
            this.tbPartyCode.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT + ",string:" + this.CurrentUser.Code;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected override void DoSearch()
    {
        string code = this.tbCode.Text != string.Empty ? this.tbCode.Text.Trim() : string.Empty;
        string status = this.ddlStatus.SelectedValue;
        string partyCode = this.tbPartyCode.Text != string.Empty ? this.tbPartyCode.Text.Trim() : string.Empty;
        string externalBillNo = this.tbExternalBillNo.Text != string.Empty ? this.tbExternalBillNo.Text.Trim() : string.Empty;
        string startDate = this.tbStartDate.Text != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
        string endDate = this.tbEndDate.Text != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;

        if (SearchEvent != null)
        {
            #region DetachedCriteria

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(Bill));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(Bill))
                .SetProjection(Projections.Count("BillNo"));

            selectCriteria.CreateAlias("BillAddress", "ba");
            selectCountCriteria.CreateAlias("BillAddress", "ba");
            selectCriteria.CreateAlias("ba.Party", "pf");
            selectCountCriteria.CreateAlias("ba.Party", "pf");

            if (code != string.Empty)
            {
                selectCriteria.Add(Expression.Like("BillNo", code, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("BillNo", code, MatchMode.Start));
            }
            if (status != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Status", status));
                selectCountCriteria.Add(Expression.Eq("Status", status));
            }
            else if (this.IsSupplier) 
            {
                selectCriteria.Add(Expression.Not(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)));
                selectCountCriteria.Add(Expression.Not(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)));
            }
            if (partyCode != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("pf.Code", partyCode));
                selectCountCriteria.Add(Expression.Eq("pf.Code", partyCode));
            }
            else if (this.IsSupplier)
            {
                SecurityHelper.SetPartyFromSearchCriteria(selectCriteria, selectCountCriteria, (this.tbPartyCode != null ? this.tbPartyCode.Text : null), BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT, this.CurrentUser.Code);
            }
            if (externalBillNo != string.Empty)
            {
                selectCriteria.Add(Expression.Like("ExternalBillNo", externalBillNo, MatchMode.Start));
                selectCountCriteria.Add(Expression.Like("ExternalBillNo", externalBillNo, MatchMode.Start));
            }
            if (startDate != string.Empty)
            {
                selectCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
                selectCountCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
            }
            if (endDate != string.Empty)
            {
                selectCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(endDate).AddDays(1).AddMilliseconds(-1)));
                selectCountCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(endDate).AddDays(1).AddMilliseconds(-1)));
            }

            selectCriteria.Add(Expression.Eq("TransactionType", ModuleType));
            selectCountCriteria.Add(Expression.Eq("TransactionType", ModuleType));

            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion
        }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        NewEvent(sender, e);
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        if (actionParameter.ContainsKey("Code"))
        {
            this.tbCode.Text = actionParameter["Code"];
        }
    }

    public IList<CodeMaster> GetAllStatus()
    {
        IList<CodeMaster> statusGroup = new List<CodeMaster>();

        statusGroup.Add(new CodeMaster());//空行
        if (!this.IsSupplier)
        {
            statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
        }
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT));
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL));
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE));
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_VOID));

        return statusGroup;
    }
    private CodeMaster GetStatus(string statusValue)
    {
        return TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_STATUS, statusValue);
    }

}
