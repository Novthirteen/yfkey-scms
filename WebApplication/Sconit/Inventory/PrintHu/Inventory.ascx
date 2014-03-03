<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Inventory.ascx.cs" Inherits="Inventory_PrintHu_Inventory" %>
<%@ Register Src="InventoryList.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlLocation" runat="server" Text="${Inventory.PrintHu.LocationLotDetail.Location}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbLocation" runat="server" DescField="Name" ValueField="Code" ServicePath="LocationMgr.service"
                    ServiceMethod="GetLocationByUserCode" Width="250" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlItem" runat="server" Text="${Inventory.PrintHu.LocationLotDetail.Item}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItem" runat="server" Visible="true" Width="250" DescField="Description"
                    ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlArea" runat="server" Text="${Inventory.PrintHu.LocationLotDetail.Area}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbArea" runat="server" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlBin" runat="server" Text="${Inventory.PrintHu.LocationLotDetail.Bin}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbBin" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlHuId" runat="server" Text="${Inventory.PrintHu.LocationLotDetail.HuId}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbHuId" runat="server" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlLotNo" runat="server" Text="${Inventory.PrintHu.LocationLotDetail.LotNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbLotNo" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlCreateDate" runat="server" Text="${Common.Business.CreateDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbCreateDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />                 
            </td>
            <td class="td01">
           
            </td>
            <td class="td02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                    Width="59px" CssClass="button2" ValidationGroup="vgSearch" />
                <asp:Button ID="btnPrint" runat="server" Text="${Common.Button.Print}" OnClick="btnPrint_Click"
                    Width="59px" CssClass="button2" />
            </td>
        </tr>
      
    </table>
</fieldset>
<uc2:List ID="ucList" runat="server" Visible="false" />
