<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Import.ascx.cs" Inherits="Inventory_CycleCount_Import" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblRegion" runat="server" Text="${Common.Business.Region}:" />
            </td>
            <td class="td02">
                <uc3:textbox id="tbRegion" runat="server" visible="true" width="280" descfield="Name"
                    valuefield="Code" mustmatch="true" servicepath="RegionMgr.service" servicemethod="GetRegion"
                    cssclass="inputRequired" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblLocation" runat="server" Text="${Common.Business.Location}:" />
            </td>
            <td class="td02">
                <uc3:textbox id="tbLocation" runat="server" visible="true" descfield="Name" width="280"
                    valuefield="Code" servicepath="LocationMgr.service" servicemethod="GetLocation"
                    serviceparameter="string:#tbRegion" cssclass="inputRequired" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblEffDate" runat="server" Text="${Common.Business.EffDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEffDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblType" runat="server" Text="${Common.Business.Type}:" />
            </td>
            <td class="td02">
                <cc1:codemstrdropdownlist id="ddlType" runat="server" code="PhysicalCountType" />
            </td>
        </tr>
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
                    NavigateUrl="~/Reports/Templates/ExcelTemplates/CycleCount.xls"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="td02">
                <div class="buttons">
                    <asp:Button ID="btnImport" runat="server" Text="${Common.Button.Import}" CssClass="add"
                        OnClick="btnImport_Click" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
