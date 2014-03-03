<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_Item_ItemKit_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="floatdiv">
    <fieldset>
        <legend>${MasterData.Item.NewItemKit}</legend>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Item.Code}:" />
                </td>
                <td class="td02">
                    <uc3:textbox ID="tbCode" runat="server" Visible="true" DescField="Description" ValueField="Code"
                        CssClass="inputRequired" MustMatch="true" ServicePath="ItemMgr.service" ServiceMethod="GetPMItem"
                        Width="250" />
                    <asp:RequiredFieldValidator ID="rtvCode" runat="server" ErrorMessage="${MasterData.Item.Code.Empty}"
                        Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                </td>
                <td class="td01">
                </td>
                <td class="td02">
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="ltlQty" runat="server" Text="${MasterData.ItemKit.Qty}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbQty" runat="server" />
                    <asp:RegularExpressionValidator ID="revQty" ControlToValidate="tbQty" runat="server"
                        ValidationGroup="vgSave" ErrorMessage="${MasterData.Item.UC.Format}" ValidationExpression="^[0-9]+(.[0-9]{1,8})?$"
                        Display="Dynamic" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblIsActive" runat="server" Text="${MasterData.Item.IsActive}:" />
                </td>
                <td class="td02">
                    <asp:CheckBox ID="cbIsActive" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                </td>
                <td class="td02">
                </td>
                <td class="td01">
                </td>
                <td class="td02">
                    <div class="buttons">
                        <asp:Button ID="btnInsert" runat="server" Text="${Common.Button.Save}" OnClick="btnSave_Click"
                            ValidationGroup="vgSave" CssClass="apply" />
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="back" />
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
