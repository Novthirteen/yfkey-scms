<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Transportation_TransportationRoute_Edit" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_TransportationRoute" runat="server" DataSourceID="ODS_TransportationRoute" DefaultMode="Edit"
        Width="100%" DataKeyNames="Code" OnDataBound="FV_TransportationRoute_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${Transportation.TransportationRoute.UpdateTransportationRoute}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Transportation.TransportationRoute.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbCode" runat="server" Text='<%# Bind("Code") %>' />
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
<asp:ObjectDataSource ID="ODS_TransportationRoute" runat="server" TypeName="com.Sconit.Web.TransportationRouteMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.Transportation.TransportationRoute" UpdateMethod="UpdateTransportationRoute"
    OnUpdated="ODS_TransportationRoute_Updated" OnUpdating="ODS_TransportationRoute_Updating" DeleteMethod="DeleteTransportationRoute"
    OnDeleted="ODS_TransportationRoute_Deleted" SelectMethod="LoadTransportationRoute">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="code" Type="String" />
    </DeleteParameters>
</asp:ObjectDataSource>
