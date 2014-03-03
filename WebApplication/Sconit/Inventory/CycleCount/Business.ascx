<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Business.ascx.cs" Inherits="Inventory_CycleCount_Business" %>
<%@ Register Src="~/Inventory/CycleCount/Edit.ascx" TagName="Edit" TagPrefix="uc" %>
<%@ Register Src="~/Controls/ItemList.ascx" TagName="ItemList" TagPrefix="uc" %>
<%@ Register Src="~/Controls/HuList.ascx" TagName="HuList" TagPrefix="uc" %>
<fieldset>
    <legend>${CycCnt.CycleCountInfo}</legend>
    <table class="mtable">
        <uc:Edit ID="ucEdit" runat="server" />
        <tr>
            <td colspan="3" />
            <td>
                <div class="buttons">
                    <asp:Button ID="btnEdit" runat="server" Text="${Common.Button.Edit}" OnClick="btnEdit_Click"
                        CssClass="edit" />
                    <asp:Button ID="btnSubmit" runat="server" Text="${Common.Button.Submit}" OnClick="btnSubmit_Click"
                        CssClass="apply" />
                    <asp:Button ID="btnSave" runat="server" Text="${Common.Button.Save}" OnClick="btnSave_Click"
                        Visible="false" CssClass="apply" />
                    <asp:Button ID="btnScanHu" runat="server" Text="${Common.Business.HuId}" OnClick="btnScanHu_Click"
                        Visible="true" CssClass="add" />
                    <asp:Button ID="btnCancel" runat="server" Text="${Common.Button.Cancel}" OnClick="btnCancel_Click"
                        Visible="false" CssClass="apply" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        CssClass="back" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
<uc:ItemList ID="ucItemList" runat="server" OnItemInput="ucItemList_ItemInput" OnQtyChanged="ucItemList_QtyChanged" />
<uc:HuList ID="ucHuList" runat="server" Visible="false" OnHuInput="ucHuList_HuInput"
    OnClosed="ucHuList_Closed" />
