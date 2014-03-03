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

public partial class Reports_LocTrans_List : ReportModuleBase
{
    private Visualization_GoodsTraceability_Traceability_View GetOrderViewControl(GridViewRow gvr)
    {
        return (Visualization_GoodsTraceability_Traceability_View)gvr.FindControl("ucView");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public override void InitPageParameter(object sender)
    {
        this._criteriaParam = (CriteriaParam)sender;
        this.SetCriteria();
        //DetachedCriteria criteria = GetCriteria();
        //SetSearchCriteria(criteria, CloneHelper.DeepClone<DetachedCriteria>(criteria));
        //UpdateView();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LocationTransactionView locationTransactionView = (LocationTransactionView)e.Row.DataItem;
            //if (locationTransactionView.TransType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_WO)
            //    || locationTransactionView.TransType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_WO))
            //{
            //    //GetOrderViewControl(e.Row).InitPageParameter(locationTransactionView.OrderNo, BusinessConstants.CODE_PREFIX_ORDER);
            //    e.Row.Cells[3].Text = locationTransactionView.OrderNo;
            //}
            //else if (locationTransactionView.TransType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_PO)
            //    || locationTransactionView.TransType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_RCT_TR))
            //{
            //    GetOrderViewControl(e.Row).InitPageParameter(locationTransactionView.RecNo, BusinessConstants.CODE_PREFIX_RECEIPT);
            //}
            //else if (locationTransactionView.TransType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_SO)
            //    || locationTransactionView.TransType.StartsWith(BusinessConstants.CODE_MASTER_LOCATION_TRANSACTION_TYPE_VALUE_ISS_TR))
            //{
            //    GetOrderViewControl(e.Row).InitPageParameter(locationTransactionView.IpNo, BusinessConstants.CODE_PREFIX_ASN);
            //}
        }
    }

    public void Export()
    {
        this.ExportXLS(GV_List);
    }

    public override void UpdateView()
    {
        this.GV_List.Columns[1].Visible = !this._criteriaParam.ClassifiedDate;
        this.GV_List.Columns[2].Visible = !this._criteriaParam.ClassifiedTransType;
        this.GV_List.Columns[13].Visible = !this._criteriaParam.ClassifiedUser;
        if (!this._criteriaParam.ClassifiedUser && !this._criteriaParam.ClassifiedDate)
        {
            this.GV_List.Columns[3].Visible = true;
            this.GV_List.Columns[6].Visible = true;
            this.GV_List.Columns[7].Visible = true;
        }
        else
        {
            this.GV_List.Columns[3].Visible = false;
            this.GV_List.Columns[6].Visible = false;
            this.GV_List.Columns[7].Visible = false;
        }
        //this.GV_List.Columns[6].Visible = this._criteriaParam.SumLocation;
        //this.GV_List.Columns[7].Visible = this._criteriaParam.SumLocation;

        this.GV_List.Execute();
    }

    protected override void SetCriteria()
    {
        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(LocationTransactionView));

        #region Select Parameters
        CriteriaHelper.SetStartDateCriteria(selectCriteria, "EffDate", this._criteriaParam);
        CriteriaHelper.SetEndDateCriteria(selectCriteria, "EffDate", this._criteriaParam);
        CriteriaHelper.SetLocationCriteria(selectCriteria, "Loc", this._criteriaParam);
        //目标库位 djin 2012-08-02
        if(_criteriaParam.RefLoc!=null)
        CriteriaHelper.SetRefLocationCriteria(selectCriteria, "RefLoc", this._criteriaParam);
        //end
        //CriteriaHelper.SetLocationCriteria(selectCriteria, "Location.Code", this._criteriaParam);
        CriteriaHelper.SetItemCriteria(selectCriteria, "Item", this._criteriaParam);
        CriteriaHelper.SetTransactionTypeCriteria(selectCriteria, "TransType", this._criteriaParam);
        #endregion

        //Party权限
        SecurityHelper.SetPartySearchCriteria(selectCriteria, "PartyFrom", this.CurrentUser.Code);
        SecurityHelper.SetPartySearchCriteria(selectCriteria, "PartyTo", this.CurrentUser.Code);

        #region Projections 默认按item,uom和location来group
        ProjectionList projectionList = Projections.ProjectionList()
            .Add(Projections.Sum("Qty").As("Qty"))
            .Add(Projections.GroupProperty("Id").As("Id"))
            .Add(Projections.GroupProperty("Item").As("Item"))
            .Add(Projections.GroupProperty("Uom").As("Uom"))
            //.Add(Projections.GroupProperty("RefOrderNo").As("RefOrderNo"))
            //.Add(Projections.GroupProperty("PartyFromName").As("PartyFromName"))
            //.Add(Projections.GroupProperty("PartyToName").As("PartyToName"))
            .Add(Projections.GroupProperty("Loc").As("Loc"))
            .Add(Projections.GroupProperty("LocName").As("LocName"))
            .Add(Projections.GroupProperty("ItemDescription").As("ItemDescription"))
            .Add(Projections.GroupProperty("TransType").As("TransType"))
             .Add(Projections.GroupProperty("OrderNo").As("OrderNo"))
         .Add(Projections.GroupProperty("IpNo").As("IpNo"))
         .Add(Projections.GroupProperty("RecNo").As("RecNo"))
         .Add(Projections.GroupProperty("LotNo").As("LotNo"));
        //.Add(Projections.GroupProperty("CreateUser").As("CreateUser"));
        if (_criteriaParam.RefLoc != null)
            projectionList.Add(Projections.GroupProperty("RefLoc").As("RefLoc"));
        //if (!this._criteriaParam.ClassifiedTransType)
        //{
        //    projectionList.Add(Projections.GroupProperty("TransType").As("TransType"));
        //}
        if (!this._criteriaParam.ClassifiedDate)
        {
            projectionList.Add(Projections.GroupProperty("EffDate").As("EffDate"));
        }
        if (!this._criteriaParam.ClassifiedUser)
        {
            projectionList.Add(Projections.GroupProperty("CreateUser").As("CreateUser"));
        }
        if (!this._criteriaParam.ClassifiedUser && !this._criteriaParam.ClassifiedDate)
        {
            projectionList.Add(Projections.GroupProperty("RefOrderNo").As("RefOrderNo"));
            projectionList.Add(Projections.GroupProperty("PartyFromName").As("PartyFromName"));
            projectionList.Add(Projections.GroupProperty("PartyToName").As("PartyToName"));
        }

        selectCriteria.SetProjection(projectionList);
        selectCriteria.SetResultTransformer(Transformers.AliasToBean(typeof(LocationTransactionView)));
        #endregion

      

        DetachedCriteria selectCountCriteria = CloneHelper.DeepClone<DetachedCriteria>(selectCriteria);

        selectCriteria.AddOrder(Order.Desc("Id"));
        selectCountCriteria.SetProjection(Projections.RowCount());
        SetSearchCriteria(selectCriteria, selectCountCriteria);
    }
}
