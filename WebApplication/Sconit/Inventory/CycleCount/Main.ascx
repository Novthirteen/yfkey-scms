<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Inventory_CycleCount_Main" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="EditMain.ascx" TagName="EditMain" TagPrefix="uc2" %>
<%@ Register Src="Import.ascx" TagName="Import" TagPrefix="uc2" %>

<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" OnlbManualClick="lbManual_Click"  OnlbImportClick="lbImport_Click"/>
<div class="ajax__tab_body">
    <uc2:EditMain ID="ucEditMain" runat="server" Visible="true" />
    <uc2:Import ID="ucImport" runat="server" Visible="false" OnBtnImportClick="ucImport_BtnImportClick" />
</div>
</div> 