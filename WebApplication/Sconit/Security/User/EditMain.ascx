<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditMain.ascx.cs" Inherits="MasterData_User_EditMain" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc2" %>
<%@ Register Src="UserRole.ascx" TagName="UserRole" TagPrefix="uc2" %>
<%@ Register Src="UserPermission.ascx" TagName="UserPermission" TagPrefix="uc2" %>
<%@ Register Src="UserPermissionList.ascx" TagName="UserPermissionList" TagPrefix="uc2" %>

<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
<div class="ajax__tab_body">
    <uc2:Edit ID="ucEdit" runat="server" Visible="true" />
    <uc2:UserRole ID="ucUserRole" runat="server" Visible="false" />
    <uc2:UserPermission ID="ucUserPermission" runat="server" Visible="false" />
    <uc2:UserPermissionList ID="ucUserPermissionList" runat="server" Visible="false" />
</div>
</div> 