<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Finance_Bill_Edit" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>
<fieldset>
    <div runat="server">
        <script language="javascript" type="text/javascript">
            function calculate() {
                var tax = $('#<%= tbInvoiceTax.ClientID %>').val();
                var amountWithoutTax = $('#<%= tbInvoiceAmountWithoutTax.ClientID %>').val();
                var totalAmount = 0;
                if (tax != "" && !isNaN(tax)) {
                    totalAmount += parseFloat(tax);
                }
                if (amountWithoutTax != "" && !isNaN(amountWithoutTax)) {
                    totalAmount += parseFloat(amountWithoutTax);
                }
                $('#<%= tbInvoiceAmount.ClientID %>').attr('value', totalAmount);
            }
        </script>
    </div>
    <legend>${MasterData.Bill.POBill}</legend>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblOrderId" runat="server" Text="${MasterData.Bill.BillNo}:" />
            </td>
            <td class="td02">
                <sc1:ReadonlyTextBox ID="tbOrderId" runat="server" CodeField="QAD_ORDER_ID" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblCreateDate" runat="server" Text="${MasterData.Bill.CreateDate}:" />
            </td>
            <td class="td02">
                <sc1:ReadonlyTextBox ID="tbCreateDate" runat="server" CodeField="ORDER_PUB_DATE" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblTotalAmount" runat="server" Text="${MasterData.Bill.TotalAmount}:" />
            </td>
            <td class="td02">
                <sc1:ReadonlyTextBox ID="tbTotalAmount" runat="server" />
            </td>
            <td class="td01">
                   <asp:Literal ID="lblCln" runat="server" Text="索赔发票：" />
            </td>
            <td class="td02">
             <sc1:ReadonlyTextBox ID="tbCln" runat="server" />
            </td>
        </tr>
    </table>
    <div class="tablefooter">
        <sc1:Button ID="btnSubmitInvoice" runat="server" Text="${MasterData.Button.Bill.SubmitInvoice}"
            Width="80px" OnClick="btnSubmitInvoice_Click" ValidationGroup="vgSubmitInvoice"
            FunctionId="SubmitInvoice" />
        <sc1:Button ID="btnRejectInvoice" runat="server" Text="${MasterData.Button.Bill.RejectInvoice}"
            Width="80px" OnClick="btnRejectInvoice_Click" FunctionId="RejectInvoice" />
        <sc1:Button ID="btnApproveInvoice" runat="server" Text="${MasterData.Button.Bill.ApproveInvoice}"
            Width="80px" OnClick="btnApproveInvoice_Click" FunctionId="ApproveInvoice" />
        <asp:Button ID="btnPrint" runat="server" Text="${Common.Button.Print}" Width="59px"
            OnClick="btnPrint_Click" />
        <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" Width="59px"
            OnClick="btnExport_Click" />
        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" Width="59px"
            OnClick="btnBack_Click" />
    </div>
</fieldset>
<fieldset>
    <legend>${MasterData.Bill.BillDetail}</legend>
    <div class="GridView">
        <asp:GridView ID="Gv_List" runat="server" AllowPaging="False" DataKeyNames="item_seq_id"
            AllowSorting="False" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText=" ${MasterData.KPBill.PurchaseOrderId}" SortExpression="purchase_order_id">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "purchase_order_id")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${Common.Business.ItemCode}" SortExpression="part_code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "part_code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${MasterData.KPBill.InComingOrderId}" SortExpression="incoming_order_id">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "incoming_order_id")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.KPBill.SeqId}" SortExpression="seq_id">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "seq_id")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.KPBill.InComingQty}" SortExpression="incoming_qty">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "incoming_qty", "{0:0.########}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.KPBill.Price}">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "price1")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.KPBill.Uom}" SortExpression="um">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "um")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.KPBill.Price1}">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "price", "{0:0.########}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.KPBill.Price2}">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "price2", "{0:0.########}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${Common.Business.ItemDescription}" SortExpression="part_name">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "part_name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.KPBill.DeliverOrderId}" SortExpression="deliver_order_id">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "deliver_order_id")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.KPBill.IncomingDate}" SortExpression="incoming_date">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "incoming_date", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
<div runat="server" id="div_inv">
<fieldset>
    <legend>${MasterData.KPBill.InvoiceDetail}</legend>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblInvoiceCount" runat="server" Text="${MasterData.Bill.InvoiceCount}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbInvoceCount" runat="server" CssClass="inputRequired" />
                <asp:RequiredFieldValidator ID="rfvInvoiceCount" runat="server" ValidationGroup="vgSubmitInvoice"
                    ControlToValidate="tbInvoceCount" ErrorMessage="${MasterData.Bill.InvoiceCount}${Common.Business.Required}" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblInvoiceDate" runat="server" Text="${MasterData.Bill.InvoiceDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbInvoiceDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                    CssClass="inputRequired" />
                <asp:RequiredFieldValidator ID="rfvInvoiceDate" runat="server" ValidationGroup="vgSubmitInvoice"
                    ControlToValidate="tbInvoiceDate" ErrorMessage="${MasterData.Bill.InvoiceDate}${Common.Business.Required}" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblInvoiceTax" runat="server" Text="${MasterData.Bill.InvoiceTax}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbInvoiceTax" runat="server" CssClass="inputRequired" />
                <asp:RequiredFieldValidator ID="rfvInvoiceTax" runat="server" ValidationGroup="vgSubmitInvoice"
                    ControlToValidate="tbInvoiceTax" ErrorMessage="${MasterData.Bill.InvoiceTax}${Common.Business.Required}" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblInvoiceAmountWithoutTax" runat="server" Text="${MasterData.Bill.InvoiceAmountWithoutTax}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbInvoiceAmountWithoutTax" runat="server" CssClass="inputRequired" />
                <asp:RequiredFieldValidator ID="rfvInvoiceAmountWithoutTax" runat="server" ValidationGroup="vgSubmitInvoice"
                    ControlToValidate="tbInvoiceAmountWithoutTax" ErrorMessage="${MasterData.Bill.InvoiceAmountWithoutTax}${Common.Business.Required}" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblInvoiceAmount" runat="server" Text="${MasterData.Bill.InvoiceAmount}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbInvoiceAmount" runat="server" CssClass="inputRequired" onfocus="this.blur();" />
                <asp:RequiredFieldValidator ID="rfvInvoiceAmount" runat="server" ValidationGroup="vgSubmitInvoice"
                    ControlToValidate="tbInvoiceAmount" ErrorMessage="${MasterData.Bill.InvoiceAmount}${Common.Business.Required}" />
            </td>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblInvoiceNumber" runat="server" Text="${MasterData.Bill.InvoiceNumber}:" />
            </td>
            <td class="td02" colspan="3">
                <asp:TextBox ID="tbInvoiceNumber" runat="server" TextMode="MultiLine" Width="650"
                    Height="50" CssClass="inputRequired" />
                <asp:RequiredFieldValidator ID="rfvInvoiceNumber" runat="server" ValidationGroup="vgSubmitInvoice"
                    ControlToValidate="tbInvoiceNumber" ErrorMessage="${MasterData.Bill.InvoiceNumber}${Common.Business.Required}" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblInvoiceRemark" runat="server" Text="${MasterData.Bill.InvoiceRemark}:" />
            </td>
            <td class="td02" colspan="3">
                <asp:TextBox ID="tbInvoiceRemark" runat="server" Width="650" />
            </td>
        </tr>
    </table>
</fieldset></div>
