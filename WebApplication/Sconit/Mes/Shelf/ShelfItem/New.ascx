<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Mes_Shelf_ShelfItem_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div id="floatdiv">
    <fieldset>
        <legend>${Mes.ShelfItem.AddShelfItem}</legend>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Label ID="lblShelfCode" runat="server" Text="${Mes.Shelf.Code}:" />
                </td>
                <td class="td02">
                    <asp:Label ID="tbShelfCode" runat="server" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblItem" runat="server" Text="${MasterData.Item.Code}:" />
                </td>
                <td class="td02">
                    <uc3:textbox ID="tbItem" runat="server" DescField="Description" ValueField="Code"
                        Width="250" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" MustMatch="true"
                        CssClass="inputRequired" />
                    <asp:RequiredFieldValidator ID="rfvItem" runat="server" ErrorMessage="${MasterData.Item.Code.Empty}"
                        Display="Dynamic" ControlToValidate="tbItem" ValidationGroup="vgSave" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                </td>
                <td>
                    <div class="buttons">
                        <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="${Common.Button.Save}"
                            CssClass="apply" ValidationGroup="vgSave" OnClick="btnCreate_Click" />
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                            CssClass="back" />
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
