<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewList.ascx.cs" Inherits="Finance_Bill_NewList" %>
<script language="javascript" type="text/javascript" src="Js/calcamount.js"></script>
<%-- <script type="text/javascript" language="javascript">
    function GVCheckClick_new() {

        if ($("input:[tag='isIntern']").val() == "False") {
            if ($("div[new=''] .GVHeader th input:checkbox").attr("checked") == true) {
                var count = 0;
                var cur = 0;
                $("div[new='']   td input:checkbox").attr("checked", function (index, attr) {
                    if (Math.abs(count) > 1000000) {
                        return false;
                    }
                    else {
                        cur = Number($($(this).parent().parent().siblings()[11]).find("input").val()); //当前行的值
                        if (Number(count) + Number(cur) <= 1000000) {
                            this.checked = "checked";
                            count = Number(count) + Number(cur);
                        }
                    }
                });
                if (Math.abs(Number(count) + Number(cur)) > 1000000) {
                    _tot = Number(count);
                    alert("账单金额不能超过100w!");
                    $("div[new=''] .GVHeader th input:checkbox").removeAttr("checked");
                }
                else {
                    _tot = Number(count) + Number(cur);
                }
                // $("#total").html("账单总金额:" + number(count).tofixed(2));


                //            $(".gvheader th input:checkbox").click();
            }
            else {

                $("div[new='']  td input:checkbox").removeAttr("checked");
                // $("#total").html("账单总金额:0.00");
                //            $(".gvheader th input:checkbox").click();
            }
        }
        else {
            if ($("div[new=''] .GVHeader input:checkbox").attr("checked") == true) {
                $("div[new=''] .GVRow input:checkbox").attr("checked", true);
                $("div[new=''] .GVAlternatingRow input:checkbox").attr("checked",true);
            }
            else {
                $("div[new=''] .GVRow input:checkbox").attr("checked", false);
                $("div[new=''] .GVAlternatingRow input:checkbox").attr("checked", false);
            }
        }
    }
    $(function () {
        $(".GridView td input:checkbox").click(function () {
            if ($("input:[tag='isIntern']").val() == "False")
                cal();
        });
    });
    var _tot = 0;
    function cal() {
        if ($("input:[tag='isIntern']").val() == "False") {
            var obj = event.srcElement; //点击控件源
            // var total = document.getElementById("total").innerHTML; //获取HTML内容
            //var _tot = Number(total.split(":")[1]).toFixed(2);

            while (obj.tagName != "TR")
                obj = obj.parentNode;
            var i = obj.rowIndex;
            if (i != 0) {
                var cur = Number(obj.cells[12].getElementsByTagName("INPUT")[0].value).toFixed(2);
                if (event.srcElement.checked == true) {
                    if (Math.abs(Number(_tot) + Number(cur)) > 1000000) {
                        alert("非内部客户账单金额不能超过100w!");
                        $(obj).find("input").removeAttr("checked");
                        return false;
                    }
                    _tot = Number((Number(_tot) + Number(cur))).toFixed(2);
                    //  document.getElementById("total").innerHTML = "账单总金额:" + Number((Number(_tot) + Number(cur))).toFixed(2);
                }
                else {
                    if (Number(cur) > 0)
                        _tot = Number((Number(_tot) - Number(cur))).toFixed(2);
                    //  document.getElementById("total").innerHTML = "账单总金额:" + Number((Number(_tot) - Number(cur))).toFixed(2);
                    else {
                        if (Math.abs(Number(_tot) + Number(cur)) > 1000000) {
                            alert("非内部客户账单金额不能超过100w!");
                            $(obj).find("input").attr("checked", "checked");
                            return false;
                        }
                        _tot = Number((Number(_tot) - Number(cur))).toFixed(2);
                        // document.getElementById("total").innerHTML = "账单总金额:" + Number((Number(_tot) - Number(cur))).toFixed(2);
                    }
                }
            }
        }
    }
</script> --%>


<script type="text/javascript" language="javascript">
    function GVCheckClick_new() {
        if ($("div[new=''] .GVHeader input:checkbox").attr("checked") == true) {
            $("div[new=''] .GVRow input:checkbox").attr("checked", true);
            $("div[new=''] .GVAlternatingRow input:checkbox").attr("checked", true);
        }
        else {
            $("div[new=''] .GVRow input:checkbox").attr("checked", false);
            $("div[new=''] .GVAlternatingRow input:checkbox").attr("checked", false);
        } 
    }
</script>

<fieldset>
    <%--<legend id="total">账单总金额:0</legend>--%>
    <div style="height: auto; color: Red" id="error">
    </div>
    <div class="GridView" new=''>
        <asp:GridView ID="GV_List" runat="server" AllowPaging="False" DataKeyNames="Id" AllowSorting="False"
            AutoGenerateColumns="False" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div onclick="GVCheckClick_new()">
                            <asp:CheckBox ID="CheckAll" runat="server" />
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:HiddenField ID="hfId" runat="server" Value='<%# Bind("Id") %>' />
                        <asp:CheckBox ID="CheckBoxGroup" name="CheckBoxGroup" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${MasterData.ActingBill.Supplier}" SortExpression="BillAddress.Party.Name">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "BillAddress.Party.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ReceiptNo" HeaderText="${MasterData.ActingBill.ReceiptNo}"
                    SortExpression="ReceiptNo" />
                <asp:BoundField DataField="ExternalReceiptNo" HeaderText="${MasterData.ActingBill.ExternalReceiptNo}"
                    SortExpression="ExternalReceiptNo" />
                <asp:TemplateField HeaderText=" ${Common.Business.ItemCode}" SortExpression="Item.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Item.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${Common.Business.ItemDescription}" SortExpression="Item.Description">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Item.Description")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- added by williamlu@esteering 2012/4/12 --%>
                <asp:TemplateField HeaderText=" ASN " SortExpression="IpNo">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "IpNo")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- added end --%>
                <asp:TemplateField HeaderText=" ${Common.Business.Uom}" SortExpression="Uom.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Uom.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ActingBill.EffectiveDate}" SortExpression="EffectiveDate">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "EffectiveDate", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ActingBill.UnitPrice}" SortExpression="UnitPrice">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfUnitPrice" runat="server" Value='<%# Bind("UnitPrice") %>' />
                        <%# DataBinder.Eval(Container.DataItem, "UnitPrice", "{0:0.########}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--        <asp:TemplateField HeaderText="${MasterData.ActingBill.Currency}" SortExpression="Currency.Code"> 
                  <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Currency.Code")%>
                    </ItemTemplate>
                </asp:TemplateField> --%>
                <asp:TemplateField HeaderText="${MasterData.ActingBill.BillQty}" SortExpression="BillQty">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "BillQty", "{0:0.########}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ActingBill.BilledQty}" SortExpression="BilledQty">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "BilledQty", "{0:0.########}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ActingBill.CurrentBillQty}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbQty" runat="server" onmouseup="if(!readOnly)select();" Width="50"
                            onchange="CalCulateRowAmount(this, 'tbQty', 'BaseOnDiscountRate', 'hfUnitPrice', 'tbQty', 'tbDiscount', 'tbDiscountRate', 'tbAmount',false);"></asp:TextBox>
                        <span style="display: none">
                            <asp:TextBox ID="tbDiscountRate" runat="server" Text="0" />
                            <asp:TextBox ID="tbDiscount" runat="server" Text="0" />
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- <asp:TemplateField HeaderText="${MasterData.ActingBill.DiscountRate}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbDiscountRate" runat="server" onmouseup="if(!readOnly)select();"
                            Width="50" onchange="CalCulateRowAmount(this, 'tbDiscountRate', 'BaseOnDiscountRate', 'hfUnitPrice', 'tbQty', 'tbDiscount', 'tbDiscountRate', 'tbAmount',false);" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ActingBill.Discount}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbDiscount" runat="server" onmouseup="if(!readOnly)select();" Width="50"
                            onchange="CalCulateRowAmount(this, 'tbDiscount', 'BaseOnDiscount', 'hfUnitPrice', 'tbQty', 'tbDiscount', 'tbDiscountRate', 'tbAmount',false);" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="${MasterData.ActingBill.Amount}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbAmount" runat="server" Width="80" onfocus="this.blur();" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Literal ID="lblNoRecordFound" runat="server" Text="${Common.GridView.NoRecordFound}"
            Visible="false" />
    </div>
</fieldset>
