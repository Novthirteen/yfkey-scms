<%@ Control Language="C#" AutoEventWireup="true" CodeFile="View.ascx.cs" Inherits="Visualization_FlowTrack_View" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_FlowTrack" runat="server" DataSourceID="ODS_FlowTrack" DefaultMode="Edit"
        DataKeyNames="Id" OnDataBound="FV_FlowTrack_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.Flow.Basic.Info}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Flow.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lCode" runat="server" Text='<%# Eval("Code") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${MasterData.Address.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%# Eval("IsActive") %>' Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblDescription" runat="server" Text="${MasterData.Flow.Description}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lDescription" runat="server" Text='<%# Eval("Description") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblRefFlow" runat="server" Text="${MasterData.Flow.ReferenceFlow}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lRefFlow" runat="server" Text='<%# Eval("ReferenceFlow") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblType" runat="server" Text="${Common.Business.Type}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbType" runat="server" Text='<%# Eval("Type") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblVersion" runat="server" Text="${MasterData.FlowTrack.Version}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbVersion" runat="server" Text='<%# Eval("Version") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblEventCode" runat="server" Text="${MasterData.FlowTrack.EventCode}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbEventCode" runat="server" Text='<%# Eval("EventCode") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblPartyFrom" runat="server" Text="${MasterData.FlowTrack.PartyFrom}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbPartyFrom" runat="server" Text='<%# Eval("PartyFrom") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblPartyTo" runat="server" Text="${MasterData.FlowTrack.PartyTo}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbPartyTo" runat="server" Text='<%# Eval("PartyTo") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblLocationFrom" runat="server" Text="${MasterData.Flow.Location.From}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbLocationFrom" runat="server" Text='<%# Eval("LocationFrom") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblLocationTo" runat="server" Text="${MasterData.Flow.Location.From}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbLocationTo" runat="server" Text='<%# Eval("LocationTo") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblShipFrom" runat="server" Text="${MasterData.Flow.Ship.From}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbShipFrom" runat="server" Text='<%# Eval("ShipFrom") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblShipTo" runat="server" Text="${MasterData.Flow.Ship.To}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbShipTo" runat="server" Text='<%# Eval("ShipTo") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblBillFrom" runat="server" Text="${MasterData.Flow.Bill.From}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbBillFrom" runat="server" Text='<%# Eval("BillFrom") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblBillTo" runat="server" Text="${MasterData.Flow.Bill.To}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbBillTo" runat="server" Text='<%# Eval("BillTo") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblPriceListFrom" runat="server" Text="${MasterData.Flow.PriceList.From}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbPriceListFrom" runat="server" Text='<%# Eval("PriceListFrom") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblPriceListTo" runat="server" Text="${MasterData.Flow.PriceList.To}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbPriceListTo" runat="server" Text='<%# Eval("PriceListTo") %>' />
                        </td>
                    </tr>
                    <tr id="trCarrier" runat="server">
                        <td class="td01">
                            <asp:Literal ID="lblDockDescription" runat="server" Text="${MasterData.Flow.DockDescription}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lDockDescription" runat="server" Text='<%# Eval("DockDescription") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCarrier" runat="server" Text="${MasterData.Flow.Carrier}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbCarrier" runat="server" Text='<%# Eval("Carrier") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCurrency" runat="server" Text="${MasterData.Flow.Currency}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbCurrency" runat="server" Text='<%# Eval("Currency") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCarrierBillAddress" runat="server" Text="${MasterData.Flow.Carrier.BillAddress}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbCarrierBillAddress" runat="server" Text='<%# Eval("CarrierBillAddress") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblRouting" runat="server" Text="${MasterData.Flow.Routing.Production}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lRouting" runat="server" Text='<%# Eval("Routing") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblMaxOnlineQty" runat="server" Text="${MasterData.Flow.MaxOnlineQty}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lMaxOnlineQty" runat="server" Text='<%# Eval("MaxOnlineQty") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblOrderTemplate" runat="server" Text="${MasterData.Flow.OrderTemplate}:" />
                        </td>
                        <td class="td02">
                            <sc1:CodeMstrLabel ID="lOrderTemplate" Code="OrderTemplate" runat="server" Value='<%# Bind("OrderTemplate") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblAsnTemplate" runat="server" Text="${MasterData.Flow.ASNTemplate}:" />
                        </td>
                        <td class="td02">
                            <sc1:CodeMstrLabel ID="lAsnTemplate" Code="AsnTemplate" runat="server" Value='<%# Bind("ASNTemplate") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblReceiptTemplate" runat="server" Text="${MasterData.Flow.ReceiptTemplate}:" />
                        </td>
                        <td class="td02">
                            <sc1:CodeMstrLabel ID="lReceiptTemplate" Code="ReceiptTemplate" runat="server" Value='<%# Bind("ReceiptTemplate") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblHuTemplate" runat="server" Text="${MasterData.Flow.HuTemplate}:" />
                        </td>
                        <td class="td02">
                            <sc1:CodeMstrLabel ID="lHuTemplate" Code="HuTemplate" runat="server" Value='<%# Bind("HuTemplate") %>' />
                        </td>
                    </tr>
                     <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCreateDate" runat="server" Text="${MasterData.FlowTrack.CreateDate}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbCreateDate" runat="server" Text='<%# Eval("CreateDate") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCreateUser" runat="server" Text="${MasterData.FlowTrack.CreateUser}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbCreateUser" runat="server" Text='<%# Eval("CreateUser") %>' />
                        </td>
                    </tr>
                     <tr>
                        <td class="td01">
                            <asp:Literal ID="lblLastModifyDate" runat="server" Text="${MasterData.FlowTrack.LastModifyDate}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbLastModifyDate" runat="server" Text='<%# Eval("LastModifyDate") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblLastModifyUser" runat="server" Text="${MasterData.FlowTrack.LastModifyUser}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbLastModifyUser" runat="server" Text='<%# Eval("LastModifyUser") %>' />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>${MasterData.Flow.Control.Option}</legend>
                <table class="mtable">
                    <tr id="trOption1" runat="server">
                        <td class="ttd01">
                            <asp:Literal ID="lblIsAutoCreate" runat="server" Text="${MasterData.Flow.AutoCreate}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsAutoCreate" runat="server" Checked='<%# Bind("IsAutoCreate") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsAutoRelease" runat="server" Text="${MasterData.Flow.AutoRelease}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsAutoRelease" runat="server" Checked='<%# Bind("IsAutoRelease") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsAutoStart" runat="server" Text="${MasterData.Flow.AutoStart}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsAutoStart" runat="server" Checked='<%# Bind("IsAutoStart") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsAutoShip" runat="server" Text="${MasterData.Flow.AutoShip}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsAutoShip" runat="server" Checked='<%# Bind("IsAutoShip") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsAutoReceive" runat="server" Text="${MasterData.Flow.AutoReceive}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsAutoReceive" runat="server" Checked='<%# Bind("IsAutoReceive") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblNeedPrintASN" runat="server" Text="${MasterData.Flow.PrintASN}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbNeedPrintASN" runat="server" Checked='<%# Bind("NeedPrintASN") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblNeedPrintReceipt" runat="server" Text="${MasterData.Flow.PrintReceipt}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbNeedPrintReceipt" runat="server" Checked='<%# Bind("NeedPrintReceipt") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblNeedPrintOrder" runat="server" Text="${MasterData.Flow.PrintOrder}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbNeedPrintOrder" runat="server" Checked='<%# Bind("NeedPrintOrder") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd01">
                            <asp:Literal ID="lblAllowExceed" runat="server" Text="${MasterData.Flow.AllowExceed}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbAllowExceed" runat="server" Checked='<%# Bind("AllowExceed") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lbAllowCreateDetail" runat="server" Text="${MasterData.Flow.AllowCreateDetail}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbAllowCreateDetail" runat="server" Checked='<%# Bind("AllowCreateDetail") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsListDetail" runat="server" Text="${MasterData.Flow.IsListDetail}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsListDetail" runat="server" Checked='<%# Bind("IsListDetail") %>'
                                Enabled="false" />
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lbFulfillUC" runat="server" Text="${MasterData.Flow.FulfillUC}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbFulfillUC" runat="server" Checked='<%# Bind("FulfillUnitCount") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsShipScanHu" runat="server" Text="${MasterData.Flow.IsShipScanHu}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsShipScanHu" runat="server" Checked='<%# Bind("IsShipScanHu") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsReceiptScanHu" runat="server" Text="${MasterData.Flow.IsReceiptScanHu}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsReceiptScanHu" runat="server" Checked='<%# Bind("IsReceiptScanHu") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblCreateHuOption" runat="server" Text="${MasterData.Flow.CreateHuOption}:" />
                        </td>
                        <td class="ttd02">
                            <sc1:CodeMstrLabel ID="ddlCreateHuOption" Code="CreateHuOption" runat="server" Value='<%# Bind("CreateHuOption") %>'>
                            </sc1:CodeMstrLabel>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblAutoPrintHu" runat="server" Text="${MasterData.Flow.AutoPrintHu}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbAutoPrintHu" runat="server" Checked='<%# Bind("AutoPrintHu") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsOddCreateHu" runat="server" Text="${MasterData.Flow.IsOddCreateHu}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsOddCreateHu" runat="server" Checked='<%# Bind("IsOddCreateHu") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lbIsAutoCreatePL" runat="server" Text="${MasterData.Flow.IsAutoCreatePL}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsAutoCreatePL" runat="server" Checked='<%# Bind("IsAutoCreatePickList") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsGoodsReceiveFIFO" runat="server" Text="${MasterData.Flow.IsGoodsReceiveFIFO}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsGoodsReceiveFIFO" runat="server" Checked='<%# Bind("IsGoodsReceiveFIFO") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lbIsPickFromBin" runat="server" Text="${MasterData.Flow.IsPickFromBin}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsPickFromBin" runat="server" Checked='<%# Bind("IsPickFromBin") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsAsnUniqueReceipt" runat="server" Text="${MasterData.Flow.IsAsnUniqueReceipt}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsAsnUniqueReceipt" runat="server" Checked='<%# Bind("IsAsnUniqueReceipt") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblCheckDetailOption" runat="server" Text="${MasterData.Flow.CheckDetailOption}:" />
                        </td>
                        <td class="ttd02">
                            <sc1:CodeMstrLabel ID="lCheckDetailOption" runat="server" Code="CheckOrderDetOption"
                                Value='<%# Bind("CheckDetailOption") %>' />
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblGrGapTo" runat="server" Text="${MasterData.Flow.GrGapTo}:" />
                        </td>
                        <td class="ttd02">
                            <sc1:CodeMstrLabel ID="lGrGapTo" runat="server" Code="GrGapTo" Value='<%# Bind("GoodsReceiptGapTo") %>' />
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lbIsShipByOrder" runat="server" Text="${MasterData.Flow.IsShipByOrder}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsShipByOrder" runat="server" Checked='<%# Bind("IsShipByOrder") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsAutoBill" runat="server" Text="${MasterData.Flow.AutoBill}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsAutoBill" runat="server" Checked='<%# Bind("IsAutoBill") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsShowPrice" runat="server" Text="${MasterData.Flow.IsShowPrice}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsShowPrice" runat="server" Checked='<%# Bind("IsShowPrice") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblBillSettleTerm" runat="server" Text="${MasterData.Flow.Flow.BillSettleTerm}:" />
                        </td>
                        <td class="ttd02">
                            <sc1:CodeMstrLabel ID="ddlBillSettleTerm" runat="server" Code="BillSettleTerm" Value='<%# Bind("BillSettleTerm") %>' />
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lbNeedInspect" runat="server" Text="${MasterData.Flow.NeedInspect}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbNeedInspect" runat="server" Checked='<%# Bind("NeedInspection") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd01">
                            <asp:Literal ID="lblAntiResolveHu" runat="server" Text="${MasterData.Flow.AntiResolveHu}:" />
                        </td>
                        <td class="ttd02">
                            <sc1:CodeMstrLabel ID="ddlAntiResolveHu" runat="server" Code="AntiResolveHu" Value='<%# Bind("AntiResolveHu") %>' />
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblAllowRepeatlyExceed" runat="server" Text="${MasterData.Flow.AllowRepeatlyExceed}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbAllowRepeatlyExceed" runat="server" Checked='<%# Bind("AllowRepeatlyExceed") %>'
                                Enabled="false"></asp:CheckBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>${MasterData.FlowTrack.Strategy}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblFlowStrategy" runat="server" Text="${MasterData.FlowTrack.Strategy}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lFlowStrategy" runat="server" Text='<%# Bind("FlowStrategy") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblLotGroup" runat="server" Text="${MasterData.Flow.Strategy.LotGroup}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lLotGroup" runat="server" Text='<%# Bind("LotGroup") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblStartLatency" runat="server" Text="${MasterData.Flow.Strategy.StartLatency}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lStartLatency" runat="server" Text='<%# Bind("StartLatency","{0:0.########}") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCompleteLatency" runat="server" Text="${MasterData.Flow.Strategy.CompleteLatency}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbCompleteLatency" runat="server" Text='<%# Bind("CompleteLatency","{0:0.########}") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblLeadTime" runat="server" Text="${MasterData.Flow.Strategy.LeadTime}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lLeadTime" runat="server" Text='<%# Bind("LeadTime","{0:0.########}") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblWeekInterval" runat="server" Text="${MasterData.Flow.Strategy.WeekInterval}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lWeekInterval" runat="server" Text='<%# Bind("WeekInterval") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblNextOrderTime" runat="server" Text="${MasterData.Flow.Strategy.NextOrderTime}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lNextOrderTime" runat="server" Text='<%# Bind("NextOrderTime") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblNextWinTime" runat="server" Text="${MasterData.Flow.Strategy.NextWinTime}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lNextWinTime" runat="server" Text='<%# Bind("NextWinTime") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime1" runat="server" Text="${MasterData.Flow.Strategy.WinTime1}:" />
                        </td>
                        <td class="td02">
                            <asp:Label ID="lWinTime1" runat="server" Text='<%# Bind("WinTime1") %>' CssClass="wordbreak" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime2" runat="server" Text="${MasterData.Flow.Strategy.WinTime2}:" />
                        </td>
                        <td class="td02">
                            <asp:Label ID="lWinTime2" runat="server" Text='<%# Bind("WinTime2") %>' CssClass="wordbreak" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime3" runat="server" Text="${MasterData.Flow.Strategy.WinTime3}:" />
                        </td>
                        <td class="td02">
                            <asp:Label ID="lWinTime3" runat="server" Text='<%# Bind("WinTime3") %>' CssClass="wordbreak" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime4" runat="server" Text="${MasterData.Flow.Strategy.WinTime4}:" />
                        </td>
                        <td class="td02">
                            <asp:Label ID="lWinTime4" runat="server" Text='<%# Bind("WinTime4") %>' CssClass="wordbreak" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime5" runat="server" Text="${MasterData.Flow.Strategy.WinTime5}:" />
                        </td>
                        <td class="td02">
                            <asp:Label ID="tbWinTime5" runat="server" Text='<%# Bind("WinTime5") %>' CssClass="wordbreak" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime6" runat="server" Text="${MasterData.Flow.Strategy.WinTime6}:" />
                        </td>
                        <td class="td02">
                            <asp:Label ID="lWinTime6" runat="server" Text='<%# Bind("WinTime6") %>' CssClass="wordbreak" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime7" runat="server" Text="${MasterData.Flow.Strategy.WinTime7}:" />
                        </td>
                        <td class="td02" colspan="3">
                            <asp:Label ID="tbWinTime7" runat="server" Text='<%# Bind("WinTime7") %>' />
                        </td>
                    </tr>
                </table>
            </fieldset>
             <div class="tablefooter">
                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                    CssClass="button2" />
            </div>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_FlowTrack" runat="server" TypeName="com.Sconit.Web.FlowTrackMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.FlowTrack" SelectMethod="LoadFlowTrack">
    <SelectParameters>
        <asp:Parameter Name="id" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
