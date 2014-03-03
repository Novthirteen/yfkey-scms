<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TransformerDetail.ascx.cs"
    Inherits="Hu_TransformerDetail" %>
<div class="GridView">
    <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        ShowHeader="false" OnRowDataBound="GV_List_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("HuId") %>'></asp:Label>
                    <asp:HiddenField ID="hfQty" runat="server" Value='<%# Bind("Qty") %>' />
                    <asp:HiddenField ID="hfId" runat="server" Value='<%#Bind("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LotNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:TextBox ID="tbCurrentQty" runat="server" Text='<%# Bind("CurrentQty","{0:0.########}") %>'
                        Width="50" AutoPostBack="true" OnTextChanged="tbQty_TextChanged" onclick="this.select();" />
                    <asp:RangeValidator ID="rvCurrentQty" ControlToValidate="tbCurrentQty" runat="server"
                        Display="Dynamic" ErrorMessage="*" MaximumValue="999999999" MinimumValue="-999999999" 
                        Type="Double" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblStorageBinCode" runat="server" Text='<%# Bind("StorageBinCode") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
