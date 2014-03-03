<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_WorkCalendar_Workday_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Workday" runat="server" DataSourceID="ODS_Workday" DefaultMode="Insert"
        DataKeyNames="Id">
        <InsertItemTemplate>
            <fieldset>
                <legend>${MasterData.WorkCalendar.Workday.AddWorkday}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblRegion" runat="server" Text="${Common.Business.Region}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbRegion" runat="server" Visible="true" Width="250" DescField="Name"
                                ValueField="Code" ServicePath="RegionMgr.service" ServiceMethod="GetRegion" />
                            <asp:CustomValidator ID="cvRegion" runat="server" ControlToValidate="tbRegion" ErrorMessage="${MasterData.WorkCalendar.WarningMessage.RegionInvalid}"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="Save_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblWorkCenter" runat="server" Text="${Common.Business.WorkCenter}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbWorkCenter" runat="server" Visible="true" Width="250" DescField="Name"
                                ValueField="Code" ServicePath="WorkCenterMgr.service" ServiceMethod="GetWorkCenter"
                                ServiceParameter="string:#tbRegion" />
                            <asp:CustomValidator ID="cvWorkCenter" runat="server" ControlToValidate="tbWorkCenter"
                                ErrorMessage="${MasterData.WorkCalendar.WarningMessage.WorkCenterInvalid}" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="Save_ServerValidate" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblDayOfWeek" runat="server" Text="${MasterData.WorkCalendar.Workday.Week}:" />
                        </td>
                        <td class="td02">
                            <asp:DropDownList ID="DayOfWeek_DDL" runat="server" Text='<%# Bind("DayOfWeek") %>'>
                                <asp:ListItem Text="${Common.Week.Monday}" Value="Monday"></asp:ListItem>
                                <asp:ListItem Text="${Common.Week.Tuesday}" Value="Tuesday"></asp:ListItem>
                                <asp:ListItem Text="${Common.Week.Wednesday}" Value="Wednesday"></asp:ListItem>
                                <asp:ListItem Text="${Common.Week.Thursday}" Value="Thursday"></asp:ListItem>
                                <asp:ListItem Text="${Common.Week.Friday}" Value="Friday"></asp:ListItem>
                                <asp:ListItem Text="${Common.Week.Saturday}" Value="Saturday"></asp:ListItem>
                                <asp:ListItem Text="${Common.Week.Sunday}" Value="Sunday"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:CustomValidator ID="cvWeek" runat="server" ControlToValidate="DayOfWeek_DDL"
                                ErrorMessage="${MasterData.WorkCalendar.WarningMessage.Error1}" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="Save_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblType" runat="server" Text="${MasterData.WorkCalendar.SpecialTime.Type}:" />
                        </td>
                        <td class="td02">
                            <asp:RadioButtonList ID="rbType" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                DataTextField='<%#Bind("Type") %>'>
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
                    <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="${Common.Button.Save}"
                        CssClass="apply" ValidationGroup="vgSave" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        CssClass="back" />
                </div>
            </div>
        </InsertItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Workday" runat="server" TypeName="com.Sconit.Web.WorkdayMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Workday" InsertMethod="CreateWorkday"
    OnInserted="ODS_Workday_Inserted" OnInserting="ODS_Workday_Inserting"></asp:ObjectDataSource>
