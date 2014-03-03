<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Reports_InvIOB_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<fieldset>
    <div class="GridView">
        <sc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" seqtext="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="${Common.Business.ItemCode}" SortExpression="Item.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item.Code")%>' />&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemDescription}" SortExpression="Item.Description">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("Item.Description")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Location}" SortExpression="Location.Name">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationName" runat="server" Text='<%# Eval("Location.Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Uom}" SortExpression="Item.Uom.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("Item.Uom.Code")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="StartInvQty" HeaderText="${Reports.StartInv}" DataFormatString="{0:0.###}"
                    ItemStyle-Font-Bold="true" />
                <asp:BoundField DataField="RCTPO" HeaderText="${Reports.TransType.RCTPO}" DataFormatString="{0:0.###}" />
                <asp:BoundField DataField="RCTTR" HeaderText="${Reports.TransType.RCTTR}" DataFormatString="{0:0.###}" />
                <asp:BoundField DataField="RCTINP" HeaderText="${Reports.TransType.RCTINP}" DataFormatString="{0:0.###}" />
                <asp:BoundField DataField="RCTWO" HeaderText="${Reports.TransType.RCTWO}" DataFormatString="{0:0.###}" />
                <asp:BoundField DataField="RCTUNP" HeaderText="${Reports.TransType.RCTUNP}" DataFormatString="{0:0.###}" />
                <asp:BoundField DataField="TotalInQty" HeaderText="${Reports.TotalInQty}" DataFormatString="{0:0.###}"
                    ItemStyle-Font-Bold="true" />
                <asp:BoundField DataField="ISSSO" HeaderText="${Reports.TransType.ISSSO}" DataFormatString="{0:0.###}" />
                <asp:BoundField DataField="ISSTR" HeaderText="${Reports.TransType.ISSTR}" DataFormatString="{0:0.###}" />
                <asp:BoundField DataField="ISSINP" HeaderText="${Reports.TransType.ISSINP}" DataFormatString="{0:0.###}" />
                <asp:BoundField DataField="ISSWO" HeaderText="${Reports.TransType.ISSWO}" DataFormatString="{0:0.###}" />
                <asp:BoundField DataField="ISSUNP" HeaderText="${Reports.TransType.ISSUNP}" DataFormatString="{0:0.###}" />
                <asp:BoundField DataField="TotalOutQty" HeaderText="${Reports.TotalOutQty}" DataFormatString="{0:0.###}"
                    ItemStyle-Font-Bold="true" />
                <asp:BoundField DataField="CYCCNT" HeaderText="${Reports.TransType.CYCCNT}" DataFormatString="{0:0.###}" />
                <asp:BoundField DataField="NoStatsQty" HeaderText="${Reports.TransType.NoStatsQty}"
                    DataFormatString="{0:0.###}" />
                <asp:BoundField DataField="InvQty" HeaderText="${Reports.EndInv}" DataFormatString="{0:0.###}"
                    ItemStyle-Font-Bold="true" />
            </Columns>
        </sc1:GridView>
        <sc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </sc1:GridPager>
    </div>
</fieldset>
