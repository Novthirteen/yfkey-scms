<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Wap_Order_Online_Main" %>
<div>
    工单号:<asp:TextBox ID="tbOrderNo" runat="server" MaxLength="15"></asp:TextBox>
    <asp:Button ID="btnOrderNo" runat="server" Text="上线" OnClick="btnOrderNo_Click" />
</div>
<hr />
<div>
    <asp:GridView ID="gvOnline" runat="server" AutoGenerateColumns="False" AllowSorting="false">
        <Columns>
            <asp:BoundField DataField="OrderNo" HeaderText="工单号" ReadOnly="true" />
            <asp:BoundField DataField="StartTime" HeaderText="时间" ReadOnly="true" DataFormatString="{0:MM-dd HH:mm}" />
        </Columns>
    </asp:GridView>
</div>
