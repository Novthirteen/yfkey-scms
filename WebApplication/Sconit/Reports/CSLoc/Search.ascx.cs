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

public partial class Reports_CSLoc_Search : SearchModuleBase
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
        this.lblFlow.Text = FlowHelper.GetFlowLabel(this.ModuleType) + ":";
        if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            this.lblParty.Text = "${Reports.CSLoc.PartyFrom}:";
            this.tbParty.ServicePath = "SupplierMgr.service";
            this.tbParty.ServiceMethod = "GetSupplier";
            this.tbParty.ServiceParameter = "string:" + this.CurrentUser.Code;

            this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:true,bool:false,bool:false,bool:false,bool:true,bool:true,string:"+BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            this.lblParty.Text = "${Reports.CSLoc.PartyTo}:";
            this.tbParty.ServicePath = "CustomerMgr.service";
            this.tbParty.ServiceMethod = "GetCustomer";
            this.tbParty.ServiceParameter = "string:" + this.CurrentUser.Code;

            this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:true,bool:false,bool:false,bool:false,bool:false,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_FROM;
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
            if (param!=null)
                SearchEvent(param, null);
        }
    }


    private object[] CollectParam()
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(PlannedBillView));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(PlannedBillView))
            .SetProjection(Projections.Count("Id"));

        IDictionary<string, string> alias = new Dictionary<string, string>();
        selectCriteria.CreateAlias("BillAddress", "ba");
        selectCountCriteria.CreateAlias("BillAddress", "ba");

        alias.Add("BillAddress", "ba");

        if (this.tbFlow.Text != string.Empty)
        {
            selectCriteria.CreateAlias("Flow", "f");
            selectCountCriteria.CreateAlias("Flow", "f");

            selectCriteria.Add(Expression.Eq("f.Code", this.tbFlow.Text.Trim()));
            selectCountCriteria.Add(Expression.Eq("f.Code", this.tbFlow.Text.Trim()));

            alias.Add("Flow", "f");
        }

        if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            selectCriteria.CreateAlias("ba.Party", "pf");
            selectCountCriteria.CreateAlias("ba.Party", "pf");

            alias.Add("BillAddress.Party", "pf");

            SecurityHelper.SetPartyFromSearchCriteria(
            selectCriteria, selectCountCriteria, (this.tbParty != null ? this.tbParty.Text : null), this.ModuleType, this.CurrentUser.Code);

            selectCriteria.Add(Expression.Eq("TransactionType", BusinessConstants.BILL_TRANS_TYPE_PO));
            selectCountCriteria.Add(Expression.Eq("TransactionType", BusinessConstants.BILL_TRANS_TYPE_PO));
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            selectCriteria.CreateAlias("ba.Party", "pt");
            selectCountCriteria.CreateAlias("ba.Party", "pt");

            alias.Add("BillAddress.Party", "pt");

            SecurityHelper.SetPartyToSearchCriteria(
            selectCriteria, selectCountCriteria, (this.tbParty != null ? this.tbParty.Text : null), this.ModuleType, this.CurrentUser.Code);

            selectCriteria.Add(Expression.Eq("TransactionType", BusinessConstants.BILL_TRANS_TYPE_SO));
            selectCountCriteria.Add(Expression.Eq("TransactionType", BusinessConstants.BILL_TRANS_TYPE_SO));
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
