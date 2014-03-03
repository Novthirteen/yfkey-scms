<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Transportation_Vehicle_List" %>

<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Code"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="${Transportation.Vehicle.Code}" SortExpression="Code" />
                <asp:TemplateField HeaderText="${Transportation.Vehicle.Carrier}" SortExpression="Carrier.Name">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Carrier.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Transportation.Vehicle.Type}" SortExpression="Type">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblType" runat="server" Code="VehicleType" Value='<%# Bind("Type") %>' />
                         <asp:Label ID="lbType" runat="server" Text='<%# Eval("Type")%>'
                            CssClass="hidden" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Driver" HeaderText="${Transportation.Vehicle.Driver}" SortExpression="Driver" />
                <asp:BoundField DataField="MobilePhone" HeaderText="${Transportation.Vehicle.MobilePhone}" SortExpression="MobilePhone" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="${Transportation.Vehicle.IsActive}"
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
