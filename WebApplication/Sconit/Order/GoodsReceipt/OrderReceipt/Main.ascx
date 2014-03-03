<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Order_GoodsReceipt_OrderReceipt_Main" %>
<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc2" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="ReceiptItemList.ascx" TagName="ReceiveMain" TagPrefix="uc2" %>
<%@ Register Src="ReceiveMain.ascx" TagName="ProductionReceiveMain" TagPrefix="uc2" %>
<%@ Register Src="~/Order/GoodsReceipt/ViewReceipt/ViewMain.ascx" TagName="ReceiptView"
    TagPrefix="uc2" %>
    
<uc2:Search ID="ucSearch" runat="server" Visible="true" />
<uc2:List ID="ucList" runat="server" Visible="false" />
<uc2:ReceiveMain ID="ucReceiveMain" runat="server" Visible="false" />
<uc2:ProductionReceiveMain ID="ucProductionReceiveMain" runat="server" Visible="false" />
<uc2:ReceiptView ID="ucReceiptView" runat="server" Visible="false" />
