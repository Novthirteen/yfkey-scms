<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserPermission.ascx.cs"
    Inherits="Security_User_UserPermission" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script type="text/javascript" language="javascript">
//$("#idNotInPermission input:checkbox").click(

//    );
function idNotInPermissionClick()
{
    if($("#idNotInPermission input:checkbox").attr("checked")==true)
    {
        $("#idNotInPermissionList input:checkbox").each(function(index,domEle){ 
                  if(this.type=="checkbox")
                       this.checked=true;
        });
     }
    else
    {
        $("#idNotInPermissionList input:checkbox").each(function(index,domEle){ 
                  if(this.type=="checkbox")
                       this.checked=false;
        });
    }
}

function idInPermissionClick()
{
    if($("#idInPermission input:checkbox").attr("checked")==true)
    {
        $("#idInPermissionList input:checkbox").each(function(index,domEle){ 
                  if(this.type=="checkbox")
                       this.checked=true;
        });
     }
    else
    {
        $("#idInPermissionList input:checkbox").each(function(index,domEle){ 
                  if(this.type=="checkbox")
                       this.checked=false;
        });
    }
}
</script>

<fieldset>
    <legend>
        <asp:Literal ID="lblCode" runat="server" Text="${Security.User.CurrentUser}:" />
        <asp:Literal ID="lbCode" runat="server" /></legend>
    <table width="100%">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblCategoryType" runat="server" Text="${Security.Permission.Category.Type}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbCategoryType" runat="server" Visible="true" Width="250" DescField="Description"
                    ValueField="Value" ServicePath="CodeMasterMgr.service" ServiceMethod="GetCachedCodeMaster"
                    CssClass="inputRequired" />
                <asp:RequiredFieldValidator ID="rfvCategoryType" runat="server" ErrorMessage="${Security.UserPermission.CategoryType.Required}"
                    Display="Dynamic" ControlToValidate="tbCategoryType" ValidationGroup="vgSearch" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblCategory" runat="server" Text="${Security.Permission.Category.Description}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbCategory" runat="server" Visible="true" Width="250" DescField="Description"
                    ValueField="Code" ServicePath="PermissionCategoryMgr.service" ServiceMethod="GetCategoryByType"
                    ServiceParameter="string:#tbCategoryType" CssClass="inputRequired" />
                <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ErrorMessage="${Security.UserPermission.Category.Required}"
                    Display="Dynamic" ControlToValidate="tbCategory" ValidationGroup="vgSearch" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                    CssClass="button2" ValidationGroup="vgSearch" />
                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="button2" />
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
                    <asp:CheckBoxList ID="CBL_NotInPermission" runat="server" DataSourceID="ODS_PermissionsNotInUser"
                        DataTextField="Description" DataValueField="Id">
                    </asp:CheckBoxList>
                    <asp:ObjectDataSource ID="ODS_PermissionsNotInUser" runat="server" SelectMethod="GetPermissionsNotInUser"
                        TypeName="com.Sconit.Web.UserPermissionMgrProxy" DataObjectTypeName="com.Sconit.Entity.MasterData.Permission">
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
                    <asp:CheckBoxList ID="CBL_InPermission" runat="server" DataSourceID="ODS_PermissionsInUser"
                        DataTextField="Description" DataValueField="Id">
                    </asp:CheckBoxList>
                    <asp:ObjectDataSource ID="ODS_PermissionsInUser" runat="server" SelectMethod="GetPermissionsByUserCode"
                        TypeName="com.Sconit.Web.UserPermissionMgrProxy" DataObjectTypeName="com.Sconit.Entity.MasterData.Permission">
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
