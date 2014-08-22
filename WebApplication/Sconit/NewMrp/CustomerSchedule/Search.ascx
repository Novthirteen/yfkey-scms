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
                <asp:Button ID="btnRunShipPlan" runat="server" Text="生成发运计划" OnClick="btnRunShipPlan_Click"   OnClientClick="return showTimesClick()" />
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
        <table id="showTimes" align="center" runat="server" style="display:none">
        <tr><td><div id="SpeedDiv"></div></td></tr>
        <tr>
            <td>
                <%--<div style="font-size: 8pt; padding: 2px; border: solid black 1px">--%>
                <div style="font-size: 8pt; padding: 2px;">
                    <span id="progress1">&nbsp; &nbsp;</span> <span id="progress2">&nbsp; &nbsp;</span>
                    <span id="progress3">&nbsp; &nbsp;</span> <span id="progress4">&nbsp; &nbsp;</span>
                    <span id="progress5">&nbsp; &nbsp;</span> <span id="progress6">&nbsp; &nbsp;</span>
                    <span id="progress7">&nbsp; &nbsp;</span> <span id="progress8">&nbsp; &nbsp;</span>
                    <span id="progress9">&nbsp; &nbsp;</span> <span id="progress10">&nbsp; &nbsp;</span>
                    <span id="progress11">&nbsp; &nbsp;</span> <span id="progress12">&nbsp; &nbsp;</span>
                    <span id="progress13">&nbsp; &nbsp;</span> <span id="progress14">&nbsp; &nbsp;</span>
                    <span id="progress15">&nbsp; &nbsp;</span> <span id="progress16">&nbsp; &nbsp;</span>
                    <span id="progress17">&nbsp; &nbsp;</span> <span id="progress18">&nbsp; &nbsp;</span>
                    <span id="progress19">&nbsp; &nbsp;</span> <span id="progress20">&nbsp; &nbsp;</span>
                </div>
            </td>
        </tr>
    </table>
</fieldset>
<script type="text/javascript">
    var timer;
    $(function () {
        clearInterval(timer);
        progress_stop();
    });

    function getSpeedDate(oldDate) {
        var newDate = new Date();
        var date = newDate - oldDate;
        var minutess = parseInt(date / 1000);

        var showTime = "00:" + (parseInt(minutess / 60) < 10 ? ("0" + parseInt(minutess / 60)) : parseInt(minutess / 60)) + ":" + (parseInt(minutess % 60) < 10 ? ("0" + parseInt(minutess % 60)) : parseInt(minutess % 60));
        $("#SpeedDiv").text("正在生成计划，请稍等… 已用时: " + showTime).show();
        return oldDate;
    }

    function stopSearch(timer) {
        clearInterval(timer);
    }

    function showTimesClick() {
        if (confirm('确定要生成发运计划？')) {
            var oldDate = new Date();
            timer = setInterval(function () { oldDate = getSpeedDate(oldDate) }, 1000);
            progress_update();
            $("#ctl01_ucSearch_showTimes").show();
            return true;
        } else {
            $("#ctl01_ucSearch_showTimes").hide();
            clearInterval(timer);
            progress_stop();
            return false;
        }
    }

    var progressEnd = 20;  // set to number of progress <span>'s.
    var progressColor = 'blue'; // set to progress bar color
    var progressInterval = 1000; // set to time between updates (milli-seconds)

    var progressAt = progressEnd;
    var progressTimer;
    var useTimes = 0;
    // start progress bar
    function progress_clear() {
        for (var i = 1; i <= progressEnd; i++) {
            document.getElementById('progress' + i).style.backgroundColor = 'transparent';
        }
        progressAt = 0;
    }
    function progress_update() {
        progressAt++;
        if (progressAt > progressEnd) {
            progress_clear();
        }
        else {
            document.getElementById('progress' + progressAt).style.backgroundColor = progressColor;
        }
        progressTimer = setTimeout('progress_update()', progressInterval);
    }
    function progress_stop() {
        clearTimeout(progressTimer);
        progress_clear();
    }
</script>

