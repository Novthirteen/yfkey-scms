<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MasterData_FlowDetail_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Sequence" HeaderText="${MasterData.Flow.FlowDetail.Sequence}"
                    SortExpression="Sequence" />
                <asp:TemplateField HeaderText="${MasterData.Flow.FlowDetail.ItemCode}" SortExpression="Item.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Item.Code") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UnitCount" HeaderText="${MasterData.Flow.FlowDetail.UnitCount}"
                    SortExpression="UnitCount" DataFormatString="{0:0.########}" />
                <asp:TemplateField HeaderText="${MasterData.Flow.FlowDetail.Uom}" SortExpression="Uom.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Uom.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.FlowDetail.LocationFrom}" SortExpression="LocationFrom">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "LocationFrom.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.FlowDetail.LocationTo}" SortExpression="LocationTo">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "LocationTo.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.FlowDetail.BillSettleTerm}" SortExpression="BillSettleTerm"
                    Visible="false">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblBillSettleTerm" runat="server" Code="BillSettleTerm" Value='<%# Bind("BillSettleTerm") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.FlowDetail.AutoCreate}" SortExpression="IsAutoCreate">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblIsAutoCreate" runat="server" Code="TrueOrFalse" Value='<%# Bind("IsAutoCreate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.FlowDetail.NeedInspect}" SortExpression="NeedInspection">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblNeedInspection" runat="server" Code="TrueOrFalse" Value='<%# Bind("NeedInspection") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click">
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.View}" OnClick="lbtnView_Click">
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
</fieldset>
