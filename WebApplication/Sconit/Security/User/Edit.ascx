<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_User_Edit" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript">
function checkPassword(source, args)
{
   var password = document.getElementById("ctl02_ucEdit_tbPassword").value;
   var confirmPassword = document.getElementById("ctl02_ucEdit_tbConfirmPassword").value;
   if(password != confirmPassword)
   {
        args.IsValid = false; 
   }
          
}
</script>

<div id="divFV" runat="server">
    <asp:FormView ID="FV_User" runat="server" DataSourceID="ODS_User" DefaultMode="Edit" Width="100%"
        DataKeyNames="Code"  OnDataBound="FV_User_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${Security.User.UpdateUser}</legend>
                <table class="mtable" width="100%">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Security.User.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbCode" runat="server" Text='<%# Bind("Code") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblFirstName" runat="server" Text="${Security.User.FirstName}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbFirstName" runat="server" Text='<%# Bind("FirstName") %>' Width="150"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="${Security.User.FirstName.Empty}"
                                Display="Dynamic" ControlToValidate="tbFirstName" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblLastName" runat="server" Text="${Security.User.LastName}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbLastName" runat="server" Text='<%# Bind("LastName") %>' Width="150"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="${Security.User.LastName.Empty}"
                                Display="Dynamic" ControlToValidate="tbLastName" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblEmail" runat="server" Text="${Security.User.Email}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbEmail" runat="server" Text='<%#Bind("Email") %>' Width="200" />
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="tbEmail"
                                Display="Dynamic" ErrorMessage="${Security.User.Email.Format.Error}" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                meta:resourcekey="RegularExpressionValidator1Resource1" ValidationGroup="vgSave" />
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="tbEmail"
                                Display="Dynamic" ErrorMessage="${Security.User.Email.Empty}" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblAddress" runat="server" Text="${Security.User.Address}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbAddress" runat="server" Text='<%#Bind("Address") %>' Width="250" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblGender" runat="server" Text="${Security.User.Gender}:" />
                        </td>
                        <td class="td02">
                            <asp:RadioButtonList ID="rblGender" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                DataTextField='<%#Bind("Gender") %>'>
                                <asp:ListItem Selected="True" Text="${Common.Gender.Male}" Value="M" />
                                <asp:ListItem Text="${Common.Gender.Female}" Value="F" />
                            </asp:RadioButtonList>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblPhone" runat="server" Text="${Security.User.Phone}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbPhone" runat="server" Text='<%#Bind("Phone") %>' Width="200" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblMobilePhone" runat="server" Text="${Security.User.MobliePhone}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMobilePhone" runat="server" Text='<%#Bind("MobliePhone") %>' Width="200" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${Security.User.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="tbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        <div class="buttons">
                            <asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="${Common.Button.Save}"
                                ValidationGroup="vgSave"  CssClass="add"/>
                            <%--<cc1:Button ID="btnChangePassword" runat="server" OnClick="btnChangePassword_Click" 
                                Text="${Security.User.UpdatePassword}"  CssClass="password" FunctionId="Page_ChangePassword"/>--%>  
                            <asp:Button ID="btnChangePassword" runat="server" OnClick="btnChangePassword_Click" 
                                Text="${Security.User.UpdatePassword}"  CssClass="password"/>                                   
                            <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}"
                                OnClientClick="return confirm('${Common.Button.Delete.Confirm}')"  CssClass="delete" />
                            <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"  CssClass="back" />
                            </div>

                        </td>
                    </tr>
                </table>
            </fieldset>

        </EditItemTemplate>
    </asp:FormView>
</div>
<div id="divPassword" visible="false" runat="server">
    <fieldset>
        <legend>${Security.User.UpdatePassword}</legend>
        <table width="100%">
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblPassword" runat="server" Text="${Security.User.Password}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbPassword" TextMode="Password" runat="server" Text='<%#Bind("Password") %>' EnableViewState="false"
                        Width="250" />
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="${Security.User.Password.Empty}"
                        Display="Dynamic" ControlToValidate="tbPassword" ValidationGroup="vgPassword" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblConfirmPassword" runat="server" Text="${Security.User.ConfirmPassword}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbConfirmPassword" TextMode="Password" runat="server" Text='<%#Bind("Password") %>'
                        Width="250" />
                    <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="${Security.User.ConfirmPassword.Empty}"
                        Display="Dynamic" ControlToValidate="tbConfirmPassword" ValidationGroup="vgPassword" />                         
                    <asp:CompareValidator ID="cvPassword" runat="server" ErrorMessage="${Security.User.Password.Different}"                     ControlToValidate="tbConfirmPassword" ControlToCompare="tbPassword"                     Operator="Equal" Display="Dynamic" Type="String" ValidationGroup="vgPassword" /> 
                </td>
            </tr>
            <tr>
                <td class="td01" colspan="3">
                </td>
                <td class="td02">
                <div class="buttons">
                    <asp:Button ID="btnUpdatePassword" runat="server" OnClick="btnUpdatePassword_Click"
                        Text="${Common.Button.Save}" CssClass="apply" ValidationGroup="vgPassword" />
                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="${Common.Button.Cancel}"
                        CssClass="cancel"  />
                        </div>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
<asp:ObjectDataSource ID="ODS_User" runat="server" TypeName="com.Sconit.Web.UserMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.User" UpdateMethod="UpdateUser"
    OnUpdated="ODS_User_Updated" OnUpdating="ODS_User_Updating" DeleteMethod="DeleteUser"
    OnDeleted="ODS_User_Deleted" SelectMethod="LoadUser" >
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="code" Type="String" />
    </DeleteParameters>
</asp:ObjectDataSource>
