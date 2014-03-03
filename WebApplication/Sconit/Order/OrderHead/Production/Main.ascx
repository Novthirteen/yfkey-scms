<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Order_OrderHead_Main" %>
<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc2" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="New.ascx" TagName="New" TagPrefix="uc2" %>
<%@ Register Src="ScrapNew.ascx" TagName="ScrapNew" TagPrefix="uc2" %>
<%@ Register Src="EditMain.ascx" TagName="Edit" TagPrefix="uc2" %>
<%@ Register Src="~/Order/GoodsReceipt/ViewReceipt/ViewMain.ascx" TagName="ViewReceipt" TagPrefix="uc2" %>
<%@ Register Src="~/MRP/ShiftPlan/Import/Main.ascx" TagName="Import" TagPrefix="uc2" %>

<uc2:Search ID="ucSearch" runat="server" Visible="true" OnBtnImportClick="ucSearch_BtnImportClick" />
<uc2:List ID="ucList" runat="server" Visible="false" />
<uc2:New ID="ucNew" runat="server" Visible="false" />
<uc2:ScrapNew ID="ucScrapNew" runat="server" Visible="false" />
<uc2:Edit ID="ucEdit" runat="server" Visible="false" />
<uc2:ViewReceipt ID="ucViewReceipt" runat="server" Visible="false" />
<uc2:Import ID="ucImport" runat="server" Visible="false" OnBtnBackClick="ucImport_BtnBackClick" />
