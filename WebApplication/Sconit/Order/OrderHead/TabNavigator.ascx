<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="Order_OrderHead_TabNavigator" %>
<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
        <span class='ajax__tab_active' id='tab_mstr' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbMstr" Text="${MasterData.Order.OrderHead}" runat="server" OnClick="lbMstr_Click" /></span></span></span></span><span id='tab_routing' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbRouting" Text="${MasterData.Order.OrderHead.Routing}" runat="server" OnClick="lbRouting_Click" /></span></span></span></span><span id='tab_locTrans' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbLocTrans" Text="${MasterData.Order.OrderHead.LocTrans}" runat="server" OnClick="lbLocTrans_Click" /></span></span></span></span><span id='tab_actBill' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbActBill" Text="${MasterData.Order.OrderHead.ActingBill}" runat="server" OnClick="lbActBill_Click" /></span></span></span></span><span id='tab_orderBinding' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbOrderBinding" Text="${MasterData.Order.OrderHead.OrderBinding}" runat="server" OnClick="lbOrderBinding_Click" /></span></span></span></span>
    </div>
