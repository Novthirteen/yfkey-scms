using System;
using System.Collections;
using System.Collections.Generic;
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
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using com.Sconit.Entity.View;
using com.Sconit.Entity;
using com.Sconit.Utility;
using NHibernate.Transform;

public partial class MasterData_Reports_WoReceipt_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    public event EventHandler ExportEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbPartyFrom.ServiceParameter = "string:" + BusinessConstants.CODE_MASTER_ORDER_TYPE_VALUE_PRODUCTION + ",string:" + this.CurrentUser.Code;
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ExportEvent != null)
        {
            ExportEvent(new Object[] { SetCriteria(), SetDetailCriteria() }, null);
        }
    }

    protected override void DoSearch()
    {
        if (SearchEvent != null)
        {
            SearchEvent(new Object[] { SetCriteria(), SetDetailCriteria() }, null);
        }
    }

    private DetachedCriteria SetCriteria()
    {
        DetachedCriteria criteria = DetachedCriteria.For(typeof(WoReceiptView));

        ProjectionList projectionList = Projections.ProjectionList()
           .Add(Projections.Sum("RecQty").As("RecQty"))
           .Add(Projections.GroupProperty("Flow").As("Flow"))
           .Add(Projections.GroupProperty("Item").As("Item"))
           .Add(Projections.GroupProperty("ItemDesc").As("ItemDesc"))
           .Add(Projections.Max("Id").As("Id"))
           .Add(Projections.Count("Id").As("BoxCount"));

        #region Customize
        SecurityHelper.SetRegionSearchCriteria(criteria, "PartyFrom", this.CurrentUser.Code); //区域权限
        #endregion

        #region Select Parameters
        if (this.tbPartyFrom.Text.Trim() != string.Empty)
        {
            criteria.Add(Expression.Eq("PartyFrom", this.tbPartyFrom.Text.Trim()));
        }
        if (this.tbOrderNo.Text.Trim() != string.Empty)
        {
            criteria.Add(Expression.Eq("OrderNo", this.tbOrderNo.Text.Trim()));
        }
        if (this.tbItem.Text.Trim() != string.Empty)
        {
            criteria.Add(Expression.Eq("Item", this.tbItem.Text.Trim()));
        }

        if (this.tbStartTime.Text.Trim() != string.Empty)
        {
            criteria.Add(Expression.Ge("CreateDate", DateTime.Parse(this.tbStartTime.Text.Trim())));
        }
        if (this.tbEndTime.Text.Trim() != string.Empty)
        {
            criteria.Add(Expression.Le("CreateDate", DateTime.Parse(this.tbEndTime.Text.Trim())));
        }
        #endregion

        criteria.SetProjection(projectionList);
        criteria.SetResultTransformer(Transformers.AliasToBean(typeof(WoReceiptView)));
        return criteria;
    }

    private DetachedCriteria SetDetailCriteria()
    {
        DetachedCriteria criteria = DetachedCriteria.For(typeof(WoReceiptView));

        #region Customize
        SecurityHelper.SetRegionSearchCriteria(criteria, "PartyFrom", this.CurrentUser.Code); //区域权限
        #endregion

        #region Select Parameters
        if (this.tbPartyFrom.Text.Trim() != string.Empty)
        {
            criteria.Add(Expression.Eq("PartyFrom", this.tbPartyFrom.Text.Trim()));
        }
        if (this.tbOrderNo.Text.Trim() != string.Empty)
        {
            criteria.Add(Expression.Eq("OrderNo", this.tbOrderNo.Text.Trim()));
        }

        if (this.tbStartTime.Text.Trim() != string.Empty)
        {
            criteria.Add(Expression.Ge("CreateDate", DateTime.Parse(this.tbStartTime.Text.Trim())));
        }
        if (this.tbEndTime.Text.Trim() != string.Empty)
        {
            criteria.Add(Expression.Le("CreateDate", DateTime.Parse(this.tbEndTime.Text.Trim())));
        }
        #endregion


        return criteria;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        OrderHead order = new OrderHead();
        if (this.tbPartyFrom.Text.Trim() != string.Empty)
        {
            order.PartyFrom = ThePartyMgr.LoadParty(this.tbPartyFrom.Text.Trim());
        }
        if (this.tbOrderNo.Text.Trim() != string.Empty)
        {
            order.OrderNo = this.tbOrderNo.Text.Trim();
        }

        if (this.tbStartTime.Text.Trim() != string.Empty)
        {
            order.ReleaseDate = DateTime.Parse(this.tbStartTime.Text.Trim());
        }

        if (this.tbEndTime.Text.Trim() != string.Empty)
        {
            order.StartDate = DateTime.Parse(this.tbEndTime.Text.Trim());
        }
        order.CreateUser = this.CurrentUser;
        order.CreateDate = DateTime.Now;

        IList<object> list = new List<object>();


        IList<WoReceiptView> woReceiptList = TheCriteriaMgr.FindAll<WoReceiptView>(SetCriteria());


        list.Add(order);
        list.Add(woReceiptList);
        string printUrl = TheReportMgr.WriteToFile("WoReceipt.xls", list);
        Page.ClientScript.RegisterStartupScript(GetType(), "method", " <script language='javascript' type='text/javascript'>PrintOrder('" + printUrl + "'); </script>");
        this.ShowSuccessMessage("MasterData.WoReceipt.Print.Successful");

    }
}
