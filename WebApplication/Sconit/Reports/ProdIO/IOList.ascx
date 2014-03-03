<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IOList.ascx.cs" Inherits="Reports_ProdIO_IOList" %>
<div class="GridView">
    <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        ShowHeader="false" OnRowDataBound="GV_List_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item.Code")%>' />&nbsp;
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("Item.Description")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblUom" runat="server" Text='<%# Eval("Item.Uom.Code")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblQty" runat="server" Text='<%# Eval("AccumQty","{0:0.########}")%>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
