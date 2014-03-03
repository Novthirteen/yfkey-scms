<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewRepackDetailList.ascx.cs"
    Inherits="Inventory_Repack_ViewRepackDetailList" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <legend>${MasterData.Inventory.RepackDetail.In}</legend>
    <div class="GridView">
        <asp:GridView ID="GV_InList" runat="server" AllowSorting="True" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemDesc%>">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("ItemDescription") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUom%>">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("UomCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUnitCount%>">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("UnitCount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLocation%>">
                    <ItemTemplate>
                        <asp:Label ID="lblLoc" runat="server" Text='<%# Bind("LocationCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataStorageBin%>">
                    <ItemTemplate>
                        <asp:Label ID="lblStorageBin" runat="server" Text='<%# Bind("StorageAreaCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLotNo%>">
                    <ItemTemplate>
                        <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LotNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuId%>">
                    <ItemTemplate>
                        <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("HuId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataQty%>">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
<fieldset>
    <legend>${MasterData.Inventory.RepackDetail.Out}</legend>
    <div class="GridView">
        <asp:GridView ID="GV_OutList" runat="server" AllowSorting="True" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemDesc%>">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("ItemDescription") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUom%>">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("UomCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUnitCount%>">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("UnitCount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLotNo%>">
                    <ItemTemplate>
                        <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LotNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuId%>">
                    <ItemTemplate>
                        <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("HuId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataQty%>">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>

