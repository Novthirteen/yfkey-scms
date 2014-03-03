<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="Inventory_Stocktaking_TabNavigator" %>

<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
        <span class='ajax__tab_active' id='tab_manual' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbManual" Text="${MasterData.Inventory.Stocktaking}" runat="server" OnClick="lbManual_Click" /></span></span></span></span><span 
        id='tab_result' runat="server" visible="false"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton  ID="lbResult" Text="${MasterData.Inventory.Stocktaking.Result}" runat="server" OnClick="lbResult_Click" /></span></span></span></span>
     </div>

