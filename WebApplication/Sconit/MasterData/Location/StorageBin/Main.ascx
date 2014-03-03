<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MasterData_Location_StorageBin_Main" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Src="New.ascx" TagName="New" TagPrefix="uc2" %>
<div id="divList" runat="server">
    <fieldset>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblLocationCode" runat="server" Text="${MasterData.Location.Code}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbLocationCode" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td class="td01">
                    <asp:Literal ID="lblAreaCode" runat="server" Text="${MasterData.Location.Area.Code}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbAreaCode1" runat="server" ReadOnly="true" Visible="false" />
                    <uc3:textbox ID="tbAreaCode" runat="server" Width="250" DescField="Description" ValueField="Code"
                        ServicePath="StorageAreaMgr.service" ServiceMethod="GetStorageArea" ServiceParameter="string:#tbLocationCode"
                        CssClass="inputRequired" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                        CssClass="button2" />
                    <asp:Button ID="btnNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click"
                        CssClass="button2" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        CssClass="button2" />
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <asp:GridView ID="GV_Bin" runat="server" AutoGenerateColumns="false" OnRowEditing="GV_Bin_RowEditing"
            OnRowUpdating="GV_Bin_RowUpdating" OnRowDeleting="GV_Bin_RowDeleting" OnRowCancelingEdit="GV_Bin_RowCancelingEdit">
            <Columns>
                <asp:TemplateField HeaderText="${Common.Business.Code}" ItemStyle-Width="80px">
                    <ItemTemplate>
                        <asp:Literal ID="ltlCode" runat="server" Text='<%# Eval("Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Description}" ItemStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbDescription" runat="server" Text='<%# Bind("Description") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.IsActive}" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbActive1" runat="server" Checked='<%# Eval("IsActive") %>' Enabled="false" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:CheckBox ID="cbActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="true" ItemStyle-Width="100px" EditText="${Common.Button.Edit}" UpdateText="${Common.Button.Save}" CancelText="${Common.Button.Cancel}"
                    HeaderText="${Common.GridView.Action}" DeleteText="&lt;span onclick=&quot;JavaScript:return confirm('${Common.Delete.Confirm}?')&quot;&gt;${Common.Button.Delete}&lt;/span&gt;">
                </asp:CommandField>
            </Columns>
        </asp:GridView>
        <asp:Literal ID="ltlMessage" runat="server" Text="${Common.GridView.NoRecordFound}" Visible="false"/>
    </fieldset>
</div>
<uc2:New ID="ucNew" runat="server" Visible="false" />
