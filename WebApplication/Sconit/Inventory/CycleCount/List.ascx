<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Inventory_CycleCount_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Code"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll" SelectCountMethod="FindCount"
            OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="${Common.Business.OrderNo}" SortExpression="Code" />
                <asp:TemplateField HeaderText="${Common.Business.Type}" SortExpression="Type">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblType" Code="PhysicalCountType" runat="server" Value='<%#Bind("Type") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Location}" SortExpression="Location.Name">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Location.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="EffectiveDate" SortExpression="EffectiveDate" HeaderStyle-Wrap="false"
                    HeaderText="${Common.Business.EffDate}" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="Status" HeaderText="${Common.CodeMaster.Status}" SortExpression="Status" />
                <asp:BoundField DataField="LastModifyDate" SortExpression="LastModifyDate" HeaderStyle-Wrap="false"
                    HeaderText="${Common.Business.LastModifyDate}" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:TemplateField HeaderText="${Common.Business.LastModifyUser}" SortExpression="LastModifyUser.Name">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "LastModifyUser.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <cc1:LinkButton ID="lbtnView" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>'
                            Text="${Common.Button.View}" OnClick="lbtnView_Click" FunctionId="ViewOrderDetail">
                        </cc1:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
