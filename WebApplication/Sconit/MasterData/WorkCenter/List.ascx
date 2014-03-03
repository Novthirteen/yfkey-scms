<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MasterData_WorkCenter_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Code"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="${MasterData.WorkCenter.Code}" SortExpression="Code" />
                <asp:BoundField DataField="Name" HeaderText="${MasterData.WorkCenter.Name}" SortExpression="Name" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="${MasterData.WorkCenter.IsActive}"
                    SortExpression="IsActive" />
                <asp:BoundField DataField="LaborBurdenPercent" HeaderText="${MasterData.WorkCenter.LaborBurdenPercent}"
                    SortExpression="LaborBurdenPercent" />
                <asp:BoundField DataField="LaborBurdenRate" HeaderText="${MasterData.WorkCenter.LaborBurdenRate}"
                    SortExpression="LaborBurdenRate" />
                <asp:BoundField DataField="SetupBurdenPercent" HeaderText="${MasterData.WorkCenter.SetupBurdenPercent}"
                    SortExpression="SetupBurdenPercent" />
                <asp:BoundField DataField="SetupBurdenRate" HeaderText="${MasterData.WorkCenter.SetupBurdenRate}"
                    SortExpression="SetupBurdenRate" />
                <asp:BoundField DataField="LaborRate" HeaderText="${MasterData.WorkCenter.LaborRate}"
                    SortExpression="LaborRate" />
                <asp:BoundField DataField="Machine" HeaderText="${MasterData.WorkCenter.Machine}"
                    SortExpression="Machine" />
                <asp:BoundField DataField="MachineQty" HeaderText="${MasterData.WorkCenter.MachineQty}"
                    SortExpression="MachineQty" />
                <asp:BoundField DataField="MachineBurdenRate" HeaderText="${MasterData.WorkCenter.MachineBurdenRate}"
                    SortExpression="MachineBurdenRate" />
                <asp:BoundField DataField="MachineSetupBurdenRate" HeaderText="${MasterData.WorkCenter.MachineSetupBurdenRate}"
                    SortExpression="MachineSetupBurdenRate" />
                <asp:BoundField DataField="RunCrew" HeaderText="${MasterData.WorkCenter.RunCrew}"
                    SortExpression="RunCrew" />
                <asp:BoundField DataField="SetupCrew" HeaderText="${MasterData.WorkCenter.SetupCrew}"
                    SortExpression="SetupCrew" />
                <asp:BoundField DataField="SetupRate" HeaderText="${MasterData.WorkCenter.SetupRate}"
                    SortExpression="SetupRate" />
                <asp:BoundField DataField="QueueTime" HeaderText="${MasterData.WorkCenter.QueueTime}"
                    SortExpression="QueueTime" />
                <asp:BoundField DataField="WaitTime" HeaderText="${MasterData.WorkCenter.WaitTime}"
                    SortExpression="WaitTime" />
                <asp:BoundField DataField="PercentEfficiency" HeaderText="${MasterData.WorkCenter.PercentEfficiency}"
                    SortExpression="PercentEfficiency" />
                <asp:BoundField DataField="PercentUtilization" HeaderText="${MasterData.PercentUtilization.SetupRate}"
                    SortExpression="PercentUtilization" />
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>'
                            Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click">
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>'
                            Text="${Common.Button.Delete}" OnClick="lbtnDelete_Click" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
