<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_WorkCenter_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="floatdiv">
    <asp:FormView ID="FV_WorkCenter" runat="server" DataSourceID="ODS_WorkCenter" DefaultMode="Insert"
        DataKeyNames="Code">
        <InsertItemTemplate>
            <fieldset>
                <legend>${MasterData.WorkCenter.AddWorkCenter}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCurrentParty" runat="server" Text="${MasterData.Party.CurrentParty}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lbCurrentParty" runat="server" />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${MasterData.WorkCenter.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Code") %>' CssClass="inputRequired"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="${MasterData.WorkCenter.Code.Empty}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvInsert" runat="server" ControlToValidate="tbCode" ErrorMessage="${MasterData.WorkCenter.Code.Exists}"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="checkWorkCenterExists" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblName" runat="server" Text="${MasterData.WorkCenter.Name}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblType" runat="server" Text="${MasterData.WorkCenter.Type}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbType" runat="server" Visible="true" DescField="Description" ValueField="Value"
                                ServicePath="CodeMasterMgr.service" ServiceMethod="GetCachedCodeMaster" CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvType" runat="server" ErrorMessage="${MasterData.WorkCenter.Type.Empty}"
                                Display="Dynamic" ControlToValidate="tbType" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${MasterData.WorkCenter.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="tbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblLaborBurdenPercent" runat="server" Text="${MasterData.WorkCenter.LaborBurdenPercent}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbLaborBurdenPercent" runat="server" Text='<%# Bind("LaborBurdenPercent") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvLaborBurdenPercent" runat="server" MaximumValue="100000000"
                                MinimumValue="0.000000001" ControlToValidate="tbLaborBurdenPercent" Display="Dynamic"
                                Type="Double" ErrorMessage="请输入合法的数字" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblLaborBurdenRate" runat="server" Text="${MasterData.WorkCenter.LaborBurdenRate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbLaborBurdenRate" runat="server" Text='<%# Bind("LaborBurdenRate") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvLaborBurdenRate" runat="server" MaximumValue="100000000"
                                MinimumValue="0.000000001" ControlToValidate="tbLaborBurdenRate" Display="Dynamic"
                                Type="Double" ErrorMessage="请输入合法的数字" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblSetupBurdenPercent" runat="server" Text="${MasterData.WorkCenter.SetupBurdenPercent}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbSetupBurdenPercent" runat="server" Text='<%# Bind("SetupBurdenPercent") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvSetupBurdenPercent" runat="server" MaximumValue="100000000"
                                MinimumValue="0.000000001" ControlToValidate="tbSetupBurdenPercent" Display="Dynamic"
                                Type="Double" ErrorMessage="请输入合法的数字" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblSetupBurdenRate" runat="server" Text="${MasterData.WorkCenter.SetupBurdenRate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbSetupBurdenRate" runat="server" Text='<%# Bind("SetupBurdenRate") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvSetupBurdenRate" runat="server" MaximumValue="100000000"
                                MinimumValue="0.000000001" ControlToValidate="tbSetupBurdenRate" Display="Dynamic"
                                Type="Double" ErrorMessage="请输入合法的数字" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblLaborRate" runat="server" Text="${MasterData.WorkCenter.LaborRate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbLaborRate" runat="server" Text='<%# Bind("LaborRate") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvLaborRate" runat="server" MaximumValue="100000000" MinimumValue="0.000000001"
                                ControlToValidate="tbLaborRate" Display="Dynamic" Type="Double" ErrorMessage="请输入合法的数字"
                                ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblMachine" runat="server" Text="${MasterData.WorkCenter.Machine}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMachine" runat="server" Text='<%# Bind("Machine") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblMachineQty" runat="server" Text="${MasterData.WorkCenter.MachineQty}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMachineQty" runat="server" Text='<%# Bind("MachineQty") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvMachineQty" runat="server" MaximumValue="100000000" MinimumValue="0.000000001"
                                ControlToValidate="tbMachineQty" Display="Dynamic" Type="Double" ErrorMessage="请输入合法的数字"
                                ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblMachineBurdenRate" runat="server" Text="${MasterData.WorkCenter.MachineBurdenRate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMachineBurdenRate" runat="server" Text='<%# Bind("MachineBurdenRate") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvMachineBurdenRate" runat="server" MaximumValue="100000000"
                                MinimumValue="0.000000001" ControlToValidate="tbMachineBurdenRate" Display="Dynamic"
                                Type="Double" ErrorMessage="请输入合法的数字" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblMachineSetupBurdenRate" runat="server" Text="${MasterData.WorkCenter.MachineSetupBurdenRate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMachineSetupBurdenRate" runat="server" Text='<%# Bind("MachineSetupBurdenRate") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvMachineSetupBurdenRate" runat="server" MaximumValue="100000000"
                                MinimumValue="0.000000001" ControlToValidate="tbMachineSetupBurdenRate" Display="Dynamic"
                                Type="Double" ErrorMessage="请输入合法的数字" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblRunCrew" runat="server" Text="${MasterData.WorkCenter.RunCrew}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbRunCrew" runat="server" Text='<%# Bind("RunCrew") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvRunCrew" runat="server" MaximumValue="100000000" MinimumValue="0.000000001"
                                ControlToValidate="tbRunCrew" Display="Dynamic" Type="Double" ErrorMessage="请输入合法的数字"
                                ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblSetupCrew" runat="server" Text="${MasterData.WorkCenter.SetupCrew}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbSetupCrew" runat="server" Text='<%# Bind("SetupCrew") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvSetupCrew" runat="server" MaximumValue="100000000" MinimumValue="0.000000001"
                                ControlToValidate="tbSetupCrew" Display="Dynamic" Type="Double" ErrorMessage="请输入合法的数字"
                                ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblSetupRate" runat="server" Text="${MasterData.WorkCenter.SetupRate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbSetupRate" runat="server" Text='<%# Bind("SetupRate") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvSetupRate" runat="server" MaximumValue="100000000" MinimumValue="0.000000001"
                                ControlToValidate="tbSetupRate" Display="Dynamic" Type="Double" ErrorMessage="请输入合法的数字"
                                ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblQueueTime" runat="server" Text="${MasterData.WorkCenter.QueueTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbQueueTime" runat="server" Text='<%# Bind("QueueTime") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvQueueTime" runat="server" MaximumValue="100000000" MinimumValue="0.000000001"
                                ControlToValidate="tbQueueTime" Display="Dynamic" Type="Double" ErrorMessage="请输入合法的数字"
                                ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblWaitTime" runat="server" Text="${MasterData.WorkCenter.WaitTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbWaitTime" runat="server" Text='<%# Bind("WaitTime") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvWaitTime" runat="server" MaximumValue="100000000" MinimumValue="0.000000001"
                                ControlToValidate="tbWaitTime" Display="Dynamic" Type="Double" ErrorMessage="请输入合法的数字"
                                ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblPercentEfficiency" runat="server" Text="${MasterData.WorkCenter.PercentEfficiency}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbPercentEfficiency" runat="server" Text='<%# Bind("PercentEfficiency") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvPercentEfficiency" runat="server" MaximumValue="100000000"
                                MinimumValue="0.000000001" ControlToValidate="tbPercentEfficiency" Display="Dynamic"
                                Type="Double" ErrorMessage="请输入合法的数字" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblPercentUtilization" runat="server" Text="${MasterData.PercentUtilization.SetupRate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbPercentUtilization" runat="server" Text='<%# Bind("PercentUtilization") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvPercentUtilization" runat="server" MaximumValue="100000000"
                                MinimumValue="0.000000001" ControlToValidate="tbPercentUtilization" Display="Dynamic"
                                Type="Double" ErrorMessage="请输入合法的数字" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                </table>
                <div class="tablefooter">
                    <div class="buttons">
                        <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="${Common.Button.Save}"
                            CssClass="add" ValidationGroup="vgSave" />
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                            CssClass="back" />
                    </div>
                </div>
            </fieldset>
        </InsertItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_WorkCenter" runat="server" TypeName="com.Sconit.Web.WorkCenterMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.WorkCenter" InsertMethod="CreateWorkCenter"
    OnInserted="ODS_WorkCenter_Inserted" OnInserting="ODS_WorkCenter_Inserting">
    <InsertParameters>
        <asp:Parameter Name="LaborBurdenPercent" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="LaborBurdenRate" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="SetupBurdenRate" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="SetupBurdenPercent" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="LaborRate" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="MachineQty" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="MachineBurdenRate" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="MachineSetupBurdenRate" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="RunCrew" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="SetupCrew" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="SetupRate" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="QueueTime" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="WaitTime" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="PercentEfficiency" Type="Decimal" ConvertEmptyStringToNull="true" />
        <asp:Parameter Name="PercentUtilization" Type="Decimal" ConvertEmptyStringToNull="true" />
    </InsertParameters>
</asp:ObjectDataSource>
