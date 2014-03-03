<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Jobs_Trigger_Main" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Jobs/Log/Main.ascx" TagName="Log" TagPrefix="uc2" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false" OnRowCommand="GV_List_RowCommand"
            OnRowDataBound="GV_List_RowDataBound" OnRowUpdating="GV_BatchTrigger_RowUpdating"
            OnRowCreated="GV_BatchTrigger_RowCreated" DataSourceID="ODS_GV_BatchTrigger"
            OnRowEditing="GV_BatchTrigger_RowEditing">
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.Jobs.Trigger.Name}">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfId" runat="server" Value='<%# Bind("Id") %>' />
                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Jobs.Trigger.Description}">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Jobs.Trigger.TimesTriggered}">
                    <ItemTemplate>
                        <asp:Label ID="lblTimesTriggered" runat="server" Text='<%# Bind("TimesTriggered") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Jobs.Trigger.PrevFireTime}">
                    <ItemTemplate>
                        <asp:Label ID="lblPrevFireTime" runat="server" Text='<%# Bind("PreviousFireTime") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Jobs.Trigger.NextFireTime}">
                    <ItemTemplate>
                        <asp:Label ID="lblNextFireTime" runat="server" Text='<%# Bind("NextFireTime") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbNextFireTime" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"
                            Text='<%# Bind("NextFireTime") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Jobs.Trigger.RepeatCount}">
                    <ItemTemplate>
                        <asp:Label ID="lblRepeatCount" runat="server" Text='<%# Bind("RepeatCount") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbRepeatCount" runat="server" Width="25" Text='<%# Bind("RepeatCount") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Jobs.Trigger.Interval}">
                    <ItemTemplate>
                        <asp:Label ID="lblInterval" runat="server" Text='<%# Bind("Interval") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbInterval" runat="server" Width="25" Text='<%# Bind("Interval") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Jobs.Trigger.IntervalType}">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblIntervalType" runat="server" Value='<%# Bind("IntervalType") %>'
                            Code="DateTimeType" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <cc1:CodeMstrDropDownList ID="ddlIntervalType" Code="DateTimeType" runat="server">
                        </cc1:CodeMstrDropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Jobs.Trigger.Status}">
                    <ItemTemplate>
                        <asp:Label ID="lbStatus" runat="server" Text='<%# Bind("Status") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" ItemStyle-Width="80px" HeaderText="${Common.GridView.Action}"
                    EditText="${Common.Button.Edit}" UpdateText="${Common.Button.Update}" CancelText="${Common.Button.Cancel}">
                </asp:CommandField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnStart" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.Start}" CommandName="StartTrigger" Visible="false">
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnStop" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.Stop}" CommandName="StopTrigger" Visible="false">
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnLog" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.Log}" CommandName="ViewLog">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ODS_GV_BatchTrigger" runat="server" TypeName="com.Sconit.Web.BatchTriggerMgrProxy"
            DataObjectTypeName="com.Sconit.Entity.Batch.BatchTrigger" UpdateMethod="UpdateBatchTrigger"
            SelectMethod="GetActiveTrigger" OnUpdating="ODS_GV_BatchTrigger_OnUpdating" OnUpdated="ODS_GV_BatchTrigger_OnUpdated">
        </asp:ObjectDataSource>
    </div>
</fieldset>
<asp:Button ID="mrp" runat="server"  Text="MRP" OnClick="MRP_onclick" />
<uc2:Log ID="ucLog" runat="server" Visible="false" />
