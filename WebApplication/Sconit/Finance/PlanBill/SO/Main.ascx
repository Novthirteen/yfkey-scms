<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Finance_PlanBill_Main" %>
<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc" %>
<%@ Register Src="Match.ascx" TagName="Match" TagPrefix="uc" %>

<uc:Search ID="ucSearch" runat="server" Visible="true" OnMatchClick="ucSearch_MatchClick" />
<uc:List ID="ucList" runat="server" Visible="false" />
<uc:Match ID="ucMatch" runat="server" Visible="false" OnSaved="ucMatch_Saved" />
