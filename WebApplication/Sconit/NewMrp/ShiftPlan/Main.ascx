<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="NewMrp_ShiftPlan_Main" %>
<%@ Register Src="Preview.ascx" TagName="Preview" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<script language="javascript" type="text/javascript" src="Js/jquery.fixedtableheader-1-0-2.min.js"></script>
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
            <%--<td class="td01">工序:</td>
            <td class="td02">
                <cc1:CodeMstrDropDownList ID="ddlOperation" Code="MrpOpt" runat="server" IncludeBlankOption="true" DefaultSelectedValue="">
                </cc1:CodeMstrDropDownList>
            </td>--%>
            <td class="td01">
                生产线:
            </td>
            <td class="td02">
                <textarea id="tbFlow" rows="2" runat="server"  style="width:200" />
                <%-- <uc3:textbox ID="tbFlow" runat="server" DescField="Description" ValueField="Code"
                    ServicePath="FlowMgr.service" MustMatch="true" Width="250"
                    ServiceMethod="GetFlowList" />--%>
            </td>
            <td class="td01">
                开始时间:
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})"
                    Width="150" />
            </td>
        </tr>
        <tr>
            
            <td class="td01">
                版本号:
            </td>
            <td class="td02">
            <asp:TextBox ID="tbPlanVersion" runat="server" Width="150" /></td>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
        </tr>
        <%--<tr>
            <td>
            </td>
            <td class="td02">
                <asp:CheckBox ID="cbIsShow0" runat="server" Text="显示无需求明细" />
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>--%>
        <tr>
            <td class="td01">
            </td>
            <td class="td02">
                <asp:Literal ID="ltlPlanVersion" runat="server" />
            </td>
            <td class="td01">
            </td>
            <td class="td02">
                <asp:Button ID="btnSave" runat="server" Text="查询" OnClick="btnSearch_Click" />
                <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" OnClick="btnExport_Click" />
                <asp:Button ID="btnMrpCalculate" runat="server" Text="Mrp运算" OnClick="btnMrpCalculate_Click" />
            </td>
        </tr>
    </table>
    <table class="mtable" runat="server" id="tblImport" visible="false">
        <tr>
            <td class="td01">
                <%--模板:--%>
            </td>
            <td class="td02">
                <table>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <%--<asp:HyperLink ID="hlTemplate1" runat="server" Text="班产计划"
                                NavigateUrl="~/Reports/Templates/MRP/班产计划.xls" />--%>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="td01">
                请选择文件:
            </td>
            <td class="td02">
                <%--<input type="file" id="fileUpload" name="upVocFile" runat="server"/>--%>
                <asp:FileUpload ID="fileUpload" ContentEditable="false" runat="server" />
                <%--<cc1:Button ID="btnImport" runat="server" Text="导入" OnClick="btnImport_Click" /> --%>
                <asp:Button ID="btnImport" runat="server" Text="${Common.Button.Import}" OnClick="btnImport_Click"
                    CssClass="apply" />
            </td>
        </tr>
    </table>
    <div id="list" runat="server">
    </div>
</div>
<div id="newOrder" runat="server" visible="false">
    <table class="mtable">
        <tr>
            <td class="td01">
                生产线:
            </td>
            <td class="td02">
                <asp:Literal ID="ltlFlow" runat="server" />
            </td>
            <td class="td01">
                生产日期:
            </td>
            <td class="td02">
                <asp:Literal ID="ltlWindowTime" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td01">
            </td>
            <td class="td02">
                <asp:CheckBox ID="cbSplitOrder" runat="server" Text="一料一单" Checked="false" />
            </td>
            <td class="td01">
            </td>
            <td class="td02">
                <cc1:Button ID="btnCreate" runat="server" Text="${Common.Button.Create}" FunctionId="NewMrp_ShiftPlanToOrder"
                    OnClick="btnCreate_Click" />
                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
    <asp:GridView ID="GV_Order" runat="server" AutoGenerateColumns="false" CellPadding="0"
        OnRowDataBound="GV_Order_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Seq">
                <ItemTemplate>
                    <%#Container.DataItemIndex + 1%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="生产线" DataField="Flow" />
            <asp:BoundField HeaderText="${Common.Business.ItemCode}" DataField="Item" />
            <asp:BoundField HeaderText="${MRP.Schedule.ItemDescription}" DataField="ItemDescription" />
            <asp:BoundField HeaderText="${Common.Business.Uom}" DataField="Uom" />
            <asp:TemplateField HeaderText="班次1">
                <HeaderTemplate>
                    <asp:CheckBox ID="cbQtyA" runat="server" Text="班次1" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="tbQtyA" runat="server" Width="50" Text='<%# Bind("QtyA","{0:0.###}") %>'
                        Enabled="false" />
                    <asp:HiddenField ID="hfIdA" runat="server" Value='<%# Bind("IdA") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="备注1" DataField="MemoA" />
            <asp:TemplateField HeaderText="班次2">
                <HeaderTemplate>
                    <asp:CheckBox ID="cbQtyB" runat="server" Text="班次2" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="tbQtyB" runat="server" Width="50" Text='<%# Bind("QtyB","{0:0.###}") %>'
                        Enabled="false" />
                    <asp:HiddenField ID="hfIdB" runat="server" Value='<%# Bind("IdB") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="备注2" DataField="MemoB" />
            <asp:TemplateField HeaderText="班次3">
                <HeaderTemplate>
                    <asp:CheckBox ID="cbQtyC" runat="server" Text="班次3" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="tbQtyC" runat="server" Width="50" Text='<%# Bind("QtyC","{0:0.###}") %>'
                        Enabled="false" />
                    <asp:HiddenField ID="hfIdC" runat="server" Value='<%# Bind("IdC") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="备注3" DataField="MemoC" />
        </Columns>
    </asp:GridView>
</div>
<div id="floatdiv">
    <uc2:Preview ID="ucPreview" runat="server" Visible="false"  />
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
        $('.GV').fixedtableheader();
    });
</script>
