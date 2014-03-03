<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditMain.ascx.cs" Inherits="MasterData_Routing_EditMain" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="RoutingMain" TagPrefix="uc2" %>
<%@ Register Src="RoutingDetail/Main.ascx" TagName="RoutingDetailMain" TagPrefix="uc2" %>


<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
<div class="ajax__tab_body">
    <uc2:RoutingMain ID="ucRoutingMain" runat="server" Visible="false" />
    <uc2:RoutingDetailMain ID="ucRoutingDetailMain" runat="server" Visible="false" />
</div>
</div>