<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Warehouse_Pickup_Edit" %>
<%@ Register Src="~/Warehouse/PutAway/List.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/UpdateProgress.ascx" TagName="UpdateProgress" TagPrefix="uc2" %>
<fieldset>
    <asp:UpdatePanel ID="UP_GV_List" runat="server">
        <ContentTemplate>
            <uc2:List ID="ucList" runat="server" />
            <uc2:UpdateProgress ID="ucUpdateProgress" runat="server" />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <table class="mtable">
                <tr>
                    <td class="td01">
                        <asp:Literal ID="ltlScanBarcode" runat="server" Text="<%$Resources:Language,ScanBarcode%>" />:
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbScanBarcode" runat="server" AutoPostBack="true" OnTextChanged="tbScanBarcode_TextChanged" />
                    </td>
                    <td colspan="2" />
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="tablefooter">
        <asp:Button ID="btnPickup" runat="server" Text="<%$Resources:Language,Pickup%>" OnClick="btnPickup_Click"
            CssClass="button2" />
    </div>
</fieldset>
