<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="Order_OrderIssue_TabNavigator" %>
<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
        <span class='ajax__tab_active' id='tab_OrderIssue' runat="server"><span class='ajax__tab_outer'><span 
                class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbOrderIssue" Text="${Common.Business.Flow}" runat="server"
                 OnClick="lblOrderIssue_Click" /></span></span></span></span><span 
                 id='tab_PickListIssue' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
                 class='ajax__tab_tab'><asp:LinkButton ID="lbPickListIssue" Text="${Common.Business.PickList}" runat="server" OnClick="lbPickListIssue_Click" />
                 </span></span></span></span></div>

