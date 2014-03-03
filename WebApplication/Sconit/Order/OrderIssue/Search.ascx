<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Distribution_OrderIssue_Search" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblFlow" runat="server" Text="${MasterData.Flow.Flow.Distribution}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbFlow" runat="server" Visible="true" DescField="Description" ValueField="Code"
                    ServiceMethod="GetFlowList" ServicePath="FlowMgr.service" OnTextChanged="tbFlow_TextChanged"
                    AutoPostBack="true" MustMatch="true" Width="250" />
            </td>
            <td class="td01">
             <%--   <asp:Literal ID="ltlOrderSubType" runat="server" Text="${MasterData.Order.OrderHead.SubType}:" />--%>
            </td>
            <td class="td02">
               <%-- <cc1:CodeMstrDropDownList ID="ddlOrderSubType" Code="OrderSubType" runat="server"
                    OnTextChanged="tbFlow_TextChanged" AutoPostBack="true">
                </cc1:CodeMstrDropDownList>--%>
            </td>
        </tr>
    </table>
</fieldset>
