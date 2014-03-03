<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DateSelect.ascx.cs" Inherits="MRP_PlanSchedule_DateSelect" %>
<tr>
    <td class="td01">
        <asp:Literal ID="lblTimePeriodType" runat="server" Text="${Common.CodeMaster.TimePeriodType}:" />
    </td>
    <td class="td02">
        <table>
            <tr>
                <td>
                    <asp:DropDownList ID="ddlTimePeriodType" runat="server" DataTextField="Description"
                        DataValueField="Value" AutoPostBack="true" OnSelectedIndexChanged="ddlTimePeriodType_SelectedIndexChanged" />
                </td>
                <td>
                    <asp:UpdatePanel ID="UP_Info" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="lblInfo" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlTimePeriodType" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="tbDate" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </td>
    <td class="td01">
        <asp:Literal ID="ltlDate" runat="server" Text="${Common.Business.Date}:" />
    </td>
    <td class="td02">
        <asp:UpdatePanel ID="UP_Date" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:TextBox ID="tbDate" runat="server" AutoPostBack="true" OnTextChanged="tbDate_TextChanged"
                    onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlTimePeriodType" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </td>
</tr>
