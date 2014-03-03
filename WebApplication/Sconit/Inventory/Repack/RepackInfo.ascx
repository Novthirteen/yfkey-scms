<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RepackInfo.ascx.cs" Inherits="Inventory_Repack_RepackInfo" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>

<fieldset>
    <legend>${MasterData.Inventory.Repack}</legend>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblRepackNo" runat="server" Text="${MasterData.Inventory.Repack.RepackNo.Repack}:" />
            </td>
            <td class="td02">
                <asp:Label ID="lbRepackNo" runat="server" />
            </td>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblCreateUser" runat="server" Text="${MasterData.Inventory.Repack.CreateUser}:" />
            </td>
            <td class="td02">
                <asp:Label ID="lbCreateUser" runat="server" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblCreateDate" runat="server" Text="${MasterData.Inventory.Repack.CreateDate}:" />
            </td>
            <td class="td02">
                <asp:Label ID="lbCreateDate" runat="server" />
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
                <asp:Button ID="btnPrint" runat="server" Text="${Common.Button.Print}" OnClick="btnPrint_Click"
                    CssClass="button2" />
                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                    CssClass="button2" />
            </td>
        </tr>
    </table>
</fieldset>
