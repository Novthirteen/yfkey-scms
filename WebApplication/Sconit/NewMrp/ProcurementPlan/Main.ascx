<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="NewMrp_ProcurementPlan_Main" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<script language="javascript" type="text/javascript" src="Js/jquery.fixedtableheader-1-0-2.min.js"></script>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<div id="search" runat="server">
    <table class="mtable" runat="server" id="tblSearch">
        <tr>
            <td class="td01">路线:
            </td>
            <td class="td02">
                <%--<uc3:textbox ID="tbFlow" runat="server" DescField="Description" ValueField="Code"
                    ServicePath="FlowMgr.service" MustMatch="true" Width="250"
                    ServiceMethod="GetFlowList" />--%>
                <textarea id="tbFlow" rows="2" runat="server"  style="width:200" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltLocation" runat="server" Text="库位:"></asp:Literal></td>
            <td class="td02">
                <uc3:textbox ID="tbLocation" runat="server" Visible="true" DescField="Name" Width="250"
                    ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetMrpLocation"
                    MustMatch="true" />
            </td>
        </tr>

        <tr id="trSupplierItem" runat="server">
            <td class="td01">供应商:
            </td>
            <td class="td02">
                <uc3:textbox ID="tbSupplier" runat="server" Width="250" DescField="Name" MustMatch="true"
                    ValueField="Code" ServicePath="SupplierMgr.service" ServiceMethod="GetAllSupplier" />
            </td>

            <td class="td01">物料:
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItem" runat="server" Width="250" DescField="Item" MustMatch="true"
                    ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />
            </td>
        </tr>
        <tr>
            
            <td class="td01">
                版本号:
            </td>
            <td class="td02">
            <asp:TextBox ID="tbPlanVersion" runat="server" Width="150" /></td>
            <td class="td01">开始时间:
            </td>
            <td class="td02">
            <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true,minDate:'%y-%M-%d'})"
                    Width="100" />
            </td>
        </tr>
        <tr>
            <td class="td01">
            </td>
            <td class="td02">
                
                <%--<asp:CheckBox ID="cbIsShow0" runat="server" Text="显示无需求明细" />--%>
            </td>
            <td class="td01">
                <%--<asp:RadioButtonList ID="rblDateType" runat="server" RepeatDirection="Horizontal" CssClass="floatright">
                </asp:RadioButtonList>--%>
            </td>
            <td class="td02">
                <asp:Button ID="btnSave" runat="server" Text="查询" OnClick="btnSearch_Click" />
                <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" OnClick="btnExport_Click" />
                <asp:Button ID="btnImportShow" runat="server" Text="${Common.Button.Import}" OnClick="btnImport_ClickShow" />
            </td>
        </tr>
    </table>

    <div id="list" runat="server">
    </div>
</div>
<div id="floatdiv">
    <table class="mtable" runat="server" id="tblImport" visible="false">
        <tr>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
             <td class="td01">
            </td>
            <td class="td02">
            </td>
            </tr>
            <tr>
             <td class="td01">
            </td>
            <td class="td02">
            </td>
            <td class="td01">请选择文件:
            </td>
            <td class="td02">
                <asp:FileUpload ID="fileUpload" ContentEditable="false" runat="server" />
                <asp:Button ID="btnImport" runat="server" Text="${Common.Button.Import}" OnClick="btnImport_Click"
                        CssClass="apply" />
             <asp:Button ID="Button1" runat="server" Text="Back" OnClick="btnImportBack_Click" CssClass="back" />
            </td>
        </tr>

    </table>
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
            <td class="td01"></td>
            <td class="td02">
                <asp:CheckBox ID="cbReleaseOrder" runat="server" Text="${MasterData.Order.OrderHead.ReleaseOrder}" />
            </td>
            <td class="td01"></td>
            <td class="td02">
                <asp:Button ID="btnCreate" runat="server" Text="${Common.Button.Create}" OnClick="btnCreate_Click" />
                <%--<cc1:Button ID="btnCreate" runat="server" Text="${Common.Button.Create}" OnClick="btnCreate_Click" />--%>
                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" />
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
            <asp:BoundField HeaderText="${Common.Business.ItemCode}" DataField="ItemCode" />
            <asp:BoundField HeaderText="${MRP.Schedule.ItemDescription}" DataField="ItemDescription" />
            <asp:BoundField HeaderText="${Common.Business.Uom}" DataField="UomCode" />
            <asp:BoundField HeaderText="单包装" DataField="Uc" />
            <asp:BoundField HeaderText="安全库存" DataField="SafeStock" DataFormatString="{0:0.##}" />
            <asp:BoundField HeaderText="库存" DataField="InvQty" DataFormatString="{0:0.##}" />
            <asp:BoundField HeaderText="待收" DataField="InQty" DataFormatString="{0:0.##}" />
            <asp:BoundField HeaderText="需求" DataField="OutQty" DataFormatString="{0:0.##}" />
            <asp:BoundField HeaderText="净需求" DataField="Qty" DataFormatString="{0:0.##}" />
            <asp:TemplateField HeaderText="${MRP.Schedule.CurrentOrderQty}">
                <ItemTemplate>
                    <asp:TextBox ID="tbQty" runat="server" Width="50" />
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
    $(document).ready(function () {
        $('.GV').fixedtableheader({
            headerrowsize: 2,
            highlightrow: true
        });
    });
</script>
