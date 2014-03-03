<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Transportation_TransportPriceList_Operation_New" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>
<div id="floatdiv">
    <asp:FormView ID="FV_Operation" runat="server" DataSourceID="ODS_Operation" DefaultMode="Insert"
        DataKeyNames="Id">
        <InsertItemTemplate>
            <fieldset>
                <legend>${Transportation.TransportPriceListDetail.AddOperation}</legend>
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
                            <asp:TextBox ID="tbStartDate" runat="server" Text='<%# Bind("StartDate") %>' CssClass="inputRequired" 
                                onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="${Transportation.TransportPriceListDetail.StartDate.Empty}"
                                Display="Dynamic" ControlToValidate="tbStartDate" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvStartDate" runat="server" ControlToValidate="tbStartDate"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
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
                            <asp:Literal ID="lblItem" runat="server" Text="${MasterData.Item.Code}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbItem" runat="server" DescField="Description" ValueField="Code"
                                Width="250" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" MustMatch="true" />
                            <asp:CustomValidator ID="cvItem" runat="server" ControlToValidate="tbItem" ErrorMessage="*"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblBillingMethod" runat="server" Text="${Transportation.TransportPriceListDetail.BillingMethod}:" />
                        </td>
                        <td class="td02">
                            <cc1:CodeMstrDropDownList ID="ddlBillingMethod" Code="BillingMethod" runat="server" IncludeBlankOption="false" />
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
                                Display="Dynamic" ErrorMessage="${Transportation.TransportPriceListDetail.UnitPrice.Format}" MaximumValue="999999999"
                                MinimumValue="0.00000001" Type="Double" ValidationGroup="vgSave" />
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
                        <td class="td01">
                        </td>
                        <td class="td02">
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
                            <asp:CustomValidator ID="cvBillingMethod" runat="server"
                             Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                <div class="tablefooter">
                    <div class="buttons">
                        <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="${Common.Button.Save}"
                            CssClass="add" ValidationGroup="vgSave" />
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                            CssClass="back" />
                    </div>
                </div>
            </fieldset>
        </InsertItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Operation" runat="server" TypeName="com.Sconit.Web.TransportPriceListDetailMgrProxy"
    InsertMethod="CreateTransportPriceListDetail" DataObjectTypeName="com.Sconit.Entity.Transportation.TransportPriceListDetail"
    OnInserted="ODS_TransportPriceListDetail_Inserted" OnInserting="ODS_TransportPriceListDetail_Inserting"></asp:ObjectDataSource>
