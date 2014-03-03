<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_WorkCalendar_Shift_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Shift" runat="server" DataSourceID="ODS_Shift" DefaultMode="Edit"
        Width="100%" DataKeyNames="Code" OnDataBound="FV_Shift_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.WorkCalendar.Shift.UpdateShift}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlCode" runat="server" Text="${Common.Business.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<%#Bind("Code") %>' ReadOnly="true" />
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
                            <asp:Literal ID="lblMemo" runat="server" Text="${MasterData.WorkCalendar.Memo}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMemo" runat="server" Text='<%#Bind("Memo") %>' />
                        </td>
                        <td colspan="2" />
                    </tr>
                </table>
            </fieldset>
            <div class="tablefooter">
                <div class="buttons">
                    <asp:Button ID="btnSave" runat="server" CommandName="Update" Text="${Common.Button.Save}" CssClass="apply" />
                    <asp:Button ID="btnInsert" runat="server" Text="${Common.Button.New}" OnClick="btnInsert_Click" CssClass="add" />
                    <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}"
                        OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" CssClass="delete" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="back" />
                </div>
            </div>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Shift" runat="server" TypeName="com.Sconit.Web.ShiftMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Shift" UpdateMethod="UpdateShift"
    OnUpdated="ODS_Shift_Updated" OnUpdating="ODS_Shift_Updating" DeleteMethod="DeleteShift"
    OnDeleted="ODS_Shift_Deleted" SelectMethod="LoadShift">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="code" Type="String" />
    </DeleteParameters>
</asp:ObjectDataSource>
