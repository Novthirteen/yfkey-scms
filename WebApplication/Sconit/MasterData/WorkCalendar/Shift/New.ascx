<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_WorkCalendar_Shift_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<div id="divFV" runat="server">
    <asp:FormView ID="FV_Shift" runat="server" DataSourceID="ODS_Shift" DefaultMode="Insert"
        DataKeyNames="Code">
        <InsertItemTemplate>
            <fieldset>
                <legend>${MasterData.WorkCalendar.Shift.AddShift}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlCode" runat="server" Text="${Common.Business.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<%#Bind("Code") %>' CssClass="inputRequired" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblShiftName" runat="server" Text="${MasterData.WorkCalendar.Shift.ShiftName}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbShiftName" runat="server" Text='<%#Bind("ShiftName") %>' CssClass="inputRequired" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblShiftTime" runat="server" Text="${MasterData.WorkCalendar.ShiftTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbShiftTime" runat="server" Text='<%# Bind("ShiftTime") %>' CssClass="inputRequired" />
                            <asp:RegularExpressionValidator ID="revShiftTime" runat="server" Display="Dynamic"
                                ValidationExpression="(\b(20|21|22|23|[0-1]+\d):([0-5]+\d)-(20|21|22|23|[0-1]+\d):[0-5]+\d(\||\b))*"
                                ControlToValidate="tbShiftTime" ErrorMessage="${MasterData.WorkCalendar.ErrorMessage.TimeFormat}"></asp:RegularExpressionValidator>
                            <asp:Label ID="lbTimeFormat" runat="server" Text="${MasterData.WorkCalendar.Shift.TimeFormat}: 06:00-12:00|13:00-18:00" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblMemo" runat="server" Text="${MasterData.WorkCalendar.Memo}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMemo" runat="server" Text='<%#Bind("Memo") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblStartDate" runat="server" Text="${Common.Business.EffDateFrom}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbStartDate" runat="server" Text='<%# Bind("StartDate") %>' onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblEndDate" runat="server" Text="${Common.Business.EffDateTo}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbEndDate" runat="server" Text='<%# Bind("EndDate") %>' onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />
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
<asp:ObjectDataSource ID="ODS_Shift" runat="server" TypeName="com.Sconit.Web.ShiftMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Shift" InsertMethod="CreateShift"
    OnInserted="ODS_Shift_Inserted" OnInserting="ODS_Shift_Inserting"></asp:ObjectDataSource>
