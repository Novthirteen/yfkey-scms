<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditMain.ascx.cs" Inherits="Order_OrderIssue_EditMain" %>
<%@ Register Src="Search.ascx" TagName="New" TagPrefix="uc2" %>
<%@ Register Src="FlowInfo.ascx" TagName="FlowInfo" TagPrefix="uc2" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="SupplierList.ascx" TagName="SupplierList" TagPrefix="uc2" %>
<%@ Register Src="ShipItemList.ascx" TagName="DetailMain" TagPrefix="uc2" %>
<%@ Register Src="~/Warehouse/InProcessLocation/ViewMain.ascx" TagName="ViewShipList"
    TagPrefix="uc2" %>
<%@ Register Src="~/PickList/PickListInfo.ascx" TagName="PickListInfo" TagPrefix="uc2" %>
<uc2:New ID="ucSearch" runat="server" Visible="true" />
<uc2:FlowInfo ID="ucFlowInfo" runat="server" Visible="false" />
<uc2:List ID="ucList" runat="server" Visible="false" />
<uc2:SupplierList ID="ucSupplierList" runat="server" Visible="false" />
<uc2:DetailMain ID="ucDetailMain" runat="server" Visible="false" />
<uc2:PickListInfo ID="ucPickListInfo" runat="server" Visible="false" />
<uc2:ViewShipList ID="ucViewShipList" runat="server" Visible="false" />
