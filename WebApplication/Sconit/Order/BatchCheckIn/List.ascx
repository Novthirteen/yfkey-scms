<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Order_BatchCheckIn_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
function CheckAll()
{
    var GV_ListId = document.getElementById("<%=GV_List.ClientID %>");
    var allselect = GV_ListId.rows[0].cells[0].getElementsByTagName("INPUT")[0].checked;
    for(i = 1;i < GV_ListId.rows.length; i++)
    {
        GV_ListId.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = allselect;
    }
}
</script>

<fieldset>
    <legend>${MasterData.Order.OrderHead.AvailableOrder}</legend>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"  DataKeyNames="OrderNo">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div onclick="CheckAll()">
                            <asp:CheckBox ID="CheckAll" runat="server" />
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbOrderNo" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="OrderNo" HeaderText="${MasterData.Order.OrderHead.OrderNo.Production}"
                    SortExpression="OrderNo" />
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.PartyFrom.Supplier}"
                    SortExpression="PartyFrom.Name">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "PartyFrom.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.PartyTo.Customer}" SortExpression="PartyTo.Name">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "PartyTo.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreateDate" HeaderText="${MasterData.Order.OrderHead.CreateDate}"
                    SortExpression="CreateDate" />
                <asp:BoundField DataField="WindowTime" HeaderText="${MasterData.Order.OrderHead.WindowTime}"
                    SortExpression="WindowTime" />
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.Priority}">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblPriority" runat="server" Code="OrderPriority" Value='<%# Bind("Priority") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.CreateUser}" SortExpression="CreateUser.Name">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "CreateUser.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
<div class="tablefooter">
    <cc1:Button ID="btnStart" runat="server" Text="${MasterData.Order.Button.Start}"
        CssClass="button2" OnClick="btnStart_Click" FunctionId="StartOrder" />
    <cc1:Button ID="btnCancel" runat="server" Text="${Common.Button.Cancel}" CssClass="button2"
        OnClick="btnCancel_Click" FunctionId="CancelOrder" />
</div>
