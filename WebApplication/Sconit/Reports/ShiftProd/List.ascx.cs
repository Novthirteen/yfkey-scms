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
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class Reports_ShiftProd_List : ReportModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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
        GV_List.Columns[1].Visible = !this._criteriaParam.ClassifiedFlow;
        GV_List.Columns[2].Visible = !this._criteriaParam.ClassifiedFlow;
        GV_List.Columns[3].Visible = !this._criteriaParam.ClassifiedParty;
        GV_List.Columns[4].Visible = !this._criteriaParam.ClassifiedDate;
        GV_List.Columns[5].Visible = !this._criteriaParam.ClassifiedShift;

        this.GV_List.Execute();
    }

    protected override void SetCriteria()
    {
        DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderDetailView));

        #region Customize
        criteria.Add(Expression.Eq("Type", BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION));
        SecurityHelper.SetRegionSearchCriteria(criteria, "PartyTo.Code", this.CurrentUser.Code); //区域权限
        OrderHelper.SetActiveOrderStatusCriteria(criteria, "Status");//订单状态
        #endregion

        #region Select Parameters
        CriteriaHelper.SetFlowCriteria(criteria, "Flow", this._criteriaParam);
        CriteriaHelper.SetPartyCriteria(criteria, "PartyTo.Code", this._criteriaParam);
        CriteriaHelper.SetStartDateCriteria(criteria, "EffDate", this._criteriaParam);
        CriteriaHelper.SetEndDateCriteria(criteria, "EffDate", this._criteriaParam);
        CriteriaHelper.SetShiftCriteria(criteria, "Shift.Code", this._criteriaParam);
        CriteriaHelper.SetItemCriteria(criteria, "Item.Code", this._criteriaParam);
        #endregion

        #region Projections
        ProjectionList projectionList = Projections.ProjectionList()
            .Add(Projections.Max("Id").As("Id"))
            .Add(Projections.Sum("OrderedQty").As("OrderedQty"))
            .Add(Projections.Sum("ReceivedQty").As("ReceivedQty"))
            .Add(Projections.Sum("RejectedQty").As("RejectedQty"))
            .Add(Projections.Sum("ScrapQty").As("ScrapQty"))
            .Add(Projections.GroupProperty("Item").As("Item"))
            .Add(Projections.GroupProperty("Uom").As("Uom"));

        if (!this._criteriaParam.ClassifiedFlow)
        {
            projectionList.Add(Projections.GroupProperty("Flow").As("Flow"));
            projectionList.Add(Projections.GroupProperty("Description").As("Description"));
        }
        if (!this._criteriaParam.ClassifiedParty)
        {
            projectionList.Add(Projections.GroupProperty("PartyTo").As("PartyTo"));
        }
        if (!this._criteriaParam.ClassifiedShift)
        {
            projectionList.Add(Projections.GroupProperty("Shift").As("Shift"));
        }
        if (!this._criteriaParam.ClassifiedDate)
        {
            projectionList.Add(Projections.GroupProperty("EffDate").As("EffDate"));
        }

        criteria.SetProjection(projectionList);
        criteria.SetResultTransformer(Transformers.AliasToBean(typeof(OrderDetailView)));
        #endregion

        DetachedCriteria selectCountCriteria = CloneHelper.DeepClone<DetachedCriteria>(criteria);
        selectCountCriteria.SetProjection(Projections.Count("Id"));
        SetSearchCriteria(criteria, selectCountCriteria);
    }
}
