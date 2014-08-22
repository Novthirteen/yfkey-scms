<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="NewMrp_ShipPlan_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="ShowErrorMsg.ascx" TagName="ShowErrorMsg" TagPrefix="uc2" %>
<fieldset>
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
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <%--<asp:TemplateField>
                    <HeaderTemplate>
                        <div onclick="GVCheckClick()">
                            <asp:CheckBox ID="CheckAll" runat="server" />
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                      <div onclick="doCheckClick()" >
                        <asp:HiddenField ID="hfId" runat="server" Value='<%# Bind("Id") %>' />
                        <asp:CheckBox ID="CheckBoxGroup" name="CheckBoxGroup" runat="server" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="ReleaseNo" HeaderText="版本号" SortExpression="ReleaseNo" />
                <asp:BoundField DataField="EffDateFormat" HeaderText="生效日期" SortExpression="EffDateFormat" />
                <asp:BoundField DataField="Status" HeaderText="状态" SortExpression="Status" />
                <asp:BoundField DataField="CreateDate" HeaderText="创建用户" SortExpression="CreateDate" />
                <asp:BoundField DataField="CreateUser" HeaderText="创建时间" SortExpression="CreateUser" />
                <asp:BoundField DataField="ReleaseUser" HeaderText="释放用户" SortExpression="LastModifyUser" />
                <asp:BoundField DataField="ReleaseDate" HeaderText="释放时间" SortExpression="LastModifyDate" />
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ReleaseNo") %>'
                            Text="查看明细" OnClick="lbtnEdit_Click">
                        </asp:LinkButton>
                        &nbsp&nbsp
                        <asp:LinkButton ID="lbtnShowErrorMsg" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BatchNo") %>'
                            Text="错误日志" OnClick="lbtnShowErrorMsg_Click">
                        </asp:LinkButton>
                        &nbsp&nbsp
                        <asp:LinkButton ID="lbtSubmit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ReleaseNo") %>'
                            Text="释放" OnClick="btnSubmit_Click" OnClientClick="return confirm('确定要释放？')">
                        </asp:LinkButton>
                        &nbsp&nbsp
                        <span onclick="showTimesClick()">
                        </span>
                        <asp:LinkButton ID="lbtRunProdPlan" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ReleaseNo") %>'
                            Text="生成主生产计划" OnClick="btnRunProdPlan_Click" OnClientClick="return showTimesClick()">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
<div id="floatdiv">
    <uc2:ShowErrorMsg ID="ucShowErrorMsg" runat="server" Visible="false" />
</div>
<script language="javascript" type="text/javascript">
//    function doRunBtnClick() {
//        alert(1);
//    }
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
        if (confirm('确定要生成主生产计划？')) {
            var oldDate = new Date();
            timer = setInterval(function () { oldDate = getSpeedDate(oldDate) }, 1000);
            progress_update();
            $("#ctl01_ucList_showTimes").show();
            return true;
        } else {
            $("#ctl01_ucList_showTimes").hide();
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

    function GVCheckClick() {
        if ($(".GVHeader input:checkbox").attr("checked") == true) {
            $(".GVRow input:checkbox").attr("checked", true);
            //            $(".GVAlternatingRow input:checkbox").attr("checked", true);
        }
        else {
            $(".GVRow input:checkbox").attr("checked", false);
            //            $(".GVAlternatingRow input:checkbox").attr("checked", false);
        }
        doCheckClick();
    }

    function doCheckClick() {
        var $checkRecords = $(".GVRow input:checkbox");
        $(".GVHeader input:checkbox").attr("checked", $checkRecords.length == $(".GVRow input[type=checkbox][checked]").length);
        if ($(".GVRow input[type=checkbox][checked]").length > 0) {
            $("#ctl01_ucSearch_btnExport").show();
        } else {
            $("#ctl01_ucSearch_btnExport").hide();
        }
        getCheckedExportId();
    }

    function getCheckedExportId() {
        var $checkRecords = $(".GVRow input:checkbox");
        var msids = "";
        for (var i = 0; i < $checkRecords.length; i++) {
            if ($checkRecords[i].checked) {
                msids += $($checkRecords[i]).prev().val() + ",";
            }
        }
        $("#ctl01_ucSearch_btMstrIds").val(msids);
    }
</script>
