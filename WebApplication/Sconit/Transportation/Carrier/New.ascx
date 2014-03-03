<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Transportation_Carrier_New" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Carrier" runat="server" DataSourceID="ODS_Carrier" DefaultMode="Insert"
        DataKeyNames="Code">
        <InsertItemTemplate>
            <fieldset>
                <legend>${Transportation.Carrier.AddCarrier}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Transportation.Carrier.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Code") %>' CssClass="inputRequired"
                                Width="250"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="${Transportation.Carrier.Code.Empty}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvInsert" runat="server" ErrorMessage="${Transportation.Carrier.Code.Exists}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" OnServerValidate="checkCarrierExists" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblName" runat="server" Text="${Transportation.Carrier.Name}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbName" runat="server" Text='<%# Bind("Name") %>' Width="250"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${Transportation.Carrier.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' />
                        </td>
                         <td class="td01">
                            <asp:Literal ID="lblCountry" runat="server" Text="${Transportation.Carrier.Country}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCountry" runat="server" Text='<%# Bind("Country") %>' Width="250"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblPaymentTerm" runat="server" Text="${Transportation.Carrier.PaymentTerm}:" />
                        </td>
                        <td class="td02">
                             <asp:TextBox ID="tbPaymentTerm" runat="server" Text='<%# Bind("PaymentTerm") %>' Width="250"></asp:TextBox>
                        </td>
                         <td class="td01">
                            <asp:Literal ID="lblTradeTerm" runat="server" Text="${Transportation.Carrier.TradeTerm}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbTradeTerm" runat="server" Text='<%# Bind("TradeTerm") %>' Width="250"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblReferenceSupplier" runat="server" Text="${Transportation.Carrier.ReferenceSupplier}:" />
                        </td>
                        <td class="td02">
                             <asp:TextBox ID="tbReferenceSupplier" runat="server" Text='<%# Bind("ReferenceSupplier") %>' Width="250"></asp:TextBox>
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
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
<asp:ObjectDataSource ID="ODS_Carrier" runat="server">
</asp:ObjectDataSource>
