<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Finance_Bill_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="ORDER_ID"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="QAD_ORDER_ID" HeaderText="${MasterData.Bill.BillNo}" SortExpression="QAD_ORDER_ID" />
                <asp:BoundField DataField="PARTY_FROM_ID" HeaderText="${MasterData.Order.OrderHead.PartyFrom.Supplier}"
                    SortExpression="PARTY_FROM_ID" />
                <asp:BoundField DataField="ORDER_TYPE_NAME" HeaderText="${MasterData.KPBill.OrderTypeName}"
                    SortExpression="ORDER_TYPE_NAME" />
                <asp:BoundField DataField="ORDER_PUB_DATE" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                    HeaderText="${MasterData.KBBill.PublicDate}" SortExpression="CreateDate" />
                  <asp:TemplateField HeaderText="${MasterData.Bill.TotalAmount}">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Amount") %>
                      
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ORDER_PRINT" HeaderText="${MasterData.KPBill.Printed}"
                    SortExpression="ORDER_PRINT" />
                <asp:TemplateField HeaderText="${MasterData.KPBill.InvoiceStatus}">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="cmlInvoiceStatus" Code="InvoiceStatus" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "InvoiceStatus") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ORDER_ID") %>'
                            Text="${Common.Button.View}" OnClick="lbtnView_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
