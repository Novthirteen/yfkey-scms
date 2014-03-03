<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_Flow_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Flow" runat="server" DataSourceID="ODS_Flow" DefaultMode="Insert"
        DataKeyNames="Code">
        <InsertItemTemplate>
            <fieldset>
                <legend>${MasterData.Flow.Basic.Info}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Flow.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Code") %>' CssClass="inputRequired"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="${MasterData.Flow.Code.Required}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvInsert" runat="server" ControlToValidate="tbCode" ErrorMessage="${MasterData.Flow.Code.Exists}"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="checkFlowExists" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${MasterData.Address.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblDescription" runat="server" Text="${MasterData.Flow.Description}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbDescription" runat="server" Text='<%# Bind("Description") %>'
                                CssClass="inputRequired"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="${MasterData.Flow.Description.Required}"
                                Display="Dynamic" ControlToValidate="tbDescription" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblRefFlow" runat="server" Text="${MasterData.Flow.ReferenceFlow}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbRefFlow" runat="server" Visible="true" DescField="Description"
                                ValueField="Code" ServicePath="FlowMgr.service" ServiceMethod="GetAllFlow" Width="250" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblPartyFrom" runat="server" Text="${MasterData.Flow.Party.From.Region.Transfer}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbPartyFrom" runat="server" Visible="true" Width="250" DescField="Name"
                                ValueField="Code" ServicePath="PartyMgr.service" ServiceMethod="GetFromParty"
                                CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvPartyFrom" runat="server" ErrorMessage="${MasterData.Flow.Party.From.Required}"
                                Display="Dynamic" ControlToValidate="tbPartyFrom" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblPartyTo" runat="server" Text="${MasterData.Flow.Party.To.Region.Transfer}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbPartyTo" runat="server" Visible="true" Width="250" DescField="Name"
                                ValueField="Code" ServicePath="PartyMgr.service" ServiceMethod="GetToParty" CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvPartyTo" runat="server" ErrorMessage="${MasterData.Flow.Party.To.Required}"
                                Display="Dynamic" ControlToValidate="tbPartyTo" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblTRoute" runat="server" Text="${MasterData.Flow.TransportationRoute}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbTRoute" runat="server" Width="250" DescField="Description" ValueField="Code"
                                ServiceMethod="GetAllTransportationRoute" ServicePath="TransportationRouteMgr.service" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCarrier" runat="server" Text="${MasterData.Flow.Carrier}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbCarrier" runat="server" Visible="true" DescField="Name" ValueField="Code"
                                Width="250" ServicePath="CarrierMgr.service" ServiceMethod="GetCarrier" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblTPriceList" runat="server" Text="${MasterData.Flow.TransportationPriceList}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbTPriceList" runat="server" DescField="Code" ValueField="Code"
                                ServiceParameter="string:#tbCarrier" Width="250" ServicePath="TransportPriceListMgr.service"
                                ServiceMethod="GetTransportPriceList" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCarrierBillAddress" runat="server" Text="${MasterData.Flow.Carrier.BillAddress}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbCarrierBillAddress" runat="server" Visible="true" DescField="Address"
                                ValueField="Code" ServicePath="AddressMgr.service" ServiceMethod="GetBillAddress"
                                Width="250" ServiceParameter="string:#tbCarrier" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lbCurrency" runat="server" Text="${MasterData.Flow.Currency}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbCurrency" runat="server" Visible="true" DescField="Name" ValueField="Code"
                                ServicePath="CurrencyMgr.service" ServiceMethod="GetAllCurrency" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblMaxOnlineQty" runat="server" Text="${MasterData.Flow.MaxOnlineQty}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMaxOnlineQty" runat="server" Text='<%# Bind("MaxOnlineQty") %>' />
                            <asp:RangeValidator ID="rvMaxOnlineQty" runat="server" ControlToValidate="tbMaxOnlineQty"
                                ErrorMessage="${Common.Validator.Valid.Number}" Display="Dynamic" Type="Integer"
                                MinimumValue="0" MaximumValue="100000" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>${MasterData.Flow.Default.Value}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblLocFrom" runat="server" Text="${MasterData.Flow.Location.From}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbLocFrom" runat="server" Visible="true" DescField="Name" ValueField="Code"
                                CssClass="inputRequired" Width="250" ServicePath="LocationMgr.service" ServiceMethod="GetLocation"
                                ServiceParameter="string:#tbPartyFrom" />
                            <asp:CustomValidator ID="cvLoc" runat="server" ControlToValidate="tbLocFrom" ErrorMessage="${MasterData.Flow.Location.Equal}"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="checkLocation" />
                            <asp:RequiredFieldValidator ID="rfvLocFrom" runat="server" ErrorMessage="${MasterData.Flow.Location.From.Required}"
                                Display="Dynamic" ControlToValidate="tbLocFrom" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblLocTo" runat="server" Text="${MasterData.Flow.Location.To}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbLocTo" runat="server" Visible="true" DescField="Name" ValueField="Code"
                                CssClass="inputRequired" Width="250" ServicePath="LocationMgr.service" ServiceMethod="GetLocation"
                                ServiceParameter="string:#tbPartyTo" />
                            <asp:RequiredFieldValidator ID="rfvLocTo" runat="server" ErrorMessage="${MasterData.Flow.Location.To.Required}"
                                Display="Dynamic" ControlToValidate="tbLocTo" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblOrderTemplate" runat="server" Text="${MasterData.Flow.OrderTemplate}:" />
                        </td>
                        <td class="td02">
                            <cc1:CodeMstrDropDownList ID="ddlOrderTemplate" Code="OrderTemplate" runat="server">
                            </cc1:CodeMstrDropDownList>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblAsnTemplate" runat="server" Text="${MasterData.Flow.ASNTemplate}:" />
                        </td>
                        <td class="td02">
                            <cc1:CodeMstrDropDownList ID="ddlAsnTemplate" Code="AsnTemplate" runat="server">
                            </cc1:CodeMstrDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblReceiptTemplate" runat="server" Text="${MasterData.Flow.ReceiptTemplate}:" />
                        </td>
                        <td class="td02">
                            <cc1:CodeMstrDropDownList ID="ddlReceiptTemplate" Code="ReceiptTemplate" runat="server">
                            </cc1:CodeMstrDropDownList>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblHuTemplate" runat="server" Text="${MasterData.Flow.HuTemplate}:" />
                        </td>
                        <td class="td02">
                            <cc1:CodeMstrDropDownList ID="ddlHuTemplate" Code="HuTemplate" runat="server">
                            </cc1:CodeMstrDropDownList>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>${MasterData.Flow.Control.Option}</legend>
                <table class="mtable">
                    <tr>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsAutoCreate" runat="server" Text="${MasterData.Flow.AutoCreate}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsAutoCreate" runat="server" Checked='<%# Bind("IsAutoCreate") %>'>
                            </asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsAutoRelease" runat="server" Text="${MasterData.Flow.AutoRelease}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsAutoRelease" runat="server" Checked='<%# Bind("IsAutoRelease") %>'>
                            </asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsAutoStart" runat="server" Text="${MasterData.Flow.AutoStart}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsAutoStart" runat="server" Checked='<%# Bind("IsAutoStart") %>'>
                            </asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsAutoShip" runat="server" Text="${MasterData.Flow.AutoShip}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsAutoShip" runat="server" Checked='<%# Bind("IsAutoShip") %>'>
                            </asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsAutoReceive" runat="server" Text="${MasterData.Flow.AutoReceive}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsAutoReceive" runat="server" Checked='<%# Bind("IsAutoReceive") %>'>
                            </asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblNeedPrintASN" runat="server" Text="${MasterData.Flow.PrintASN}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbNeedPrintASN" runat="server" Checked='<%# Bind("NeedPrintASN") %>'>
                            </asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblNeedPrintReceipt" runat="server" Text="${MasterData.Flow.PrintReceipt}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbNeedPrintReceipt" runat="server" Checked='<%# Bind("NeedPrintReceipt") %>'>
                            </asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblNeedPrintOrder" runat="server" Text="${MasterData.Flow.PrintOrder}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbNeedPrintOrder" runat="server" Checked='<%# Bind("NeedPrintOrder") %>'>
                            </asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd01">
                            <asp:Literal ID="lblAllowExceed" runat="server" Text="${MasterData.Flow.AllowExceed}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbAllowExceed" runat="server" Checked='<%# Bind("AllowExceed") %>'>
                            </asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lbAllowCreateDetail" runat="server" Text="${MasterData.Flow.AllowCreateDetail}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbAllowCreateDetail" runat="server" Checked='<%# Bind("AllowCreateDetail") %>'>
                            </asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsListDetail" runat="server" Text="${MasterData.Flow.IsListDetail}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsListDetail" runat="server" Checked='<%# Bind("IsListDetail") %>' />
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lbFulfillUC" runat="server" Text="${MasterData.Flow.FulfillUC}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbFulfillUC" runat="server" Checked='<%# Bind("FulfillUnitCount") %>'>
                            </asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsShipScanHu" runat="server" Text="${MasterData.Flow.IsShipScanHu}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsShipScanHu" runat="server" Checked='<%# Bind("IsShipScanHu") %>'>
                            </asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsReceiptScanHu" runat="server" Text="${MasterData.Flow.IsReceiptScanHu}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsReceiptScanHu" runat="server" Checked='<%# Bind("IsReceiptScanHu") %>'>
                            </asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblCreateHuOption" runat="server" Text="${MasterData.Flow.CreateHuOption}:" />
                        </td>
                        <td class="ttd02">
                            <cc1:CodeMstrDropDownList ID="ddlCreateHuOption" Code="CreateHuOption" runat="server">
                            </cc1:CodeMstrDropDownList>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblAutoPrintHu" runat="server" Text="${MasterData.Flow.AutoPrintHu}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbAutoPrintHu" runat="server" Checked='<%# Bind("AutoPrintHu") %>'>
                            </asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsOddCreateHu" runat="server" Text="${MasterData.Flow.IsOddCreateHu}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsOddCreateHu" runat="server" Checked='<%# Bind("IsOddCreateHu") %>'>
                            </asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lbIsAutoCreatePL" runat="server" Text="${MasterData.Flow.IsAutoCreatePL}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsAutoCreatePL" runat="server" Checked='<%# Bind("IsAutoCreatePickList") %>'>
                            </asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsGoodsReceiveFIFO" runat="server" Text="${MasterData.Flow.IsGoodsReceiveFIFO}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsGoodsReceiveFIFO" runat="server" Checked='<%# Bind("IsGoodsReceiveFIFO") %>'>
                            </asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lbIsPickFromBin" runat="server" Text="${MasterData.Flow.IsPickFromBin}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsPickFromBin" runat="server" Checked='<%# Bind("IsPickFromBin") %>'>
                            </asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsAsnUniqueReceipt" runat="server" Text="${MasterData.Flow.IsAsnUniqueReceipt}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsAsnUniqueReceipt" runat="server" Checked='<%# Bind("IsAsnUniqueReceipt") %>'>
                            </asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblCheckDetailOption" runat="server" Text="${MasterData.Flow.CheckDetailOption}:" />
                        </td>
                        <td class="ttd02">
                            <cc1:CodeMstrDropDownList ID="ddlCheckDetailOption" Code="CheckOrderDetOption" runat="server">
                            </cc1:CodeMstrDropDownList>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblGrGapTo" runat="server" Text="${MasterData.Flow.GrGapTo}:" />
                        </td>
                        <td class="ttd02">
                            <cc1:CodeMstrDropDownList ID="ddlGrGapTo" Code="GrGapTo" runat="server">
                            </cc1:CodeMstrDropDownList>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lbIsShipByOrder" runat="server" Text="${MasterData.Flow.IsShipByOrder}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsShipByOrder" runat="server" Checked='<%# Bind("IsShipByOrder") %>'>
                            </asp:CheckBox>
                        </td>
                    </tr>
                      <tr>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsMRP" runat="server" Text="${MasterData.Flow.IsMRP}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsMRP" runat="server" Checked='<%# Bind("IsMRP") %>' />
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsMes" runat="server" Text="${MasterData.Flow.IsMes}:" />
                        </td>
                        <td class="ttd02">
                             <asp:CheckBox ID="cbIsMes" runat="server" Checked='<%# Bind("IsMes") %>' />
                        </td>
                        <td class="ttd01">
                        </td>
                        <td class="ttd02">
                        </td>
                        <td class="ttd01">
                        </td>
                        <td class="ttd02">
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div class="tablefooter">
                <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="${Common.Button.Save}"
                    CssClass="button2" ValidationGroup="vgSave" />
                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                    CssClass="button2" />
            </div>
        </InsertItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Flow" runat="server" TypeName="com.Sconit.Web.FlowMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Flow" InsertMethod="CreateFlow"
    OnInserted="ODS_Flow_Inserted" OnInserting="ODS_Flow_Inserting" SelectMethod="LoadFlow">
    <SelectParameters>
        <asp:Parameter Type="String" Name="code" />
    </SelectParameters>
    <InsertParameters>
        <asp:Parameter Name="MaxOnlineQty" Type="Int32" ConvertEmptyStringToNull="true" />
    </InsertParameters>
</asp:ObjectDataSource>
