<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewShipList.ascx.cs"
    Inherits="Distribution_OrderIssue_ViewShipList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>


<%--<div id="floatdiv">--%>
<fieldset>
    <legend>${MasterData.Order.OrderDetail}</legend>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataOrderNo%>">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.OrderHead.OrderNo","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataSequence%>">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfId" runat="server" Value='<%# Bind("Id") %>' />
                        <asp:HiddenField ID="hfOrderLocTransId" runat="server" Value='<%# Bind("OrderLocationTransaction.Id") %>' />
                        <asp:Label ID="lblSeq" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.Sequence") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.Item.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemDesc%>">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.Item.Description") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataReferenceItemCode%>">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceItemCode" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.ReferenceItemCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUom%>">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.Uom.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUnitCount%>">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.UnitCount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLocationFrom%>">
                    <ItemTemplate>
                        <asp:Label ID="lblLocFrom" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.DefaultLocationFrom.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLocationTo%>">
                    <ItemTemplate>
                        <asp:Label ID="lblLocTo" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.DefaultLocationTo.Code") %>' />
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
                        <asp:Label ID="lblOrderQty" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.OrderedQty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataShippedQty%>">
                    <ItemTemplate>
                        <asp:Label ID="lblShippedQty" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.ShippedQty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataCurrentShipQty%>">
                    <ItemTemplate>
                        <asp:Label ID="lblShipQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="tablefooter">
        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
            CssClass="button2" />
        <asp:Button ID="btnPrint" runat="server" Text="${Common.Button.Print}" OnClick="btnPrint_Click"
            CssClass="button2" />
    </div>
</fieldset>
<%--</div>--%>
