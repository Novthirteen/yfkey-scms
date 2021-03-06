﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="MasterData_Reports_Inventory_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblLocation" runat="server" Text="${Common.Business.Location}:" />
            </td>
            <td class="td02">
               <%-- <uc3:TextBox ID="tbLocation" runat="server" Visible="true" DescField="Name" Width="280"
                    ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocationByUserCode" />--%>
                    <textarea id="ttLocation" rows="2" runat="server"  style="width:200" />
            </td>
            <td class="td01">
                <%--<asp:Literal ID="lblItem" runat="server" Text="${Common.Business.ItemCode}:" />--%>
                物料代码(多选)
            </td>
            <td class="td02">
                    <textarea id="ttItem" rows="2" runat="server"  style="width:200" />
              <%--  <uc3:TextBox ID="tbItem" runat="server" Visible="true" DescField="Description" ImageUrlField="ImageUrl"
                    Width="280" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />--%>
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblLotNo" runat="server" Text="${Common.Business.LotNo}:" />
            </td>
            <td class="td02">
                    <%--<textarea id="ttLotNo" rows="2" runat="server"  style="width:200" />--%>
                <asp:TextBox ID="tbLotNo" runat="server" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblItem" runat="server" Text="${Common.Business.ItemCode}:" />
            </td>
            <td class="t02">
               <uc3:TextBox ID="tbItem" runat="server" Visible="true" DescField="Description" ImageUrlField="ImageUrl"
                    Width="280" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />
                
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
            <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" CssClass="button2"
                    OnClick="btnSearch_Click" />
                <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" CssClass="button2"
                    OnClick="btnExport_Click" />
                <asp:textbox ID="PostBackSortHidden" runat="server" Text=""  style="display:none" />
                <asp:textbox ID="PostBackHidden" runat="server" Text=""  style="display:none" />
            </td>

        </tr>
    </table>
</fieldset>
