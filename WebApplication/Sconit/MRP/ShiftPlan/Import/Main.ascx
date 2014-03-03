<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MRP_ShiftPlan_Import_Main" %>
<%@ Register Src="Import.ascx" TagName="Import" TagPrefix="uc" %>
<%@ Register Src="Preview.ascx" TagName="Preview" TagPrefix="uc" %>

<uc:Import ID="ucImport" runat="server" Visible="true" OnImportEvent="ucImport_ImportEvent"
    OnBtnBackClick="ucImport_BtnBackClick" />
<div id="floatdiv">
    <uc:Preview ID="ucPreview" runat="server" Visible="false" OnBtnCreateClick="ucPreview_BtnCreateClick" />
</div>
