<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Transportation_TransportationAddress_Search" %>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblAddress" runat="server" Text="${Transportation.TransportationAddress.Address}:" />
            </td>
            <td class="ttd02">
                <asp:TextBox ID="tbAddress" runat="server" Visible="true" />
            </td>
            <td class="ttd01"> 
            </td>
            <td class="ttd02">
                <asp:TextBox ID="TextBox1" runat="server" Visible="true" style="display:none;"/>
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="ttd02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                        CssClass="query" />
                    <asp:Button ID="btnNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click"
                        CssClass="back" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
