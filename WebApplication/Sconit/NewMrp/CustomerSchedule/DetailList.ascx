<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DetailList.ascx.cs" Inherits="NewMrp_ShipPlan_DetailList" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>
<div id="search" runat="server">
    <table class="mtable" runat="server" id="tblSearch">
        <tr>
           <%-- <td class="td01">
                路线:
            </td>
            <td class="td02">
                <uc3:textbox ID="tbFlow" runat="server" DescField="Description" ValueField="Code"
                    ServicePath="FlowMgr.service" MustMatch="true" Width="250" ServiceMethod="GetFlowList" />
            </td>--%>
            <td class="td01">
                物料代码
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItemCode" runat="server" Visible="true" Width="250" MustMatch="true"
                    DescField="Description" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />
            </td>
        </tr>
        <tr>
            <td class="ttd01">
            </td>
            <td class="ttd02">
            </td>
            <td>
            </td>
            <td class="td02">
                <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
                <asp:Button ID="btnBack" runat="server" Text=" 返回" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
    <div id="list" runat="server">
    </div>
    <asp:Literal ID="ltlPlanVersion" runat="server" />
    <div id="mstrList" runat="server">
    </div>
    <div id="ShowTraceDiv" style="position:absolute;width:500px; ">
    </div>
    <div id="ShowDetsDiv" style="position:absolute; width:500px;">
    </div>
</div>
