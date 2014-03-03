<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Reports_IntransitDetail_Detail" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="floatdiv" class="GridView">
    <fieldset>
        <legend>${Reports.IntransitDetail.Detail}</legend>
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" DefaultSortExpression="OrderDetail.OrderHead.WindowTime"
            DefaultSortDirection="Descending">
            <Columns>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.Asn}" SortExpression="InProcessLocation.IpNo">
                    <ItemTemplate>
                        <asp:Label ID="lblIpNo" runat="server" Text='<%# Eval("InProcessLocation.IpNo")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.Supplier}" SortExpression="InProcessLocation.PartyFrom.Name">
                    <ItemTemplate>
                        <asp:Label ID="lblSupplier" runat="server" Text='<%# Eval("InProcessLocation.PartyFrom.Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.Customer}" SortExpression="InProcessLocation.PartyTo.Name">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("InProcessLocation.PartyTo.Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.ItemCode}" SortExpression="OrderDetail.Item.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("OrderDetail.Item.Code")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.ItemDescription}" SortExpression="OrderDetail.Item.Desc1">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("OrderDetail.Item.Description")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.ReferenceItemCode}" SortExpression="OrderDetail.ReferenceItemCode">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceItemCode" runat="server" Text='<%# Eval("OrderDetail.ReferenceItemCode")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.Uom}" SortExpression="OrderDetail.Uom.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Eval("OrderDetail.Uom.Code")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.UnitCount}" SortExpression="OrderDetail.UnitCount">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Eval("OrderDetail.UnitCount", "{0:0.###}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.LocationFrom}" SortExpression="OrderDetail.LocationFrom">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationFrom" runat="server" Text='<%# Eval("OrderDetail.DefaultLocationFrom.Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.LocationTo}" SortExpression="OrderDetail.LocationTo">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationTo" runat="server" Text='<%# Eval("OrderDetail.DefaultLocationTo.Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Qty" HeaderText="${Reports.IntransitDetail.Qty}" DataFormatString="{0:0.###}" />
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.CreateDate}" SortExpression="InProcessLocation.CreateDate">
                    <ItemTemplate>
                        <asp:Label ID="lblWindowTime" runat="server" Text='<%# Eval("InProcessLocation.CreateDate", "{0:MM-dd HH:mm}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.WindowTime}" SortExpression="OrderDetail.OrderHead.WindowTime">
                    <ItemTemplate>
                        <asp:Label ID="lblWindowTime" runat="server" Text='<%# Eval("OrderDetail.OrderHead.WindowTime", "{0:MM-dd HH:mm}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
        <div class="tablefooter">
            <asp:Button ID="btnClose" runat="server" Text="${Common.Button.Close}" CssClass="button2"
                OnClick="btnClose_Click" />
        </div>
    </fieldset>
</div>
