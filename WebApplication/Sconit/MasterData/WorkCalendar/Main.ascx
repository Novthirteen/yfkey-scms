<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MasterData_WorkCalendar_Main" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="./WorkCalendarView/Main.ascx" TagName="WorkCalendarViewMain" TagPrefix="uc2" %>
<%@ Register Src="./Workday/Main.ascx" TagName="WorkdayMain" TagPrefix="uc2" %>
<%@ Register Src="./Shift/Main.ascx" TagName="ShiftMain" TagPrefix="uc2" %>
<%@ Register Src="./SpecialTime/Main.ascx" TagName="SpecialTimeMain" TagPrefix="uc2" %>

<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
<div class="ajax__tab_body">
    <uc2:WorkCalendarViewMain ID="ucWorkCalendarViewMain" runat="server" Visible="false" />
    <uc2:WorkdayMain ID="ucWorkdayMain" runat="server" Visible="false" />
    <uc2:ShiftMain ID="ucShiftMain" runat="server" Visible="false" />
    <uc2:SpecialTimeMain ID="ucSpecialTimeMain" runat="server" Visible="false" />
</div>
</div>
