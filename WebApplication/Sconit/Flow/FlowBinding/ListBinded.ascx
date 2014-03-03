<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ListBinded.ascx.cs" Inherits="MasterData_FlowBinding_ListBinded" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <legend>${MasterData.Flow.Binding.Binded}</legend>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll" SelectCountMethod="FindCount">
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.Flow.Binding.MasterFlow.Code}" SortExpression="MasterFlow.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "MasterFlow.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.Binding.MasterFlow.Type}" SortExpression="MasterFlow.Type">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "MasterFlow.Type")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.Binding.MasterFlow.Description}" SortExpression="MasterFlow.Description">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "MasterFlow.Description")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.Binding.BindingType}" SortExpression="BindingType">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblBindingType" runat="server" Code="BindingType" Value='<%# Bind("BindingType") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10" NamedQuery="true">
        </cc1:GridPager>
    </div>
</fieldset>
