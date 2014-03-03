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
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using com.Sconit.Web;

public partial class MasterData_WorkCalendar_Workday_New : NewModuleBase
{
    private Workday workday;
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
        ((Controls_TextBox)(this.FV_Workday.FindControl("tbRegion"))).ServiceParameter = "string:" + this.CurrentUser.Code;
    }

    public void PageCleanup()
    {
        //((Controls_TextBox)(this.FV_Workday.FindControl("tbRegion"))).Text = string.Empty;
        //((Controls_TextBox)(this.FV_Workday.FindControl("tbWorkCenter"))).Text = string.Empty;
        ((TextBox)(this.FV_Workday.FindControl("tbDescription"))).Text = string.Empty;
    }

    protected void ODS_Workday_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        string region = ((Controls_TextBox)(this.FV_Workday.FindControl("tbRegion"))).Text.Trim();
        string workcenter = ((Controls_TextBox)(this.FV_Workday.FindControl("tbWorkCenter"))).Text.Trim();
        RadioButtonList rbType = (RadioButtonList)(this.FV_Workday.FindControl("rbType"));

        workday = (Workday)e.InputParameters[0];
        workday.Region = TheRegionMgr.LoadRegion(region);
        workday.WorkCenter = TheWorkCenterMgr.LoadWorkCenter(workcenter);
        workday.Type = rbType.SelectedValue;
    }

    protected void ODS_Workday_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (CreateEvent != null)
        {
            CreateEvent(workday.Id.ToString(), e);
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

        string region = ((Controls_TextBox)(this.FV_Workday.FindControl("tbRegion"))).Text.Trim();
        string workcenter = ((Controls_TextBox)(this.FV_Workday.FindControl("tbWorkCenter"))).Text.Trim();
        string dayOfWeek = ((DropDownList)(this.FV_Workday.FindControl("DayOfWeek_DDL"))).SelectedValue;
        RadioButtonList rbType = (RadioButtonList)(this.FV_Workday.FindControl("rbType"));

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
            case "cvWeek":
                if (TheWorkdayMgr.GetWorkdayByDayofweek(dayOfWeek, region, workcenter).Count > 0)
                    args.IsValid = false;
                break;
            default:
                break;
        }
    }
}
