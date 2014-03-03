<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Production_Feed_Main" %>
<%@ Register Src="New.ascx" TagName="New" TagPrefix="uc2" %>
<%@ Register Src="~/Inventory/InspectOrder/InspectOrderInfo.ascx" TagName="InspectOrderInfo" TagPrefix="uc2" %>

<uc2:New ID="ucNew" runat="server" Visible="true" />
<uc2:InspectOrderInfo ID="ucInspectOrderInfo" runat="server" Visible="false" />


