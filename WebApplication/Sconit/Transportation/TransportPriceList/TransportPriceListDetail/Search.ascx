<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Transportation_TransportPriceList_TransportPriceListDetail_Search" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <legend>
        <asp:Literal ID="lblCurrentTransportPriceList" runat="server" Text="${Transportation.TransportPriceListDetail.CurrentTransportPriceList}:" />
        <asp:Literal ID="lbCurrentTransportPriceList" runat="server" />
    </legend>
    <table class="mtable">
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblShipFrom" runat="server" Text="${Transportation.TransportPriceListDetail.ShipFrom}:" />
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbShipFrom" runat="server" Width="250" DescField="City" ValueField="Address"
                    MustMatch="true" ServiceMethod="GetAllTransportationAddress" ServicePath="TransportationAddressMgr.service"/>
            </td>
            <td class="ttd01">
                <asp:Literal ID="lblShipTo" runat="server" Text="${Transportation.TransportPriceListDetail.ShipTo}:" />
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
                        CssClass="add" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        CssClass="back" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
