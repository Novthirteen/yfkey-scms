<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductionReceiptItemList.ascx.cs"
    Inherits="Order_GoodsReceipt_OrderReceipt_ProductionReceiptItemList" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Hu/List.ascx" TagName="HuList" TagPrefix="uc2" %>

<script language="javascript" type="text/javascript">

    function setChanged() {
        $('#<%= hfIsChanged.ClientID %>').attr('value', true);
    }
</script>

<fieldset>
    <legend>
        <asp:Literal ID="ltlOrderNo" runat="server" Text="${MasterData.Order.OrderHead.CurrentOrder}" />
        :<%=OrderNo %></legend>
    <asp:HiddenField ID="hfIsChanged" runat="server" Value="false" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%;">
                            <asp:Literal ID="lblRefNo" runat="server" Text="<%$Resources:Language,MasterDataRefNo%>"
                                Visible="false" />
                            <asp:TextBox ID="tbRefNo" runat="server" Visible="false" />
                        </td>
                        <td style="margin-right: 5px; width: 50%; text-align: right">
                            <asp:Literal ID="ltlHuScan" runat="server" Text="<%$Resources:Language,QuickScanHu%>" />
                            <asp:TextBox ID="tbHuScan" runat="server" OnTextChanged="tbHuScan_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <asp:Button ID="btnHuScan" runat="server" Text="<%$Resources:Language,ButtonScan%>"
                                CssClass="hidden" />
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:CustomValidator ID="cvShipQty" runat="server" />
                <div class="GridView">
                    <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        OnRowDataBound="GV_List_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataSequence%>">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfId" runat="server" Value='<%# Bind("Id") %>' />
                                    <asp:HiddenField ID="hfOrderDetailId" runat="server" Value='<%# Bind("OrderDetail.Id") %>' />
                                    <asp:Label ID="lblSeq" runat="server" Text='<%# Bind("OrderDetail.Sequence") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("OrderDetail.Item.Code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemDesc%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("OrderDetail.Item.Description") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataReferenceItemCode%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblReferenceItemCode" runat="server" Text='<%# Bind("OrderDetail.ReferenceItemCode") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUom%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblUom" runat="server" Text='<%# Bind("OrderDetail.Uom.Code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUnitCount%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("OrderDetail.UnitCount","{0:0.########}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLocationFrom%>" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocFrom" runat="server" Text='<%# Bind("OrderDetail.DefaultLocationFrom.Code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLocationTo%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocTo" runat="server" Text='<%# Bind("OrderDetail.DefaultLocationTo.Code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUnitPrice%>" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitPrice" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataDiscountRate%>" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiscountRate" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataDiscount%>" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiscount" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataAmount%>" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblPrice" runat="server" Width="50" onfocus="this.blur();" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataOrderedQty%>">
                                <ItemTemplate>
                                    <asp:Label ID="tbOrderQty" runat="server" Text='<%# Bind("OrderDetail.OrderedQty","{0:0.########}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataReceivedQty%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblReceivedQty" runat="server" Text='<%# Bind("OrderDetail.ReceivedQty","{0:0.########}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Literal ID="ltlReceiveQty" runat="server" Text='<%$Resources:Language,MasterDataCurrentReceiveQty%>' />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="tbReceiveQty" runat="server" onmouseup="if(!readOnly)select();"
                                        onchange="setChanged();" Width="50"></asp:TextBox>
                                    <asp:RangeValidator ID="rvReceiveQty" ControlToValidate="tbReceiveQty" runat="server"
                                        Display="Dynamic" ErrorMessage="*" MaximumValue="999999999" MinimumValue="0"
                                        Type="Double" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Literal ID="ltlRejectQty" runat="server" Text='<%$Resources:Language,MasterDataCurrentRejectQty%>' />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="tbRejectQty" runat="server" onmouseup="if(!readOnly)select();" Width="50"
                                        onchange="setChanged();"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Literal ID="ltlScrapQty" runat="server" Text='<%$Resources:Language,MasterDataCurrentScrapQty%>' />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="tbScrapQty" runat="server" onmouseup="if(!readOnly)select();" Width="50"
                                        onchange="setChanged();"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuId%>" ItemStyle-Width="170px">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbHuId" runat="server" Width="100" onfocus="this.blur();" Text='<%# Bind("HuId") %>' />
                                    <asp:TextBox ID="tbHuReceiveQty" runat="server" onmouseup="this.select();" Text='<%# Bind("HuQty","{0:0.########}") %>'
                                        Width="50" />
                                    <asp:RequiredFieldValidator ID="rfvHuReceiveQty" runat="server" ErrorMessage="${MasterData.Hu.Qty.Empty}"
                                        Display="Dynamic" ControlToValidate="tbHuReceiveQty" ValidationGroup="vgSave" />
                                    <asp:RegularExpressionValidator ID="revHuReceiveQty" ControlToValidate="tbHuReceiveQty"
                                        runat="server" ValidationGroup="vgSave" ErrorMessage="*" ValidationExpression="^(-)?(0|([1-9]\\d*))(\\.\\d+)?$"
                                        Display="Dynamic" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnScanHu" />
        </Triggers>
    </asp:UpdatePanel>
</fieldset>
<uc2:HuList ID="ucHuList" runat="server" Visible="false" />
<div class="tablefooter">
    <asp:Button ID="btnScanHu" runat="server" OnClick="btnScanHu_Click" Text="${Common.Button.BatchScanHu}"
        CssClass="button2" />
</div>
