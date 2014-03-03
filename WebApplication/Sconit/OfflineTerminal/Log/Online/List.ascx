<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MasterData_Client_Log_Online_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div>
        <cc1:GridView ID="GV_List" runat="server" DataKeyNames="Id" AllowSorting="true" AllowPaging="True"
            AutoGenerateColumns="false" PagerID="gp" Width="100%">
            <Columns>
                <asp:BoundField DataField="Id" Visible="false" SortExpression="Id" />
                <asp:TemplateField HeaderText="${MasterData.Client.Code}" SortExpression="Client.ClientId">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Client.ClientId")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="OrderNo" HeaderText="${Common.Business.OrderNo}" SortExpression="OrderNo" />
                <asp:BoundField DataField="Operation" HeaderText="${MasterData.Client.Operation}"
                    SortExpression="Operation" />
                <asp:BoundField DataField="Result" HeaderText="${MasterData.Client.Result}" SortExpression="Result" />
                <asp:BoundField DataField="Message" HeaderText="${MasterData.Client.Message}" SortExpression="Message" />
                <asp:BoundField DataField="SynTime" HeaderText="${MasterData.Client.SynTime}" SortExpression="SynTime" />
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
