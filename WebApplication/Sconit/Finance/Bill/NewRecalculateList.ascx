<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewRecalculateList.ascx.cs"
    Inherits="Finance_Bill_NewRecalculateList" %>

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

    
</script>

<fieldset>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AllowPaging="False" DataKeyNames="Id" AllowSorting="False"
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
                <asp:TemplateField HeaderText=" ${MasterData.ActingBill.Supplier}" SortExpression="BillAddress.Party.Name">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "BillAddress.Party.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ReceiptNo" HeaderText="${MasterData.ActingBill.ReceiptNo}"
                    SortExpression="ReceiptNo" />
                <asp:BoundField DataField="ExternalReceiptNo" HeaderText="${MasterData.ActingBill.ExternalReceiptNo}"
                    SortExpression="ExternalReceiptNo" />
                <asp:TemplateField HeaderText=" ${Common.Business.ItemCode}" SortExpression="Item.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Item.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${Common.Business.ItemDescription}" SortExpression="Item.Description">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Item.Description")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${Common.Business.Uom}" SortExpression="Uom.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Uom.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ActingBill.EffectiveDate}" SortExpression="EffectiveDate">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "EffectiveDate", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.ActingBill.IsProvisionalEstimate}"
                    SortExpression="UnitPrice">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfUnitPrice" runat="server" Value='<%# Bind("UnitPrice") %>' />
                        <%# DataBinder.Eval(Container.DataItem, "UnitPrice", "{0:0.########}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ActingBill.Currency}" SortExpression="Currency.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Currency.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ActingBill.BillQty}" SortExpression="BillQty">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "BillQty", "{0:0.########}")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Literal ID="lblNoRecordFound" runat="server" Text="${Common.GridView.NoRecordFound}"
            Visible="false" />
    </div>
</fieldset>
