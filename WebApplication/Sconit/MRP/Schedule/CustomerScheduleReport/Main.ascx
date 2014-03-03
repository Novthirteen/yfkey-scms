<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MRP_Schedule_CustomerScheduleReport_Main" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script language="javascript" type="text/javascript">
    $(document).ready(function() {
        $('.GV').fixedtableheader();
    });
</script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblFlow" runat="server" Text="${Inventory.PrintHu.Flow}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbFlow" runat="server" DescField="Description" ValueField="Code"
                    CssClass="inputRequired" ServicePath="FlowMgr.service" MustMatch="true" Width="250"
                    ServiceMethod="GetFlowList" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblStartDate" runat="server" Text="${Common.Business.StartDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})"
                    Width="150" />
            </td>
        </tr>
        <tr>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
            <td />
            <td class="td02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" OnClick="btnExport_Click" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
<fieldset id="fld_Group" runat="server" visible="false">
    <legend>${MRP.Schedule.Group}</legend>
    <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_List_RowDataBound"
        EnableViewState="false" CellPadding="0">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblSequence" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MRP.Schedule.Item}">
                <ItemTemplate>
                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item")%>' ToolTip='<%# Eval("ItemDescription")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="${Common.Business.RefCode}" DataField="ItemReference" />
            <asp:TemplateField HeaderText="${Common.Business.Uom}">
                <ItemTemplate>
                    <asp:Label ID="lblUom" runat="server" Text='<%# Eval("Uom")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="${MRP.Schedule.UnitCount}" DataField="UnitCount" DataFormatString="{0:0.########}" />
            <asp:BoundField HeaderText="${MRP.Schedule.Location}" DataField="Location" />
        </Columns>
    </asp:GridView>
    <asp:Literal ID="ltl_GV_List_Result" runat="server" Text="${Common.GridView.NoRecordFound}" />
</fieldset>
