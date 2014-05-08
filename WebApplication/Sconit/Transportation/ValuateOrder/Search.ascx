<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Transportation_ValuateOrder_Search" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblOrderNo" runat="server" Text="${Transportation.TransportationOrder.OrderNo}:" />
            </td>
            <td class="ttd02">
                <asp:TextBox ID="tbOrderNo" runat="server" Visible="true" />
            </td>
            <td class="ttd01">
                <asp:Literal ID="lblRoute" runat="server" Text="${Transportation.TransportationOrder.Route}:" />
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbRoute" runat="server" Width="250" DescField="Description" ValueField="Code"
                    MustMatch="true" ServiceMethod="GetAllTransportationRoute"
                    ServicePath="TransportationRouteMgr.service" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlStartDate" runat="server" Text="${Common.Business.StartDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlEndDate" runat="server" Text="${Common.Business.EndDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEndDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
        </tr>
        <tr>
            <td class="td01">
            文件位置：
            </td>
            <td class="td02">
            <asp:FileUpload ID="fileUpload" runat="server" />
                    <asp:Button runat="server" ID="btnUpload" OnClick="btnUpload_Click" Text="上传" CssClass="button2" />
                    <asp:HyperLink ID="hlTemplate" runat="server" Text="模板下载"
                    NavigateUrl="~/Reports/Templates/YFKExcelTemplates/ImportValuateOrder.xls"></asp:HyperLink>
            </td>
            <td class="td01">
            </td>
            <td class="ttd02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                        CssClass="query" />
                    <asp:Button ID="btnValuate" runat="server" Text="${Common.Button.Valuate}" OnClick="btnValuate_Click"
                        CssClass="button2" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
