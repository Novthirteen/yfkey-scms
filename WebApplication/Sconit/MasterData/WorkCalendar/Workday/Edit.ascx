<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_WorkCalendar_Workday_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script type="text/javascript" language="javascript">
    function idNotInWorkdayClick() {
        if ($("#idNotInWorkday input:checkbox").attr("checked") == true) {
            $("#idNotInWorkdayList input:checkbox").each(function(index, domEle) {
                if (this.type == "checkbox")
                    this.checked = true;
            });
        }
        else {
            $("#idNotInWorkdayList input:checkbox").each(function(index, domEle) {
                if (this.type == "checkbox")
                    this.checked = false;
            });
        }
    }

    function idInWorkdayClick() {
        if ($("#idInWorkday input:checkbox").attr("checked") == true) {
            $("#idInWorkdayList input:checkbox").each(function(index, domEle) {
                if (this.type == "checkbox")
                    this.checked = true;
            });
        }
        else {
            $("#idInWorkdayList input:checkbox").each(function(index, domEle) {
                if (this.type == "checkbox")
                    this.checked = false;
            });
        }
    }
</script>

<div id="divFV" runat="server">
    <asp:FormView ID="FV_Workday" runat="server" DataSourceID="ODS_Workday" DefaultMode="Edit"
        Width="100%" DataKeyNames="Id" OnDataBound="FV_Workday_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.WorkCalendar.Workday.UpdateWorkday}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblRegion" runat="server" Text="${Common.Business.Region}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbRegion" runat="server" ReadOnly="true" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblWorkCenter" runat="server" Text="${Common.Business.WorkCenter}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbWorkCenter" runat="server" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblDayOfWeek" runat="server" Text="${MasterData.WorkCalendar.Workday.Week}:" />
                        </td>
                        <td class="td02">
                            <asp:DropDownList ID="DayOfWeek_DDL" runat="server" Text='<%# Bind("DayOfWeek") %>'
                                Enabled="false">
                                <asp:ListItem Text="${Common.Week.Monday}" Value="Monday"></asp:ListItem>
                                <asp:ListItem Text="${Common.Week.Tuesday}" Value="Tuesday"></asp:ListItem>
                                <asp:ListItem Text="${Common.Week.Wednesday}" Value="Wednesday"></asp:ListItem>
                                <asp:ListItem Text="${Common.Week.Thursday}" Value="Thursday"></asp:ListItem>
                                <asp:ListItem Text="${Common.Week.Friday}" Value="Friday"></asp:ListItem>
                                <asp:ListItem Text="${Common.Week.Saturday}" Value="Saturday"></asp:ListItem>
                                <asp:ListItem Text="${Common.Week.Sunday}" Value="Sunday"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblType" runat="server" Text="${MasterData.WorkCalendar.SpecialTime.Type}:" />
                        </td>
                        <td class="td02">
                            <asp:RadioButtonList ID="rblType" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                DataValueField='<%#Bind("Type") %>'>
                                <asp:ListItem Selected="True" Text="${MasterData.WorkCalendar.Type.Work}" Value="Work" />
                                <asp:ListItem Text="${MasterData.WorkCalendar.Type.Rest}" Value="Rest" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblDescription" runat="server" Text="${MasterData.WorkCalendar.Description}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbDescription" runat="server" Text='<%#Bind("Description") %>' Width="250" />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div class="tablefooter">
                <div class="buttons">
                    <asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="${Common.Button.Save}" CssClass="add"
                        ValidationGroup="vgSave" />
                    <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}" CssClass="delete"
                        OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="back" />
                </div>
            </div>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Workday" runat="server" TypeName="com.Sconit.Web.WorkdayMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Workday" UpdateMethod="UpdateWorkday"
    OnUpdated="ODS_Workday_Updated" OnUpdating="ODS_Workday_Updating" DeleteMethod="DeleteWorkday"
    OnDeleted="ODS_Workday_Deleted" SelectMethod="LoadWorkday">
    <SelectParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </DeleteParameters>
</asp:ObjectDataSource>
<div id="divWorkdayShift" runat="server">
    <fieldset>
        <table width="100%">
            <tr>
                <td style="width: 10%">
                    ${MasterData.WorkCalendar.NotInWorkday}:
                </td>
                <td style="width: 30%" id="idNotInWorkday" onclick="idNotInWorkdayClick()">
                    <asp:CheckBox ID="cb_NotInWorkday" runat="server" Text="${Common.Select.All}" />
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                    ${MasterData.WorkCalendar.InWorkday}:
                </td>
                <td style="width: 30%" id="idInWorkday" onclick="idInWorkdayClick()">
                    <asp:CheckBox ID="cb_InWorkday" runat="server" Text="${Common.Select.All}" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                </td>
                <td style="width: 30%" valign="top">
                    <div class="scrolly" id="idNotInWorkdayList">
                        <asp:CheckBoxList ID="CBL_NotInWorkday" runat="server" DataSourceID="ODS_ShiftsNotInWorkday"
                            DataTextField="ShiftName" DataValueField="Code">
                        </asp:CheckBoxList>
                        <asp:ObjectDataSource ID="ODS_ShiftsNotInWorkday" runat="server" SelectMethod="GetShiftsNotInWorkday"
                            TypeName="com.Sconit.Web.WorkdayShiftMgrProxy" DataObjectTypeName="com.Sconit.Entity.MasterData.Shift">
                            <SelectParameters>
                                <asp:Parameter Name="Id" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                </td>
                <td valign="middle" align="center">
                    <asp:Button ID="ToInBT" runat="server" Text=">>>" OnClick="ToInBT_Click" />
                    <br />
                    <br />
                    <asp:Button ID="ToOutBT" runat="server" Text="<<<" OnClick="ToOutBT_Click" />
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 30%" valign="top">
                    <div class="scrolly" id="idInWorkdayList">
                        <asp:CheckBoxList ID="CBL_InWorkday" runat="server" DataSourceID="ODS_ShiftsInWorkday"
                            DataTextField="ShiftName" DataValueField="Code">
                        </asp:CheckBoxList>
                        <asp:ObjectDataSource ID="ODS_ShiftsInWorkday" runat="server" SelectMethod="GetShiftsByWorkdayId"
                            TypeName="com.Sconit.Web.WorkdayShiftMgrProxy" DataObjectTypeName="com.Sconit.Entity.MasterData.Shift">
                            <SelectParameters>
                                <asp:Parameter Name="Id" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
