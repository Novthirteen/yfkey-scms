﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="NewMrp_PurchasePlan_Main" %>
<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc2" %>
<%@ Register Src="List.ascx"   TagName="List"   TagPrefix="uc2" %>
<%@ Register Src="DetailList.ascx"   TagName="DetailList"   TagPrefix="uc2" %>

<uc2:Search ID="ucSearch" runat="server" Visible="true" />
<uc2:List ID="ucList" runat="server" Visible="false" />
<uc2:DetailList ID="ucDetailList" runat="server" Visible="false" />


