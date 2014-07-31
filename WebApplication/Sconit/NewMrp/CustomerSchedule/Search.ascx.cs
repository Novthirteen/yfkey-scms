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
using System.Data.SqlClient;

public partial class NewMrp_CustomerSchedule_Search : SearchModuleBase
{



    public event EventHandler SearchEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code;
        this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:true,bool:true,bool:false,bool:false,bool:false,string:"
          + BusinessConstants.PARTY_AUTHRIZE_OPTION_TO;
        if (!IsPostBack)
        {
            this.tbCreateStartDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            this.tbCreateEndDate.Text = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd");
        }
        this.showTimes.Style.Value = "display:none";
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
        string releaseNo = this.tbReferenceScheduleNo.Text.Trim();
        string flowCode =this.tbFlow.Text.Trim();
        if (SearchEvent != null)
        {
            #region DetachedCriteria

            DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(CustomerSchedule));
            DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(CustomerSchedule))
                     .SetProjection(Projections.Count("Id"));
                if (releaseNo != string.Empty)
                {
                    selectCriteria.Add(Expression.Eq("ReferenceScheduleNo", releaseNo));
                    selectCountCriteria.Add(Expression.Eq("ReferenceScheduleNo", releaseNo));
                }
                if (flowCode != string.Empty)
                {
                    selectCriteria.Add(Expression.Eq("Flow", flowCode));
                    selectCountCriteria.Add(Expression.Eq("Flow", flowCode));
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
        }
        else
        {
            this.tblImport.Visible = true;
            this.tblSearch.Visible = false;
        }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            var dateType = this.rblDateType.SelectedValue;
            var customerPlanList = TheMrpMgr.ReadCustomerPlanFromXls(fileUpload.PostedFile.InputStream, dateType, this.CurrentUser);

            ShowSuccessMessage("导入成功。");
        }
        catch (com.Sconit.Entity.Exception.BusinessErrorException ex)
        {
            ShowErrorMessage(ex);
        }
        catch (Exception et)
        {
            ShowErrorMessage(et.Message);
        }
    }

    protected void btnRunShipPlan_Click(object sender, EventArgs e)
    {
        try
        {

            TheMrpMgr.RunShipPlan(this.CurrentUser);
            ShowSuccessMessage("生成成功。");
        }
        catch (SqlException ex)
        {
            ShowErrorMessage(ex.Message);
        }
        catch (Exception ee)
        {
            ShowErrorMessage(ee.Message);
        }
        this.showTimes.Style.Value = "display:none";
    }


}
