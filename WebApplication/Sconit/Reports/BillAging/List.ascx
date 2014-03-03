<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Reports_BillAging_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" DefaultSortExpression="BillAddress.Party.Name"
            DefaultSortDirection="Ascending">
            <Columns>
                <asp:TemplateField SortExpression="BillAddress.Party.Name">
                    <ItemTemplate>
                        <asp:Label ID="lblPartyName" runat="server" Text='<%# Eval("BillAddress.Party.Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.BillAging.ItemCode}" SortExpression="Item.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item.Code")%>' />&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.BillAging.ItemDescription}" SortExpression="Item.Desc1">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("Item.Description")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.BillAging.Uom}" SortExpression="Uom.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Eval("Uom.Code")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.BillAging.UnitCount}" SortExpression="UnitCount">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Eval("UnitCount", "{0:0.###}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.BillAging.Qty1}" SortExpression="Qty1">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnQty1" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text='<%# Eval("Qty1", "{0:0.###}")%>' OnClick="lbtnDetail1_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.BillAging.Qty2}" SortExpression="Qty2">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnQty2" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text='<%# Eval("Qty2", "{0:0.###}")%>' OnClick="lbtnDetail2_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.BillAging.Qty3}" SortExpression="Qty3">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnQty3" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text='<%# Eval("Qty3", "{0:0.###}")%>' OnClick="lbtnDetail3_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.BillAging.Qty4}" SortExpression="Qty4">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnQty4" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text='<%# Eval("Qty4", "{0:0.###}")%>' OnClick="lbtnDetail4_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.BillAging.Qty5}" SortExpression="Qty5">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnQty5" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text='<%# Eval("Qty5", "{0:0.###}")%>' OnClick="lbtnDetail5_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.BillAging.Qty6}" SortExpression="Qty6">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnQty6" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text='<%# Eval("Qty6", "{0:0.###}")%>' OnClick="lbtnDetail6_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.BillAging.Qty7}" SortExpression="Qty7">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnQty7" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text='<%# Eval("Qty7", "{0:0.###}")%>' OnClick="lbtnDetail7_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.BillAging.Qty8}" SortExpression="Qty8">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnQty8" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text='<%# Eval("Qty8", "{0:0.###}")%>' OnClick="lbtnDetail8_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.BillAging.Qty9}" SortExpression="Qty9">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnQty9" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text='<%# Eval("Qty9", "{0:0.###}")%>' OnClick="lbtnDetail9_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
