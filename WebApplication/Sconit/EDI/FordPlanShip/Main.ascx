<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="EDI_FordPlanShip_Main" %>
<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc2" %>
<%@ Register Src="List.ascx"   TagName="List"   TagPrefix="uc2" %>
<%@ Register Src="ShipList.ascx"   TagName="ShipList"   TagPrefix="uc2" %>

<uc2:Search ID="ucSearch" runat="server" Visible="true" />
<uc2:List ID="ucList" runat="server" Visible="false" />
<uc2:ShipList ID="ucShipList" runat="server" Visible="false" />

