<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="Security_TabNavigator" %>
        
<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
        <span class='ajax__tab_active' id='tab_EntityOpt' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbEntityOpt" Text="企业选项" runat="server" OnClick="lbEntityOpt_Click" /></span></span></span></span><span 
        id='tab_CodeMstr' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbCodeMstr" Text="通用代码" runat="server" OnClick="lbCodeMstr_Click" /></span></span></span></span>
    </div>
