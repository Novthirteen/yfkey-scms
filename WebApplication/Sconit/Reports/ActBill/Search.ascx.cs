using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity;
using com.Sconit.Web;
using NHibernate.Expression;
using com.Sconit.Entity.View;
using com.Sconit.Utility;

public partial class Reports_ActBill_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler ExportEvent;

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
        if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            this.lblParty.Text = "${Reports.ActBill.PartyFrom}:";
            this.tbParty.ServicePath = "SupplierMgr.service";
            this.tbParty.ServiceMethod = "GetSupplier";
            this.tbParty.ServiceParameter = "string:" + this.CurrentUser.Code;
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            this.lblParty.Text = "${Reports.ActBill.PartyTo}:";
            this.tbParty.ServicePath = "CustomerMgr.service";
            this.tbParty.ServiceMethod = "GetCustomer";
            this.tbParty.ServiceParameter = "string:" + this.CurrentUser.Code;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ExportEvent != null)
        {
            object[] param = this.CollectParam();
            if (param != null)
                ExportEvent(param, null);
        }
    }

    protected override void DoSearch()
    {

        if (SearchEvent != null)
        {
            object[] param = CollectParam();
            if (param != null)
                SearchEvent(param, null);
        }
    }

    private object[] CollectParam()
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(ActingBillView));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(ActingBillView))
            .SetProjection(Projections.Count("Id"));

        IDictionary<string, string> alias = new Dictionary<string, string>();
        selectCriteria.CreateAlias("BillAddress", "ba");
        selectCountCriteria.CreateAlias("BillAddress", "ba");

        alias.Add("BillAddress", "ba");

        if (this.tbOrderNo.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("OrderNo", this.tbOrderNo.Text.Trim()));
            selectCountCriteria.Add(Expression.Eq("OrderNo", this.tbOrderNo.Text.Trim()));
        }

        if (this.tbReceiptNo.Text.Trim() != string.Empty)
        {
            if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
            {
                selectCriteria.Add(Expression.Eq("ReceiptNo", this.tbReceiptNo.Text.Trim()));
                selectCountCriteria.Add(Expression.Eq("ReceiptNo", this.tbReceiptNo.Text.Trim()));
            }
            else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
            {
                selectCriteria.Add(Expression.Eq("ExternalReceiptNo", this.tbReceiptNo.Text.Trim()));
                selectCountCriteria.Add(Expression.Eq("ExternalReceiptNo", this.tbReceiptNo.Text.Trim()));
            }
        }

        #region party设置
        if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            selectCriteria.CreateAlias("ba.Party", "pf");
            selectCountCriteria.CreateAlias("ba.Party", "pf");

            alias.Add("BillAddress.Party", "pf");

            if (this.tbParty.Text.Trim() != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("pf.Code", this.tbParty.Text.Trim()));
                selectCountCriteria.Add(Expression.Eq("pf.Code", this.tbParty.Text.Trim()));
            }
            else
            {
                SecurityHelper.SetPartyFromSearchCriteria(
                    selectCriteria, selectCountCriteria, (this.tbParty != null ? this.tbParty.Text : null), this.ModuleType, this.CurrentUser.Code);
            }
            selectCriteria.Add(Expression.Eq("TransactionType", BusinessConstants.BILL_TRANS_TYPE_PO));
            selectCountCriteria.Add(Expression.Eq("TransactionType", BusinessConstants.BILL_TRANS_TYPE_PO));
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            selectCriteria.CreateAlias("ba.Party", "pt");
            selectCountCriteria.CreateAlias("ba.Party", "pt");

            alias.Add("BillAddress.Party", "pt");

            if (this.tbParty.Text.Trim() != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("pt.Code", this.tbParty.Text.Trim()));
                selectCountCriteria.Add(Expression.Eq("pt.Code", this.tbParty.Text.Trim()));
            }
            else
            {
                SecurityHelper.SetPartyToSearchCriteria(
                selectCriteria, selectCountCriteria, (this.tbParty != null ? this.tbParty.Text : null), this.ModuleType, this.CurrentUser.Code);
            }

            selectCriteria.Add(Expression.Eq("TransactionType", BusinessConstants.BILL_TRANS_TYPE_SO));
            selectCountCriteria.Add(Expression.Eq("TransactionType", BusinessConstants.BILL_TRANS_TYPE_SO));
        }
        #endregion

        if (this.tbItem.Text.Trim() != string.Empty)
        {
            selectCriteria.CreateAlias("Item", "it");
            selectCountCriteria.CreateAlias("Item", "it");

            alias.Add("Item", "it");

            selectCriteria.Add(Expression.Eq("it.Code", this.tbItem.Text.Trim()));
            selectCountCriteria.Add(Expression.Eq("it.Code", this.tbItem.Text.Trim()));
        }

        if (this.tbEffectiveDateFrom.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Ge("EffectiveDate", DateTime.Parse(this.tbEffectiveDateFrom.Text.Trim())));
            selectCountCriteria.Add(Expression.Ge("EffectiveDate", DateTime.Parse(this.tbEffectiveDateFrom.Text.Trim())));
        }

        if (this.tbEffectiveDateTo.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Le("EffectiveDate", DateTime.Parse(this.tbEffectiveDateTo.Text.Trim()).AddDays(1).AddMilliseconds(-1)));
            selectCountCriteria.Add(Expression.Le("EffectiveDate", DateTime.Parse(this.tbEffectiveDateTo.Text.Trim()).AddDays(1).AddMilliseconds(-1)));
        }

        return new object[] { selectCriteria, selectCountCriteria, alias };

    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        //if (actionParameter.ContainsKey("Location"))
        //{
        //    this.tbLocation.Text = actionParameter["Location"];
        //}
        //if (actionParameter.ContainsKey("Item"))
        //{
        //    this.tbItem.Text = actionParameter["Item"];
        //}
        //if (actionParameter.ContainsKey("EffDate"))
        //{
        //    this.tbEffDate.Text = actionParameter["EffDate"];
        //}
    }
}