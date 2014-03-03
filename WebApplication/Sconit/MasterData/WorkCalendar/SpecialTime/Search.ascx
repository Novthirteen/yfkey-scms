<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="MasterData_WorkCalendar_SpecialTime_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblRegion" runat="server" Text="${Common.Business.Region}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbRegion" runat="server" DescField="Name" ValueField="Code" Width="200"
                    ServicePath="RegionMgr.service" ServiceMethod="GetRegion" MustMatch="true" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblWorkCenter" runat="server" Text="${Common.Business.WorkCenter}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbWorkCenter" runat="server" Visible="true" Width="250" DescField="Name"
                    ValueField="Code" ServicePath="WorkCenterMgr.service" ServiceMethod="GetWorkCenter"
                    ServiceParameter="string:#tbRegion" />
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="td02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                        CssClass="query" />
                    <asp:Button ID="btnNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click"
                        CssClass="add" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
