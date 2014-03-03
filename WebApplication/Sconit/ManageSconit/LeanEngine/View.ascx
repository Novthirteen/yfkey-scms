<%@ Control Language="C#" AutoEventWireup="true" CodeFile="View.ascx.cs" Inherits="ManageSconit_LeanEngine_View" %>
<%@ Register Src="~/Order/OrderDetail/List.ascx" TagName="OrderDetailList" TagPrefix="uc" %>
<div id="floatdiv">
    <uc:OrderDetailList ID="ucOrderDetailList" runat="server" />
    <div class="tablefooter">
        <asp:Button ID="btnCancel" runat="server" Text="${Common.Button.Cancel}" OnClick="btnCancel_Click"
            CssClass="button2" />
    </div>
</div>
