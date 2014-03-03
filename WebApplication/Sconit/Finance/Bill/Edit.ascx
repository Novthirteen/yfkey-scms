<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Finance_Bill_Edit" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<%@ Register Src="NewSearch.ascx" TagName="NewSearch" TagPrefix="uc" %>

<script language="javascript" type="text/javascript" src="Js/calcamount.js"></script>

<script type="text/javascript" language="javascript">
    function GVCheckClick() {
        if ($(".GVHeader input:checkbox").attr("checked") == true) {
            $(".GVRow input:checkbox").attr("checked", true);
            $(".GVAlternatingRow input:checkbox").attr("checked", true);
        }
        else {
            $(".GVRow input:checkbox").attr("checked", false);
            $(".GVAlternatingRow input:checkbox").attr("checked", false);
        }
    }
    function discountChanged(obj) {
        CalCulateRowAmount(obj, "tbDiscount", "BaseOnDiscount", "hfUnitPrice", "tbQty", "tbDiscount", "tbDiscountRate", "tbAmount", '#<%= tbTotalDiscount.ClientID %>', '#<%= tbTotalDiscountRate.ClientID %>', '#<%= tbTotalDetailAmount.ClientID %>', '#<%= tbTotalAmount.ClientID %>', '#<%= tbTotalQty.ClientID %>', false);
    }
    function qtyChanged(obj) {
        CalCulateRowAmount(obj, "tbQty", "BaseOnDiscountRate", "hfUnitPrice", "tbQty", "tbDiscount", "tbDiscountRate", "tbAmount", '#<%= tbTotalDiscount.ClientID %>', '#<%= tbTotalDiscountRate.ClientID %>', '#<%= tbTotalDetailAmount.ClientID %>', '#<%= tbTotalAmount.ClientID %>', '#<%= tbTotalQty.ClientID %>', false);
    }
    function discountRateChanged(obj) {
        CalCulateRowAmount(obj, "tbDiscountRate", "BaseOnDiscountRate", "hfUnitPrice", "tbQty", "tbDiscount", "tbDiscountRate", "tbAmount", '#<%= tbTotalDiscount.ClientID %>', '#<%= tbTotalDiscountRate.ClientID %>', '#<%= tbTotalDetailAmount.ClientID %>', '#<%= tbTotalAmount.ClientID %>', '#<%= tbTotalQty.ClientID %>', false);
    }
    function orderDiscountChanged(obj) {
        CalCulateTotalAmount("BaseOnDiscount", '#<%= tbTotalDiscount.ClientID %>', '#<%= tbTotalDiscountRate.ClientID %>', '#<%= tbTotalDetailAmount.ClientID %>', '#<%= tbTotalAmount.ClientID %>', null, 0, 0);
    }

    function orderDiscountRateChanged(obj) {
        CalCulateTotalAmount("BaseOnDiscountRate", '#<%= tbTotalDiscount.ClientID %>', '#<%= tbTotalDiscountRate.ClientID %>', '#<%= tbTotalDetailAmount.ClientID %>', '#<%= tbTotalAmount.ClientID %>', null, 0, 0);
    }
</script>

<fieldset>
    <legend>${MasterData.Bill.POBill}</legend>
    <asp:FormView ID="FV_Bill" runat="server" DataSourceID="ODS_Bill" DefaultMode="Edit"
        DataKeyNames="BillNo" OnDataBound="FV_Bill_DataBound">
        <EditItemTemplate>
            <table class="mtable">
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblBillNo" runat="server" Text="${MasterData.Bill.BillNo}:" />
                    </td>
                    <td class="td02">
                        <sc1:ReadonlyTextBox ID="tbBillNo" runat="server" CodeField="BillNo" />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="lblRefBillNo" runat="server" Text="${MasterData.Bill.RefBillNo}:" />
                    </td>
                    <td class="td02">
                        <asp:LinkButton ID="lbRefBillNo" runat="server" Text='<%# Bind("ReferenceBillNo") %>'
                            CommandArgument='<%# Bind("ReferenceBillNo") %>' OnClick="lbRefBillNo_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblParty" runat="server" Text="${MasterData.Bill.Supplier}:" />
                    </td>
                    <td class="td02">
                        <sc1:ReadonlyTextBox ID="tbParty" runat="server" CodeField="BillAddress.Party.Code"
                            DescField="BillAddress.Party.Name" />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="lblBillAddress" runat="server" Text="${MasterData.Bill.BillAddress}:" />
                    </td>
                    <td class="td02">
                        <sc1:ReadonlyTextBox ID="tbBillAddress" runat="server" CodeField="BillAddress.Code"
                            DescField="BillAddress.Address" />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblCreateDate" runat="server" Text="${MasterData.Bill.CreateDate}:" />
                    </td>
                    <td class="td02">
                        <sc1:ReadonlyTextBox ID="tbCreateDate" runat="server" CodeField="CreateDate" />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="lblCreateUser" runat="server" Text="${MasterData.Bill.CreateUser}:" />
                    </td>
                    <td class="td02">
                        <sc1:ReadonlyTextBox ID="tbCreateUser" runat="server" CodeField="CreateUser.Code"
                            DescField="CreateUser.Name" />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblStatus" runat="server" Text="${MasterData.Bill.Status}:" />
                    </td>
                    <td class="td02">
                        <sc1:ReadonlyTextBox ID="tbStatus" runat="server" CodeField="Status" />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="lblExternalBillNo" runat="server" Text="${MasterData.Bill.ExternalBillNo}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbExternalBillNo" runat="server" Text='<%# Bind("ExternalBillNo") %>' />
                    </td>
                </tr>
            </table>
        </EditItemTemplate>
    </asp:FormView>
    <div class="tablefooter">
        <sc1:Button ID="btnSave" runat="server" Text="${Common.Button.Save}" Width="59px"
            OnClick="btnSave_Click" FunctionId="EditBill" />
        <sc1:Button ID="btnSubmit" runat="server" Text="${Common.Button.Submit}" Width="59px"
            OnClick="btnSubmit_Click" FunctionId="EditBill" />
        <asp:Button ID="btnPrint" runat="server" Text="${Common.Button.Print}" Width="59px"
            OnClick="btnPrint_Click" />
        <sc1:Button ID="btnDelete" runat="server" Text="${Common.Button.Delete}" Width="59px"
            OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" OnClick="btnDelete_Click"
            FunctionId="EditBill" />
        <sc1:Button ID="btnClose" runat="server" Text="${Common.Button.Close}" Width="59px"
            OnClientClick="return confirm('${Common.Button.Close.Confirm}')" OnClick="btnClose_Click"
            FunctionId="CloseBill" />
        <sc1:Button ID="btnCancel" runat="server" Text="${Common.Button.Cancel}" Width="59px"
            OnClientClick="return confirm('${Common.Button.Cancel.Confirm}')" OnClick="btnCancel_Click"
            FunctionId="CancelBill" />
        <sc1:Button ID="btnVoid" runat="server" Text="${Common.Button.Void}" Width="59px"
            OnClientClick="return confirm('${Common.Button.Void.Confirm}')" OnClick="btnVoid_Click"
            FunctionId="VoidBill" />
        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" Width="59px"
            OnClick="btnBack_Click" />
    </div>
</fieldset>
<asp:ObjectDataSource ID="ODS_Bill" runat="server" TypeName="com.Sconit.Web.BillMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Bill" SelectMethod="LoadBill">
    <SelectParameters>
        <asp:Parameter Name="billNo" Type="String" />
        <asp:Parameter Name="includeDetail" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>
<fieldset>
    <legend>${MasterData.Bill.BillDetail}</legend>
    <div class="GridView">
        <asp:GridView ID="Gv_List" runat="server" AllowPaging="False" DataKeyNames="Id" AllowSorting="False"
            AutoGenerateColumns="False" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div onclick="GVCheckClick()">
                            <asp:CheckBox ID="CheckAll" runat="server" />
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:HiddenField ID="hfId" runat="server" Value='<%# Bind("Id") %>' />
                        <asp:CheckBox ID="CheckBoxGroup" name="CheckBoxGroup" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${MasterData.Bill.ReceiptNo}" SortExpression="ActingBill.ReceiptNo">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ActingBill.ReceiptNo")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${MasterData.Bill.ExternalReceiptNo}" SortExpression="ActingBill.ExternalReceiptNo">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ActingBill.ExternalReceiptNo")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${Common.Business.ItemCode}" SortExpression="ActingBill.Item.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ActingBill.Item.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${Common.Business.ItemDescription}" SortExpression="ActingBill.Item.Description">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ActingBill.Item.Description")%>
                    </ItemTemplate>
                </asp:TemplateField>
                    <%-- added by williamlu@esteering 2012/4/12 --%>
                <asp:TemplateField HeaderText=" ASN " SortExpression="IpNo">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "IpNo")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- added end --%> 

                <asp:TemplateField HeaderText=" ${Common.Business.RefCode}">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ActingBill.ReferenceItemCode")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${Common.Business.Uom}" SortExpression="ActingBill.Uom.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ActingBill.Uom.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ActingBill.EffectiveDate}" SortExpression="ActingBill.EffectiveDate">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ActingBill.EffectiveDate", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>
             <asp:TemplateField HeaderText="${MasterData.Bill.UnitPrice}" SortExpression="UnitPrice">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfUnitPrice" runat="server" Value='<%# Bind("UnitPrice") %>' />
                        <%# DataBinder.Eval(Container.DataItem, "UnitPrice", "{0:0.########}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--   <asp:TemplateField HeaderText="${MasterData.Bill.Currency}" SortExpression="Currency.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Currency.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="${MasterData.ActingBill.BillQty}">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ActingBill.BillQty", "{0:0.########}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ActingBill.BilledQty}">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ActingBill.BilledQty", "{0:0.########}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ActingBill.CurrentBillQty}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbQty" runat="server" onmouseup="if(!readOnly)select();" Width="50"
                            Text='<%# Bind("BilledQty","{0:0.########}") %>' onchange="qtyChanged(this);"></asp:TextBox>
                        <span style="display: none">
                            <asp:TextBox ID="tbDiscountRate" runat="server" Text="0" />
                            <asp:TextBox ID="tbDiscount" runat="server" Text="0" />
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="${MasterData.ActingBill.DiscountRate}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbDiscountRate" runat="server" onmouseup="if(!readOnly)select();" Text='<%# Bind("DiscountRate","{0:0.########}") %>'
                            Width="50" onchange="discountRateChanged(this);" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ActingBill.Discount}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbDiscount" runat="server" onmouseup="if(!readOnly)select();" Text='<%# Bind("Discount","{0:0.########}") %>'
                            Width="50" onchange="discountChanged(this);" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="${MasterData.ActingBill.Amount}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbAmount" runat="server" Width="80px" onfocus="this.blur();" Text='<%# Bind("Amount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Button ID="btnAddDetail" runat="server" Text="${Common.Button.New}" OnClick="btnAddDetail_Click" />
                    <asp:Button ID="btnDeleteDetail" runat="server" Text="${Common.Button.Remove}" OnClick="btnDeleteDetail_Click" />
                </td>
                <td class="td02">
                </td>
                <td class="ttd01">
                </td>
                <td class="ttd02">
                </td>
                <td class="ttd01">
                    <asp:Literal ID="lblTotalQty" runat="server" Text="${MasterData.Bill.TotalQty}:" />
                </td>
                <td class="ttd02">
                    <asp:TextBox ID="tbTotalQty" runat="server" onfocus="this.blur();" Width="150px" />
                </td>
                <td class="ttd01">
                    <asp:Literal ID="lblTotalDetailAmount" runat="server" Text="${MasterData.Bill.TotalDetailAmount}:" />
                </td>
                <td class="ttd02">
                    <asp:TextBox ID="tbTotalDetailAmount" runat="server" onfocus="this.blur();" Width="150px" />
                    <span style="display: none">
                        <asp:TextBox ID="tbTotalDiscountRate" runat="server" Text="0" />
                        <asp:TextBox ID="tbTotalDiscount" runat="server" Text="0" />
                        <asp:TextBox ID="tbTotalAmount" runat="server" Visible="false" onfocus="this.blur();"
                            Width="150px" />
                    </span>
                </td>
            </tr>
            <%--<tr>
                <td class="td02">
                </td>
                <td class="td02">
                </td>
                <td class="td01">
                    <asp:Literal ID="lblTotalDiscount" runat="server" Text="${MasterData.Bill.TotalDiscount}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbTotalDiscountRate" runat="server" onChange="orderDiscountRateChanged(this);" Width="65px" />%
                    <asp:TextBox ID="tbTotalDiscount" runat="server" onChange="orderDiscountChanged(this);" Width="65px" />
                </td>
            </tr>
            <tr>
                <td class="td02">
                </td>
                <td class="td02">
                </td>
                <td class="td01">
                    <asp:Literal ID="lblTotalAmount" runat="server" Text="${MasterData.Bill.TotalAmount}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbTotalAmount" runat="server" Visible="true" onfocus="this.blur();" Width="150px" />
                </td>
            </tr>--%>
        </table>
    </div>
</fieldset>
<div id="floatdiv">
    <uc:NewSearch ID="ucNewSearch" runat="server" Visible="false" />
</div>
