<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderTracer.ascx.cs" Inherits="Order_OrderDetail_OrderTracer" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<div id="floatdiv">
    <fieldset>
        <legend>${MasterData.Flow.Basic.Info}</legend>
        <asp:FormView ID="FV_FormView" runat="server" DefaultMode="ReadOnly" DataKeyNames="Id">
            <ItemTemplate>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlItemCode" runat="server" Text="${Common.Business.ItemCode}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbItemCode" runat="server" ReadOnly="true" Text='<%# Bind("Item.Code") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlItemDescription" runat="server" Text="${Common.Business.ItemDescription}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbItemDescription" runat="server" ReadOnly="true" Text='<%# Bind("Item.Description") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlUom" runat="server" Text="${Common.Business.Uom}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbUom" runat="server" ReadOnly="true" Text='<%# Bind("Uom.Code") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlUnitCount" runat="server" Text="${Common.Business.UnitCount}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbUnitCount" runat="server" ReadOnly="true" Text='<%# Bind("UnitCount","{0:0.########}") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlStartTime" runat="server" Text="${MasterData.Order.OrderHead.StartTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbStartTime" runat="server" ReadOnly="true" Text='<%# Bind("OrderHead.StartTime") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlWindowTime" runat="server" Text="${MasterData.Order.OrderHead.WindowTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbWindowTime" runat="server" ReadOnly="true" Text='<%# Bind("OrderHead.WindowTime") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlRequiredQty" runat="server" Text="${MasterData.Order.OrderDetail.RequiredQty}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbRequiredQty" runat="server" ReadOnly="true" Text='<%# Bind("RequiredQty","{0:0.########}") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlOrderedQty" runat="server" Text="${MasterData.Order.OrderDetail.OrderedQty}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbOrderedQty" runat="server" ReadOnly="true" Text='<%# Bind("OrderedQty","{0:0.########}") %>' />
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
    </fieldset>
    <fieldset>
        <legend>${Reports.ViewDetail}</legend>
        <div class="GridView">
            <sc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" seqtext="No."
                ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
                CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
                SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="${Common.Business.Type}">
                        <ItemTemplate>
                            <asp:Label ID="lblTracerType" runat="server" Text='<%# Bind("TracerType") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.Code}">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("Code") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.Date}">
                        <ItemTemplate>
                            <asp:Label ID="lblReqTime" runat="server" Text='<%# Bind("ReqTime","{0:yyyy-MM-dd HH:mm}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.Item}">
                        <ItemTemplate>
                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="${Common.Business.ItemDescription}">
                        <ItemTemplate>
                            <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("OrderDetail.Item.Description") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="${Common.Business.Qty}">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderedQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.Remark}">
                        <ItemTemplate>
                            <asp:Label ID="lblMemo" runat="server" Text='<%# Bind("Memo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </sc1:GridView>
            <sc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
            </sc1:GridPager>
        </div>
    </fieldset>
    <div class="tablefooter">
        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" CssClass="btnClose" />
    </div>
</div>
