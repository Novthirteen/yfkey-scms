<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="MasterData_Client_Log_Offline_Search" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblClientId" runat="server" Text="${MasterData.Client.Code}:" />
            </td>
            <td class="td02">
                <asp:DropDownList ID="ddlClientId" runat="server" DataTextField="ClientId" DataValueField="ClientId"
                    AppendDataBoundItems="True">
                    <asp:ListItem Value=""></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="td01">
                <asp:Literal ID="ltlOrderType" runat="server" Text="${MasterData.Client.OrderType}:" />
            </td>
            <td class="td02">
                <asp:DropDownList ID="ddlOrderType" runat="server" 
                    AppendDataBoundItems="True">
                    <asp:ListItem Text="${MasterData.Client.CS_WOScanOnline}" Value="CS_WOOnline" />
                    <asp:ListItem Text = "${MasterData.Client.CS_WOScanOffline}" Value="CS_WOReceipt" />
                    <asp:ListItem Text="${MasterData.Client.CS_Invtransfer}" Value="CS_Invtransfer" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
            <td class="td01">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                    CssClass="button2" />
            </td>
            <td class="td02">
            </td>
        </tr>
    </table>
</fieldset>
