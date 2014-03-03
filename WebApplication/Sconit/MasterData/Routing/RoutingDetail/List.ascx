<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MasterData_Routing_RoutingDetail_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
<legend>${MasterData.Routing.Code}:<asp:Literal runat= "server" ID= "lgdRouting" /></legend>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="${Common.Business.Id}" SortExpression="Id"
                    Visible="false" />
                <asp:TemplateField HeaderText="${MasterData.Routing.Code}" SortExpression="Routing.Code" Visible = "false">
                    <ItemTemplate>
                        <asp:Label ID="GV_lbRoutingCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Routing.Code")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Operation" HeaderText="${MasterData.RoutingDetail.Operation}"
                    SortExpression="Operation" />
                <asp:BoundField DataField="Reference" HeaderText="${MasterData.RoutingDetail.Reference}"
                    SortExpression="Reference" />
                <asp:BoundField DataField="Activity" HeaderText="${MasterData.RoutingDetail.Activity}"
                    SortExpression="Activity" />
                <asp:BoundField DataField="StartDate" HeaderText="${Common.Business.StartDate}" SortExpression="StartDate"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="EndDate" HeaderText="${Common.Business.EndDate}" SortExpression="EndDate"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:TemplateField HeaderText="${MasterData.WorkCenter.Code}" SortExpression="WorkCenter.Code">
                    <ItemTemplate>
                        <asp:Label ID="GV_lblWorkCenter" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WorkCenter.Code")%>'
                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "WorkCenter.Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Location.Code}" SortExpression="Location.Code">
                    <ItemTemplate>
                        <asp:Label ID="GV_lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location.Code")%>'
                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "Location.Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SetupTime" HeaderText="${MasterData.RoutingDetail.SetupTime}"
                    SortExpression="SetupTime" DataFormatString="{0:0.########}" />
                <asp:BoundField DataField="RunTime" HeaderText="${MasterData.RoutingDetail.RunTime}"
                    SortExpression="RunTime" DataFormatString="{0:0.########}" />
                <asp:BoundField DataField="MoveTime" HeaderText="${MasterData.RoutingDetail.MoveTime}"
                    SortExpression="MoveTime" DataFormatString="{0:0.########}" />
                <asp:BoundField DataField="TactTime" HeaderText="${MasterData.RoutingDetail.TactTime}"
                    SortExpression="TactTime" DataFormatString="{0:0.########}" />
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click">
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.Delete}" OnClick="lbtnDelete_Click" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
    <div class="tablefooter">
        <asp:Button ID="btnNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click"
            CssClass="button2" />
        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
            CssClass="button2" />
    </div>
</fieldset>
