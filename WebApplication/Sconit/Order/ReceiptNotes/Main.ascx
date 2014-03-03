<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Order_ReceiptNotes_Main" %>
<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc2" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="~/Order/GoodsReceipt/ViewReceipt/ViewMain.ascx" TagName="ViewMain" TagPrefix="uc2" %>
<%@ Register Src="~/Order/GoodsReceipt/AdjustReceipt/AdjustMain.ascx" TagName="AdjustMain" TagPrefix="uc2" %>

<uc2:Search ID="ucSearch" runat="server" Visible="true" />
<uc2:List ID="ucList" runat="server" Visible="false" />
<uc2:ViewMain ID="ucViewMain" runat="server" Visible="false" />
<uc2:AdjustMain ID="ucAdjustMain" runat="server" Visible="false" />