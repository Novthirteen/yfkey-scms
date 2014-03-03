<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="Production_Feed_TabNavigator" %>
        
<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
        <span class='ajax__tab_active' id='tab_hu' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbCreateByHu" Text="${MasterData.InspectOrder.CreateByHu}" runat="server" OnClick="lbCreateByHu_Click" /></span></span></span></span><span id='tab_qty' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbCreateByQty" Text="${MasterData.InspectOrder.CreateByQty}" runat="server" OnClick="lbCreateByQty_Click" /></span></span></span></span>
    </div>
