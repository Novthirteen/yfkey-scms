<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="NewMrp_TransferPlan_Main" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<script language="javascript" type="text/javascript" src="Js/jquery.fixedtableheader-1-0-2.min.js"></script>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<table class="mtable">
    <tr>
        <td class="td01">移库路线:
        </td>
        <td class="td02">
            <asp:DropDownList ID="ddlFlow" runat="server" DataTextField="Description" DataValueField="Code" />
        </td>
        <td />
        <td />
    </tr>
    <tr>
        <td class="td01">生产日期:</td>
        <td class="td02">
            <asp:TextBox ID="tbPlanDateDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})"
                Width="100" CssClass="inputRequired" />
            <asp:RequiredFieldValidator ID="rtvPlanDate" runat="server" ErrorMessage="生产日期不能为空"
                Display="Dynamic" ControlToValidate="tbPlanDateDate" ValidationGroup="vgSave" />
        </td>
        <td class="td01">班次:</td>
        <td class="td02">
            <asp:DropDownList ID="ddlShift" runat="server" DataTextField="ShiftName" DataValueField="Code" />
        </td>
    </tr>
    <tr>
        <td />
        <td />
        <td class="td01"></td>
        <td class="td02">
            <%--<cc1:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click" ValidationGroup="vgSave" />--%>
            <%--<cc1:Button ID="btnCreate" runat="server" Text="${Common.Button.Create}" FunctionId="NewMrp_TransferPlanToOrder" OnClick="btnCreate_Click" ValidationGroup="vgSave" />--%>
            <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"  CssClass="apply" />
            <asp:Button ID="btnCreate" runat="server" Text="${Common.Button.Create}" OnClick="btnCreate_Click"  CssClass="apply" />
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
        <asp:BoundField HeaderText="路线" DataField="FlowCode" />
        <asp:BoundField HeaderText="${Common.Business.ItemCode}" DataField="ItemCode" />
        <asp:BoundField HeaderText="${MRP.Schedule.ItemDescription}" DataField="ItemDescription" />
        <asp:BoundField HeaderText="${Common.Business.Uom}" DataField="UomCode" />
        <asp:BoundField HeaderText="单包装" DataField="Uc" />
        <asp:BoundField HeaderText="安全库存" DataField="SafeStock" DataFormatString="{0:0.##}" />
        <asp:BoundField HeaderText="当前库存" DataField="InvQty" DataFormatString="{0:0.##}" />
        <asp:BoundField HeaderText="待收" DataField="InQty" DataFormatString="{0:0.##}" Visible="false" />
        <asp:BoundField HeaderText="需求" DataField="OutQty" DataFormatString="{0:0.##}" />
        <asp:BoundField HeaderText="净需求" DataField="Qty" DataFormatString="{0:0.##}" Visible="false" />
        <asp:TemplateField HeaderText="订单数">
            <ItemTemplate>
                <asp:TextBox ID="tbQty" runat="server" Width="50" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

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



