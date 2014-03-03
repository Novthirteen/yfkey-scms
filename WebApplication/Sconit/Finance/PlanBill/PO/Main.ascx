<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Finance_PlanBill_Main" %>
<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc" %>

<uc:Search ID="ucSearch" runat="server" Visible="true" />
<uc:List ID="ucList" runat="server" Visible="false" />
