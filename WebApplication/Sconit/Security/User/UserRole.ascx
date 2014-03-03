<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserRole.ascx.cs" Inherits="Security_User_UserRole" %>

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
<%--
<div style="background: url(Images/Line.png) repeat-x center; padding-left: 10px;">
    <div style="border: solid 1px #d0d0bf; background: #FFF; width: 100px; padding: 3px;
        text-align: center">
        <asp:Literal ID="lblCode" runat="server" Text="${Security.User.CurrentUser}:" />
        <asp:Literal ID="lbCode" runat="server" />
    </div>
</div>
--%>
<fieldset>
    <legend>
        <asp:Literal ID="lblCode" runat="server" Text="${Security.User.CurrentUser}:" />
        <asp:Literal ID="lbCode" runat="server" /></legend>
    <table width="100%">
        <tr>
            <td style="width: 10%">
            </td>
            <td style="width: 30%">
            </td>
            <td style="width: 10%">
            </td>
            <td style="width: 10%">
            </td>
            <td style="width: 30%">
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                ${Security.User.Role.NotInRole}:
            </td>
            <td style="width: 30%" id="idNotInRole" onclick="idNotInRoleClick()">
                <asp:CheckBox ID="cb_NotInRole" runat="server" Text="${Common.Select.All}" />
            </td>
            <td style="width: 10%">
            </td>
            <td style="width: 10%">
                ${Security.User.Role.InRole}:
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
                    <asp:CheckBoxList ID="CBL_NotInRole" runat="server" DataSourceID="ODS_RolesNotInUser"
                        DataTextField="Description" DataValueField="Code">
                    </asp:CheckBoxList>
                    <asp:ObjectDataSource ID="ODS_RolesNotInUser" runat="server" SelectMethod="GetRolesNotInUser"
                        TypeName="com.Sconit.Web.UserRoleMgrProxy" DataObjectTypeName="com.Sconit.Entity.MasterData.Role">
                        <SelectParameters>
                            <asp:Parameter Name="code" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </td>
            <td valign="middle" align="center" colspan="2">
                <asp:Button ID="ToInBT" runat="server" Text=">>>" OnClick="ToInBT_Click" CssClass="button2"/>
                <br />
                <br />
                <asp:Button ID="ToOutBT" runat="server" Text="<<<" OnClick="ToOutBT_Click" CssClass="button2" />
            </td>
            <td style="width: 30%" valign="top">
                <div class="scrolly" id="idInRoleList">
                    <asp:CheckBoxList ID="CBL_InRole" runat="server" DataSourceID="ODS_RolesInUser" DataTextField="Description"
                        DataValueField="Code">
                    </asp:CheckBoxList>
                    <asp:ObjectDataSource ID="ODS_RolesInUser" runat="server" SelectMethod="GetRolesByUserCode"
                        TypeName="com.Sconit.Web.UserRoleMgrProxy" DataObjectTypeName="com.Sconit.Entity.MasterData.Role">
                        <SelectParameters>
                            <asp:Parameter Name="code" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </td>
        </tr>
    </table>
    <div class="tablefooter"><asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="button2" /></div>
</fieldset>
