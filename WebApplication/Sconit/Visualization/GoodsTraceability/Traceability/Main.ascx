<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Visualization_Traceability_Main" %>
<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc2" %>
<%--<%@ Register Src="InvList.ascx" TagName="InvList" TagPrefix="uc2" %>--%>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc2" %>

<uc2:Search ID="ucSearch" runat="server" Visible="true" />
<uc2:Edit ID="ucEdit" runat="server" Visible="false" />
<%--<uc2:InvList ID="ucInvList" runat="server" Visible="false" />--%>
<uc2:List ID="ucList" runat="server" Visible="false" />
