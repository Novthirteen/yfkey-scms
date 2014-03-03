<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MasterData_Bom_BomView_List" %>
<fieldset>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="ParCode" HeaderText="${MasterData.Bom.ParCode}" SortExpression="ParCode" />
                <asp:BoundField DataField="ParUom" HeaderText="${Common.Business.Uom}" SortExpression="ParUom" />
                <asp:BoundField DataField="CompCode" HeaderText="${MasterData.Bom.CompCode}" SortExpression="CompCode" />
                <asp:BoundField DataField="CompUom" HeaderText="${Common.Business.Uom}" SortExpression="CompUom" />
                <asp:BoundField DataField="CalculatedQty" HeaderText="${MasterData.Bom.RateQty}" SortExpression="CalculatedQty"
                    DataFormatString="{0:0.########}" />
                <asp:BoundField DataField="StructureType" HeaderText="${MasterData.Bom.StructureType}"
                    SortExpression="StructureType" />
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
