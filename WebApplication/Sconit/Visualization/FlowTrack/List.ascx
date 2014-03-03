<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Visualization_FlowTrack_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Code"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll" SelectCountMethod="FindCount">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="${MasterData.Flow.Code}" SortExpression="Code" />
                <asp:BoundField DataField="Description" HeaderText="${MasterData.Flow.Description}" />
                <asp:BoundField DataField="Type" HeaderText="${Common.Business.Type}" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="${Common.IsActive}" ReadOnly="true" />
                <asp:BoundField DataField="PartyFrom" HeaderText="${MasterData.FlowTrack.PartyFrom}" />
                <asp:BoundField DataField="PartyTo" HeaderText="${MasterData.FlowTrack.PartyTo}" />
                <asp:BoundField DataField="LocationFrom" HeaderText="${MasterData.Flow.Location.From}" />
                <asp:BoundField DataField="LocationTo" HeaderText="${MasterData.Flow.Location.To}" />
                <asp:BoundField DataField="EventCode" HeaderText="${MasterData.FlowTrack.EventCode}" />
                <asp:BoundField DataField="Version" HeaderText="${MasterData.FlowTrack.Version}" />
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.View}" OnClick="lbtnView_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
