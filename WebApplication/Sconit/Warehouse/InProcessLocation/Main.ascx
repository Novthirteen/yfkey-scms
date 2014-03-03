<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Order_GoodsReceipt_AsnReceipt_Main" %>
<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc2" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="EditMain.ascx" TagName="EditMain" TagPrefix="uc2" %>
<%@ Register Src="ViewMain.ascx" TagName="ViewMain" TagPrefix="uc2" %>

<uc2:Search ID="ucSearch" runat="server" Visible="true" />
<uc2:List ID="ucList" runat="server" Visible="false" />
<uc2:EditMain ID="ucEditMain" runat="server" Visible="false" />
<uc2:ViewMain ID="ucViewMain" runat="server" Visible="false" />