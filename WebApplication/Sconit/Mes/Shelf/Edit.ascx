<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Mes_Shelf_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Shelf" runat="server" DataSourceID="ODS_Shelf" DefaultMode="Edit"
        Width="100%" DataKeyNames="Code" OnDataBound="FV_Shelf_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${Mes.Shelf.UpdateShelf}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Mes.Shelf.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<% #Bind("Code") %>' ReadOnly="true" />
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblProductLine" runat="server" Text="${Mes.Shelf.ProductLine}:" />
                        </td>
                        <td class="ttd02">
                            <uc3:textbox ID="tbProductLine" runat="server" Visible="true" DescField="Description"
                                ValueField="Code" Width="250" ServicePath="FlowMgr.service" ServiceMethod="GetProductionFlow" />
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
                            <asp:TextBox ID="tbCapacity" runat="server" Text='<% #Bind("Capacity") %>' CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvCapacity" runat="server" ErrorMessage="${Mes.Shelf.Capacity}${Common.String.Empty}"
                                Display="Dynamic" ControlToValidate="tbCapacity" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCurrentCartons" runat="server" Text="${Mes.Shelf.CurrentCartons}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCurrentCartons" runat="server" Text='<% #Bind("CurrentCartons") %>'
                                ReadOnly="true" />
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblOriginalCartonNo" runat="server" Text="${Mes.Shelf.OriginalCartonNo}:" />
                        </td>
                        <td class="ttd02">
                            <asp:TextBox ID="tbOriginalCartonNo" runat="server" Text='<% #Bind("OriginalCartonNo") %>'
                                ReadOnly="true" />
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
                </table>
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
            </fieldset>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Shelf" runat="server" TypeName="com.Sconit.Web.ShelfMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.Mes.Shelf" SelectMethod="LoadShelf" UpdateMethod="UpdateShelf"
    OnUpdated="ODS_Shelf_Updated" OnUpdating="ODS_Shelf_Updating" OnDeleted="ODS_Shelf_Deleted">
    <SelectParameters>
        <asp:Parameter Name="Code" Type="String" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="Code" Type="String" />
    </DeleteParameters>
</asp:ObjectDataSource>
