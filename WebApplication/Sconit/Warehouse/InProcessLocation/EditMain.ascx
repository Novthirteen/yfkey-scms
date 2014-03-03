<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditMain.ascx.cs" Inherits="Warehouse_InProcessLocation_EditMain" %>
<%@ Register Src="~/Order/GoodsReceipt/OrderReceipt/ReceiptItemList.ascx" TagName="DetailList"
    TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc2" %>
<%@ Register Src="~/Hu/List.ascx" TagName="HuList" TagPrefix="uc2" %>
<%@ Register Src="~/Order/GoodsReceipt/ViewReceipt/ViewMain.ascx" TagName="ViewMain"
    TagPrefix="uc2" %>
<div>
    <fieldset>
        <legend>${MasterData.Order.OrderHead}</legend>
        <uc2:Edit ID="ucEdit" runat="server" Visible="true" />
    </fieldset>
    <uc2:DetailList ID="ucDetailList" runat="server" Visible="true" />
    <uc2:HuList ID="ucHuList" runat="server" Visible="false" />
</div>
<uc2:ViewMain ID="ucViewMain" runat="server" Visible="false" />
