<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InventoryList.ascx.cs" Inherits="Inventory_PrintHu_InventoryList" %>
<script type="text/javascript" language="javascript">
    function GVCheckClick() {
        if ($(".GVHeader input:checkbox").attr("checked") == true) {
            $(".GVRow input:checkbox").attr("checked", true);
            $(".GVAlternatingRow input:checkbox").attr("checked", true);
        }
        else {
            $(".GVRow input:checkbox").attr("checked", false);
            $(".GVAlternatingRow input:checkbox").attr("checked", false);
        }
    }
</script>

<fieldset>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AllowPaging="False" DataKeyNames="Id" AllowSorting="False"
            AutoGenerateColumns="False" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div onclick="GVCheckClick()">
                            <asp:CheckBox ID="CheckAll" runat="server" />
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:HiddenField ID="hfHuId" runat="server" Value='<%# Bind("Hu.HuId") %>' />
                        <asp:CheckBox ID="CheckBoxGroup" name="CheckBoxGroup" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${Inventory.PrintHu.LocationLotDetail.LocationCode}" SortExpression="Location.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Location.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${Inventory.PrintHu.LocationLotDetail.LocationName}" SortExpression="Location.Name">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Location.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText=" ${Inventory.PrintHu.LocationLotDetail.Bin}" SortExpression="StorageBin">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "StorageBin.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${Inventory.PrintHu.LocationLotDetail.HuId}" SortExpression="Hu.HuId">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Hu.HuId")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${Inventory.PrintHu.LocationLotDetail.ItemCode}" SortExpression="Item.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Item.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${Inventory.PrintHu.LocationLotDetail.ItemDescription}" SortExpression="Item.Desc1">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Item.Description")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${Inventory.PrintHu.LocationLotDetail.Uom}" SortExpression="Hu.Uom.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Hu.Uom.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${Inventory.PrintHu.LocationLotDetail.UnitCount}" SortExpression="Hu.UnitCount">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Hu.UnitCount", "{0:0.########}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="LotNo" HeaderText="${Inventory.PrintHu.LocationLotDetail.LotNo}" SortExpression="LotNo" />
                <asp:BoundField DataField="Qty" DataFormatString="{0:0.########}" HeaderText="${Inventory.PrintHu.LocationLotDetail.Qty}" SortExpression="Qty" />
                <asp:BoundField DataField="CreateDate" DataFormatString="{0:yyyy-MM-dd HH:mm}" HeaderText="${Inventory.PrintHu.LocationLotDetail.CreateDate}" SortExpression="CreateDate" />
            </Columns>
        </asp:GridView>
    </div>
</fieldset>