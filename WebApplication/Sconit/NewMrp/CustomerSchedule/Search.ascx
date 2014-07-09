<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="NewMrp_CustomerSchedule_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="ASTreeView" Namespace="Geekees.Common.Controls" TagPrefix="ct" %>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:RadioButtonList ID="rblAction" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                    CssClass="floatright" OnSelectedIndexChanged="rblAction_SelectedIndexChanged">
                    <asp:ListItem Text="查询" Value="Search" Selected="True" />
                    <asp:ListItem Text="导入" Value="Import" />
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
            <td class="td01">
                销售路线:
            </td>
            <td class="td02">
                <uc3:textbox ID="tbFlow" runat="server" DescField="Description" ValueField="Code"
                    ServicePath="FlowMgr.service" MustMatch="true" Width="250" ServiceMethod="GetFlowList" />
            </td>
            <td class="td01">
                版本号
            </td>
            <td class="td02">
                <asp:TextBox ID="tbReferenceScheduleNo" runat="server" Width="100" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                创建日期 从
            </td>
            <td class="td02">
                <asp:TextBox ID="tbCreateStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})"
                    Width="100" />
            </td>
            <td class="td01">
                至
            </td>
            <td class="td02">
                <asp:TextBox ID="tbCreateEndDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})"
                    Width="100" />
            </td>
        </tr>
        <tr>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
            <td />
            <td class="td02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click" />
                </div>
            </td>
        </tr>
    </table>
    <table class="mtable" runat="server" id="tblImport" visible="false">
        <tr>
            <td class="td01">
                类型:
            </td>
            <td class="td02">
                <table>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rblDateType" runat="server" RepeatDirection="Horizontal"
                                CssClass="floatright">
                                <asp:ListItem Text="天" Value="Daily" Selected="True" />
                                <asp:ListItem Text="周" Value="Weekly" />
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:HyperLink ID="hlTemplate1" runat="server" Text="模板(天)" NavigateUrl="~/Reports/Templates/MRP/客户计划(天).xls" />
                            <asp:HyperLink ID="hlTemplate2" runat="server" Text="模板(周)" NavigateUrl="~/Reports/Templates/MRP/客户计划(周).xls" />
                        </td>
                    </tr>
                </table>
            </td>
            <td class="td01">
                请选择文件:
            </td>
            <td class="td02">
                <asp:FileUpload ID="fileUpload" ContentEditable="false" runat="server" />
                <%--<cc1:Button ID="btnImport" runat="server" Text="导入" OnClick="btnImport_Click" />--%>
                <asp:Button ID="btnImport" runat="server" Text="${Common.Button.Import}" OnClick="btnImport_Click"
                    CssClass="apply" />
            </td>
        </tr>
    </table>
</fieldset>
