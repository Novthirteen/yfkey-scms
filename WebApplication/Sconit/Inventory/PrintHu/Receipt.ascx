<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Receipt.ascx.cs" Inherits="Inventory_PrintHu_Receipt" %>
<%@ Register Src="ReceiptList.ascx" TagName="List" TagPrefix="uc2" %>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblReceipt" runat="server" Text="${Inventory.PrintHu.Receipt}:" />
            </td>
            <td class="td02">
                <asp:textbox ID="tbReceipt" runat="server" CssClass="inputRequired" />
                <asp:RequiredFieldValidator ID="rfvReceipt" runat="server" ErrorMessage="${Inventory.PrintHu.Receipt.Required}"
                    Display="Dynamic" ControlToValidate="tbReceipt" ValidationGroup="vgPrint" />
            </td>
            <td class="td01">
            </td>
            <td class="td02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="tbReceipt_TextChanged"
                    CssClass="button2" ValidationGroup="vgPrint" />
            </td>
        </tr>
    </table>
</fieldset>

<uc2:List ID="ucList" runat="server" Visible="false" />