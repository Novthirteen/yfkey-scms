<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs"
    Inherits="MasterData_WorkCalendar_TabNavigator" %>
<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
        <span class='ajax__tab_active' id='tab_workcalendar' runat="server"><span class='ajax__tab_outer'><span 
                class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="LinkButton1" Text="${MasterData.WorkCalendar.WorkCalendarView}" runat="server"
                 OnClick="lbWorkCalendar_Click" /></span></span></span></span><span 
                 id='tab_shift' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
                 class='ajax__tab_tab'><asp:LinkButton ID="lbShift" Text="${MasterData.WorkCalendar.Shift}" runat="server" OnClick="lbShift_Click" 
                 /></span></span></span></span><span id='tab_workday' runat="server"><span 
                 class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton 
                 ID="lbWorkday" Text="${MasterData.WorkCalendar.Workday}" runat="server" OnClick="lbWorkday_Click" /></span></span></span></span><span id='tab_specialtime' runat="server"><span class='ajax__tab_outer'><span 
                 class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbSpecialTime" Text="${MasterData.WorkCalendar.SpecialTime}" runat="server" 
                 OnClick="lbSpecialTime_Click" /></span></span></span></span></div>

