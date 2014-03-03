<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Inventory_Stocktaking_Main" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="EditMain.ascx" TagName="EditMain" TagPrefix="uc2" %>
<%@ Register Src="Result.ascx" TagName="Result" TagPrefix="uc2" %>
<%-- 
<%@ Register Src="Import.ascx" TagName="Import" TagPrefix="uc2" %>
--%>
<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" OnlbManualClick="lbManual_Click"  OnlbResultClick="lbResult_Click"/>
<div class="ajax__tab_body">
    <uc2:EditMain ID="ucEditMain" runat="server" Visible="true" />
    <uc2:Result ID="ucResult" runat="server" Visible="false" OnBtnImportClick="ucResult_BtnResultClick" />
</div>
</div> 