<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_WorkCalendar_SpecialTime_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<div id="divFV" runat="server">
    <asp:FormView ID="FV_SpecialTime" runat="server" DataSourceID="ODS_SpecialTime" DefaultMode="Insert"
        DataKeyNames="ID">
        <InsertItemTemplate>
            <fieldset>
                <legend>${MasterData.WorkCalendar.SpecialTime.AddSpecialTime}</legend>
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
                            <asp:Literal ID="lblStartTime" runat="server" Text="${MasterData.WorkCalendar.StartTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbStartTime" runat="server" Text='<%# Bind("StartTime") %>' onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"
                                CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvStartTime" runat="server" ErrorMessage="${MasterData.WorkCalendar.WarningMessage.TimeEmpty}"
                                Display="Dynamic" ControlToValidate="tbStartTime" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvStartTime" runat="server" ControlToValidate="tbStartTime"
                                ErrorMessage="${MasterData.WorkCalendar.WarningMessage.StartTimeInvalid}" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="Save_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblEndTime" runat="server" Text="${MasterData.WorkCalendar.EndTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbEndTime" runat="server" Text='<%# Bind("EndTime") %>' onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"
                                CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvEndTime" runat="server" ErrorMessage="${MasterData.WorkCalendar.WarningMessage.TimeEmpty}"
                                Display="Dynamic" ControlToValidate="tbEndTime" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvEndTime" runat="server" ControlToValidate="tbEndTime"
                                ErrorMessage="${MasterData.WorkCalendar.WarningMessage.EndTimeInvalid}" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="Save_ServerValidate" />
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
                            <asp:Literal ID="lblType" runat="server" Text="${MasterData.WorkCalendar.SpecialTime.Type}:" />
                        </td>
                        <td class="td02">
                            <asp:RadioButtonList ID="rblType" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                DataTextField='<%#Bind("Type") %>'>
                                <asp:ListItem Text="${MasterData.WorkCalendar.Type.Work}" Value="Work" />
                                <asp:ListItem Selected="True" Text="${MasterData.WorkCalendar.Type.Rest}" Value="Rest" />
                            </asp:RadioButtonList>
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
<asp:ObjectDataSource ID="ODS_SpecialTime" runat="server" TypeName="com.Sconit.Web.SpecialTimeMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.SpecialTime" InsertMethod="CreateSpecialTime"
    OnInserted="ODS_SpecialTime_Inserted" OnInserting="ODS_SpecialTime_Inserting">
</asp:ObjectDataSource>
