<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Warehouse_CheckASN_Search" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControlToolkit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblIpNo" runat="server" Text="${InProcessLocation.IpNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbIpNo" runat="server" Visible="true" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblStatus" runat="server" Text="${InProcessLocation.Status}:" />
            </td>
            <td class="td02">
 <asp:RadioButton ID="RadioButton1"   Text="全部" GroupName="State" runat="server" />
                <asp:RadioButton ID="RadioButton2"  Text="已确认" GroupName="State" runat="server" />
                <asp:RadioButton ID="RadioButton3" Checked="true" Text="未确认" GroupName="State" runat="server" />            </td>
        </tr>
        <%--   <tr>
            <td class="td01">
                <asp:Literal ID="lblPartyFrom" runat="server" Text="${MasterData.Order.OrderHead.PartyFrom.Supplier}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbPartyFrom" runat="server" Visible="true" Width="250" DescField="Name"
                    ValueField="Code" ServicePath="PartyMgr.service" ServiceMethod="GetFromParty" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblPartyTo" runat="server" Text="${MasterData.Order.OrderHead.PartyTo.Customer}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbPartyTo" runat="server" Visible="true" Width="250" DescField="Name"
                    ValueField="Code" MustMatch="true" ServicePath="PartyMgr.service" ServiceMethod="GetToParty" />
            </td>
        </tr>--%>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlStartDate" runat="server" Text="${MasterData.PlannedBill.CreateDateFrom}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlEndDate" runat="server" Text="${MasterData.PlannedBill.CreateDateTo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEndDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
        </tr>
        <%-- <tr runat="server" id="trDetails">
            <td class="td01">
                <asp:Literal ID="ltlOrderNo" runat="server" Text="${InProcessLocation.InProcessLocationDetail.OrderNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbOrderNo" runat="server" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlItem" runat="server" Text="${InProcessLocation.InProcessLocationDetail.Item}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItem" runat="server" Visible="true" DescField="Description" ImageUrlField="ImageUrl"
                    Width="280" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />
            </td>
        </tr>--%>
        <tr>
            <td class="td01" colspan="1">
            PartyTo:
            </td>
            <td>
                <uc3:textbox ID="tbPartyTo" runat="server" Width="250" DescField="Name" ValueField="Code"
                    MustMatch="true" ServiceMethod="GetAllParty" ServicePath="PartyMgr.service"/>
            </td>
            <td></td>
            <td class="td02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                    CssClass="button2" />
                 <asp:Button CssClass="button2" runat="server" ID="BatchConfirm" Text="批量确认"
                    onclick="BatchConfirm_Click" />

            </td>
        </tr>
    </table>
</fieldset>
<br />
 
