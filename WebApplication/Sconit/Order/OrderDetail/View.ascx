<%@ Control Language="C#" AutoEventWireup="true" CodeFile="View.ascx.cs" Inherits="Order_OrderDetail_View" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<div id="floatdiv">
    <fieldset>
        <legend>${MasterData.Order.OrderDetail}</legend>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblSeq" runat="server" Text="${MasterData.Order.OrderDetail.Sequence}:" />
                </td>
                <td class="td02">
                    <sc1:ReadonlyTextBox ID="tbSeq" runat="server" CodeField="OrderNo" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblItemCode" runat="server" Text="${MasterData.Order.OrderDetail.Item.Code}:" />
                </td>
                <td class="td02">
                    <sc1:ReadonlyTextBox ID="tbItemCode" runat="server" CodeField="Item.Code" DescField="Item.Description" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblUom" runat="server" Text="${MasterData.Order.OrderDetail.Uom}:" />
                </td>
                <td class="td02">
                    <sc1:ReadonlyTextBox ID="tbUom" runat="server" CodeField="Uom.Code" DescField="Uom.Description" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblUC" runat="server" Text="${MasterData.Order.OrderDetail.UnitCount}:" />
                </td>
                <td class="td02">
                    <sc1:ReadonlyTextBox ID="tbUC" runat="server" CodeField="UnitCount" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblGoodsReceiptLotSize" runat="server" Text="${MasterData.Order.OrderDetail.UnitCount.GoodsReceiptLotSize}:" />
                </td>
                <td class="td02">
                    <sc1:ReadonlyTextBox ID="tbGoodsReceiptLotSize" runat="server" CodeField="GoodsReceiptLotSize" />
                </td>
                <td class="ttd01">
                </td>
                <td class="ttd02">
                </td>
            </tr>
            <tr id="trBom" runat="server">
                <td class="td01">
                    <asp:Literal ID="lblBom" runat="server" Text="${MasterData.Order.OrderDetail.Bom}:" />
                </td>
                <td class="td02">
                    <sc1:ReadonlyTextBox ID="tbBom" runat="server" CodeField="Bom.Code" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblLocFrom" runat="server" Text="${MasterData.Order.OrderDetail.LocationFrom}:" />
                </td>
                <td class="td02">
                    <sc1:ReadonlyTextBox ID="tbLocFrom" runat="server" CodeField="LocationFrom.Code"
                        DescField="LocationFrom.Name" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblLocTo" runat="server" Text="${MasterData.Order.OrderDetail.LocationTo}:" />
                </td>
                <td class="td02">
                    <sc1:ReadonlyTextBox ID="tbLocTo" runat="server" CodeField="LocationTo.Code" DescField="LocationTo.Name" />
                </td>
            </tr>
            <tr id="trBill" runat="server">
                <td class="td01">
                    <asp:Literal ID="lblBillFrom" runat="server" Text="${MasterData.Order.OrderDetail.BillFrom}:" />
                </td>
                <td class="td02">
                    <sc1:ReadonlyTextBox ID="tbBillFrom" runat="server" CodeField="BillFrom.Code" DescField="BillFrom.Address" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblBillTo" runat="server" Text="${MasterData.Order.OrderDetail.BillTo}:" />
                </td>
                <td class="td02">
                    <sc1:ReadonlyTextBox ID="tbBillTo" runat="server" CodeField="BillTo.Code" DescField="BillTo.Address" />
                </td>
            </tr>
            <tr id="trPriceList" runat="server">
                <td class="td01">
                    <asp:Literal ID="lblPriceListFrom" runat="server" Text="${MasterData.Order.OrderDetail.PriceListFrom}:" />
                </td>
                <td class="td02">
                    <sc1:ReadonlyTextBox ID="tbPriceListFrom" runat="server" CodeField="PriceListFrom.Code" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblPriceListTo" runat="server" Text="${MasterData.Order.OrderHead.PriceListTo}:" />
                </td>
                <td class="td02">
                    <sc1:ReadonlyTextBox ID="tbPriceListTo" runat="server" CodeField="PriceListTo.Code" />
                </td>
            </tr>
        </table>
    </fieldset>
    <div class="tablefooter">
        <asp:Button ID="btnCancel" runat="server" Text="${Common.Button.Cancel}" OnClick="btnCancel_Click"
            CssClass="button2" />
    </div>
</div>
