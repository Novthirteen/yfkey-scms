<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MasterData_WorkCalendar_Shift_Main" %>
<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc2" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="New.ascx" TagName="New" TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc2" %>
<%@ Register Src="~/MasterData/WorkCalendar/ShiftDetail/List.ascx" TagName="DetailList"
    TagPrefix="uc2" %>
<%@ Register Src="~/MasterData/WorkCalendar/ShiftDetail/Edit.ascx" TagName="DetailEdit"
    TagPrefix="uc2" %>
<%@ Register Src="~/MasterData/WorkCalendar/ShiftDetail/New.ascx" TagName="DetailNew"
    TagPrefix="uc2" %>
<uc2:Search ID="ucSearch" runat="server" Visible="true" />
<uc2:List ID="ucList" runat="server" Visible="false" />
<uc2:New ID="ucNew" runat="server" Visible="false" />
<uc2:Edit ID="ucEdit" runat="server" Visible="false" />
<uc2:DetailList ID="ucDetailList" runat="server" Visible="false" />
<div id="div1" runat="server" visible="false">
    <div id="floatdiv">
        <uc2:DetailEdit ID="ucDetailEdit" runat="server" Visible="false" />
        <uc2:DetailNew ID="ucDetailNew" runat="server" Visible="false" />
    </div>
</div>
