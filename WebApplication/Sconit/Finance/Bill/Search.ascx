<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Finance_Bill_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Bill.BillNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbCode" runat="server" Visible="true" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlStatus" runat="server" Text="${MasterData.Bill.Status}:" />
            </td>
            <td class="td02">
                <asp:DropDownList ID="ddlStatus" runat="server" DataTextField="Description" DataValueField="Value" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlPartyCode" runat="server" Text="${MasterData.Bill.Supplier}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbPartyCode" runat="server" DescField="Name" ValueField="Code" ServicePath="SupplierMgr.service"
                    ServiceMethod="GetAllSupplier" Width="250" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlExternalBillNo" runat="server" Text="${MasterData.Bill.ExternalBillNo}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbExternalBillNo" runat="server" Visible="true" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlStartDate" runat="server" Text="${Common.Business.StartDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlEndDate" runat="server" Text="${Common.Business.EndDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEndDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlHasProvEst" runat="server" Text="含暂估价:" />
            </td>
            <td class="td02">
                <asp:DropDownList ID="ddlHasProvEst" runat="server">
                    <asp:ListItem Text="" Value ="" Selected="True"/>
                    <asp:ListItem Text="仅显示含暂估" Value ="True"/>
                    <asp:ListItem Text="仅显示不含暂估价" Value ="False"/>
                </asp:DropDownList>
            </td>
            <td/>
            <td class="ttd02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                    Width="59px" CssClass="button2" />
                <cc1:Button ID="btnNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click"
                    Width="59px" CssClass="button2"   FunctionId="EditBill"/>
            </td>
        </tr>
    </table>
</fieldset>
