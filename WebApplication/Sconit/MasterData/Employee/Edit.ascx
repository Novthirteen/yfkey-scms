<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_Employee_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc2" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Employee" runat="server" DataSourceID="ODS_Employee" DefaultMode="Edit"
        Width="100%" DataKeyNames="Code" OnDataBound="FV_Employee_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.Employee.UpdateEmployee}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Common.Business.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Code") %>' ReadOnly="true" />
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
                                <asp:Button ID="btnSave" runat="server" CommandName="Update" Text="${Common.Button.Save}"
                                    CssClass="apply" ValidationGroup="vgSave" />
                                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}"
                                    CssClass="delete" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                                    CssClass="back" />
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Employee" runat="server" TypeName="com.Sconit.Web.EmployeeMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Employee" UpdateMethod="UpdateEmployee"
    OnUpdated="ODS_Employee_Updated" SelectMethod="LoadEmployee" OnUpdating="ODS_Employee_Updating"
    DeleteMethod="DeleteEmployee" OnDeleted="ODS_Employee_Deleted">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
