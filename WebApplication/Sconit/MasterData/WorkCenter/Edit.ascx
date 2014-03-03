<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_WorkCenter_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="floatdiv">
    <asp:FormView ID="FV_WorkCenter" runat="server" DataSourceID="ODS_WorkCenter" DefaultMode="Edit"
        Width="100%" DataKeyNames="Code" OnDataBound="FV_WorkCenter_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.WorkCenter.UpdateWorkCenter}</legend>
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
                            <asp:Literal ID="tbCode" runat="server" Text='<%# Bind("Code") %>' />
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
                            <asp:TextBox ID="tbLaborBurdenPercent" runat="server" Text='<%# Bind("LaborBurdenPercent","{0:F3}") %>'></asp:TextBox>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblLaborBurdenRate" runat="server" Text="${MasterData.WorkCenter.LaborBurdenRate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbLaborBurdenRate" runat="server" Text='<%# Bind("LaborBurdenRate","{0:F3}") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblSetupBurdenPercent" runat="server" Text="${MasterData.WorkCenter.SetupBurdenPercent}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbSetupBurdenPercent" runat="server" Text='<%# Bind("SetupBurdenPercent","{0:F3}") %>'></asp:TextBox>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblSetupBurdenRate" runat="server" Text="${MasterData.WorkCenter.SetupBurdenRate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbSetupBurdenRate" runat="server" Text='<%# Bind("SetupBurdenRate","{0:F3}") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblLaborRate" runat="server" Text="${MasterData.WorkCenter.LaborRate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbLaborRate" runat="server" Text='<%# Bind("LaborRate","{0:F3}") %>'></asp:TextBox>
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
                            <asp:TextBox ID="tbMachineQty" runat="server" Text='<%# Bind("MachineQty","{0:F3}") %>'></asp:TextBox>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblMachineBurdenRate" runat="server" Text="${MasterData.WorkCenter.MachineBurdenRate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMachineBurdenRate" runat="server" Text='<%# Bind("MachineBurdenRate","{0:F3}") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblMachineSetupBurdenRate" runat="server" Text="${MasterData.WorkCenter.MachineSetupBurdenRate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMachineSetupBurdenRate" runat="server" Text='<%# Bind("MachineSetupBurdenRate","{0:F3}") %>'></asp:TextBox>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblRunCrew" runat="server" Text="${MasterData.WorkCenter.RunCrew}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbRunCrew" runat="server" Text='<%# Bind("RunCrew","{0:F3}") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblSetupCrew" runat="server" Text="${MasterData.WorkCenter.SetupCrew}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbSetupCrew" runat="server" Text='<%# Bind("SetupCrew","{0:F3}") %>'></asp:TextBox>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblSetupRate" runat="server" Text="${MasterData.WorkCenter.SetupRate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbSetupRate" runat="server" Text='<%# Bind("SetupRate","{0:F3}") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblQueueTime" runat="server" Text="${MasterData.WorkCenter.QueueTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbQueueTime" runat="server" Text='<%# Bind("QueueTime","{0:F3}") %>'></asp:TextBox>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblWaitTime" runat="server" Text="${MasterData.WorkCenter.WaitTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbWaitTime" runat="server" Text='<%# Bind("WaitTime","{0:F3}") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblPercentEfficiency" runat="server" Text="${MasterData.WorkCenter.PercentEfficiency}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbPercentEfficiency" runat="server" Text='<%# Bind("PercentEfficiency","{0:F3}") %>'></asp:TextBox>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblPercentUtilization" runat="server" Text="${MasterData.PercentUtilization.SetupRate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbPercentUtilization" runat="server" Text='<%# Bind("PercentUtilization","{0:F3}") %>'></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div class="tablefooter">
                <div class="buttons">
                    <asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="${Common.Button.Save}"
                        CssClass="apply" ValidationGroup="vgSave" />
                    <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}"
                        CssClass="back" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        CssClass="back" />
                </div>
            </div>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_WorkCenter" runat="server" TypeName="com.Sconit.Web.WorkCenterMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.WorkCenter" UpdateMethod="UpdateWorkCenter"
    OnUpdated="ODS_WorkCenter_Updated" OnUpdating="ODS_WorkCenter_Updating" DeleteMethod="DeleteWorkCenter"
    OnDeleted="ODS_WorkCenter_Deleted" SelectMethod="LoadWorkCenter">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="code" Type="String" />
    </DeleteParameters>
    <UpdateParameters>
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
    </UpdateParameters>
</asp:ObjectDataSource>
