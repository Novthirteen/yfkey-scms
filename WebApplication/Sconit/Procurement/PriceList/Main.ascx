<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MasterData_PriceList_PriceList_Main" %>
<%@ Register Src="~/MasterData/PriceList/Search.ascx" TagName="Search" TagPrefix="uc2" %>
<%@ Register Src="~/MasterData/PriceList/List.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="~/MasterData/PriceList/New.ascx" TagName="New" TagPrefix="uc2" %>
<%@ Register Src="~/MasterData/PriceList/EditMain.ascx" TagName="Edit" TagPrefix="uc2" %>

<uc2:Search ID="ucSearch" runat="server" Visible="true" />
<uc2:List ID="ucList" runat="server" Visible="false" />
<uc2:New ID="ucNew" runat="server" Visible="false" />
<uc2:Edit ID="ucEdit" runat="server" Visible="false" />