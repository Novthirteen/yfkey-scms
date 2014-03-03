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

public partial class MasterData_WorkCalendar_TabNavigator : ModuleBase
{
    public event EventHandler lbWorkCalendarClickEvent;
    public event EventHandler lbWorkdayClickEvent;
    public event EventHandler lbShiftClickEvent;
    public event EventHandler lbSpecialTimeClickEvent;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void lbWorkCalendar_Click(object sender, EventArgs e)
    {
        if (lbWorkCalendarClickEvent != null)
        {
            lbWorkCalendarClickEvent(this, e);
        }

        this.tab_workcalendar.Attributes["class"] = "ajax__tab_active";
        this.tab_workday.Attributes["class"] = "ajax__tab_inactive";
        this.tab_shift.Attributes["class"] = "ajax__tab_inactive";
        this.tab_specialtime.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbWorkday_Click(object sender, EventArgs e)
    {
        if (lbWorkdayClickEvent != null)
        {
            lbWorkdayClickEvent(this, e);
        }

        this.tab_workcalendar.Attributes["class"] = "ajax__tab_inactive";
        this.tab_workday.Attributes["class"] = "ajax__tab_active";
        this.tab_shift.Attributes["class"] = "ajax__tab_inactive";
        this.tab_specialtime.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbShift_Click(object sender, EventArgs e)
    {
        if (lbShiftClickEvent != null)
        {
            lbShiftClickEvent(this, e);
        }

        this.tab_workcalendar.Attributes["class"] = "ajax__tab_inactive";
        this.tab_workday.Attributes["class"] = "ajax__tab_inactive";
        this.tab_shift.Attributes["class"] = "ajax__tab_active";
        this.tab_specialtime.Attributes["class"] = "ajax__tab_inactive";
    }

    protected void lbSpecialTime_Click(object sender, EventArgs e)
    {
        if (lbSpecialTimeClickEvent != null)
        {
            lbSpecialTimeClickEvent(this, e);
        }

        this.tab_workcalendar.Attributes["class"] = "ajax__tab_inactive";
        this.tab_workday.Attributes["class"] = "ajax__tab_inactive";
        this.tab_shift.Attributes["class"] = "ajax__tab_inactive";
        this.tab_specialtime.Attributes["class"] = "ajax__tab_active";
    }
}
