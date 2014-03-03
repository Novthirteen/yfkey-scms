<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="MRP_TabNavigator" %>

<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
        <span  class='ajax__tab_active' id='tab_planning' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton  ID="lbPlanning" Text="${Common.Business.Manual}" runat="server" OnClick="lbPlanning_Click" /></span></span></span></span><span 
        id='tab_import' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton  ID="lbImport" Text="${MRP.TabName.Import}" runat="server" OnClick="lbImport_Click" /></span></span></span></span>
     </div>

