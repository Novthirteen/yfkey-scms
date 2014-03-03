<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InLocTransList.ascx.cs"
    Inherits="Order_GoodsReceipt_OrderReceipt_InLocTransList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <legend>${MasterData.Order.GoodsReceipt.OrderReceipt.InLocTrans}</legend>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.Operation}">
                    <ItemTemplate>
                        <asp:Label ID="lbOperation" runat="server" Text='<%# Bind("Operation") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.ItemCode}" SortExpression="Item.Code">
                    <ItemTemplate>
                        <asp:Label ID="lbItem" runat="server" Text='<%# Bind("Item.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.Uom}" SortExpression="Uom.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Uom.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderDetail.LocationFrom}" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblLocFrom" runat="server" Text='<%# Bind("OrderDetail.DefaultLocationFrom.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="${MasterData.Order.LocTrans.OrderdQty}" DataField="OrderedQty"
                    DataFormatString="{0:0.########}" />
                <asp:BoundField HeaderText="${MasterData.Order.LocTrans.UnitQty}" DataField="UnitQty"
                    DataFormatString="{0:0.########}" />
                <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.ItemQty}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbCurrentReceiveQty" runat="server" Text='<%# Bind("CurrentReceiveQty","{0:0.########}") %>' onmouseup="if(!readOnly)select();"
                            Width="100" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
