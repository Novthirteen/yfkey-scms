<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Reports_CSLoc_Detail" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="floatdiv" class="GridView">
    <fieldset>
        <legend>${Reports.CSLoc.Detail}</legend>
        <asp:GridView ID="GV_List" runat="server" AllowPaging="False" AllowSorting="False"
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="LocationName" HeaderText="${Reports.CSLoc.Location}" />
                <asp:BoundField DataField="Bin" HeaderText="${Reports.CSLoc.Bin}" />
                <asp:BoundField DataField="ItemCode" HeaderText="${Reports.CSLoc.ItemCode}" />
                <asp:BoundField DataField="ItemName" HeaderText="${Reports.CSLoc.ItemName}" />
                <asp:BoundField DataField="Uom" HeaderText="${Reports.CSLoc.Uom}" />
                <asp:BoundField DataField="UnitCount" HeaderText="${Reports.CSLoc.UnitCount}" DataFormatString="{0:0.###}" />
                <asp:BoundField DataField="LotNo" HeaderText="${Reports.CSLoc.LotNo}" />
                <asp:BoundField DataField="Qty" HeaderText="${Reports.CSLoc.Qty}" DataFormatString="{0:0.###}" />
                <asp:BoundField DataField="OrderNo" HeaderText="${Reports.CSLoc.OrderNo}" />
                <asp:BoundField DataField="ReceiptNo" HeaderText="${Reports.CSLoc.ReceiptNo}" />
                <asp:BoundField DataField="ReceiptDate" HeaderText="${Reports.CSLoc.ReceiptDate}" />
                <asp:TemplateField HeaderText="${Reports.CSLoc.SettleTerm}">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblSettleTerm" runat="server" Code="BillSettleTerm" Value='<%# Bind("SettleTerm") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="tablefooter">
            <asp:Button ID="btnClose" runat="server" Text="${Common.Button.Close}" CssClass="button2"
                OnClick="btnClose_Click" />
        </div>
    </fieldset>
</div>
