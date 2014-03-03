<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Wap_Order_Transfer_Main" %>
<div>
    移库路线:<asp:Label ID="ltlFlow" Text="*" runat="server" ForeColor="Red" />
    <br />
    <asp:TextBox ID="tbFlow" runat="server" />
    <br />
    物料号:<asp:Label ID="ltlItem" Text="*" runat="server" ForeColor="Red" />
    <br />
    <asp:TextBox ID="tbItem" runat="server" />
    <br />
    数量:<asp:Label ID="ltlQty" Text="*" runat="server" ForeColor="Red" />
    <br />
    <asp:TextBox ID="tbQty" runat="server" />
    <br />
</div>
<hr />
<div>
    <asp:GridView ID="gvTransfer" runat="server" AutoGenerateColumns="False" AllowSorting="false">
        <Columns>
            <asp:BoundField DataField="Seq" HeaderText="序号" ReadOnly="true" />
            <asp:BoundField DataField="ItemCode" HeaderText="物料号" ReadOnly="true" />
            <asp:BoundField DataField="ItemDescription" HeaderText="物料描述" ReadOnly="true" />
            <asp:BoundField DataField="UnitCount" HeaderText="单包装" ReadOnly="true" />
            <asp:BoundField DataField="UomDescription" HeaderText="单位" ReadOnly="true" />
            <asp:BoundField DataField="OrderedQty" HeaderText="数量" ReadOnly="true" />
        </Columns>
    </asp:GridView>
</div>
