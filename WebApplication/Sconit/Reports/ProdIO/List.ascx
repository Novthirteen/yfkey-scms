<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Reports_ProdIO_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<%@ Register Src="~/Reports/ProdIO/IOList.ascx" TagName="IOList" TagPrefix="uc" %>
<fieldset>
    <div class="GridView">
        <sc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowPaging="True" PagerID="gp" Width="100%" CellMaxLength="10"
            OnRowDataBound="GV_List_RowDataBound" SearchMode="Customize">
            <Columns>
                <asp:TemplateField HeaderText="${Reports.ProdIO.FGItem}" SortExpression="Item.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item.Code")%>' />&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Description}" SortExpression="Item.Description">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("Item.Description")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Uom}" SortExpression="Uom">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Eval("Uom")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Qty}" SortExpression="ReceivedQty">
                    <ItemTemplate>
                        <asp:Label ID="lblQty" runat="server" Text='<%# Eval("ReceivedQty","{0:0.########}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.LocTrans.Out}">
                    <ItemTemplate>
                        <uc:IOList ID="ucOutList" runat="server" DataSource='<%# Eval("OutList")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </sc1:GridView>
        <sc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </sc1:GridPager>
    </div>
</fieldset>
