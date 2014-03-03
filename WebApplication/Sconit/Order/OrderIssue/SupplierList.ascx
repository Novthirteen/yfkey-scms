<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SupplierList.ascx.cs" Inherits="Distribution_OrderIssue_SupplierList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>


<fieldset>
    <legend>${MasterData.Order.OrderHead.AvailableOrder}</legend>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.OrderNo.Distribution}"
                    SortExpression="OrderNo">
                    <ItemTemplate>
                        <asp:LinkButton ID="ltlOrderNo" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderNo") %>'
                            Text='<%# Eval("OrderNo")%>' OnClick="lbtnOrder_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="StartTime" HeaderText="${MasterData.Order.OrderHead.StartTime}"
                    SortExpression="StartTime" />
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.WinDate}" SortExpression="WindowTime">
                    <ItemTemplate>
                        <asp:Label ID="lblWinDate" Text='<%# Bind("WinDate") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.WindowTime}">
                    <ItemTemplate>
                        <asp:Label ID="lblWinTime" Text='<%# Bind("WinTime") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.DeliverySite}">
                    <ItemTemplate>
                        <asp:Label ID="lblDeliverySite" Text='<%# Bind("DeliverySite") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.Priority}">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblPriority" runat="server" Code="OrderPriority" Value='<%# Bind("Priority") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.IsConfirmed}" SortExpression="IsConfirmed">
                    <ItemTemplate>
                         <cc1:CodeMstrLabel ID="lblIsConfirmed" runat="server" Code="TrueOrFalse" Value='<%# Bind("IsConfirmed") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.IsPrinted}" SortExpression="IsPrinted">
                    <ItemTemplate>
                         <cc1:CodeMstrLabel ID="lblIsPrinted" runat="server" Code="TrueOrFalse" Value='<%# Bind("IsPrinted") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>

