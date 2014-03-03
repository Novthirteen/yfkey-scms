<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="Mes_Shelf_TabNavigator" %>

<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
    
        <span class='ajax__tab_active' id='tab_Shelf' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbShelf" Text="${Mes.Shelf.TabName.Shelf}" runat="server" OnClick="lbShelf_Click" /></span></span></span></span><span 
        id='tab_ShelfItem' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton  ID="lbShelfDetail" Text="${Mes.Shelf.TabName.ShelfItem}" runat="server" OnClick="lbShelfItem_Click" /></span></span></span></span>
</div>
