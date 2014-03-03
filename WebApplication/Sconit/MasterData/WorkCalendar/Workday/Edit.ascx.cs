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
using com.Sconit.Service.MasterData;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity;
using System.Collections.Generic;

public partial class MasterData_WorkCalendar_Workday_Edit : EditModuleBase
{
    private Workday workday;
    public event EventHandler BackEvent;

    protected string code
    {
        get
        {
            return (string)ViewState["code"];
        }
        set
        {
            ViewState["code"] = value;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    public void InitPageParameter(string code)
    {
        this.code = code;
        this.ODS_Workday.SelectParameters["Id"].DefaultValue = code;
        this.ODS_Workday.DeleteParameters["Id"].DefaultValue = code;
        this.UpdateView();

        this.ODS_ShiftsNotInWorkday.SelectParameters["Id"].DefaultValue = code;
        this.ODS_ShiftsInWorkday.SelectParameters["Id"].DefaultValue = code;
    }

    protected void FV_Workday_DataBound(object sender, EventArgs e)
    {
        this.UpdateView();
    }

    private void UpdateView()
    {
        workday = TheWorkdayMgr.LoadWorkday(Convert.ToInt32(this.code));
        TextBox tbRegion = (TextBox)(this.FV_Workday.FindControl("tbRegion"));
        TextBox tbWorkCenter = (TextBox)(this.FV_Workday.FindControl("tbWorkCenter"));
        RadioButtonList rblType = (RadioButtonList)(this.FV_Workday.FindControl("rblType"));

        if (workday.Region != null)
        {
            tbRegion.Text = workday.Region.Code;
        }
        if (workday.WorkCenter != null)
        {
            tbWorkCenter.Text = workday.WorkCenter.Code;
        }
        rblType.SelectedValue = workday.Type;
    }

    protected void ODS_Workday_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string region = ((TextBox)(this.FV_Workday.FindControl("tbRegion"))).Text.Trim();
        string workcenter = ((TextBox)(this.FV_Workday.FindControl("tbWorkCenter"))).Text.Trim();
        RadioButtonList rblType = (RadioButtonList)(this.FV_Workday.FindControl("rblType"));

        workday = (Workday)e.InputParameters[0];
        if (region == "")
        {
            workday.Region = null;
        }
        else
        {
            workday.Region = TheRegionMgr.LoadRegion(region);
            if (workday.Region == null)
            {
                ShowWarningMessage("MasterData.WorkCalendar.WarningMessage.RegionInvalid", region);
                e.Cancel = true;
            }
        }
        if (workcenter == "")
        {
            workday.WorkCenter = null;
        }
        else
        {
            workday.WorkCenter = TheWorkCenterMgr.LoadWorkCenter(workcenter);
            if (workday.WorkCenter == null)
            {
                ShowWarningMessage("MasterData.WorkCalendar.WarningMessage.WorkCenterInvalid", workcenter);
                e.Cancel = true;
            }
        }
        workday.Type = rblType.SelectedValue;
    }

    protected void ODS_Workday_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("MasterData.WorkCalendar.Update.Successfully");
    }

    protected void ODS_Workday_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception == null)
        {
            btnBack_Click(this, e);
            ShowSuccessMessage("MasterData.WorkCalendar.Delete.Successfully");
        }
        else if (e.Exception.InnerException is Castle.Facilities.NHibernateIntegration.DataException)
        {
            ShowErrorMessage("MasterData.WorkCalendar.Delete.Failed");
            e.ExceptionHandled = true;
        }
    }

    #region WorkdayShift
    protected void ToInBT_Click(object sender, EventArgs e)
    {
        List<Shift> sList = new List<Shift>();
        foreach (ListItem item in this.CBL_NotInWorkday.Items)
        {
            if (item.Selected)
            {
                sList.Add(TheShiftMgr.LoadShift(item.Value));
            }
        }
        if (sList.Count > 0) TheWorkdayShiftMgr.CreateWorkdayShifts(TheWorkdayMgr.LoadWorkday(Convert.ToInt32(this.code)), sList);
        this.CBL_NotInWorkday.DataBind();
        this.CBL_InWorkday.DataBind();
        this.cb_InWorkday.Checked = false;
        this.cb_NotInWorkday.Checked = false;
    }

    protected void ToOutBT_Click(object sender, EventArgs e)
    {
        List<WorkdayShift> wsList = new List<WorkdayShift>();
        foreach (ListItem item in this.CBL_InWorkday.Items)
        {
            if (item.Selected)
            {
                WorkdayShift workdayShift = TheWorkdayShiftMgr.LoadWorkdayShift(Convert.ToInt32(this.code), item.Value);
                wsList.Add(workdayShift);
            }
        }
        if (wsList.Count > 0) TheWorkdayShiftMgr.DeleteWorkdayShift(wsList);
        this.CBL_NotInWorkday.DataBind();
        this.CBL_InWorkday.DataBind();
        this.cb_InWorkday.Checked = false;
        this.cb_NotInWorkday.Checked = false;
    }
    #endregion
}
