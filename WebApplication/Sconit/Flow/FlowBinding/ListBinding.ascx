<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ListBinding.ascx.cs" Inherits="MasterData_FlowBinding_ListBinding" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <legend>${MasterData.Flow.Binding.Binding}</legend>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll" SelectCountMethod="FindCount"
            OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.Flow.Binding.SlaveFlow.Code}" SortExpression="SlaveFlow.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "SlaveFlow.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.Binding.SlaveFlow.Type}" SortExpression="SlaveFlow.Type">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblSlaveType"  runat="server" Code="FlowType" Value='<%# Bind("SlaveFlow.Type") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.Binding.SlaveFlow.Description}" SortExpression="SlaveFlow.Description">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "SlaveFlow.Description")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.Binding.BindingType}" SortExpression="BindingType">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblBindingType"  runat="server" Code="BindingType" Value='<%# Bind("BindingType") %>' />
                    </ItemTemplate>
                </asp:TemplateField>     
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click">
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.Delete}" OnClick="lbtnDelete_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10" NamedQuery="true">
        </cc1:GridPager>
    </div>
</fieldset>
