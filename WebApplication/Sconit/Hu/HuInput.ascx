<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HuInput.ascx.cs" Inherits="Hu_HuInput" %>
<div class="GridView">
    <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        ShowHeader="false" OnRowDataBound="GV_List_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("HuId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LotNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:TextBox ID="tbQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>'
                        Width="50" AutoPostBack="true" OnTextChanged="tbQty_TextChanged"
                        onclick="if(!readOnly)select();" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
