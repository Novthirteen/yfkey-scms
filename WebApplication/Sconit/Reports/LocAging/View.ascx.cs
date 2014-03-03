using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity.View;
using NHibernate.Expression;
using com.Sconit.Utility;
using System.Collections;
using NHibernate.Transform;

public partial class Reports_LocAging_View : ReportModuleBase
{
    public event EventHandler Click;

    public string Text
    {
        get { return this.lbView.Text; }
        set { this.lbView.Text = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public override void InitPageParameter(object sender)
    {
        this._criteriaParam = (CriteriaParam)sender;
        this.UpdateView();
    }

    public override void UpdateView()
    {
        this.SumQty();
    }

    protected override void SetCriteria()
    {
    }

    protected void lbView_Click(object sender, EventArgs e)
    {
        if (Click != null)
        {
            DetachedCriteria selectCriteria = this.GetCriteria();
            DetachedCriteria selectCountCriteria = CloneHelper.DeepClone<DetachedCriteria>(selectCriteria);
            selectCountCriteria.SetProjection(Projections.Count("Id"));

            Click(new object[] { selectCriteria, selectCountCriteria }, null);
        }
    }

    private DetachedCriteria GetCriteria()
    {
        DetachedCriteria criteria = DetachedCriteria.For(typeof(InventoryAgingView));

        SecurityHelper.SetRegionSearchCriteria(criteria, "Region.Code", this.CurrentUser.Code); //区域权限

        #region Select Parameters
        CriteriaHelper.SetPartyCriteria(criteria, "Region.Code", this._criteriaParam);
        CriteriaHelper.SetLocationCriteria(criteria, "Location.Code", this._criteriaParam);
        CriteriaHelper.SetItemCriteria(criteria, "Item.Code", this._criteriaParam, MatchMode.Exact);
        CriteriaHelper.SetStartDateCriteria(criteria, "CreateDate", this._criteriaParam);
        CriteriaHelper.SetEndDateCriteria(criteria, "CreateDate", this._criteriaParam);
        #endregion

        return criteria;
    }

    private void SumQty()
    {
        DetachedCriteria criteria = this.GetCriteria();
        criteria.SetProjection(Projections.Sum("Qty"));

        IList result = TheCriteriaMgr.FindAll(criteria);
        if (result[0] != null)
        {
            this.Text = ((decimal)result[0]).ToString("0.########");
        }
    }
}
