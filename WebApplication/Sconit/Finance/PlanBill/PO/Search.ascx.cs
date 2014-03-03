using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using NHibernate.Transform;
using com.Sconit.Entity;
using com.Sconit.Utility;

public partial class Finance_PlanBill_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler ConfirmEvent;
    public event EventHandler ExportEvent;

    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }

    public bool IsSupplier
    {
        get { return ViewState["IsSupplier"] != null ? (bool)ViewState["IsSupplier"] : false; }
        set { ViewState["IsSupplier"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbPartyCode.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT + ",string:" + this.CurrentUser.Code;
        if (!IsPostBack)
        {
            this.PageCleanUp();
        }
        this.btnConfirm.Visible = !IsSupplier;
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
        if (actionParameter.ContainsKey("ReceiptNo"))
        {
            this.tbReceiptNo.Text = actionParameter["ReceiptNo"];
        }
        if (actionParameter.ContainsKey("ItemCode"))
        {
            this.tbItemCode.Text = actionParameter["ItemCode"];
        }
        if (actionParameter.ContainsKey("OrderNo"))
        {
            this.tbOrderNo.Text = actionParameter["OrderNo"];
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (SearchEvent != null)
        {
            object criteriaParam = CollectParam();
            ExportEvent(criteriaParam, null);
        }
    }

    protected override void DoSearch()
    {
        if (SearchEvent != null)
        {
            object selectCriteria =  CollectParam();
            SearchEvent(selectCriteria, null);
        }
    }

    private object CollectParam()
    {
        string partyCode = this.tbPartyCode.Text != string.Empty ? this.tbPartyCode.Text.Trim() : string.Empty;
        string ReceiptNo = this.tbReceiptNo.Text != string.Empty ? this.tbReceiptNo.Text.Trim() : string.Empty;
        string startDate = this.tbStartDate.Text != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
        string endDate = this.tbEndDate.Text != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;
        string itemCode = this.tbItemCode.Text != string.Empty ? this.tbItemCode.Text.Trim() : string.Empty;
        string orderNo = this.tbOrderNo.Text != string.Empty ? this.tbOrderNo.Text.Trim() : string.Empty;
        string ipNo = this.tbIpNo.Text.Trim();

        #region DetachedCriteria查询

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(PlannedBill));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(PlannedBill));
        selectCriteria.Add(Expression.Eq("TransactionType", this.ModuleType));
        selectCriteria.Add(Expression.Or(Expression.IsNull("ActingQty"), Expression.GtProperty("PlannedQty", "ActingQty")));
        selectCriteria.CreateAlias("BillAddress", "ba");
        selectCriteria.CreateAlias("ba.Party", "pf");

        SecurityHelper.SetPartyFromSearchCriteria(selectCriteria, selectCountCriteria, partyCode, BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT, this.CurrentUser.Code);
        if (partyCode != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("ba.Party.Code", partyCode));
        }
        if (ReceiptNo != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("ReceiptNo", ReceiptNo));
        }
        if (startDate != string.Empty)
        {
            selectCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
        }
        if (endDate != string.Empty)
        {
            selectCriteria.Add(Expression.Le("CreateDate", DateTime.Parse(endDate).AddDays(1)));
        }
        if (itemCode != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("Item.Code", itemCode));
        }
        if (orderNo != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("OrderNo", orderNo));
        }
        if (ipNo != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("IpNo", ipNo));
        }

        return selectCriteria;
        #endregion
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (ConfirmEvent != null)
        {
            ConfirmEvent(sender, e);
        }
    }

    private void PageCleanUp()
    {
        this.tbStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
        this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        this.tbPartyCode.Text = string.Empty;
        this.tbReceiptNo.Text = string.Empty;
        this.tbItemCode.Text = string.Empty;
    }
}
