<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Transportation_Expense_Edit" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Expense" runat="server" DataSourceID="ODS_Expense" DefaultMode="Edit"
        Width="100%" DataKeyNames="Code" OnDataBound="FV_Expense_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${Transportation.Expense.UpdateExpense}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Transportation.Expense.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbCode" runat="server" Text='<%# Bind("Code") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCarrier" runat="server" Text="${Transportation.Expense.Carrier}:" />
                        </td>
                        <td class="td02">
                            <cc1:readonlytextbox id="tbCarrier" runat="server" codefield="Carrier.Name" />                            
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblAmount" runat="server" Text="${Transportation.Expense.Amount}:" />
                        </td>
                        <td class="td02">
                            <cc1:readonlytextbox id="tbAmount" runat="server" codefield="Amount" CodeFieldFormat="{0:0.########}" />  
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCurrency" runat="server" Text="${MasterData.Currency.Code}:" />
                        </td>
                        <td class="td02">
                            <cc1:readonlytextbox id="tbCurrency" runat="server" codefield="Currency.Code" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCreateDate" runat="server" Text="${Transportation.Expense.CreateDate}:" />
                        </td>
                        <td class="td02">
                            <cc1:readonlytextbox id="tbCreateDate" runat="server" codefield="CreateDate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCreateUser" runat="server" Text="${Transportation.Expense.CreateUser}:" />
                        </td>
                        <td class="td02">
                            <cc1:readonlytextbox id="tbCreateUser" runat="server" codefield="CreateUser.Name" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblTaxCode" runat="server" Text="${Transportation.Expense.TaxCode}:" />
                        </td>
                        <td class="td02">
                            <cc1:readonlytextbox id="tbTaxCode" runat="server" codefield="TaxCode" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblIsIncludeTax" runat="server" Text="${Transportation.Expense.IsIncludeTax}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsIncludeTax" runat="server" Checked='<%#Bind("IsIncludeTax") %>' Enabled="false" />
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
                                <asp:Button ID="Button1" runat="server" CommandName="Update" Text="${Common.Button.Save}" CssClass="apply"
                                    ValidationGroup="vgSave" />
                                <asp:Button ID="Button2" runat="server" CommandName="Delete" Text="${Common.Button.Delete}" CssClass="delete"
                                    OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                                <asp:Button ID="Button3" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="back" />
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Expense" runat="server" TypeName="com.Sconit.Web.ExpenseMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.Transportation.Expense" UpdateMethod="UpdateExpense"
    OnUpdated="ODS_Expense_Updated" OnUpdating="ODS_Expense_Updating" DeleteMethod="DeleteExpense"
    OnDeleted="ODS_Expense_Deleted" SelectMethod="LoadExpense">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="code" Type="String" />
    </DeleteParameters>
</asp:ObjectDataSource>
