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
using com.Sconit.Entity.MasterData;

public partial class MasterData_WorkCalendar_SpecialTime_Edit : EditModuleBase
{
    private SpecialTime specialTime;
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
        this.ODS_SpecialTime.SelectParameters["ID"].DefaultValue = code;
        this.ODS_SpecialTime.DeleteParameters["ID"].DefaultValue = code;
        this.UpdateView();
    }

    protected void FV_SpecialTime_DataBound(object sender, EventArgs e)
    {
        this.UpdateView();
    }

    private void UpdateView()
    {
        specialTime = TheSpecialTimeMgr.LoadSpecialTime(Convert.ToInt32(this.code));
        if (specialTime == null)
            return;

        TextBox tbRegion = (TextBox)(this.FV_SpecialTime.FindControl("tbRegion"));
        TextBox tbWorkCenter = (TextBox)(this.FV_SpecialTime.FindControl("tbWorkCenter"));
        RadioButtonList rblType = (RadioButtonList)(this.FV_SpecialTime.FindControl("rblType"));

        if (specialTime.Region != null)
        {
            tbRegion.Text = specialTime.Region.Code;
        }
        if (specialTime.WorkCenter != null)
        {
            tbWorkCenter.Text = specialTime.WorkCenter.Code;
        }
        rblType.SelectedValue = specialTime.Type;
    }

    protected void ODS_SpecialTime_Updating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string region = ((TextBox)(this.FV_SpecialTime.FindControl("tbRegion"))).Text.Trim();
        string workcenter = ((TextBox)(this.FV_SpecialTime.FindControl("tbWorkCenter"))).Text.Trim();
        RadioButtonList rblType = (RadioButtonList)(this.FV_SpecialTime.FindControl("rblType"));

        SpecialTime specialTime = (SpecialTime)e.InputParameters[0];
        if (region == "")
        {
            specialTime.Region = null;
        }
        else
        {
            specialTime.Region = TheRegionMgr.LoadRegion(region);
            if (specialTime.Region == null)
            {
                ShowWarningMessage("MasterData.WorkCalendar.WarningMessage.RegionInvalid", region);
                e.Cancel = true;
            }
        }
        if (workcenter == "")
        {
            specialTime.WorkCenter = null;
        }
        else
        {
            specialTime.WorkCenter = TheWorkCenterMgr.LoadWorkCenter(workcenter);
            if (specialTime.WorkCenter == null)
            {
                ShowWarningMessage("MasterData.WorkCalendar.WarningMessage.WorkCenterInvalid", workcenter);
                e.Cancel = true;
            }
        }
        specialTime.Type = rblType.SelectedValue;
        try
        {
            Convert.ToDateTime(specialTime.StartTime);
        }
        catch (Exception)
        {
            ShowWarningMessage("MasterData.WorkCalendar.WarningMessage.StartTimeInvalid");
            e.Cancel = true;
        }
        try
        {
            Convert.ToDateTime(specialTime.EndTime);
        }
        catch (Exception)
        {
            ShowWarningMessage("MasterData.WorkCalendar.WarningMessage.EndTimeInvalid");
            e.Cancel = true;
        }
        if (DateTime.Compare(specialTime.StartTime, specialTime.EndTime) >= 0)
        {
            ShowWarningMessage("MasterData.WorkCalendar.WarningMessage.TimeCompare");
            e.Cancel = true;
        }
        IList specialTimes = TheSpecialTimeMgr.GetSpecialTime(specialTime.StartTime, specialTime.EndTime, region, workcenter);
        foreach (SpecialTime st in specialTimes)
        {
            if (st.Type == specialTime.Type)
            {
                ShowWarningMessage("MasterData.WorkCalendar.WarningMessage.Error3");
                e.Cancel = true;
            }
        }
    }

    protected void ODS_SpecialTime_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ShowSuccessMessage("MasterData.WorkCalendar.Update.Successfully");
    }

    protected void ODS_SpecialTime_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
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
}
