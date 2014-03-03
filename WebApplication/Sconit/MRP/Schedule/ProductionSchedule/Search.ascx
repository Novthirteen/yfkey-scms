<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="MRP_Schedule_ProductionSchedule_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="ASTreeView" Namespace="Geekees.Common.Controls" TagPrefix="ct" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblFlow" runat="server" Text="${Inventory.PrintHu.Flow}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbFlow" runat="server" DescField="Description" ValueField="Code"
                    ServicePath="FlowMgr.service" MustMatch="true" Width="250" ServiceMethod="GetMRPFlow" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblRefOrderNo" runat="server" Text="${MRP.Schedule.ProductionScheduleNo}:"  />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbRefOrderNo" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblStartDate" runat="server" Text="${Common.Business.StartDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})"
                    Width="100" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlEndDate" runat="server" Text="${Common.Business.EndDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEndDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})"
                    Width="100" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblStatus" runat="server" Text="${MasterData.Order.OrderHead.Status}:" />
            </td>
            <td class="td02">
                <ct:ASDropDownTreeView ID="astvMyTree" runat="server" BasePath="~/Js/astreeview/"
                    DataTableRootNodeValue="0" EnableRoot="false" EnableNodeSelection="false" EnableCheckbox="true"
                    EnableDragDrop="false" EnableTreeLines="false" EnableNodeIcon="false" EnableCustomizedNodeIcon="false"
                    EnableDebugMode="false" EnableRequiredValidator="false" InitialDropdownText=""
                    Width="170" EnableCloseOnOutsideClick="true" EnableHalfCheckedAsChecked="true"
                    DropdownIconDown="~/Js/astreeview/images/windropdown.gif" EnableContextMenuAdd="false"
                    MaxDropdownHeight="200" />
            </td>
            <td />
            <td class="td02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
<fieldset id="fld_Details" runat="server" visible="false">
    <legend>${MRP.Schedule.CustomerSchedule}</legend>
    <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_List_RowDataBound"
        CellPadding="0">
        <Columns>
            <asp:TemplateField HeaderText="${MRP.Schedule.ProductionScheduleNo}">
                <ItemTemplate>
                    <asp:Literal ID="lblReferenceOrderNo" runat="server" Text='<%# Eval("ReferenceScheduleNo")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MRP.Schedule.Flow}">
                <ItemTemplate>
                    <asp:Literal ID="lblFlow" runat="server" Text='<%# Eval("Flow")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MRP.Schedule.Status}">
                <ItemTemplate>
                    <asp:Literal ID="ltlStatus" runat="server" Text='<%# Eval("Status")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MRP.Schedule.CreateUser}">
                <ItemTemplate>
                    <asp:Literal ID="ltlCreateUser" runat="server" Text='<%# Eval("CreateUser")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MRP.Schedule.CreateTime}">
                <ItemTemplate>
                    <asp:Literal ID="ltlCreateDate" runat="server" Text='<%# Bind("CreateDate","{0:yyyy-MM-dd HH:mm}")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MRP.Schedule.ReleaseUser}">
                <ItemTemplate>
                    <asp:Literal ID="ltlReleaseUser" runat="server" Text='<%# Eval("ReleaseUser")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MRP.Schedule.ReleaseTime}">
                <ItemTemplate>
                    <asp:Literal ID="ltlReleaseDate" runat="server" Text='<%# Bind("ReleaseDate","{0:yyyy-MM-dd HH:mm}")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.GridView.Action}">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                        Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Literal ID="ltl_Result" runat="server" Text="${Common.GridView.NoRecordFound}"
        Visible="false" />
</fieldset>
