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

public partial class NewMrp_PurchasePlan_Search : SearchModuleBase
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
            this.tbCreateStartDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            this.tbCreateEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
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

                DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(PurchasePlanMstr2));
                DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(PurchasePlanMstr2))
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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        ////flowDetail.PackagingCode = ((System.Web.UI.HtmlControls.HtmlSelect)this.FV_FlowDetail.FindControl("tbPackagingCode")).Value;
        //string controlNums = this.btControl_Num.Value;
        //if (!string.IsNullOrEmpty(controlNums))
        //{
        //    string sql = string.Format(" select e from EDIFordPlan as e  where Control_Num in ('{0}') ", string.Join("','", controlNums.Split(',')));
        //    IList<EDIFordPlan> exportList = TheGenericMgr.FindAllWithCustomQuery<EDIFordPlan>(sql);
        //    if (exportList != null && exportList.Count > 0)
        //    {
        //        ExportExcel(exportList);
        //    }
        //}

    }

   



}
