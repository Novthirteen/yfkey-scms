<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="NewMrp_CustomerPlan_Main" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
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
            <td class="td02"></td>
            <td class="td01"></td>
            <td class="td02"></td>
        </tr>
    </table>
    <hr />


    <table class="mtable" runat="server" id="tblSearch">
        <tr>
            <td class="td01">路线:
            </td>
            <td class="td02">
                <uc3:textbox ID="tbFlow" runat="server" DescField="Description" ValueField="Code"
                    ServicePath="FlowMgr.service" MustMatch="true" Width="250"
                    ServiceMethod="GetFlowList" />
            </td>
            <td class="td01">开始时间:
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})"
                    Width="100" />
            </td>
        </tr>
        <tr>
            <td class="td01">版本号:</td>
            <td class="td02">
                <asp:TextBox ID="tbPlanVersion" runat="server" Width="50" />
            </td>
            <td>
                <asp:RadioButtonList ID="rblSearchDateType" runat="server" RepeatDirection="Horizontal"
                    CssClass="floatright">
                    <asp:ListItem Text="天" Value="4" Selected="True" />
                    <asp:ListItem Text="周" Value="5" />
                    <asp:ListItem Text="月" Value="6" />
                </asp:RadioButtonList></td>
            <td class="td02">
                <asp:Button ID="btnSave" runat="server" Text="查询" OnClick="btnSearch_Click" />
                <asp:Button ID="btnCalculateProdPlan" runat="server" Text="生成生产需求" OnClick="btnCalculateProdPlan_Click" />
            </td>
        </tr>
    </table>

    <table class="mtable" runat="server" id="tblImport" visible="false">
        <tr>
            <td class="td01">类型:
            </td>
            <td class="td02">
                <table>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="rblDateType" runat="server" RepeatDirection="Horizontal"
                                CssClass="floatright">
                                <asp:ListItem Text="天" Value="4" Selected="True" />
                                <asp:ListItem Text="周" Value="5" />
                                <asp:ListItem Text="月" Value="6" />
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            <asp:HyperLink ID="hlTemplate1" runat="server" Text="模板(天)"
                                NavigateUrl="~/Reports/Templates/MRP/客户计划(天).xls" />
                            <asp:HyperLink ID="hlTemplate2" runat="server" Text="模板(周)"
                                NavigateUrl="~/Reports/Templates/MRP/客户计划(周).xls" />
                            <asp:HyperLink ID="hlTemplate3" runat="server" Text="模板(月)"
                                NavigateUrl="~/Reports/Templates/MRP/客户计划(月).xls" />
                        </td>
                    </tr>
                </table>
            </td>
            <td class="td01">请选择文件:
            </td>
            <td class="td02">
                <asp:FileUpload ID="fileUpload" ContentEditable="false" runat="server" />
                <%--<cc1:Button ID="btnImport" runat="server" Text="导入" OnClick="btnImport_Click" />--%>
                <asp:Button ID="btnImport" runat="server" Text="${Common.Button.Import}" OnClick="btnImport_Click"
                        CssClass="apply" />
            </td>
        </tr>

    </table>

    <asp:Literal ID="ltlPlanVersion" runat="server" />
    <div id="list" runat="server"></div>
</div>
<!----new-->
<div id="newOrder" runat="server" visible="false">
    <table class="mtable">
        <tr>
            <td class="td01">路线:
            </td>
            <td class="td02">
                <asp:Literal ID="ltlFlow" runat="server" /></td>
            <td class="td01"></td>
            <td class="td02"></td>
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
                                onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />
                        </td>
                        <td>
                            <asp:CheckBox ID="cbIsUrgent" runat="server" Text="${MasterData.Order.OrderHead.IsUrgent}" />
                        </td>
                    </tr>
                </table>
            </td>
            <td class="td01">
                <asp:Literal ID="lblStartTime" runat="server" Text="${MasterData.Order.OrderHead.StartTime}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartTime" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />
            </td>
        </tr>

        <tr>
            <td class="td01">
                <asp:Literal ID="lblRefOrderNo" runat="server" Text="${MasterData.Order.OrderHead.Flow.RefOrderNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbRefOrderNo" runat="server"></asp:TextBox>
            </td>
            <td class="td01">
                <asp:Literal ID="lblExtOrderNo" runat="server" Text="${MasterData.Order.OrderHead.ExtOrderNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbExtOrderNo" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td class="td01"></td>
            <td class="td02">
                <asp:CheckBox ID="cbReleaseOrder" runat="server" Text="${MasterData.Order.OrderHead.ReleaseOrder}" />
            </td>
            <td class="td01"></td>
            <td class="td02">
                <%--<cc1:Button ID="btnCreate" runat="server" Text="${Common.Button.Create}" OnClick="btnCreate_Click" />--%>
                <%--<asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" />--%>
            </td>
        </tr>
    </table>
    <asp:GridView ID="GV_Order" runat="server" AutoGenerateColumns="false" CellPadding="0" OnRowDataBound="GV_Order_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Seq">
                <ItemTemplate>
                    <%#Container.DataItemIndex + 1%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="${Common.Business.ItemCode}" DataField="Item" />
            <asp:BoundField HeaderText="${MRP.Schedule.ItemDescription}" DataField="ItemDescription" />
            <asp:BoundField HeaderText="${Common.Business.Uom}" DataField="Uom" />
            <asp:BoundField HeaderText="需求" DataField="Qty" DataFormatString="{0:0.##}" />
            <asp:TemplateField HeaderText="${MRP.Schedule.CurrentOrderQty}">
                <ItemTemplate>
                    <asp:TextBox ID="tbQty" runat="server" Width="50" Text='<%# Bind("Qty","{0:0.###}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
<script type="text/javascript">
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
</script>
