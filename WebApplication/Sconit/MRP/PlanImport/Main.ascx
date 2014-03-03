<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MRP_PlanImport_Main" %>
<%@ Register Src="Import.ascx" TagName="Import" TagPrefix="uc2" %>
<%@ Register Src="~/MRP/ShiftPlan/Import/Preview.ascx" TagName="Preview" TagPrefix="uc2" %>

<uc2:Import ID="ucImport" runat="server" Visible="true" />
<div id="floatdiv">
    <uc2:Preview ID="ucPreview" runat="server" Visible="false" />
</div>
