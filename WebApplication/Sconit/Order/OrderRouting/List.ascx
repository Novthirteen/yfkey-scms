<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Order_OrderRouting_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <legend id="lTitle" runat="server"></legend>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="false" AllowPaging="True" PagerID="gp" Width="100%"
            TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount">
            <Columns>
                <asp:BoundField DataField="Operation" HeaderText="${MasterData.Order.Routing.Operation}" />
                <asp:BoundField DataField="Activity" HeaderText="${MasterData.Order.Routing.Activity}" />
                <asp:TemplateField HeaderText="${MasterData.Order.Routing.WorkCenter.Code}">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "WorkCenter.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.Routing.WorkCenter.Name}">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "WorkCenter.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UnitTime" HeaderText="${MasterData.Order.Routing.UnitTime}" />
                <asp:BoundField DataField="WorkTime" HeaderText="${MasterData.Order.Routing.WorkTime}" />
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
