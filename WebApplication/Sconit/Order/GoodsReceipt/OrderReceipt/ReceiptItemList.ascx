<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReceiptItemList.ascx.cs"
    Inherits="Order_GoodsReceipt_OrderReceipt_ReceiptItemList" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Hu/List.ascx" TagName="HuList" TagPrefix="uc2" %>
<%@ Register Src="~/Hu/Transformer.ascx" TagName="Transformer" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/UpdateProgress.ascx" TagName="UpdateProgress" TagPrefix="uc2" %>
<fieldset>
    <legend>
        <asp:Literal ID="ltlReceiptDetails" runat="server" /></legend>
    <asp:UpdatePanel ID="UP_GV_List" runat="server">
        <ContentTemplate>
            <div>
                <uc2:Transformer ID="ucTransformer" runat="server" />
                <uc2:UpdateProgress ID="ucUpdateProgress" runat="server" />
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblRefNo" runat="server" Text="<%$Resources:Language,MasterDataRefNo%>"
                                Visible="false" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbRefNo" runat="server" Visible="false" CssClass="inputRequired" />
                        </td>
                        <td colspan="2" />
                    </tr>
                    <tr id="trBarcode" runat="server" visible="false">
                        <td class="td01">
                            <asp:Literal ID="ltlScanBarcode" runat="server" Text="<%$Resources:Language,ScanBarcode%>" />:
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbScanBarcode" runat="server" AutoPostBack="true" OnTextChanged="tbScanBarcode_TextChanged" />
                        </td>
                        <td colspan="2" />
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>
<uc2:HuList ID="ucHuList" runat="server" Visible="false" />
<div class="tablefooter">
    <asp:CheckBox ID="cbPrintReceipt" runat="server" Text="${MasterData.Flow.PrintReceipt}" />
    <asp:Button ID="btnReceive" runat="server" Text="${MasterData.Order.Button.Receive}"
        CssClass="button2" OnClick="btnReceive_Click" />
    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
        CssClass="button2" />
    <asp:Button ID="btnScanHu" runat="server" Visible="false" OnClick="btnScanHu_Click"
        Text="${Common.Button.BatchScanHu}" CssClass="button2" />
</div>
