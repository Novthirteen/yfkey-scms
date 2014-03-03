<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Transportation_TransportationAddress_New" %>

<div id="divFV" runat="server">
    <asp:FormView ID="FV_TransportationAddress" runat="server" DataSourceID="ODS_TransportationAddress" DefaultMode="Insert"
        DataKeyNames="Id">
        <InsertItemTemplate>
            <fieldset>
                <legend>${Transportation.TransportationAddress.AddTransportationAddress}</legend>
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
<asp:ObjectDataSource ID="ODS_TransportationAddress" runat="server" TypeName="com.Sconit.Web.TransportationAddressMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.Transportation.TransportationAddress" InsertMethod="CreateTransportationAddress"
    OnInserted="ODS_TransportationAddress_Inserted" OnInserting="ODS_TransportationAddress_Inserting">
</asp:ObjectDataSource>
