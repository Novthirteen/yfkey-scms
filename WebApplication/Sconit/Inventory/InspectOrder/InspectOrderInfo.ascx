<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InspectOrderInfo.ascx.cs"
    Inherits="Inventory_InspectOrder_InspectOrderInfo" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="InspectDetailList.ascx" TagName="DetailList" TagPrefix="uc2" %>

<fieldset>
    <legend>${MasterData.Inventory.InspectOrder}</legend>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblInspectNo" runat="server" Text="${MasterData.Inventory.InspectOrder.InspectNo}:" />
            </td>
            <td class="td02">
                <asp:Label ID="lbInspectNo" runat="server" />
            </td>
             <td class="td01">
                <asp:Literal ID="lblStatus" runat="server" Text="${Common.CodeMaster.Status}:" />
            </td>
            <td class="td02">
                <asp:Label ID="lbStatus" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblCreateUser" runat="server" Text="${MasterData.Inventory.Repack.CreateUser}:" />
            </td>
            <td class="td02">
                <asp:Label ID="lbCreateUser" runat="server" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblCreateDate" runat="server" Text="${MasterData.Inventory.Repack.CreateDate}:" />
            </td>
            <td class="td02">
                <asp:Label ID="lbCreateDate" runat="server" />
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
                <asp:Button ID="btnInspect" runat="server" OnClick="btnInspect_Click" Text="${Common.Button.Confirm}"
                    CssClass="button2" />
                <asp:Button ID="btnQualified" runat="server" OnClick="btnQualified_Click" Text="${MasterData.InspectOrder.Qualified}"
                    CssClass="button2" />
                <asp:Button ID="btnUnqalified" runat="server" OnClick="btnUnQualified_Click" Text="${MasterData.InspectOrder.Unqualified}"
                    CssClass="button2" />
                <asp:Button ID="btnPrint" runat="server" Text="${MasterData.InspectOrder.Print}" OnClick="btnPrint_Click"
                    CssClass="button2" />
                <asp:Button ID="btnUnqualifiedPrint" runat="server" Text="${MasterData.InspectOrder.Unqualified.Print}" OnClick="btnUnqualifiedPrint_Click"
                    CssClass="button2" />
                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                    CssClass="button2" />
            </td>
        </tr>
    </table>
</fieldset>
<uc2:DetailList ID="ucDetailList" runat="server" Visible="true" />