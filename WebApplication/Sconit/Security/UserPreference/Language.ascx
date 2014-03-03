<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Language.ascx.cs" Inherits="Security_Language" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlLanguage" runat="server" Text="请选择语言:"/>                
            </td>
            <td class="td01">
                <cc1:CodeMstrDropDownList ID="ddlLanguage" Code="Language" runat="server" />
            </td>
            <td class="td01">
                <asp:Button ID="btSave" runat="server" Text="${Common.Button.Save}" onclick="btSave_Click" />
            </td>
             <td class="td01">
            </td>
        </tr>
    </table>
</fieldset>
