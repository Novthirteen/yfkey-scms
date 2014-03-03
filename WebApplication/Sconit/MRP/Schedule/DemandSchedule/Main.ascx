<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MRP_Schedule_DemandSchedule_Main" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Src="~/MRP/ShiftPlan/Manual/Shift.ascx" TagName="Shift" TagPrefix="uc" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<script language="javascript" type="text/javascript">
    function setStartTime() {
        var leadTime = $('#<%= hfLeadTime.ClientID %>').val();
        var emTime = $('#<%= hfEmTime.ClientID %>').val();
        var winTime = $('#<%= tbWinTime.ClientID %>').val();
        var isUrgent = $('#<%= cbIsUrgent.ClientID %>').attr('checked');
        var dateStr = "";
        if (winTime != "") {
            var startDate = new Date(Date.parse(winTime.replace(/-/g, '/')));
            if (isUrgent) {
                startDate = new Date(startDate.valueOf() - emTime * 60 * 60 * 1000);
            } else {
                startDate = new Date(startDate.valueOf() - leadTime * 60 * 60 * 1000);
            }
            var dateMinute = startDate.getMinutes();

            if (dateMinute < 10) {
                dateMinute = "0" + dateMinute;
            }
            dateStr = startDate.getFullYear() + "-" + (startDate.getMonth() + 1) + "-" + startDate.getDate() + " " +
            startDate.getHours() + ":" + dateMinute;
        }
        $('#<%= tbStartTime.ClientID %>').attr('value', dateStr);
        $('#<%= tbSettleTime.ClientID %>').attr('value', winTime);
    }

    function getActQty(obj) {
        if ($(obj).html() != "0") {
            var objId = $(obj).attr("id");
            var index = objId.substring(6);
            var date = $(".GVHeader").children('th').eq(6 + parseInt(index)).children('a').html();
            var itemCode = $(obj).parent().parent().children('td').eq(1).html();
            window.showModalDialog('Main.aspx?mid=MRP.Schedule.DemandSchedule.View__act--ViewActQty__ap--ItemCode-' + itemCode + '_Date-' + date.replace(/-/g, "/") + '_EffDate-' + '<%=EffDate%>'.replace(/_/g, "/") + '_IsFlow-<%=isFlow%>_FlowOrLoc-' + '<%=flowOrLoc%>'.replace(/-/g, "/").replace(/_/g, ")") + '_IsWinTime-<%=isWinTime%>_IgnorePermission-true',
                                 window, 'dialogWidth=600px;dialogHeight=400px;');
        }
    }
    function getRequiredQty(obj) {
        var objId = $(obj).attr("id");
        var index = objId.substring(11);
        var date = $(".GVHeader").children('th').eq(6 + parseInt(index)).children('a').html();
        var itemCode = $(obj).parent().parent().children('td').eq(1).html();
        window.showModalDialog('Main.aspx?mid=MRP.Schedule.DemandSchedule.View__act--ViewRequiredQty__ap--ItemCode-' + itemCode + '_Date-' + date.replace(/-/g, "/") + '_EffDate-' + '<%=EffDate%>'.replace(/-/g, "/") + '_IsFlow-<%=isFlow%>_FlowOrLoc-' + '<%=flowOrLoc%>'.replace(/-/g, "/").replace(/_/g, ")") + '_IsWinTime-<%=isWinTime%>_IgnorePermission-true', 
                                window, 'dialogWidth=600px;dialogHeight=400px;');
    }
</script>

<fieldset id="fld_Search" runat="server">
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblStartDate" runat="server" Text="${MRP.Schedule.MRPEffectTime}:" />
            </td>
            <td class="td02">
                <asp:DropDownList ID="ddlDate" runat="server" />
            </td>
            <td class="td01">
                <asp:RadioButtonList ID="rblFlowOrLoc" runat="server" RepeatDirection="Horizontal"
                    AutoPostBack="true" OnSelectedIndexChanged="rblFlowOrLoc_SelectedIndexChanged"
                    CssClass="floatright">
                    <asp:ListItem Text="${MRP.Schedule.Flow}" Value="Flow" Selected="True" />
                    <asp:ListItem Text="${MRP.Schedule.Location}" Value="Location" />
                </asp:RadioButtonList>
            </td>
            <td class="td02">
                <uc3:textbox ID="tbFlowOrLoc" runat="server" Visible="true" DescField="Description"
                    ValueField="Code" CssClass="inputRequired" ServicePath="FlowMgr.service" MustMatch="true"
                    Width="250" ServiceMethod="GetFlowList" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:RadioButtonList ID="rblDateType" runat="server" RepeatDirection="Horizontal"
                    CssClass="floatright">
                    <asp:ListItem Text="${MRP.Schedule.DateType.StartTime}" Value="StartTime" />
                    <asp:ListItem Text="${MRP.Schedule.DateType.WinTime}" Value="WindowTime" Selected="True" />
                </asp:RadioButtonList>
            </td>
            <td class="td02">
                <asp:TextBox ID="tbScheduleTime" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})"
                    Width="100" />
                <asp:HiddenField ID="hfLastScheduleTime" runat="server" />
            </td>
            <td class="td01">
                ${MRP.Schedule.ItemCode}:
            </td>
            <td class="td02">
                <asp:TextBox ID="tbItemCode" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td01">
            
            </td>
            <td class="td02">
                <asp:RadioButtonList ID="rblListFormat" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="${Common.ListFormat.Group}" Value="Group" Selected="True" />
                <asp:ListItem Text="${Common.ListFormat.Detail}" Value="Detail" />
                </asp:RadioButtonList>
            </td>
            <td class="td01">

                ${MRP.Schedule.Plant}:</td>
            <td class="td02">
                <asp:DropDownList ID="ddlPlant" runat="server">
                    <asp:ListItem Value="1">SW</asp:ListItem>
                    <asp:ListItem Value="2">AB</asp:ListItem>
                    <asp:ListItem Value="3">SB</asp:ListItem>
                    <asp:ListItem Value="4">CU</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td01">
            </td>
            <td class="td02">

            </td>
            <td class="td01">
            
            </td>
            <td class="td02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click" />
                <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" OnClick="btnSearch_Click" Visible="false" />
                <asp:Button ID="btnBack1" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                    Visible="false" />
            </td>
        </tr>
    </table>
</fieldset>
<fieldset id="fld_Group" runat="server" visible="false">
    <legend>${MRP.Schedule.Group}</legend>
    <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_List_RowDataBound"
        CellPadding="0" OnSorting="GV_List_Sorting" AllowSorting="true" OnSorted="CustomersGridView_Sorted"
        DataKeyNames="Item">
        <Columns>
            <asp:BoundField HeaderText="Seq" />
            <asp:BoundField HeaderText="${MRP.Schedule.Item}" DataField="Item" />
            <asp:BoundField HeaderText="${MRP.Schedule.ItemDescription}" DataField="ItemDescription" />
            <asp:BoundField HeaderText="${MRP.Schedule.DefaultSupplier}" />
            <asp:BoundField HeaderText="${Common.Business.Uom}" DataField="Uom" />
            <asp:BoundField HeaderText="${MRP.Schedule.UnitCount}" DataField="UnitCount" DataFormatString="{0:#,##0.##}" />
        </Columns>
    </asp:GridView>
    <asp:Literal ID="ltl_GV_List_Result" runat="server" Text="${Common.GridView.NoRecordFound}" />
</fieldset>
<div id="div_MRP_Detail" runat="server" visible="false">
    <fieldset>
        <legend>${MRP.Schedule.Detail}</legend>
        <asp:GridView ID="GV_Detail" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_Detail_RowDataBound"
            CellPadding="0">
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lblSequence" runat="server" />
                        <asp:HiddenField ID="hdfId" runat="server" Value='<%# Eval("Id")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="${Common.Business.ItemCode}" DataField="Item" />
                <asp:BoundField HeaderText="${MRP.Schedule.ItemDescription}" DataField="ItemDescription" />
                
                <asp:BoundField HeaderText="${MRP.Schedule.DefaultSupplier}" />
                <asp:BoundField HeaderText="${Common.Business.Uom}" DataField="Uom" />
                <asp:BoundField HeaderText="${MRP.Schedule.UnitCount}" DataField="UnitCount" DataFormatString="{0:#,##0.##}" />
                <asp:BoundField HeaderText="${MRP.Schedule.PeroidType}" DataField="SourceDateType" />
                <asp:BoundField HeaderText="${MRP.Schedule.SourceType}" DataField="SourceType" />
                <asp:BoundField HeaderText="${MRP.Schedule.LocationFrom}" DataField="LocationFrom" />
                <asp:BoundField HeaderText="${MRP.Schedule.LocationTo}" DataField="LocationTo" />
                <asp:TemplateField HeaderText="${MRP.Schedule.StartTime}">
                    <ItemTemplate>
                        <asp:Label ID="lblStartTime" runat="server" Text='<%# Eval("StartTime","{0:yyyy-MM-dd HH:mm}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MRP.Schedule.WinTime}">
                    <ItemTemplate>
                        <asp:Label ID="lblWinTime" runat="server" Text='<%# Eval("WindowTime","{0:yyyy-MM-dd HH:mm}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Qty}">
                    <ItemTemplate>
                        <asp:Label ID="tbQty" runat="server" Text='<%# Eval("Qty","{0:#,##0.##}")%>'   />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <table class="mtable">
            <tr>
                <td align="right">
                    <asp:Button ID="btnSave" runat="server" Text="${Common.Button.Save}" OnClick="btnSave_Click"
                        ValidationGroup="vgSave" Visible="false" />
                </td>
            </tr>
        </table>
        <asp:Literal ID="ltl_MRP_List_Result" runat="server" Text="${Common.GridView.NoRecordFound}"
            Visible="false" />
    </fieldset>
</div>
<div id="div_OrderDetail" runat="server" visible="false">

    <script type="text/javascript">
        function timedMsg(url) {
            var t = setTimeout("PageRedirect('" + url + "')", 5000)
        }
        function PageRedirect(url) {
            try {
                //alert(url);
                window.location.href = url;
            }
            catch (err) { }
        }
    </script>

    <fieldset>
        <legend>${MRP.Schedule.CreateOrder}</legend>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="ltlFlow" runat="server" Text="${MRP.Schedule.Flow}:" />
                </td>
                <td class="td02">
                    <uc3:textbox ID="tbFlow" runat="server" Visible="true" DescField="Description" ValueField="Code"
                        CssClass="inputRequired" ServicePath="FlowMgr.service" MustMatch="true" Width="250"
                        OnTextChanged="tbFlow_TextChanged" ServiceMethod="GetFlowList" AutoPostBack="true" />
                </td>
                <td class="td01">
                    <asp:Literal ID="ltlShift" runat="server" Text="${MasterData.WorkCalendar.Shift}:"
                        Visible="false" />
                </td>
                <td class="td02">
                    <uc:Shift ID="ucShift" runat="server" Visible="false" />
                    <asp:TextBox ID="tbSettleTime" runat="server" Text='<%# Bind("SettleTime","{0:yyyy-MM-dd HH:mm}") %>'
                        CssClass="inputRequired" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"
                        Visible="false" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblWinDate" runat="server" Text="${MasterData.Order.OrderHead.WindowTime}:" />
                </td>
                <td class="td02">
                    <table style="border: 0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:TextBox ID="tbWinTime" runat="server" Text='<%# Bind("WindowTime") %>' CssClass="inputRequired"
                                    onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" AutoPostBack="true" OnTextChanged="tbWinTime_TextChanged" />
                            </td>
                            <td>
                                <asp:CheckBox ID="cbIsUrgent" runat="server" Text="${MasterData.Order.OrderHead.IsUrgent}" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="td01">
                    <asp:Literal ID="lblExtOrderNo" runat="server" Text="${MasterData.Order.OrderHead.ExtOrderNo}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbExtOrderNo" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblStartTime" runat="server" Text="${MasterData.Order.OrderHead.StartTime}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbStartTime" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />
                    <asp:HiddenField ID="hfLeadTime" runat="server" Value="0" />
                    <asp:HiddenField ID="hfEmTime" runat="server" Value="0" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblRefOrderNo" runat="server" Text="${MasterData.Order.OrderHead.Flow.RefOrderNo}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbRefOrderNo" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td01">
                </td>
                <td class="td02">
                    <asp:CheckBox ID="cbReleaseOrder" runat="server" Text="${MasterData.Order.OrderHead.ReleaseOrder}" />
                    <asp:CheckBox ID="cbPrintOrder" runat="server" Text="${MasterData.Order.OrderHead.PrintOrder}"
                        Visible="false" />
                </td>
                <td class="td01">
                </td>
                <td class="td02">
                    <asp:Button ID="btnCreate" runat="server" Text="${Common.Button.Create}" OnClick="btnCreate_Click" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <asp:GridView ID="GV_Order" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_Order_RowDataBound"
            CellPadding="0">
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lblSequence" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="${Common.Business.ItemCode}" DataField="Item" />
                <asp:BoundField HeaderText="${MRP.Schedule.ItemDescription}" DataField="ItemDescription" />
                <asp:BoundField HeaderText="${MRP.Schedule.DefaultSupplier}" />
                <asp:BoundField HeaderText="${Common.Business.Uom}" DataField="Uom" />
                <asp:BoundField HeaderText="${MRP.Schedule.UnitCount}" DataField="UnitCount" DataFormatString="{0:#,##0.##}" />
                <asp:BoundField HeaderText="${MRP.Schedule.DemandQty}" DataField="Qty0" DataFormatString="{0:#,##0.##}" />
                <asp:TemplateField HeaderText="${MRP.Schedule.CurrentOrderQty}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbQty" runat="server" Width="50" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </fieldset>
</div>
