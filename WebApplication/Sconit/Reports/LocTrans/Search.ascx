<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Reports_LocTrans_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlTransType" runat="server" Text="${Visualization.GoodsTraceability.TransType}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbTransType" runat="server" DescField="Description" ValueField="Value"
                    ServicePath="CodeMasterMgr.service" ServiceMethod="GetCachedCodeMaster" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblLocation" runat="server" Text="${Common.Business.Location}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbLocation" runat="server" Visible="true" DescField="Name" Width="280"
                    ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocationByUserCode" />
            </td>
        </tr>
        <tr><td class="td01">
                <asp:Literal ID="lblItem" runat="server" Text="${Common.Business.ItemCode}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItem" runat="server" Visible="true" DescField="Description" ImageUrlField="ImageUrl"
                    Width="280" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" CssClass="inputRequired"/>
                <asp:RequiredFieldValidator ID="rfvItemCode" runat="server" ControlToValidate="tbItem"
                    Display="Dynamic" ErrorMessage="${MasterData.Flow.FlowDetail.ItemCode.Required}"
                    ValidationGroup="vgSearch"  />
            </td>
            <!--djin 20120802-->
           <td class="td01">目标库位:</td>
           <td class="td02"> <uc3:textbox ID="tbRefLoc" runat="server" Visible="true" DescField="Name" Width="280"
                    ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocationByUserCode" /></td>
                    <!--end-->
        </tr>
        <tr>
        <td class="td01">
                <asp:Literal ID="lblStartDate" runat="server" Text="${Common.Business.StartDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                    CssClass="inputRequired" />
                <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="${Common.Business.StartDate.Empty}"
                    Display="Dynamic" ControlToValidate="tbStartDate" ValidationGroup="vgSearch" />
            </td>
            
          
             <td class="td01">
                <asp:Literal ID="lblEndDate" runat="server" Text="${Common.Business.EndDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEndDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                    CssClass="inputRequired" />
                <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ErrorMessage="${Common.Business.EndDate.Empty}"
                    Display="Dynamic" ControlToValidate="tbEndDate" ValidationGroup="vgSearch" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlSumParam" runat="server" Text="${Reports.SummarizeParameters}:" />
            </td>
            <td class="td02">
                <%--<asp:CheckBox ID="cbSumType" runat="server" Text="${Common.Business.Type}" />--%>
                <asp:CheckBox ID="cbSumDate" runat="server" Text="${Common.Business.Date}" />
                <asp:CheckBox ID="cbSumUser" runat="server" Text="${Common.Business.User}" />
            </td>
            <td class="td01">
            </td>
            <td class="t02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" CssClass="query"
                        OnClick="btnSearch_Click" ValidationGroup="vgSearch" />
                    <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" CssClass="button2"
                        OnClick="btnExport_Click" ValidationGroup="vgSearch" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
