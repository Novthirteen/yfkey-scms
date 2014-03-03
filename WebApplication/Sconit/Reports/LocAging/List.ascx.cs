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

public partial class Reports_LocAging_List : ReportModuleBase
{
    private int Days
    {
        get { return (int)ViewState["Days"]; }
        set { ViewState["Days"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            this.SetViewControl(e.Row);
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
            ((Literal)e.Row.FindControl("ltlRange1")).Text = TheLanguageMgr.TranslateMessage("Reports.InvAging.Range1", this.CurrentUser, Days.ToString());
            ((Literal)e.Row.FindControl("ltlRange2")).Text = TheLanguageMgr.TranslateMessage("Reports.InvAging.Range2", this.CurrentUser, Days.ToString(), (Days * 2).ToString());
            ((Literal)e.Row.FindControl("ltlRange3")).Text = TheLanguageMgr.TranslateMessage("Reports.InvAging.Range2", this.CurrentUser, (Days * 2).ToString(), (Days * 3).ToString());
            ((Literal)e.Row.FindControl("ltlRange4")).Text = TheLanguageMgr.TranslateMessage("Reports.InvAging.Range2", this.CurrentUser, (Days * 3).ToString(), (Days * 4).ToString());
            ((Literal)e.Row.FindControl("ltlRange5")).Text = TheLanguageMgr.TranslateMessage("Reports.InvAging.Range2", this.CurrentUser, (Days * 4).ToString(), (Days * 5).ToString());
            ((Literal)e.Row.FindControl("ltlRange6")).Text = TheLanguageMgr.TranslateMessage("Reports.InvAging.Range3", this.CurrentUser, (Days * 5).ToString());
        }
    }

    public override void InitPageParameter(object sender)
    {
        this._criteriaParam = (CriteriaParam)((object[])sender)[0];
        this.Days = (int)((object[])sender)[1];
        this.SetCriteria();
        this.UpdateView();
    }

    public void Export()
    {
        this.ExportXLS(GV_List);
    }

    public override void UpdateView()
    {
        GV_List.Columns[1].Visible = this._criteriaParam.ClassifiedParty;
        GV_List.Columns[2].Visible = this._criteriaParam.ClassifiedLocation;
        GV_List.Columns[3].Visible = this._criteriaParam.ClassifiedLocation;

        this.GV_List.Execute();
    }

    protected override void SetCriteria()
    {
        DetachedCriteria criteria = DetachedCriteria.For(typeof(LocationDetail));
        criteria.CreateAlias("Location", "l");

        #region Customize
        criteria.Add(Expression.Not(Expression.Eq("Qty", new decimal(0))));
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

        if (this._criteriaParam.ClassifiedParty)
        {
            projectionList.Add(Projections.GroupProperty("l.Region").As("Region"));
        }
        if (this._criteriaParam.ClassifiedLocation)
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

    protected void ucView_Click(object sender, EventArgs e)
    {
        this.ucDetail.SetSearchCriteria((DetachedCriteria)((object[])sender)[0], (DetachedCriteria)((object[])sender)[1]);
        this.ucDetail.Visible = true;
        this.ucDetail.UpdateView();
    }

    private void SetViewControl(GridViewRow gvr)
    {
        LocationDetail locationDetail = (LocationDetail)gvr.DataItem;
        int days = this.Days;
        DateTime date = this._criteriaParam.EndDate.HasValue ? this._criteriaParam.EndDate.Value : DateTime.Today;
        for (int i = 1; i <= 6; i++)
        {
            CriteriaParam criteriaParam = new CriteriaParam();
            criteriaParam.Location = locationDetail.Location != null ? new string[] { locationDetail.Location.Code } : null;
            criteriaParam.Party = locationDetail.Region != null ? new string[] { locationDetail.Region.Code } : null;
            criteriaParam.Item = locationDetail.Item.Code;
            if (i < 6)
                criteriaParam.StartDate = date.AddDays(-Convert.ToDouble(i * days));
            criteriaParam.EndDate = date.AddDays(-Convert.ToDouble((i - 1) * days));

            Reports_LocAging_View ucView = this.GetViewControl(gvr, i);
            ucView.InitPageParameter(criteriaParam);
        }
    }

    private Reports_LocAging_View GetViewControl(GridViewRow gvr, int range)
    {
        switch (range)
        {
            case 1:
                return (Reports_LocAging_View)gvr.FindControl("ucView1");
            case 2:
                return (Reports_LocAging_View)gvr.FindControl("ucView2");
            case 3:
                return (Reports_LocAging_View)gvr.FindControl("ucView3");
            case 4:
                return (Reports_LocAging_View)gvr.FindControl("ucView4");
            case 5:
                return (Reports_LocAging_View)gvr.FindControl("ucView5");
            case 6:
                return (Reports_LocAging_View)gvr.FindControl("ucView6");
            default:
                return null;
        }
    }
}
