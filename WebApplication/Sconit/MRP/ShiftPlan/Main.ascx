<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MRP_ShiftPlan_Main" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="~/MRP/ShiftPlan/Manual/Main.ascx" TagName="Manual" TagPrefix="uc2" %>
<%@ Register Src="~/MRP/ShiftPlan/Import/Main.ascx" TagName="Import" TagPrefix="uc2" %>

<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
<div class="ajax__tab_body">
    <uc2:Manual ID="ucManual" runat="server" Visible="true" />
    <uc2:Import ID="ucImport" runat="server" Visible="false" />
</div>
</div>
