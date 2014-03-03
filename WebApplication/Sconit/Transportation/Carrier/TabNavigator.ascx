<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="Transportation_Carrier_TabNavigator" %>

<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
    
        <span class='ajax__tab_active' id='tab_carrier' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbCarrier" Text="${Transportation.Carrier.Carrier}" runat="server" 
        OnClick="lbCarrier_Click" /></span></span></span></span><span 
        id='tab_billaddress' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbBillAddress" Text="${MasterData.Address.BillAddress}" runat="server" 
        OnClick="lbBillAddress_Click" /></span></span></span></span>
    </div>
