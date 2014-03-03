<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ListBinding.ascx.cs" Inherits="MasterData_OrderBinding_ListBinding" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" AllowSorting="True" AllowPaging="false"
            Width="100%" OnRowDataBound="GV_List_RowDataBound" OnRowCommand="GV_List_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.Order.Binding.BindedFlow.Code}" SortExpression="BindedFlow.Code">
                    <ItemTemplate>
                        <asp:Label ID="lbFlow" Text='<% #Bind("BindedFlow.Code")%>' runat="server" />
                        <uc3:textbox ID="tbFlow" runat="server" Width="250" DescField="Description" ValueField="Code"
                            ServicePath="FlowMgr.service" CssClass="inputRequired" ServiceMethod="GetAllFlow"
                            Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.Binding.BindingType}" SortExpression="BindingType">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblBindingType" runat="server" Code="BindingType" Value='<%# Bind("BindingType") %>' />
                        <asp:DropDownList ID="ddlBindingType" runat="server" Visible="false" DataTextField="Description" DataValueField="Value"  />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.Binding.BindedOrderHead.OrderNo}"
                    SortExpression="BindedOrderNo">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "BindedOrderNo")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.Binding.Remark}" SortExpression="Remark">
                    <ItemTemplate>
                        <asp:Label ID="lbRemark" Text='<% #Bind("Remark")%>' runat="server" />
                        <asp:TextBox ID="tbRemark" runat="server"  Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.GridView.Action}" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnAdd" runat="server" CommandArgument='<%# Container.DataItemIndex %>'
                            Text="${Common.Button.New}" CommandName="AddBinding">
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.Delete}" CommandName="DeleteBinding">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
<div class="tablefooter">
    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
        CssClass="button2" />
</div>
