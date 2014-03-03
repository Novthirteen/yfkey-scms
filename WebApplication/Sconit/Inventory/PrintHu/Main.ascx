<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Inventory_PrintHu_Main" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="Asn.ascx" TagName="Asn" TagPrefix="uc2" %>
<%@ Register Src="Flow.ascx" TagName="Flow" TagPrefix="uc2" %>
<%@ Register Src="Inventory.ascx" TagName="Inventory" TagPrefix="uc2" %>
<%@ Register Src="Order.ascx" TagName="Order" TagPrefix="uc2" %>
<%@ Register Src="Receipt.ascx" TagName="Receipt" TagPrefix="uc2" %>
<uc2:TabNavigator ID="ucTabNavigator" runat="server" />
    <div class="ajax__tab_body">
        <uc2:Asn ID="ucAsn" runat="server" Visible="false" />
        <uc2:Flow ID="ucFlow" runat="server" Visible="true" />
        <uc2:Inventory ID="ucInventory" runat="server" Visible="false" />
        <uc2:Order ID="ucOrder" runat="server" Visible="false" />
        <uc2:Receipt ID="ucReceipt" runat="server" Visible="false" />
    </div>
</div> 