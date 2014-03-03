<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_User_New" %>

<script language="javascript" type="text/javascript">
function checkPassword(source, args)
{
   var password = document.getElementById("ctl02_ucNew_FV_User_tbPassword").value;
   var confirmPassword = document.getElementById("ctl02_ucNew_FV_User_tbConfirmPassword").value;
   if(password != confirmPassword)
   {
        args.IsValid = false; 
   }
          
}
</script>

<div id="divFV" runat="server">
    <asp:FormView ID="FV_User" runat="server" DataSourceID="ODS_User" DefaultMode="Insert"
        DataKeyNames="Code">
        <InsertItemTemplate>
            <fieldset>
                <legend>${Security.User.AddUser}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Security.User.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Code") %>' CssClass="inputRequired"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="${Security.User.Code.Empty}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave"  />
                            <asp:CustomValidator ID="cvInsert" runat="server" ControlToValidate="tbCode" ErrorMessage="${Security.User.Code.Exists}"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="checkUserExists"
                                 />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblFirstName" runat="server" Text="${Security.User.FirstName}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbFirstName" runat="server" Text='<%# Bind("FirstName") %>' CssClass="inputRequired"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="${Security.User.FirstName.Empty}"
                                Display="Dynamic" ControlToValidate="tbFirstName" ValidationGroup="vgSave"  />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblLastName" runat="server" Text="${Security.User.LastName}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbLastName" runat="server" Text='<%# Bind("LastName") %>' CssClass="inputRequired"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ErrorMessage="${Security.User.LastName.Empty}"
                                Display="Dynamic" ControlToValidate="tbLastName" ValidationGroup="vgSave"  />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblPassword" runat="server" Text="${Security.User.Password}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbPassword" TextMode="Password" runat="server" Text='<%#Bind("Password") %>'
                                CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="${Security.User.Password.Empty}"
                                Display="Dynamic" ControlToValidate="tbPassword" ValidationGroup="vgSave"  />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblConfirmPassword" runat="server" Text="${Security.User.ConfirmPassword}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbConfirmPassword" TextMode="Password" runat="server" Text='<%#Bind("Password") %>'
                                CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="${Security.User.ConfirmPassword.Empty}"
                                Display="Dynamic" ControlToValidate="tbConfirmPassword" ValidationGroup="vgSave"
                                 />
                             <asp:CustomValidator ID="cvPassword" runat="server" ErrorMessage="${Security.User.Password.Different}"
                                    ValidationGroup="vgSave" ClientValidationFunction="checkPassword"  Display="Dynamic"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblEmail" runat="server" Text="${Security.User.Email}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbEmail" runat="server" Text='<%#Bind("Email") %>' CssClass="inputRequired" />
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="tbEmail"
                                Display="Dynamic" ErrorMessage="${Security.User.Email.Format.Error}" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="vgSave"  />
                                <asp:RequiredFieldValidator
                                    ID="rfvEmail" runat="server" ControlToValidate="tbEmail" 
                                    Display="Dynamic" ErrorMessage="${Security.User.Email.Empty}" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblAddress" runat="server" Text="${Security.User.Address}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbAddress" runat="server" Text='<%#Bind("Address") %>' />
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
                            <asp:TextBox ID="tbPhone" runat="server" Text='<%#Bind("Phone") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblMobilePhone" runat="server" Text="${Security.User.MobliePhone}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMobilePhone" runat="server" Text='<%#Bind("MobliePhone") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${Security.User.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="tbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' />
                        </td>
                    </tr>
                </table>
                <div class="tablefooter">
                    <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="${Common.Button.Save}"
                        CssClass="button2" ValidationGroup="vgSave" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        CssClass="button2" />
                </div>
            </fieldset>
        </InsertItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_User" runat="server" TypeName="com.Sconit.Web.UserMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.User" InsertMethod="CreateUser"
    OnInserted="ODS_User_Inserted" OnInserting="ODS_User_Inserting"></asp:ObjectDataSource>
