<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RolePermissionList.ascx.cs"
    Inherits="Security_Role_RolePermissionList" %>
<fieldset>
    <legend>
        <asp:Literal ID="lblCode" runat="server" Text="${Security.Role.CurrentRole}:" />
        <asp:Literal ID="lbCode" runat="server" />
    </legend>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" OnDataBound="GV_List_OnDataBound">
            <Columns>
                <asp:TemplateField HeaderText="${Security.Permission.Category.Type}" SortExpression="Category.Type">
                    <ItemTemplate>
                        <asp:Label ID="lblCategoryType" runat="server" Text='<%# Eval("Category.Type") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <Columns>
                <asp:TemplateField HeaderText="${Security.Permission.Category.Description}" SortExpression="Category.Id">
                    <ItemTemplate>
                        <asp:Label ID="lblCategoryDescription" runat="server" Text='<%# Eval("Category.Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <Columns>
                <asp:TemplateField HeaderText="${Security.Permission.Description}" SortExpression="Description">
                    <ItemTemplate>
                        <asp:Label ID="lblPermissionDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
        <asp:Label runat="server" ID="lblResult" Text="${Common.GridView.NoRecordFound}"
            Visible="false" />
    </div>
    <div class="buttons tablefooter"><asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="back" /></div>
</fieldset>
