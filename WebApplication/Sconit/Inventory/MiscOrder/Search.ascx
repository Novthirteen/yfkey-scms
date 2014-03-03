<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="MasterData_MiscOrder_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ac1" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblCustomerSupplierCode" runat="server" Text="${Common.Business.ID}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbMiscOrderCode" runat="server" Visible="true" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblMiscOrderEffectDate" runat="server" Text="${MasterData.MiscOrder.EffectDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbMiscOrderEffectDate" runat="server" onClick="WdatePicker()" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblMiscOrderRegion" runat="server" Text="${Common.Business.Region}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbMiscOrderRegion" runat="server" Visible="true" Width="250" DescField="Name"
                    ValueField="Code" MustMatch="true" ServicePath="RegionMgr.service" ServiceMethod="GetRegion" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblMIscOrderLocation" runat="server" Text="${Common.Business.Location}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbMiscOrderLocation" runat="server" Visible="true" DescField="Name"
                    ValueField="Code" Width="250" ServicePath="LocationMgr.service" ServiceMethod="GetLocation"
                    ServiceParameter="string:#tbMiscOrderRegion" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblSubjectCode" runat="server" Text="${MasterData.MiscOrder.SubjectCode}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbSubjectCode" runat="server" Visible="true" Width="250" DescField="SubjectName"
                    ValueField="SubjectCode" ServicePath="SubjectListMgr.service" ServiceMethod="GetAllSubject" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblCostCenterCode" runat="server" Text="${MasterData.MiscOrder.CostCenterCode}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbCostCenterCode" runat="server" Visible="true" Width="250" DescField="CostCenterName"
                    ValueField="CostCenterCode" ServicePath="SubjectListMgr.service" ServiceMethod="GetSubjectList"
                    ServiceParameter="string:#tbSubjectCode" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblStartDate" runat="server" Text="${Common.Business.StartDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="startDate" runat="server" onClick="WdatePicker()" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblEndDate" runat="server" Text="${Common.Business.EndDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="endDate" runat="server" />
                <ac1:CalendarExtender ID="CalendarExtender3" TargetControlID="endDate" Format="yyyy-MM-dd HH:mm"
                    runat="server">
                </ac1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="ttd02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                    Width="59px" CssClass="button2" />
                <asp:Literal ID="tbTransactionType" runat="server" Visible="false" />
                <asp:Button ID="btnBackAdd" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click"
                    Width="59px" />
            </td>
        </tr>
    </table>
</fieldset>
