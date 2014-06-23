<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="NewMrp_ProductPlan_Main" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<script language="javascript" type="text/javascript" src="Js/jquery.fixedtableheader-1-0-2.min.js"></script>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>
<div id="search" runat="server">
    <table class="mtable" runat="server" id="tblSearch">
        <tr>
           <%-- <td class="td01">工序:</td>
            <td class="td02">
                <cc1:CodeMstrDropDownList ID="ddlOperation" Code="MrpOpt" runat="server" IncludeBlankOption="true">
                </cc1:CodeMstrDropDownList>
            </td>--%>
            <td class="td01">生产线:
            </td>
            <td class="td02">
                <%--<uc3:textbox ID="tbFlow" runat="server" DescField="Description" ValueField="Code"
                    ServicePath="FlowMgr.service" MustMatch="true" Width="250"
                    ServiceMethod="GetFlowList" />--%>
                    <%--<uc3:textbox ID="tbFlow" runat="server" Width="250" height="50"/>--%>
                    <%--<asp:TextBox ID="tbFlow" runat="server" Width="250" Rows="3" />--%>
                   <textarea ID="tbFlow" rows="2"  runat="server" Width="250" />
                   
            </td>
             <td class="td01">开始时间:
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})"
                    Width="100" />
            </td>
        </tr>
        <tr>
            <td class="td01">提前期</td>
            <td class="td02">
             <asp:TextBox ID="LeadTime" runat="server" Width="50" />
            </td>
            <td></td>
            <td class="td02">
                <asp:CheckBox ID="cbIsShow0" runat="server" Text="显示无需求明细" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td class="td02">
                <asp:Button ID="btnSave" runat="server" Text="查询" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>


    <div id="list" runat="server"></div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $('.GV').fixedtableheader();
    });
</script>
