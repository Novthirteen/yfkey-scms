<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_Location_StorageArea_Edit" %>
<%@ Register Src="../StorageBin/Main.ascx" TagName="Bin" TagPrefix="uc2" %>
<fieldset id="fldInsert" runat="server">
    <legend>${MasterData.Location.Area.Edit}</legend>
    <asp:FormView ID="FV_StorageArea" runat="server" DataSourceID="ODS_FV_StorageArea"
        DefaultMode="Edit" DataKeyNames="Code">
        <EditItemTemplate>
            <table class="mtable">
                <tr>
                    <td class="td01">
                        <asp:Literal ID="ltlLocationCode" runat="server" Text="${MasterData.Location.Code}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbLocationCode" runat="server" ReadOnly="true" Text='<%# Eval("Location.Code") %>' />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="ltlAreaCode" runat="server" Text="${MasterData.Location.Area.Code}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbAreaCode" runat="server" ReadOnly="true" Text='<%# Bind("Code") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="ltlAreaDescrition" runat="server" Text="${MasterData.Location.Area.Description}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbAreaDescription" runat="server" Text='<%# Bind("Description") %>' />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="ltlIsActive" runat="server" Text="${Common.Business.IsActive}:" />
                    </td>
                    <td class="td02">
                        <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    </td>
                    <td>
                        <div class="buttons">
                            <asp:Button ID="btnSave" runat="server" CommandName="Update" Text="${Common.Button.Save}"
                                CssClass="apply" ValidationGroup="vgSave" />
                            <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}"
                                CssClass="delete" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                            <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                                CssClass="back" />
                        </div>
                    </td>
                </tr>
            </table>
        </EditItemTemplate>
    </asp:FormView>
</fieldset>
<asp:ObjectDataSource ID="ODS_FV_StorageArea" runat="server" TypeName="com.Sconit.Web.StorageAreaMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.StorageArea" UpdateMethod="UpdateStorageArea"
    OnUpdated="ODS_StorageArea_Updated" SelectMethod="LoadStorageArea" OnUpdating="ODS_StorageArea_Updating"
    DeleteMethod="DeleteStorageArea" OnDeleted="ODS_StorageArea_Deleted">
    <SelectParameters>
        <asp:Parameter Name="Code" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
