<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="NewMrp_ShiftPlan_Main" %>
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
    <table class="mtable" runat="server" id="tblSearch">
        <tr>
            <td class="td01">
                生产线:
            </td>
            <td class="td02">
                <textarea id="tbFlow" rows="2" runat="server"  style="width:200" />
            </td>
            <td class="td01">
                开始时间:
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})"
                    Width="150" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                版本号:
            </td>
            <td class="td02">
            <asp:TextBox ID="tbRefPlanNo" runat="server" Width="150" /></td>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
        </tr>
        <tr>
            <td class="td01">
            </td>
            <td class="td02">
                <asp:Literal ID="ltlPlanVersion" runat="server" />
            </td>
            <td class="td01">
            </td>
            <td class="td02">
                <asp:Button ID="btnSave" runat="server" Text="查询" OnClick="btnSearch_Click" />
                <asp:Button ID="btmSubmit" runat="server" Text="释放" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" OnClick="btnExport_Click" />
                <%--<asp:Button ID="btnMrpCalculate" runat="server" Text="Mrp运算" OnClick="btnMrpCalculate_Click" />--%>
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
                        </td>
                        <td>
                            <%--<asp:HyperLink ID="hlTemplate1" runat="server" Text="班产计划"
                                NavigateUrl="~/Reports/Templates/MRP/班产计划.xls" />--%>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="td01">
                请选择文件:
            </td>
            <td class="td02">
                <%--<input type="file" id="fileUpload" name="upVocFile" runat="server"/>--%>
                <asp:FileUpload ID="fileUpload" ContentEditable="false" runat="server" />
                <%--<cc1:Button ID="btnImport" runat="server" Text="导入" OnClick="btnImport_Click" /> --%>
                <asp:Button ID="btnImport" runat="server" Text="${Common.Button.Import}" OnClick="btnImport_Click"
                    CssClass="apply" />
            </td>
        </tr>
    </table>
    <div id="list" runat="server">
    </div>
</div>
<%--<div id="floatdiv">
    <uc2:Preview ID="ucPreview" runat="server" Visible="false"  />
</div>--%>
