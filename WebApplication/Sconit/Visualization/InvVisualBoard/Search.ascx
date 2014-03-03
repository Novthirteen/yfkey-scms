<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Visualization_InvVisualBoard_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ac1" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblLocation" runat="server" Text="${MasterData.Flow.Location.To}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbLocation" runat="server" Visible="true" DescField="Name" Width="280"
                    ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocationByUserCode"
                    CssClass="inputRequired" MustMatch="false" />
                <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="tbLocation"
                    ErrorMessage="${MasterData.InvVisualBoard.Location.Empty}" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
            <td class="td01">
                <asp:Literal ID="ltlFlow" runat="server" Text="${Common.Business.Flow}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbFlow" runat="server" Visible="true" DescField="Description" Width="280"
                    ValueField="Code" ServicePath="FlowMgr.service" ServiceMethod="GetAllFlow" MustMatch="false" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblItem" runat="server" Text="${Common.Business.ItemCode}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItem" runat="server" MustMatch="false" Visible="true" DescField="Description"
                    ImageUrlField="ImageUrl" Width="280" ValueField="Code" ServicePath="ItemMgr.service"
                    ServiceMethod="GetCacheAllItem" />
            </td>
            <td class="td01">
                <%-- <asp:Literal ID="ltlDate" runat="server" Text="${MasterData.Order.OrderHead.WindowTime}:" />--%>
            </td>
            <td class="td02">
                <%--<asp:TextBox ID="tbDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />--%>
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="t02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" CssClass="button2"
                    OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
</fieldset>
