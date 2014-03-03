<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdjustMain.ascx.cs" Inherits="Order_GoodsReceipt_AdjustReceipt_AdjustMain" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc2" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc2" %>

<div id="floatdiv">
    <fieldset>
        <legend>${MasterData.Order.OrderHead}</legend>
        <uc2:Edit ID="ucEdit" runat="server" Visible="true" />
        <div class="tablefooter">
            <asp:Button ID="btnConfirm" runat="server" Text="${Common.Button.Confirm}" OnClick="btnConfirm_Click"
                CssClass="button2" />
            <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" CssClass="button2"
                OnClick="btnBack_Click" />
        </div>
    </fieldset>
    <uc2:List ID="ucList" runat="server" Visible="true" />
</div>
