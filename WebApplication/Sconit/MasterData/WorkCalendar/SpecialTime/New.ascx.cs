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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;

public partial class MasterData_WorkCalendar_SpecialTime_New : NewModuleBase
{
    private SpecialTime specialTime;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;


    protected void Page_Load(object sender, EventArgs e)
    {
        ((Controls_TextBox)(this.FV_SpecialTime.FindControl("tbRegion"))).ServiceParameter = "string:" + this.CurrentUser.Code;
    }
    public void PageCleanup()
    {
        ((Controls_TextBox)(this.FV_SpecialTime.FindControl("tbRegion"))).Text = string.Empty;
        ((Controls_TextBox)(this.FV_SpecialTime.FindControl("tbWorkCenter"))).Text = string.Empty;
        ((TextBox)(this.FV_SpecialTime.FindControl("tbStartTime"))).Text = string.Empty;
        ((TextBox)(this.FV_SpecialTime.FindControl("tbEndTime"))).Text = string.Empty;
        ((TextBox)(this.FV_SpecialTime.FindControl("tbDescription"))).Text = string.Empty;
    }

    protected void ODS_SpecialTime_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string region = ((Controls_TextBox)(this.FV_SpecialTime.FindControl("tbRegion"))).Text.Trim();
        string workcenter = ((Controls_TextBox)(this.FV_SpecialTime.FindControl("tbWorkCenter"))).Text.Trim();
        RadioButtonList rblType = (RadioButtonList)(this.FV_SpecialTime.FindControl("rblType"));

        specialTime = (SpecialTime)e.InputParameters[0];
        specialTime.Region = TheRegionMgr.LoadRegion(region);
        specialTime.WorkCenter = TheWorkCenterMgr.LoadWorkCenter(workcenter);
        specialTime.Type = rblType.SelectedValue;
    }

    protected void ODS_SpecialTime_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(specialTime.ID.ToString(), e);
            ShowSuccessMessage("MasterData.WorkCalendar.Insert.Successfully");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }

    protected void Save_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator cv = (CustomValidator)source;

        string region = ((Controls_TextBox)(this.FV_SpecialTime.FindControl("tbRegion"))).Text.Trim();
        string workcenter = ((Controls_TextBox)(this.FV_SpecialTime.FindControl("tbWorkCenter"))).Text.Trim();
        string startTime = ((TextBox)(this.FV_SpecialTime.FindControl("tbStartTime"))).Text.Trim();
        string endTime = ((TextBox)(this.FV_SpecialTime.FindControl("tbEndTime"))).Text.Trim();
        string type = ((RadioButtonList)(this.FV_SpecialTime.FindControl("rblType"))).SelectedValue;

        switch (cv.ID)
        {
            case "cvRegion":
                if (TheRegionMgr.LoadRegion(region) == null)
                    args.IsValid = false;
                break;
            case "cvWorkCenter":
                if (TheWorkCenterMgr.LoadWorkCenter(workcenter) == null)
                    args.IsValid = false;
                break;
            case "cvStartTime":
                try
                {
                    Convert.ToDateTime(startTime);
                }
                catch (Exception)
                {
                    args.IsValid = false;
                }
                break;
            case "cvEndTime":
                try
                {
                    Convert.ToDateTime(endTime);
                }
                catch (Exception)
                {
                    args.IsValid = false;
                }
                if (DateTime.Compare(DateTime.Parse(startTime), DateTime.Parse(endTime)) >= 0)
                {
                    cv.ErrorMessage = "${MasterData.WorkCalendar.WarningMessage.TimeCompare}";
                    args.IsValid = false;
                }
                IList specialTimes = TheSpecialTimeMgr.GetSpecialTime(DateTime.Parse(startTime), DateTime.Parse(endTime), region, workcenter);
                foreach (SpecialTime st in specialTimes)
                {
                    if (st.Type == type)
                    {
                        cv.ErrorMessage = "${MasterData.WorkCalendar.WarningMessage.Error3}";
                        args.IsValid = false;
                    }
                }
                break;
            default:
                break;
        }
    }
}
