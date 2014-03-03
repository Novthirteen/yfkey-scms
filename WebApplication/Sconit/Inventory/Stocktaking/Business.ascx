<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Business.ascx.cs" Inherits="Inventory_Stocktaking_Business" %>
<%@ Register Src="~/Inventory/Stocktaking/Edit.ascx" TagName="Edit" TagPrefix="uc" %>
<%@ Register Src="ItemList.ascx" TagName="ItemList" TagPrefix="uc" %>
<%@ Register Src="StorageBinList.ascx" TagName="StorageBinList" TagPrefix="uc" %>
<%@ Register Src="CycleCountResultList.ascx" TagName="CycleCountResultList" TagPrefix="uc" %>
<fieldset>
    <legend>${CycCnt.CycleCountInfo}</legend>
    <table class="mtable">
        <uc:Edit ID="ucEdit" runat="server" />
        <tr>
            <td colspan="1" />
            <td colspan="2" >
                <asp:FileUpload ID="fileUpload" ContentEditable="false" runat="server" />
                <asp:Button ID="btnImport" runat="server" Text="${Common.Button.Import}" OnClick="btnImport_Click"
                    CssClass="apply" />
                <asp:HyperLink ID="hlTemplate" runat="server" Text="${Common.Business.ClickToDownload}"
                    NavigateUrl="~/Reports/Templates/ExcelTemplates/CycleCount.xls"></asp:HyperLink>
            </td>
            <td>
                <div class="buttons">
                    <asp:Button ID="btnSave" runat="server" Text="${Common.Button.Save}" OnClick="btnSave_Click"
                        CssClass="apply" />
                    <asp:Button ID="btnSubmit" runat="server" Text="${Common.Button.Submit}" OnClick="btnSubmit_Click"
                        CssClass="apply" />
                    <asp:Button ID="btnComplete" runat="server" Text="${Common.Button.Complete}" OnClick="btnComplete_Click"
                        CssClass="apply" />
                    <asp:Button ID="btnDelete" runat="server" Text="${Common.Button.Delete}" OnClick="btnDelete_Click"
                        CssClass="apply" />
                    <asp:Button ID="btnCancel" runat="server" Text="${Common.Button.Cancel}" OnClick="btnCancel_Click"
                        CssClass="apply" />
                    <asp:Button ID="btnPrint" runat="server" Text="${Common.Button.Print}" OnClick="btnPrint_Click"
                        CssClass="apply" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        CssClass="back" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
<uc:StorageBinList ID="ucStorageBinList" runat="server" />
<uc:ItemList ID="ucItemList" runat="server" />
<%-- 
<uc:CycleCountResultList ID="ucCycleCountResultList" runat="server"  />
--%>
