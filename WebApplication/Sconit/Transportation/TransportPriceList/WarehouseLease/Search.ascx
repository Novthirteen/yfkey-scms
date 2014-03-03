<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Transportation_TransportPriceList_WarehouseLease_Search" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>
<fieldset>
    <legend>
        <asp:Literal ID="lblCurrentTransportPriceList" runat="server" Text="${Transportation.TransportPriceListDetail.CurrentTransportPriceList}:" />
        <asp:Literal ID="lbCurrentTransportPriceList" runat="server" />
    </legend>
    <table class="mtable">
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblStartDate" runat="server" Text="${Transportation.TransportPriceListDetail.StartDate}:" />
            </td>
            <td class="ttd02">
                <asp:TextBox ID="tbStartDate" runat="server" Text='<%# Bind("StartDate") %>' onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
            <td class="ttd01">
                <asp:Literal ID="lblEndDate" runat="server" Text="${Transportation.TransportPriceListDetail.EndDate}:" />
            </td>
            <td class="ttd02">
                <asp:TextBox ID="tbEndDate" runat="server" Text='<%#Bind("EndDate") %>' onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="ttd02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                        CssClass="query" />
                    <asp:Button ID="btnNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click"
                        CssClass="add" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        CssClass="back" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
