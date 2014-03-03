<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Order_GoodsReceipt_Main" %>
<%@ Register Src="TabNavigator.ascx" TagName="Navigator" TagPrefix="uc2" %>
<%@ Register Src="OrderReceipt/Main.ascx" TagName="OrderReceipt" TagPrefix="uc2" %>
<%@ Register Src="~/Warehouse/InProcessLocation/Main.ascx" TagName="AsnReceipt" TagPrefix="uc2" %>

<uc2:Navigator ID="ucNavigator" runat="server" Visible="true" />
    <div class="ajax__tab_body">
        <uc2:OrderReceipt ID="ucOrderReceipt" runat="server" Visible="true" />
        <uc2:AsnReceipt ID="ucAsnReceipt" runat="server" Visible="false" />
    </div>
</div>