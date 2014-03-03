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

public partial class Reports_CycCntDiff_List : ReportModuleBase
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
        GV_List.Columns[1].Visible = !this._criteriaParam.ClassifiedOrderNo;//订单
        GV_List.Columns[2].Visible = !this._criteriaParam.ClassifiedLocation;//库位
        GV_List.Columns[3].Visible = !this._criteriaParam.ClassifiedBin;//库格
        GV_List.Columns[8].Visible = !this._criteriaParam.ClassifiedHuId;//条码
        GV_List.Columns[9].Visible = !this._criteriaParam.ClassifiedHuId;//批号
        GV_List.Columns[14].Visible = !this._criteriaParam.ClassifiedHuId;//参考库位
        GV_List.Columns[15].Visible = !this._criteriaParam.ClassifiedHuId;//原因

        this.GV_List.Execute();
    }

    protected override void SetCriteria()
    {
        DetachedCriteria criteria = DetachedCriteria.For(typeof(CycleCountResult));
        criteria.CreateAlias("CycleCount", "cc");
        criteria.CreateAlias("cc.Location", "l");

        #region Customize
        SecurityHelper.SetRegionSearchCriteria(criteria, "l.Region.Code", this.CurrentUser.Code); //区域权限
        OrderHelper.SetActiveOrderStatusCriteria(criteria, "cc.Status");//状态
        #endregion

        #region Select Parameters
        CriteriaHelper.SetPartyCriteria(criteria, "l.Region.Code", this._criteriaParam);
        CriteriaHelper.SetLocationCriteria(criteria, "cc.Location.Code", this._criteriaParam);
        CriteriaHelper.SetStartDateCriteria(criteria, "cc.EffectiveDate", this._criteriaParam);
        CriteriaHelper.SetEndDateCriteria(criteria, "cc.EffectiveDate", this._criteriaParam);
        CriteriaHelper.SetStorageBinCriteria(criteria, "StorageBin.Code", this._criteriaParam);
        CriteriaHelper.SetItemCriteria(criteria, "Item.Code", this._criteriaParam);
        CriteriaHelper.SetOrderNoCriteria(criteria, "CycleCount.Code", this._criteriaParam, MatchMode.Start);
        #endregion

        #region Projections
        ProjectionList projectionList = Projections.ProjectionList()
            .Add(Projections.Max("Id").As("Id"))
            .Add(Projections.Sum("Qty").As("Qty"))
            .Add(Projections.Sum("InvQty").As("InvQty"))
            .Add(Projections.Sum("DiffQty").As("DiffQty"))
            .Add(Projections.Count("HuId").As("Cartons"))
            .Add(Projections.GroupProperty("Item").As("Item"));

        if (!this._criteriaParam.ClassifiedOrderNo)
        {
            projectionList.Add(Projections.GroupProperty("CycleCount").As("CycleCount"));
        }
        if (!this._criteriaParam.ClassifiedLocation)
        {
            projectionList.Add(Projections.GroupProperty("cc.Location").As("Location"));
        }
        if (!this._criteriaParam.ClassifiedBin)
        {
            projectionList.Add(Projections.GroupProperty("StorageBin").As("StorageBin"));//库格
        }
        if (!this._criteriaParam.ClassifiedHuId)
        {
            projectionList.Add(Projections.GroupProperty("HuId").As("HuId"));//条码
            projectionList.Add(Projections.GroupProperty("LotNo").As("LotNo"));//批号
            projectionList.Add(Projections.GroupProperty("ReferenceLocation").As("ReferenceLocation"));//参考库位
            projectionList.Add(Projections.GroupProperty("DiffReason").As("DiffReason"));//差异原因
        }

        criteria.SetProjection(projectionList);
        criteria.SetResultTransformer(Transformers.AliasToBean(typeof(CycleCountResult)));
        #endregion

        DetachedCriteria selectCountCriteria = CloneHelper.DeepClone<DetachedCriteria>(criteria);
        selectCountCriteria.SetProjection(Projections.Count("Id"));
        SetSearchCriteria(criteria, selectCountCriteria);
    }
}
