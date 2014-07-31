<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DetailList.ascx.cs" Inherits="NewMrp_Shift_DetailList" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>
<div id="search" runat="server">
    <table class="mtable" runat="server" id="tblSearch">
        <tr>
           
           <td class="td01">
           生产线：
            </td>
            <td class="td02">
                <textarea id="tbFlow" rows="2" runat="server"  style="width:200" />
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
            </td>
            <td class="td02">
            </td>
            <td>
            </td>
            <td class="td02">
                <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" />
                <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" OnClick="btnExport_Click" />
               <%-- <asp:Button ID="btnCreateOrder" runat="server" Text="生成生产单" Style="display: none"
                    OnClick="btnCreateOrder_Click" />--%>
                <asp:Button ID="btnBack" runat="server" Text=" 返回" OnClick="btnBack_Click" />
                <input type="hidden" id="btIds" runat="server" />
                <input type="hidden" id="btSeqHidden" runat="server" />
                <input type="hidden" id="btQtyHidden" runat="server" />
            </td>
        </tr>
    </table>
    <div id="list" runat="server">
    </div>
</div>
<script type="text/javascript">

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

    function doCheckAllClickA() {
        if ($("#CheckAllA").attr("checked") == true) {
            $("input:checkbox[name='CheckBoxGroupA']").attr("checked", true);
            $("#ctl01_ucDetailList_btnCreateOrder").show();
        }
        else {
            $("input:checkbox[name='CheckBoxGroupA']").attr("checked", false);
            $("#ctl01_ucDetailList_btnCreateOrder").hide();
        }
        getCheckedValue();
    }

    function doCheckClickA() {
        var $checkRecords = $("input:checkbox[name='CheckBoxGroupA']");
        $("#CheckAllA").attr("checked", $checkRecords.length == $("input:checkbox[name='CheckBoxGroupA'][checked]").length);
        if ($("input:checkbox[name='CheckBoxGroupA'][checked]").length > 0) {
            $("#ctl01_ucDetailList_btnCreateOrder").show();
        } else {
            $("#ctl01_ucDetailList_btnCreateOrder").hide();
        }
        getCheckedValue();
    }

    function doCheckAllClickB() {
        if ($("#CheckAllB").attr("checked") == true) {
            $("input:checkbox[name='CheckBoxGroupB']").attr("checked", true);
            $("#ctl01_ucDetailList_btnCreateOrder").show();
        }
        else {
            $("input:checkbox[name='CheckBoxGroupB']").attr("checked", false);
            $("#ctl01_ucDetailList_btnCreateOrder").hide();
        }
        getCheckedValue();
    }

    function doCheckClickB() {
        var $checkRecords = $("input:checkbox[name='CheckBoxGroupB']");
        $("#CheckAllB").attr("checked", $checkRecords.length == $("input:checkbox[name='CheckBoxGroupB'][checked]").length);
        if ($("input:checkbox[name='CheckBoxGroupB'][checked]").length > 0) {
            $("#ctl01_ucDetailList_btnCreateOrder").show();
        } else {
            $("#ctl01_ucDetailList_btnCreateOrder").hide();
        }
        getCheckedValue();
    }

    function doCheckAllClickC() {
        if ($("#CheckAllC").attr("checked") == true) {
            $("input:checkbox[name='CheckBoxGroupC']").attr("checked", true);
            $("#ctl01_ucDetailList_btnCreateOrder").show();
        }
        else {
            $("input:checkbox[name='CheckBoxGroupC']").attr("checked", false);
            $("#ctl01_ucDetailList_btnCreateOrder").hide();
        }
        getCheckedValue();
    }

    function doCheckClickA() {
        var $checkRecords = $("input:checkbox[name='CheckBoxGroupA']");
        $("#CheckAllA").attr("checked", $checkRecords.length == $("input:checkbox[name='CheckBoxGroupA'][checked]").length);
        if ($("input:checkbox[name='CheckBoxGroupA'][checked]").length > 0) {
            $("#ctl01_ucDetailList_btnCreateOrder").show();
        } else {
            $("#ctl01_ucDetailList_btnCreateOrder").hide();
        }
        getCheckedValue();
    }

    function getCheckedValue() {
        var $checkRecords = $("input:checkbox[name='CheckBoxGroup'][checked]");
        var ids = "";
        for (var i = 0; i < $checkRecords.length; i++) {
            ids += $($checkRecords[i]).val() + ",";
        }
        $("#ctl01_ucDetailList_btIds").val(ids);
    }

</script>

