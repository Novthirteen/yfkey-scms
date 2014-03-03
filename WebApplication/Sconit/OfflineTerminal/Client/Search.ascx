<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="MasterData_Client_Search" %>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblClientId" runat="server" Text="${Common.Business.Code}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbClientId" runat="server"></asp:TextBox>
            </td>
            <td class="td01">
                <asp:Literal ID="ltlDescription" runat="server" Text="${Common.Business.Description}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbDescription" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td01">
             <asp:Literal ID="lblIsActive" runat="server" Text="${Common.Business.IsActive}:" />
            </td>
            <td class="td02">
                <asp:CheckBox ID="cbIsActive" runat="server" Checked="true" />
            </td>
            <td class="td01">
                
            </td>
            <td class="td02">
            </td>
        </tr>
        <tr><td colspan="3"></td><td>
            <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click" CssClass="button2"/>
            <asp:Button ID="btnNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click" CssClass="button2" /></td></tr>
    </table>
</fieldset>
