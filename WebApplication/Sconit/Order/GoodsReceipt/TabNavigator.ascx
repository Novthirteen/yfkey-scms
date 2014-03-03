<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="Order_GoodsReceipt_TabNavigator" %>
<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
        <span class='ajax__tab_active' id='tab_order' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbOrder" Text="${MasterData.Order.GoodsReceipt.Order}" runat="server" OnClick="lbOrder_Click" /></span></span></span></span><span id='tab_asn' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbAsn" Text="${MasterData.Order.GoodsReceipt.ASN}" runat="server" OnClick="lbAsn_Click" /></span></span></span></span>
    </div>
