<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Finance_Bill_Main" %>
<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc" %>

<uc:Search ID="ucSearch" runat="server" Visible="true" />
<uc:List ID="ucList" runat="server" Visible="false" />
<uc:Edit ID="ucEdit" runat="server" Visible="false" />
