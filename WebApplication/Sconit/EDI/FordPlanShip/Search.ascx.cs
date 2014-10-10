using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity;
using Geekees.Common.Controls;
using com.Sconit.Entity.EDI;
using NHibernate.Expression;

public partial class EDI_FordPlanShip_Search : SearchModuleBase
{



    public event EventHandler SearchEvent;
    //private List<string> StatusList
    //{
    //    get { return this.astvMyTree.GetCheckedNodes().Select(a => a.NodeValue).ToList(); }
    //}


    protected void Page_Load(object sender, EventArgs e)
    {
        //this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
       
        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch();
    }

    protected override void DoSearch()
    {

        DateTime? startDate = null;
        DateTime? endDate = null;
        DateTime? createStartDate = null;
        DateTime? createEndDate = null;
        if (!string.IsNullOrEmpty(this.tbStartDate.Text))
        {
            startDate = DateTime.Parse(this.tbStartDate.Text);
        }
        if (!string.IsNullOrEmpty(this.tbEndDate.Text))
        {
            endDate = DateTime.Parse(this.tbEndDate.Text);
        }

        if (SearchEvent != null)
        {
            #region

            string searchSql =string.Format( " select e from EDIFordPlan as e  where Type='D' ");

            if (!string.IsNullOrEmpty(this.tbStartDate.Text))
            {
                searchSql += string.Format(" and ForecastDate >='{0}'", DateTime.Parse(this.tbStartDate.Text));
            }
            if (!string.IsNullOrEmpty(this.tbEndDate.Text))
            {
                searchSql += string.Format(" and ForecastDate <='{0}'", DateTime.Parse(this.tbEndDate.Text));
            }
            if (!string.IsNullOrEmpty(this.tbCreateStartDate.Text))
            {
                searchSql += string.Format(" and CreateDate >='{0}'", DateTime.Parse(this.tbCreateStartDate.Text));
            }
            if (!string.IsNullOrEmpty(this.tbCreateEndDate.Text))
            {
                searchSql += string.Format(" and CreateDate <='{0}'", DateTime.Parse(this.tbCreateEndDate.Text));
            }

            SearchEvent((new object[] { searchSql + " order by CreateDate Desc,Id asc " }), null);
            #endregion
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
    }

}
