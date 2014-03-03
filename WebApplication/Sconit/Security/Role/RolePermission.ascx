<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RolePermission.ascx.cs"
    Inherits="Security_Role_RolePermission" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script type="text/javascript" language="javascript">
    //$("#idNotInPermission input:checkbox").click(

    //    );
    function idNotInPermissionClick() {
        if ($("#idNotInPermission input:checkbox").attr("checked") == true) {
            $("#idNotInPermissionList input:checkbox").each(function(index, domEle) {
                if (this.type == "checkbox")
                    this.checked = true;
            });
        }
        else {
            $("#idNotInPermissionList input:checkbox").each(function(index, domEle) {
                if (this.type == "checkbox")
                    this.checked = false;
            });
        }
    }

    function idInPermissionClick() {
        if ($("#idInPermission input:checkbox").attr("checked") == true) {
            $("#idInPermissionList input:checkbox").each(function(index, domEle) {
                if (this.type == "checkbox")
                    this.checked = true;
            });
        }
        else {
            $("#idInPermissionList input:checkbox").each(function(index, domEle) {
                if (this.type == "checkbox")
                    this.checked = false;
            });
        }
    }
</script>

<fieldset>
    <legend>
        <asp:Literal ID="lblCode" runat="server" Text="${Security.Role.CurrentRole}:" />
        <asp:Literal ID="lbCode" runat="server" /></legend>
    <table width="100%">
        <tr>
            <td style="width: 10%">
                <asp:Literal ID="lblCategoryType" runat="server" Text="${Security.Permission.Category.Type}:" />
            </td>
            <td style="width: 30%">
                <uc3:textbox ID="tbCategoryType" runat="server" Visible="true" Width="250" DescField="Description"
                    ValueField="Value" ServicePath="CodeMasterMgr.service" ServiceMethod="GetCachedCodeMaster" />
                <asp:RequiredFieldValidator ID="rfvCategoryType" runat="server" ErrorMessage="${Security.UserPermission.CategoryType.Required}"
                    Display="Dynamic" ControlToValidate="tbCategoryType" ValidationGroup="vgSearch" />
            </td>
            <td style="width: 10%">
                <asp:Literal ID="lblCategory" runat="server" Text="${Security.Permission.Category.Description}:" />
            </td>
            <td style="width: 20%">
                <uc3:textbox ID="tbCategory" runat="server" Visible="true" Width="250" DescField="Description"
                    ValueField="Code" ServicePath="PermissionCategoryMgr.service" ServiceMethod="GetCategoryByType"
                    ServiceParameter="string:#tbCategoryType" />
                <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ErrorMessage="${Security.UserPermission.Category.Required}"
                    Display="Dynamic" ControlToValidate="tbCategory" ValidationGroup="vgSearch" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
            </td>
            <td>
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                        CssClass="query" ValidationGroup="vgSearch" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        CssClass="back" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
<fieldset>
    <table width="100%">
        <tr>
            <td style="width: 10%">
                ${Security.User.Permission.NotInPermission}:
            </td>
            <td style="width: 30%" id="idNotInPermission" onclick="idNotInPermissionClick()">
                <asp:CheckBox ID="cb_NotInPermission" runat="server" Text="${Common.Select.All}" />
            </td>
            <td style="width: 10%">
            </td>
            <td style="width: 10%">
                ${Security.User.Permission.InPermission}:
            </td>
            <td style="width: 30%" id="idInPermission" onclick="idInPermissionClick()">
                <asp:CheckBox ID="cb_InPermission" runat="server" Text="${Common.Select.All}" />
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
            </td>
            <td style="width: 30%" valign="top">
                <div class="scrolly" id="idNotInPermissionList">
                    <asp:CheckBoxList ID="CBL_NotInPermission" runat="server" DataSourceID="ODS_PermissionsNotInRole"
                        DataTextField="Description" DataValueField="Id">
                    </asp:CheckBoxList>
                    <asp:ObjectDataSource ID="ODS_PermissionsNotInRole" runat="server" SelectMethod="GetPermissionsNotInRole"
                        TypeName="com.Sconit.Web.RolePermissionMgrProxy" DataObjectTypeName="com.Sconit.Entity.MasterData.Permission">
                        <SelectParameters>
                            <asp:Parameter Name="code" Type="String" />
                            <asp:Parameter Name="categoryCode" Type="String" DefaultValue="null" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </td>
            <td valign="middle" align="center" colspan="2">
                <asp:Button ID="ToInBT" runat="server" Text=">>>" OnClick="ToInBT_Click" CssClass="button2" />
                <br />
                <br />
                <asp:Button ID="ToOutBT" runat="server" Text="<<<" OnClick="ToOutBT_Click" CssClass="button2" />
            </td>
            <td style="width: 30%" valign="top">
                <div class="scrolly" id="idInPermissionList">
                    <asp:CheckBoxList ID="CBL_InPermission" runat="server" DataSourceID="ODS_PermissionsInRole"
                        DataTextField="Description" DataValueField="Id">
                    </asp:CheckBoxList>
                    <asp:ObjectDataSource ID="ODS_PermissionsInRole" runat="server" SelectMethod="GetPermissionsByRoleCode"
                        TypeName="com.Sconit.Web.RolePermissionMgrProxy" DataObjectTypeName="com.Sconit.Entity.MasterData.Permission">
                        <SelectParameters>
                            <asp:Parameter Name="code" Type="String" />
                            <asp:Parameter Name="categoryCode" Type="String" DefaultValue="null" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </td>
        </tr>
    </table>
</fieldset>
