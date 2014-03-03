<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditMain.ascx.cs" Inherits="Security_Role_EditMain" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc2" %>
<%@ Register Src="RoleUser.ascx" TagName="RoleUser" TagPrefix="uc2" %>
<%@ Register Src="RolePermission.ascx" TagName="RolePermission" TagPrefix="uc2" %>
<%@ Register Src="RolePermissionList.ascx" TagName="RolePermissionList" TagPrefix="uc2" %>

<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
<div class="ajax__tab_body">
    <uc2:Edit ID="ucEdit" runat="server" Visible="true" />
    <uc2:RoleUser ID="ucRoleUser" runat="server" Visible="false" />
    <uc2:RolePermission ID="ucRolePermission" runat="server" Visible="false" />
    <uc2:RolePermissionList ID="ucRolePermissionList" runat="server" Visible="false" />
</div>
</div> 