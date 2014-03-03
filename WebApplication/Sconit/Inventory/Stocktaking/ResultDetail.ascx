<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ResultDetail.ascx.cs"
    Inherits="Inventory_Stocktaking_ResultDetail" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>

<div id="floatdiv" class="GridView">
    <fieldset>
        <legend>${MasterData.Inventory.Stocktaking.ResultDetail}</legend> 
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div>
                            <asp:CheckBox ID="CheckAll" runat="server"  AutoPostBack="true" OnCheckedChanged="CheckAllChanged"  />
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBoxGroup" name="CheckBoxGroup" runat="server" Visible="false" />
                        <asp:HiddenField ID="hfId" runat="server" Value='<%# Bind("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="No." ItemStyle-Width="30">
                    <ItemTemplate>
                        <asp:Literal ID="ltlSeq" runat="server" Text='<%# (Container as GridViewRow).RowIndex+1 %> ' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Item}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemDescription}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("Item.Description") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Uom}">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Item.Uom.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.HuId}">
                    <ItemTemplate>
                        <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("HuId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.LotNo}">
                    <ItemTemplate>
                        <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LotNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Inventory.Stocktaking.DetailQty}">
                    <ItemTemplate>
                        <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty", "{0:#.######}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Inventory.Stocktaking.DetailQty}">
                    <ItemTemplate>
                        <asp:Label ID="lblInvQty" runat="server" Text='<%# Bind("InvQty", "{0:#.######}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="tablefooter">
             <cc1:Button ID="btnAdjust" runat="server" Text="${Common.Button.Adjust}" OnClick="btnAdjust_Click" Visible="false" 
                    Width="59px" CssClass="button2"   FunctionId="CycleCountAdjust"/>
            <asp:Button ID="btnClose" runat="server" Text="${Common.Button.Close}" CssClass="button2"
                OnClick="btnClose_Click" />
        </div>
    </fieldset>
</div>
