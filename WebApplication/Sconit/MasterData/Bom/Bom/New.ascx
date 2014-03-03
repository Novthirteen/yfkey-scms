<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_Bom_Bom_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Bom" runat="server" DataSourceID="ODS_Bom" DefaultMode="Insert"
        Width="100%" DataKeyNames="Code">
        <InsertItemTemplate>
            <fieldset>
                <legend>${MasterData.Bom.AddBom}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Common.Business.Code}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbCode" runat="server" Text='<%#Bind("Code") %>' DescField="Description"
                                ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetAllItem" CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="${MasterData.Bom.WarningMessage.CodeEmpty}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvCode" runat="server" ControlToValidate="tbCode" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
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
                            <asp:CustomValidator ID="cvUom" runat="server" ControlToValidate="tbUom" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblRegion" runat="server" Text="${Common.Business.Region}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbRegion" runat="server" DescField="Name" ValueField="Code" Width="200"
                                ServicePath="RegionMgr.service" ServiceMethod="GetRegion" MustMatch="true" />
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
                <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="${Common.Button.Save}"
                    CssClass="save" ValidationGroup="vgSave" />
                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                    CssClass="back" />
                    </div>
            </div>
        </InsertItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Bom" runat="server" TypeName="com.Sconit.Web.BomMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Bom" InsertMethod="CreateBom"
    OnInserted="ODS_Bom_Inserted" OnInserting="ODS_Bom_Inserting"></asp:ObjectDataSource>
