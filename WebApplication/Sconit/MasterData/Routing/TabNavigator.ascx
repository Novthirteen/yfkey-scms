<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="MasterData_Routing_TabNavigator" %>

<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
    
        <span class='ajax__tab_active' id='tab_Routing' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbRouting" Text=" 　${MasterData.Routing.TabName.Routing}" runat="server" OnClick="lbRouting_Click" /></span></span></span></span><span 
        id='tab_RoutingDetail' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton  ID="lbRoutingDetail" Text="${MasterData.Routing.TabName.RoutingDetail}" runat="server" OnClick="lbRoutingDetail_Click" /></span></span></span></span>
</div>
