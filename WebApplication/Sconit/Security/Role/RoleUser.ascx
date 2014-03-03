<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RoleUser.ascx.cs" Inherits="Security_Role_RoleUser" %>

<script type="text/javascript" language="javascript">
//$("#idNotInRole input:checkbox").click(

//    );
function idNotInRoleClick()
{
    if($("#idNotInRole input:checkbox").attr("checked")==true)
    {
        $("#idNotInRoleList input:checkbox").each(function(index,domEle){ 
                  if(this.type=="checkbox")
                       this.checked=true;
        });
     }
    else
    {
        $("#idNotInRoleList input:checkbox").each(function(index,domEle){ 
                  if(this.type=="checkbox")
                       this.checked=false;
        });
    }
}

function idInRoleClick()
{
    if($("#idInRole input:checkbox").attr("checked")==true)
    {
        $("#idInRoleList input:checkbox").each(function(index,domEle){ 
                  if(this.type=="checkbox")
                       this.checked=true;
        });
     }
    else
    {
        $("#idInRoleList input:checkbox").each(function(index,domEle){ 
                  if(this.type=="checkbox")
                       this.checked=false;
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
                ${Security.Role.NotInUser}:
            </td>
            <td style="width: 30%" id="idNotInRole" onclick="idNotInRoleClick()">
                <asp:CheckBox ID="cb_NotInRole" runat="server" Text="${Common.Select.All}" />
            </td>
            <td style="width: 10%">
            </td>
            <td style="width: 10%">
                ${Security.Role.InUser}:
            </td>
            <td style="width: 30%" id="idInRole" onclick="idInRoleClick()">
                <asp:CheckBox ID="cb_InRole" runat="server" Text="${Common.Select.All}" />
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
            </td>
            <td style="width: 30%" valign="top">
                <div class="scrolly" id="idNotInRoleList">
                    <asp:CheckBoxList ID="CBL_NotInUser" runat="server" DataSourceID="ODS_UsersNotInRole"
                        DataTextField="CodeName" DataValueField="Code" OnDataBinding="CBL_NotInUser_DataBinding">
                    </asp:CheckBoxList>
                    <asp:ObjectDataSource ID="ODS_UsersNotInRole" runat="server" SelectMethod="GetUsersNotInRole"
                        TypeName="com.Sconit.Web.UserRoleMgrProxy" DataObjectTypeName="com.Sconit.Entity.MasterData.Role">
                        <SelectParameters>
                            <asp:Parameter Name="code" Type="String" />
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
                <div id="idInRoleList" class="scrolly">
                    <asp:CheckBoxList ID="CBL_InUser" runat="server" DataSourceID="ODS_UsersInRole" DataTextField="CodeName"
                        DataValueField="Code" OnDataBinding="CBL_InUser_DataBinding">
                    </asp:CheckBoxList>
                    <asp:ObjectDataSource ID="ODS_UsersInRole" runat="server" SelectMethod="GetUsersByRoleCode"
                        TypeName="com.Sconit.Web.UserRoleMgrProxy" DataObjectTypeName="com.Sconit.Entity.MasterData.Role">
                        <SelectParameters>
                            <asp:Parameter Name="code" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </td>
        </tr>
    </table>
    <div class="tablefooter buttons"><asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="back" /></div>
</fieldset>
