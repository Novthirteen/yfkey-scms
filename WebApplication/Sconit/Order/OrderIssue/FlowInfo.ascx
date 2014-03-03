<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlowInfo.ascx.cs" Inherits="Distribution_OrderIssue_FlowInfo" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="divFV" runat="server">
    <fieldset>
        <legend>${MasterData.Flow.Basic.Info}</legend>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblPartyFrom" runat="server" Text="${MasterData.Flow.Party.From.Supplier}:" />
                </td>
                <td class="td02">
                    <cc1:ReadonlyTextBox ID="tbPartyFrom" runat="server" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblPartyTo" runat="server" Text="${MasterData.Flow.Party.To.Customer}:" />
                </td>
                <td class="td02">
                    <cc1:ReadonlyTextBox ID="tbPartyTo" runat="server" />
                </td>
            </tr>
            <tr id="trShipAddress" runat="server">
                <td class="td01">
                    <asp:Literal ID="lblShipFrom" runat="server" Text="${MasterData.Flow.Ship.From}:" />
                </td>
                <td class="td02">
                    <cc1:ReadonlyTextBox ID="tbShipFrom" runat="server" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblShipTo" runat="server" Text="${MasterData.Flow.Ship.To}:" />
                </td>
                <td class="td02">
                    <cc1:ReadonlyTextBox ID="tbShipTo" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblCarrier" runat="server" Text="${MasterData.Flow.Carrier}:" />
                </td>
                <td class="td02">
                    <cc1:ReadonlyTextBox ID="tbCarrier" runat="server" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblDockDescription" runat="server" Text="${MasterData.Flow.DockDescription}:" />
                </td>
                <td class="td02">
                    <cc1:ReadonlyTextBox ID="tbDockDescription" runat="server" ShowDescFieldOnly="true" />
                </td>
            </tr>
        </table>
    </fieldset>
</div>
