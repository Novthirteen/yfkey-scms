<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Transportation_TransportationRoute_Search" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblCode" runat="server" Text="${Transportation.TransportationRoute.Code}:" />
            </td>
            <td class="ttd02">
                <asp:TextBox ID="tbCode" runat="server" Visible="true" />
            </td>
            <td class="ttd01">
            </td>
            <td class="ttd02">
            </td>
        </tr>
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblShipFrom" runat="server" Text="${Transportation.TransportationRoute.ShipFrom}:" />
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbShipFrom" runat="server" Width="250" DescField="City" ValueField="Address"
                    MustMatch="true" ServiceMethod="GetAllTransportationAddress" ServicePath="TransportationAddressMgr.service"/>
            </td>
            <td class="ttd01">
                <asp:Literal ID="lblShipTo" runat="server" Text="${Transportation.TransportationRoute.ShipTo}:" />
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbShipTo" runat="server" Width="250" DescField="City" ValueField="Address"
                    MustMatch="true" ServiceMethod="GetAllTransportationAddress" ServicePath="TransportationAddressMgr.service"/>
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="ttd02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                        CssClass="query" />
                    <asp:Button ID="btnNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click"
                        CssClass="back" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
