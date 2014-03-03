<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="ManageSconit_LeanEngine_TabNavigator" %>

<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
        <span class='ajax__tab_active' id='tab_view' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbView" Text="${LeanEngine.TabName.View}" runat="server" OnClick="lbView_Click" /></span></span></span></span><span 
        id='tab_setup' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton  ID="lbSetup" Text="${LeanEngine.TabName.Setup}" runat="server" OnClick="lbSetup_Click" /></span></span></span></span>
     </div>

