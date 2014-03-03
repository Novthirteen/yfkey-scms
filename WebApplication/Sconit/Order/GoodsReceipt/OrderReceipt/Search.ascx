<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Order_GoodsReceipt_OrderReceipt_Search" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblOrderNo" runat="server" Text="${MasterData.Order.OrderHead.OrderNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbOrderNo" runat="server" Visible="true" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblFlow" runat="server" Text="${MasterData.Flow.Flow.Procurement}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbFlow" runat="server" Visible="true" DescField="Description" ValueField="Code"
                    ServicePath="FlowMgr.service" AutoPostBack="true" MustMatch="true"
                    Width="250"  ServiceMethod="GetFlowList" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblPartyFrom" runat="server"/>
            </td>
            <td class="td02">
                <uc3:textbox ID="tbPartyFrom" runat="server" Visible="true" Width="250" DescField="Name"
                    ValueField="Code" ServicePath="PartyMgr.service" ServiceMethod="GetFromParty" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblPartyTo" runat="server"  />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbPartyTo" runat="server" Visible="true" Width="250" DescField="Name"
                    ValueField="Code" MustMatch="true" ServicePath="PartyMgr.service" ServiceMethod="GetToParty" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblLocFrom" runat="server" Text="${MasterData.Order.OrderHead.LocFrom}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbLocFrom" runat="server" Visible="true" Width="250" DescField="Name"
                    ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocation"
                    ServiceParameter="string:#tbPartyFrom" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblLocTo" runat="server" Text="${MasterData.Order.OrderHead.LocTo}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbLocTo" runat="server" Visible="true" Width="250" DescField="Name"
                    ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocation"
                    ServiceParameter="string:#tbPartyTo" />
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="ttd02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                    CssClass="button2" />
            </td>
        </tr>
    </table>
</fieldset>