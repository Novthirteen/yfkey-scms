<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Flow.ascx.cs" Inherits="Inventory_PrintHu_Flow" %>
<%@ Register Src="FlowDetailList.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblFlow" runat="server" Text="${Inventory.PrintHu.Flow}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbFlow" runat="server" Visible="true" DescField="Description" ValueField="Code"
                    ServicePath="FlowMgr.service" MustMatch="true" Width="250" CssClass="inputRequired"
                    ServiceMethod="GetFlowList" />
                <asp:RequiredFieldValidator ID="rfvFlow" runat="server" ErrorMessage="${Inventory.PrintHu.Flow.Required}"
                    Display="Dynamic" ControlToValidate="tbFlow" ValidationGroup="vgPrint" />
                <asp:RadioButtonList runat="server" ID="rblPackageType" RepeatDirection="Horizontal">
                    <asp:ListItem runat="server" Text="${Inventory.PrintHu.Inner.Package}" Value="0" />
                    <asp:ListItem runat="server" Text="${Inventory.PrintHu.Outer.Package}" Value="1"
                        Selected="True" />
                </asp:RadioButtonList>
            </td>
            <td class="td01">
            </td>
            <td class="td02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="tbFlow_TextChanged"
                    CssClass="button2" />
            </td>
        </tr>
    </table>
</fieldset>
<uc2:List ID="ucList" runat="server" Visible="false" />
