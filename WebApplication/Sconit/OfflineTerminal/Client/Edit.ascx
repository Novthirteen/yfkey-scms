<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_Client_Edit" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Client" runat="server" DataSourceID="ODS_Client" DefaultMode="Edit"
        Width="100%" DataKeyNames="ClientId" OnDataBound="FV_Client_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.Client.UpdateClient}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblClientId" runat="server" Text="${Common.Business.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbClientId" runat="server" Text='<%# Bind("ClientId") %>' ReadOnly="true" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlDescription" runat="server" Text="${Common.Business.Description}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbDescription" runat="server" Text='<%# Bind("Description") %>' CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="${MasterData.Client.Name.Empty}"
                                Display="Dynamic" ControlToValidate="tbDescription" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${Common.Business.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="tbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                            <asp:Button ID="btnSave" runat="server" CommandName="Update" Text="${Common.Button.Save}" CssClass="button2"
                                ValidationGroup="vgSave" />
                            <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}" CssClass="button2"
                                OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                            <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="button2"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Client" runat="server" TypeName="com.Sconit.Web.ClientMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Client" UpdateMethod="UpdateClient"
    OnUpdated="ODS_Client_Updated" SelectMethod="LoadClient" OnUpdating="ODS_Client_Updating"
    DeleteMethod="DeleteClient" OnDeleted="ODS_Client_Deleted">
    <SelectParameters>
        <asp:Parameter Name="ClientId" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
