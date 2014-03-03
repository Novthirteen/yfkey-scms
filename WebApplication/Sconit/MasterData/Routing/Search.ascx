<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="MasterData_Routing_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlRegion" runat="server" Text="${MasterData.Region.Code}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbRegion" runat="server" Width="250" DescField="Name" ValueField="Code"
                    ServicePath="RegionMgr.service" ServiceMethod="GetRegion" MustMatch="true" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlCode" runat="server" Text="${MasterData.Routing.Code}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbCode" runat="server" />
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
                </div>
            </td>
        </tr>
    </table>
</fieldset>
