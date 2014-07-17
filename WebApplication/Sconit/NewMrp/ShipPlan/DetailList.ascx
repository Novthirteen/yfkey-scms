<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DetailList.ascx.cs" Inherits="NewMrp_ShipPlan_DetailList" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>
<div id="search" runat="server">
   <table class="mtable">
        <tr>
            <td class="td01">
            计划类型
            </td>
            <td class="td02">
                <asp:RadioButtonList ID="rbType" runat="server" RepeatDirection="Horizontal" 
                    >
                    <asp:ListItem Text="天" Value="Daily" Selected="True" />
                    <asp:ListItem Text="周" Value="Weekly" />
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
                路线:
            </td>
            <td class="td02">
                <uc3:textbox ID="tbFlow" runat="server" DescField="Description" ValueField="Code"
                    ServicePath="FlowMgr.service" MustMatch="true" Width="250" ServiceMethod="GetFlowList" />
                  <%--  <textarea id="tbFlow" rows="2" runat="server"  style="width:200" />--%>
            </td>
            <td class="td01">
                物料代码
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItemCode" runat="server" Visible="true" Width="250" MustMatch="true"
                    DescField="Description" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />
            </td>
        </tr>
       <%-- <tr>
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
        </tr>--%>
        <tr>
             <td class="td01">
            </td>
            <td class="td02">
            </td>
            <td>
            </td>
            <td class="td02">
                <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
                <asp:Button ID="btnBack" runat="server" Text=" 返回" OnClick="btnBack_Click" />
                <input type="hidden" id="btSeqHidden" runat="server" />
                <input type="hidden" id="btQtyHidden" runat="server" />
                <%--<asp:Button ID="btnRunProdPlan" runat="server" Text="生成主生产需求" OnClick="btnRunProdPlan_Click" />--%>
            </td>
        </tr>
    </table>
    <div id="list" runat="server">
    </div>
    <asp:Literal ID="ltlPlanVersion" runat="server" />
    <div id="mstrList" runat="server">
    </div>
    <div id="ShowTraceDiv" style="position:absolute;width:500px; ">
    </div>
    <div id="ShowDetsDiv" style="position:absolute; width:650px;">
    </div>
    <div id="ShowIpdets" style="position:absolute; width:500px;">
    </div>
</div>
<script type="text/javascript">
    function doTdClick(e) {
        var htmlt = $(e).attr("tital");
        htmlt = htmlt.replace("<table>", "<table class='GV' style=' border:1 solid black;background-color:White'>");
        //        htmlt = htmlt.replace("<td>", "<td style='border:1px' >");
        htmlt = htmlt.replace(/<td>/g, "<td style='border:1px solid black' >");
        //        a.replace(/,/g, ".");   
        htmlt = htmlt.replace("<thead><tr>", "<thead><tr class='GVHeader' onclick='hideClick()'>");
        htmlt = htmlt.replace(/<tr>/g, "<tr style='border:1px solid black' >");
        $("#ShowTraceDiv").html(htmlt);
        var obj = document.getElementById("ShowTraceDiv");
        obj.style.left = event.x + document.documentElement.scrollLeft + 10;
        obj.style.top = event.y + document.documentElement.scrollTop + 10;
        $(obj).show();
        $("#ShowDetsDiv").hide()
        $("#ShowIpdets").hide()
    }

    function hideClick() {
        $("#ShowTraceDiv").hide()
        $("#ShowDetsDiv").hide()
        $("#ShowIpdets").hide()
    } 

    function doShowDetsClick(e) {
        var htmlt = $(e).attr("tital");
        htmlt = htmlt.replace("<table>", "<table class='GV' style=' border:1 solid black;background-color:White'>");
        //        htmlt = htmlt.replace("<td>", "<td style='border:1px' >");
        htmlt = htmlt.replace(/<td>/g, "<td style='border:1px solid black' >");
        //        a.replace(/,/g, ".");   
        htmlt = htmlt.replace("<thead><tr>", "<thead><tr class='GVHeader' onclick='hideClick()'>");
        htmlt = htmlt.replace(/<tr>/g, "<tr style='border:1px solid black' >");
        $("#ShowDetsDiv").html(htmlt);
        var obj = document.getElementById("ShowDetsDiv");
        obj.style.left = event.x + document.documentElement.scrollLeft + 10;
        obj.style.top = event.y + document.documentElement.scrollTop + 10;
        $(obj).show();
        $("#ShowTraceDiv").hide()
        $("#ShowIpdets").hide()
    }

    function doShowIpdets(e) {
        var htmlt = $(e).attr("tital");
        htmlt = htmlt.replace("<table>", "<table class='GV' style=' border:1 solid black;background-color:White'>");
        //        htmlt = htmlt.replace("<td>", "<td style='border:1px' >");
        htmlt = htmlt.replace(/<td>/g, "<td style='border:1px solid black' >");
        //        a.replace(/,/g, ".");   
        htmlt = htmlt.replace("<thead><tr>", "<thead><tr class='GVHeader' onclick='hideClick()'>");
        htmlt = htmlt.replace(/<tr>/g, "<tr style='border:1px solid black' >");
        $("#ShowIpdets").html(htmlt);
        var obj = document.getElementById("ShowIpdets");
        obj.style.left = event.x + document.documentElement.scrollLeft + 10;
        obj.style.top = event.y + document.documentElement.scrollTop + 10;
        $(obj).show();
        $("#ShowTraceDiv").hide()
        $("#ShowDetsDiv").hide()
    }


    //    function doclick() {
    //        debugger
    //        //        alert($("#mstrList").html());
    //        //    var allHtml = $("#ctl01_list").html();
    //        var allFlow = "";
    //        var allItem = "";
    //        var allId = "";
    //        var allQty = "";
    //        $("#ctl01_list tbody input").each(function () {
    //            if (allFlow == "") {
    //                debugger
    //                alert($(this).flow);
    //                allFlow = $(this).flow;
    //                allItem = +$(this).item;
    //                allId = $(this).id;
    //                allQty = $(this).val();
    //            } else {
    //                allFlow += "," + $(this).flow;
    //                allItem += "," + $(this).item;
    //                allId += "," + $(this).id;
    //                allQty += "," + $(this).val();
    //            }
    //        });
    //        alert(allFlow);
    //    }


    function doFocusClick(e) {
        var cSeq = $(e).attr("seq");
        var cQty = $(e).val();
        var valSeqs = $("#ctl01_ucDetailList_btSeqHidden").val();
        var valQtys = $("#ctl01_ucDetailList_btQtyHidden").val();
        if (valSeqs == "[object]") valSeqs = "";
        if (valQtys == "[object]") valQtys = "";
        if (valSeqs != "") {
            var vSeqlArr = valSeqs.split(",");
            var vQtylArr = valQtys.split(",");
            var ii = 1;
            for (var i = 0; i < vSeqlArr.length; i++) {
                if (cSeq == vSeqlArr[i]) {
                    vQtylArr[i] = cQty;
                    ii = 0;
                }
            }
            if (ii == 1) {
                $("#ctl01_ucDetailList_btSeqHidden").val(valSeqs + "," + cSeq);
                $("#ctl01_ucDetailList_btQtyHidden").val(valQtys + "," + cQty);
            } else {
                valSeqs = "";
                valQtys = "";
                for (var i = 0; i < vSeqlArr.length; i++) {
                    if (valSeqs == "") {
                        valSeqs = vSeqlArr[i];
                        valQtys = vQtylArr[i];
                    } else {
                        valSeqs += "," + vSeqlArr[i];
                        valQtys += "," + vQtylArr[i];
                    }
                }
                $("#ctl01_ucDetailList_btSeqHidden").val(valSeqs);
                $("#ctl01_ucDetailList_btQtyHidden").val(valQtys);
            }
        } else {
            $("#ctl01_ucDetailList_btSeqHidden").val(cSeq);
            $("#ctl01_ucDetailList_btQtyHidden").val(cQty);
        }
        //        $("#ctl01_ucList_btHidden").val(e);
        //        document.getElementById('ctl01_ucList_btShowDetail').click();
    }
</script>
<!----new-->
<%--<script type="text/javascript">
    function timedMsg(url) {
        var t = setTimeout("PageRedirect('" + url + "')", 1000)
    }
    function PageRedirect(url) {
        try {
            //alert(url);
            window.location.href = url;
        }
        catch (err) { }
    }
//    $(document).ready(function () {
//        $('.GV').fixedtableheader();
//    });
</script>--%>
