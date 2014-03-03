<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs"
    Inherits="MasterData_User_TabNavigator" %>
<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
    
        <span class='ajax__tab_active' id='tab_region' runat="server"><span class='ajax__tab_outer'><span 
        class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbUser" 
        Text="${Security.User.User}" runat="server" OnClick="lbUser_Click" /></span></span></span></span><span 
        id='tab_userrole' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbUserRole" Text="${Security.User.UserRole}" runat="server"
        OnClick="lbUserRole_Click" /></span></span></span></span><span 
        id='tab_userpermission' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbUserPermission" Text="${Security.User.UserAuthorization}" runat="server"
        OnClick="lbUserPermission_Click" /></span></span></span></span><span 
        id='tab_userpermissionlist' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbUserPeremissionList" Text="${Security.User.UserPermission}" runat="server"
        OnClick="lbUserPermissionList_Click" /></span></span></span></span>
    </div>

