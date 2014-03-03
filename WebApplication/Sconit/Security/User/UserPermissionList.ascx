<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserPermissionList.ascx.cs"
    Inherits="Security_User_UserPermissionList" %>

<fieldset>
<legend><asp:Literal ID="lblCode" runat="server" Text="${Security.User.CurrentUser}:" /> <asp:Literal ID="lbCode" runat="server" /></legend>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" >
            <Columns>
                <asp:TemplateField HeaderText="${Security.Permission.Category.Type}" SortExpression="Category.Type">
                    <ItemTemplate>
                        <asp:Label ID="lblCategoryType" runat="server" Text='<%# Bind("Category.Type") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <Columns>
                <asp:TemplateField HeaderText="${Security.Permission.Category.Description}" SortExpression="Category.Id">
                    <ItemTemplate>
                        <asp:Label ID="lblCategoryDescription" runat="server" Text='<%# Bind("Category.Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <Columns>
                <asp:TemplateField HeaderText="${Security.Permission.Description}" SortExpression="Description">
                    <ItemTemplate>
                        <asp:Label ID="lblPermissionDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="tablefooter"><asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="button2"/></div>
</fieldset>
