<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MasterData_Routing_Main" %>
<%@ Register Src="Routing.ascx" TagName="Routing" TagPrefix="uc2" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="Routing.ascx" TagName="ReturnRouting" TagPrefix="uc2" %>
<%@ Register Src="List.ascx" TagName="ReturnRoutingList" TagPrefix="uc2" %>


<fieldset>
<legend id="lRouting" runat="server"></legend>
<uc2:Routing ID="ucRouting" runat="server" Visible="true" />
<uc2:List ID="ucList" runat="server" Visible="false" />
</fieldset>
<fieldset id="fdReturn"  visible="false" runat="server">
<legend>${MasterData.Flow.ReturnRouting}</legend>
<uc2:ReturnRouting ID="ucReturnRouting" runat="server" Visible="false" />
<uc2:ReturnRoutingList ID="ucReturnRoutingList" runat="server" Visible="false" />
</fieldset>