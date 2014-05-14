<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Finance_Bill_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblBillNo" runat="server" Text="${MasterData.Bill.BillNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbBillNo" runat="server" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblPartyFrom" runat="server" Text="${MasterData.Order.OrderHead.PartyFrom.Supplier}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbPartyFrom" runat="server" Visible="true" Width="250" DescField="Name"
                    ValueField="Code" ServicePath="PartyMgr.service" ServiceMethod="GetOrderFromParty" />
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
                <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" CssClass="button2"
                    OnClick="btnSearch_Click" />
                <cc1:Button ID="btnImportKPOrder" runat="server" OnClick="btnImportKPOrder_Click"
                    Text="导入开票数据" FunctionId="ImportKPOrder" />
            </td>
        </tr>
    </table>
</fieldset>
<div id="floatdiv">
    <fieldset runat="server" id="fs01" visible="false">
        <legend>导入开票数据</legend>
        <table style="width: 100%" class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="ltlKpDate" runat="server" Text="开票日期" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbImportStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                </td>
                <td class="td01">
                </td>
                <td class="td02">
                    <asp:Button ID="butImport" runat="server" Text="导入"  Width="59px" OnClick="Button9_Click" CssClass="button2" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        CssClass="button2" />
                </td>
            </tr>
        </table>
    </fieldset>
</div>
