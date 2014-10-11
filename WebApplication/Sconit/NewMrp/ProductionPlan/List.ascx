<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="NewMrp_ProductionPlan_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="ShowErrorMsg.ascx"   TagName="ShowErrorMsg"   TagPrefix="uc2" %>
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
                <asp:BoundField DataField="ReleaseNo" HeaderText="版本号" SortExpression="ReleaseNo" />
                <asp:BoundField DataField="EffDateFormat" HeaderText="生效日期" SortExpression="EffDateFormat" />
                <asp:BoundField DataField="Status" HeaderText="状态" SortExpression="Status" />
                <asp:BoundField DataField="CreateDate" HeaderText="创建用户" SortExpression="CreateDate" />
                <asp:BoundField DataField="CreateUser" HeaderText="创建时间" SortExpression="CreateUser" />
                <asp:BoundField DataField="ReleaseUser" HeaderText="释放用户" SortExpression="ReleaseUser" />
                <asp:BoundField DataField="ReleaseDate" HeaderText="释放时间" SortExpression="ReleaseDate" />
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
                       <asp:LinkButton ID="lbtRunProdPlan" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ReleaseNo") %>'
                            Text="生成采购计划" OnClick="btnRunPurchasePlan_Click" OnClientClick="return showTimesClick()">
                       </asp:LinkButton>
                        &nbsp&nbsp
                       <asp:LinkButton ID="lbtRunProdPlan2" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ReleaseNo") %>'
                            Text="生成采购发货计划" OnClick="btnRunPurchasePlan_Click2" OnClientClick="return showTimesClick2()">
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
    <uc2:ShowErrorMsg ID="ucShowErrorMsg" runat="server" Visible="false"  />
</div>
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
        if (confirm('确定要生成采购计划？')) {
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

    function showTimesClick2() {
        if (confirm('确定要生成采购发货计划？')) {
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
</script>
