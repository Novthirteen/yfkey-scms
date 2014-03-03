<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OutLocTransList.ascx.cs"
    Inherits="Order_GoodsReceipt_OrderReceipt_OutLocTransList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <legend>${MasterData.Order.GoodsReceipt.OrderReceipt.OutLocTrans}</legend>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False">
            <Columns>
               <asp:BoundField HeaderText="${MasterData.Order.LocTrans.Operation}" DataField="Operation" />
                <asp:TemplateField HeaderText="${MasterData.Order.OrderDetail.Item.Code}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderDetail.Uom}">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Uom.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderDetail.UnitCount}">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("OrderDetail.UnitCount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="${MasterData.Order.OrderDetail.LocationTo}">
                    <ItemTemplate>
                        <asp:Label ID="lblLocTo" runat="server" Text='<%# Bind("OrderDetail.DefaultLocationTo.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="${MasterData.Order.OrderDetail.OrderedQty}" DataField="OrderedQty"
                    DataFormatString="{0:0.########}" />
                <asp:BoundField HeaderText="${MasterData.Order.LocTrans.UnitQty}" DataField="UnitQty"
                    DataFormatString="{0:0.########}" />
                <asp:BoundField HeaderText="${MasterData.Order.OrderDetail.CurrentReceiveQty}" DataField="CurrentReceiveQty"
                    DataFormatString="{0:0.########}" />
                <asp:BoundField HeaderText="${MasterData.Order.OrderDetail.CurrentRejectQty}" DataField="CurrentRejectQty"
                    DataFormatString="{0:0.########}" />
                <asp:BoundField HeaderText="${MasterData.Order.OrderDetail.CurrentScrapQty}" DataField="CurrentScrapQty"
                    DataFormatString="{0:0.########}" />
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
