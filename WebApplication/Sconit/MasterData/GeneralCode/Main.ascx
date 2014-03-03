<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MasterData_GeneralCode_Main" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="CodeMstr.ascx" TagName="CodeMstr" TagPrefix="uc2" %>
<%@ Register Src="CodeMstrList.ascx" TagName="CodeMstrList" TagPrefix="uc2" %>
<%@ Register Src="EntityOpt.ascx" TagName="EntityOpt" TagPrefix="uc2" %>

<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
<div class="ajax__tab_body">
    <uc2:EntityOpt ID="ucEntityOpt" runat="server" Visible="true" />
    <uc2:CodeMstr ID="ucCodeMstr" runat="server" Visible="false" />
    <uc2:CodeMstrList ID="ucCodeMstrList" runat="server" Visible="false" />
</div>
</div> 