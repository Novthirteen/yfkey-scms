<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HuView.ascx.cs" Inherits="Visualization_LocationBin_HuView" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<div id="floatdiv">
    <div id='floatdivtitle'>
        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" CssClass="btnClose" />
    </div>
    <fieldset>
        <div class="GridView">
            <sc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" seqtext="No."
                ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
                CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
                SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="Location" HeaderText="${Common.Business.Location}" SortExpression="Location" />
                    <asp:BoundField DataField="Bin" HeaderText="${MasterData.Location.Bin}" SortExpression="Bin" />
                    <asp:TemplateField HeaderText="${Common.Business.HuId}" SortExpression="HuId">
                        <ItemTemplate>
                            <asp:Label ID="lblHuId" runat="server" Text='<%# Eval("HuId")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.LotNo}" SortExpression="LotNo">
                        <ItemTemplate>
                            <asp:Label ID="lblLotNo" runat="server" Text='<%# Eval("LotNo")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.ItemCode}" SortExpression="Item">
                        <ItemTemplate>
                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item")%>' />
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
                    <asp:TemplateField HeaderText="${Common.Business.Qty}" SortExpression="Qty">
                        <ItemTemplate>
                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty","{0:0.########}")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </sc1:GridView>
            <sc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
            </sc1:GridPager>
        </div>
    </fieldset>
</div>
<div id='divHide' />
