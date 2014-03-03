<%@ Control Language="C#" AutoEventWireup="true" CodeFile="View.ascx.cs" Inherits="Visualization_FlowDetailTrack_View" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_FlowDetailTrack" runat="server" DataSourceID="ODS_FlowDetailTrack"
        DefaultMode="ReadOnly" OnDataBound="FV_FlowDetailTrack_DataBound" DataKeyNames="Id">
        <ItemTemplate>
            <fieldset>
                <legend>${MasterData.Flow.FlowDetail.Basic.Info}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblSeq" runat="server" Text="${MasterData.Flow.FlowDetail.Sequence}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbSeq" runat="server" Text='<%# Bind("Sequence") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblFlow" runat="server" Text="${MasterData.FlowDetailTrack.FlowDetail.Flow}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbFlow" runat="server" Text='<%# Bind("Flow") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblItemCode" runat="server" Text="${MasterData.Flow.FlowDetail.ItemCode}:" />
                        </td>
                        <td class="td02">
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblUom" runat="server" Text="${MasterData.Flow.FlowDetail.Uom}:" />
                        </td>
                        <td class="td02">
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblUC" runat="server" Text="${MasterData.Flow.FlowDetail.UnitCount}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbUc" runat="server" Text='<%# Bind("UnitCount","{0:0.########}") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblHuLotSize" runat="server" Text="${MasterData.Flow.FlowDetail.HuLotSize}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbHuLotSize" runat="server" Text='<%# Bind("HuLotSize") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblStartDate" runat="server" Text="${MasterData.Flow.FlowDetail.StartDate}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbStartDate" runat="server" Text='<%# Bind("StartDate") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblEndDate" runat="server" Text="${MasterData.Flow.FlowDetail.EndDate}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbEndDate" runat="server" Text='<%# Bind("EndDate") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblSafeStock" runat="server" Text="${MasterData.Flow.FlowDetail.SafeStock}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbSafeStock" runat="server" Text='<%# Bind("SafeStock","{0:0.########}") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblMaxStock" runat="server" Text="${MasterData.Flow.FlowDetail.MaxStock}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbMaxStock" runat="server" Text='<%# Bind("MaxStock","{0:0.########}") %>' />
                        </tdLiteral
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblMinLotSize" runat="server" Text="${MasterData.Flow.FlowDetail.MinLotSize}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbMinLotSize" runat="server" Text='<%# Bind("MinLotSize","{0:0.########}") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblOrderLotSize" runat="server" Text="${MasterData.Flow.FlowDetail.OrderLotSize}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbOrderLotSize" runat="server" Text='<%# Bind("OrderLotSize","{0:0.########}") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblOrderGoodsReceiptLotSize" runat="server" Text="${MasterData.Flow.FlowDetail.GoodsReceiptLotSize}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbOrderGoodsReceiptLotSize" runat="server" Text='<%# Bind("GoodsReceiptLotSize","{0:0.########}") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblRefItemCode" runat="server" Text="${MasterData.Flow.FlowDetail.RefItemCode}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbRefItemCode" runat="server" Text='<%# Bind("ReferenceItemCode") %>' />
                        </td>
                    </tr>
                    <tr id="trBom" runat="server" visible="false">
                        <td class="td01">
                            <asp:Literal ID="lblBom" runat="server" Text="${MasterData.Flow.FlowDetail.Bom}:" />
                        </td>
                        <td class="td02">
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblBatchSize" runat="server" Text="${MasterData.Flow.FlowDetail.BatchSize}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbBatchSize" runat="server" Text='<%# Bind("BatchSize","{0:0.########}") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblPackageVol" runat="server" Text="${MasterData.Flow.FlowDetail.PackageVolumn}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbPackageVol" runat="server" Text='<%# Bind("PackageVolumn","{0:0.########}") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblTimeUnit" runat="server" Text="${MasterData.Flow.FlowDetail.TimeUnit}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbTimeUnit" runat="server" Text='<%# Bind("TimeUnit") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblRoundUpOpt" runat="server" Text="${MasterData.Flow.FlowDetail.RoundUpOpt}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbRoundUpOpt" runat="server" Text='<%# Bind("RoundUpOption") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblProjectDescription" runat="server" Text="${MasterData.Flow.FlowDetail.ProjectDescription}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbProjectDescription" runat="server" Text='<%# Bind("ProjectDescription") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblIdMark" runat="server" Text="${MasterData.Flow.FlowDetail.IdMark.Procurement}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbIdMark" runat="server" Text='<%# Bind("IdMark") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblBarCodeType" runat="server" Text="${MasterData.Flow.FlowDetail.BarTypeCode}:" />
                        </td>
                        <td class="td02">
                            <sc1:CodeMstrLabel ID="ddlBarCodeType" runat="server" Code="FGBarCodeType" Text='<%# Bind("BarCodeType") %>' />
                        </td>
                    </tr>
                    <tr id="trCustomer" runat="server">
                        <td class="td01">
                            <asp:Literal ID="lblCustomer" runat="server" Text="${MasterData.Flow.FlowDetail.Customer}:" />
                        </td>
                        <td class="td02">
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCustomerItemCode" runat="server" Text="${MasterData.Flow.FlowDetail.CustomerItemCode}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbCustomerItemCode" runat="server" Text='<%# Bind("CustomerItemCode") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblRemark" runat="server" Text="${MasterData.Flow.FlowDetail.Remark}:" />
                        </td>
                        <td class="td02" colspan="3">
                            <asp:Literal ID="tbRemark" runat="server" Text='<%# Bind("Remark") %>' />
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
                </table>
            </fieldset>
            <fieldset runat="server">
                <legend>${MasterData.Flow.FlowDetail.Default.Value}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblLocFrom" runat="server" Text="${MasterData.Flow.FlowDetail.LocFrom}:" />
                        </td>
                        <td class="td02">
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblLocTo" runat="server" Text="${MasterData.Flow.FlowDetail.LocTo}:" />
                        </td>
                        <td class="td02">
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
                </table>
            </fieldset>
            <fieldset id="fdOption" runat="server">
                <legend>${MasterData.Flow.FlowDetail.Control.Option}</legend>
                <table class="mtable">
                    <tr>
                        <td class="ttd01">
                            <asp:Literal ID="lblIsAutoCreate" runat="server" Text="${MasterData.Flow.FlowDetail.AutoCreate}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbIsAutoCreate" runat="server" Checked='<%# Bind("IsAutoCreate") %>'
                                Enabled="false" />
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblNeedInspect" runat="server" Text="${MasterData.Flow.FlowDetail.NeedInspect}:" />
                        </td>
                        <td class="ttd02">
                            <asp:CheckBox ID="cbNeedInspect" runat="server" Checked='<%# Bind("NeedInspection") %>'
                                Enabled="false" />
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblBillSettleTerm" runat="server" Text="${MasterData.Flow.FlowDetail.BillSettleTerm}:" />
                        </td>
                        <td class="ttd02">
                            <sc1:CodeMstrLabel ID="ddlBillSettleTerm" Code="BillSettleTerm" runat="server" Value='<%# Bind("BillSettleTerm") %>' />
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblOddShipOption" runat="server" Text="${MasterData.Flow.FlowDetail.OddShipOption}:" />
                        </td>
                        <td class="ttd02">
                            <sc1:CodeMstrLabel ID="ddlShipOption" Code="ShipOption" runat="server" Value='<%# Bind("OddShipOption") %>' />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div class="tablefooter">
                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                    CssClass="button2" />
            </div>
        </ItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_FlowDetailTrack" runat="server" TypeName="com.Sconit.Web.FlowDetailTrackMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.FlowDetailTrack" SelectMethod="LoadFlowDetailTrack">
    <SelectParameters>
        <asp:Parameter Name="id" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
