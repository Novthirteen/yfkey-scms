<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Jobs_Log_Main" %>
<%@ Register Src="List.ascx" TagName="List" TagPrefix="uc2" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<div id="floatdiv">
    <fieldset>
        <table class="mtable">
         <tr>
                <td class="td01">
                    <asp:Literal ID="ltlJobName" runat="server" Text="${MasterData.Jobs.Name}:" />
                </td>
                <td class="td02">
                    <asp:Label ID="lbJobName" runat="server"  />
                </td>
                <td class="td01">
                    <asp:Literal ID="ltlJobDescription" runat="server" Text="${MasterData.Jobs.Description}:" />
                </td>
                <td class="td02">
                    <asp:Label ID="lbJobDescription" runat="server"  />
                </td>
            </tr>
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
            <tr>
                <td colspan="3" />
                <td class="ttd02">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                        CssClass="button2" />
                    <asp:Button ID="btnClose" runat="server" Text="${Common.Button.Close}" OnClick="btnClose_Click"
                        CssClass="button2" />
                </td>
            </tr>
        </table>
    </fieldset>
    <uc2:List ID="ucList" runat="server" Visible="false" />
</div>
