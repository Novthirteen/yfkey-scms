<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_Item_ItemRef_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="floatdiv">
    <fieldset id="fldInsert" runat="server">
        <legend>${MasterData.ItemReference.NewItemReference}</legend>
        <asp:FormView ID="FV_ItemReference" runat="server" DataSourceID="ODS_FV_ItemReference"
            DefaultMode="Insert">
            <InsertItemTemplate>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlItemCode" runat="server" Text="${MasterData.Item.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:Label ID="lblItemCode" runat="server" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltllPartyType" runat="server" Text="${MasterData.ItemReference.Party.Type}:" />
                        </td>
                        <td class="td02">
                            <asp:DropDownList ID="ddlPartyType" runat="server">
                                <asp:ListItem Selected="True" />
                                <asp:ListItem Text="${MasterData.Customer.Customer}" Value="Customer" />
                                <asp:ListItem Text="${MasterData.Supplier.Supplier}" Value="Supplier" />
                            </asp:DropDownList>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlPartyCode" runat="server" Text="${MasterData.ItemReference.Party.Code}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbPartyCode" runat="server" Visible="true" DescField="Name" ValueField="Code"
                                ServicePath="PartyMgr.service" ServiceMethod="GetAllParty" MustMatch="true" Width="250" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlReferenceCode" runat="server" Text="${MasterData.ItemReference.ReferenceCode}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbReferenceCode" runat="server" Text='<%# Bind("ReferenceCode") %>'
                                CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rtvReferenceCode" runat="server" ErrorMessage="${MasterData.ItemReference.Required.ReferenceCode}"
                                Display="Dynamic" ControlToValidate="tbReferenceCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvReferenceCode" runat="server" ControlToValidate="tbReferenceCode"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlDescription" runat="server" Text="${MasterData.ItemReference.Description}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbDescription" runat="server" Text='<%# Bind("Description") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlRemark" runat="server" Text="${MasterData.ItemReference.Remark}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbRemark" runat="server" Text='<%# Bind("Remark") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlIsActive" runat="server" Text="${Common.Business.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="tbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' />
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
<asp:ObjectDataSource ID="ODS_FV_ItemReference" runat="server" TypeName="com.Sconit.Web.ItemReferenceMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.ItemReference" InsertMethod="CreateItemReference"
    OnInserted="ODS_ItemReference_Inserted" OnInserting="ODS_ItemReference_Inserting">
</asp:ObjectDataSource>
