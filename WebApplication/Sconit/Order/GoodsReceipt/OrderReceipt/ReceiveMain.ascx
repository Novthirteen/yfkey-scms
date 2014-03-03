<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReceiveMain.ascx.cs" Inherits="Order_GoodsReceipt_OrderReceipt_ReceiveMain" %>
<%@ Register Src="ProductionReceiptItemList.ascx" TagName="DetailList" TagPrefix="uc2" %>
<%@ Register Src="InLocTransList.ascx" TagName="inLocTransList" TagPrefix="InLocTrans" %>
<%@ Register Src="NewItemInLocTransList.ascx" TagName="newItemInLocTransList" TagPrefix="NewItemInLocTrans" %>
<%@ Register Src="OutLocTransList.ascx" TagName="outLocTransList" TagPrefix="OutLocTrans" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="ConfirmInfo.ascx" TagName="ConfirmInfo" TagPrefix="uc2" %>

<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
<div class="ajax__tab_body">
    <uc2:DetailList ID="ucDetailList" runat="server" Visible="true" />
    <InLocTrans:inLocTransList ID="ucInLocTransList" runat="server" Visible="false" />
    <NewItemInLocTrans:newItemInLocTransList ID="ucNewItemInLocTransList" runat="server" Visible="false" />
    <OutLocTrans:outLocTransList ID="ucOutLocTransList" runat="server" Visible="false" />
    <div class="tablefooter buttons">
        <asp:Button ID="btnReceive" runat="server" Text="${MasterData.Order.Button.Receive}"
            CssClass="apply" OnClick="btnReceive_Click" />
        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="back"/>
    </div>
    <uc2:ConfirmInfo ID="ucConfirmInfo" runat="server" Visible="false" />
</div>
</div> 