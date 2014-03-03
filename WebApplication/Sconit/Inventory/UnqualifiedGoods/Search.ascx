<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Inventory_UnqualifiedGoods_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblInspectNo" runat="server" Text="${MasterData.Inventory.InspectOrder.PrintNo}:" />
            </td>
            <td class="ttd02">
                <asp:TextBox ID="tbInspectNo" runat="server" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblItemCode" runat="server" Text="${MasterData.Flow.FlowDetail.ItemCode}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItemCode" runat="server" Visible="true" Width="250" MustMatch="true"
                    DescField="Description" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblStartDate" runat="server" Text="${MasterData.Common.CreateDateFrom}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblEndDate" runat="server" Text="${MasterData.Common.CreateDateTo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEndDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblCreateUser" runat="server" Text="${MasterData.Common.DispositionUser}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbCreateUser" runat="server" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblLocFrom" runat="server" Text="${MasterData.Order.OrderHead.LocFrom}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbLocation" runat="server" DescField="Name" ValueField="Code" ServicePath="LocationMgr.service"
                    ServiceMethod="GetLocationByUserCode" Width="250"  />
        </tr>
        <tr>
            </td>
            <td class="td01">
                <asp:Literal ID="lblDisposition" runat="server" Text="${MasterData.Common.Disposition}:" />
            </td>
            <td class="td02">
                <cc1:CodeMstrDropDownList ID="ddlDisposition" runat="server" Code="Disposition" IncludeBlankOption="true" />
            </td>
            <td class="td01">
            </td>
            <td class="td02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                    CssClass="button2" />
            </td>
        </tr>
    </table>
</fieldset>
