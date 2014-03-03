<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Transportation_TransportationRoute_TransportationRouteDetail_Search" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <legend>
        <asp:Literal ID="lblCurrentTransportationRoute" runat="server" Text="${Transportation.TransportationRouteDetail.CurrentTransportationRoute}:" />
        <asp:Literal ID="lbCurrentTransportationRoute" runat="server" />
    </legend>
    <table class="mtable">
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblTAddress" runat="server" Text="${Transportation.TransportationRouteDetail.TAddress}:" />
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbTAddress" runat="server" Width="250" DescField="City" ValueField="Address"
                    MustMatch="true" ServiceMethod="GetAllTransportationAddress" ServicePath="TransportationAddressMgr.service"/>
            </td>
            <td class="ttd01">
            </td>
            <td class="ttd02">
                <asp:TextBox ID="TextBox1" runat="server" Visible="true" style="display:none;"/>
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
