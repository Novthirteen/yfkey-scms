<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs"
    Inherits="Security_Role_TabNavigator" %>
<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
    
        <span class='ajax__tab_active' id='tab_region' runat="server"><span class='ajax__tab_outer'><span 
        class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbRole" 
        Text="${Security.Role}" runat="server" OnClick="lbRole_Click" /></span></span></span></span><span 
        id='tab_userrole' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbRoleUser" Text="${Security.Role.RoleUser}" runat="server"
        OnClick="lbRoleUser_Click" /></span></span></span></span><span 
        id='tab_userpermission' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbRolePermission" Text="${Security.Role.RoleAuthorization}" runat="server"
        OnClick="lbRolePermission_Click" /></span></span></span></span><span 
        id='tab_userpermissionlist' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbRolePeremissionList" Text="${Security.Role.RolePermission}" runat="server"
        OnClick="lbRolePermissionList_Click" /></span></span></span></span>
    </div>

