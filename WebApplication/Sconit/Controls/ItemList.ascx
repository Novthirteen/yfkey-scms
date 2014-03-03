<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemList.ascx.cs" Inherits="Controls_ItemList" %>
<%@ Register Src="~/Controls/UpdateProgress.ascx" TagName="UpdateProgress" TagPrefix="uc2" %>
<fieldset>
    <legend>${Common.Business.OrderDetails}</legend>
    <asp:UpdatePanel ID="UP_GV_List" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_List_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="No." ItemStyle-Width="30">
                        <ItemTemplate>
                            <asp:Literal ID="ltlSeq" runat="server" Text='<%# (Container as GridViewRow).RowIndex+1 %> ' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                        <ItemTemplate>
                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>' />
                            <asp:TextBox ID="tbItemCode" runat="server" Text='<%# Bind("ItemCode") %>' Visible="false"
                                AutoPostBack="true" OnTextChanged="tbItemCode_TextChanged" onclick="this.select();" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemDesc%>">
                        <ItemTemplate>
                            <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("ItemDescription") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,UOM%>">
                        <ItemTemplate>
                            <asp:Label ID="lblUom" runat="server" Text='<%# Bind("UomCode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,UnitCount%>">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("UnitCount","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataQty%>">
                        <ItemTemplate>
                            <asp:TextBox ID="tbCurrentQty" runat="server" Text='<%# Bind("CurrentQty","{0:0.########}") %>'
                                Width="50" AutoPostBack="true" OnTextChanged="tbCurrentQty_TextChanged" />
                            <asp:RangeValidator ID="rvCurrentQty" ControlToValidate="tbCurrentQty" runat="server"
                                Display="Dynamic" ErrorMessage="*" MaximumValue="999999999" MinimumValue="-999999999"
                                Type="Double" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <uc2:UpdateProgress ID="ucUpdateProgress" runat="server" />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
        </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>
