<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ActBillMain.ascx.cs" Inherits="Order_OrderView_ActBillMain" %>
<%@ Register Src="ActBillList.ascx" TagName="ActBillList" TagPrefix="uc2" %>
<uc2:ActBillList ID="ucActPOBillList" runat="server" Visible="false" />
<uc2:ActBillList ID="ucActSOBillList" runat="server" Visible="false" />
<div class="tablefooter">
    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
        CssClass="button2" />
</div>
