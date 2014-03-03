<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_Customer_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Customer" runat="server" DataSourceID="ODS_Customer" DefaultMode="Insert"
        DataKeyNames="Code">
        <InsertItemTemplate>
            <fieldset>
                <legend>${MasterData.Customer.AddCustomer}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Customer.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Code") %>' CssClass="inputRequired"
                                Width="250"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="${MasterData.Customer.Code.Empty}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvInsert" runat="server" ControlToValidate="tbCode" ErrorMessage="${MasterData.Customer.Code.Exists}"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="checkCustomerExists" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblName" runat="server" Text="${MasterData.Customer.Name}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbName" runat="server" Text='<%# Bind("Name") %>' Width="250"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${MasterData.Customer.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' />
                        </td>
                         <td class="td01">
                            <asp:Literal ID="lblIsIntern" runat="server" Text="${MasterData.Customer.IsIntern}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsIntern" runat="server" Checked='<%#Bind("IsIntern") %>' />
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
                                <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="${Common.Button.Save}"
                                    CssClass="add" ValidationGroup="vgSave" />
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
<asp:ObjectDataSource ID="ODS_Customer" runat="server" TypeName="com.Sconit.Web.CustomerMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Customer" InsertMethod="CreateCustomer">
</asp:ObjectDataSource>
