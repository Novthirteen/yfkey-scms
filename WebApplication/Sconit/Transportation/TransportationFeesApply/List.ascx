<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Transportation_TransportationFeesApply_List" %>

<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Code"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="${Transportation.Expense.Code}" SortExpression="Code" />
                <asp:TemplateField HeaderText="${Transportation.Expense.Carrier}" SortExpression="Carrier.Name">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Carrier.Name")%>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Transportation.Expense.Amount}" SortExpression="Amount">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfAmount" runat="server" Value='<%# Bind("Amount") %>' />
                        <%# DataBinder.Eval(Container.DataItem, "Amount", "{0:0.########}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Remark" HeaderText="${Transportation.Expense.Remark}" SortExpression="Remark" />
                <asp:BoundField DataField="CreateDate" HeaderText="${Transportation.Expense.CreateDate}" SortExpression="CreateDate"
                    DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
                <asp:TemplateField HeaderText="${Transportation.Expense.CreateUser}" SortExpression="CreateUser">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "CreateUser.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnCreate" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>'
                            Text="${Transportation.TransportationOrder.Create}" OnClick="lbtnCreate_Click" OnClientClick="return confirm('${Transportation.TransportationOrder.Create.Confirm}')" >
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
