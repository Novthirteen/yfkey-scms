using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;
using com.Sconit.Utility;
using com.Sconit.Web;
using NHibernate.Expression;
using System.Web.UI.WebControls;
using com.Sconit.Entity.View;

public partial class Order_GoodsReceipt_AsnReceipt_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;

    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }

    public string AsnType
    {
        get { return (string)ViewState["AsnType"]; }
        set { ViewState["AsnType"] = value; }
    }

    public string Action
    {
        get { return (string)ViewState["Action"]; }
        set { ViewState["Action"] = value; }
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
            this.tbStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            if (this.Action == "View")
            {
                IList<CodeMaster> statusList = GetStatusGroup();
                statusList.Insert(0, new CodeMaster()); //添加空选项
                this.ddlStatus.DataSource = statusList;
                this.ddlStatus.DataBind();
            }
            else if (this.Action == "Receive")
            {
                this.lblStatus.Visible = false;
                this.ddlStatus.Visible = false;
                this.trDetails.Visible = false;
                this.ltlListFormat.Visible = false;
                this.rblListFormat.Visible = false;
            }
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

    private object CollectDetailParam(bool isExport)
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(InProcessLocationDetailView));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(InProcessLocationDetailView))
            .SetProjection(Projections.Count("Id"));
        selectCriteria.CreateAlias("InProcessLocation", "ip");
        selectCountCriteria.CreateAlias("InProcessLocation", "ip");
        selectCriteria.CreateAlias("ip.PartyFrom", "pf");
        selectCountCriteria.CreateAlias("ip.PartyFrom", "pf");
        selectCriteria.CreateAlias("ip.PartyTo", "pt");
        selectCountCriteria.CreateAlias("ip.PartyTo", "pt");
        selectCriteria.CreateAlias("OrderDetail", "od");
        selectCountCriteria.CreateAlias("OrderDetail", "od");
        selectCriteria.CreateAlias("od.OrderHead", "o");
        selectCountCriteria.CreateAlias("od.OrderHead", "o");


        if (this.dicParam["IpNo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Like("ip.IpNo", this.dicParam["IpNo"], MatchMode.Start));
            selectCountCriteria.Add(Expression.Like("ip.IpNo", this.dicParam["IpNo"], MatchMode.Start));
        }
        #region date
        if (this.dicParam["StartDate"] != string.Empty)
        {
            selectCriteria.Add(Expression.Ge("ip.CreateDate", DateTime.Parse(this.dicParam["StartDate"])));
            selectCountCriteria.Add(Expression.Ge("ip.CreateDate", DateTime.Parse(this.dicParam["StartDate"])));
        }
        if (this.dicParam["EndDate"] != string.Empty)
        {
            selectCriteria.Add(Expression.Lt("ip.CreateDate", DateTime.Parse(this.dicParam["EndDate"]).AddDays(1)));
            selectCountCriteria.Add(Expression.Lt("ip.CreateDate", DateTime.Parse(this.dicParam["EndDate"]).AddDays(1)));
        }
        #endregion

        #region partyFrom
        if (this.dicParam["PartyFrom"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("pf.Code", this.dicParam["PartyFrom"]));
            selectCountCriteria.Add(Expression.Eq("pf.Code", this.dicParam["PartyFrom"]));
        }
        else if (this.ModuleType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            SecurityHelper.SetPartyFromSearchCriteria(
                selectCriteria, selectCountCriteria, this.dicParam["PartyFrom"], this.ModuleType, this.CurrentUser.Code);
        }
        #endregion

        #region partyTo
        if (this.dicParam["PartyTo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("pt.Code", this.dicParam["PartyTo"]));
            selectCountCriteria.Add(Expression.Eq("pt.Code", this.dicParam["PartyTo"]));
        }
        else if (this.ModuleType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            SecurityHelper.SetPartyToSearchCriteria(
                selectCriteria, selectCountCriteria, this.dicParam["PartyTo"], this.ModuleType, this.CurrentUser.Code);
        }
        #endregion

        #region AsnType
        if (this.dicParam["AsnType"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("ip.Type", this.dicParam["AsnType"]));
            selectCountCriteria.Add(Expression.Eq("ip.Type", this.dicParam["AsnType"]));
        }
        else
        {
            selectCriteria.Add(Expression.Eq("ip.Type", BusinessConstants.CODE_MASTER_INPROCESS_LOCATION_TYPE_VALUE_NORMAL));
            selectCountCriteria.Add(Expression.Eq("ip.Type", BusinessConstants.CODE_MASTER_INPROCESS_LOCATION_TYPE_VALUE_NORMAL));
        }
        #endregion

        #region item order
        if (this.dicParam["Item"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("Item.Code", this.dicParam["Item"]));
            selectCountCriteria.Add(Expression.Eq("Item.Code", this.dicParam["Item"]));
        }
        if (this.dicParam["OrderNo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("o.OrderNo", this.dicParam["OrderNo"]));
            selectCountCriteria.Add(Expression.Eq("o.OrderNo", this.dicParam["OrderNo"]));
        }
        #endregion

        if (this.dicParam["Status"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("ip.Status", this.dicParam["Status"]));
            selectCountCriteria.Add(Expression.Eq("ip.Status", this.dicParam["Status"]));
        }

        return new object[] { selectCriteria, selectCountCriteria, isExport, false };
    }

    private object CollectMasterParam(bool isExport)
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(InProcessLocation));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(InProcessLocation))
            .SetProjection(Projections.Count("IpNo"));
        selectCriteria.CreateAlias("PartyFrom", "pf");
        selectCriteria.CreateAlias("PartyTo", "pt");
        selectCountCriteria.CreateAlias("PartyFrom", "pf");
        selectCountCriteria.CreateAlias("PartyTo", "pt");

        if (this.dicParam["IpNo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Like("IpNo", this.dicParam["IpNo"], MatchMode.Start));
            selectCountCriteria.Add(Expression.Like("IpNo", this.dicParam["IpNo"], MatchMode.Start));
        }
        #region date
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
        #endregion

        #region partyFrom
        if (this.dicParam["PartyFrom"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("pf.Code", this.dicParam["PartyFrom"]));
            selectCountCriteria.Add(Expression.Eq("pf.Code", this.dicParam["PartyFrom"]));
        }
        else if (this.ModuleType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PROCUREMENT)
        {
            SecurityHelper.SetPartyFromSearchCriteria(
                selectCriteria, selectCountCriteria, this.dicParam["PartyFrom"], this.ModuleType, this.CurrentUser.Code);
        }
        #endregion

        #region partyTo
        if (this.dicParam["PartyTo"] != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("pt.Code", this.dicParam["PartyTo"]));
            selectCountCriteria.Add(Expression.Eq("pt.Code", this.dicParam["PartyTo"]));
        }
        else if (this.ModuleType != BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_DISTRIBUTION)
        {
            SecurityHelper.SetPartyToSearchCriteria(
                selectCriteria, selectCountCriteria, this.dicParam["PartyTo"], this.ModuleType, this.CurrentUser.Code);
        }
        #endregion

        if (this.Action == "View")
        {
            if (this.dicParam["AsnType"] != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Type", this.dicParam["AsnType"]));
                selectCountCriteria.Add(Expression.Eq("Type", this.dicParam["AsnType"]));
            }
            if (this.dicParam["Status"] != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Status", this.dicParam["Status"]));
                selectCountCriteria.Add(Expression.Eq("Status", this.dicParam["Status"]));
            }
        }
        else if (this.Action == "Receive")
        {
            selectCriteria.Add(Expression.Eq("Type", BusinessConstants.CODE_MASTER_INPROCESS_LOCATION_TYPE_VALUE_NORMAL));
            selectCountCriteria.Add(Expression.Eq("Type", BusinessConstants.CODE_MASTER_INPROCESS_LOCATION_TYPE_VALUE_NORMAL));

            selectCriteria.Add(Expression.Not(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE)));
            selectCountCriteria.Add(Expression.Not(Expression.Eq("Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE)));
        }

        return new object[] { selectCriteria, selectCountCriteria, isExport, true };
    }

    private void FillParameter()
    {
        this.dicParam = new Dictionary<string, string>();
        this.dicParam["IpNo"] = this.tbIpNo.Text.Trim();
        this.dicParam["PartyFrom"] = this.tbPartyFrom.Text.Trim();
        this.dicParam["PartyTo"] = this.tbPartyTo.Text.Trim();
        this.dicParam["AsnType"] = this.AsnType;
        this.dicParam["Status"] = this.ddlStatus.SelectedValue;
        this.dicParam["OrderNo"] = this.tbOrderNo.Text.Trim();
        this.dicParam["Item"] = this.tbItem.Text.Trim();
        this.dicParam["StartDate"] = this.tbStartDate.Text.Trim();
        this.dicParam["EndDate"] = this.tbEndDate.Text.Trim();
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
        //todo
    }

    private IList<CodeMaster> GetStatusGroup()
    {
        IList<CodeMaster> statusGroup = new List<CodeMaster>();

        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE));

        return statusGroup;
    }

    private CodeMaster GetStatus(string statusValue)
    {
        return TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_STATUS, statusValue);
    }
}
