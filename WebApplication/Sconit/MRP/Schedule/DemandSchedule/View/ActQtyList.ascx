<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ActQtyList.ascx.cs" Inherits="Schedule_DemandSchedule_ActQtyList" %>
<fieldset>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="${MRP.Schedule.Item}" DataField="Item" />
                <asp:BoundField HeaderText="${MRP.Schedule.Flow}" DataField="Flow" />
                <asp:BoundField HeaderText="${MRP.Schedule.Location}" DataField="Location"/>
                <asp:BoundField HeaderText="${Common.Business.OrderNo}" DataField="OrderNo" />
                <asp:BoundField HeaderText="${MRP.Schedule.WinTime}" DataFormatString="{0:yyyy-MM-dd HH:mm}" DataField="WindowTime" />
                <asp:BoundField HeaderText="${Common.Business.Uom}" DataField="Uom" />
                <asp:BoundField HeaderText="${Common.Business.UnitCount}" DataField="UnitCount" DataFormatString="{0:#,##0.##}" />
                <asp:BoundField HeaderText="${Common.Business.Qty}" DataField="TransitQty" DataFormatString="{0:#,##0.##}" />
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
