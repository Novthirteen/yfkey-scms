<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewMain.ascx.cs" Inherits="Finance_Bill_NewMain" %>
<%@ Register Src="NewTabNavigator.ascx" TagName="NewTabNavigator" TagPrefix="uc" %>
<%@ Register Src="NewSearch.ascx" TagName="NewSearch" TagPrefix="uc" %>
<%@ Register Src="NewBatchMain.ascx" TagName="NewBatchSearch" TagPrefix="uc" %>
<%@ Register Src="NewRecalculateSearch.ascx" TagName="NewRecalculateSearch" TagPrefix="uc" %>

<uc:NewTabNavigator ID="ucNewTabNavigator" runat="server" Visible="true" />
    <div class="ajax__tab_body">
        <uc:NewSearch ID="ucNewSearch" runat="server" Visible="true" />
        <uc:NewBatchSearch ID="ucNewBatchSearch" runat="server" Visible="false" />
        <uc:NewRecalculateSearch ID="ucNewRecalculateSearch" runat="server" Visible="false" />
    </div>
</div> 