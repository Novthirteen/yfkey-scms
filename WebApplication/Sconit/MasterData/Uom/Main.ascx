<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MasterData_Uom_Main" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="UomConv.ascx" TagName="UomConv" TagPrefix="uc2" %>
<%@ Register Src="Uom.ascx" TagName="Uom" TagPrefix="uc2" %>

<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
<div class="ajax__tab_body">
    <uc2:Uom ID="ucUom" runat="server" Visible="true" />
    <uc2:UomConv ID="ucUomConv" runat="server" Visible="false" />
</div>
</div> 