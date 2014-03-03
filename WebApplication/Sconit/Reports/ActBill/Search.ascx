<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Reports_ActBill_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ac1" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblOrderNo" runat="server" Text="${Reports.ActBill.OrderNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbOrderNo" runat="server" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblReceiptNo" runat="server" Text="${Reports.ActBill.ReceiptNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbReceiptNo" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblParty" runat="server" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbParty" runat="server" Visible="true" Width="250" DescField="Name"
                    ValueField="Code"  />
            </td>   
            <td class="td01">
                <asp:Literal ID="lblItem" runat="server" Text="${Reports.ActBill.ItemCode}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItem" runat="server" Visible="true" DescField="Description" ImageUrlField="ImageUrl"
                    Width="280" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblEffectiveDateFrom" runat="server" Text="${Reports.ActBill.EffectiveDateFrom}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEffectiveDateFrom" runat="server" />
                <ac1:CalendarExtender ID="CalendarExtender2" TargetControlID="tbEffectiveDateFrom" Format="yyyy-MM-dd"
                    runat="server">
                </ac1:CalendarExtender>
            </td>
            <td class="td01">
                <asp:Literal ID="lblEffectiveDateTo" runat="server" Text="${Reports.ActBill.EffectiveDateTo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEffectiveDateTo" runat="server" />
                <ac1:CalendarExtender ID="CalendarExtender1" TargetControlID="tbEffectiveDateTo" Format="yyyy-MM-dd"
                    runat="server">
                </ac1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="t02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" CssClass="button2"
                    OnClick="btnSearch_Click" />
                <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" CssClass="button2"
                    OnClick="btnExport_Click" />
            </td>
        </tr>
    </table>
</fieldset>