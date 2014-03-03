<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Transportation_TransportationRoute_TransportationRouteDetail_Edit" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="floatdiv">
    <asp:FormView ID="FV_TransportationRouteDetail" runat="server" DataSourceID="ODS_TransportationRouteDetail" DefaultMode="Edit"
        Width="100%" DataKeyNames="Id" OnDataBound="FV_TransportationRouteDetail_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${Transportation.TransportationRouteDetail.UpdateTransportationRouteDetail}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCurrentTransportationRoute" runat="server" Text="${Transportation.TransportationRouteDetail.CurrentTransportationRoute}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbCurrentTransportationRoute" runat="server" />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblSequence" runat="server" Text="${Transportation.TransportationRouteDetail.Sequence}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbSequence" runat="server" Text='<%# Bind("Sequence") %>' CssClass="inputRequired"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSequence" runat="server" ErrorMessage="${Transportation.TransportationRouteDetail.Sequence.Empty}"
                                Display="Dynamic" ControlToValidate="tbSequence" ValidationGroup="vgSave" />
                            <asp:RangeValidator ID="rvSequence" runat="server" ErrorMessage="${Common.Validator.Valid.Number}" 
                                Display="Dynamic" ControlToValidate="tbSequence" ValidationGroup="vgSave"
                                Type="Integer" MinimumValue="1" MaximumValue="100000" />
                            <asp:CustomValidator ID="cvSequence" runat="server" ErrorMessage="${Transportation.TransportationRouteDetail.Sequence.Exists}"
                                Display="Dynamic" ControlToValidate="tbSequence" ValidationGroup="vgSave" 
                                OnServerValidate="checkSeqExists" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblTAddress" runat="server" Text="${Transportation.TransportationRouteDetail.TAddress}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbTAddress" runat="server" Width="250" DescField="Empty" ValueField="FullAddressAndId"
                                CssClass="inputRequired" MustMatch="true" ServiceMethod="GetAllTransportationAddress" ServicePath="TransportationAddressMgr.service"/>
                            <asp:RequiredFieldValidator ID="rfvTAddress" runat="server" ErrorMessage="${Transportation.TransportationRoute.TAddress.Empty}"
                                Display="Dynamic" ControlToValidate="tbTAddress" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvTAddress" runat="server" ControlToValidate="tbTAddress" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
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
<asp:ObjectDataSource ID="ODS_TransportationRouteDetail" runat="server" TypeName="com.Sconit.Web.TransportationRouteDetailMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.Transportation.TransportationRouteDetail" UpdateMethod="UpdateTransportationRouteDetail"
    OnUpdated="ODS_TransportationRouteDetail_Updated" OnUpdating="ODS_TransportationRouteDetail_Updating" DeleteMethod="DeleteTransportationRouteDetail"
    OnDeleted="ODS_TransportationRouteDetail_Deleted" SelectMethod="LoadTransportationRouteDetail">
    <SelectParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </DeleteParameters>
</asp:ObjectDataSource>
