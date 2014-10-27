<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MasterData_Reports_Inventory_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="${Common.Business.ItemCode}" SortExpression="ItemCode">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode")%>' />&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemDescription}" SortExpression="ItemDesc">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("ItemDesc")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Uom}" SortExpression="Uom">
                    <ItemTemplate>
                        <asp:Label ID="lblItemUom" runat="server" Text='<%# Eval("Uom")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Location}" SortExpression="LocCode">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationCode" runat="server" Text='<%# Eval("LocCode")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.LocationName}" SortExpression="LocName">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationName" runat="server" Text='<%# Eval("LocName")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Bin}" SortExpression="BinCode">
                    <ItemTemplate>
                        <asp:Label ID="lblBinCode" runat="server" Text='<%# Eval("BinCode")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.LotNo}" SortExpression="LotNo">
                    <ItemTemplate>
                        <asp:Label ID="lblLotNo" runat="server" Text='<%# Eval("LotNo")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Qty" HeaderText="${Common.Business.Qty}" SortExpression="Qty"
                    DataFormatString="{0:0.###}" />
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var argumets = eventArgument.split("$");
        if (argumets[0] == "Sort") {
            document.getElementById("ctl01_ucSearch_PostBackSortHidden").value = argumets[1];
        } else {
            document.getElementById("ctl01_ucSearch_PostBackHidden").value = eventArgument;
        }
        document.getElementById("ctl01_ucSearch_btnSearch").click();
    }
</script>
