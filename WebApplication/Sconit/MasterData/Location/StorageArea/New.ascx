<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_Location_StorageArea_New" %>
<div id="floatdiv">
    <fieldset id="fldInsert" runat="server">
        <legend>${MasterData.Location.Area.New}</legend>
        <asp:FormView ID="FV_StorageArea" runat="server" DataSourceID="ODS_FV_StorageArea"
            DefaultMode="Insert">
            <InsertItemTemplate>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlLocationCode" runat="server" Text="${MasterData.Location.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbLocationCode" runat="server" ReadOnly="true" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlAreaCode" runat="server" Text="${MasterData.Location.Area.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbAreaCode" runat="server" CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvAreaCode" runat="server" ErrorMessage="${MasterData.Location.Area.Required.Code}"
                                Display="Dynamic" ControlToValidate="tbAreaCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvAreaCode" runat="server" ControlToValidate="tbAreaCode"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlAreaDescrition" runat="server" Text="${MasterData.Location.Area.Description}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbAreaDescription" runat="server" />
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
                                <asp:Button ID="btnNew" runat="server" CommandName="Insert" Text="${Common.Button.Save}"
                                    CssClass="apply" ValidationGroup="vgSave" />
                                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                                    CssClass="back" />
                            </div>
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
        </asp:FormView>
    </fieldset>
</div>
<asp:ObjectDataSource ID="ODS_FV_StorageArea" runat="server" TypeName="com.Sconit.Web.StorageAreaMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.StorageArea" InsertMethod="CreateStorageArea"
    OnInserting="ODS_StorageArea_Inserting"></asp:ObjectDataSource>
