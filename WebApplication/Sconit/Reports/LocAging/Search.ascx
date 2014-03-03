<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Reports_LocAging_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlRegion" runat="server" Text="${Common.Business.Region}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbRegion" runat="server" DescField="Name" ValueField="Code" Width="200"
                    ServicePath="RegionMgr.service" ServiceMethod="GetRegion" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblLocation" runat="server" Text="${Common.Business.Location}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbLocation" runat="server" Visible="true" DescField="Name" Width="280"
                    ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocationByUserCode" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblDate" runat="server" Text="${Common.Business.Date}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblItem" runat="server" Text="${Common.Business.ItemCode}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItem" runat="server" Visible="true" DescField="Description" ImageUrlField="ImageUrl"
                    Width="280" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlDays" runat="server" Text="${Reports.InvAging.Days}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbDays" runat="server" />
                <asp:RangeValidator ID="rvDays" ControlToValidate="tbDays" runat="server" Display="Dynamic"
                    ErrorMessage="*" MaximumValue="999999999" MinimumValue="0" Type="Integer" />
            </td>
            <td colspan="2" />
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlClassifiedParam" runat="server" Text="${Reports.Subtotal}:" />
            </td>
            <td class="td02">
                <asp:CheckBox ID="cbClassifiedParty" runat="server" Text="${Common.Business.Region}" />
                <asp:CheckBox ID="cbClassifiedLocation" runat="server" Text="${Common.Business.Location}" />
            </td>
            <td class="td01">
            </td>
            <td class="t02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" CssClass="query"
                        OnClick="btnSearch_Click" />
                    <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" CssClass="button2"
                    	OnClick="btnExport_Click" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
