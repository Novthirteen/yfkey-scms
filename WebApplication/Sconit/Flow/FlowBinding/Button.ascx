<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Button.ascx.cs" Inherits="MasterData_FlowBinding_Button" %>
<div class="tablefooter">
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="${MasterData.Flow.Button.AddBinding}"  Width="90px" />
    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="button2" />
</div>