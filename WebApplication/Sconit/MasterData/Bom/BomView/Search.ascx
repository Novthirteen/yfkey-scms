<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="MasterData_Bom_BomView_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblItemCode" runat="server" Text="${Common.Business.ItemCode}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItemCode" runat="server" Visible="true" DescField="Description"
                    ImageUrlField="ImageUrl" Width="300" ValueField="Code" ServicePath="ItemMgr.service"
                    ServiceMethod="GetCacheAllItem" />
                <asp:RequiredFieldValidator ID="rfvItemCode" runat="server" ErrorMessage="${MasterData.BomDetail.WarningMessage.ItemCodeEmpty}"
                    Display="Dynamic" ControlToValidate="tbItemCode" ValidationGroup="vgSave" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblDate" runat="server" Text="${Common.Business.Date}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbDate" runat="server" />
                <cc1:CalendarExtender ID="ceDate" TargetControlID="tbDate" Format="yyyy-MM-dd HH:mm"
                    runat="server">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblViewType" runat="server" Text="${MasterData.BomView.ViewType}:" />
            </td>
            <td class="td02">
                <asp:RadioButtonList ID="rblViewType" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Text="${MasterData.BomView.ViewType.Normal}" Value="Normal" />
                    <asp:ListItem Text="${MasterData.BomView.ViewType.Cost}" Value="Cost" />
                    <asp:ListItem Text="${MasterData.BomView.ViewType.Tree}" Value="Tree" />
                </asp:RadioButtonList>
            </td>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="ttd02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                        CssClass="query" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
