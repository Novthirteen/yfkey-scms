<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShipItemList.ascx.cs"
    Inherits="Order_OrderIssue_ShipItemList" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Hu/List.ascx" TagName="HuList" TagPrefix="uc2" %>
<%@ Register Src="~/Hu/Transformer.ascx" TagName="Transformer" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/UpdateProgress.ascx" TagName="UpdateProgress" TagPrefix="uc2" %>
<fieldset>
    <legend>${MasterData.Order.OrderDetail.Distribution}</legend>
    <asp:UpdatePanel ID="UP_GV_List" runat="server">
        <ContentTemplate>
            <div>
                <uc2:Transformer ID="ucTransformer" runat="server" DetailReadOnly="true" />
                <uc2:UpdateProgress ID="ucUpdateProgress" runat="server" />
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlScanBarcode" runat="server" Visible="false" Text="<%$Resources:Language,ScanBarcode%>" />:
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbScanBarcode" runat="server" Visible="false" AutoPostBack="true"
                                OnTextChanged="tbScanBarcode_TextChanged" />
                        </td>
                        <td colspan="2" />
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc2:HuList ID="ucHuList" runat="server" Visible="false" />
    <div class="tablefooter">
        <asp:CheckBox ID="cbPrintAsn" runat="server" Text="${MasterData.Distribution.PrintAsn}" />
        <cc1:Button ID="btnShip" runat="server" OnClick="btnShip_Click" Text="${MasterData.Distribution.Button.Ship}"
            FunctionId="ShipOrder" />
        <cc1:Button ID="btnCreatePickList" runat="server" Visible="false" OnClick="btnCreatePickList_Click"
            Text="${Warehouse.PickList.CreatePickList}" CssClass="button2" FunctionId="ShipOrder" />
        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
            CssClass="button2" />
        <asp:Button ID="btnScanHu" runat="server" OnClick="btnScanHu_Click" Text="${Common.Button.BatchScanHu}"
            CssClass="button2" />
    </div>
</fieldset>
