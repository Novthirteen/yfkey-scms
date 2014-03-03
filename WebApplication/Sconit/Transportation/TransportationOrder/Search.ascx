<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Transportation_TransportationOrder_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblOrderNo" runat="server" Text="${Transportation.TransportationOrder.OrderNo}:" />
            </td>
            <td class="ttd02">
                <asp:TextBox ID="tbOrderNo" runat="server" />
            </td>
            <td class="ttd01">
                <asp:Literal ID="lblRoute" runat="server" Text="${Transportation.TransportationOrder.Route}:" />
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbRoute" runat="server" Width="250" DescField="Description" ValueField="Code"
                    MustMatch="true" ServiceMethod="GetAllTransportationRoute"
                    ServicePath="TransportationRouteMgr.service" />
            </td>
        </tr>
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblShipFrom" runat="server" Text="${Transportation.TransportationOrder.ShipFrom}:" />
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbShipFrom" runat="server" Width="250" DescField="Address" ValueField="Id"
                    ServiceMethod="GetAllTransportationAddress" ServicePath="TransportationAddressMgr.service" />
            </td>
            <td class="ttd01">
                <asp:Literal ID="lblShipTo" runat="server" Text="${Transportation.TransportationOrder.ShipTo}:" />
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbShipTo" runat="server" Width="250" DescField="Address" ValueField="Id"
                    MustMatch="true" ServiceMethod="GetAllTransportationAddress"
                    ServicePath="TransportationAddressMgr.service" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlStartDate" runat="server" Text="${Common.Business.StartDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlEndDate" runat="server" Text="${Common.Business.EndDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEndDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
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
