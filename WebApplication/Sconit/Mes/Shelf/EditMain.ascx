<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditMain.ascx.cs" Inherits="Mes_Shelf_EditMain" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="PriceListMain" TagPrefix="uc2" %>
<%@ Register Src="ShelfItem/Main.ascx" TagName="ShelfItemMain" TagPrefix="uc2" %>


<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
<div class="ajax__tab_body">
    <uc2:PriceListMain ID="ucShelfMain" runat="server" Visible="false" />
    <uc2:ShelfItemMain ID="ucShelfItemMain" runat="server" Visible="false" />
</div>
</div>