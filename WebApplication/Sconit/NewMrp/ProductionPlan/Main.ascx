<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="NewMrp_ProductionPlan_Main" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>
<div id="search" runat="server">
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:RadioButtonList ID="rblAction" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                    CssClass="floatright" OnSelectedIndexChanged="rblAction_SelectedIndexChanged">
                    <asp:ListItem Text="明细" Value="Search" Selected="True" />
                    <asp:ListItem Text="日志" Value="Import" />
                </asp:RadioButtonList>
            </td>
            <td class="td02">
            </td>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
        </tr>
    </table>
    <hr />
    <table class="mtable" runat="server" id="tblSearch">
        <tr>
            <td class="ttd01">
                生产计划版本号
            </td>
            <td class="ttd02">
                <asp:TextBox ID="tbReleaseNo" runat="server" />
            </td>
            <td class="td01">
                物料代码
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItemCode" runat="server" Visible="true" Width="250" MustMatch="true"
                    DescField="Description" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                发运日期 从
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})"
                    Width="165" />
            </td>
            <td class="td01">
                至
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEndDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})"
                    Width="165" />
            </td>
        </tr>
        <tr>
            <td>
            </td><td>
            </td>
            <td>
            </td>
            <td class="td02">
                <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
                <asp:Button ID="btnExport" runat="server" Text="导出" OnClick="btnExport_Click" />
                <%--<asp:Button ID="btnRunProdPlan" runat="server" Text="生成主生产需求" OnClick="btnRunProdPlan_Click" />--%>
            </td>
        </tr>
    </table>
    <div id="list" runat="server">
    </div>
    <table class="mtable" runat="server" id="tblImport" visible="false">
        <tr>
            <td class="td01">
                生产计划版本号
            </td>
            <td class="td02">
                <asp:TextBox ID="tbMstrReleaseNo" runat="server" />
            </td>
            <td>
            </td>
            <td class="td02">
                <asp:Button ID="btnMstrSearch" runat="server" Text="查询" OnClick="btnMstrSearch_Click" />
            </td>
        </tr>
    </table>
    <asp:Literal ID="ltlPlanVersion" runat="server" />
    <div id="mstrList" runat="server">
    </div>
    <div id="ShowTraceDiv" style="position:absolute; ">
    </div>
</div>

