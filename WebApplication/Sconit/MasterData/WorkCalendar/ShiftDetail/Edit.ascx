<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_WorkCalendar_ShiftDetail_Edit" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<div id="divFV" runat="server">
    <asp:FormView ID="FV_ShiftDetail" runat="server" DataSourceID="ODS_ShiftDetail" DefaultMode="Edit"
        Width="100%" DataKeyNames="Id" OnDataBound="FV_ShiftDetail_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.WorkCalendar.Shift.UpdateShiftDetail}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlCode" runat="server" Text="${MasterData.WorkCalendar.ShiftCode}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" ReadOnly="true" CssClass="inputRequired" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblShiftName" runat="server" Text="${MasterData.WorkCalendar.Shift.ShiftName}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbShiftName" runat="server" ReadOnly="true" CssClass="inputRequired" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblShiftTime" runat="server" Text="${MasterData.WorkCalendar.ShiftTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbShiftTime" runat="server" Text='<%# Bind("ShiftTime") %>' CssClass="inputRequired" />
                            <asp:RegularExpressionValidator ID="revShiftTime" runat="server" Display="Dynamic"
                                ValidationGroup="vgSave" ValidationExpression="(\b(20|21|22|23|[0-1]+\d):([0-5]+\d)-(20|21|22|23|[0-1]+\d):[0-5]+\d(\||\b))*"
                                ControlToValidate="tbShiftTime" ErrorMessage="${MasterData.WorkCalendar.ErrorMessage.TimeFormat}"></asp:RegularExpressionValidator>
                            <asp:Label ID="lbTimeFormat" runat="server" Text="${MasterData.WorkCalendar.Shift.TimeFormat}: 06:00-12:00|13:00-18:00" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblMemo" runat="server" Text="${MasterData.WorkCalendar.Memo}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMemo" runat="server" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblStartDate" runat="server" Text="${Common.Business.EffDateFrom}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbStartDate" runat="server" Text='<%# Bind("StartDate","{0:yyyy-MM-dd}") %>'
                                onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblEndDate" runat="server" Text="${Common.Business.EffDateTo}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbEndDate" runat="server" Text='<%# Bind("EndDate","{0:yyyy-MM-dd}") %>'
                                onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblDaysAdd" runat="server" Text="${Common.WorkCalendar.DaysAdd}:" />
                        </td>
                        <td class="td02">
                            <cc1:CodeMstrDropDownList ID="ddlDaysAdd" runat="server" Code="DaysAddType"  />
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
                    <asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="${Common.Button.Save}"
                        CssClass="apply" ValidationGroup="vgSave" />
                    <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}"
                        CssClass="delete" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        CssClass="back" />
                </div>
            </div>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_ShiftDetail" runat="server" TypeName="com.Sconit.Web.ShiftDetailMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.ShiftDetail" UpdateMethod="UpdateShiftDetail"
    OnUpdated="ODS_ShiftDetail_Updated" OnUpdating="ODS_ShiftDetail_Updating" DeleteMethod="DeleteShiftDetail"
    OnDeleted="ODS_ShiftDetail_Deleted" SelectMethod="LoadShiftDetail">
    <SelectParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </DeleteParameters>
</asp:ObjectDataSource>
