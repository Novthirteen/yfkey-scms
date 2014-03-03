<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReceiptList.ascx.cs" Inherits="Inventory_PrintHu_ReceiptList" %>

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
        <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div onclick="GVCheckClick()">
                            <asp:CheckBox ID="CheckAll" runat="server" />
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:HiddenField ID="hfId" runat="server" Value='<%# Bind("Id") %>' />
                        <asp:CheckBox ID="CheckBoxGroup" name="CheckBoxGroup" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.ReceiptDetail.Item.Code}">
                    <ItemTemplate>
                        <asp:TextBox ID="lblItemCode" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.Item.Code") %>'
                            ReadOnly="true" Width="100" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.ReceiptDetail.Item.Description}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.Item.Description") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.ReceiptDetail.ReferenceItem}">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceItemCode" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.ReferenceItemCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.ReceiptDetail.Uom}">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.Uom.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.ReceiptDetail.UnitCount}">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.UnitCount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.ReceiptDetail.PackageType}">
                    <ItemTemplate>
                        <asp:Label ID="lblPackageType" runat="server" Text='<%#Bind("OrderLocationTransaction.OrderDetail.PackageType") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="HuId" HeaderText="${Inventory.PrintHu.ReceiptDetail.HuId}" />
                <asp:BoundField DataField="LotNo" HeaderText="${Inventory.PrintHu.ReceiptDetail.LotNo}" />
                <asp:BoundField DataField="ReceivedQty" HeaderText="${Inventory.PrintHu.ReceiptDetail.Qty}" />
            </Columns>
        </asp:GridView>
    </div>
    <div class="tablefooter">
        <asp:Button ID="btnPrint" runat="server" Text="${Common.Button.Print}" OnClick="btnPrint_Click"
            CssClass="button2" ValidationGroup="vgPrint" />
    </div>
</fieldset>
