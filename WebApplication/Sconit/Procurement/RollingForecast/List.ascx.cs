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
using com.Sconit.Entity.MasterData;
using NHibernate.Expression;
using System.Collections.Generic;

public partial class Procurement_RollingForecast_List : ListModuleBase
{
    public event EventHandler EditEvent;
    public DateTime StartDate
    {
        get
        {
            return (DateTime)ViewState["StartDate"];
        }
        set
        {
            ViewState["StartDate"] = value;
        }
    }

    public DateTime[] DateArr
    {
        get
        {
            return (DateTime[])ViewState["DateArr"];
        }
        set
        {
            ViewState["DateArr"] = value;
        }
    }

    public bool IsExport
    {
        get { return (bool)ViewState["IsExport"]; }
        set { ViewState["IsExport"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }


    public override void UpdateView()
    {
        this.DateArr = new DateTime[20];
        for (int i = 0; i < 20; i++)
        {
            DateTime day = StartDate.AddDays(7 * i);
            DateArr[i] = day;
            this.GV_List.Columns[3 + i].HeaderText = day.ToShortDateString();
        }
        if (!this.IsExport)
        {
            this.GV_List.Execute();
        }
        else
        {
            this.ExportXLS(GV_List, "RollingForecast" + DateTime.Now.ToString("ddhhmmss") + ".xls");
        }
    }


    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            object[] obj = (object[])e.Row.DataItem;

            ((Label)(e.Row.FindControl("lblSupplier"))).Text = ((Supplier)obj[0]).Code;
            ((Label)(e.Row.FindControl("lblItem"))).Text = ((Item)obj[1]).Code;
            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(RollingForecast));
            selectCriteria.Add(Expression.Eq("Supplier.Code", ((Supplier)obj[0]).Code));
            selectCriteria.Add(Expression.Eq("Item.Code", ((Item)obj[1]).Code));
            selectCriteria.Add(Expression.Ge("Date", StartDate));
            selectCriteria.Add(Expression.Lt("Date", StartDate.AddDays(140)));

            IList<RollingForecast> rollingForecastList = TheCriteriaMgr.FindAll<RollingForecast>(selectCriteria);

            for (int i = 0; i < DateArr.Count(); i++)
            {
                var q = rollingForecastList.Where(f => f.Date.ToShortDateString() == DateArr[i].ToShortDateString()).OrderByDescending(p => p.CreateDate);
                if (q.Count() > 0)
                {
                    RollingForecast c = q.First();
                    ((Label)(e.Row.FindControl("lblQty" + i))).Text = c.Qty.ToString("0.########");
                }

            }
        }
    }

    protected void lbtnView_Click(object sender, EventArgs e)
    {
        if (EditEvent != null)
        {
            string code = ((LinkButton)sender).CommandArgument;
            EditEvent(code, null);
        }
    }
}
