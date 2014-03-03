<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_Supplier_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Supplier" runat="server" DataSourceID="ODS_Supplier" DefaultMode="Insert"
        DataKeyNames="Code">
        <InsertItemTemplate>
            <fieldset>
                <legend>${MasterData.Supplier.AddSupplier}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Supplier.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Code") %>' CssClass="inputRequired"
                                Width="250"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="${MasterData.Supplier.Code.Empty}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvInsert" runat="server" ControlToValidate="tbCode" ErrorMessage="${MasterData.Supplier.Code.Exists}"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="checkSupplierExists" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblName" runat="server" Text="${MasterData.Supplier.Name}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbName" runat="server" Text='<%# Bind("Name") %>' Width="250"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${MasterData.Supplier.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' />
                        </td>
                         <td class="td01">
                            <asp:Literal ID="lblCountry" runat="server" Text="${MasterData.Supplier.Country}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCountry" runat="server" Text='<%# Bind("Country") %>' Width="250"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblPaymentTerm" runat="server" Text="${MasterData.Supplier.PaymentTerm}:" />
                        </td>
                        <td class="td02">
                             <asp:TextBox ID="tbPaymentTerm" runat="server" Text='<%# Bind("PaymentTerm") %>' Width="250"></asp:TextBox>
                        </td>
                         <td class="td01">
                            <asp:Literal ID="lblTradeTerm" runat="server" Text="${MasterData.Supplier.TradeTerm}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbTradeTerm" runat="server" Text='<%# Bind("TradeTerm") %>' Width="250"></asp:TextBox>
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
<asp:ObjectDataSource ID="ODS_Supplier" runat="server" TypeName="com.Sconit.Web.SupplierMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Supplier" InsertMethod="CreateSupplier">
</asp:ObjectDataSource>
