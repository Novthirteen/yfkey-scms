<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderDetailList.ascx.cs"
    Inherits="Inventory_PrintHu_OrderDetailList" %>

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
                <asp:TemplateField HeaderText="${Inventory.PrintHu.OrderDetail.Sequence}">
                    <ItemTemplate>
                        <asp:Label ID="lblSeq" runat="server" Text='<%# Bind("Sequence") %>' onmouseup="if(!readOnly)select();" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.OrderDetail.Item.Code}">
                    <ItemTemplate>
                        <asp:TextBox ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' ReadOnly="true"
                            Width="100" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.OrderDetail.Item.Description}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("Item.Description") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.OrderDetail.ReferenceItem}">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceItemCode" runat="server" Text='<%# Bind("ReferenceItemCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.OrderDetail.Uom}">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Uom.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.OrderDetail.UnitCount}">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("UnitCount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.OrderDetail.HuLotSize}">
                    <ItemTemplate>
                        <asp:Label ID="lblHuLotSize" runat="server" Text='<%# Bind("HuLotSize","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.OrderDetail.PackageType}">
                    <ItemTemplate>
                        <asp:Label ID="lblPackageType" runat="server" Text='<%#Bind("PackageType") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.OrderDetail.LotNo}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbLotNo" runat="server" onmouseup="if(!readOnly)select();"  Text='<%# Bind("HuLotNo") %>' Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.OrderDetail.OrderQty}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbOrderQty" runat="server" onmouseup="if(!readOnly)select();" Text='<%# Bind("OrderedQty","{0:0.########}") %>'
                            Width="50"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revOrderQty" runat="server" Display="Dynamic"
                            ControlToValidate="tbOrderQty" Enabled="false"></asp:RegularExpressionValidator>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="tablefooter">
        <asp:Button ID="btnPrint" runat="server" Text="${Common.Button.Print}" OnClick="btnPrint_Click"
            CssClass="button2" ValidationGroup="vgPrint" />
    </div>
</fieldset>
