<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Reports_BillAging_Detail" %>
<%@ Register Src="~\Reports\ActBill\List.ascx" TagName="Detail" TagPrefix="uc2" %>
<div id="floatdiv" class="GridView">
    <fieldset>
        <legend>${Reports.BillAging.Detail}</legend>
        <uc2:Detail ID="ucDetail" runat="server" />
        <div class="tablefooter">
            <asp:Button ID="btnClose" runat="server" Text="${Common.Button.Close}" CssClass="button2"
                OnClick="btnClose_Click" />
        </div>
    </fieldset>
</div>
