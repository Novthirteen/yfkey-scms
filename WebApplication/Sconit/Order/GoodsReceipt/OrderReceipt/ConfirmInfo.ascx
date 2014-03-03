<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ConfirmInfo.ascx.cs" Inherits="Order_GoodsReceipt_OrderReceipt_ConfirmInfo" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="floatdiv">
    <fieldset>
        <legend>${MasterData.Common.Confirm.Info}</legend>
        <div class="GridView">
            <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False" >
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataSequence%>">
                        <ItemTemplate>
                            <asp:Label ID="lblSeq" runat="server" Text='<%# Bind("Sequence") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                        <ItemTemplate>
                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemDesc%>">
                        <ItemTemplate>
                            <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("Item.Description") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataReferenceItemCode%>">
                        <ItemTemplate>
                            <asp:Label ID="lblReferenceItemCode" runat="server" Text='<%# Bind("ReferenceItemCode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUom%>">
                        <ItemTemplate>
                            <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Uom.Code") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUnitCount%>">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("UnitCount","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLocationFrom%>" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblLocFrom" runat="server" Text='<%# Bind("DefaultLocationFrom.Code") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLocationTo%>">
                        <ItemTemplate>
                            <asp:Label ID="lblLocTo" runat="server" Text='<%# Bind("DefaultLocationTo.Code") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataOrderedQty%>">
                        <ItemTemplate>
                            <asp:Label ID="tbOrderQty" runat="server" Text='<%# Bind("OrderedQty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataReceivedQty%>">
                        <ItemTemplate>
                            <asp:Label ID="lblReceivedQty" runat="server" Text='<%# Bind("ReceivedQty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataCurrentReceiveQty%>">
                        <ItemTemplate>
                            <asp:Label ID="tbReceiveQty" runat="server" Text='<%# Bind("CurrentReceiveQty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataCurrentRejectQty%>">
                        <ItemTemplate>
                            <asp:Label ID="tbRejectQty" runat="server" Text='<%# Bind("CurrentRejectQty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataCurrentScrapQty%>">
                        <ItemTemplate>
                            <asp:Label ID="tbScrapQty" runat="server" Text='<%# Bind("CurrentScrapQty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuId%>" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="tbHuId" runat="server" Text='<%# Bind("HuId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuQty%>" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="tbHuQty" runat="server" Text='<%# Bind("HuQty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <table class="mtable">
            <tr>
                <td class="ttd01">
                    <asp:Literal ID="lblIsOddCreateHu" runat="server" Text="${MasterData.Flow.IsOddCreateHu}:" Visible="false" />
                </td>
                <td class="ttd02">
                    <asp:CheckBox ID="cbIsOddCreateHu" runat="server" Visible="false"></asp:CheckBox>
                </td>
                <td class="td01">
                </td>
                <td class="td02">
                    <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" Text="${Common.Button.Confirm}"
                        CssClass="button2" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        CssClass="button2" />
                </td>
            </tr>
        </table>
    </fieldset>
</div>
