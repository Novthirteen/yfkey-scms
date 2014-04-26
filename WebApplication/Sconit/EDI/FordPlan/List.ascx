<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="EDI_FordPlan_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<style type="text/css">
    .Listtable
    {
        border: 0.1px solid #6F6F6F;
        text-align: center;
        margin: 0;
        border-collapse: collapse;
        border-spacing: 0;
        width: 100%;
    }
    .Listthead
    {
        background-color: #666666;
        border: 0.1px solid #6F6F6F;
    }
    
    .Listtd
    {
        border: 0.2px solid #CFCFC9;
    }
    
    
    .Listtr
    {
        height: 30px;
    }
    .Listtr02
    {
        height: 30px;
        background-color: #CFCFC9;
    }
    
    .footSpan
    {
        color: #6600FF;
    }
</style>
<script type="text/javascript">
    $(function () {
        //        $("#listTable td").css("padding", "0");
        //        $("#listTable td").css("padding-left", "3");
        //        $("#listTable td").css("padding-right", "3");
        $("#listThead td").css("border", "1px solid #D7D7D2");
        $("#listThead td").css("color", "#FFFFFF");
        $("#listThead td").css("font-weight", "bold");
        $("#listTbody td").css("border-left", "1px solid #B8B8AD");
        $("#listTbody td").css("border-right", "1px solid #B8B8AD");
        //        $("#btPrev").attr("disabled",<%=isMinPage %>);
        //        $("#btNext).attr("disabled",<%=isMaxPage %>);
        $(" input:checkbox").attr("checked", false);
    });

    function doDetClick(e) {
        $("#ctl01_ucList_btHidden").val(e);
        document.getElementById('ctl01_ucList_btShowDetail').click();

    }

    function GVCheckClick() {
        if ($(".Listthead input:checkbox").attr("checked") == true) {
            $("#listTbody input:checkbox").attr("checked", true);
            $("#ctl01_ucSearch_btnExport").show();
        }
        else {
            $("#listTbody input:checkbox").attr("checked", false);
            $("#ctl01_ucSearch_btnExport").hide();
        }
        getCheckedControl_Num();
    }

    function doCheckClick() {
        var $checkRecords = $("#listTbody input:checkbox");
        $(".Listthead input:checkbox").attr("checked", $checkRecords.length == $("#listTbody input[type=checkbox][checked]").length);
        if ($("#listTbody input[type=checkbox][checked]").length > 0) {
            $("#ctl01_ucSearch_btnExport").show();
        } else {
            $("#ctl01_ucSearch_btnExport").hide();
        }
        getCheckedControl_Num();
    }

    function getCheckedControl_Num() {
        var $checkRecords = $("#listTbody input:checkbox");
        var control_Num = "";
        for (var i = 0; i < $checkRecords.length; i++) {
            if ($checkRecords[i].checked) {
                control_Num += $($checkRecords[i]).parent().next().text()+",";
            }
        }
        $("#ctl01_ucSearch_btControl_Num").val(control_Num);
    }
</script>
<fieldset>
    <div class="GridView">
        <asp:Button ID="btShowDetail" Text="" runat="server"   OnClick="btnShowDetail_Click"   Style="display: none" />
        <input type="hidden" id="btHidden" runat="server" />
        <% if (returnList != null && returnList.Count > 0)
           { %>
        <table class="Listtable" id="listTable">
            <thead class="Listthead" id="listThead">
                <tr class="Listtr">
                    <td>
                        <asp:TemplateField>
                            <headertemplate>
                        <div onclick="GVCheckClick()">
                            <asp:CheckBox ID="CheckAll" runat="server" />
                        </div>
                    </headertemplate>
                        </asp:TemplateField>
                    </td>
                    <td>
                        <%="${EDI.EDIFordPlanBase.Control_Num}"%>
                    </td>
                    <td>
                        <%="${EDI.EDIFordPlanBase.ReleaseIssueDate}"%>
                    </td>
                    <td>
                        <%="${EDI.EDIFordPlanBase.Type}"%>
                    </td>
                    <td>
                        计划日期
                    </td>
                    <td>
                        导入日期
                    </td>
                    <td>
                    </td>
                </tr>
            </thead>
            <tbody id="listTbody">
                <%
               int i = 1;
               foreach (var fordPlan in returnList)
               {
                %>
                <tr class="<%=i%2==0?"Listtr":"Listtr02" %>">
                    <td>
                      <%--  <itemtemplate>
                        <asp:CheckBox ID="CheckBoxGroup" name="CheckBoxGroup" runat="server" />
                    </itemtemplate>--%>
                    <input type="checkbox" id="CheckBoxGroup" name="CheckBoxGroup" value="<%=fordPlan.Control_Num%>" runat="server" onclick="doCheckClick()" />
                    </td>
                    <td>
                        <a href="#" onclick="doDetClick(<%=fordPlan.Control_Num%>)">
                            <%=fordPlan.Control_Num%></a>
                    </td>
                    <td>
                        <%=fordPlan.ReleaseIssueDate.ToShortDateString()%>
                    </td>
                    <td>
                        <%=fordPlan.Type=="D"?"天":"周"%>
                    </td>
                    <td>
                        <%=fordPlan.PlanDateString %>
                    </td>
                    <td>
                        <%=fordPlan.CreateDate %>
                    </td>
                    <td>
                        <%--<asp:Button ID="Button1" runat="server" Text="查看明细"  OnClick="btnShowDetail_Click(<%#fordPlan.Control_Num %>)" />--%>
                        <%--<asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderNo") %>'
                            Text="${Common.Button.View}" OnClick="btnShowDetail_Click">--%>
                    </td>
                </tr>
                <%} %>
            </tbody>
            <tfoot>
                <tr style="height: 30px; text-align: left; border: 1px solid #CFCFC9">
                    <td colspan="20" style="border: 1px solid #CFCFC9">
                        总共【<span id="totalItem"><%=totalItem %></span>】项,当前显示【<span id="currentItem01"><%=currentItem01%></span>～<span
                            id="currentItem02"><%=currentItem02%></span>】项，
                        <asp:Button ID="btFistr" runat="server" Text="首页" OnClick="btnFirst_Click" />
                        <asp:Button ID="btPrev" runat="server" Text="上一页" OnClick="btnPrev_Click" />
                        <asp:Button ID="btNext" runat="server" Text="下一页" OnClick="btnNext_Click" />
                        <asp:Button ID="btLast" runat="server" Text="尾页" OnClick="btnLast_Click" />
                    </td>
                </tr>
            </tfoot>
        </table>
        <%}
           else
           { 
        %>
        没有符合条件的记录。
        <% } %>
    </div>
</fieldset>
