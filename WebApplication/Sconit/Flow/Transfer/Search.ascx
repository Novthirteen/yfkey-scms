<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="MasterData_Flow_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblFlow" runat="server"  Text="${MasterData.Flow.Flow.Transfer}:"/>
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbFlow" runat="server" Visible="true" DescField="Description" ValueField="Code" Width="250"
                    ServicePath="FlowMgr.service" ServiceMethod="GetTransferFlow" />
            </td>
            <td class="ttd01">
                <asp:Literal ID="lblStrategy" runat="server" Text="${MasterData.Flow.Strategy.Transfer}:" />
            </td>
            <td class="ttd02">
                <cc1:CodeMstrDropDownList ID="ddlStrategy" Code="FlowStrategy" runat="server" IncludeBlankOption="true"
                    DefaultSelectedValue="">
                </cc1:CodeMstrDropDownList>
            </td>
        </tr>
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblPartyFrom" runat="server" Text="${MasterData.Flow.Party.From.Region.Transfer}:" />
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbPartyFrom" runat="server" Visible="true" DescField="Name" ValueField="Code" Width="250"
                      ServicePath="PartyMgr.service" ServiceMethod="GetFromParty" />
            </td>
            <td class="ttd01">
                <asp:Literal ID="lblPartyTo" runat="server" Text="${MasterData.Flow.Party.To.Region.Transfer}:" />
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbPartyTo" runat="server" Visible="true" DescField="Name" ValueField="Code" Width="250"
                      ServicePath="PartyMgr.service" ServiceMethod="GetToParty" />
            </td>
        </tr>
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblLocFrom" runat="server" Text="${MasterData.Flow.Location.From}:" />
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbLocFrom" runat="server" Visible="true" DescField="Name" ValueField="Code" Width="250"
                    ServicePath="LocationMgr.service" ServiceMethod="GetLocation" ServiceParameter="string:#tbPartyFrom" />
            </td>
            <td class="ttd01">
             <asp:Literal ID="lblLocTo" runat="server" Text="${MasterData.Flow.Location.To}:" />
            </td>
            <td class="ttd02">
            <uc3:textbox ID="tbLocTo" runat="server" Visible="true" DescField="Name" ValueField="Code" Width="250"
                    ServicePath="LocationMgr.service" ServiceMethod="GetLocation" ServiceParameter="string:#tbPartyTo" />
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="ttd02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                    CssClass="button2" />
                <asp:Button ID="btnNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click"
                    CssClass="button2" />
            </td>
        </tr>
    </table>
</fieldset>
