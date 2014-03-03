<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Order_GoodsReceipt_ViewReceipt_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<asp:FormView ID="FV_Receipt" runat="server" DataSourceID="ODS_Receipt" DefaultMode="Edit"
    DataKeyNames="ReceiptNo" OnDataBound="FV_DataBound">
    <EditItemTemplate>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="ltlReceiptNo" runat="server" Text="${MasterData.Bill.ReceiptNo}:" />
                </td>
                <td class="td02">
                    <asp:Label ID="lblReceiptNo" runat="server" Text='<%# Bind("ReceiptNo") %>' />
                </td>
                <td class="td01">
                    <asp:Literal ID="ltlRefOrderNo" runat="server" Text="${InProcessLocation.IpNo}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbRefOrderNo" runat="server" ReadOnly="true" Text='<%# Bind("ReferenceIpNo") %>' />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblPartyFrom" runat="server" Text="${InProcessLocation.PartyFrom}:" />
                </td>
                <td class="td02">
                    <sc1:ReadonlyTextBox ID="tbPartyFrom" runat="server" CodeField="PartyFrom.Code" DescField="PartyFrom.Name" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblPartyTo" runat="server" Text="${InProcessLocation.PartyTo}:" />
                </td>
                <td class="td02">
                    <sc1:ReadonlyTextBox ID="tbPartyTo" runat="server" CodeField="PartyTo.Code" DescField="PartyTo.Name" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblShipFrom" runat="server" Text="${InProcessLocation.ShipFrom.Address}:" />
                </td>
                <td class="td02">
                    <sc1:ReadonlyTextBox ID="tbShipFrom" runat="server" CodeField="ShipFrom.Code" DescField="ShipFrom.Address" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblShipTo" runat="server" Text="${InProcessLocation.ShipTo.Address}:" />
                </td>
                <td class="td02">
                    <sc1:ReadonlyTextBox ID="tbShipTo" runat="server" CodeField="ShipTo.Code" DescField="ShipTo.Address" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblDockDescription" runat="server" Text="${InProcessLocation.DockDescription}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbDockDescription" runat="server" Text='<%# Bind("DockDescription") %>'
                        ReadOnly="true" />
                </td>
                <td class="td01">
                    <asp:Literal ID="ltlExternalReceiptNo" runat="server" Text="${MasterData.Order.OrderHead.ExtOrderNo}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbExternalReceiptNo" runat="server" Text='<%# Bind("ExternalReceiptNo") %>'
                        ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ODS_Receipt" runat="server" TypeName="com.Sconit.Web.ReceiptMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Receipt" SelectMethod="LoadReceipt">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
