<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewMain.ascx.cs" Inherits="Production_Feed_NewMain" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="NewHu.ascx" TagName="NewHu" TagPrefix="uc2" %>
<%@ Register Src="NewQty.ascx" TagName="NewQty" TagPrefix="uc2" %>

<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
<div class="ajax__tab_body">
    <uc2:NewHu ID="ucNewHu" runat="server" Visible="true" />
    <uc2:NewQty ID="ucNewQty" runat="server"  Visible="false" />
</div>
</div> 