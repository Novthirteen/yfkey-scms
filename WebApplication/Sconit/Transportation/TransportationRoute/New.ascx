<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Transportation_TransportationRoute_New" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_TransportationRoute" runat="server" DataSourceID="ODS_TransportationRoute" DefaultMode="Insert"
        DataKeyNames="Code">
        <InsertItemTemplate>
            <fieldset>
                <legend>${Transportation.TransportationRoute.AddTransportationRoute}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Transportation.TransportationRoute.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Code") %>' CssClass="inputRequired"
                                Width="250"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="${Transportation.TransportationRoute.Code.Empty}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvCode" runat="server" ControlToValidate="tbCode" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblDescription" runat="server" Text="${Transportation.TransportationRoute.Description}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbDescription" runat="server" Text='<%# Bind("Description") %>' Width="250"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblShipFrom" runat="server" Text="${Transportation.TransportationRoute.ShipFrom}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbShipFrom" runat="server" Width="250" DescField="Empty" ValueField="FullAddressAndId"
                                CssClass="inputRequired" MustMatch="true" ServiceMethod="GetAllTransportationAddress" ServicePath="TransportationAddressMgr.service"/>
                            <asp:RequiredFieldValidator ID="rfvShipFrom" runat="server" ErrorMessage="${Transportation.TransportationRoute.ShipFrom.Empty}"
                                Display="Dynamic" ControlToValidate="tbShipFrom" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvShipFrom" runat="server" ControlToValidate="tbShipFrom" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblShipTo" runat="server" Text="${Transportation.TransportationRoute.ShipTo}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbShipTo" runat="server" Width="250" DescField="Empty" ValueField="FullAddressAndId"
                                CssClass="inputRequired" MustMatch="true" ServiceMethod="GetAllTransportationAddress" ServicePath="TransportationAddressMgr.service"/>
                            <asp:RequiredFieldValidator ID="rfvShipTo" runat="server" ErrorMessage="${Transportation.TransportationRoute.ShipTo.Empty}"
                                Display="Dynamic" ControlToValidate="tbShipTo" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvShipTo" runat="server" ControlToValidate="tbShipTo" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${Transportation.TransportationRoute.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' />
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
<asp:ObjectDataSource ID="ODS_TransportationRoute" runat="server" TypeName="com.Sconit.Web.TransportationRouteMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.Transportation.TransportationRoute" InsertMethod="CreateTransportationRoute"
    OnInserted="ODS_TransportationRoute_Inserted" OnInserting="ODS_TransportationRoute_Inserting">
</asp:ObjectDataSource>
