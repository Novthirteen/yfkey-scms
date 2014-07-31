using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MRP;
using com.Sconit.Utility;
using com.Sconit.Web;
using System.Data.SqlClient;
using com.Sconit.Entity.MasterData;
using System.Xml;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using NHibernate.Expression;

public partial class NewMrp_ShiftPlan_Search : SearchModuleBase
{

    public event EventHandler SearchEvent;
    protected void Page_Load(object sender, EventArgs e)
    {
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

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(ShiftPlanMstr));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(ShiftPlanMstr))
                 .SetProjection(Projections.Count("Id"));
            if (releaseNo != string.Empty)
            {
                selectCriteria.Add(Expression.Eq("ReleaseNo", int.Parse(releaseNo)));
                selectCountCriteria.Add(Expression.Eq("ReleaseNo",int.Parse( releaseNo)));
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

    protected void rblAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.rblAction.SelectedIndex == 0)
        {
            this.tblImport.Visible = false;
            this.tblSearch.Visible = true;
            this.list.Visible = true;
        }
        else
        {
            this.tblImport.Visible = true;
            this.tblSearch.Visible = false;
            this.list.Visible = false;
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            var shiftPlanList = TheMrpMgr.ReadShiftPlanFromXls(fileUpload.PostedFile.InputStream, this.CurrentUser);
            ShowSuccessMessage("导入成功。");

            //this.ListTable(shiftPlanList);
        }
        catch (BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
    }

}