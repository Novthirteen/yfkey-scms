<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Production_Backflush_Main" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

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
                <asp:Literal ID="ltlProductLine" runat="server" Text="${MasterData.Production.Feed.ProductLine}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbProductLine" runat="server" DescField="Description" ValueField="Code"
                    OnTextChanged="tbProductLine_TextChanged" AutoPostBack="true" ServicePath="FlowMgr.service"
                    ServiceMethod="GetProductionFlow" Width="250" CssClass="inputRequired" />
                <asp:RequiredFieldValidator ID="rfvProductLine" runat="server" ControlToValidate="tbProductLine"
                    ErrorMessage="${MasterData.Production.Feed.ProductLine.Required}" Display="Dynamic"
                    ValidationGroup="vgSaveGroup" />
            </td>
            <td class="td01">
            </td>
            <td class="td02">
                <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" Text="${Common.Button.Confirm}"
                    CssClass="button2" ValidationGroup="vgSaveGroup" />
            </td>
        </tr>
    </table>
</fieldset>
<fieldset>
    <legend>${MasterData.Production.Feeded.Detail}</legend>
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
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.ItemCode}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.ItemDescription}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("Item.Description") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.UomCode}">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Item.Uom.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.UnitCount}">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("Item.UnitCount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.Qty}">
                    <ItemTemplate>
                        <asp:Label ID="tbQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="${MasterData.Production.Feed.BackflushQty}">
                    <ItemTemplate>
                        <asp:Label ID="tbBackflushQty" runat="server" Text='<%# Bind("BackflushQty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.RemainQty}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbRemainQty" runat="server" Text="0" />
                         <asp:RangeValidator ID="rvQty" runat="server" ControlToValidate="tbRemainQty" ErrorMessage="${Common.Validator.Valid.Number}"
                            Display="Dynamic" Type="Double" MinimumValue="0" MaximumValue="10000000" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
