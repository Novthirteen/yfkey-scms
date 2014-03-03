<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Jobs_Log_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" DefaultSortExpression="Id" DefaultSortDirection="Descending">
            <Columns>
                <asp:BoundField DataField="StartTime" HeaderText="${MasterData.Jobs.Log.StartTime}"
                    SortExpression="StartTime" />
                <asp:BoundField DataField="EndTime" HeaderText="${MasterData.Jobs.Log.EndTime}" SortExpression="EndTime" />
                <asp:BoundField DataField="Status" HeaderText="${MasterData.Jobs.Log.Status}" SortExpression="Status" />
                <asp:BoundField DataField="Message" HeaderText="${MasterData.Jobs.Log.Message}" SortExpression="Message" />
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
<asp:Button ID="btnClose" runat="server" Text="${Common.Button.Close}" OnClick="btnClose_Click"
    CssClass="button2" />