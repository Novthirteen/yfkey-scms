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
using com.Sconit.Entity.MRP;
using NHibernate.Expression;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public partial class NewMrp_ProductionPlan_Search : SearchModuleBase
{



    public event EventHandler SearchEvent;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbCreateStartDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            this.tbCreateEndDate.Text = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd");
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch();
    }

    protected override void DoSearch()
    {

        DateTime? startDate = System.DateTime.Now;
        DateTime? endDate = null;
        if (!string.IsNullOrEmpty(this.tbCreateStartDate.Text))
        {
            startDate = DateTime.Parse(this.tbCreateStartDate.Text);
        }
        if (!string.IsNullOrEmpty(this.tbCreateEndDate.Text))
        {
            endDate = DateTime.Parse(this.tbCreateEndDate.Text);
        }
        string releaseNo = this.tbReleaseNo.Text.Trim();

        if (SearchEvent != null)
        {
            #region DetachedCriteria

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(ProductionPlanMstr));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(ProductionPlanMstr))
                 .SetProjection(Projections.Count("Id"));
            if (releaseNo != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("RelaseNo", releaseNo));
                selectCountCriteria.Add(Expression.Eq("RelaseNo", releaseNo));
            }
            if (startDate != null)
            {
                selectCriteria.Add(Expression.Ge("CreateDate", startDate));
                selectCountCriteria.Add(Expression.Ge("CreateDate", startDate));
            }
            if (endDate != null)
            {
                selectCriteria.Add(Expression.Le("CreateDate", endDate));
                selectCountCriteria.Add(Expression.Le("CreateDate", endDate));
            }
            SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
    }
}
