<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditMain.ascx.cs" Inherits="Transportation_TransportPriceList_EditMain" %>

<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc2" %>
<%@ Register Src="WarehouseLease/Main.ascx" TagName="WarehouseLease" TagPrefix="uc2" %>
<%@ Register Src="Operation/Main.ascx" TagName="Operation" TagPrefix="uc2" %>
<%@ Register Src="TransportPriceListDetail/Main.ascx" TagName="TransportPriceListDetail" TagPrefix="uc2" %>

<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
<div class="ajax__tab_body">
    <uc2:Edit ID="ucEdit" runat="server" Visible="true" />
    <uc2:WarehouseLease ID="ucWarehouseLease" runat="server" Visible="false" />
    <uc2:Operation ID="ucOperation" runat="server" Visible="false" />
    <uc2:TransportPriceListDetail ID="ucTransportPriceListDetail" runat="server" Visible="false" />
</div>