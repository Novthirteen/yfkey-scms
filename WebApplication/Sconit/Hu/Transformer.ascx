<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Transformer.ascx.cs" Inherits="Hu_Transformer" %>
<%@ Register Src="~/Hu/TransformerDetail.ascx" TagName="TransformerDetail" TagPrefix="uc2" %>
<div class="GridView">
    <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
        OnRowDataBound="GV_List_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataOrderNo%>">
                <ItemTemplate>
                    <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("OrderNo") %>' />
                    <asp:HiddenField ID="hfOrderLocTransId" runat="server" Value='<%# Bind("OrderLocTransId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataSequence%>">
                <ItemTemplate>
                    <asp:Label ID="lblSequence" runat="server" Text='<%# Bind("Sequence") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                <ItemTemplate>
                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>' />
                     <asp:HiddenField ID="hfId" runat="server" Value='<%# Bind("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemDesc%>">
                <ItemTemplate>
                    <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("ItemDescription") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataReferenceItemCode%>">
                <ItemTemplate>
                    <asp:Label ID="lblReferenceItemCode" runat="server" Text='<%# Bind("ReferenceItemCode") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUom%>">
                <ItemTemplate>
                    <asp:Label ID="lblUom" runat="server" Text='<%# Bind("UomCode") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUnitCount%>">
                <ItemTemplate>
                    <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("UnitCount","{0:0.########}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLocationFrom%>" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblLocationFrom" runat="server" Text='<%# Bind("LocationFromCode") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLocationTo%>" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblLocationTo" runat="server" Text='<%# Bind("LocationToCode") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ActingQty">
                <ItemTemplate>
                    <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataReceivedQty%>" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblReceivedQty" runat="server" Text='<%# Bind("ReceivedQty","{0:0.########}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CurrentQty">
                <ItemTemplate>
                    <asp:TextBox ID="tbCurrentQty" runat="server" Text='<%# Bind("CurrentQty","{0:0.########}") %>'
                        onmouseup="if(!readOnly)select();" Width="50"></asp:TextBox>
                    <asp:RangeValidator ID="rvCurrentQty" ControlToValidate="tbCurrentQty" runat="server"
                        Display="Dynamic" ErrorMessage="*" MaximumValue="999999999" MinimumValue="0" Enabled="false"
                        Type="Double" />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="CurrentRejectQty" Visible="false">
                <ItemTemplate>
                    <asp:TextBox ID="tbCurrentRejectQty" runat="server" Text='<%# Bind("CurrentRejectQty","{0:0.########}") %>'
                        onmouseup="if(!readOnly)select();" Width="50"></asp:TextBox>
                    <asp:RangeValidator ID="rvRejectQty" ControlToValidate="tbCurrentRejectQty" runat="server"
                        Display="Dynamic" ErrorMessage="*" MaximumValue="999999999" MinimumValue="0" Enabled="false"
                        Type="Double" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuId%>">
                <ItemTemplate>
                    <uc2:TransformerDetail ID="ucTransformerDetail" runat="server" ReadOnly='<%# DetailReadOnly %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
