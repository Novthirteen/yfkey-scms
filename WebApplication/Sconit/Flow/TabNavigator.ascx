<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="MasterData_Flow_TabNavigator" %>

        
<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
    
        <span class='ajax__tab_active' id='tab_mstr' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbMstr" Text="" runat="server" OnClick="lbMstr_Click" /></span></span></span></span><span id='tab_strategy' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbStrategy" Text="" runat="server" OnClick="lbStrategy_Click" /></span></span></span></span><span id='tab_detail' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbDetail" Text="" runat="server" OnClick="lbDetail_Click" /></span></span></span></span><span id='tab_routing' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbRouting" Text="" runat="server" OnClick="lbRouting_Click" /></span></span></span></span><span id='tab_binding' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbBinding" Text="${MasterData.Flow.Binding}" runat="server" OnClick="lbBinding_Click" /></span></span></span></span><span id='tab_view' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbView" Text="${MasterData.Flow.View}" runat="server" OnClick="lbView_Click" /></span></span></span></span>
    </div>
