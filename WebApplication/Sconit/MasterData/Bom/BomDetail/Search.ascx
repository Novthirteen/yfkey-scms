<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="MasterData_Bom_BomDetail_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblParCode" runat="server" Text="${MasterData.Bom.ParCode}:" />
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbParCode" runat="server" Visible="true" Width="250" DescField="Description"
                    ValueField="Code" ServicePath="BomMgr.service" ServiceMethod="GetAllBom" />
            </td>
            <td class="ttd01">
                <asp:Literal ID="lblCompCode" runat="server" Text="${MasterData.Bom.CompCode}:" />
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbCompCode" runat="server" Visible="true" Width="250" DescField="Description"
                    ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetAllItem" />
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
