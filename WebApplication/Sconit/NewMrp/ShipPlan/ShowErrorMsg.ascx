<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShowErrorMsg.ascx.cs" Inherits="NewMrp_ShipPlan_ShowErrorMsg" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Flow"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Flow" HeaderText="路线" SortExpression="Flow" />
                <asp:BoundField DataField="Item" HeaderText="物料" SortExpression="Item" />
                <asp:BoundField DataField="Msg" HeaderText="消息" SortExpression="Msg" />
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="50">
        </cc1:GridPager>
    </div>
    <asp:Button ID="btnBack" runat="server" Text=" 返回" OnClick="btnBack_Click" />
</fieldset>

