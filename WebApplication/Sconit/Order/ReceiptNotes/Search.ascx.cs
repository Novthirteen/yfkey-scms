using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using NHibernate.Expression;
using com.Sconit.Entity.Distribution;
using com.Sconit.Utility;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;

public partial class Order_ReceiptNotes_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler ExportEvent;

    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }

    public string ModuleSubType
    {
        get { return (string)ViewState["ModuleSubType"]; }
        set { ViewState["ModuleSubType"] = value; }
    }

    public bool IsSupplier
    {
        get { return ViewState["IsSupplier"] != null ? (bool)ViewState["IsSupplier"] : false; }
        set { ViewState["IsSupplier"] = value; }
    }

    private IDictionary<string, string> dicParam;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsSupplier)
        {
            this.tbPartyFrom.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT + ",string:" + this.CurrentUser.Code;
            this.tbPartyTo.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT + ",string:" + this.CurrentUser.Code;
        }
        else
        {
            this.tbPartyFrom.ServiceParameter = "string:" + this.ModuleType + ",string:" + this.CurrentUser.Code;
            this.tbPartyTo.ServiceParameter = "string:" + this.ModuleType + ",string:" + this.CurrentUser.Code;
        }

        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillParameter();

        int rblListIndex = this.rblListFormat.SelectedIndex;
        if ((this.dicParam["OrderNo"] != string.Empty || this.dicParam["Item"] != string.Empty) && rblListIndex == 0)
        {
            rblListIndex = 1;
        }
        //if (this.dicParam["IpNo"] != string.Empty && rblListIndex == 1)
        //{
        //    rblListIndex = 0;
        //}
        this.rblListFormat.SelectedIndex = rblListIndex;

        Button btn = (Button)sender;
        if (SearchEvent != null)
        {
            if (btn == this.btnExport)
            {
                if (this.rblListFormat.SelectedValue == "Detail")
                {
                    object criteriaParam = this.CollectDetailParam(true);
                    SearchEvent(criteriaParam, null);
                }
                else
                {
                    object criteriaParam = this.CollectMasterParam(true);
                    SearchEvent(criteriaParam, null);
                }
            }
            else
            {
                DoSearch();
            }
        }
    }

    protected override void DoSearch()
    {
        FillParameter();
        if (this.rblListFormat.SelectedValue == "Detail")
        {
            object criteriaParam = this.CollectDetailParam(false);
            SearchEvent(criteriaParam, null);
        }
        else
        {
            object criteriaParam = this.CollectMasterParam(false);
            SearchEvent(criteriaParam, null);
        }
    }

    private object CollectMasterParam(bool isExport)
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(Receipt));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(Receipt))
            .SetProjection(Projections.Count("ReceiptNo"));
        selectCriteria.CreateAlias("PartyFrom", "pf");
        selectCriteria.CreateAlias("PartyTo", "pt");
        selectCountCriteria.CreateAlias("PartyFrom", "pf");
        selectCountCriteria.CreateAlias("PartyTo", "pt");

        #region ModuleType
        if (IsSupplier)
        {
            selectCriteria.Add(Expression.In("OrderType", new object[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT }));
            selectCountCriteria.Add(Expression.In("OrderType", new object[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT }));
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            selectCriteria.Add(Expression.In("OrderType", new object[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT, 
                    BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER }));
            selectCountCriteria.Add(Expression.In("OrderType", new object[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT, 
                    BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER }));
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        {
            selectCriteria.Add(Expression.Eq("OrderType", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION));
            selectCountCriteria.Add(Expression.Eq("OrderType", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION));
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            selectCriteria.Add(Expression.In("OrderType", new object[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION, 
                    BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER }));
            selectCountCriteria.Add(Expression.In("OrderType", new object[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION, 
                    BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER }));
        }
        else
        {
            throw new TechnicalException("invalided module type:" + this.ModuleType);
        }
        #endregion

        if (this.dicParam["ReceiptNo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Like("ReceiptNo", this.dicParam["ReceiptNo"], MatchMode.Start));
            selectCountCriteria.Add(Expression.Like("ReceiptNo", this.dicParam["ReceiptNo"], MatchMode.Start));
        }

        if (this.dicParam["StartDate"] != string.Empty)
        {
            selectCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(this.dicParam["StartDate"])));
            selectCountCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(this.dicParam["StartDate"])));
        }
        if (this.dicParam["EndDate"] != string.Empty)
        {
            selectCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(this.dicParam["EndDate"]).AddDays(1)));
            selectCountCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(this.dicParam["EndDate"]).AddDays(1)));
        }

        if (this.dicParam["IpNo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("ReferenceIpNo", this.dicParam["IpNo"]));
            selectCountCriteria.Add(Expression.Eq("ReferenceIpNo", this.dicParam["IpNo"]));
        }

        if (this.dicParam["PartyFrom"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("pf.Code", this.dicParam["PartyFrom"]));
            selectCountCriteria.Add(Expression.Eq("pf.Code", this.dicParam["PartyFrom"]));
        }
        else if (this.ModuleType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            #region partyFrom
            SecurityHelper.SetPartyFromSearchCriteria(
                selectCriteria, selectCountCriteria, this.dicParam["PartyFrom"], this.ModuleType, this.CurrentUser.Code);
            #endregion
        }

        if (this.dicParam["PartyTo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("pt.Code", this.dicParam["PartyTo"]));
            selectCountCriteria.Add(Expression.Eq("pt.Code", this.dicParam["PartyTo"]));
        }
        else if (this.ModuleType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            #region partyTo
            SecurityHelper.SetPartyToSearchCriteria(
                selectCriteria, selectCountCriteria, this.dicParam["PartyTo"], this.ModuleType, this.CurrentUser.Code);
            #endregion
        }

        return (new object[] { selectCriteria, selectCountCriteria, isExport, true });
    }

    private object CollectDetailParam(bool isExport)
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(ReceiptDetail));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(ReceiptDetail))
            .SetProjection(Projections.Count("Id"));
        selectCriteria.CreateAlias("Receipt", "r");
        selectCountCriteria.CreateAlias("Receipt", "r");

        selectCriteria.CreateAlias("r.PartyFrom", "pf");
        selectCountCriteria.CreateAlias("r.PartyFrom", "pf");
        selectCriteria.CreateAlias("r.PartyTo", "pt");
        selectCountCriteria.CreateAlias("r.PartyTo", "pt");

        selectCriteria.CreateAlias("OrderLocationTransaction", "olt");
        selectCountCriteria.CreateAlias("OrderLocationTransaction", "olt");
        selectCriteria.CreateAlias("olt.OrderDetail", "od");
        selectCountCriteria.CreateAlias("olt.OrderDetail", "od");
        selectCriteria.CreateAlias("od.OrderHead", "o");
        selectCountCriteria.CreateAlias("od.OrderHead", "o");

        #region ModuleType
        if (IsSupplier)
        {
            selectCriteria.Add(Expression.In("r.OrderType", new object[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT }));
            selectCountCriteria.Add(Expression.In("r.OrderType", new object[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT }));
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            selectCriteria.Add(Expression.In("r.OrderType", new object[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT, 
                    BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER }));
            selectCountCriteria.Add(Expression.In("r.OrderType", new object[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT, 
                    BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER }));
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION)
        {
            selectCriteria.Add(Expression.Eq("r.OrderType", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION));
            selectCountCriteria.Add(Expression.Eq("r.OrderType", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION));
        }
        else if (this.ModuleType == BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            selectCriteria.Add(Expression.In("r.OrderType", new object[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION, 
                    BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER }));
            selectCountCriteria.Add(Expression.In("r.OrderType", new object[] { BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION, 
                    BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_TRANSFER }));
        }
        else
        {
            throw new TechnicalException("invalided module type:" + this.ModuleType);
        }
        #endregion

        if (this.dicParam["ReceiptNo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Like("r.ReceiptNo", this.dicParam["ReceiptNo"], MatchMode.Start));
            selectCountCriteria.Add(Expression.Like("r.ReceiptNo", this.dicParam["ReceiptNo"], MatchMode.Start));
        }

        if (this.dicParam["StartDate"] != string.Empty)
        {
            selectCriteria.Add(Expression.Ge("r.CreateDate", DateTime.Parse(this.dicParam["StartDate"])));
            selectCountCriteria.Add(Expression.Ge("r.CreateDate", DateTime.Parse(this.dicParam["StartDate"])));
        }
        if (this.dicParam["EndDate"] != string.Empty)
        {
            selectCriteria.Add(Expression.Lt("r.CreateDate", DateTime.Parse(this.dicParam["EndDate"]).AddDays(1)));
            selectCountCriteria.Add(Expression.Lt("r.CreateDate", DateTime.Parse(this.dicParam["EndDate"]).AddDays(1)));
        }

        if (this.dicParam["IpNo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("r.ReferenceIpNo", this.dicParam["IpNo"]));
            selectCountCriteria.Add(Expression.Eq("r.ReferenceIpNo", this.dicParam["IpNo"]));
        }

        if (this.dicParam["PartyFrom"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("pf.Code", this.dicParam["PartyFrom"]));
            selectCountCriteria.Add(Expression.Eq("pf.Code", this.dicParam["PartyFrom"]));
        }
        else if (this.ModuleType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            #region partyFrom
            SecurityHelper.SetPartyFromSearchCriteria(
                selectCriteria, selectCountCriteria, this.dicParam["PartyFrom"], this.ModuleType, this.CurrentUser.Code);
            #endregion
        }

        if (this.dicParam["PartyTo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("pt.Code", this.dicParam["PartyTo"]));
            selectCountCriteria.Add(Expression.Eq("pt.Code", this.dicParam["PartyTo"]));
        }
        else if (this.ModuleType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            #region partyTo
            SecurityHelper.SetPartyToSearchCriteria(
                selectCriteria, selectCountCriteria, this.dicParam["PartyTo"], this.ModuleType, this.CurrentUser.Code);
            #endregion
        }

        #region item order
        if (this.dicParam["Item"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("olt.Item.Code", this.dicParam["Item"]));
            selectCountCriteria.Add(Expression.Eq("olt.Item.Code", this.dicParam["Item"]));
        }
        if (this.dicParam["OrderNo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("o.OrderNo", this.dicParam["OrderNo"]));
            selectCountCriteria.Add(Expression.Eq("o.OrderNo", this.dicParam["OrderNo"]));
        }
        #endregion

        return (new object[] { selectCriteria, selectCountCriteria, isExport, false });
    }

    private void FillParameter()
    {
        this.dicParam = new Dictionary<string, string>();
        this.dicParam["ReceiptNo"] = this.tbReceiptNo.Text.Trim();
        this.dicParam["IpNo"] = this.tbIpNo.Text.Trim();
        this.dicParam["PartyFrom"] = this.tbPartyFrom.Text.Trim();
        this.dicParam["PartyTo"] = this.tbPartyTo.Text.Trim();
        this.dicParam["OrderNo"] = this.tbOrderNo.Text.Trim();
        this.dicParam["Item"] = this.tbItem.Text.Trim();
        this.dicParam["StartDate"] = this.tbStartDate.Text.Trim();
        this.dicParam["EndDate"] = this.tbEndDate.Text.Trim();
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        //todo
    }
}
