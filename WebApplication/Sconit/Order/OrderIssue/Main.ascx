<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Distribution_OrderIssue_Main" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="EditMain.ascx" TagName="OrderIssue" TagPrefix="uc2" %>
<%@ Register Src="~/PickList/Main.ascx" TagName="PickListIssue" TagPrefix="uc2" %>

<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="false" />
<div class="ajax__tab_body">
    <uc2:OrderIssue ID="ucOrderIssue" runat="server" Visible="true" />
    <uc2:PickListIssue ID="ucPickListIssue" runat="server" Visible="false" />
</div>
</div> 