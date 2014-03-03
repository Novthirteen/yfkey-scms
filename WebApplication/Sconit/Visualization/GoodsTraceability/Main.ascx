<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Visualization_GoodsTraceability_Main" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="~/Visualization/GoodsTraceability/HuSearch/Main.ascx" TagName="HuSearchMain" TagPrefix="uc2" %>
<%@ Register Src="~/Visualization/GoodsTraceability/Traceability/Main.ascx" TagName="TraceabilityMain" TagPrefix="uc2" %>

<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
<div class="ajax__tab_body">
    <uc2:HuSearchMain ID="ucHuSearchMain" runat="server" Visible="true" />
    <uc2:TraceabilityMain ID="ucTraceabilityMain" runat="server" Visible="false" />
</div>
</div>