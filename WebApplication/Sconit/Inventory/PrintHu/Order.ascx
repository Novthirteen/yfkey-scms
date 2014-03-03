<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Order.ascx.cs" Inherits="Inventory_PrintHu_Order" %>
<%@ Register Src="OrderDetailList.ascx" TagName="List" TagPrefix="uc2" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblOrderNo" runat="server" Text="${Inventory.PrintHu.OrderNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbOrderNo" runat="server" CssClass="inputRequired" />
                <asp:RadioButtonList runat="server" ID="rblPackageType" RepeatDirection="Horizontal">
                    <asp:ListItem runat="server" Text="${Inventory.PrintHu.Inner.Package}" Value="0" />
                    <asp:ListItem runat="server" Text="${Inventory.PrintHu.Outer.Package}" Value="1"
                        Selected="True" />
                </asp:RadioButtonList>
            </td>
            <td class="td02">
                <asp:RequiredFieldValidator ID="rfvOrderNo" runat="server" ErrorMessage="${Inventory.PrintHu.OrderNo.Required}"
                    Display="Dynamic" ControlToValidate="tbOrderNo" ValidationGroup="vgPrint" />
            </td>
            <td class="td02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="tbOrderNo_TextChanged"
                    CssClass="button2" ValidationGroup="vgPrint"/>
            </td>
        </tr>
    </table>
</fieldset>
<uc2:List ID="ucList" runat="server" Visible="false" />
