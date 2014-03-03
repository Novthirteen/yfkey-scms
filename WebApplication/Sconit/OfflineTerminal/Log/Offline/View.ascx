<%@ Control Language="C#" AutoEventWireup="true" CodeFile="View.ascx.cs" Inherits="OfflineTerminal_Log_Offline_View" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="floatdiv">
    <fieldset>
        <legend>${MasterData.Client.OrderDetail}</legend>
        <asp:GridView ID="GV_OfflineLog" runat="server" AutoGenerateColumns="False" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Id" Visible="false" />
                <asp:BoundField DataField="Seq" HeaderText="${Common.GridView.Seq}" ReadOnly="true" />
                <asp:TemplateField HeaderText="${Common.Business.ItemCode}">
                    <ItemTemplate>
                        <asp:Label ID="ltlItem" Text='<%# DataBinder.Eval(Container.DataItem, "ItemCode")%>'
                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "ItemDescription")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UomCode" HeaderText="${MasterData.Uom.Code}" ReadOnly="true" />
                <asp:BoundField DataField="UnitCount" HeaderText="${Common.Business.UnitCount}" ReadOnly="true"
                    DataFormatString="{0:0.########}" />
                <asp:BoundField DataField="OrderedQty" HeaderText="${MasterData.Order.OrderDetail.OrderedQty}"
                    DataFormatString="{0:0.########}" ReadOnly="true" />
                <asp:BoundField DataField="ShippedQty" HeaderText="${MasterData.Order.OrderDetail.ShippedQty}"
                    DataFormatString="{0:0.########}" ReadOnly="true" />
                <asp:BoundField DataField="ReceivedQty" HeaderText="${MasterData.Order.OrderDetail.ReceivedQty}"
                    DataFormatString="{0:0.########}" ReadOnly="true" />
                <asp:BoundField DataField="ReceiveQty" HeaderText="${MasterData.Order.OrderDetail.CurrentReceiveQty}"
                    DataFormatString="{0:0.########}" ReadOnly="true" />
                <asp:BoundField DataField="RejectQty" HeaderText="${MasterData.Order.OrderDetail.CurrentRejectQty}"
                    DataFormatString="{0:0.########}" ReadOnly="true" />
                <asp:BoundField DataField="ScrapQty" HeaderText="${MasterData.Order.OrderDetail.CurrentScrapQty}"
                    DataFormatString="{0:0.########}" ReadOnly="true" />
            </Columns>
        </asp:GridView>
    </fieldset>
    <fieldset runat="server" id="fieldsetWorkingHours" visible="false">
        <legend>${MasterData.Client.WorkingHours}</legend>
        <asp:GridView ID="GV_WorkingHours" runat="server" AutoGenerateColumns="False" DataKeyNames="Id">
            <Columns>
                <asp:BoundField DataField="Id" Visible="false" />
                <asp:BoundField DataField="Employee" HeaderText="${MasterData.Employee.Code}" ReadOnly="true" />
                <asp:BoundField DataField="Hours" HeaderText="${MasterData.Client.WorkingHours}"
                    DataFormatString="{0:0.########}" ReadOnly="true" />
            </Columns>
        </asp:GridView>
    </fieldset>
    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" />
</div>
