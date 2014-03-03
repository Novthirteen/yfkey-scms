<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ResultDetail.ascx.cs"
    Inherits="MasterData_Reports_WoReceipt_ResultDetail" %>

<div id="floatdiv" class="GridView">
    <fieldset>
        <legend>${MasterData.Inventory.Stocktaking.ResultDetail}</legend>
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="No." ItemStyle-Width="30">
                    <ItemTemplate>
                        <asp:Literal ID="ltlSeq" runat="server" Text='<%# (Container as GridViewRow).RowIndex+1 %> ' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Item}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemDescription}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("ItemDesc") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.HuId}">
                    <ItemTemplate>
                        <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("HuId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Qty}">
                    <ItemTemplate>
                        <asp:Label ID="lblQty" runat="server" Text='<%# Bind("RecQty", "{0:#.######}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="tablefooter">
            <asp:Button ID="btnClose" runat="server" Text="${Common.Button.Close}" CssClass="button2"
                OnClick="btnClose_Click" />
        </div>
    </fieldset>
</div>
