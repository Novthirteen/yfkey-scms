<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Transportation_TransportationAddress_Edit" %>

<div id="divFV" runat="server">
    <asp:FormView ID="FV_TransportationAddress" runat="server" DataSourceID="ODS_TransportationAddress" DefaultMode="Edit"
        Width="100%" DataKeyNames="Id" OnDataBound="FV_TransportationAddress_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${Transportation.TransportationAddress.UpdateTransportationAddress}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCountry" runat="server" Text="${Transportation.TransportationAddress.Country}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCountry" runat="server" Text='<%# Bind("Country") %>' Width="250"></asp:TextBox>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblProvince" runat="server" Text="${Transportation.TransportationAddress.Province}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbProvince" runat="server" Text='<%# Bind("Province") %>' Width="250"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCity" runat="server" Text="${Transportation.TransportationAddress.City}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCity" runat="server" Text='<%# Bind("City") %>' Width="250"></asp:TextBox>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblDistrict" runat="server" Text="${Transportation.TransportationAddress.District}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbDistrict" runat="server" Text='<%# Bind("District") %>' Width="250"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblAddress" runat="server" Text="${Transportation.TransportationAddress.Address}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbAddress" runat="server" Text='<%# Bind("Address") %>' CssClass="inputRequired"
                                Width="250"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ErrorMessage="${Transportation.TransportationAddress.Address.Empty}"
                                Display="Dynamic" ControlToValidate="tbAddress" ValidationGroup="vgSave" />
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
<asp:ObjectDataSource ID="ODS_TransportationAddress" runat="server" TypeName="com.Sconit.Web.TransportationAddressMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.Transportation.TransportationAddress" UpdateMethod="UpdateTransportationAddress"
    OnUpdated="ODS_TransportationAddress_Updated" OnUpdating="ODS_TransportationAddress_Updating" DeleteMethod="DeleteTransportationAddress"
    OnDeleted="ODS_TransportationAddress_Deleted" SelectMethod="LoadTransportationAddress">
    <SelectParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </DeleteParameters>
</asp:ObjectDataSource>
