<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Transportation_Vehicle_Search" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblCode" runat="server" Text="${Transportation.Vehicle.Code}:" />
            </td>
            <td class="ttd02">
                <asp:TextBox ID="tbCode" runat="server" Visible="true" />
            </td>
            <td class="ttd01">
                <asp:Literal ID="lblCarrier" runat="server" Text="${Transportation.Vehicle.Carrier}:" />
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbCarrier" runat="server" Width="250" DescField="Name" ValueField="Code"
                    MustMatch="true" ServiceMethod="GetCarrier" ServicePath="CarrierMgr.service"/>
            </td>
        </tr>
        <tr>
            <td class="ttd01">
                <asp:Literal ID="lblType" runat="server" Text="${Transportation.Vehicle.Type}:" />
            </td>
            <td class="ttd02">
                <cc1:CodeMstrDropDownList ID="ddlType" Code="VehicleType" runat="server" IncludeBlankOption="true"
                    DefaultSelectedValue="">
                </cc1:CodeMstrDropDownList>
            </td>
            <td class="ttd01">
            </td>
            <td class="ttd02">
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="ttd02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                        CssClass="query" />
                    <asp:Button ID="btnNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click"
                        CssClass="back" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
