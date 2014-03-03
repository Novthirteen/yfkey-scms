<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MasterData_Item_ItemKit_Main" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <legend>${MasterData.Item.ParentKit}</legend>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Item.Code}:" />
            </td>
            <td class="td02">
                <asp:Literal ID="tbCode" runat="server" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlDesc" runat="server" Text="${MasterData.Item.Description}:" />
            </td>
            <td class="td02">
                <asp:Literal ID="tbDesc" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlUom" runat="server" Text="${MasterData.Item.Uom}:" />
            </td>
            <td class="td02">
                <asp:Literal ID="tbUom" runat="server" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlUc" runat="server" Text="${MasterData.Item.Uc}:" />
            </td>
            <td class="td02">
                <asp:Literal ID="tbUc" runat="server" />
            </td>
        </tr>
    </table>
</fieldset>
<fieldset>
    <legend>${MasterData.Item.ItemKit.Detail}</legend>
    <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="ParentItem,ChildItem"
        ShowSeqNo="true" AllowSorting="false">
        <Columns>
            <asp:TemplateField HeaderText="${MasterData.Item.Code}" SortExpression="ChildItem.Code">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ChildItem.Code")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Item.Description}" SortExpression="ChildItem.Desc1">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ChildItem.Description")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Item.Type}" SortExpression="ChildItem.Type">
                <ItemTemplate>
                    <cc1:codemstrlabel id="lblType" runat="server" code="ItemType" value='<%# DataBinder.Eval(Container.DataItem, "ChildItem.Type")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Item.Uom}" SortExpression="ChildItem.Uom.Code">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ChildItem.Uom.Code")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Item.Uc}" SortExpression="ChildItem.UnitCount">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ChildItem.UnitCount","{0:0.########}")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Qty" HeaderText="${MasterData.ItemKit.Qty}" SortExpression="Qty" DataFormatString="{0:0.########}" />
            <asp:CheckBoxField DataField="IsActive" HeaderText="${MasterData.Item.IsActive}"
                SortExpression="IsActive" />
            <asp:TemplateField HeaderText="${Common.GridView.Action}">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ChildItem.Code") %>'
                        Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click" />
                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ChildItem.Code") %>'
                        Text="${Common.Button.Delete}" OnClick="lbtnDelete_Click" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </cc1:GridView>
    <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
    </cc1:GridPager>
    <div class="tablefooter">
        <asp:Button ID="btNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click" CssClass="button2" />
        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="button2" />
    </div>
</fieldset>
