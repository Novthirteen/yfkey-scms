<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_FlowDetail_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<div id="floatdiv">
    <asp:FormView ID="FV_FlowDetail" runat="server" DataSourceID="ODS_FlowDetail" DefaultMode="Insert"
        DataKeyNames="Id">
        <InsertItemTemplate>
            <fieldset>
                <legend>${MasterData.Flow.FlowDetail.Basic.Info}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblSeq" runat="server" Text="${MasterData.Flow.FlowDetail.Sequence}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbSeq" runat="server" Text='<%# Bind("Sequence") %>' />
                            <asp:RangeValidator ID="rvSeq" runat="server" ControlToValidate="tbSeq" ErrorMessage="${Common.Validator.Valid.Number}"
                                Display="Dynamic" Type="Integer" MinimumValue="0" MaximumValue="100000" ValidationGroup="vgSaveGroup" />
                            <asp:CustomValidator ID="cvSeqCheck" runat="server" ControlToValidate="tbSeq" ErrorMessage="${MasterData.Flow.FlowDetail.Sequence.Exists}"
                                Display="Dynamic" ValidationGroup="vgSaveGroup" OnServerValidate="checkSeqExists" />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblItemCode" runat="server" Text="${MasterData.Flow.FlowDetail.ItemCode}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbItemCode" runat="server" Visible="true" Width="250" DescField="Description"
                                ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem"
                                CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvItemCode" runat="server" ControlToValidate="tbItemCode"
                                Display="Dynamic" ErrorMessage="${MasterData.Flow.FlowDetail.ItemCode.Required}"
                                ValidationGroup="vgSaveGroup" />
                            <asp:CustomValidator ID="cvItemCheck" runat="server" ControlToValidate="tbItemCode"
                                Display="Dynamic" ValidationGroup="vgSaveGroup" OnServerValidate="checkItemExists" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblUom" runat="server" Text="${MasterData.Flow.FlowDetail.Uom}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbUom" runat="server" Visible="true" Width="250" DescField="Description"
                                ServiceParameter="string:#tbItemCode" ValueField="Code" ServicePath="UomMgr.service"
                                ServiceMethod="GetItemUom" CssClass="inputRequired" MustMatch="true" />
                            <asp:RequiredFieldValidator ID="rfvUom" runat="server" ControlToValidate="tbUom"
                                Display="Dynamic" ErrorMessage="${MasterData.Flow.FlowDetail.Uom.Required}" ValidationGroup="vgSaveGroup" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblUC" runat="server" Text="${MasterData.Flow.FlowDetail.UnitCount}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbUC" runat="server" Text='<%# Bind("UnitCount","{0:0.########}") %>'
                                 CssClass="inputRequired"/>
                            <asp:RequiredFieldValidator ID="rfvUC" runat="server" ErrorMessage="${MasterData.Flow.FlowDetail.UnitCount.Required}"
                                Display="Dynamic" ControlToValidate="tbUC" ValidationGroup="vgSaveGroup" />
                            <asp:RangeValidator ID="rvUC" ControlToValidate="tbUC" runat="server" Display="Dynamic"
                                ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="999999999" MinimumValue="0.00000001"
                                Type="Double" ValidationGroup="vgSaveGroup" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblHuLotSize" runat="server" Text="${MasterData.Flow.FlowDetail.HuLotSize}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbHuLotSize" runat="server" Text='<%# Bind("HuLotSize") %>' 
                                CssClass="inputRequired"/>
                            <%--wurui----2012/10/09--start--%>
                            <asp:RequiredFieldValidator ID="rfvHuLotSize" runat="server" ControlToValidate="tbHuLotSize"
                                Display="Dynamic" ErrorMessage="${MasterData.Flow.FlowDetail.HuLotSize.Required}" ValidationGroup="vgSaveGroup" />
                            <asp:RangeValidator ID="rvHuLotSize" ControlToValidate="tbHuLotSize" runat="server" Display="Dynamic"
                                ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="999999999" MinimumValue="0.00000001"
                                Type="Double" ValidationGroup="vgSaveGroup" />
                            <%--wurui--end--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblStartDate" runat="server" Text="${MasterData.Flow.FlowDetail.StartDate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbStartDate" runat="server" Text='<%# Bind("StartDate") %>' onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblEndDate" runat="server" Text="${MasterData.Flow.FlowDetail.EndDate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbEndDate" runat="server" Text='<%# Bind("EndDate") %>' onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblSafeStock" runat="server" Text="${MasterData.Flow.FlowDetail.SafeStock}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbSafeStock" runat="server" Text='<%# Bind("SafeStock","{0:0.########}") %>' />
                            <asp:RangeValidator ID="rvSafeStock" ControlToValidate="tbSafeStock" runat="server"
                                Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="999999999"
                                MinimumValue="-999999999" Type="Double" ValidationGroup="vgSaveGroup" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblMaxStock" runat="server" Text="${MasterData.Flow.FlowDetail.MaxStock}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMaxStock" runat="server" Text='<%# Bind("MaxStock","{0:0.########}") %>' />
                            <asp:RangeValidator ID="rvMaxStock" ControlToValidate="tbMaxStock" runat="server"
                                Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="999999999"
                                MinimumValue="-999999999" Type="Double" ValidationGroup="vgSaveGroup" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblMinLotSize" runat="server" Text="${MasterData.Flow.FlowDetail.MinLotSize}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMinLotSize" runat="server" Text='<%# Bind("MinLotSize","{0:0.########}") %>' />
                            <asp:RangeValidator ID="rvShiftStartStock" ControlToValidate="tbMinLotSize" runat="server"
                                Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="999999999"
                                MinimumValue="0" Type="Double" ValidationGroup="vgSaveGroup" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblOrderLotSize" runat="server" Text="${MasterData.Flow.FlowDetail.OrderLotSize}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbOrderLotSize" runat="server" Text='<%# Bind("OrderLotSize","{0:0.########}") %>' />
                            <asp:RangeValidator ID="rvOrderLotSize" ControlToValidate="tbOrderLotSize" runat="server"
                                Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="999999999"
                                MinimumValue="0" Type="Double" ValidationGroup="vgSaveGroup" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblOrderGoodsReceiptLotSize" runat="server" Text="${MasterData.Flow.FlowDetail.GoodsReceiptLotSize}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbOrderGoodsReceiptLotSize" runat="server" Text='<%# Bind("GoodsReceiptLotSize","{0:0.########}") %>' />
                            <asp:RangeValidator ID="rvOrderGoodsReceiptLotSize" ControlToValidate="tbOrderGoodsReceiptLotSize"
                                runat="server" Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}"
                                MaximumValue="999999999" MinimumValue="0" Type="Double" ValidationGroup="vgSaveGroup" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblRefItemCode" runat="server" Text="${MasterData.Flow.FlowDetail.RefItemCode}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbRefItemCode" runat="server" Visible="true" Width="250" DescField="ReferenceCode"
                                ValueField="ReferenceCode" ServicePath="ItemReferenceMgr.service" ServiceMethod="GetItemReference"
                                Text='<%# Bind("ReferenceItemCode")%>' />
                        </td>
                    </tr>
                    <tr id="trBom" runat="server" visible="false">
                        <td class="td01">
                            <asp:Literal ID="lblBom" runat="server" Text="${MasterData.Flow.FlowDetail.Bom}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbBom" runat="server" Visible="true" Width="250" DescField="Description"
                                ValueField="Code" ServicePath="BomMgr.service" ServiceMethod="GetAllBom" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblBatchSize" runat="server" Text="${MasterData.Flow.FlowDetail.BatchSize}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbBatchSize" runat="server" Text='<%# Bind("BatchSize","{0:0.########}") %>' />
                            <asp:RangeValidator ID="rvBatchSize" ControlToValidate="tbBatchSize" runat="server"
                                Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="999999999"
                                MinimumValue="0" Type="Double" ValidationGroup="vgSaveGroup" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblPackageVol" runat="server" Text="${MasterData.Flow.FlowDetail.PackageVolumn}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbPackageVol" runat="server" Text='<%# Bind("PackageVolumn","{0:0.########}") %>' />
                            <asp:RangeValidator ID="rvPackageVol" ControlToValidate="tbPackageVol" runat="server"
                                Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="999999999"
                                MinimumValue="0" Type="Double" ValidationGroup="vgSaveGroup" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblTimeUnit" runat="server" Text="${MasterData.Flow.FlowDetail.TimeUnit}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbTimeUnit" runat="server" Text='<%# Bind("TimeUnit") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblRoundUpOpt" runat="server" Text="${MasterData.Flow.FlowDetail.RoundUpOpt}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbRoundUpOpt" runat="server" Text='<%# Bind("RoundUpOption","{0:0.########}") %>' />
                            <asp:RangeValidator ID="rvRoundUpOpt" ControlToValidate="tbRoundUpOpt" runat="server"
                                Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="1"
                                MinimumValue="-1" Type="Double" ValidationGroup="vgSaveGroup" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblProjectDescription" runat="server" Text="${MasterData.Flow.FlowDetail.ProjectDescription}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbProjectDescription" runat="server" Text='<%# Bind("ProjectDescription") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblIdMark" runat="server" Text="${MasterData.Flow.FlowDetail.IdMark.Procurement}:"
                                Visible="false" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbIdMark" runat="server" Text='<%# Bind("IdMark") %>' Visible="false" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblBarCodeType" runat="server" Text="${MasterData.Flow.FlowDetail.BarTypeCode}:"
                                Visible="false" />
                        </td>
                        <td class="td02">
                            <cc1:CodeMstrDropDownList ID="ddlBarCodeType" runat="server" Visible="false" Code="RMBarCodeType" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCustomer" runat="server" Text="${MasterData.Flow.FlowDetail.Customer}:"
                                Visible="false" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbCustomer" runat="server" Width="250" DescField="Name" ValueField="Code"
                                ServicePath="PartyMgr.service" ServiceMethod="GetFromParty" Visible="false" MustMatch="true" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCustomerItemCode" runat="server" Text="${MasterData.Flow.FlowDetail.CustomerItemCode}:"
                                Visible="false" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCustomerItemCode" runat="server" Text='<%# Bind("CustomerItemCode") %>'
                                Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblRemark" runat="server" Text="${MasterData.Flow.FlowDetail.Remark}:" />
                        </td>
                        <td class="td02" colspan="3">
                            <asp:TextBox ID="tbRemark" runat="server" Text='<%# Bind("Remark") %>' TextMode="MultiLine"
                                Width="635" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <%--采购--%>
            <fieldset id="fdProcurement" runat="server" visible="false">
                <legend>${MasterData.Flow.FlowDetail.Default.Value}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblProcurementLocTo" runat="server" Text="${MasterData.Flow.FlowDetail.LocTo}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbProcurementLocTo" runat="server" Visible="true" Width="250" DescField="Name"
                                ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocation" />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                    </tr>
                </table>
            </fieldset>
            <%--销售--%>
            <fieldset id="fdDistribution" runat="server" visible="false">
                <legend>${MasterData.Flow.FlowDetail.Default.Value}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblDistributionLocFrom" runat="server" Text="${MasterData.Flow.FlowDetail.LocFrom}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbDistributionLocFrom" runat="server" Visible="true" Width="250"
                                DescField="Name" ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocation" />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                    </tr>
                </table>
            </fieldset>
            <%--生产--%>
            <fieldset id="fdProduction" runat="server" visible="false">
                <legend>${MasterData.Flow.FlowDetail.Default.Value}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblProductionLocFrom" runat="server" Text="${MasterData.Flow.FlowDetail.LocFrom.Production}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbProductionLocFrom" runat="server" Visible="true" Width="250" DescField="Name"
                                ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocation" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblProductionLocTo" runat="server" Text="${MasterData.Flow.FlowDetail.LocTo.Production}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbProductionLocTo" runat="server" Visible="true" Width="250" DescField="Name"
                                ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocation" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <%--移库--%>
            <fieldset id="fdTransfer" runat="server" visible="false">
                <legend>${MasterData.Flow.FlowDetail.Default.Value}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblTransferLocFrom" runat="server" Text="${MasterData.Flow.FlowDetail.LocFrom}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbTransferLocFrom" runat="server" Visible="true" Width="250" DescField="Name"
                                ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocation" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblTransferLocTo" runat="server" Text="${MasterData.Flow.FlowDetail.LocTo}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbTransferLocTo" runat="server" Visible="true" Width="250" DescField="Name"
                                ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocation" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset id="fdOption" runat="server">
                <legend>${MasterData.Flow.FlowDetail.Control.Option}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblIsAutoCreate" runat="server" Text="${MasterData.Flow.FlowDetail.AutoCreate}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsAutoCreate" runat="server" Checked='<%# Bind("IsAutoCreate") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblNeedInspect" runat="server" Text="${MasterData.Flow.FlowDetail.NeedInspect}:"
                                Visible="false" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbNeedInspect" runat="server" Checked='<%# Bind("NeedInspection") %>'
                                Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd01">
                            <asp:Literal ID="lblBillSettleTerm" runat="server" Text="${MasterData.Flow.FlowDetail.BillSettleTerm}:"
                                Visible="false" />
                        </td>
                        <td class="ttd02">
                            <asp:DropDownList ID="ddlBillSettleTerm" runat="server" DataTextField="Description"
                                DataValueField="Value" Visible="false" />
                        </td>
                          <td class="ttd01">
                            <asp:Literal ID="lblMRPWeight" runat="server" Text="${MasterData.Flow.FlowDetail.MRPWeight}:"
                                />
                        </td>
                        <td class="ttd02">
                            <asp:TextBox ID="tbMRPWeight" runat="server" Text='<%# Bind("MRPWeight") %>' CssClass="inputRequired"/>
                            <asp:RequiredFieldValidator ID="rfvMRPWeight" runat="server" ControlToValidate="tbMRPWeight"
                                Display="Dynamic" ErrorMessage="${MasterData.Flow.FlowDetail.MRPWeight.Required}"
                                ValidationGroup="vgSaveGroup" />
                            <asp:RangeValidator ID="rvMRPWeight" ControlToValidate="tbMRPWeight"
                                runat="server" Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}"
                                 Type="Integer" MinimumValue="0" MaximumValue="99999999"  ValidationGroup="vgSaveGroup" />
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd01">
                            <asp:Literal ID="lblOddShipOption" runat="server" Text="${MasterData.Flow.FlowDetail.OddShipOption}:"
                                Visible="false" />
                        </td>
                        <td class="ttd02">
                            <cc1:CodeMstrDropDownList ID="ddlOddShipOption" Code="OddShipOption" runat="server"
                                Visible="false">
                            </cc1:CodeMstrDropDownList>
                        </td>
                        <td class="ttd01">
                        </td>
                        <td class="ttd02">
                        </td>
                    </tr>
                </table>
            </fieldset>
            <%--供货--%>
            <fieldset id="fdFordEDIOption" runat="server" visible="false">
                <legend>Ford EDI发货配置选项</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            Gross Weight
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbGrossWeight" runat="server" Text='<%# Bind("GrossWeight") %>' />
                        </td>
                        <td class="td01">
                        Net Weight
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbNetWeight" runat="server" Text='<%# Bind("NetWeight") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            净重毛重单位
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbWeightUom" runat="server" Text='<%# Bind("WeightUom") %>' />
                        </td>
                        <td class="td01">
                        Carrier Code
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCarrierCode" runat="server" Text='<%# Bind("CarrierCode") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            Trans Mode Code
                        </td>
                        <td class="td02">
                            <%--<asp:TextBox ID="tbTransModeCode" runat="server" Text='<%# Bind("TransModeCode") %>' />--%>
                            <select id="tbTransModeCode" runat="server" >
                                <option  value=""></option>
                                <option  value="M">Motor(汽运)</option>
                                <option  value="O">海运</option>
                                <option   value="A">空运</option>
                            </select>
                        </td>
                        <td class="td01">
                        Conveyance Number
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbConveyanceNumber" runat="server" Text='<%# Bind("ConveyanceNumber") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            Receiving Plant
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbReceivingPlant" runat="server" Text='<%# Bind("ReceivingPlant") %>' />
                        </td>
                        <td class="td01">
                        Ship From
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbShipFrom" runat="server" Text='<%# Bind("ShipFrom") %>' />
                        </td>
                    </tr>
                     <tr>
                        <td class="td01">
                            Packaging Code
                        </td>
                        <td class="td02">
                            <%--<asp:TextBox ID="tbPackagingCode" runat="server" Text='<%# Bind("PackagingCode") %>' />--%>
                             <select id="tbPackagingCode" runat="server" >
                                <option  value=""></option>
                                <option  value="CD">CD</option>
                                <option   value="COP">COP</option>
                            </select>
                        </td>
                        <td class="td01">
                        Lading Quantity
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbLadingQuantity" runat="server" Text='<%# Bind("LadingQuantity") %>' />
                        </td>
                    </tr>
                     <tr>
                        <td class="td01">
                            Units Per Container
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbUnitsPerContainer" runat="server" Text='<%# Bind("UnitsPerContainer") %>' />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div class="tablefooter">
                <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="${Common.Button.Save}"
                    CssClass="button2" ValidationGroup="vgSaveGroup" />
                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                    CssClass="button2" />
            </div>
        </InsertItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_FlowDetail" runat="server" TypeName="com.Sconit.Web.FlowDetailMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.FlowDetail" InsertMethod="CreateFlowDetail"
    OnInserted="ODS_FlowDetail_Inserted" OnInserting="ODS_FlowDetail_Inserting" SelectMethod="FindFlowDetail">
    <SelectParameters>
        <asp:Parameter Name="id" Type="Int32" />
    </SelectParameters>
    <InsertParameters>
        <asp:Parameter Name="SafeStock" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="MaxStock" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="MinLotSize" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="OrderLotSize" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="GoodsReceiptLotSize" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="BatchSize" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="HuLotSize" Type="Int32" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="PackageVolumn" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="StartDate" Type="DateTime" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="EndDate" Type="DateTime" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="Sequence" Type="Int32" ConvertEmptyStringToNull="true" />
    </InsertParameters>
</asp:ObjectDataSource>
