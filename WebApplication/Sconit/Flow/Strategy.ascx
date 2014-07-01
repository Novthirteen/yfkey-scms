<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Strategy.ascx.cs" Inherits="MasterData_Flow_Strategy" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc2" %>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<div id="divFV" runat="server">
    <asp:FormView ID="FV_Strategy" runat="server" DataSourceID="ODS_Strategy" DefaultMode="Edit"
        DataKeyNames="Code" OnDataBound="FV_Strategy_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend id="lStrategy" runat="server"></legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblFlowStrategy" runat="server" />
                        </td>
                        <td class="td02">
                            <cc2:CodeMstrDropDownList ID="ddlStrategy" Code="FlowStrategy" runat="server" IncludeBlankOption="true">
                            </cc2:CodeMstrDropDownList>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblLotGroup" runat="server" Text="${MasterData.Flow.Strategy.LotGroup}:"  visible="false" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbLotGroup" runat="server" Text='<%# Bind("LotGroup") %>'  visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblLeadTime" runat="server" Text="${MasterData.Flow.Strategy.LeadTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbLeadTime" runat="server" Text='<%# Bind("LeadTime","{0:0.########}") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvLeadTime" ControlToValidate="tbLeadTime" runat="server"
                                Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="1000"
                                MinimumValue="0" Type="Double" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblEmTime" runat="server" Text="${MasterData.Flow.Strategy.EmTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbEmTime" runat="server" Text='<%# Bind("EmTime","{0:0.########}") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvEmTime" ControlToValidate="tbEmTime" runat="server" Display="Dynamic"
                                ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="1000" MinimumValue="0"
                                Type="Double" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblWeekInterval" runat="server" Text="${MasterData.Flow.Strategy.WeekInterval}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbWeekInterval" runat="server" Text='<%# Bind("WeekInterval") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvWeekInterval" runat="server" ControlToValidate="tbWeekInterval"
                                Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="1000"
                                MinimumValue="0" Type="Integer" ValidationGroup="vgSave" />
                        </td>
                         <td class="td01">
                            <asp:Literal ID="lblInterval" runat="server" Text="${MasterData.Flow.Strategy.Interval}:"  />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbInterval" runat="server" Text='<%# Bind("Interval") %>' ></asp:TextBox>
                           
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblNextOrderTime" runat="server" Text="${MasterData.Flow.Strategy.NextOrderTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbNextOrderTime" runat="server" Text='<%# Bind("NextOrderTime") %>' onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"></asp:TextBox>
                           
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblNextWinTime" runat="server" Text="${MasterData.Flow.Strategy.NextWinTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbNextWinTime" runat="server" Text='<%# Bind("NextWinTime") %>' onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"></asp:TextBox>
                          
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="WinTime" runat="server" Text="${MasterData.Flow.Strategy.WinTime}:" />
                        </td>
                        <td class="td02" >
                            <asp:Literal ID="Tips" runat="server" Text="${MasterData.Flow.Strategy.WinTime.Format}" />
                        </td>
                        <td class="td01">
                        Mrp提前期(天)
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="lblMrpLeadTime" runat="server" Text='<%# Bind("MrpLeadTime","{0:0.########}") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime1" runat="server" Text="${MasterData.Flow.Strategy.WinTime1}:" />
                        </td>
                        <td class="td02" colspan="3">
                            <asp:TextBox ID="tbWinTime1" runat="server" Text='<%# Bind("WinTime1") %>' Width="600px"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" Display="Dynamic" ValidationExpression="(\b(20|21|22|23|[0-1]+\d):[0-5]+\d(\||\b))*"
                                ControlToValidate="tbWinTime1" ErrorMessage="${MasterData.Flow.Strategy.WinTime.Correct.Format}"
                                ValidationGroup="vgSave"></asp:RegularExpressionValidator>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime2" runat="server" Text="${MasterData.Flow.Strategy.WinTime2}:" />
                        </td>
                        <td class="td02" colspan="3">
                            <asp:TextBox ID="tbWinTime2" runat="server" Text='<%# Bind("WinTime2") %>' Width="600px"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" Display="Dynamic" ValidationExpression="(\b(20|21|22|23|[0-1]+\d):[0-5]+\d(\||\b))*"
                                ControlToValidate="tbWinTime2" ErrorMessage="${MasterData.Flow.Strategy.WinTime.Correct.Format}"
                                ValidationGroup="vgSave"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime3" runat="server" Text="${MasterData.Flow.Strategy.WinTime3}:" />
                        </td>
                        <td class="td02" colspan="3">
                            <asp:TextBox ID="tbWinTime3" runat="server" Text='<%# Bind("WinTime3") %>' Width="600px"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" Display="Dynamic" ValidationExpression="(\b(20|21|22|23|[0-1]+\d):[0-5]+\d(\||\b))*"
                                ControlToValidate="tbWinTime3" ErrorMessage="${MasterData.Flow.Strategy.WinTime.Correct.Format}"
                                ValidationGroup="vgSave"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime4" runat="server" Text="${MasterData.Flow.Strategy.WinTime4}:" />
                        </td>
                        <td class="td02" colspan="3">
                            <asp:TextBox ID="tbWinTime4" runat="server" Text='<%# Bind("WinTime4") %>' Width="600px"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" Display="Dynamic" ValidationExpression="(\b(20|21|22|23|[0-1]+\d):[0-5]+\d(\||\b))*"
                                ControlToValidate="tbWinTime4" ErrorMessage="${MasterData.Flow.Strategy.WinTime.Correct.Format}"
                                ValidationGroup="vgSave"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime5" runat="server" Text="${MasterData.Flow.Strategy.WinTime5}:" />
                        </td>
                        <td class="td02" colspan="3">
                            <asp:TextBox ID="tbWinTime5" runat="server" Text='<%# Bind("WinTime5") %>' Width="600px"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" Display="Dynamic" ValidationExpression="(\b(20|21|22|23|[0-1]+\d):[0-5]+\d(\||\b))*"
                                ControlToValidate="tbWinTime5" ErrorMessage="${MasterData.Flow.Strategy.WinTime.Correct.Format}"
                                ValidationGroup="vgSave"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime6" runat="server" Text="${MasterData.Flow.Strategy.WinTime6}:" />
                        </td>
                        <td class="td02" colspan="3">
                            <asp:TextBox ID="tbWinTime6" runat="server" Text='<%# Bind("WinTime6") %>' Width="600px"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" Display="Dynamic" ValidationExpression="(\b(20|21|22|23|[0-1]+\d):[0-5]+\d(\||\b))*"
                                ControlToValidate="tbWinTime6" ErrorMessage="${MasterData.Flow.Strategy.WinTime.Correct.Format}"
                                ValidationGroup="vgSave"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime7" runat="server" Text="${MasterData.Flow.Strategy.WinTime7}:" />
                        </td>
                        <td class="td02" colspan="3">
                            <asp:TextBox ID="tbWinTime7" runat="server" Text='<%# Bind("WinTime7") %>' Width="600px"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" Display="Dynamic" ValidationExpression="(\b(20|21|22|23|[0-1]+\d):[0-5]+\d(\||\b))*"
                                ControlToValidate="tbWinTime7" ErrorMessage="${MasterData.Flow.Strategy.WinTime.Correct.Format}"
                                ValidationGroup="vgSave"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                            <asp:Button ID="btnEdit" runat="server" CommandName="Update" Text="${Common.Button.Save}"
                                CssClass="button2" ValidationGroup="vgSave" />
                            <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                                CssClass="button2" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Strategy" runat="server" TypeName="com.Sconit.Web.FlowMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Flow" UpdateMethod="UpdateFlow"
    OnUpdated="ODS_Strategy_Updated" OnUpdating="ODS_Strategy_Updating" SelectMethod="LoadFlow">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="StartLatency" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="CompleteLatency" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="NextOrderTime" Type="DateTime" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="NextWinTime" Type="DateTime" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="WeekInterval" Type="Int32" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="LeadTime" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="EmTime" Type="Decimal" ConvertEmptyStringToNull="true" />
    </UpdateParameters>
</asp:ObjectDataSource>
