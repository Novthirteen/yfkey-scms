<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Transportation_Expense_New" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Expense" runat="server" DataSourceID="ODS_Expense" DefaultMode="Insert"
        DataKeyNames="Code">
        <InsertItemTemplate>
            <fieldset>
                <legend>${Transportation.Expense.AddExpense}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Transportation.Expense.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Code") %>' CssClass="inputRequired"
                                Width="250"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="${Transportation.Expense.Code.Empty}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvCode" runat="server" ControlToValidate="tbCode" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCarrier" runat="server" Text="${Transportation.Expense.Carrier}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbCarrier" runat="server" Width="250" DescField="Name" ValueField="Code"
                                CssClass="inputRequired" MustMatch="true" ServiceMethod="GetCarrier" ServicePath="CarrierMgr.service"/>
                            <asp:RequiredFieldValidator ID="rfvCarrier" runat="server" ErrorMessage="${Transportation.Expense.Carrier.Empty}"
                                Display="Dynamic" ControlToValidate="tbCarrier" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvCarrier" runat="server" ControlToValidate="tbCarrier" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblAmount" runat="server" Text="${Transportation.Expense.Amount}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbAmount" runat="server" Text='<%#Bind("Amount","{0:0.########}") %>'
                                CssClass="inputRequired" />
                            <asp:CustomValidator ID="cvAmount" runat="server" ControlToValidate="tbAmount"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                            <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ErrorMessage="${Transportation.Expense.Amount.Empty}"
                                Display="Dynamic" ControlToValidate="tbAmount" ValidationGroup="vgSave" />
                            <asp:RangeValidator ID="rvAmount" ControlToValidate="tbAmount" runat="server"
                                Display="Dynamic" ErrorMessage="${Transportation.Expense.Amount.Format}" MaximumValue="999999999"
                                MinimumValue="0.00000001" Type="Double" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCurrency" runat="server" Text="${MasterData.Currency.Code}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbCurrency" runat="server" DescField="Name" ValueField="Code" ServicePath="CurrencyMgr.service"
                                ServiceMethod="GetAllCurrency" MustMatch="true" CssClass="inputRequired" />
                            <asp:CustomValidator ID="cvCurrency" runat="server" ControlToValidate="tbCurrency"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                            <asp:RequiredFieldValidator ID="rfvCurrency" runat="server" ErrorMessage="${MasterData.Currency.Code.Empty}"
                                Display="Dynamic" ControlToValidate="tbCurrency" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblTaxCode" runat="server" Text="${Transportation.Expense.TaxCode}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbTaxCode" runat="server" Text='<%#Bind("TaxCode") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblIsIncludeTax" runat="server" Text="${Transportation.Expense.IsIncludeTax}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsIncludeTax" runat="server" Checked='<%#Bind("IsIncludeTax") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblRemark" runat="server" Text="${Transportation.Expense.Remark}:" />
                        </td>
                        <td class="td02">
                             <asp:TextBox ID="tbRemark" runat="server" Text='<%# Bind("Remark") %>' Width="250"></asp:TextBox>
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
<asp:ObjectDataSource ID="ODS_Expense" runat="server" TypeName="com.Sconit.Web.ExpenseMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.Transportation.Expense" InsertMethod="CreateExpense"
    OnInserted="ODS_Expense_Inserted" OnInserting="ODS_Expense_Inserting">
</asp:ObjectDataSource>
