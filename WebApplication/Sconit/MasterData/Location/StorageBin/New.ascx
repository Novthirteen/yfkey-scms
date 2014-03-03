<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_Location_StorageBin_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <legend>${MasterData.Location.Bin.New}</legend>
    <asp:FormView ID="FV_StorageBin" runat="server" DataSourceID="ODS_FV_StorageBin"
        DefaultMode="Insert">
        <InsertItemTemplate>
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
                            ServicePath="StorageAreaMgr.service" ServiceMethod="GetStorageArea" MustMatch="true"
                            ServiceParameter="string:#tbLocationCode" CssClass="inputRequired" />
                        <asp:RequiredFieldValidator ID="rfvAreaCode" runat="server" ErrorMessage="${MasterData.Location.Area.Required.Code}"
                            Display="Dynamic" ControlToValidate="tbAreaCode" ValidationGroup="vgBinSave" />
                        <asp:CustomValidator ID="cvAreaCode" runat="server" ControlToValidate="tbAreaCode"
                            ErrorMessage="*" Display="Dynamic" ValidationGroup="vgBinSave" OnServerValidate="CV_ServerValidate" />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="ltlBinCode" runat="server" Text="${MasterData.Location.Bin.Code}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbBinCode" runat="server" CssClass="inputRequired" />
                        <asp:RequiredFieldValidator ID="rfvBinCode" runat="server" ErrorMessage="${MasterData.Location.Bin.Required.Code}"
                            Display="Dynamic" ControlToValidate="tbBinCode" ValidationGroup="vgBinSave" />
                        <asp:CustomValidator ID="cvBinCode" runat="server" ControlToValidate="tbBinCode"
                            ErrorMessage="*" Display="Dynamic" ValidationGroup="vgBinSave" OnServerValidate="CV_ServerValidate" />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="ltlBinDescrition" runat="server" Text="${MasterData.Location.Bin.Description}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbBinDescription" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="ltlIsActive" runat="server" Text="${Common.Business.IsActive}:" />
                    </td>
                    <td class="td02">
                        <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' />
                    </td>
                    <td />
                    <td>
                        <div class="buttons">
                            <asp:Button ID="btnNew" runat="server" CommandName="Insert" Text="${Common.Button.Create}"
                                CssClass="apply" ValidationGroup="vgBinSave" />
                            <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                                CssClass="back" />
                        </div>
                    </td>
                </tr>
            </table>
        </InsertItemTemplate>
    </asp:FormView>
</fieldset>
<asp:ObjectDataSource ID="ODS_FV_StorageBin" runat="server" TypeName="com.Sconit.Web.StorageBinMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.StorageBin" InsertMethod="CreateStorageBin"
    OnInserting="ODS_StorageBin_Inserting" OnInserted="ODS_StorageBin_Inserted">
</asp:ObjectDataSource>
