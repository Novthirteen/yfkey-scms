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
using com.Sconit.Entity;
using System.Collections.Generic;

public partial class MasterData_WorkCalendar_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ucTabNavigator.lbWorkCalendarClickEvent += new System.EventHandler(this.TabWorkCalendarClick_Render);
        this.ucTabNavigator.lbWorkdayClickEvent += new System.EventHandler(this.TabWorkdayClick_Render);
        this.ucTabNavigator.lbShiftClickEvent += new System.EventHandler(this.TabShiftClick_Render);
        this.ucTabNavigator.lbSpecialTimeClickEvent += new System.EventHandler(this.TabSpecialTimeClick_Render);

        if (!IsPostBack)
        {
            this.ucWorkCalendarViewMain.Visible = true;
            this.ucWorkdayMain.Visible = false;
            this.ucShiftMain.Visible = false;
            this.ucSpecialTimeMain.Visible = false;
        }
    }

    //The event handler when user click link button to "WorkCalendar" tab
    void TabWorkCalendarClick_Render(object sender, EventArgs e)
    {
        this.ucWorkCalendarViewMain.Visible = true;
        this.ucWorkdayMain.Visible = false;
        this.ucShiftMain.Visible = false;
        this.ucSpecialTimeMain.Visible = false;
    }

    //The event handler when user click link button to "Workday" tab
    void TabWorkdayClick_Render(object sender, EventArgs e)
    {
        this.ucWorkCalendarViewMain.Visible = false;
        this.ucWorkdayMain.Visible = true;
        this.ucShiftMain.Visible = false;
        this.ucSpecialTimeMain.Visible = false;
    }

    //The event handler when user click link button to "Shift" tab
    void TabShiftClick_Render(object sender, EventArgs e)
    {
        this.ucWorkCalendarViewMain.Visible = false;
        this.ucWorkdayMain.Visible = false;
        this.ucShiftMain.Visible = true;
        this.ucSpecialTimeMain.Visible = false;
    }

    //The event handler when user click link button to "SpecialTime" tab
    void TabSpecialTimeClick_Render(object sender, EventArgs e)
    {
        this.ucWorkCalendarViewMain.Visible = false;
        this.ucWorkdayMain.Visible = false;
        this.ucShiftMain.Visible = false;
        this.ucSpecialTimeMain.Visible = true;
    }
}
