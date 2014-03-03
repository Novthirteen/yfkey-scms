<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AjaxViewMain.ascx.cs" Inherits="Warehouse_InProcessLocation_AjaxViewMain" %>
<%@ Register Src="~/Warehouse/InProcessLocationDetail/List.ascx" TagName="DetailList"
    TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc2" %>

<div id="floatdiv">
    <div id='floatdivtitle'>
        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" CssClass="btnClose" />
    </div>
    <fieldset>
        <legend>${MasterData.Order.OrderHead}</legend>
        <uc2:Edit ID="ucEdit" runat="server" Visible="true" />
        <div class="tablefooter">
            <asp:Button ID="btnClose" runat="server" Text="${Common.Button.Close}" Visible="false"
                CssClass="button2" OnClick="btnClose_Click" ValidationGroup="vgClose" />
            <asp:Button ID="btnUpdate" runat="server" Text="${Common.Button.Save}" Visible="false"
                CssClass="button2" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnPrint" runat="server" Text="${Common.Button.Print}" OnClick="btnPrint_Click"
                CssClass="button2" />
        </div>
    </fieldset>
    <uc2:DetailList ID="ucDetailList" runat="server" Visible="true" />
</div>
<div id='divHide' />
