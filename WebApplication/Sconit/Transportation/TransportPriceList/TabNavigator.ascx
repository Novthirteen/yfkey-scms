<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="Transportation_TransportPriceList_TabNavigator" %>

<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
    
        <span class='ajax__tab_active' id='tab_TransportPriceList' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbTransportPriceList" Text="${Transportation.TransportPriceList.TransportPriceList}" runat="server" 
        OnClick="lbTransportPriceList_Click" /></span></span></span></span><span 
        id='tab_WarehouseLease' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbWarehouseLease" Text="${Transportation.TransportPriceList.WarehouseLease}" runat="server" 
        OnClick="lbWarehouseLease_Click" /></span></span></span></span><span 
        id='tab_Operation' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbOperation" Text="${Transportation.TransportPriceList.Operation}" runat="server" 
        OnClick="lbOperation_Click" /></span></span></span></span><span 
        id='tab_TransportPriceListDetail' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbTransportPriceListDetail" Text="${Transportation.TransportPriceList.TransportPriceListDetail}" runat="server" 
        OnClick="lbTransportPriceListDetail_Click" /></span></span></span></span>
    </div>
