<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditMain.ascx.cs" Inherits="MasterData_PriceList_PriceList_EditMain" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="PriceListMain" TagPrefix="uc2" %>
<%@ Register Src="PirceListDetail/Main.ascx" TagName="PriceListDetailMain" TagPrefix="uc2" %>


<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
<div class="ajax__tab_body">
    <uc2:PriceListMain ID="ucPriceListMain" runat="server" Visible="false" />
    <uc2:PriceListDetailMain ID="ucPriceListDetailMain" runat="server" Visible="false" />
</div>
</div>