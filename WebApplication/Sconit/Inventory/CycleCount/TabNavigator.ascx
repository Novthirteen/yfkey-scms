<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="Inventory_CycleCount_TabNavigator" %>

<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
        <span class='ajax__tab_active' id='tab_manual' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbManual" Text="${Common.Business.Manual}" runat="server" OnClick="lbManual_Click" /></span></span></span></span><span 
        id='tab_import' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton  ID="lbImport" Text="${Common.Business.Import}" runat="server" OnClick="lbImport_Click" /></span></span></span></span>
     </div>

