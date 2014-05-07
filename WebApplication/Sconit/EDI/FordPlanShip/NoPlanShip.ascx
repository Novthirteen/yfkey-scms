<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NoPlanShip.ascx.cs" Inherits="EDI_FordPlan_NoPlanShip" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script language="javascript" type="text/javascript">
    function GVCheckClick() {
        if ($(".GVHeader input:checkbox").attr("checked") == true) {
            $(".GVRow input:checkbox").attr("checked", true);
            $(".GVAlternatingRow input:checkbox").attr("checked", true);
        }
        else {
            $(".GVRow input:checkbox").attr("checked", false);
            $(".GVAlternatingRow input:checkbox").attr("checked", false);
        }
    }
</script>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                发货路线:
            </td>
            <td class="td02" style="font-weight: bold;">
                <uc3:textbox ID="tbFlow" runat="server" Visible="true" DescField="Description" ValueField="Code"
                    ServiceMethod="GetFlowList" ServicePath="FlowMgr.service" OnTextChanged="tbFlow_TextChanged"
                    AutoPostBack="true" MustMatch="true" Width="250" />
            </td>
            <td class="td02">
                <div class="buttons">
                    <cc1:Button ID="btnShip" runat="server" OnClick="btnShip_Click" Text="${MasterData.Distribution.Button.Ship}"
                        FunctionId="ShipOrder" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
<fieldset>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div onclick="GVCheckClick()">
                            <asp:CheckBox ID="CheckAll" runat="server" />
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBoxGroup" name="CheckBoxGroup" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${EDI.EDIFordPlanBase.Item}">
                    <ItemTemplate>
                        <asp:Label ID="lblItem" runat="server" Text='<%# Bind("Item") %>' Width="100" />
                        <asp:HiddenField ID="ftItem" runat="server" Value='<%# Bind("Item") %>' />
                        <asp:HiddenField ID="ftItemDesc" runat="server" Value='<%# Bind("ItemDesc") %>' />
                        <asp:HiddenField ID="ftId" runat="server" Value='<%# Bind("Id") %>' />
                        <asp:HiddenField ID="ftRefItem" runat="server" Value='<%# Bind("RefItem") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 
                <asp:TemplateField HeaderText="${EDI.EDIFordPlanBase.ItemDesc}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("ItemDesc")  %>' Width="150" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Part Number">
                    <ItemTemplate>
                        <asp:Label ID="lblRefItem" runat="server" Text='<%# Bind("RefItem") %>'  Width="150"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Receiving Plant">
                    <ItemTemplate>
                        <asp:TextBox ID="tbCustomerCode" runat="server" Text='<%# Bind("CustomerCode") %>'  Width="50"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ship From">
                    <ItemTemplate>
                        <asp:TextBox ID="tbSupplierCode" runat="server" Text='<%# Bind("SupplierCode") %>'  Width="50"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- <asp:TemplateField HeaderText="采购订单号">
                    <ItemTemplate>
                        <asp:TextBox ID="tbPurchaseOrder" runat="server" Text='<%# Bind("PurchaseOrder") %>'  Width="50"/>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                 <asp:TemplateField HeaderText="中间商">
                    <ItemTemplate>
                        <asp:TextBox ID="tbIntermediateConsignee" runat="server" Text='<%# Bind("IntermediateConsignee") %>'  Width="50"/>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="本次发货量">
                    <ItemTemplate>
                        <asp:TextBox ID="tbShipQty" runat="server" Text='<%# Bind("ShipQty","{0:0.########}") %>'
                            Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Accumulative Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="tbCurrenCumQty" runat="server" Text='<%# Bind("CurrenCumQty","{0:0.########}") %>'
                            Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${EDI.EDIFordPlanBase.Uom}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbInUom" runat="server" Text='<%# Bind("Uom") %>' Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="装箱单号">
                    <ItemTemplate>
                        <asp:TextBox ID="tbShipmentID" runat="server" Text='<%# Bind("ShipmentID") %>' Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bill of Lading Reference">
                    <ItemTemplate>
                        <asp:TextBox ID="tbLadingNum" runat="server" Text='<%# Bind("LadingNum") %>' Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Purpose">
                    <ItemTemplate>
                            <select id="tbPurpose" runat="server" >
                                <option selected="selected"  value="00">Original</option>
                                <option value="01">Cancellation</option>
                                <option value="05">Replace</option>
                                <option value="12">Not Processed</option>
                            </select>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Gross Weight">
                    <ItemTemplate>
                        <asp:TextBox ID="tbGrossWeight" runat="server" Text='<%# Bind("GrossWeight","{0:0.########}") %>'
                            Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Net Weight">
                    <ItemTemplate>
                        <asp:TextBox ID="tbNetWeight" runat="server" Text='<%# Bind("NetWeight","{0:0.########}") %>'
                            Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="U/M">
                    <ItemTemplate>
                        <asp:TextBox ID="tbWeightUom" runat="server" Text='<%# Bind("WeightUom") %>' Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Packaging Type">
                    <ItemTemplate>
                        <asp:TextBox ID="tbOutPackType" runat="server" Text='<%# Bind("OutPackType") %>'
                            Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Lading Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="tbOutPackQty" runat="server" Text='<%# Bind("OutPackQty","{0:0.########}") %>'
                            Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Carrier Code">
                    <ItemTemplate>
                        <asp:TextBox ID="tbCarrierCode" runat="server" Text='<%# Bind("CarrierCode") %>'
                            Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Trans Method Code">
                    <ItemTemplate>
                        <asp:TextBox ID="tbTransportationMethod" runat="server" Text='<%# Bind("TransportationMethod") %>'
                            Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Conveyance Number">
                    <ItemTemplate>
                        <asp:TextBox ID="tbEquipmentNum" runat="server" Text='<%# Bind("EquipmentNum") %>'
                            Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Units Per Container"> <%--单箱件数--%>
                    <ItemTemplate>
                        <asp:TextBox ID="tbPerLoadQty" runat="server" Text='<%# Bind("PerLoadQty","{0:0.########}") %>'
                            Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="机场代码">
                    <ItemTemplate>
                        <asp:TextBox ID="tbAirportCode" runat="server" Text='<%# Bind("AirportCode") %>'
                            Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
