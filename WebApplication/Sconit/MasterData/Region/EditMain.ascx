<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditMain.ascx.cs" Inherits="MasterData_Region_EditMain" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc2" %>
<%@ Register Src="../WorkCenter/Main.ascx" TagName="WorkCenter" TagPrefix="uc2" %>
<%@ Register Src="../Address/Main.ascx" TagName="BillAddress" TagPrefix="uc2" %>
<%@ Register Src="../Address/Main.ascx" TagName="ShipAddress" TagPrefix="uc2" %>

<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
    <div class="ajax__tab_body">
        <uc2:Edit ID="ucEdit" runat="server" Visible="true" />
        <uc2:WorkCenter ID="ucWorkCenter" runat="server" Visible="false" />
        <uc2:BillAddress ID="ucBillAddress" runat="server" Visible="false" />
         <uc2:ShipAddress ID="ucShipAddress" runat="server" Visible="false" />
    </div>
</div>
