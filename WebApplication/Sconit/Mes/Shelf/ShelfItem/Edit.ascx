<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Mes_Shelf_ShelfItem_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div id="floatdiv">
    <asp:FormView ID="FV_ShelfItem" runat="server" DataSourceID="ODS_ShelfItem"
        DefaultMode="Edit" Width="100%" DataKeyNames="Id" OnDataBound="FV_ShelfItem_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.ShelfItem.UpdateShelfItem}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblShelf" runat="server" Text="${Mes.Shelf.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbShelf" runat="server" ReadOnly="true" />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                    </tr>
                   
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblItem" runat="server" Text="${MasterData.Item.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbItem" runat="server" ReadOnly="true" />
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
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="back"/>
                    </div>
                </div>
            </fieldset>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_ShelfItem" runat="server" TypeName="com.Sconit.Web.ShelfItemMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.ShelfItem" UpdateMethod="UpdateShelfItem"
    OnUpdated="ODS_ShelfItem_Updated" OnUpdating="ODS_ShelfItem_Updating"
    DeleteMethod="DeleteShelfItem" OnDeleted="ODS_ShelfItem_Deleted"
    SelectMethod="LoadShelfItem">
    <SelectParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </DeleteParameters>
</asp:ObjectDataSource>
