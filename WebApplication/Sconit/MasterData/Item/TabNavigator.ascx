<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="MasterData_Item_TabNavigator" %>
        
<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
    
        <span class='ajax__tab_active' id='tab_Item' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbItem" Text="${MasterData.Item}" runat="server" OnClick="lbItem_Click" /></span></span></span></span><span 
        id='tab_ItemRef' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbItemRef" Text="${MasterData.ItemRef}" runat="server" OnClick="lbItemRef_Click" /></span></span></span></span><span 
        id='tab_ItemKit' runat="server" visible="false"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbItemKit" Text="${MasterData.ItemKit}" runat="server" OnClick="lbItemKit_Click" /></span></span></span></span>
    </div>
<div class="ajax__tab_body">