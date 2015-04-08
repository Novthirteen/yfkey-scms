<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Transportation_TransportPriceList_TransportPriceListDetail_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<div id="floatdiv">
    <asp:FormView ID="FV_TransportPriceListDetail" runat="server" DataSourceID="ODS_TransportPriceListDetail"
        DefaultMode="Edit" Width="100%" DataKeyNames="Id" OnDataBound="FV_TransportPriceListDetail_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${Transportation.TransportPriceListDetail.UpdateTransportPriceListDetail}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCurrentTransportPriceList" runat="server" Text="${Transportation.TransportPriceListDetail.CurrentTransportPriceList}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbCurrentTransportPriceList" runat="server" />
                        </td>
                          <td class="td01">
                            <asp:Literal ID="lblIsProvisionalEstimate" runat="server" Text="${Transportation.TransportPriceListDetail.IsProvisionalEstimate}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsProvisionalEstimate" runat="server" Checked='<%#Bind("IsProvisionalEstimate") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblShipFrom" runat="server" Text="${Transportation.TransportPriceListDetail.ShipFrom}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbShipFrom" runat="server" Width="250" DescField="Empty" ValueField="FullAddressAndId"
                                CssClass="inputRequired" MustMatch="true" ServiceMethod="GetAllTransportationAddress"
                                ServicePath="TransportationAddressMgr.service" />
                            <asp:RequiredFieldValidator ID="rfvShipFrom" runat="server" ErrorMessage="${Transportation.TransportPriceListDetail.ShipFrom.Empty}"
                                Display="Dynamic" ControlToValidate="tbShipFrom" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvShipFrom" runat="server" ControlToValidate="tbShipFrom"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblShipTo" runat="server" Text="${Transportation.TransportPriceListDetail.ShipTo}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbShipTo" runat="server" Width="250" DescField="Empty" ValueField="FullAddressAndId"
                                CssClass="inputRequired" MustMatch="true" ServiceMethod="GetAllTransportationAddress"
                                ServicePath="TransportationAddressMgr.service" />
                            <asp:RequiredFieldValidator ID="rfvShipTo" runat="server" ErrorMessage="${Transportation.TransportPriceListDetail.ShipTo.Empty}"
                                Display="Dynamic" ControlToValidate="tbShipTo" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvShipTo" runat="server" ControlToValidate="tbShipTo" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblStartDate" runat="server" Text="${Transportation.TransportPriceListDetail.StartDate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbStartDate" runat="server" Text='<%# Bind("StartDate") %>' Enabled="false" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblEndDate" runat="server" Text="${Transportation.TransportPriceListDetail.EndDate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbEndDate" runat="server" Text='<%#Bind("EndDate") %>' onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                            <asp:CustomValidator ID="cvEndDate" runat="server" ControlToValidate="tbEndDate"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblPricingMethod" runat="server" Text="${Transportation.TransportPriceListDetail.PricingMethod}:" />
                        </td>
                        <td class="td02">
                            <cc1:CodeMstrDropDownList ID="ddlPricingMethod" Code="PricingMethod" runat="server"
                                IncludeBlankOption="false" AutoPostBack="true" OnSelectedIndexChanged="ddlPricingMethod_SelectedIndexChanged" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblVehicleType" runat="server" Text="${Transportation.TransportPriceListDetail.VehicleType}:" />
                        </td>
                        <td class="td02">
                            <asp:DropDownList ID="ddlVehicleType" runat="server" DataTextField="Description" DataValueField="Value" />
                        </td>
                    </tr>
                     <tr>
                        <td class="td01">
                            <asp:Literal ID="lblStartQty" runat="server" Text="${Transportation.TransportPriceListDetail.StartQty}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbStartQty" runat="server" Text='<%#Bind("StartQty","{0:0.########}") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblEndQty" runat="server" Text="${Transportation.TransportPriceListDetail.EndQty}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbEndQty" runat="server" Text='<%#Bind("EndQty","{0:0.########}") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblUnitPrice" runat="server" Text="${Transportation.TransportPriceListDetail.UnitPrice}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbUnitPrice" runat="server" Text='<%#Bind("UnitPrice","{0:0.########}") %>'
                                CssClass="inputRequired" />
                            <asp:CustomValidator ID="cvUnitPrice" runat="server" ControlToValidate="tbUnitPrice"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                            <asp:RequiredFieldValidator ID="rfvUnitPrice" runat="server" ErrorMessage="${Transportation.TransportPriceListDetail.UnitPrice.Empty}"
                                Display="Dynamic" ControlToValidate="tbUnitPrice" ValidationGroup="vgSave" />
                            <asp:RangeValidator ID="rvUnitPrice" ControlToValidate="tbUnitPrice" runat="server"
                                Display="Dynamic" ErrorMessage="${Transportation.TransportPriceListDetail.UnitPrice.Format}"
                                MaximumValue="999999999" MinimumValue="0.00000001" Type="Double" ValidationGroup="vgSave" />
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
                            <asp:Literal ID="lblMinVolume" runat="server" Text="${Transportation.TransportPriceListDetail.MinVolume}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMinVolume" runat="server" Text='<%#Bind("MinVolume","{0:0.########}") %>'
                                CssClass="inputRequired" />
                            <asp:CustomValidator ID="cvMinVolume" runat="server" ControlToValidate="tbMinVolume"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                            <asp:RequiredFieldValidator ID="rfvMinVolume" runat="server" ErrorMessage="${Transportation.TransportPriceListDetail.MinVolume.Empty}"
                                Display="Dynamic" ControlToValidate="tbMinVolume" ValidationGroup="vgSave" />
                            <asp:RangeValidator ID="rvMinVolume" ControlToValidate="tbMinVolume" runat="server"
                                Display="Dynamic" ErrorMessage="${Transportation.TransportPriceListDetail.MinVolume.Format}"
                                MaximumValue="999999999" MinimumValue="0" Type="Double" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlMinPrice" runat="server" Text="${Transportation.TransportPriceListDetail.MinPrice}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMinPrice" runat="server" Text='<%#Bind("MinPrice","{0:0.########}") %>'/>
                            <asp:CustomValidator ID="cvMinPrice" runat="server" ControlToValidate="tbMinPrice"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                            <asp:RangeValidator ID="rfMinPrice" ControlToValidate="tbMinPrice" runat="server"
                                Display="Dynamic" ErrorMessage="${Transportation.TransportPriceListDetail.UnitPrice.Format}"
                                MaximumValue="999999999" MinimumValue="0" Type="Double" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblTaxCode" runat="server" Text="${Transportation.TransportPriceListDetail.TaxCode}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbTaxCode" runat="server" Text='<%#Bind("TaxCode") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblTransportMethod" runat="server" Text="${Transportation.TransportPriceListDetail.TransportMethod}:" />
                            <%--<asp:TextBox ID="tbTransportMethod" runat="server" Text='<%#Bind("TransportMethod") %>' />--%>
                        </td>
                        <td class="td02">
                            <cc1:CodeMstrDropDownList ID="ddlTransportMethod" Code="TransportMethod" runat="server"
                                IncludeBlankOption="false" AutoPostBack="true"  />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblIsIncludeTax" runat="server" Text="${Transportation.TransportPriceListDetail.IsIncludeTax}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsIncludeTax" runat="server" Checked='<%#Bind("IsIncludeTax") %>' />
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
<asp:ObjectDataSource ID="ODS_TransportPriceListDetail" runat="server" TypeName="com.Sconit.Web.TransportPriceListDetailMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.Transportation.TransportPriceListDetail"
    UpdateMethod="UpdateTransportPriceListDetail" OnUpdated="ODS_TransportPriceListDetail_Updated"
    OnUpdating="ODS_TransportPriceListDetail_Updating" DeleteMethod="DeleteTransportPriceListDetail"
    OnDeleted="ODS_TransportPriceListDetail_Deleted" SelectMethod="LoadTransportPriceListDetail">
    <SelectParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </DeleteParameters>
</asp:ObjectDataSource>
