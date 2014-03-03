<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Security_Role_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Role" runat="server" DataSourceID="ODS_Role" DefaultMode="Edit"
        Width="100%" DataKeyNames="Code" OnDataBound="FV_Role_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${Security.Role.UpdateRole}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Security.Role.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbCode" runat="server" Text='<%# Bind("Code") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblDescription" runat="server" Text="${Security.Role.Description}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbDescription" runat="server" Text='<%# Bind("Description") %>'
                                Width="250"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                        <td>
                            <div class="buttons">
                                <asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="${Common.Button.Save}"
                                    ValidationGroup="vgSave" CssClass="apply" />
                                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}"
                                    OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" CssClass="delete" />
                                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                                    CssClass="back" />
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Role" runat="server" TypeName="com.Sconit.Web.RoleMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Role" UpdateMethod="UpdateRole"
    OnUpdated="ODS_Role_Updated" OnUpdating="ODS_Role_Updating" DeleteMethod="DeleteRole"
    OnDeleted="ODS_Role_Deleted" SelectMethod="LoadRole">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="code" Type="String" />
    </DeleteParameters>
</asp:ObjectDataSource>
