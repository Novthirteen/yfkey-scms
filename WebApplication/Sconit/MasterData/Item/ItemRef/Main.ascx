<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MasterData_Item_ItemRef_Main" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="New.ascx" TagName="New" TagPrefix="uc2" %>

<uc2:New ID="ucNew" runat="server" Visible="false" />
<fieldset id="fldGV" runat="server" >
    <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
        OnRowCancelingEdit="GV_List_RowCancelingEdit" OnRowEditing="GV_List_RowEditing"
        OnRowUpdating="GV_List_RowUpdating" OnRowDeleting="GV_List_RowDeleting" OnRowDeleted="GV_List_RowDeleted">
        <Columns>
            <asp:BoundField DataField="Id" Visible="false" />
            <asp:TemplateField HeaderText="${MasterData.Item.Code}" >
                <ItemTemplate>
                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item.Code") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Item.Description}">
                <ItemTemplate>
                    <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("Item.Description") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.ItemReference.Party.Code}">
                <ItemTemplate>
                    <asp:Label ID="lblPartyCode" runat="server" Text='<%# Eval("Party.Code") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.ItemReference.Party.Name}">
                <ItemTemplate>
                    <asp:Label ID="lblPartyName" runat="server" Text='<%# Eval("Party.Name") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.ItemReference.ReferenceCode}">
                <ItemTemplate>
                    <asp:Label ID="lblItemReferenceCode" runat="server" Text='<%# Eval("ReferenceCode") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.ItemReference.Description}" ItemStyle-Width="160px">
                <ItemTemplate>
                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="tbDescription" runat="server" Text='<%# Bind("Description") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.ItemReference.Remark}" ItemStyle-Width="160px">
                <ItemTemplate>
                    <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="tbRemark" runat="server" Text='<%# Bind("Remark") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.Business.IsActive}">
                <ItemTemplate>
                    <asp:CheckBox ID="cbActive1" runat="server" Checked='<%# Eval("IsActive") %>' Enabled="false"/>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="cbActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="true" ItemStyle-Width="100px"
                HeaderText="${Common.GridView.Action}" DeleteText="&lt;span onclick=&quot;JavaScript:return confirm('${Common.Delete.Confirm}?')&quot;&gt;${Common.Button.Delete}&lt;/span&gt;">
            </asp:CommandField>
        </Columns>
    </cc1:GridView>
    <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
    </cc1:GridPager>
    <div class="tablefooter">
        <asp:Button ID="btnInsert" runat="server" Text="${Common.Button.New}" OnClick="btnInsert_Click"
            CssClass="button2" />
        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
            CssClass="button2" />
    </div>
</fieldset>

