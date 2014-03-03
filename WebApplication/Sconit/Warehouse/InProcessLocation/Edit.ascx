<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Warehouse_InProcessLocation_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<asp:FormView ID="FV_InProcessLocation" runat="server" DataSourceID="ODS_InProcessLocation"
    DefaultMode="Edit" DataKeyNames="IpNo" OnDataBound="FV_Flow_DataBound">
    <EditItemTemplate>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblIpNo" runat="server" Text="${InProcessLocation.IpNo}:" />
                </td>
                <td class="td02">
                    <asp:Literal ID="lbIpNo" runat="server" Text='<%# Bind("IpNo") %>' />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblType" runat="server" Text="${InProcessLocation.Type}:" />
                </td>
                <td class="td02">
                    <sc1:CodeMstrLabel ID="cmlType" runat="server" Code="IpType" Value='<%# Bind("Type") %>' />
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
                    <asp:Literal ID="lbDockDescription" runat="server" Text='<%# Bind("DockDescription") %>' />
                </td>
                <td class="td01">
                    <asp:Literal ID="lbStatus" runat="server" Text="${InProcessLocation.Status}:" />
                </td>
                <td class="td02">
                    <sc1:ReadonlyTextBox ID="tbStatus" runat="server" CodeField="Status" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblCurrentOperation" runat="server" Text="${InProcessLocation.CurrentOperation}:" />
                </td>
                <td class="td02">
                    <uc3:textbox ID="tbCurrentOperation" runat="server" Visible="false" Width="250" DescField="Activity"
                        ValueField="Operation" ServicePath="InProcessLocationTrackMgr.service" ServiceMethod="GetInProcessLocationTrack"
                        Text='<%# Bind("CurrentOperation") %>' />
                    <sc1:ReadonlyTextBox ID="rtbCurrentOperation" runat="server" CodeField="CurrentOperation"
                        Visible="false" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblCurrentActivity" runat="server" Text="${InProcessLocation.CurrentActivity}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbCurrentActivity" runat="server" Text='<%# Bind("CurrentActivity") %>'
                        onfocus="this.blur();" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblDisposition" runat="server" Text="${InProcessLocation.Disposition}:" />
                </td>
                <td class="td02">
                    <asp:Label ID="lbDisposition" runat="server" Text='<%# Bind("Disposition") %>' Visible="true" />
                    <asp:TextBox ID="tbDisposition" runat="server" CssClass="inputRequired" Visible="false" />
                    <asp:RequiredFieldValidator ID="rfvDisposition" runat="server" Enabled="true" ErrorMessage="${InProcessLocation.Disposition.Required}"
                        ControlToValidate="tbDisposition"  ValidationGroup="vgClose" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblReferenceOrderNo" runat="server" Text="${InProcessLocation.ReferenceOrderNo}:" />
                </td>
                <td class="td02">
                    <asp:Label ID="lbReferenceOrderNo" runat="server" Text='<%# Bind("ReferenceOrderNo") %>'
                        Visible="true" />
                    <asp:TextBox ID="tbReferenceOrderNo" runat="server" CssClass="inputRequired" Visible="false" />
                    <asp:RequiredFieldValidator ID="rfvReferenceOrderNo" runat="server" Enabled="true"
                        ErrorMessage="${InProcessLocation.ReferenceOrderNo.Required}" ControlToValidate="tbReferenceOrderNo"  ValidationGroup="vgClose" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="ltlFlow" runat="server" Text="${MasterData.Distribution.PickList.OrderType}:" />
                </td>
                <td class="td02">
                    <asp:Label ID="lblFlowCode" runat="server" Text='<%# Eval("OrderType") %>' Visible="true" />
                </td>
                <td class="td01">
                </td>
                <td class="td02">
                         </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ODS_InProcessLocation" runat="server" TypeName="com.Sconit.Web.InProcessLocationMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.Distribution.InProcessLocation" SelectMethod="LoadInProcessLocation">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
