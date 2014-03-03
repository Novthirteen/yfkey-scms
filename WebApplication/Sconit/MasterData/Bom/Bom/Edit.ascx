<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_Bom_Bom_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Bom" runat="server" DataSourceID="ODS_Bom" DefaultMode="Edit"
        Width="100%" DataKeyNames="Code" OnDataBound="FV_Bom_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.Bom.UpdateBom}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Common.Business.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:Label ID="lbCode" runat="server" Text='<%#Bind("Code") %>'></asp:Label>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblDescription" runat="server" Text="${Common.Business.Description}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbDescription" runat="server" Text='<%#Bind("Description") %>' Width="250" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblUom" runat="server" Text="${Common.Business.Uom}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbUom" runat="server" DescField="Name" ValueField="Code" ServicePath="UomMgr.service"
                                ServiceMethod="GetAllUom" />
                            <asp:RequiredFieldValidator ID="rfvUom" runat="server" ErrorMessage="${MasterData.Bom.WarningMessage.UomEmpty}"
                                Display="Dynamic" ControlToValidate="tbUom" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvUom" runat="server" ControlToValidate="tbUom" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblRegion" runat="server" Text="${Common.Business.Region}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbRegion" runat="server" Enabled="false" />
                            <asp:CustomValidator ID="cvRegion" runat="server" ControlToValidate="tbRegion" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${Common.Business.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div class="tablefooter">
                <div class="buttons">
                    <asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="${Common.Button.Save}"
                        CssClass="apply" ValidationGroup="vgSave" />
                    <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}"
                        CssClass="delete" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        CssClass="back" />
                </div>
            </div>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Bom" runat="server" TypeName="com.Sconit.Web.BomMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Bom" UpdateMethod="UpdateBom"
    OnUpdated="ODS_Bom_Updated" OnUpdating="ODS_Bom_Updating" DeleteMethod="DeleteBom"
    OnDeleted="ODS_Bom_Deleted" SelectMethod="LoadBom">
    <SelectParameters>
        <asp:Parameter Name="Code" Type="String" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="Code" Type="String" />
    </DeleteParameters>
</asp:ObjectDataSource>
