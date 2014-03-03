<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CodeMstrList.ascx.cs"
    Inherits="MasterData_GeneralCode_CodeMstrList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Code"
            AllowMultiColumnSorting="false" SeqNo="0" SeqText="No." ShowSeqNo="true" AllowSorting="True"
            AllowPaging="True" PagerID="gp" Width="100%" CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy"
            SelectMethod="FindAll" SelectCountMethod="FindCount">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="${MasterData.GeneralCode.Code}" SortExpression="Code" />
                <asp:BoundField DataField="Value" HeaderText="${MasterData.GeneralCode.Value}" SortExpression="Value" />
                <asp:BoundField DataField="IsDefault" HeaderText="${MasterData.GeneralCode.IsDefault}"
                    SortExpression="IsDefault" />
                <asp:BoundField DataField="Description" HeaderText="${MasterData.GeneralCode.Description}"
                    SortExpression="Description" />
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="20">
        </cc1:GridPager>
    </div>
</fieldset>
