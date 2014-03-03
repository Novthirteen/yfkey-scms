<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Visualization_LocationBin_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblLocation" runat="server" Text="${Common.Business.Location}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbLocation" runat="server" Visible="true" DescField="Name" Width="280"
                    ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocationByUserCode" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlStorageArea" runat="server" Text="${MasterData.Location.Area}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbStorageArea" runat="server" Visible="true" DescField="Description"
                    Width="280" ValueField="Code" ServicePath="StorageAreaMgr.service" ServiceMethod="GetAllStorageArea" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlStorageBin" runat="server" Text="${MasterData.Location.Bin}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbStorageBin" runat="server" Visible="true" DescField="Description"
                    Width="280" ValueField="Code" ServicePath="StorageBinMgr.service" ServiceMethod="GetAllStorageBin" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblItem" runat="server" Text="${Common.Business.ItemCode}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItem" runat="server" Visible="true" DescField="Description" ImageUrlField="ImageUrl"
                    Width="280" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlHuId" runat="server" Text="${Common.Business.HuId}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbHuId" runat="server" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlLotNo" runat="server" Text="${Common.Business.LotNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbLotNo" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="t02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" CssClass="query"
                        OnClick="btnSearch_Click" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
