<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Order_GoodsReceipt_ViewReceipt_List" %>
<%@ Register Src="~/Hu/Transformer.ascx" TagName="Transformer" TagPrefix="uc2" %>
<fieldset>
    <legend>${Receipt.ReceiptDetails}</legend>
    <uc2:Transformer ID="ucTransformer" runat="server" ReadOnly="true" DetailReadOnly="true"
        DetailVisible="false" />
</fieldset>
