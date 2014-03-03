<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Upload.ascx.cs" Inherits="Hu_Inbound_Upload" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblOrderNo" runat="server" Text="${Hu.Inbound.OrderNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbOrderNo" runat="server" CssClass="inputRequired"/>
            </td>
            <td class="td01">
                <asp:Literal ID="lblFileUpload" runat="server" Text="${Hu.Inbound.FileUpload}:" />
            </td>
            <td class="td02">
                <asp:FileUpload ID="fileUpload" ContentEditable="false" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="ttd02">
            <asp:Button ID="Button1" runat="server" OnClick="btnUpload_Click" Text="${Common.Button.Import}"/>
            </td>
        </tr>
    </table>
</fieldset>
