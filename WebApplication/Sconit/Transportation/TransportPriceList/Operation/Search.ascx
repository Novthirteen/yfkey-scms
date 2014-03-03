<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Transportation_TransportPriceList_Operation_Search" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <legend>
        <asp:Literal ID="lblCurrentTransportPriceList" runat="server" Text="${Transportation.TransportPriceListDetail.CurrentTransportPriceList}:" />
        <asp:Literal ID="lbCurrentTransportPriceList" runat="server" />
    </legend>
    <table class="mtable">
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblItem" runat="server" Text="${MasterData.Item.Code}:" />
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbItem" runat="server" DescField="Description" ValueField="Code"
                    Width="250" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" MustMatch="true" />
            </td>
            <td class="ttd01">
            </td>
            <td class="ttd02">
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="ttd02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                        CssClass="query" />
                    <asp:Button ID="btnNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click"
                        CssClass="add" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        CssClass="back" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
