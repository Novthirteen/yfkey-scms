<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="Order_GoodsReceipt_OrderReceipt_TabNavigator" %>
<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
        <span class='ajax__tab_active' id='tab_detail' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbDetail" Text="${MasterData.Order.GoodsReceipt.OrderReceipt.FinishedGoods}" runat="server" OnClick="lbDetail_Click" /></span></span></span></span><span id='tab_inloctrans' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbInLocTrans" Text="${MasterData.Order.GoodsReceipt.OrderReceipt.InLocTrans}" runat="server" OnClick="lbInLocTrans_Click" /></span></span></span></span><span id='tab_newiteminloctrans' runat="server" Visible="false"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbNewItemInLocTrans" Text="${MasterData.Order.GoodsReceipt.OrderReceipt.NewItemInLocTrans}" runat="server" OnClick="lbNewItemInLocTrans_Click" /></span></span></span></span><span id='tab_outloctrans' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbOutLocTrans" Text="${MasterData.Order.GoodsReceipt.OrderReceipt.OutLocTrans}" runat="server" OnClick="lbOutLocTrans_Click" /></span></span></span></span>
    </div>
