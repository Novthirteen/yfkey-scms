<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Distribution_OrderIssue_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript">
    function GVCheckClick() {
        if ($(".GVHeader input:checkbox").attr("checked") == true) {
            $(".GVRow input:checkbox").attr("checked", true);
            $(".GVAlternatingRow input:checkbox").attr("checked", true);
        }
        else {
            $(".GVRow input:checkbox").attr("checked", false);
            $(".GVAlternatingRow input:checkbox").attr("checked", false);
        }
    }
</script>

<fieldset>
    <legend>${MasterData.Order.OrderHead.AvailableOrder}</legend>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" OnRowDataBound="GV_List_RowDataBound" >
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div onclick="GVCheckClick()">
                            <asp:CheckBox ID="CheckAll" runat="server" />
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBoxGroup" name="CheckBoxGroup" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.OrderNo.Distribution}"
                    SortExpression="OrderNo">
                    <ItemTemplate>
                        <asp:Literal ID="ltlOrderNo" runat="server" Text='<%# Eval("OrderNo")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
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
                <asp:BoundField DataField="StartTime" HeaderText="${MasterData.Order.OrderHead.StartTime}"
                    SortExpression="StartTime" />
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.WindowTime}" SortExpression="WindowTime">
                    <ItemTemplate>
                        <asp:Label ID="lblWinTime" Text='<%# Bind("WindowTime") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
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
    <cc1:Button ID="btnEditShipQty" runat="server" OnClick="btnEditShipQty_Click" Text="${MasterData.Distribution.Button.EditShipQty}"
        CssClass="button2" FunctionId="ShipOrder" />
    <cc1:Button ID="btnCreatePickList" runat="server" Visible="false" OnClick="btnCreatePickList_Click"
        Text="${Warehouse.PickList.CreatePickList}" CssClass="button2" FunctionId="ShipOrder" />
</div>
