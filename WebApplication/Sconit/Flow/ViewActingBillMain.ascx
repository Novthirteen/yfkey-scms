<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewActingBillMain.ascx.cs"
    Inherits="MasterData_Flow_ViewActingBillMain" %>
<%@ Register Src="ViewActingBillList.ascx" TagName="ViewActingBillList" TagPrefix="uc2" %>
<fieldset id="fdActBill" runat="server">
    <legend id="lTitle" runat="server">${MasterData.Flow.ActingBill.Po}</legend>
    <uc2:ViewActingBillList ID="ucViewActingBillList" runat="server" Visible="true" />
</fieldset>