<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditMain.ascx.cs" Inherits="MasterData_Item_EditMain" %>

<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc2" %>
<%@ Register Src="ItemKit/Main.ascx" TagName="Kit" TagPrefix="uc2" %>
<%@ Register Src="ItemKit/New.ascx" TagName="ItemKitNew" TagPrefix="uc2" %>
<%@ Register Src="ItemKit/Edit.ascx" TagName="ItemKitEdit" TagPrefix="uc2" %>
<%@ Register Src="ItemRef/Main.ascx" TagName="ItemRef" TagPrefix="uc2" %>

    <uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
    <uc2:Edit ID="ucEdit" runat="server" Visible="true" />
    <uc2:Kit ID="ucItemKit" runat="server" Visible="false" />
    <uc2:ItemKitNew ID="ucItemKitNew" runat="server" Visible="false" />
    <uc2:ItemKitEdit ID="ucItemKitEdit" runat="server" Visible="false" />
    <uc2:ItemRef ID="ucItemRef" runat="server" Visible="false" />
    
</div>
</div> 