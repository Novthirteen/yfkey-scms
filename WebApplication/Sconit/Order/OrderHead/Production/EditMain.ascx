<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditMain.ascx.cs" Inherits="Order_OrderHead_EditMain" %>
<%@ Register Src="~/Order/OrderHead/TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc2" %>
<%@ Register Src="~/Order/OrderRouting/View.ascx" TagName="Routing" TagPrefix="uc2" %>
<%@ Register Src="~/Order/OrderView/ActBillMain.ascx" TagName="ActBillView" TagPrefix="uc2" %>
<%@ Register Src="~/Order/OrderView/LocTransMain.ascx" TagName="LocTransView" TagPrefix="uc2" %>
<%@ Register Src="~/Order/OrderHead/ListBinding.ascx" TagName="OrderBinding" TagPrefix="uc2" %>

<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
    <div class="ajax__tab_body">
        <uc2:Edit ID="ucEdit" runat="server" Visible="true" />
        <uc2:Routing ID="ucRouting" runat="server" Visible="false" />
        <uc2:ActBillView ID="ucActBillView" runat="server" Visible="false" />
        <uc2:LocTransView ID="ucLocTransView" runat="server" Visible="false" />
        <uc2:OrderBinding ID="ucOrderBinding" runat="server" Visible="false" />
    </div>
</div>