using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Service.Criteria;
using com.Sconit.Web;
using com.Sconit.Entity;
using NHibernate.Expression;
using System.Collections.Generic;
using com.Sconit.Utility;
using NHibernate.Transform;


public partial class Distribution_PickList_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    private bool IsExport
    {
        get { return (bool)ViewState["IsExport"]; }
        set { ViewState["IsExport"] = value; }
    }

    //private int rblListIndex; 

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.StatusDataBind();
            this.tbStartDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.IsExport = false;
        DoSearch();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.IsExport = true;
        DoSearch();
    }

    protected override void DoSearch()
    {
        string pickListNo = this.tbPickListNo.Text.Trim();
        string orderNo = this.tbOrderNo.Text.Trim();
        string itemCode = this.tbItemCode.Text.Trim();
        string hu = this.tbHu.Text.Trim();
        string status = this.ddlStatus.SelectedValue;
        string startDate = this.tbStartDate.Text.Trim();
        string endDate = this.tbEndDate.Text.Trim();

        int rblListIndex = this.rblListFormat.SelectedIndex;
        if ((itemCode != string.Empty || orderNo != string.Empty) && rblListIndex == 0)
        {
            rblListIndex = 1;
            //ShowWarningMessage("MasterData.PickList.ItemOrOrderNo.Empty.Warning");
        }
        if (hu != string.Empty)
        {
            rblListIndex = 2;
            //ShowWarningMessage("MasterData.PickList.Hu.Empty.Warning");
        }
        //if (pickListNo == string.Empty && orderNo == string.Empty
        //    && itemCode == string.Empty && hu == string.Empty
        //    && (rblListIndex == 2 || rblListIndex == 1))
        //{
        //    rblListIndex = 0;
        //    ShowWarningMessage("MasterData.PickList.All.Empty.Warning");
        //}
        this.rblListFormat.SelectedIndex = rblListIndex;

        if (SearchEvent != null)
        {
            if (rblListIndex == 0)
            {
                object param = CollectGroupParam(pickListNo, orderNo, status, startDate, endDate);
                SearchEvent(param, null);
            }
            else if (rblListIndex == 1)
            {
                object param = CollectDetailParam(pickListNo, orderNo, status, startDate, endDate, itemCode);
                SearchEvent(param, null);
            }
            else if (rblListIndex == 2)
            {
                object param = CollectResultParam(pickListNo, orderNo, status, startDate, endDate, itemCode, hu);
                SearchEvent(param, null);
            }
        }
    }

    private object CollectGroupParam(string pickListNo, string orderNo, string status, string startDate, string endDate)
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(PickList));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(PickList))
            .SetProjection(Projections.ProjectionList()
           .Add(Projections.Count("PickListNo")));
        selectCriteria.CreateAlias("PartyFrom", "pf");
        selectCountCriteria.CreateAlias("PartyFrom", "pf");
        selectCriteria.CreateAlias("PartyTo", "pt");
        selectCountCriteria.CreateAlias("PartyTo", "pt");

        #region partyFrom
        SecurityHelper.SetPartyFromSearchCriteria(
            selectCriteria, selectCountCriteria, null, BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION, this.CurrentUser.Code);
        #endregion

        #region partyTo
        //SecurityHelper.SetPartyToSearchCriteria(
        //    selectCriteria, selectCountCriteria, null, BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION, this.CurrentUser.Code);
        #endregion

        if (pickListNo != string.Empty)
        {
            selectCriteria.Add(Expression.Like("PickListNo", pickListNo, MatchMode.Start));
            selectCountCriteria.Add(Expression.Like("PickListNo", pickListNo, MatchMode.Start));
        }
        if (startDate != string.Empty)
        {
            selectCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
            selectCountCriteria.Add(Expression.Ge("CreateDate", DateTime.Parse(startDate)));
        }
        if (endDate != string.Empty)
        {
            selectCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(endDate).AddDays(1)));
            selectCountCriteria.Add(Expression.Lt("CreateDate", DateTime.Parse(endDate).AddDays(1)));
        }
        if (status != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("Status", status));
            selectCountCriteria.Add(Expression.Eq("Status", status));
        }
        return new object[] { 0, selectCriteria, selectCountCriteria, IsExport };
    }

    private object CollectDetailParam(string pickListNo, string orderNo, string status, string startDate, string endDate, string itemCode)
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(PickListDetail));
        selectCriteria.CreateAlias("PickList", "p");
        selectCriteria.CreateAlias("p.CreateUser", "u");
        selectCriteria.CreateAlias("OrderLocationTransaction", "t");
        selectCriteria.CreateAlias("t.OrderDetail", "d");
        selectCriteria.CreateAlias("d.OrderHead", "h");

        #region partyFrom
        SecurityHelper.SetPartySearchCriteria(selectCriteria, "p.PartyFrom", this.CurrentUser.Code);
        #endregion

        if (startDate != string.Empty)
        {
            selectCriteria.Add(Expression.Ge("p.CreateDate", DateTime.Parse(startDate)));
        }
        if (endDate != string.Empty)
        {
            selectCriteria.Add(Expression.Lt("p.CreateDate", DateTime.Parse(endDate).AddDays(1)));
        }
        if (itemCode != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("Item.Code", itemCode));
        }
        if (orderNo != string.Empty)
        {
            selectCriteria.Add(Expression.Like("h.OrderNo", orderNo, MatchMode.Start));
        }
        if (pickListNo != string.Empty)
        {
            selectCriteria.Add(Expression.Like("p.PickListNo", pickListNo, MatchMode.Start));
        }

        if (status != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("p.Status", status));
        }
        DetachedCriteria selectCountCriteria = CloneHelper.DeepClone<DetachedCriteria>(selectCriteria);
        selectCountCriteria.SetProjection(Projections.Count("Id"));
        return new object[] { 1, selectCriteria, selectCountCriteria, IsExport };
    }

    private object CollectResultParam(string pickListNo, string orderNo, string status, string startDate, string endDate, string itemCode, string hu)
    {
        DetachedCriteria criteria = DetachedCriteria.For<PickListResult>();
        criteria.CreateAlias("LocationLotDetail", "lld");
        criteria.CreateAlias("lld.Location", "l");
        criteria.CreateAlias("lld.Item", "i");
        criteria.CreateAlias("lld.Hu", "hu");
        criteria.CreateAlias("PickListDetail", "pld");
        criteria.CreateAlias("pld.Uom", "u");
        criteria.CreateAlias("pld.PickList", "pl");
        criteria.CreateAlias("pld.OrderLocationTransaction", "olt");
        criteria.CreateAlias("olt.OrderDetail", "od");
        criteria.CreateAlias("od.OrderHead", "oh");

        #region partyFrom
        SecurityHelper.SetPartySearchCriteria(criteria, "pl.PartyFrom", this.CurrentUser.Code);
        #endregion

        if (pickListNo != null && pickListNo != string.Empty)
        {
            criteria.Add(Expression.Like("pl.PickListNo", pickListNo, MatchMode.Start));
        }
        if (orderNo != null && orderNo != string.Empty)
        {
            criteria.Add(Expression.Like("oh.OrderNo", orderNo, MatchMode.Start));
        }
        if (itemCode != string.Empty)
        {
            criteria.Add(Expression.Eq("i.Code", itemCode));
        }
        if (hu != string.Empty)
        {
            criteria.Add(Expression.Eq("hu.HuId", hu));
        }
        if (status != string.Empty)
        {
            criteria.Add(Expression.Eq("pl.Status", status));
        }
        if (startDate != string.Empty)
        {
            criteria.Add(Expression.Ge("pl.CreateDate", DateTime.Parse(startDate)));
        }
        if (endDate != string.Empty)
        {
            criteria.Add(Expression.Lt("pl.CreateDate", DateTime.Parse(endDate).AddDays(1)));
        }
        criteria.SetProjection(Projections.ProjectionList()
            .Add(Projections.GroupProperty("l.Code").As("LocationCode"))
            .Add(Projections.GroupProperty("i.Code").As("ItemCode"))
            .Add(Projections.GroupProperty("i.Desc1").As("ItemDescription"))
            .Add(Projections.GroupProperty("u.Code").As("UomCode"))
            .Add(Projections.GroupProperty("pld.UnitCount").As("UnitCount"))
            .Add(Projections.GroupProperty("pld.StorageBin.Code").As("StorageBinCode"))
            .Add(Projections.GroupProperty("pld.LotNo").As("LotNo"))
            .Add(Projections.GroupProperty("pl.Status").As("Status"))
            .Add(Projections.GroupProperty("pl.PickListNo").As("PickListNo"))
            .Add(Projections.GroupProperty("hu.HuId").As("HuId"))
            .Add(Projections.GroupProperty("oh.OrderNo").As("OrderNo"))
            .Add(Projections.Sum("Qty").As("Qty")));
        criteria.SetResultTransformer(Transformers.AliasToBean(typeof(PickListResult)));

        DetachedCriteria selectCountCriteria = CloneHelper.DeepClone<DetachedCriteria>(criteria);
        //DetachedCriteria selectCountCriteria = DetachedCriteria.For<PickListResult>();
        //CloneHelper.CopyProperty(criteria, selectCountCriteria);
        selectCountCriteria.SetProjection(Projections.Count("Id"));
        return new object[] { 2, criteria, selectCountCriteria, IsExport };
    }

    private void StatusDataBind()
    {
        this.ddlStatus.DataSource = this.GetStatusGroup();
        this.ddlStatus.DataBind();
    }
    private IList<CodeMaster> GetStatusGroup()
    {
        IList<CodeMaster> statusGroup = new List<CodeMaster>();

        statusGroup.Add(new CodeMaster()); //添加空选项
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE));
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT));
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS));
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CLOSE));
        statusGroup.Add(GetStatus(BusinessConstants.CODE_MASTER_STATUS_VALUE_CANCEL));

        return statusGroup;
    }
    private CodeMaster GetStatus(string statusValue)
    {
        return TheCodeMasterMgr.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_STATUS, statusValue);
    }
}
