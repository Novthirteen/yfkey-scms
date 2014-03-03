<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_Supplier_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Supplier" runat="server" DataSourceID="ODS_Supplier" DefaultMode="Edit"
        Width="100%" DataKeyNames="Code" OnDataBound="FV_Supplier_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.Supplier.UpdateSupplier}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Supplier.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbCode" runat="server" Text='<%# Bind("Code") %>' />
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
                            <asp:CheckBox ID="tbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' Width="250" />
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
<asp:ObjectDataSource ID="ODS_Supplier" runat="server" TypeName="com.Sconit.Web.SupplierMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Supplier" UpdateMethod="UpdateSupplier"
    OnUpdated="ODS_Supplier_Updated" OnUpdating="ODS_Supplier_Updating" DeleteMethod="DeleteSupplier"
    OnDeleted="ODS_Supplier_Deleted" SelectMethod="LoadSupplier">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="code" Type="String" />
    </DeleteParameters>
</asp:ObjectDataSource>
