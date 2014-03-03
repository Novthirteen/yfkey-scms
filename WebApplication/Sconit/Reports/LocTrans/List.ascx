<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Reports_LocTrans_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<%@ Register Src="~/Visualization/GoodsTraceability/Traceability/View.ascx" TagName="View"
    TagPrefix="uc" %>
<fieldset>
    <div class="GridView">
        <sc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" seqtext="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="${Common.Business.EffDate}" SortExpression="EffDate">
                    <ItemTemplate>
                        <asp:Label ID="lblEffDate" runat="server" Text='<%# Eval("EffDate","{0:yyyy-MM-dd}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Visualization.GoodsTraceability.TransType}" SortExpression="TransType">
                    <ItemTemplate>
                        <asp:Label ID="lblTransType" runat="server" Text='<%# Eval("TransType")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.OrderNo}" SortExpression="OrderNo">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderNo" runat="server" Text='<%# Eval("OrderNo")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="${Common.Business.IpNo}" SortExpression="IpNo">
                    <ItemTemplate>
                        <asp:Label ID="lblIpNo" runat="server" Text='<%# Eval("IpNo")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="${Common.Business.ReceiptNo}" SortExpression="RecNo">
                    <ItemTemplate>
                        <asp:Label ID="lblRecNo" runat="server" Text='<%# Eval("RecNo")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Supplier}" SortExpression="PartyFromName">
                    <ItemTemplate>
                        <asp:Label ID="lblSupplier" runat="server" Text='<%# Eval("PartyFromName")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Customer}" SortExpression="PartyToName">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("PartyToName")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Location}" SortExpression="Loc">
                    <ItemTemplate>
                        <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Loc")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Location.Name}" SortExpression="LocName">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationName" runat="server" Text='<%# Eval("LocName")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemCode}" SortExpression="Item">
                    <ItemTemplate>
                        <asp:Label ID="lblItem" runat="server" Text='<%# Eval("Item")%>' />&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemDescription}" SortExpression="ItemDescription">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("ItemDescription")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Uom}" SortExpression="Uom">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Eval("Uom")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="${Common.Business.LotNo}" SortExpression="LotNo">
                    <ItemTemplate>
                        <asp:Label ID="lblLotNo" runat="server" Text='<%# Eval("LotNo")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Qty}" SortExpression="Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty","{0:0.########}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.CreateUser}" SortExpression="CreateUser">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateUser" runat="server" Text='<%# Eval("CreateUser.Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </sc1:GridView>
        <sc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </sc1:GridPager>
    </div>
</fieldset>
