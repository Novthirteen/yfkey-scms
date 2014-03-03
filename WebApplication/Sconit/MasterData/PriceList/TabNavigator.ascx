<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="MasterData_PriceList_TabNavigator" %>

<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
    
        <span class='ajax__tab_active' id='tab_PriceList' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbPriceList" Text=" 　${MasterData.PriceList.TabName.PriceList}" runat="server" OnClick="lbPriceList_Click" /></span></span></span></span><span 
        id='tab_PriceListdetail' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton  ID="lbPriceListDetail" Text="${MasterData.PriceList.TabName.PriceListDetail}" runat="server" OnClick="lbPriceListDetail_Click" /></span></span></span></span>
</div>
