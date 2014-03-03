<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Order_OrderHead_Main" %>
<%@ Register Src="New.ascx" TagName="New" TagPrefix="uc2" %>
<%@ Register Src="~/Order/GoodsReceipt/ViewReceipt/ViewMain.ascx" TagName="ViewReceipt" TagPrefix="uc2" %>

<uc2:New ID="ucNew" runat="server" Visible="false" />
<uc2:ViewReceipt ID="ucViewReceipt" runat="server" Visible="false" />
