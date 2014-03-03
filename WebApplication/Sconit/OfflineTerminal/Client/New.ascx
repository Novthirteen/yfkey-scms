<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_Client_New" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Client" runat="server" DataSourceID="ODS_Client" DefaultMode="Insert"
        Width="100%" DataKeyNames="ClientId" OnDataBinding="FV_Client_OnDataBinding">
        <InsertItemTemplate>
            <fieldset>
                <legend>${MasterData.Client.NewClient}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblClientId" runat="server" Text="${Common.Business.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbClientId" runat="server" Text='<%# Bind("ClientId") %>' CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rtvCode" runat="server" ErrorMessage="${MasterData.Client.ClientId.Empty}"
                                Display="Dynamic" ControlToValidate="tbClientId" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvClientId" runat="server" ControlToValidate="tbClientId" ErrorMessage="${Common.Code.Exist}"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="checkClient" />
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
                            <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="${Common.Button.Save}" CssClass="button2"
                                ValidationGroup="vgSave" />
                            <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="button2"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </InsertItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Client" runat="server" TypeName="com.Sconit.Web.ClientMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Client" InsertMethod="CreateClient"
    OnInserted="ODS_Client_Inserted" OnInserting="ODS_Client_Inserting">   
</asp:ObjectDataSource>
