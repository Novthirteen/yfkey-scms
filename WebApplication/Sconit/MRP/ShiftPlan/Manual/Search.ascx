<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="MRP_ShiftPlan_Manual_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ac1" %>
<%@ Register Src="Shift.ascx" TagName="Shift" TagPrefix="uc" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlRegion" runat="server" Text="${Common.Business.Region}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbRegion" runat="server" DescField="Name" ValueField="Code" Width="200"
                    ServicePath="RegionMgr.service" ServiceMethod="GetRegion" MustMatch="true" CssClass="inputRequired"
                    AutoPostBack="true" OnTextChanged="tbRegion_TextChanged" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlProductionLine" runat="server" Text="${Common.Business.ProductionLine}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbFlow" runat="server" Visible="true" DescField="Description" Width="280"
                    ValueField="Code" ServicePath="ItemFlowPlanMgr.service" ServiceMethod="GetFlowByPartyAndPlanType"
                    ServiceParameter="string:#tbRegion,string:MPS" MustMatch="true" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlDate" runat="server" Text="${Common.Business.Date}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbDate" runat="server" AutoPostBack="true" OnTextChanged="tbDate_TextChanged"
                    onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlShift" runat="server" Text="${MasterData.WorkCalendar.Shift}:" />
            </td>
            <td class="td02">
                <asp:UpdatePanel ID="UP_Shift" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <uc:Shift ID="ucShift" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="tbRegion" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="tbDate" EventName="TextChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblItemCode" runat="server" Text="${Common.Business.ItemCode}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItemCode" runat="server" Visible="true" DescField="Description"
                    Width="280" ValueField="Code" ServicePath="FlowDetailMgr.service" ServiceMethod="GetAllFlowDetailItem"
                    ServiceParameter="string:#tbFlow" />
            </td>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="ttd02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                        CssClass="query" />
                    <asp:Button ID="btnSave" runat="server" Text="${Common.Button.Save}" OnClick="btnSave_Click"
                        CssClass="apply" />
                    <asp:Button ID="btnGenOrders" runat="server" Text="${ShiftPlanSchedule.Button.GenerateWorkOrder}"
                        OnClick="btnGenOrders_Click" CssClass="add" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
