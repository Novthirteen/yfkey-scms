<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="Visualization_GoodsTraceability_TabNavigator" %>
<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
        <span class='ajax__tab_active' id='tab_HuSearch' runat="server"><span class='ajax__tab_outer'><span 
                class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbHuSearch" Text="${Visualization.GoodsTraceability.HuSearch}" runat="server"
                 OnClick="lbHuSearch_Click" /></span></span></span></span><span 
                 id='tab_Traceability' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
                 class='ajax__tab_tab'><asp:LinkButton ID="lbTraceability" Text="${Visualization.GoodsTraceability.Traceability}" runat="server" OnClick="lbTraceability_Click" />
                 </span></span></span></span></div>

