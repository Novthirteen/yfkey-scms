<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="Security_TabNavigator" %>

        
<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
        <span class='ajax__tab_active' id='tab_Basic' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbBasic" Text="${Security.UserPreference.Basic}" runat="server" OnClick="lbBasic_Click" /></span></span></span></span><span id='tab_Theme' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbTheme" Text="${Security.UserPreference.Theme}" runat="server" OnClick="lbTheme_Click" /></span></span></span></span><span id='tab_NamedQuery' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbNamedQuery" Text="${Security.UserPreference.NamedQuery}" runat="server" OnClick="lbNamedQuery_Click" /></span></span></span></span><!--<span id='tab_ScFav' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbScFav" Text="首选语言" runat="server" OnClick="lbScFav_Click" /></span></span></span></span>-->
    </div>
