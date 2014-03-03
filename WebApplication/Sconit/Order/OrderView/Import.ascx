<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Import.ascx.cs" Inherits="Inventory_OrderView_Import" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<div id="floatdiv">
    <fieldset>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="ltlSelect" runat="server" Text="${Common.FileUpload.PleaseSelect}:"></asp:Literal>
                </td>
                <td class="td02">
                    <asp:FileUpload ID="fileUpload" ContentEditable="false" runat="server" />
                </td>
                <td class="td01">
                    <asp:Literal ID="ltlTemplate" runat="server" Text="${Common.Business.Template}:" />
                </td>
                <td class="td02">
                    <asp:HyperLink ID="hlTemplate" runat="server" Text="${Common.Business.ClickToDownload}"
                        NavigateUrl="~/Reports/Templates/ExcelTemplates/OrderLocationTransaction.xls"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td colspan="3" />
                <td class="td02">
                    <div class="buttons">
                        <asp:Button ID="btnImport" runat="server" Text="${Common.Button.Import}" CssClass="add"
                            OnClick="btnImport_Click" />
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                            CssClass="button2" />
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
