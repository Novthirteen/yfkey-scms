<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PreviewDetail.ascx.cs" Inherits="NewMrp_ShiftPlan_PreviewDetail" %>
<div class="GridView">
    <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_List_RowDataBound"
        ShowHeader="false">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblSequence" runat="server" Text='<%# Eval("Sequence")%>' />
                    <asp:HiddenField ID="hfFlowDetailId" runat="server" Value='<%# Eval("FlowDetail.Id")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item.Code")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("Item.Description")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("Uom.Code")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:TextBox ID="tbOrderedQty" runat="server" Text='<%# Eval("OrderedQty","{0:0.########}")%>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>
