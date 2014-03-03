<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Wap_Order_Receive_Main" %>
<div id="divOrder" runat="server">
    工单号:<asp:TextBox ID="tbOrderNo" runat="server" MaxLength="15" OnTextChanged="btnOrderNo_Click"></asp:TextBox>
</div>
<hr />
<asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
<div>
    <asp:GridView ID="gv_List" runat="server" AutoGenerateColumns="False" AllowSorting="false">
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                <ItemTemplate>
                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUom%>">
                <ItemTemplate>
                    <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Uom.Code") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUnitCount%>">
                <ItemTemplate>
                    <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("UnitCount","{0:0.########}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataOrderedQty%>">
                <ItemTemplate>
                    <asp:Label ID="tbOrderQty" runat="server" Text='<%# Bind("OrderedQty","{0:0.########}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Literal ID="ltlReceiveQty" runat="server" Text='<%$Resources:Language,MasterDataCurrentReceiveQty%>' />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="tbReceiveQty" runat="server" onmouseup="if(!readOnly)select();"
                        onchange="setChanged();" Width="50"  Text='<%# Bind("CurrentReceiveQty") %>'></asp:TextBox>
                    <asp:RangeValidator ID="rvReceiveQty" ControlToValidate="tbReceiveQty" runat="server"
                        Display="Dynamic" ErrorMessage="*" MaximumValue="999999999" MinimumValue="0"
                        Type="Double" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
     <asp:Button ID="btnReceive" runat="server" Text="收货" OnClick="btnReceive_Click" Visible="false"/>
    <asp:GridView ID="gv_Hu" runat="server" AutoGenerateColumns="false" Visible="false">
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuid%>" ItemStyle-Width="103px">
                <ItemTemplate>
                    <asp:Label ID="lblHuId" runat="server" Text='<%# Eval("Hu.HuId") %>' />
                    <asp:TextBox ID="tbHuId" runat="server" Text='<%# Bind("Hu.HuId") %>' Visible="false" Width="100px"
                        AutoPostBack="true" OnTextChanged="tbHuId_TextChanged" onclick="this.select();" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                <ItemTemplate>
                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Qty" DataFormatString="{0:0.########}" HeaderText="<%$Resources:Language,MasterDataQty%>" />
            <asp:TemplateField HeaderText="<%$Resources:Language,ButtonDelete%>" >
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDeleteHu" runat="server" Text="<%$Resources:Language,ButtonDelete%>"
                        CommandArgument='<%# Eval("Id") %>' OnClick="lbtnDelete_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Button ID="btn_Hu_Create" runat="server" Text="确定Hu" OnClick="btn_Hu_Create_Click" Visible="false"/>
</div>
