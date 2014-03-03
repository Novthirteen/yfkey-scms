<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Finance_PlanBill_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>

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
                <asp:TemplateField HeaderText=" ${MasterData.PlannedBill.OrderNo}" SortExpression="OrderNo">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "OrderNo")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="IpNo" HeaderText="${Common.Business.AsnNo}" SortExpression="IpNo" />
                <asp:BoundField DataField="ReceiptNo" HeaderText="${MasterData.PlannedBill.ReceiptNo}"
                    SortExpression="ReceiptNo" />
                <asp:TemplateField HeaderText=" ${Common.Business.Supplier}" SortExpression="BillAddress.Party.Name">
                    <ItemTemplate>
                        <asp:Label ID="lblSupplier" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BillAddress.Party.Code")%>'
                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "BillAddress.Party.Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${MasterData.Item}" SortExpression="Item.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItem" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Item.Code")%>'
                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "Item.Description")%>' />&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.ReferenceItemCode}" SortExpression="ReferenceItemCode"
                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ReferenceItemCode")%>
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
                <asp:TemplateField HeaderText="${MasterData.PlannedBill.ReceiveDate}" SortExpression="CreateDate">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "CreateDate", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.PlannedBill.UnitPrice}" SortExpression="UnitPrice">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfUnitPrice" runat="server" Value='<%# Bind("UnitPrice") %>' />
                        <%# DataBinder.Eval(Container.DataItem, "UnitPrice", "{0:0.########}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.PlannedBill.Currency}" SortExpression="Currency.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Currency.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.PlannedBill.PlannedQty}" SortExpression="PlannedQty">
                    <ItemTemplate>
                        <asp:Label ID="lbPlannedQty" runat="server" Text='<%# Bind("PlannedQty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.PlannedBill.PlannedAmount}">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "PlannedAmount", "{0:0.########}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.PlannedBill.ActingQty}" SortExpression="ActingQty">
                    <ItemTemplate>
                        <asp:Label ID="lbActingQty" runat="server" Text='<%# Bind("ActingQty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.PlannedBill.ActingAmount}">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ActingAmount", "{0:0.########}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.PlannedBill.CurrentActingQty}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbCurrentActingQty" runat="server" onmouseup="if(!readOnly)select();"
                            Width="50" ReadOnly="true"></asp:TextBox>
                        <asp:RangeValidator ID="rvCurrentActingQty" ControlToValidate="tbCurrentActingQty"
                            runat="server" Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}"
                            MaximumValue="999999999" MinimumValue="-99999999" Type="Double" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Literal ID="lblNoRecordFound" runat="server" Text="${Common.GridView.NoRecordFound}"
            Visible="false" />
    </div>
</fieldset>
