<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CycleCountResultList.ascx.cs" Inherits="Inventory_Stocktaking_CycleCountResultList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <legend>${Common.Business.OrderDetails}</legend>
    <asp:GridView ID="GV_List" runat="server" AllowPaging="False" DataKeyNames="Id" AllowSorting="False"
        AutoGenerateColumns="False" OnRowDataBound="GV_List_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="No." ItemStyle-Width="30">
                <ItemTemplate>
                    <asp:Literal ID="ltlSeq" runat="server" Text='<%# (Container as GridViewRow).RowIndex+1 %> ' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.Business.ItemCode}">
                <ItemTemplate>
                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.Business.ItemDescription}">
                <ItemTemplate>
                    <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("Item.Description") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.Business.Uom}">
                <ItemTemplate>
                    <asp:Label ID="lblUOM" runat="server" Text='<%# Bind("Item.Uom.Code") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.Business.UnitCount}">
                <ItemTemplate>
                    <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("Item.UnitCount","{0:0.########}") %>' />
                </ItemTemplate>
            </asp:TemplateField>        
            <asp:TemplateField HeaderText="${CycCnt.PhysicalCount}">
                <ItemTemplate>
                    <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${CycCnt.InvQty}">
                <ItemTemplate>
                    <asp:Label ID="lblInvQty" runat="server" Text='<%# Bind("InvQty","{0:0.########}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${CycCnt.DiffQty}">
                <ItemTemplate>
                    <asp:Label ID="lblDiffQty" runat="server" Text='<%# Bind("DiffQty","{0:0.########}") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${CycCnt.ReferenceLocation}">
                <ItemTemplate>
                    <asp:Label ID="lblReferenceLocation" runat="server" Text='<%# Bind("ReferenceLocation.Code") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${CycCnt.DiffReason}">
                <ItemTemplate>
                    <asp:TextBox ID="tbDiffReason" runat="server" Text='<%# Bind("DiffReason") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</fieldset>
