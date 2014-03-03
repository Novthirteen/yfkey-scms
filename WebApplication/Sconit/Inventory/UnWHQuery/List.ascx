<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Inventory_UnWHQuery_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False"  
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount"  
            >
            <Columns>
                <asp:TemplateField HeaderText="库位"  >
                    <ItemTemplate>
                        <asp:Label ID="lblPartyName" runat="server" Text='<%# Eval("Location")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="物料代码"  >
                    <ItemTemplate>
                        <asp:Label ID="lblOrderNo" runat="server" Text='<%# Eval("ItemCode")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="物料描述"  >
                    <ItemTemplate>
                        <asp:Label ID="lblReceiptNo" runat="server" Text='<%# Eval("Desc1")%>' />&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="条形码"  >
                    <ItemTemplate>
                        <asp:Label ID="lblExternalReceiptNo" runat="server" Text='<%# Eval("HuId")%>' />&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="批次"  >
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("LotNo")%>' />&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="数量"  >
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Eval("Qty","{0:0}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="下线时间"  >
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("CreateDate")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
                <%--<asp:TemplateField HeaderText="${Reports.ActBill.UnitCount}" SortExpression="UnitCount">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Eval("UnitCount", "{0:0.###}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.ActBill.Qty}" SortExpression="Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty", "{0:0.###}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.ActBill.EffectiveDate}" SortExpression="EffectiveDate">
                    <ItemTemplate>
                        <asp:Label ID="lblEffectiveDate" runat="server" Text='<%# Eval("EffectiveDate")%>' />
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
