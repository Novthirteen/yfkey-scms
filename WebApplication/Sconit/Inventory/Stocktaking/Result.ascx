<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Result.ascx.cs" Inherits="Inventory_Stocktaking_Result" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="ResultDetail.ascx" TagName="Detail" TagPrefix="uc2" %>

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
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblItemCode" runat="server" Text="${Common.Business.Item}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItemCode" runat="server" Width="250" DescField="Description" ValueField="Code"
                    ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" MustMatch="true" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblBinCode" runat="server" Text="${Common.Business.Bin}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbBinCode" runat="server" Width="250" DescField="Description" ValueField="Code"
                    ServicePath="StorageBinMgr.service" ServiceMethod="GetStorageBinByLocation" MustMatch="true" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlOptions" runat="server" Text="${MasterData.Inventory.Stocktaking.Options}:" />
            </td>
            <td class="td02">
                <asp:CheckBox ID="cbShortage" runat="server" Text="${MasterData.Inventory.Stocktaking.Shortage}"
                    Checked />
                <asp:CheckBox ID="cbProfit" runat="server" Text="${MasterData.Inventory.Stocktaking.Profit}"
                    Checked />
                <asp:CheckBox ID="cbEqual" runat="server" Text="${MasterData.Inventory.Stocktaking.Equal}"
                    Checked />
            </td>
            <td class="td01">
            </td>
            <td class="t02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" CssClass="button2"
                        OnClick="btnExport_Click" />
                    <asp:Button ID="btnExportDetail" runat="server" Text="${Common.Button.ExportDetail}"
                        OnClick="btnExportDetail_Click" />
                    <cc1:Button ID="btnAdjust" runat="server" Text="${Common.Button.Adjust}" OnClick="btnAdjust_Click"
                        FunctionId="CycleCountAdjust" />
                    <asp:Button ID="btnClose" runat="server" Text="${Common.Button.Close}" OnClick="btnClose_Click" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
<fieldset>
    <legend>${MasterData.Inventory.Stocktaking.ResultDetail}</legend>
    <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_List_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <div onclick="GVCheckClick()">
                        <asp:CheckBox ID="CheckAll" runat="server" />
                    </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBoxGroup" name="CheckBoxGroup" runat="server" Visible="false" />
                    <asp:HiddenField ID="hfId" runat="server" Value='<%# Bind("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No." ItemStyle-Width="30">
                <ItemTemplate>
                    <asp:Literal ID="ltlSeq" runat="server" Text='<%# (Container as GridViewRow).RowIndex+1 %> ' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.Business.Bin}">
                <ItemTemplate>
                    <asp:Label ID="lblStorageBin" runat="server" Text='<%# Bind("StorageBin") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.Business.Item}">
                <ItemTemplate>
                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.Business.ItemDescription}">
                <ItemTemplate>
                    <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("Item.Description") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.Business.Uom}">
                <ItemTemplate>
                    <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Item.Uom.Code") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Inventory.Stocktaking.Shortage}">
                <ItemTemplate>
                    <asp:LinkButton ID="lblShortageQty" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "StorageBin") +"|"+DataBinder.Eval(Container.DataItem, "Item.Code")%>'
                        Text='<%# Eval("ShortageQty", "{0:#.######}")%>' OnClick="lbtnShortageQty_Click">
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Inventory.Stocktaking.Profit}">
                <ItemTemplate>
                    <asp:LinkButton ID="lblProfitQty" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "StorageBin") +"|"+DataBinder.Eval(Container.DataItem, "Item.Code") %>'
                        Text='<%# Eval("ProfitQty", "{0:#.######}")%>' OnClick="lbtnProfitQty_Click">
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Inventory.Stocktaking.Equal}">
                <ItemTemplate>
                    <asp:LinkButton ID="lblEqualQty" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "StorageBin") +"|"+DataBinder.Eval(Container.DataItem, "Item.Code") %>'
                        Text='<%# Eval("EqualQty", "{0:#.######}")%>' OnClick="lbtnEqualQty_Click">
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Inventory.Stocktaking.InvQty}">
                <ItemTemplate>
                    <asp:Label ID="lblInvQty" runat="server" Text='<%# Bind("InvQty","{0:0.######}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Inventory.Stocktaking.Qty}">
                <ItemTemplate>
                    <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty","{0:0.######}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Inventory.Stocktaking.DiffQty}">
                <ItemTemplate>
                    <asp:Label ID="lblDiffQty" runat="server" Text='<%# Bind("DiffQty","{0:0.######}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:GridView ID="GVResultDetail" runat="server" AutoGenerateColumns="false" Visible="false"
        OnRowDataBound="GV_ResultDetail_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="No." ItemStyle-Width="30">
                <ItemTemplate>
                    <asp:Literal ID="ltlSeq" runat="server" Text='<%# (Container as GridViewRow).RowIndex+1 %> ' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.Business.Bin}">
                <ItemTemplate>
                    <asp:Label ID="lblStorageBin" runat="server" Text='<%# Bind("StorageBin") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.Business.Item}">
                <ItemTemplate>
                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.Business.ItemDescription}">
                <ItemTemplate>
                    <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("Item.Description") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.Business.Uom}">
                <ItemTemplate>
                    <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Item.Uom.Code") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.Business.HuId}">
                <ItemTemplate>
                    <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("HuId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.Business.Qty}">
                <ItemTemplate>
                    <asp:Label ID="lblQty" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblDiffQty" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</fieldset>
<uc2:Detail ID="ucDetail" runat="server" Visible="false" />
