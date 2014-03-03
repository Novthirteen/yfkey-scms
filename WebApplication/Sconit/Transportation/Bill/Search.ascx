﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Transportation_Bill_Search" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblCode" runat="server" Text="${Transportation.TransportationBill.BillNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbCode" runat="server" Visible="true" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlStatus" runat="server" Text="${Transportation.TransportationBill.Status}:" />
            </td>
            <td class="td02">
                <asp:DropDownList ID="ddlStatus" runat="server" DataTextField="Description" DataValueField="Value" />
            </td>
        </tr>
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblParty" runat="server" Text="${Transportation.TransportationBill.Party}:" />
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbParty" runat="server" Width="250" DescField="Name" ValueField="Code"
                    MustMatch="true" ServiceMethod="GetTransportationParty" ServicePath="PartyMgr.service"/>
            </td>
            <td class="td01">
                <asp:Literal ID="ltlExternalBillNo" runat="server" Text="${Transportation.TransportationBill.ExternalBillNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbExternalBillNo" runat="server" Visible="true" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlStartDate" runat="server" Text="${Common.Business.StartDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlEndDate" runat="server" Text="${Common.Business.EndDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEndDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="ttd02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                    Width="59px" CssClass="button2" />
                <cc1:Button ID="btnNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click"
                    Width="59px" CssClass="button2"   FunctionId="EditBill"/>
            </td>
        </tr>
    </table>
</fieldset>
