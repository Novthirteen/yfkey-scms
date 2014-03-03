<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Visualization_GoodsTraceability_HuSearch_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ac1" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblItem" runat="server" Text="${Common.Business.ItemCode}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItem" runat="server" Visible="true" DescField="Description" ImageUrlField="ImageUrl"
                    Width="280" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlHuId" runat="server" Text="${Common.Business.HuId}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbHuId" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlLotNo" runat="server" Text="库位" />
                :</td> 
            <td class="td02">
               <uc3:textbox ID="tbLotNo" runat="server" Visible="true" DescField="Name" Width="280"
                    ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocationByUserCode" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlOrderNo" runat="server" Text="${Common.Business.OrderNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbOrderNo" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlStartDate" runat="server" Text="${Visualization.GoodsTraceability.ManufactureDateFrom}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlEndDate" runat="server" Text="${Visualization.GoodsTraceability.ManufactureDateTo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEndDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="t02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" CssClass="query"
                        OnClick="btnSearch_Click" />
                        <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" CssClass="query"
                        OnClick="btnExport_Click" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
