<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AjaxViewMain.ascx.cs"
    Inherits="Order_GoodsReceipt_ViewReceipt_AjaxViewMain" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc2" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc2" %>
<div id="floatdiv">
    <div id='floatdivtitle'>
        <asp:Button ID="btnBack" runat="server" CssClass="btnClose" OnClick="btnBack_Click" />
    </div>
    <fieldset>
        <legend>${MasterData.Order.OrderHead}</legend>
        <uc2:Edit ID="ucEdit" runat="server" Visible="true" />
        <div class="tablefooter">
            <asp:Button ID="btnPrint" runat="server" Text="${Common.Button.Print}" OnClick="btnPrint_Click"
                CssClass="button2" />
        </div>
    </fieldset>
    <uc2:List ID="ucList" runat="server" Visible="true" />
    <div class="tablefooter">
    <asp:Button ID="Button1" runat="server"  class="button2" OnClick="btnBack_Click" Text="${Common.Button.Back}"  />
    </div>
</div>
<div id='divHide' />
