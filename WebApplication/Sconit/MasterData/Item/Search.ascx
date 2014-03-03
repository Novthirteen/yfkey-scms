<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="MasterData_Item_Search" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Item.Code}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbCode" runat="server" />
                <%--            <uc3:textbox ID="Textbox1" runat="server" DescField="Description" ImageUrlField="ImageUrl"
                    Width="300" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />--%>
            </td>
            <td class="td01">
                <asp:Literal ID="ltlDescription" runat="server" Text="${MasterData.Item.Description}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbDesc" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlShowImage" runat="server" Text="${MasterData.Item.ShowImage}:" />
            </td>
            <td class="td02">
                <asp:CheckBox ID="cbShowImage" runat="server" Checked="false" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblIsActive" runat="server" Text="${MasterData.Item.IsActive}:" />
            </td>
            <td class="td02">
                <asp:CheckBox ID="cbIsActive" runat="server" Checked="true" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
            </td>
            <td class="td02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                        CssClass="query" />
                    <asp:Button ID="btnNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click"
                        CssClass="add" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
