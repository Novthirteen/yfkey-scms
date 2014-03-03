<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderIOList.ascx.cs" Inherits="Visualization_InTransit_OrderIOList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<div id="floatdiv">
    <div id='floatdivtitle'>
        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" CssClass="btnClose" />
    </div>
    <fieldset>
        <legend>${Reports.ViewDetail}</legend>
        <div class="GridView">
            <sc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" seqtext="No."
                ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
                CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
                SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="${Common.Business.OrderNo}">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("OrderDetail.OrderHead.OrderNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="${Common.From}">
                        <ItemTemplate>
                            <asp:Label ID="lblPartyFrom" runat="server" Text='<%# Bind("OrderDetail.OrderHead.PartyFrom.Code") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="${Common.Business.ItemCode}">
                        <ItemTemplate>
                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("OrderDetail.Item.Code") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.ItemDescription}">
                        <ItemTemplate>
                            <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("OrderDetail.Item.Description") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.ReferenceItemCode}">
                        <ItemTemplate>
                            <asp:Label ID="lblReferenceItemCode" runat="server" Text='<%# Bind("OrderDetail.ReferenceItemCode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.Uom}">
                        <ItemTemplate>
                            <asp:Label ID="lblUom" runat="server" Text='<%# Bind("OrderDetail.Uom.Code") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.UnitCount}">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("OrderDetail.UnitCount","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${MasterData.Order.OrderDetail.OrderedQty}">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderedQty" runat="server" Text='<%# Bind("OrderDetail.OrderedQty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${MasterData.Order.OrderDetail.ShippedQty}">
                        <ItemTemplate>
                            <asp:Label ID="lblShippedQty" runat="server" Text='<%# Bind("OrderDetail.ShippedQty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Reports.QtyToBeOut}">
                        <ItemTemplate>
                            <asp:Label ID="lblRemainShippedQty" runat="server" Text='<%# Bind("OrderDetail.RemainShippedQty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Reports.InTransitQty}">
                        <ItemTemplate>
                            <asp:Label ID="lblInTransitQty" runat="server" Text='<%# Bind("OrderDetail.InTransitQty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${MasterData.Order.OrderDetail.ReceivedQty}">
                        <ItemTemplate>
                            <asp:Label ID="lblReceivedQty" runat="server" Text='<%# Bind("OrderDetail.ReceivedQty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Reports.QtyToBeIn}">
                        <ItemTemplate>
                            <asp:Label ID="lblRemainReceivedQty" runat="server" Text='<%# Bind("OrderDetail.RemainReceivedQty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </sc1:GridView>
            <sc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
            </sc1:GridPager>
        </div>
    </fieldset>
</div>
<div id='divHide' />
