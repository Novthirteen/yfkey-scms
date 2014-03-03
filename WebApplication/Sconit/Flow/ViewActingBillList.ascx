<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewActingBillList.ascx.cs"
    Inherits="MasterData_Flow_ViewActingBillList" %>
<fieldset>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.Flow.ActingBill.ItemCode}" SortExpression="Item.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Item.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.ActingBill.Uom}" SortExpression="Uom.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Uom.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.ActingBill.PriceList}" SortExpression="PriceList.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "PriceList.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="${MasterData.Flow.ActingBill.UnitPrice}" DataField="UnitPrice"
                    DataFormatString="{0:0.########}" />
                <asp:TemplateField HeaderText="${MasterData.Flow.ActingBill.Currency}" SortExpression="Currency.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Currency.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CheckBoxField HeaderText="${MasterData.Flow.ActingBill.IsIncludeTax}" DataField="IsIncludeTax" />
                <asp:CheckBoxField HeaderText="${MasterData.Flow.ActingBill.IsProvisionalEstimate}" DataField="IsProvisionalEstimate" />
                <asp:BoundField HeaderText="${MasterData.Flow.ActingBill.TaxCode}" DataField="TaxCode" />
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
