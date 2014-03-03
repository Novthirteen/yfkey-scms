<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LocTransMain.ascx.cs"
    Inherits="Order_OrderView_LocTransMain" %>
<%@ Register Src="LocTransList.ascx" TagName="LocTransList" TagPrefix="uc2" %>
<%@ Register Src="AbstractItemBomDetail.ascx" TagName="AbstractItemBomDetail" TagPrefix="uc2" %>
<%@ Register Src="Import.ascx" TagName="Import" TagPrefix="uc2" %>
<fieldset>
    <legend>${MasterData.Order.LocTrans.In}</legend>
    <uc2:LocTransList ID="ucLocInTransList" runat="server" Visible="false" />
</fieldset>
<fieldset>
    <legend>${MasterData.Order.LocTrans.Out}</legend>
    <uc2:LocTransList ID="ucLocOutTransList" runat="server" Visible="false" />
    <uc2:AbstractItemBomDetail ID="ucAbstractItemBomDetail" runat="server" Visible="false" />
     <uc2:Import ID="ucImport" runat="server" Visible="false" />
</fieldset>
<div class="tablefooter">
    <asp:Button ID="btnSave" runat="server" Text="${Common.Button.Save}" OnClick="btnSave_Click"
        CssClass="button2" Visible="false" />
    <asp:Button ID="btnImport" runat="server" Text="${Common.Button.Import}" OnClick="btnImport_Click"
        CssClass="button2" Visible="false" />
    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
        CssClass="button2" />
</div>
