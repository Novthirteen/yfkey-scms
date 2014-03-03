<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Transportation_TransportPriceList_WarehouseLease_Edit" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>
<div id="floatdiv">
    <asp:FormView ID="FV_WarehouseLease" runat="server" DataSourceID="ODS_WarehouseLease" DefaultMode="Edit"
        Width="100%" DataKeyNames="Id" OnDataBound="FV_TransportPriceListDetail_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${Transportation.TransportPriceListDetail.UpdateWarehouseLease}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCurrentTransportPriceList" runat="server" Text="${Transportation.TransportPriceListDetail.CurrentTransportPriceList}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbCurrentTransportPriceList" runat="server" />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
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
                            <asp:Literal ID="lblUnitPrice" runat="server" Text="${Transportation.TransportPriceListDetail.UnitPrice.ForWarehouseLease}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbUnitPrice" runat="server" Text='<%#Bind("UnitPrice","{0:0.########}") %>'
                                CssClass="inputRequired" />
                            <asp:CustomValidator ID="cvUnitPrice" runat="server" ControlToValidate="tbUnitPrice"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                            <asp:RequiredFieldValidator ID="rfvUnitPrice" runat="server" ErrorMessage="${Transportation.TransportPriceListDetail.UnitPrice.Empty}"
                                Display="Dynamic" ControlToValidate="tbUnitPrice" ValidationGroup="vgSave" />
                            <asp:RangeValidator ID="rvUnitPrice" ControlToValidate="tbUnitPrice" runat="server"
                                Display="Dynamic" ErrorMessage="${Transportation.TransportPriceListDetail.UnitPrice.Format}" MaximumValue="999999999"
                                MinimumValue="0.00000001" Type="Double" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblRemark" runat="server" Text="${Transportation.TransportPriceListDetail.Remark}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbRemark" runat="server" Text='<%#Bind("Remark") %>' />
                        </td>
                    </tr>
                    <tr>
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
                        <td class="td01">
                            <asp:Literal ID="lblServiceCharge" runat="server" Text="${Transportation.TransportPriceListDetail.ServiceCharge}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbServiceCharge" runat="server" Text='<%#Bind("ServiceCharge","{0:0.########}") %>'
                                CssClass="inputRequired" />
                            <asp:CustomValidator ID="cvServiceCharge" runat="server" ControlToValidate="tbServiceCharge"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                            <asp:RequiredFieldValidator ID="rfvServiceCharge" runat="server" ErrorMessage="${Transportation.TransportPriceListDetail.ServiceCharge.Empty}"
                                Display="Dynamic" ControlToValidate="tbServiceCharge" ValidationGroup="vgSave" />
                            <asp:RangeValidator ID="rvServiceCharge" ControlToValidate="tbServiceCharge" runat="server"
                                Display="Dynamic" ErrorMessage="${Transportation.TransportPriceListDetail.ServiceCharge.Format}" MaximumValue="999999999"
                                MinimumValue="0" Type="Double" ValidationGroup="vgSave" />
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
                            <asp:Literal ID="lblIsIncludeTax" runat="server" Text="${Transportation.TransportPriceListDetail.IsIncludeTax}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsIncludeTax" runat="server" Checked='<%#Bind("IsIncludeTax") %>' />
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
<asp:ObjectDataSource ID="ODS_WarehouseLease" runat="server" TypeName="com.Sconit.Web.TransportPriceListDetailMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.Transportation.TransportPriceListDetail" UpdateMethod="UpdateTransportPriceListDetail"
    OnUpdated="ODS_TransportPriceListDetail_Updated" OnUpdating="ODS_TransportPriceListDetail_Updating" DeleteMethod="DeleteTransportPriceListDetail"
    OnDeleted="ODS_TransportPriceListDetail_Deleted" SelectMethod="LoadTransportPriceListDetail">
    <SelectParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </DeleteParameters>
</asp:ObjectDataSource>
