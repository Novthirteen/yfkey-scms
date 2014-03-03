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
using NHibernate.Expression;
using System.Collections.Generic;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;

public partial class Procurement_RollingForecast_Search : SearchModuleBase
{
    public event EventHandler SearchEvent;
    private bool IsExport
    {
        get { return (bool)ViewState["IsExport"]; }
        set { ViewState["IsExport"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
        this.tbSupplier.ServiceParameter = "string:" + this.CurrentUser.Code + ",string:" + BusinessConstants.PARTY_TYPE_SUPPLIER;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.IsExport = false;
        DoSearch();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.IsExport = true;
        DoSearch();
    }

    protected override void DoSearch()
    {
        if (SearchEvent != null)
        {


            DateTime startDate = DateTime.Parse(this.tbStartDate.Text.Trim());


            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(RollingForecast));
            SecurityHelper.SetPartySearchCriteria(selectCriteria, "Supplier.Code", this.CurrentUser.Code);

            if (this.tbSupplier.Text.Trim() != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("Supplier.Code", this.tbSupplier.Text.Trim()));
            }

            selectCriteria.Add(Expression.Ge("Date", startDate));
            selectCriteria.Add(Expression.Lt("Date", startDate.AddDays(140)));
            

            selectCriteria.SetProjection(Projections.ProjectionList()
                .Add(Projections.GroupProperty("Supplier").As("Supplier"))
                .Add(Projections.GroupProperty("Item").As("Item"))
                .Add(Projections.Max("Id").As("Id")));


            DetachedCriteria selectCountCriteria = CloneHelper.DeepClone<DetachedCriteria>(selectCriteria);
            selectCountCriteria.SetProjection(Projections.Count("Id"));
            SearchEvent((new object[] { selectCriteria, selectCountCriteria,startDate,this.IsExport}), null);
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {

        if (actionParameter.ContainsKey("StartDate"))
        {
            this.tbStartDate.Text = actionParameter["StartDate"];
        }

    }


}
