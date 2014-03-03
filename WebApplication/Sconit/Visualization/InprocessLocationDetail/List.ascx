<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Visualization_InprocessLocationDetail_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <legend>${MasterData.Inventory.InspectOrder.Detail}</legend>
    <div>
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" DefaultSortExpression="Id" DefaultSortDirection="Descending">
            <Columns>
                <asp:TemplateField HeaderText="${InProcessLocation.IpNo}" SortExpression="InProcessLocation">
                    <ItemTemplate>
                        <asp:Label ID="lblIpNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InProcessLocation.IpNo")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${InProcessLocation.InProcessLocationDetail.OrderNo}"
                    SortExpression="OrderLocationTransaction.OrderDetail.OrderHead">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.OrderHead.OrderNo")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${InProcessLocation.InProcessLocationDetail.Item}"
                    SortExpression="OrderLocationTransaction.Item">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.Item.Code")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemDescription}" SortExpression="OrderLocationTransaction.Item">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.Item.Description")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Uom}" SortExpression="OrderLocationTransaction.Uom.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.Item.Uom.Code")%>' 
                        ToolTip='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.Item.Uom.Description")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.UnitCount}" SortExpression="OrderLocationTransaction.OrderDetail.UnitCount">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.UnitCount", "{0:0.########}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.LocFrom}" SortExpression="OrderLocationTransaction.OrderDetail.LocationFrom.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationFrom" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.DefaultLocationFrom.Code")%>' 
                         ToolTip='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.DefaultLocationFrom.Name")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.LocTo}" SortExpression="OrderLocationTransaction.OrderDetail.LocationTo.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationTo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.DefaultLocationTo.Code")%>'
                        ToolTip='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.DefaultLocationTo.Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="RemainQty" HeaderText="${Common.Business.Qty}" SortExpression="RemainQty"  DataFormatString="{0:0.########}"/>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
