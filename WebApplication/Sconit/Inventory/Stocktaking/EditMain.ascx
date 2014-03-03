<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditMain.ascx.cs" Inherits="Inventory_Stocktaking_EditMain" %>
<%@ Register Src="Search.ascx" TagName="Search" TagPrefix="uc2" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="New.ascx" TagName="New" TagPrefix="uc2" %>
<%@ Register Src="Business.ascx" TagName="Business" TagPrefix="uc2" %>

<uc2:Search ID="ucSearch" runat="server" Visible="true" />
<uc2:List ID="ucList" runat="server" Visible="false" OnEditEvent="ListEdit_Render" />
<uc2:New ID="ucNew" runat="server" Visible="false" />
<uc2:Business ID="ucBusiness" runat="server" Visible="false" />