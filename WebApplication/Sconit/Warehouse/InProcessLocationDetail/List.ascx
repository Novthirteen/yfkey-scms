<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Warehouse_InProcessLocation_List" %>
<%@ Register Src="~/Hu/Transformer.ascx" TagName="Transformer" TagPrefix="uc2" %>
<fieldset>
    <legend>${InProcessLocation.InProcessLocationDetail}</legend>
    <uc2:Transformer ID="ucTransformer" runat="server" ReadOnly="true" DetailReadOnly="true"
        DetailVisible="false" />
    <div class="tablefooter">
        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
            CssClass="button2" />
    </div>
</fieldset>
