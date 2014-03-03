<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Match.ascx.cs" Inherits="Finance_PlanBill_SO_Match" %>

<script type="text/javascript" language="javascript">
    function GoSearch() {
        var btnSearch = document.getElementById("<%=btnSearch.ClientID %>");
        if (event.keyCode == 13) {
            event.keyCode = 9;
            event.returnValue = false;
            btnSearch.click();
        }
    }
</script>

<div id="floatdiv">
    <fieldset>
        <legend>${MasterData.PlannedBill.MatchParam}</legend>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="ltlItemCode" runat="server" Text="${Common.Business.ItemCode}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbItemCode" runat="server" onkeypress="GoSearch()" />
                </td>
                <td class="td01">
                    <asp:Literal ID="ltlQty" runat="server" Text="${Common.Business.Qty}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbQty" runat="server" onkeypress="GoSearch()" />
                    <asp:RangeValidator ID="rvQty" ControlToValidate="tbQty" runat="server" Display="Dynamic"
                        ErrorMessage="*" MaximumValue="999999999" MinimumValue="0" Type="Double" />
                </td>
            </tr>
            <tr>
                <td colspan="3" />
                <td class="td02">
                    <div class="buttons">
                        <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                            CssClass="query" />
                        <asp:Button ID="btnSave" runat="server" Text="${Common.Button.Save}" OnClick="btnSave_Click"
                            CssClass="apply" />
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                            CssClass="back" />
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>${MasterData.PlannedBill.MatchDetailList}</legend>
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="No." ItemStyle-Width="30">
                    <ItemTemplate>
                        <asp:Literal ID="ltlSeq" runat="server" Text='<%# (Container as GridViewRow).RowIndex+1 %> ' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.PlannedBill.ExtReceiptNo}">
                    <ItemTemplate>
                        <asp:Label ID="lblExtReceiptNo" runat="server" Text='<%# Bind("OrderNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemCode}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemDescription}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("ItemDescription") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Uom}">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("UomCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.PlannedBill.PlannedQty}">
                    <ItemTemplate>
                        <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.PlannedBill.CurrentActingQty}">
                    <ItemTemplate>
                        <asp:Label ID="lblCurrentQty" runat="server" Text='<%# Bind("CurrentQty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,ButtonDelete%>" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDeleteHu" runat="server" Text="<%$Resources:Language,ButtonDelete%>"
                            OnClick="lbtnDelete_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </fieldset>
</div>
