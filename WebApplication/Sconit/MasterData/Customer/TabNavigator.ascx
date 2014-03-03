<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs"
    Inherits="MasterData_Customer_TabNavigator" %>
<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
    
        <span class='ajax__tab_active' id='tab_customer' runat="server"><span class='ajax__tab_outer'><span 
        class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbCustomer" 
        Text="${MasterData.Customer.Customer}" runat="server" OnClick="lbCustomer_Click" /></span></span></span></span><span 
        id='tab_workcenter' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbWorkCenter" Text="${MasterData.WorkCenter.WorkCenter}" runat="server"
        OnClick="lbWorkCenter_Click" /></span></span></span></span><span 
        id='tab_billaddress' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbBillAddress" Text="${MasterData.Address.BillAddress}" runat="server"
        OnClick="lbBillAddress_Click" /></span></span></span></span><span 
        id='tab_shipaddress' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbShipAddress" Text="${MasterData.Address.ShipAddress}" runat="server"
        OnClick="lbShipAddress_Click" /></span></span></span></span>
    </div>
