<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="MasterData_User_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblCode" runat="server" Text="${Security.User.Code}:" />
            </td>
            <td class="ttd02">
                <asp:TextBox ID="tbCode" runat="server" Visible="true" />
            </td>
            <td class="ttd01">
                <asp:Literal ID="lblFirstName" runat="server" Text="${Security.User.FirstName}:" />
            </td>
            <td class="ttd02">
                <asp:TextBox ID="tbFirstName" runat="server" Visible="true" />
            </td>
            <td class="ttd01">
                <asp:Literal ID="lblLastName" runat="server" Text="${Security.User.LastName}:" />
            </td>
            <td class="ttd02">
                <asp:TextBox ID="tbLastName" runat="server" Visible="true" />
            </td>
        </tr>
        <tr>
            <td colspan="5" />
            <td class="ttd02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click" CssClass="button2" />
                <asp:Button ID="btnNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click" CssClass="button2" />
            </td>
        </tr>
    </table>
</fieldset>
