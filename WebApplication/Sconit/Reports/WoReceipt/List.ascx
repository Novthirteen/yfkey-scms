<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MasterData_Reports_WoReceipt_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="ResultDetail.ascx" TagName="List" TagPrefix="uc2" %>
<fieldset>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="${Common.Business.ProductionLine}" SortExpression="Flow">
                    <ItemTemplate>
                        <asp:Label ID="lblProductLine" runat="server" Text='<%# Eval("Flow")%>' />&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemCode}" SortExpression="Item">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item")%>' />&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemDescription}" SortExpression="ItemDesc">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("ItemDesc")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Qty}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lblRecQty" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Flow") +"|"+DataBinder.Eval(Container.DataItem, "Item") %>'
                            Text='<%# Eval("RecQty", "{0:#.######}")%>' OnClick="lbtnQty_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.BoxCount}">
                    <ItemTemplate>
                        <asp:Label ID="lblBoxCount" runat="server" Text='<%# Eval("BoxCount")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
<uc2:List ID="ucList" runat="server" Visible="false" />
