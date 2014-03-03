<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewItemInLocTransList.ascx.cs"
    Inherits="Order_GoodsReceipt_OrderReceipt_NewItemInLocTransList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <legend>${MasterData.Order.GoodsReceipt.OrderReceipt.InLocTrans}</legend>
    <asp:UpdatePanel ID="UP_GV_List" runat="server">
        <ContentTemplate>
            <div>
                <div id="divMessage" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%;">
                            </td>
                            <td style="margin-right: 5px; width: 50%; text-align: right">
                                <asp:Literal ID="ltlHuScan" runat="server" Text="<%$Resources:Language,ScanBarcode%>" />
                                <asp:TextBox ID="tbHuScan" runat="server" OnTextChanged="tbHuScan_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:Button ID="btnHuScan" runat="server" Text="<%$Resources:Language,ButtonScan%>"
                                    CssClass="hidden" />
                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="GridView">
                    <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False">
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
                                    <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("LocationCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuId%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("HuId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLotNo%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LotNo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataQty%>">
                                <ItemTemplate>
                                    <asp:Label ID="tbQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataQty%>">
                                <ItemTemplate>
                                    <asp:Label ID="tbCurrentQty" runat="server" Text='<%# Bind("CurrentQty","{0:0.########}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>
