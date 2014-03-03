<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="MRP_PlanSchedule_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="DateSelect.ascx" TagName="DateSelect" TagPrefix="uc2" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <uc2:DateSelect ID="ucDateSelect" runat="server" />
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblRegion" runat="server" Text="${Common.Business.Region}:" />
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbRegion" runat="server" DescField="Name" ValueField="Code" Width="200"
                    ServicePath="RegionMgr.service" ServiceMethod="GetRegion" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblFlow" runat="server" Text="${Common.Business.Flow}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbFlow" runat="server" Visible="true" DescField="Description" Width="280"
                    ValueField="Code" ServicePath="FlowMgr.service" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblItemCode" runat="server" Text="${Common.Business.ItemCode}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItemCode" runat="server" Visible="true" DescField="Description"
                    ImageUrlField="ImageUrl" Width="280" ValueField="Code" ServicePath="ItemMgr.service"
                    ServiceMethod="GetCacheAllItem" />
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
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click" CssClass="query"/>
                    <asp:Button ID="btnSave" runat="server" Text="${Common.Button.Save}" OnClick="btnSave_Click" CssClass="apply"/>
                    <asp:Button ID="btnRelease" runat="server" Text="${MRP.Release}" OnClick="btnRelease_Click" CssClass="apply"/>
                    <asp:Button ID="btnRun" runat="server" Text="${MRP.AutoPlan}" OnClick="btnRun_Click" CssClass="apply"
                        Visible="false" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
