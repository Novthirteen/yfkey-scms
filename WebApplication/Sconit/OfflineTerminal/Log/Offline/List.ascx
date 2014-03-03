<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MasterData_Client_Log_Offline_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div>
        <cc1:GridView ID="GV_List" runat="server" DataKeyNames="Id" AllowSorting="true" AllowPaging="True"
            AutoGenerateColumns="false" PagerID="gp" Width="100%" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Id" Visible="false" SortExpression="Id" />
                <asp:TemplateField HeaderText="${MasterData.Client.Code}" SortExpression="Client.ClientId">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Client.ClientId")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="OrderNo" HeaderText="${Common.Business.OrderNo}" SortExpression="OrderNo" />
                <asp:BoundField DataField="UserCode" HeaderText="${MasterData.Client.UserCode}" SortExpression="UserCode" />
                <asp:BoundField DataField="OrderType" HeaderText="${MasterData.Client.OrderType}"
                    SortExpression="OrderType" />
                <asp:BoundField DataField="Flow" HeaderText="${Common.Business.Flow}" SortExpression="Flow" />
                <asp:BoundField DataField="SynStatus" HeaderText="${MasterData.Client.SynStatus}"
                    SortExpression="SynStatus" />
                <asp:BoundField DataField="SynTime" HeaderText="${MasterData.Client.SynTime}" SortExpression="SynTime" />
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${MasterData.Client.Detail}" OnClick="lbtnView_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
