<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Visualization_OrderEfficiency_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblDate" runat="server" Text="${MasterData.Common.Date}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
          <td class="td01">
                <asp:Literal ID="lblOrderViewType" runat="server" Text="${MasterData.OrderEfficiency.OrderViewType}:" />
            </td>
            <td class="td02">
                <cc1:CodeMstrDropDownList ID="ddlOrderViewType" Code="OrderViewType" runat="server" IncludeBlankOption="true" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblUser" runat="server" Text="${MasterData.OrderEfficiency.StartUser}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbUserCode" runat="server"  />
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
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                    CssClass="button2" />
                <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" CssClass="button2"
                    OnClick="btnExport_Click" />
            </td>
        </tr>
    </table>
</fieldset>
