<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Reports_PassRate_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<fieldset>
    <div class="GridView">
        <sc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" SkinID="GV"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" seqtext="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Flow" HeaderText="${Common.Business.ProductionLine}" SortExpression="Flow" />
                <asp:BoundField DataField="Description" HeaderText="${Common.Business.Description}"
                    SortExpression="Description" />
                <asp:TemplateField HeaderText="${Common.Business.Region}" SortExpression="PartyTo.Name">
                    <ItemTemplate>
                        <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("PartyTo.Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="EffDate" HeaderText="${Common.Business.Date}" SortExpression="EffDate"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:TemplateField HeaderText="${MasterData.WorkCalendar.Shift}" SortExpression="Shift">
                    <ItemTemplate>
                        <asp:Label ID="lblShift" runat="server" Text='<%# Eval("Shift.ShiftName")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
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
                <asp:TemplateField HeaderText="${Common.Business.Uom}" SortExpression="Uom">
                    <ItemTemplate>
                        <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("Uom")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="OrderedQty" HeaderText="${MasterData.Order.LocTrans.OrderdQty}"
                    SortExpression="OrderedQty" DataFormatString="{0:0.########}" />
                <asp:BoundField DataField="ReceivedQty" HeaderText="${MasterData.Order.LocTrans.AccumulateQty}"
                    SortExpression="ReceivedQty" DataFormatString="{0:0.########}" />
                <asp:BoundField DataField="RejectedQty" HeaderText="${MasterData.Order.LocTrans.AccumulateScrapQty}"
                    SortExpression="RejectedQty" DataFormatString="{0:0.########}" />
                <asp:BoundField DataField="ScrapQty" HeaderText="${MasterData.Order.LocTrans.AccumulateRejectQty.Production}"
                    SortExpression="ScrapQty" DataFormatString="{0:0.########}" />
                <asp:TemplateField HeaderText="${Reports.ShiftProd.PercentPass}">
                    <ItemTemplate>
                        <asp:Label ID="lblPassRate" runat="server" Text='<%# Eval("PassRate","{0:P}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </sc1:GridView>
        <sc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </sc1:GridPager>
    </div>
</fieldset>
