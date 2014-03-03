<%@ Control Language="C#" AutoEventWireup="true" CodeFile="View.ascx.cs" Inherits="Visualization_GoodsTraceability_Traceability_View" %>
<%@ Register Src="~/Order/GoodsReceipt/ViewReceipt/AjaxViewMain.ascx" TagName="Receipt"
    TagPrefix="uc2" %>
<%@ Register Src="~/Warehouse/InProcessLocation/AjaxViewMain.ascx" TagName="Asn" TagPrefix="uc2" %>
<asp:LinkButton ID="lbOrderNo" runat="server" OnClick="lbOrderNo_Click"></asp:LinkButton>
<asp:UpdatePanel ID="upViewMain" runat="server">
    <ContentTemplate>
        <uc2:Receipt ID="ucReceipt" runat="server" Visible="false" />
        <uc2:Asn ID="ucASN" runat="server" Visible="false" />
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lbOrderNo" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
