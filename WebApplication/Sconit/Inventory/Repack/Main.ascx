<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Inventory_Repack_Main" %>
<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc2" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="RepackInfo.ascx" TagName="RepackInfo" TagPrefix="uc2" %>
<%@ Register Src="RepackDetailList.ascx" TagName="RepackDetailList" TagPrefix="uc2" %>
<%@ Register Src="ViewRepackDetailList.ascx" TagName="ViewRepackDetailList" TagPrefix="uc2" %>

<uc2:Search ID="ucSearch" runat="server" Visible="true" />
<uc2:List ID="ucList" runat="server" Visible="false" />
<uc2:RepackInfo ID="ucRepackInfo" runat="server" Visible="false" />
<uc2:RepackDetailList ID="ucRepackDetailList" runat="server" Visible="false" />
<uc2:ViewRepackDetailList ID="ucViewRepackDetailList" runat="server" Visible="false" />
