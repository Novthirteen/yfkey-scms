<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CodeMstr.ascx.cs" Inherits="MasterData_GeneralCode_CodeMstr" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01" style="width: 250px;">
                ${MasterData.GeneralCode.Type}:
            </td>
            <td class="td02" style="width: 250px;">
                <asp:DropDownList ID="ddlCode" runat="server" DataTextField="Code" DataValueField="Code"
                    AppendDataBoundItems="True">
                    <asp:ListItem Value="">${MasterData.GeneralCode.Choice}</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="td02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                        CssClass="query" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
