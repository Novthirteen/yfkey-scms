<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MasterData_Client_Main" %>
<%@ Register Src="Offline/List.ascx" TagName="OfflineList" TagPrefix="uc2" %>
<%@ Register Src="Offline/Search.ascx" TagName="OfflineSearch" TagPrefix="uc2" %>
<%@ Register Src="Offline/View.ascx" TagName="OfflineView" TagPrefix="uc2" %>
<%@ Register Src="Online/List.ascx" TagName="OnlineList" TagPrefix="uc2" %>
<%@ Register Src="Online/Search.ascx" TagName="OnlineSearch" TagPrefix="uc2" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>

<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
<div class="ajax__tab_body">
    <uc2:OfflineSearch ID="ucOfflineSearch" runat="server" Visible="false" />
    <uc2:OfflineList ID="ucOfflineList" runat="server" Visible="false" />
    <uc2:OfflineView ID="ucOfflineView" runat="server" Visible="false" />
    <uc2:OnlineSearch ID="ucOnlineSearch" runat="server" Visible="True" />
    <uc2:OnlineList ID="ucOnlineList" runat="server" Visible="false" />
</div>
</div> 