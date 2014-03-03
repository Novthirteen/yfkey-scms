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


public partial class Visualization_InspectDetail_Search : ModuleBase
{

    public event EventHandler SearchEvent;
    public event EventHandler ExportEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.tbLocation.ServiceParameter = "string:" + this.CurrentUser.Code;
     
        if (!IsPostBack)
        {
            this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.tbStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
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

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(InspectDetailView));
        selectCriteria.CreateAlias("InspectOrder", "o");
        selectCriteria.CreateAlias("LocationFrom", "lf");
        selectCriteria.CreateAlias("LocationTo", "lt");

        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(InspectDetailView)).SetProjection(Projections.Count("Id"));
        selectCountCriteria.CreateAlias("InspectOrder", "o");
        selectCountCriteria.CreateAlias("LocationFrom", "lf");

        if (this.tbLocation.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("lf.Code", this.tbLocation.Text.Trim()));
            selectCountCriteria.Add(Expression.Eq("lf.Code", this.tbLocation.Text.Trim()));
        }
        else
        {
            SecurityHelper.SetRegionSearchCriteria(selectCriteria, "lf.Region", this.CurrentUser.Code);
            SecurityHelper.SetRegionSearchCriteria(selectCountCriteria, "lf.Region", this.CurrentUser.Code); 
        }

        if (this.tbStartDate.Text.Trim() != string.Empty)
        {
            DateTime startDate = DateTime.Parse(this.tbStartDate.Text.Trim());
            selectCriteria.Add(Expression.Ge("o.CreateDate", startDate));
            selectCountCriteria.Add(Expression.Ge("o.CreateDate", startDate));
        }

        if (this.tbEndDate.Text.Trim() != string.Empty)
        {
            DateTime endDate = DateTime.Parse(this.tbEndDate.Text.Trim());
            selectCriteria.Add(Expression.Lt("o.CreateDate", endDate.AddDays(1)));
            selectCountCriteria.Add(Expression.Lt("o.CreateDate", endDate.AddDays(1)));
        }

        if (this.tbItemCode.Text.Trim() != string.Empty)
        {
            selectCriteria.Add(Expression.Eq("Item.Code", tbItemCode.Text.Trim()));
            selectCountCriteria.Add(Expression.Eq("Item.Code", tbItemCode.Text.Trim()));
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
