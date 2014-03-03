<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_Employee_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc2" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Employee" runat="server" DataSourceID="ODS_Employee" DefaultMode="Insert"
        Width="100%" DataKeyNames="Code" OnDataBinding="FV_Employee_OnDataBinding">
        <InsertItemTemplate>
            <fieldset>
                <legend>${MasterData.Employee.NewEmployee}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Common.Business.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Code") %>' CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rtvCode" runat="server" ErrorMessage="${MasterData.Employee.Code.Empty}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvInsert" runat="server" ControlToValidate="tbCode" ErrorMessage="${Common.Code.Exist}"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="checkEmployee" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlName" runat="server" Text="${Common.Business.Name}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbName" runat="server" Text='<%# Bind("Name") %>' CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="${MasterData.Employee.Name.Empty}"
                                Display="Dynamic" ControlToValidate="tbName" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlGender" runat="server" Text="${Security.User.Gender}:" />
                        </td>
                        <td class="td02">
                            <cc2:CodeMstrDropDownList ID="ddlGender" Code="Gender" runat="server" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlDepartment" runat="server" Text="${MasterData.Employee.Department}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbDepartment" runat="server" Text='<%# Bind("Department") %>' Width="250" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lbWorkGroup" runat="server" Text="${MasterData.Employee.WorkGroup}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbWorkGroup" runat="server" Text='<%# Bind("WorkGroup") %>' Width="250" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlPost" runat="server" Text="${MasterData.Employee.Post}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbPost" runat="server" Text='<%# Bind("Post") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlMemo" runat="server" Text="${MasterData.Employee.Memo}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMemo" runat="server" Text='<%# Bind("Memo") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${Common.Business.IsActive}:" />
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
                                <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="${Common.Button.Save}"
                                    CssClass="apply" ValidationGroup="vgSave" />
                                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                                    CssClass="back" />
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </InsertItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Employee" runat="server" TypeName="com.Sconit.Web.EmployeeMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Employee" InsertMethod="CreateEmployee"
    OnInserted="ODS_Employee_Inserted" OnInserting="ODS_Employee_Inserting"></asp:ObjectDataSource>
