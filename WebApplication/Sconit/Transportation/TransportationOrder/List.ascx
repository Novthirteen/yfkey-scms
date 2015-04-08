<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Transportation_TransportationOrder_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="OrderNo"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="OrderNo" HeaderText="${Transportation.TransportationOrder.OrderNo}"
                    SortExpression="OrderNo" />
                <asp:TemplateField HeaderText="${Transportation.TransportationOrder.Route}" SortExpression="PartyFrom.Name">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "TransportationRoute.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--add by ljz start--%>
                <%--<asp:TemplateField HeaderText="${Transportation.TransportationOrder.TransportMethod}">
                    <ItemTemplate>
                        <asp:Label ID="lblTransportMethod" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <%--<asp:BoundField DataField="TransportMethod" HeaderText="${Transportation.TransportationOrder.TransportMethod}"
                    SortExpression="TransportMethod" />--%>
                      <asp:TemplateField HeaderText="${Transportation.TransportPriceListDetail.TransportMethod}"
                    SortExpression="TransportMethod">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblTransportMethod" runat="server" Code="TransportMethod" Value='<%# Bind("TransportMethod") %>' />
                        <asp:Label ID="lbTransportMethod" runat="server" Text='<%# Eval("TransportMethod")%>'
                            CssClass="hidden" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--add by ljz end--%>
                <asp:TemplateField HeaderText="${Transportation.TransportationOrder.ShipFrom}">
                    <ItemTemplate>
                        <asp:Label ID="lblShipFrom" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Transportation.TransportationOrder.ShipTo}">
                    <ItemTemplate>
                        <asp:Label ID="lblShipTo" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Status" HeaderText="${Transportation.TransportationOrder.Status}"
                    SortExpression="Status" />
                <asp:CheckBoxField DataField="IsValuated" HeaderText="${Transportation.TransportationOrder.IsValuated}"
                   />
                <asp:BoundField DataField="CreateDate" HeaderText="${Transportation.TransportationOrder.CreateDate}"
                    SortExpression="CreateDate" />
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.CreateUser}" SortExpression="CreateUser.FirstName">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "CreateUser.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderNo") %>'
                            Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click">
                        </asp:LinkButton>
                        <cc1:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderNo") %>'
                            Visible="false" Text="${Common.Button.Delete}" OnClick="lbtnDelete_Click" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')"
                            FunctionId="DeleteOrder">
                        </cc1:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
