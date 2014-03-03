<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewLocTransMain.ascx.cs"
    Inherits="MasterData_Flow_ViewLocTransMain" %>
<%@ Register Src="ViewLocTransList.ascx" TagName="ViewLocTransList" TagPrefix="uc2" %>
<fieldset>
    <legend>${MasterData.Flow.LocTrans.In}</legend>
    <uc2:ViewLocTransList ID="ucViewLocInTransList" runat="server" Visible="true" />
</fieldset>
<fieldset>
    <legend>${MasterData.Flow.LocTrans.Out}</legend>
    <uc2:ViewLocTransList ID="ucViewLocOutTransList" runat="server" Visible="true" />
</fieldset>