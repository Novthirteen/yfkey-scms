<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="ManageSconit_LeanEngine_Edit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Label ID="lbLastRunTime" runat="server" Text="${LeanEngine.LastRunTime}:"></asp:Label>
            </td>
            <td class="td02">
                <sc1:ReadonlyTextBox ID="tbPrevFireTime" runat="server" />
            </td>
            <td class="td01">
                <asp:Label ID="lbNextRunTime" runat="server" Text="${LeanEngine.NextRunTime}:"></asp:Label>
            </td>
            <td class="td02">
                <sc1:ReadonlyTextBox ID="tbNextFireTime" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Label ID="lbRunInterval" runat="server" Text="${MasterData.Jobs.Trigger.Interval}: "></asp:Label>
            </td>
            <td class="td02">
                <asp:TextBox ID="tbInterval" runat="server" Text="10" ReadOnly="true"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="tbInterval"
                    FilterType="Numbers">
                </cc1:FilteredTextBoxExtender>
            </td>
            <td class="td01">
                <asp:Label ID="ltlIntervalType" runat="server" Text="${MasterData.Jobs.Trigger.IntervalType}: "></asp:Label>
            </td>
            <td class="td02">
                <sc1:CodeMstrLabel ID="lblIntervalType" runat="server" Code="DateTimeType" />
            </td>
        </tr>
    </table>
</fieldset>
