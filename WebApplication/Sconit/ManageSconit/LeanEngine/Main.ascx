<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="ManageSconit_LeanEngine_Main" %>
<%@ Register Src="StartStop.ascx" TagName="StartStop" TagPrefix="uc2" %>
<%@ Register Src="EditMain.ascx" TagName="EditMain" TagPrefix="uc2" %>

<uc2:StartStop ID="ucStartStop" runat="server" Visible="true" />
<uc2:EditMain ID="ucEditMain" runat="server" Visible="false" />

