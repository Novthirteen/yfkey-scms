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
using com.Sconit.Web;
using com.Sconit.Service.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;
using com.Sconit.Entity;
using com.Sconit.Utility;
using NHibernate.Expression;
using com.Sconit.Entity.View;


public partial class Inventory_PendingInspectOrder_Search : ModuleBase
{

    public event EventHandler SearchEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.tbStartDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(InspectOrderDetail));
        selectCriteria.CreateAlias("InspectOrder", "o");
        selectCriteria.CreateAlias("o.CreateUser", "u");
        selectCriteria.CreateAlias("LocationTo", "lt");
        selectCriteria.CreateAlias("lt.Region", "pt");
        selectCriteria.Add(Expression.Or( Expression.Gt("PendingRejectedQty", new Decimal(0)),Expression.Gt("PendingQualifiedQty", new Decimal(0))));

        #region 根据LocTo权限
        SecurityHelper.SetPartySearchCriteria(selectCriteria, "pt.Code", this.CurrentUser.Code);
        #endregion

        if (this.tbStartDate.Text.Trim() != string.Empty)
        {
            DateTime startDate = DateTime.Parse(this.tbStartDate.Text.Trim());
            selectCriteria.Add(Expression.Ge("o.CreateDate", startDate));
        }

        if (this.tbEndDate.Text.Trim() != string.Empty)
        {
            DateTime endDate = DateTime.Parse(this.tbEndDate.Text.Trim());
            selectCriteria.Add(Expression.Lt("o.CreateDate", endDate.AddDays(1)));
        }
        if (this.tbCreateUser.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("u.Code", tbCreateUser.Text.Trim()));
        }
        if (this.tbInspectNo.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("o.InspectNo", tbInspectNo.Text.Trim()));
        }
        if (this.cbOnlyShowQualify.Checked)
        {
            selectCriteria.Add(Expression.Gt("PendingQualifiedQty", new Decimal(0)));
            selectCriteria.Add(Expression.Eq("PendingRejectedQty", new Decimal(0)));
        }
        if (this.cbOnlyShowReject.Checked)
        {
            selectCriteria.Add(Expression.Gt("PendingRejectedQty", new Decimal(0)));
            selectCriteria.Add(Expression.Eq("PendingQualifiedQty", new Decimal(0)));
        }
        SearchEvent(selectCriteria, e);

    }

}
