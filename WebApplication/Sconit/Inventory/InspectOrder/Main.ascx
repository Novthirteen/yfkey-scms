<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Inventory_InspectOrder_Main" %>
<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc2" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="InspectOrderInfo.ascx" TagName="InspectOrderInfo" TagPrefix="uc2" %>
<%@ Register Src="NewMain.ascx" TagName="New" TagPrefix="uc2" %>

<uc2:Search ID="ucSearch" runat="server" Visible="true" />
<uc2:List ID="ucList" runat="server" Visible="false" />
<uc2:InspectOrderInfo ID="ucInspectOrderInfo" runat="server" Visible="false" />
<uc2:New ID="ucNew" runat="server" Visible="false" />



