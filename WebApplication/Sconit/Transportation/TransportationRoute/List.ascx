﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Transportation_TransportationRoute_List" %>

<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Code"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="${Transportation.TransportationRoute.Code}" SortExpression="Code" />
                <asp:BoundField DataField="Description" HeaderText="${Transportation.TransportationRoute.Description}" SortExpression="Description" />
                <asp:TemplateField HeaderText="${Transportation.TransportationRoute.ShipFrom}" SortExpression="ShipFrom">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ShipFrom.FullAddress")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Transportation.TransportationRoute.ShipTo}" SortExpression="ShipTo">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ShipTo.FullAddress")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CheckBoxField DataField="IsActive" HeaderText="${Transportation.TransportationRoute.IsActive}"
                    SortExpression="IsActive" />
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>'
                            Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click">
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>'
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
