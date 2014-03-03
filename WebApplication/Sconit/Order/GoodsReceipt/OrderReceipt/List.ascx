<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Order_GoodsReceipt_OrderReceipt_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="OrderNo"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="OrderNo" HeaderText="${MasterData.Order.OrderHead.OrderNo}"
                    SortExpression="OrderNo" />
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.PartyFrom.Supplier}"
                    SortExpression="PartyFrom">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "PartyFrom.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.PartyTo.Customer}" SortExpression="PartyTo">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "PartyTo.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreateDate" HeaderText="${MasterData.Order.OrderHead.CreateDate}"
                    SortExpression="CreateDate" />
                <asp:BoundField DataField="WindowTime" HeaderText="${MasterData.Order.OrderHead.WindowTime}"
                    SortExpression="WindowTime" />
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.Priority}" SortExpression="Priority">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblPriority" runat="server" Code="OrderPriority" Value='<%# Bind("Priority") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.CreateUser}" SortExpression="CreateUser.FirstName">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "CreateUser.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfModuleSubType" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "SubType") %>' />
                        <cc1:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderNo") %>'
                            Text="${MasterData.Order.Button.Receive}" OnClick="lbtnEdit_Click" FunctionId="ReceiveOrder">
                        </cc1:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
