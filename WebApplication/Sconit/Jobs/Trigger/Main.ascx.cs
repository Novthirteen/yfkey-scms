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
using com.Sconit.Entity.Exception;


public partial class Jobs_Trigger_Main : MainModuleBase
{
    public int CurrentIndex
    {
        get
        {
            return (int)ViewState["CurrentIndex"];
        }
        set
        {
            ViewState["CurrentIndex"] = value;
        }
    }

    public string CurrentJob
    {
        get
        {
            return (string)ViewState["CurrentJob"];
        }
        set
        {
            ViewState["CurrentJob"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void GV_List_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //停止
        if (e.CommandName.Equals("StopTrigger"))
        {
            int id = int.Parse(e.CommandArgument.ToString());
            BatchTrigger batchTrigger = TheBatchTriggerMgr.LoadBatchTrigger(id);
            batchTrigger.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_PAUSE;
            TheBatchTriggerMgr.UpdateBatchTrigger(batchTrigger);
            ShowSuccessMessage("MasterData.Jobs.Trigger.StopSuccessfully", batchTrigger.BatchJobDetail.Name);
            this.DataBind();
        }
        //启动
        if (e.CommandName.Equals("StartTrigger"))
        {
            int id = int.Parse(e.CommandArgument.ToString());
            BatchTrigger batchTrigger = TheBatchTriggerMgr.LoadBatchTrigger(id);
            batchTrigger.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS;
            //int minuteOdd = DateTime.Now.Minute % batchTrigger.Interval;
            //string newDate = DateTime.Now.AddMinutes(batchTrigger.Interval - minuteOdd).ToString("yyyy-MM-dd hh:mm");
            //batchTrigger.NextFireTime = DateTime.Parse(newDate);

            TheBatchTriggerMgr.UpdateBatchTrigger(batchTrigger);
            ShowSuccessMessage("MasterData.Jobs.Trigger.StartSuccessfully", batchTrigger.BatchJobDetail.Name);
            this.DataBind();
        }
        //查看日志
        if (e.CommandName.Equals("ViewLog"))
        {
            int id = int.Parse(e.CommandArgument.ToString());
            this.ucLog.Visible = true;
            this.ucLog.InitPageParameter(id);
        }
    }


    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            BatchTrigger batchTrigger = (BatchTrigger)e.Row.DataItem;
           // ((Label)e.Row.FindControl("lblName")).Text = batchTrigger.BatchJobDetail.Name;
           // ((Label)e.Row.FindControl("lblDescription")).Text = batchTrigger.BatchJobDetail.Description;

            if (batchTrigger.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_PAUSE)
            {

                e.Row.FindControl("lbtnStop").Visible = false;
                e.Row.FindControl("lbtnStart").Visible = true;

            }
            if (batchTrigger.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
            {

                e.Row.FindControl("lbtnStop").Visible = true;
                e.Row.FindControl("lbtnStart").Visible = false;

            }
        }
    }



    protected void ODS_GV_BatchTrigger_OnUpdating(object source, ObjectDataSourceMethodEventArgs e)
    {

        BatchTrigger batchTrigger = (BatchTrigger)e.InputParameters[0];
        BatchTrigger oldTrigger = TheBatchTriggerMgr.LoadBatchTrigger(batchTrigger.Id);
        batchTrigger.BatchJobDetail = oldTrigger.BatchJobDetail;
        batchTrigger.Name = oldTrigger.Name;
        batchTrigger.Description = oldTrigger.Description;

        GridViewRow row = this.GV_List.Rows[this.CurrentIndex];
        com.Sconit.Control.CodeMstrDropDownList ddlIntervalType = (com.Sconit.Control.CodeMstrDropDownList)row.FindControl("ddlIntervalType");
        if (ddlIntervalType.SelectedIndex != -1)
        {
            batchTrigger.IntervalType = ddlIntervalType.SelectedValue;
        }
        CurrentJob = oldTrigger.BatchJobDetail.Name;
    }

    protected void ODS_GV_BatchTrigger_OnUpdated(object sender, ObjectDataSourceStatusEventArgs e)
    {

        ShowSuccessMessage("MasterData.Jobs.Trigger.UpdateSuccessfully", CurrentJob);
    }

    protected void GV_BatchTrigger_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        this.CurrentIndex = e.RowIndex;
    }

    protected void GV_BatchTrigger_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            com.Sconit.Control.CodeMstrDropDownList ddlIntervalType = (com.Sconit.Control.CodeMstrDropDownList)e.Row.FindControl("ddlIntervalType");
            if (e.Row.DataItem != null)
            {
                BatchTrigger batchTrigger = (BatchTrigger)e.Row.DataItem;
                ddlIntervalType.DataBind();
                ddlIntervalType.DefaultSelectedValue = batchTrigger.IntervalType;

            }
        }
    }

    protected void GV_BatchTrigger_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewRow row = this.GV_List.Rows[e.NewEditIndex];
        HiddenField hfId = (HiddenField)row.FindControl("hfId");
        BatchTrigger batchTrigger = TheBatchTriggerMgr.LoadBatchTrigger(int.Parse(hfId.Value));
        if (batchTrigger.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)
        {
            ShowErrorMessage("MasterData.Jobs.Trigger.StopFirst");
            e.Cancel = true;
        }
    }

    protected void MRP_onclick(object sender, EventArgs e)
    {
        try
        {
            TheMrpMgr.RunMrp(this.CurrentUser);
        }
        catch (BusinessErrorException ex)
        {
            this.ShowErrorMessage(ex);
        }
    }
}
