using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.View;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Utility;
using System.Collections;
using com.Sconit.Entity.MasterData;
using NHibernate.Transform;

public partial class Reports_InvTurn_List : ReportModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LocationDetail locationDetail = (LocationDetail)e.Row.DataItem;
            string regionCode = locationDetail.Region != null ? locationDetail.Region.Code : null;
            string locationCode = locationDetail.Location != null ? locationDetail.Location.Code : null;
            string itemCode = locationDetail.Item != null ? locationDetail.Item.Code : null;
            DateTime startDate = _criteriaParam.StartDate.HasValue ? _criteriaParam.StartDate.Value.AddDays(-1) : DateTime.Today.AddDays(-1);
            DateTime endDate = _criteriaParam.EndDate.HasValue ? _criteriaParam.EndDate.Value : DateTime.Today;

            locationDetail.StartInvQty = TheLocationDetailMgr.GetDateInventory(itemCode, locationCode, startDate);
            locationDetail.InvQty = TheLocationDetailMgr.GetDateInventory(itemCode, locationCode, endDate);
            locationDetail.TotalOutQty = this.GetTotalOutQty(locationDetail);
            //to be refactored
            ((Label)e.Row.FindControl("lblStartInvQty")).Text = locationDetail.StartInvQty.ToString("0.###");
            ((Label)e.Row.FindControl("lblInvQty")).Text = locationDetail.InvQty.ToString("0.###");
            ((Label)e.Row.FindControl("lblTotalOutQty")).Text = (-locationDetail.TotalOutQty).ToString("0.###");
            if (locationDetail.InvTurnRate.HasValue)
                ((Label)e.Row.FindControl("lblInvTurnRate")).Text = locationDetail.InvTurnRate.Value.ToString("0.###");
        }
    }

    public override void InitPageParameter(object sender)
    {
        this._criteriaParam = (CriteriaParam)sender;
        this.SetCriteria();
        this.UpdateView();
    }

    public void Export()
    {
        this.ExportXLS(GV_List);
    }

    public override void UpdateView()
    {
        GV_List.Columns[1].Visible = !this._criteriaParam.ClassifiedParty;
        GV_List.Columns[2].Visible = !this._criteriaParam.ClassifiedLocation;
        GV_List.Columns[3].Visible = !this._criteriaParam.ClassifiedLocation;

        this.GV_List.Execute();
    }

    protected override void SetCriteria()
    {
        DetachedCriteria criteria = DetachedCriteria.For(typeof(LocationDetail));
        criteria.CreateAlias("Location", "l");

        #region Customize
        SecurityHelper.SetRegionSearchCriteria(criteria, "l.Region.Code", this.CurrentUser.Code); //区域权限
        #endregion

        #region Select Parameters
        CriteriaHelper.SetPartyCriteria(criteria, "l.Region.Code", this._criteriaParam);
        CriteriaHelper.SetLocationCriteria(criteria, "Location.Code", this._criteriaParam);
        CriteriaHelper.SetItemCriteria(criteria, "Item.Code", this._criteriaParam);
        #endregion

        #region Projections
        ProjectionList projectionList = Projections.ProjectionList()
            .Add(Projections.Max("Id").As("Id"))
            .Add(Projections.Sum("Qty").As("Qty"))
            .Add(Projections.GroupProperty("Item").As("Item"));

        if (!this._criteriaParam.ClassifiedParty)
        {
            projectionList.Add(Projections.GroupProperty("l.Region").As("Region"));
        }
        if (!this._criteriaParam.ClassifiedLocation)
        {
            projectionList.Add(Projections.GroupProperty("Location").As("Location"));
        }

        criteria.SetProjection(projectionList);
        criteria.SetResultTransformer(Transformers.AliasToBean(typeof(LocationDetail)));
        #endregion

        DetachedCriteria selectCountCriteria = CloneHelper.DeepClone<DetachedCriteria>(criteria);
        selectCountCriteria.SetProjection(Projections.Count("Id"));
        SetSearchCriteria(criteria, selectCountCriteria);
    }

    private decimal GetTotalOutQty(LocationDetail locationDetail)
    {
        DetachedCriteria criteria = DetachedCriteria.For(typeof(LocationTransactionView));
        //criteria.CreateAlias("Location", "l");

        #region Customize
        //SecurityHelper.SetRegionSearchCriteria(criteria, "l.Region.Code", this.CurrentUser.Code); //区域权限
        CriteriaHelper.SetStartDateCriteria(criteria, "EffDate", _criteriaParam);
        CriteriaHelper.SetEndDateCriteria(criteria, "EffDate", _criteriaParam);

        #endregion

        #region Select Parameters
        string[] regionCode = locationDetail.Region != null ? new string[] { locationDetail.Region.Code } : null;
        string[] locationCode = locationDetail.Location != null ? new string[] { locationDetail.Location.Code } : null;
        string itemCode = locationDetail.Item != null ? locationDetail.Item.Code : null;
        //CriteriaHelper.SetPartyCriteria(criteria, "l.Region.Code", regionCode);
        CriteriaHelper.SetLocationCriteria(criteria, "Loc", locationCode);
        CriteriaHelper.SetItemCriteria(criteria, "Item", itemCode, MatchMode.Exact);
        criteria.Add(Expression.In("TransType", GetTransType()));
        #endregion

        criteria.SetProjection(Projections.Sum("Qty"));

        IList result = CriteriaMgr.FindAll(criteria);
        if (result[0] != null)
        {
            return (decimal)result[0];
        }
        else
        {
            return 0;
        }
    }

    private string[] GetTransType()
    {
        if (this._criteriaParam.ClassifiedParty || this._criteriaParam.ClassifiedLocation)
        {
            return new string[]{ 
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_SO,
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO,
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_UNP,
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_TR};
        }
        else
        {
            return new string[]{ 
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_SO,
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO,
                BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_UNP};
        }
    }
}
