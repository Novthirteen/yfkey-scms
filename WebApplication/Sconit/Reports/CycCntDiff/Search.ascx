<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Reports_CycCntDiff_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblRegion" runat="server" Text="${Common.Business.Region}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbRegion" runat="server" Visible="true" DescField="Name" Width="280"
                    ValueField="Code" ServicePath="RegionMgr.service" ServiceMethod="GetRegion" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblLocation" runat="server" Text="${Common.Business.Location}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbLocation" runat="server" Visible="true" DescField="Name" Width="280"
                    ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocation"
                    ServiceParameter="string:#tbRegion" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblStartDate" runat="server" Text="${Common.Business.StartDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblEndDate" runat="server" Text="${Common.Business.EndDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEndDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlStorageBin" runat="server" Text="${MasterData.Location.Bin}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbStorageBin" runat="server" Visible="true" DescField="Description"
                    Width="280" ValueField="Code" ServicePath="StorageBinMgr.service" ServiceMethod="GetAllStorageBin" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblItem" runat="server" Text="${Common.Business.ItemCode}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItem" runat="server" Visible="true" DescField="Description" ImageUrlField="ImageUrl"
                    Width="280" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblOrderNo" runat="server" Text="${Common.Business.OrderNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbOrderNo" runat="server" />
            </td>
            <td colspan="2" />
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlClassifiedParam" runat="server" Text="${Reports.SummarizeParameters}:" />
            </td>
            <td class="td02">
                <asp:CheckBox ID="cbClassifiedOrderNo" runat="server" Text="${Common.Business.OrderNo}" />
                <asp:CheckBox ID="cbClassifiedLocation" runat="server" Text="${Common.Business.Location}" />
                <asp:CheckBox ID="cbClassifiedBin" runat="server" Text="${Common.Business.Bin}" />
                <asp:CheckBox ID="cbClassifiedHuId" runat="server" Text="${Common.Business.HuId}" />
            </td>
            <td class="td01">
            </td>
            <td class="t02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" CssClass="query"
                        OnClick="btnSearch_Click" />
                   <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" CssClass="button2"
                    OnClick="btnExport_Click" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
