<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_WorkCalendar_SpecialTime_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<div id="divFV" runat="server">
    <asp:FormView ID="FV_SpecialTime" runat="server" DataSourceID="ODS_SpecialTime" DefaultMode="Edit"
        Width="100%" DataKeyNames="ID" OnDataBound="FV_SpecialTime_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.WorkCalendar.SpecialTime.UpdateSpecialTime}</legend>
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
                            <asp:Literal ID="lblStartTime" runat="server" Text="${MasterData.WorkCalendar.StartTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbStartTime" runat="server" Text='<%# Bind("StartTime","{0:yyyy-MM-dd HH:mm}") %>'
                                onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvStartTime" runat="server" ErrorMessage="${MasterData.WorkCalendar.WarningMessage.TimeEmpty}"
                                Display="Dynamic" ControlToValidate="tbStartTime" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblEndTime" runat="server" Text="${MasterData.WorkCalendar.EndTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbEndTime" runat="server" Text='<%# Bind("EndTime","{0:yyyy-MM-dd HH:mm}") %>'
                                onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvEndTime" runat="server" ErrorMessage="${MasterData.WorkCalendar.WarningMessage.TimeEmpty}"
                                Display="Dynamic" ControlToValidate="tbEndTime" ValidationGroup="vgSave" />
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
                    <asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="${Common.Button.Save}" CssClass="apply"
                        ValidationGroup="vgSave" />
                    <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}" CssClass="delete"
                        OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="back" />
                </div>
            </div>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_SpecialTime" runat="server" TypeName="com.Sconit.Web.SpecialTimeMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.SpecialTime" UpdateMethod="UpdateSpecialTime"
    OnUpdated="ODS_SpecialTime_Updated" OnUpdating="ODS_SpecialTime_Updating" DeleteMethod="DeleteSpecialTime"
    OnDeleted="ODS_SpecialTime_Deleted" SelectMethod="LoadSpecialTime">
    <SelectParameters>
        <asp:Parameter Name="ID" Type="Int32" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="ID" Type="Int32" />
    </DeleteParameters>
</asp:ObjectDataSource>
