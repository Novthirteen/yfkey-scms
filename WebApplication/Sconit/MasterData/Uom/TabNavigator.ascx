<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="MasterData_Uom_TabNavigator" %>
        
<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
        <span class='ajax__tab_active' id='tab_Uom' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbUom" Text="计量单位" runat="server" OnClick="lbUom_Click" /></span></span></span></span><span 
        id='tab_UomConv' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbUomConv" Text="计量单位转换" runat="server" OnClick="lbUomConv_Click" /></span></span></span></span>
    </div>
