<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Visualization__FlowDetailTrack_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll" SelectCountMethod="FindCount">
            <Columns>
                <asp:BoundField DataField="Sequence" HeaderText="${MasterData.Flow.FlowDetail.Sequence}"
                    SortExpression="Sequence" />
                <asp:TemplateField HeaderText="${MasterData.Flow.FlowDetail.ItemCode}" SortExpression="Item">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Item") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UnitCount" HeaderText="${MasterData.Flow.FlowDetail.UnitCount}"
                    SortExpression="UnitCount" DataFormatString="{0:0.########}" />
                <asp:TemplateField HeaderText="${MasterData.Flow.FlowDetail.Uom}">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Uom")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.FlowDetail.LocationFrom}" SortExpression="LocationFrom">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "LocationFrom")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.FlowDetail.LocationTo}" SortExpression="LocationTo">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "LocationTo")%>
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
                <asp:BoundField DataField="EventCode" HeaderText="${MasterData.FlowTrack.EventCode}" />
                <asp:BoundField DataField="Version" HeaderText="${MasterData.FlowTrack.Version}" />
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.View}" OnClick="lbtnView_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
