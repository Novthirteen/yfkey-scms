<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="NewMrp_ShiftPlan_Search" %>
<%--<%@ Register Src="Preview.ascx" TagName="Preview" TagPrefix="uc2" %>--%>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<script language="javascript" type="text/javascript" src="Js/jquery.fixedtableheader-1-0-2.min.js"></script>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>
<div id="search" runat="server">
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
    <table class="mtable" runat="server" id="tblSearch" >
         <tr>
            <td class="td01">
                版本号
            </td>
            <td class="td02">
                <asp:TextBox ID="tbReleaseNo" runat="server" Width="100" />
            </td>
            <td class="td01">
            </td>
            <td class="td02">
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
                <%--模板:--%>
            </td>
            <td class="td02">
                <table>
                    <tr>
                        <td>
                        模版下载：
                        </td>
                        <td>
                            <asp:HyperLink ID="hlTemplate1" runat="server" Text="班产计划"
                                NavigateUrl="~/Reports/Templates/MRP/ShiftPlan.xls" />
                        </td>
                    </tr>
                </table>
            </td>
            <td class="td01">
                请选择文件:
            </td>
            <td class="td02">
                <asp:FileUpload ID="fileUpload" ContentEditable="false" runat="server" />
                <asp:Button ID="btnImport" runat="server" Text="${Common.Button.Import}" OnClick="btnImport_Click"
                    CssClass="apply" />
            </td>
        </tr>
    </table>
    <div id="list" runat="server">
    </div>
</div>

