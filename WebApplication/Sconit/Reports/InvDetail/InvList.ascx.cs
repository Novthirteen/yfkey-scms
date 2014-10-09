using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity;
using com.Sconit.Utility;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using System.Text.RegularExpressions;
using com.Sconit.Entity.View;
using NHibernate.Expression;
using System.Collections;

public partial class Reports_InvDetail_InvList : ReportModuleBase
{
    public string ModuleType
    {
        get { return (string)ViewState["ModuleType"]; }
        set { ViewState["ModuleType"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public override void InitPageParameter(object sender)
    {
        this._criteriaParam = (CriteriaParam)((object[])sender)[0];
        this.SetCriteria();
        this.UpdateView();
    }

    public override void UpdateView()
    {
        this.GV_List.Execute();
    }

    public void Export()
    {
        this.GV_List.ExportXLS();
    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected override void SetCriteria()
    {
        DetachedCriteria criteria = DetachedCriteria.For(typeof(LocationDetail));
        criteria.CreateAlias("Location", "l");

        #region Customize
        SecurityHelper.SetRegionSearchCriteria(criteria, "l.Region.Code", this.CurrentUser.Code); //区域权限
        #endregion

        #region Select Parameters
        CriteriaHelper.SetLocationCriteria(criteria, "l.Code", this._criteriaParam);
        CriteriaHelper.SetItemCriteria(criteria, "Item.Code", this._criteriaParam);
        #endregion

        DetachedCriteria selectCountCriteria = CloneHelper.DeepClone<DetachedCriteria>(criteria);
        selectCountCriteria.SetProjection(Projections.Count("Id"));
        SetSearchCriteria(criteria, selectCountCriteria);
    }

    public override void PostProcess(IList list)
    {
        TheLocationDetailMgr.PostProcessInvHistory(list, _criteriaParam.EndDate);
    }
}
