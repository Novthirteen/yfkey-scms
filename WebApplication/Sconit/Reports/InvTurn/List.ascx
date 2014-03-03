<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Reports_InvTurn_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<%@ Register Src="~/Reports/LocAging/View.ascx" TagName="View" TagPrefix="uc" %>
<%@ Register Src="~/Reports/LocAging/DetailList.ascx" TagName="Detail" TagPrefix="uc" %>
<fieldset>
    <div class="GridView">
        <sc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" SkinID="GV"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" seqtext="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="${Common.Business.Region}" SortExpression="Region">
                    <ItemTemplate>
                        <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("Region.Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Location}" SortExpression="Location">
                    <ItemTemplate>
                        <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location.Code")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Location.Name}" SortExpression="Location">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationName" runat="server" Text='<%# Eval("Location.Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemCode}" SortExpression="Item">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item.Code")%>' />&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemDescription}" SortExpression="Item">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("Item.Description")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Uom}" SortExpression="Item">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Eval("Item.Uom.Code")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.StartInv}" SortExpression="StartInvQty">
                    <ItemTemplate>
                        <asp:Label ID="lblStartInvQty" runat="server" Text='<%# Eval("StartInvQty")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.EndInv}" SortExpression="InvQty">
                    <ItemTemplate>
                        <asp:Label ID="lblInvQty" runat="server" Text='<%# Eval("InvQty")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.TotalOutQty}" SortExpression="TotalOutQty">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalOutQty" runat="server" Text='<%# Eval("TotalOutQty")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.InvTurn.InvTurnRate}" SortExpression="InvTurnRate">
                    <ItemTemplate>
                        <asp:Label ID="lblInvTurnRate" runat="server" Text='<%# Eval("InvTurnRate")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
               <%-- <asp:TemplateField HeaderText="${Reports.InvTurn.InvTurnDays}" SortExpression="InvTurnDays">
                    <ItemTemplate>
                        <asp:Label ID="lblInvTurnDays" runat="server" Text='<%# Eval("InvTurnDays")%>' />
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </sc1:GridView>
        <sc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </sc1:GridPager>
    </div>
</fieldset>