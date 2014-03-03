<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MasterData_FlowBinding_Main" %>
<%@ Register Src="ListBinding.ascx" TagName="ListBinding" TagPrefix="uc2" %>
<%@ Register Src="ListBinded.ascx" TagName="ListBinded" TagPrefix="uc2" %>
<%@ Register Src="Button.ascx" TagName="Button" TagPrefix="uc2" %>
<%@ Register Src="New.ascx" TagName="New" TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc2" %>

<table class="mtable"><tr><td>
<uc2:ListBinding ID="ucListBinding" runat="server" Visible="true" />
<uc2:ListBinded ID="ucListBinded" runat="server" Visible="true" />
<uc2:Button ID="ucButton" runat="server" Visible="true" />
</td></tr></table>
<uc2:New ID="ucNew" runat="server" Visible="false" />
<uc2:Edit ID="ucEdit" runat="server" Visible="false" />