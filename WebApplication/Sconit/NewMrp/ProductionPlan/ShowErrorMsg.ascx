<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShowErrorMsg.ascx.cs" Inherits="NewMrp_ProductionPlan_ShowErrorMsg" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Item"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound" OnPageIndexChanging="GridView1_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="Item" HeaderText="物料" SortExpression="Item" />
                <asp:BoundField DataField="Bom" HeaderText="Bom" SortExpression="Bom" />
                <asp:BoundField DataField="EffDate" HeaderText="生效日期" SortExpression="EffDate" />
                <asp:BoundField DataField="Msg" HeaderText="消息" SortExpression="Msg" />
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="50">
        </cc1:GridPager>
    </div>
    <asp:Button ID="btnBack" runat="server" Text=" 返回" OnClick="btnBack_Click" />
</fieldset>

