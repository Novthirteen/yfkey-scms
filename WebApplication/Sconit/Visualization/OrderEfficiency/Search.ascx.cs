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


public partial class Visualization_OrderEfficiency_Search : ModuleBase
{

    public event EventHandler SearchEvent;
    public event EventHandler ExportEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            this.tbDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (ExportEvent != null)
        {
            DoSearch(true);
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (SearchEvent != null)
        {
            DoSearch(false);
        }

    }
    private void DoSearch(bool isExport)
    {

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(OrderView));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(OrderView)).SetProjection(Projections.Count("OrderNo"));

        if (this.tbDate.Text.Trim() != string.Empty)
        {
            DateTime date = DateTime.Parse(this.tbDate.Text.Trim());
            selectCriteria.Add(Expression.Eq("Date", date));
            selectCountCriteria.Add(Expression.Eq("Date", date));
        }

        if (this.ddlOrderViewType.Text.Trim() != string.Empty)
        {

            selectCriteria.Add(Expression.Eq("OrderViewType", this.ddlOrderViewType.SelectedValue));
            selectCountCriteria.Add(Expression.Eq("OrderViewType", this.ddlOrderViewType.SelectedValue));
        }
        if (this.tbUserCode.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Lt("StartUser.Code", this.tbUserCode.Text.Trim()));
            selectCountCriteria.Add(Expression.Lt("StartUser.Code", this.tbUserCode.Text.Trim()));
        }

        if (isExport)
        {
            ExportEvent((new object[] { selectCriteria, selectCountCriteria }), null);
        }
        else
        {
            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
        }
    }

}
