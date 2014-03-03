<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Warehouse_PutAway_List" %>
<div class="GridView">
    <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" OnRowDataBound="GV_List_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuid%>" SortExpression="HuId">
                <ItemTemplate>
                    <asp:Label ID="lblHuId" runat="server" Text='<%# Eval("HuId")%>' />
                    <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("Id")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLotNo%>" SortExpression="LotNo">
                <ItemTemplate>
                    <asp:Label ID="lblLotNo" runat="server" Text='<%# Eval("LotNo")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>" SortExpression="ItemCode">
                <ItemTemplate>
                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemDesc%>" SortExpression="ItemDescription">
                <ItemTemplate>
                    <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("ItemDescription")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUom%>" SortExpression="UomCode">
                <ItemTemplate>
                    <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UomCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataQty%>" SortExpression="Qty">
                <ItemTemplate>
                    <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty","{0:0.########}")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataStorageBinCode%>"
                SortExpression="StorageBinCode">
                <ItemTemplate>
                    <asp:Label ID="lblStorageBinCode" runat="server" Text='<%# Eval("StorageBinCode")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="<%$Resources:Language,Description%>" SortExpression="NewStorageBin.Description">
                <ItemTemplate>
                    <asp:Label ID="lblStorageBinDescription" runat="server" Text='<%# Eval("NewStorageBin.Description")%>' />
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="<%$Resources:Language,ButtonDelete%>" ItemStyle-Width="100px">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDelete" runat="server" Text="<%$Resources:Language,ButtonDelete%>"
                        OnClick="lbtnDelete_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
