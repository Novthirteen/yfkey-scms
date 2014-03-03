<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Order_GoodsReceipt_AdjustReceipt_List" %>
<%@ Register Src="~/Hu/Transformer.ascx" TagName="Transformer" TagPrefix="uc2" %>
<fieldset>
    <legend>${Receipt.ReceiptDetails}</legend>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="${InProcessLocation.InProcessLocationDetail.OrderNo}">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("OrderNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Receipt.Sequence}">
                    <ItemTemplate>
                        <asp:Label ID="lblSequence" runat="server" Text='<%# Bind("Sequence") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Receipt.Item.Code}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Receipt.Item.Description}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("ItemDescription") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Receipt.ReferenceItem.Description}">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceItemCode" runat="server" Text='<%# Bind("ReferenceItemCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Receipt.Uom}">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("UomCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Receipt.UnitCount}">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("UnitCount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Receipt.LocationFrom.Code}">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationFrom" runat="server" Text='<%# Bind("LocationFromCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Receipt.LocationTo.Code}">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationTo" runat="server" Text='<%# Bind("LocationToCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="${MasterData.Receipt.HuId}">
                    <ItemTemplate>
                        <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("HuId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="${MasterData.Receipt.LotNo}">
                    <ItemTemplate>
                        <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LotNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Receipt.ShippedQty}">
                    <ItemTemplate>
                        <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Receipt.ReceiptQty}">
                    <ItemTemplate>
                        <asp:Label ID="lblCurrentQty" runat="server" Text='<%# Bind("CurrentQty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Receipt.AdjustQty}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbAdjustQty" runat="server" Text='<%# Bind("AdjustQty","{0:0.########}") %>'
                            onmouseup="if(!readOnly)select();" Width="50"></asp:TextBox>
                        <asp:RangeValidator ID="rvAdjustQty" ControlToValidate="tbAdjustQty" runat="server"
                            Display="Dynamic" ErrorMessage="*" MaximumValue="999999999" MinimumValue="-999999999"
                            Type="Double" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
