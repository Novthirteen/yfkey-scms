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
using NHibernate.Expression;
using com.Sconit.Entity.Batch;
using com.Sconit.Entity;
using System.Collections.Generic;


public partial class Jobs_Log_Main : ModuleBase
{
    public int TriggerId
    {
        get
        {
            return (int)ViewState["TriggerId"];
        }
        set
        {
            ViewState["TriggerId"] = value;
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {


    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DoSearch();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        this.Visible = false;
    }

    protected  void DoSearch()
    {

        string startDate = this.tbStartDate.Text != string.Empty ? this.tbStartDate.Text.Trim() : string.Empty;
        string endDate = this.tbEndDate.Text != string.Empty ? this.tbEndDate.Text.Trim() : string.Empty;


        DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(BatchRunLog));
        DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(BatchRunLog))
            .SetProjection(Projections.ProjectionList()
           .Add(Projections.Count("Id")));


        selectCriteria.Add(Expression.Eq("BatchTrigger.Id", this.TriggerId));
        selectCountCriteria.Add(Expression.Eq("BatchTrigger.Id", this.TriggerId));

        if (startDate != string.Empty)
        {
            selectCriteria.Add(Expression.Ge("StartTime", DateTime.Parse(startDate)));
            selectCountCriteria.Add(Expression.Ge("StartTime", DateTime.Parse(startDate)));
        }
        if (endDate != string.Empty)
        {
            selectCriteria.Add(Expression.Lt("StartTime", DateTime.Parse(endDate).AddDays(1)));
            selectCountCriteria.Add(Expression.Lt("StartTime", DateTime.Parse(endDate).AddDays(1)));
        }

        this.ucList.Visible = true;
        this.ucList.SetSearchCriteria(selectCriteria, selectCountCriteria);
        this.ucList.UpdateView();
    }

    public void InitPageParameter(int triggerId)
    {
        this.TriggerId = triggerId;
        BatchTrigger batchTrigger = TheBatchTriggerMgr.LoadBatchTrigger(triggerId);
        this.lbJobName.Text = batchTrigger.BatchJobDetail.Name;
        this.lbJobDescription.Text = batchTrigger.BatchJobDetail.Description;
        this.tbStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        this.tbEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        DoSearch();
    }

}
