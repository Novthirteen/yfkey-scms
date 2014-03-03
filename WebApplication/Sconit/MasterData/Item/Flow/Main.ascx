<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Item_FLowItem_Main" %>
<%@ Register Src="FlowDetailList.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblItem" runat="server" Text="${MasterData.Item.FlowItem}:" />
            </td>
            <td class="td02">
                <uc3:textbox id="tbItemCode" runat="server" visible="true" width="250" mustmatch="true"    OnTextChanged="tbItem_TextChanged"
                    descfield="Description" valuefield="Code" servicepath="ItemMgr.service" servicemethod="GetCacheAllItem" AutoPostBack="true" />
            </td>
        </tr>
    </table>
</fieldset>
<uc2:list id="ucList" runat="server" visible="false" />
