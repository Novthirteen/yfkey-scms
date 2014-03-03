<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Visualization_InvVisualBoard_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<%@ Register Src="~/Visualization/InTransit/OrderIOList.ascx" TagName="OrderIOList"
    TagPrefix="uc2" %>
<%@ Register Src="~/Visualization/InTransit/InTransitList.ascx" TagName="InTransitList"
    TagPrefix="uc2" %>
<%@ Register Src="~/Visualization/InvVisualBoard/Kanban.ascx" TagName="Kanban" TagPrefix="uc2" %>
<fieldset>
    <legend>
        <asp:Literal ID="ltlLocation" runat="server"></asp:Literal></legend>
    <div class="GridView">
        <sc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" seqtext="No."
            ShowSeqNo="true" AllowSorting="false" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="${Common.Business.ItemCode}" SortExpression="FlowDetail.Item.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("FlowDetail.Item.Code")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemDescription}" SortExpression="FlowDetail.Item.Description">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("FlowDetail.Item.Description")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Flow}" SortExpression="Flow">
                    <ItemTemplate>
                        <asp:Label ID="lblFlow" runat="server" Text='<%# Eval("Flow.Description")%>' ToolTip='<%# Eval("Flow")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Uom}" SortExpression="Item.Uom.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("FlowDetail.Uom.Code")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.UnitCount}" SortExpression="FlowDetail.UnitCount">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Eval("FlowDetail.UnitCount","{0:0.###}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="InvQty">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltlQty" runat="server" Text="${Reports.Inv}"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Image ID="imgInvQty" runat="server" ImageUrl="~/Images/Icon/Inv.gif" />
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblQty" runat="server" Text='<%# Eval("LocationDetail.Qty","{0:0.###}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="QtyToBeIn">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltlQtyToBeIn" runat="server" Text="${Reports.QtyToBeIn}"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Image ID="imgQtyToBeIn" runat="server" ImageUrl="~/Images/Icon/ToBeIn.gif" />
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbQtyToBeIn" runat="server" Text='<%# Eval("LocationDetail.QtyToBeIn","{0:0.###}")%>'
                            OnClick="lbQtyToBeIn_Click"></asp:LinkButton>
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
                <asp:TemplateField SortExpression="InTransitQty">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltlInTransitQty" runat="server" Text="${Reports.InTransitQty}"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Image ID="imgInTransitQty" runat="server" ImageUrl="~/Images/Icon/InTransit.gif" />
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbInTransitQty" runat="server" Text='<%# Eval("LocationDetail.InTransitQty","{0:0.###}")%>'
                            OnClick="lbInTransitQty_Click"></asp:LinkButton>
                        <asp:UpdatePanel ID="upInTransitQty" runat="server">
                            <ContentTemplate>
                                <uc2:InTransitList ID="ucInTransitList" runat="server" Visible="false" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lbInTransitQty" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="QtyToBeOut">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltlQtyToBeOut" runat="server" Text="${Reports.QtyToBeOut}"></asp:Literal>
                                </td>
                                <td>
                                    <asp:Image ID="imgQtyToBeOut" runat="server" ImageUrl="~/Images/Icon/ToBeOut.gif" />
                                </td>
                            </tr>
                        </table>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbQtyToBeOut" runat="server" Text='<%# Eval("LocationDetail.QtyToBeOut","{0:0.###}")%>'
                            OnClick="lbQtyToBeOut_Click"></asp:LinkButton>
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
                <asp:TemplateField HeaderText="${Reports.PAB}">
                    <ItemTemplate>
                        <asp:Label ID="lblPAB" runat="server" Text='<%# Eval("LocationDetail.PAB","{0:0.###}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Visualization.InvVisualBoard.Kanban}">
                    <ItemTemplate>
                        <uc2:Kanban ID="ucKanban" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </sc1:GridView>
        <sc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </sc1:GridPager>
    </div>
</fieldset>
