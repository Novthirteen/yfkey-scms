<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Reports_InvDetail_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ac1" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblLocation" runat="server" Text="${Common.Business.Location}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbLocation" runat="server" Visible="true" DescField="Name" Width="280"
                    ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocationByUserCode"
                    MustMatch="false" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblItem" runat="server" Text="${Common.Business.ItemCode}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItem" runat="server" Visible="true" DescField="Description" ImageUrlField="ImageUrl"
                    Width="280" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem"
                    MustMatch="false"/>
                <asp:RequiredFieldValidator ID="rfvItemCode" runat="server" ControlToValidate="tbItem"
                    Display="Dynamic" ErrorMessage="${MasterData.Flow.FlowDetail.ItemCode.Required}"
                    ValidationGroup="vgSearch" />
            </td>
        </tr>
        <tr id="trLocation" runat="server">
            <td class="td01">
                <asp:Literal ID="lblType" runat="server" Text="${MasterData.Location.Type}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbType" runat="server" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblPlan" runat="server" Text="${MasterData.Location.VisiblePan}:" />
            </td>
            <td class="td02">
                <asp:CheckBox ID="cbPlan" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblEffDate" runat="server" Text="${Common.Business.Date}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEffDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
            <td />
            <td class="t02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" CssClass="button2"
                    OnClick="btnSearch_Click"  ValidationGroup="vgSearch"  />
                <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" CssClass="button2"
                    OnClick="btnExport_Click" ValidationGroup="vgSearch" />
            </td>
        </tr>
    </table>
</fieldset>
