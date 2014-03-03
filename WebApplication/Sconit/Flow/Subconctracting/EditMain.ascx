<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditMain.ascx.cs" Inherits="MasterData_Flow_EditMain" %>
<%@ Register Src="../TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc2" %>
<%@ Register Src="../FlowDetail/Main.ascx" TagName="Detail" TagPrefix="uc2" %>
<%@ Register Src="../FlowRouting/Main.ascx" TagName="Routing" TagPrefix="uc2" %>
<%@ Register Src="../Strategy.ascx" TagName="Strategy" TagPrefix="uc2" %>
<%@ Register Src="../FlowBinding/Main.ascx" TagName="Binding" TagPrefix="uc2" %>
<%@ Register Src="View.ascx" TagName="View" TagPrefix="uc2" %>

<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
<div class="ajax__tab_body">
    <uc2:Edit ID="ucEdit" runat="server" Visible="true" />
    <uc2:Detail ID="ucDetail" runat="server" Visible="false" />
    <uc2:Routing ID="ucRouting" runat="server" Visible="false" />
    <uc2:Strategy ID="ucStrategy" runat="server" Visible="false" />
    <uc2:Binding ID="ucBinding" runat="server" Visible="false" />
    <uc2:View ID="ucView" runat="server" Visible="false" />
</div>
</div> 