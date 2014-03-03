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
using com.Sconit.Entity.Dss;


public partial class Visualization_CompletedWithoutStorage_Search : ModuleBase
{

    public event EventHandler SearchEvent;
    public event EventHandler ExportEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.tbStartDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
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

        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(CompletedWithoutStorageView));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(CompletedWithoutStorageView)).SetProjection(Projections.Count("Id"));

        if (this.tbPartyFrom.Text.Trim() != string.Empty)
        {
           
            selectCriteria.Add(Expression.Like("PartyFrom.Code",this.tbPartyFrom.Text.Trim(),MatchMode.Start));
            selectCountCriteria.Add(Expression.Like("PartyFrom.Code", this.tbPartyFrom.Text.Trim(), MatchMode.Start));
        }

        if (this.tbStartDate.Text.Trim() != string.Empty)
        {
            DateTime startDate = DateTime.Parse(this.tbStartDate.Text.Trim());
            selectCriteria.Add(Expression.Ge("CreateDate", startDate));
            selectCountCriteria.Add(Expression.Ge("CreateDate", startDate));
        }

        if (this.tbEndDate.Text.Trim() != string.Empty)
        {
            DateTime endDate = DateTime.Parse(this.tbEndDate.Text.Trim());
            selectCriteria.Add(Expression.Lt("CreateDate", endDate.AddDays(1)));
            selectCountCriteria.Add(Expression.Lt("CreateDate", endDate.AddDays(1)));
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
