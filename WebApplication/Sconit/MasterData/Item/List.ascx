<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MasterData_Item_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Code"
            ShowSeqNo="true" AllowSorting="true" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.Item.Image}" SortExpression="ImageUrl" ItemStyle-Width="150px">
                    <ItemTemplate>
                       <asp:Image ImageUrl='<%# DataBinder.Eval(Container.DataItem, "ImageUrl")%>' runat="server" ID="imgImageUrl"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Code" HeaderText="${Common.Business.Code}" SortExpression="Code" />
                <asp:TemplateField HeaderText="${Common.Business.Description}" SortExpression="Desc1">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Item.Type}" SortExpression="Type">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblType" runat="server" Code="ItemType" Value='<%# DataBinder.Eval(Container.DataItem, "Type")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UnitCount" DataFormatString="{0:0.########}" HeaderText="${MasterData.Item.Uc}"
                    SortExpression="UnitCount" />
                <asp:TemplateField HeaderText="${MasterData.Item.Uom}" SortExpression="Uom.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Uom.Code") %> [<%# DataBinder.Eval(Container.DataItem, "Uom.Description")%>]
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Item.Location}" SortExpression="Location">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Location.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Item.Bom}" SortExpression="Bom">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Bom.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Item.Routing}" SortExpression="Routing">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Routing.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Memo" HeaderText="${MasterData.Item.Memo}" SortExpression="Memo" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="${MasterData.Item.IsActive}"
                    SortExpression="IsActive" />
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>'
                            Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click" />
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>'
                            Text="${Common.Button.Delete}" OnClick="lbtnDelete_Click" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
