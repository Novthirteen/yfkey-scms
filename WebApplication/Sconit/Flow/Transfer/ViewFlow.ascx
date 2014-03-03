<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewFlow.ascx.cs" Inherits="MasterData_Flow_ViewFlow" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Flow" runat="server" DataSourceID="ODS_Flow" DefaultMode="Edit"
        DataKeyNames="Code" OnDataBound="FV_Flow_DataBound">
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
                            <asp:Literal ID="lblPartyFrom" runat="server" Text="${MasterData.Flow.Party.From.Region.Transfer}:" />
                        </td>
                        <td class="td02">
                            <sc1:ReadonlyTextBox ID="tbPartyFrom" runat="server" CodeField="PartyFrom.Code" DescField="PartyFrom.Name" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblPartyTo" runat="server" Text="${MasterData.Flow.Party.To.Region.Transfer}:" />
                        </td>
                        <td class="td02">
                            <sc1:ReadonlyTextBox ID="tbPartyTo" runat="server" CodeField="PartyTo.Code" DescField="PartyTo.Name" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblMaxOnlineQty" runat="server" Text="${MasterData.Flow.MaxOnlineQty}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lMaxOnlineQty" runat="server" Text='<%# Bind("MaxOnlineQty") %>' />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
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
                                <asp:CheckBox ID="cbIsAsnUniqueReceipt" runat="server" Checked='<%# Bind("IsAsnUniqueReceipt") %>' Enabled="false">
                                </asp:CheckBox>
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
                    </tr>
                </table>
            </fieldset>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Flow" runat="server" TypeName="com.Sconit.Web.FlowMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Flow" SelectMethod="LoadFlow">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
