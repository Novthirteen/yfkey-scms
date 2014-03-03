<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Visualization_ProductLineInprocessLocation_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <legend>${MasterData.Inventory.InspectOrder.Detail}</legend>
    <div>
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" DefaultSortExpression="Id" DefaultSortDirection="Descending" >
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.ProductLineInprocessLocation.Flow}" SortExpression="ProductLine.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblFlowCode" runat="server" Text='<%# Bind("ProductLine.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ProductLineInprocessLocation.ItemCode}" SortExpression="Item.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="${MasterData.ProductLineInprocessLocation.ItemDescription}" SortExpression="Item.Desc1">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("Item.Description") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ProductLineInprocessLocation.HuId}" SortExpression="HuId">
                    <ItemTemplate>
                        <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("HuId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ProductLineInprocessLocation.Qty}" >
                    <ItemTemplate>
                        <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ProductLineInprocessLocation.BackFlushQty}" >
                    <ItemTemplate>
                        <asp:Label ID="lblBackFlushQty" runat="server" Text='<%# Bind("BackFlushQty","{0:0.########}")  %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ProductLineInprocessLocation.Status}" >
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ProductLineInprocessLocation.LocationFrom}" SortExpression="LocationFrom.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblLocFrom" runat="server" Text='<%# Bind("LocationFrom.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="${MasterData.ProductLineInprocessLocation.CreateUser}" SortExpression="CreateUser">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateUser" runat="server" Text='<%# Bind("CreateUser.Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
