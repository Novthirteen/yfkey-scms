<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RequiredQtyList.ascx.cs"
    Inherits="Schedule_DemandSchedule_RequiredQtyList" %>
<fieldset>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" SkinID="GV"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="false" AllowPaging="false" Width="100%" CellMaxLength="10"
            OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="${MRP.Schedule.Item}" DataField="Item" />
                <asp:BoundField HeaderText="${MRP.Schedule.Flow}" DataField="Flow" />
                <asp:BoundField HeaderText="${MRP.Schedule.Location}" DataField="LocationTo" />
                <asp:BoundField HeaderText="${MRP.Schedule.StartTime}" DataFormatString="{0:MM-dd}"
                    DataField="StartTime" />
                <asp:BoundField HeaderText="${MRP.Schedule.WinTime}" DataFormatString="{0:MM-dd}"
                    DataField="WindowTime" />
                <asp:BoundField HeaderText="${MRP.Schedule.SourceDateType}" DataField="SourceDateType" />
                <asp:BoundField HeaderText="${MRP.Schedule.SourceType}" DataField="SourceType" />
                <asp:BoundField HeaderText="${Common.Business.Qty}" DataField="Qty" DataFormatString="{0:#,##0.##}" />
                <asp:BoundField HeaderText="${MRP.Schedule.SourceItem}" DataField="SourceItemCode" />
                <asp:BoundField HeaderText="${MRP.Schedule.SourceItemDescription}" DataField="SourceItemDescription" />
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
