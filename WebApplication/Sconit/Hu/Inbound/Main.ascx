<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Hu_Inbound_Main" %>
<%@ Register Src="Upload.ascx" TagName="Upload" TagPrefix="uc2" %>
<%@ Register Src="Result.ascx" TagName="Result" TagPrefix="uc2" %>

<uc2:Upload ID="ucUpload" runat="server" Visible="true" />
<uc2:Result ID="ucResult" runat="server" Visible="false" />
