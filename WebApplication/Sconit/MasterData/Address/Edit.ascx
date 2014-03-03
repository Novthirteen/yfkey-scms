<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_Address_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="floatdiv">
    <asp:FormView ID="FV_Address" runat="server" DataSourceID="ODS_Address" DefaultMode="Edit"
        Width="100%" DataKeyNames="Code" OnDataBound="FV_Address_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.Address.UpdateAddress}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCurrentParty" runat="server" Text="${MasterData.Party.CurrentParty}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbCurrentParty" runat="server" />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Address.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbCode" runat="server" Text='<%# Bind("Code") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblSequence" runat="server" Text="${MasterData.Address.Sequence}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbSequence" runat="server" Text='<%# Bind("Sequence") %>' CssClass="inputRequired"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSequence" runat="server" ErrorMessage="${MasterData.Address.Sequence.Empty}"
                                Display="Dynamic" ControlToValidate="tbSequence" ValidationGroup="vgSave" />
                            <asp:RangeValidator ID="rvSequence" runat="server" ControlToValidate="tbSequence"
                                ErrorMessage="${Common.Validator.Valid.Number}" Display="Dynamic" Type="Integer"
                                MinimumValue="1" MaximumValue="100000" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblAddress" runat="server" Text="${MasterData.Address.Address}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbAddress" runat="server" Text='<%# Bind("Address") %>'></asp:TextBox>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblIsPrimary" runat="server" Text="${MasterData.Address.IsPrimary}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="tbIsPrimary" runat="server" Checked='<%# Bind("IsPrimary") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblPostalCode" runat="server" Text="${MasterData.Address.PostalCode}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbPostalCode" runat="server" Text='<%# Bind("PostalCode") %>'></asp:TextBox>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblPostalCodeExtention" runat="server" Text="${MasterData.Address.PostalCodeExtention}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbPostalCodeExtention" runat="server" Text='<%# Bind("PostalCodeExtention") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblTelephoneNumber" runat="server" Text="${MasterData.Address.TelephoneNumber}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbTelephoneNumber" runat="server" Text='<%# Bind("TelephoneNumber") %>'></asp:TextBox>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblMobilePhone" runat="server" Text="${MasterData.Address.MobilePhone}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMobilePhone" runat="server" Text='<%# Bind("MobilePhone") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblContactPersonName" runat="server" Text="${MasterData.Address.ContactPersonName}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbContactPersonName" runat="server" Text='<%# Bind("ContactPersonName") %>'></asp:TextBox>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblFax" runat="server" Text="${MasterData.Address.Fax}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="Ftbax" runat="server" Text='<%# Bind("Fax") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblEmail" runat="server" Text="${MasterData.Address.Email}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbEmail" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblWebSite" runat="server" Text="${MasterData.Address.WebSite}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbWebSite" runat="server" Text='<%# Bind("WebSite") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${MasterData.Address.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="tbIsActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div class="tablefooter">
                <div class="buttons">
                    <asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="${Common.Button.Save}"
                        CssClass="apply" ValidationGroup="vgSave" />
                    <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}"
                        CssClass="delete" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        CssClass="close" />
                </div>
            </div>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Address" runat="server" TypeName="com.Sconit.Web.BillAddressMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.BillAddress" UpdateMethod="UpdateBillAddress"
    OnUpdated="ODS_Address_Updated" OnUpdating="ODS_Address_Updating" DeleteMethod="DeleteBillAddress"
    OnDeleted="ODS_Address_Deleted" SelectMethod="LoadBillAddress">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="code" Type="String" />
    </DeleteParameters>
</asp:ObjectDataSource>
