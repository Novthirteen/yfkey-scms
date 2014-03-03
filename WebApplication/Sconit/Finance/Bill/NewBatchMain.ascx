<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewBatchMain.ascx.cs" Inherits="Finance_Bill_NewBatchMain" %>
<%@ Register Src="NewBatchSearch.ascx" TagName="Search" TagPrefix="uc2" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc" %>

<uc2:Search ID="ucSearch" runat="server" />
<uc2:List ID="ucList" runat="server" Visible="false" />
<uc:Edit ID="ucEdit" runat="server" Visible="false" />