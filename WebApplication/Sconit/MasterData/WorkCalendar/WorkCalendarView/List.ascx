<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MasterData_WorkCalendar_WorkCalendarView_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Date" HeaderText="${MasterData.WorkCalendar.Date}" SortExpression="Date"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="DayOfWeek" HeaderText="${MasterData.WorkCalendar.Workday.Week}"
                    SortExpression="DayOfWeek" />
                <asp:BoundField DataField="ShiftName" HeaderText="${MasterData.WorkCalendar.Shift.ShiftName}"
                    SortExpression="ShiftName" />
                <asp:BoundField DataField="StartTime" HeaderText="${MasterData.WorkCalendar.StartTime}"
                    SortExpression="StartTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:BoundField DataField="EndTime" HeaderText="${MasterData.WorkCalendar.EndTime}"
                    SortExpression="EndTime" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:BoundField DataField="Type" HeaderText="${MasterData.WorkCalendar.SpecialTime.Type}"
                    SortExpression="Type" />
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
