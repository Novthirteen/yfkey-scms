<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RepackDetailInfo.ascx.cs"
    Inherits="Inventory_Repack_RepackDetailInfo" %>

<script language="javascript" type="text/javascript">
    function QtyChanged(obj) {
        var oldQty = $('#<%= tbQty.ClientID %>').attr('oldValue');
        var oldUnitCount = $('#<%= tbUnitCount.ClientID %>').attr('oldValue');
        var newQty = $(obj).val();
        if (!isNaN(oldQty) && !isNaN(oldUnitCount) && !isNaN(newQty)) {
            var newUnitCount = oldQty * oldUnitCount / newQty;
            $('#<%= tbUnitCount.ClientID %>').attr('value', newUnitCount);
            $('#<%= tbUnitCount.ClientID %>').attr('oldValue', newUnitCount);
            $('#<%= tbQty.ClientID %>').attr('oldValue', newQty);
        }
    }


    function UnitCountChanged(obj) {
        var oldQty = $('#<%= tbQty.ClientID %>').attr('oldValue');
        var oldUnitCount = $('#<%= tbUnitCount.ClientID %>').attr('oldValue');
        var newUnitCount = $(obj).val();
        if (!isNaN(oldQty) && !isNaN(oldUnitCount) && !isNaN(newUnitCount)) {
            var newQty = oldQty * oldUnitCount / newUnitCount;
            $('#<%= tbQty.ClientID %>').attr('value', newQty);
            $('#<%= tbQty.ClientID %>').attr('oldValue', newQty);
            $('#<%= tbUnitCount.ClientID %>').attr('oldValue', newUnitCount);
        }
    }

</script>

<div id="floatdiv">
    <fieldset>
        <legend>${MasterData.Inventory.RepackDetail}</legend>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblUnitCount" runat="server" Text="${MasterData.Inventory.Repack.UnitCount}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbUnitCount" runat="server" />
                    <asp:RangeValidator ID="rvUnitCount" runat="server" ControlToValidate="tbUnitCount"
                        ErrorMessage="${Common.Validator.Valid.Number}" Display="Dynamic" Type="Double"
                        MinimumValue="1" MaximumValue="100000" ValidationGroup="vgConfirmGroup" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblQty" runat="server" Text="${MasterData.Inventory.Repack.Qty}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbQty" runat="server" />
                    <asp:RangeValidator ID="rvQty" runat="server" ControlToValidate="tbQty" ErrorMessage="${Common.Validator.Valid.Number}"
                        Display="Dynamic"  Type="Double" MinimumValue="1" MaximumValue="100000" ValidationGroup="vgConfirmGroup" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                </td>
                <td class="td02">
                </td>
                <td class="td01">
                </td>
                <td class="td02">
                    <asp:Button ID="btnConfirm" runat="server" Text="${Common.Button.Confirm}" OnClick="btnConfirm_Click"
                        CssClass="button2" ValidationGroup="vgConfirmGroup" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        CssClass="button2" />
                </td>
            </tr>
        </table>
    </fieldset>
</div>
