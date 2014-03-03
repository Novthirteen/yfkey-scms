<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Mes_Shelf_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Shelf" runat="server" DataSourceID="ODS_Shelf" DefaultMode="Insert"
        Width="100%" DataKeyNames="Code">
        <InsertItemTemplate>
            <fieldset>
                <legend>${Mes.Shelf.AddShelf}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Mes.Shelf.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<% #Bind("Code") %>' CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="${Mes.Shelf.Code}${Common.String.Empty}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvCode" runat="server" ControlToValidate="tbCode" Display="Dynamic" ErrorMessage="${Mes.Shelf.Code.Exists}"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblProductLine" runat="server" Text="${Mes.Shelf.ProductLine}:" />
                        </td>
                        <td class="ttd02">
                            <uc3:textbox ID="tbProductLine" runat="server" Visible="true" DescField="Description" ValueField="Code"
                                Width="250" ServicePath="FlowMgr.service" ServiceMethod="GetProductionFlow" />
                            <asp:RequiredFieldValidator ID="rfvProductLine" runat="server" ErrorMessage="${Mes.Shelf.ProductLine}${Common.String.Empty}"
                                Display="Dynamic" ControlToValidate="tbProductLine" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                     <tr>
                        <td class="td01">
                            <asp:Literal ID="lblTagNo" runat="server" Text="${Mes.Shelf.TagNo}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbTagNo" runat="server" Text='<% #Bind("TagNo") %>' CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvTagNo" runat="server" ErrorMessage="${Mes.Shelf.TagNo}${Common.String.Empty}"
                                Display="Dynamic" ControlToValidate="tbTagNo" ValidationGroup="vgSave" />
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblCapacity" runat="server" Text="${Mes.Shelf.Capacity}:" />
                        </td>
                        <td class="ttd02">
                            <asp:textbox ID="tbCapacity" runat="server" Text='<% #Bind("Capacity") %>' CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvCapacity" runat="server" ErrorMessage="${Mes.Shelf.Capacity}${Common.String.Empty}"
                                Display="Dynamic" ControlToValidate="tbCapacity" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${Common.Business.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                        <td>
                            <div class="buttons">
                                <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="${Common.Button.Save}"
                                    CssClass="apply" ValidationGroup="vgSave" />
                                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                                    CssClass="back" />
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </InsertItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Shelf" runat="server" TypeName="com.Sconit.Web.ShelfMgrProxy"
    InsertMethod="CreateShelf" DataObjectTypeName="com.Sconit.Entity.Mes.Shelf" OnInserted="ODS_Shelf_Inserted"
    OnInserting="ODS_Shelf_Inserting"></asp:ObjectDataSource>
