<%@ Control Language="C#" AutoEventWireup="true" CodeFile="View.ascx.cs" Inherits="Order_OrderRouting_View" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc2" %>
<fieldset>
    <legend id="lTitle" runat="server"></legend>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblRoutingCode" runat="server" Text="${MasterData.Order.OrderHead.Routing.Code}:" />
            </td>
            <td class="td02">
                <asp:Literal ID="lblRoutingCodeValue" runat="server" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblDescription" runat="server" Text="${MasterData.Order.OrderHead.Routing.Description}:" />
            </td>
            <td class="td02">
                <asp:Literal ID="lblDescriptionValue" runat="server" />
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
                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                    CssClass="button2" />
            </td>
        </tr>
    </table>
</fieldset>
<uc2:List ID="ucList" runat="server" Visible="false" />
