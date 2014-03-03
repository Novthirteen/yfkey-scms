<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MasterData_PriceList_PriceListDetail_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="${Common.Business.Id}" SortExpression="Id"
                    Visible="false" />
                <asp:TemplateField HeaderText="${MasterData.PriceList.Code}" SortExpression="PriceList.Code">
                    <ItemTemplate>
                        <asp:Label ID="GV_lbPriceListCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PriceList.Code")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Item.Code}" SortExpression="Item.Code">
                    <ItemTemplate>
                        <asp:Label ID="GV_lbItemCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Item.Code")%>'
                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "Item.Description")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="StartDate" HeaderText="${Common.Business.StartDate}" SortExpression="StartDate"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="EndDate" HeaderText="${Common.Business.EndDate}" SortExpression="EndDate"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:TemplateField HeaderText="${MasterData.Currency.Code}" SortExpression="Currency.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Currency.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UnitPrice" HeaderText="${MasterData.PriceListDetail.UnitPrice}"
                    SortExpression="UnitPrice" DataFormatString="{0:0.########}" />
                <asp:TemplateField HeaderText="${MasterData.Uom.Code}" SortExpression="Uom.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Uom.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TaxCode" HeaderText="${MasterData.PriceListDetail.TaxCode}"
                    SortExpression="TaxCode" />
                <asp:CheckBoxField DataField="IsIncludeTax" HeaderText="${MasterData.PriceListDetail.IsIncludeTax}"
                    SortExpression="IsIncludeTax" />
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click">
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.Delete}" OnClick="lbtnDelete_Click" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
