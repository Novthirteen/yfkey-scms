<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Reports_InvDetail_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<%@ Register Src="~/Visualization/InTransit/OrderIOList.ascx" TagName="OrderIOList"
    TagPrefix="uc2" %>
<%@ Register Src="~/Visualization/InTransit/InTransitList.ascx" TagName="InTransitList"
    TagPrefix="uc2" %>
<%@ Register Src="~/Visualization/InTransit/PickList.ascx" TagName="PickList" TagPrefix="uc2" %>
<fieldset>
    <div class="GridView">
        <sc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" seqtext="No."
            ShowSeqNo="true" AllowSorting="false" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="${Common.Business.ItemCode}" SortExpression="Item.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item.Code")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemDescription}" SortExpression="Item.Description">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("Item.Description")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Location}" SortExpression="Location.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location.Code")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Location.Name}" SortExpression="Location.Name">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationName" runat="server" Text='<%# Eval("Location.Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Uom}" SortExpression="Item.Uom.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("Item.Uom.Code")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Qty" HeaderText="${Reports.Inv}" SortExpression="Qty"
                    DataFormatString="{0:0.###}" />
                <asp:BoundField DataField="ConsignmentQty" HeaderText="${Reports.CsInvQty}" SortExpression="ConsignmentQty"
                    DataFormatString="{0:0.###}" />
                <asp:TemplateField HeaderText="${Reports.QtyToBeIn}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbQtyToBeIn" runat="server" Text='<%# Eval("QtyToBeIn","{0:0.###}")%>'
                            OnClick="lbQtyToBeInOrOut_Click"></asp:LinkButton>
                        <asp:UpdatePanel ID="upQtyToBeIn" runat="server">
                            <ContentTemplate>
                                <uc2:OrderIOList ID="ucOrderInList" runat="server" Visible="false" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lbQtyToBeIn" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.InTransitQty}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbInTransitQtyIn" runat="server" Text='<%# Eval("InTransitQty","{0:0.###}")%>'
                            OnClick="lbInTransitQtyInOrOut_Click"></asp:LinkButton>
                        <asp:UpdatePanel ID="upInTransitQtyIn" runat="server">
                            <ContentTemplate>
                                <uc2:InTransitList ID="ucInTransitListIn" runat="server" Visible="false" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lbInTransitQtyIn" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.QtyToBeOut}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbQtyToBeOut" runat="server" Text='<%# Eval("QtyToBeOut","{0:0.###}")%>'
                            OnClick="lbQtyToBeInOrOut_Click"></asp:LinkButton>
                        <asp:UpdatePanel ID="upQtyToBeOut" runat="server">
                            <ContentTemplate>
                                <uc2:OrderIOList ID="ucOrderOutList" runat="server" Visible="false" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lbQtyToBeOut" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.PickedQty}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbPickedQty" runat="server" Text='<%# Eval("PickedQty","{0:0.###}")%>'
                            OnClick="lbPickedQty_Click"></asp:LinkButton>
                        <asp:UpdatePanel ID="upPickedQty" runat="server">
                            <ContentTemplate>
                                <uc2:PickList ID="ucPickList" runat="server" Visible="false" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lbPickedQty" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.InTransitQtyOut}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbInTransitQtyOut" runat="server" Text='<%# Eval("InTransitQtyOut","{0:0.###}")%>'
                            OnClick="lbInTransitQtyInOrOut_Click"></asp:LinkButton>
                        <asp:UpdatePanel ID="upInTransitQtyOut" runat="server">
                            <ContentTemplate>
                                <uc2:InTransitList ID="ucInTransitListOut" runat="server" Visible="false" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lbInTransitQtyOut" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PAB" HeaderText="${Reports.PAB}" DataFormatString="{0:0.###}" />
            </Columns>
        </sc1:GridView>
        <sc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </sc1:GridPager>
    </div>
</fieldset>
