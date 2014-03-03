<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="MasterData_Bom_TabNavigator" %>

<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
        <span class='ajax__tab_active' id='tab_bomview' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbBomView" Text="${MasterData.Bom.TabName.BomView}" runat="server" OnClick="lbBomView_Click" /></span></span></span></span><span 
        id='tab_bom' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton  ID="lbBom" Text="　${MasterData.Bom.TabName.Bom}" runat="server" OnClick="lbBom_Click" /></span></span></span></span><span 
        id='tab_bomdetail' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton  ID="lbBomDetail" Text="${MasterData.Bom.TabName.BomDetail}" runat="server" OnClick="lbBomDetail_Click" /></span></span></span></span>
     </div>

