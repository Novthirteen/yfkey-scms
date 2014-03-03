<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Visualization_OrderEfficiency_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <legend>${MasterData.Inventory.InspectOrder.Detail}</legend>
    <div>
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="OrderNo"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" DefaultSortExpression="OrderNo" DefaultSortDirection="Descending" >
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.Common.Date}" SortExpression="Date">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Date","{0:yyyy-MM-dd}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.OrderEfficiency.OrderNo}" SortExpression="OrderNo">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("OrderNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.OrderEfficiency.PickListNo}" SortExpression="PickListNo">
                    <ItemTemplate>
                        <asp:Label ID="lblPickListNo" runat="server" Text='<%# Bind("PickListNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.OrderEfficiency.FlowCode}" SortExpression="Flow">
                    <ItemTemplate>
                        <asp:Label ID="lblFlowCode" runat="server" Text='<%# Bind("Flow") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.OrderEfficiency.OrderViewType}" >
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="ddlOrderViewType" runat="server" Value='<%# Bind("OrderViewType") %>' Code="OrderViewType" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.OrderEfficiency.CreateDate}" SortExpression="CreateDate">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# Bind("CreateDate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.OrderEfficiency.StartDate}" SortExpression="StartDate">
                    <ItemTemplate>
                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("StartDate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.OrderEfficiency.StartUser}" SortExpression="StartUser">
                    <ItemTemplate>
                        <asp:Label ID="lblStartUser" runat="server" Text='<%# Bind("StartUser.Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.OrderEfficiency.CompleteDate}" SortExpression="CompleteDate">
                    <ItemTemplate>
                        <asp:Label ID="lblCompleteDate" runat="server" Text='<%# Bind("CompleteDate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="${MasterData.OrderEfficiency.CompleteUser}" SortExpression="CompleteUser">
                    <ItemTemplate>
                        <asp:Label ID="lblCompleteUser" runat="server" Text='<%# Bind("CompleteUser.Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
