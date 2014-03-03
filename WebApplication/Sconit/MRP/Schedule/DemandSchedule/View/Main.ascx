<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MRP_Schedule_DemandSchedule_Main" %>

<%@ Register Src="ActQtyList.ascx" TagName="ActQty" TagPrefix="uc2" %>
<%@ Register Src="RequiredQtyList.ascx" TagName="RequiredQty" TagPrefix="uc2" %>


<uc2:ActQty ID="ucActQty" runat="server" Visible="true" />
<uc2:RequiredQty ID="ucRequiredQty" runat="server" Visible="false" />