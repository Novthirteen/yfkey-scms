<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ActBillList.ascx.cs" Inherits="Order_OrderView_ActBillList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <legend id="lTitle" runat="server"></legend>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="false" AllowPaging="True" PagerID="gp" Width="100%"
            TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll" SelectCountMethod="FindCount">
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.Order.ActingBill.ItemCode}" SortExpression="Item.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Item.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.ActingBill.Uom}" SortExpression="Uom.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Uom.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="${MasterData.Order.ActingBill.OrderedQty}" DataField="OrderedQty"
                    DataFormatString="{0:0.########}" />
                <asp:BoundField HeaderText="${MasterData.Order.ActingBill.AccumulateQty}" DataField="AccumulateQty"
                    DataFormatString="{0:0.########}" />
                <asp:TemplateField HeaderText="${MasterData.Order.ActingBill.UnitPrice}" SortExpression="UnitPrice"
                    Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbUnitPrice" runat="server" Text='<%# Bind("UnitPrice","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.ActingBill.Discount}" SortExpression="Discount"
                    Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbDiscount" runat="server" Text='<%# Bind("Discount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.ActingBill.Amount}" SortExpression="TotalAmount"
                    Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbTotalAmount" runat="server" Text='<%# Bind("Amount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.ActingBill.Currency}" SortExpression="Currency.Code"
                    Visible="false">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Currency.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CheckBoxField HeaderText="${MasterData.Order.ActingBill.IsIncludeTax}" DataField="IsIncludeTax"
                    Visible="false" />
                <asp:BoundField HeaderText="${MasterData.Order.ActingBill.TaxCode}" DataField="TaxCode"
                    Visible="false" />
                <asp:TemplateField HeaderText="${MasterData.Order.ActingBill.BillAddress}" SortExpression="BillAddress.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "BillAddress.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
