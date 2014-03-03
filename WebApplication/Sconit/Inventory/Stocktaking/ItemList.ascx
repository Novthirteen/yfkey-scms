<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemList.ascx.cs" Inherits="Inventory_Stocktaking_ItemList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <legend>${MasterData.Inventory.Stocktaking.Item}</legend>
    <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_List_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="No." ItemStyle-Width="30">
                <ItemTemplate>
                    <asp:Literal ID="ltlSeq" runat="server" Text='<%# (Container as GridViewRow).RowIndex+1 %> ' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                <ItemTemplate>
                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Code") %>' />
                    <uc3:textbox ID="tbItemCode" runat="server" Visible="false" Width="250" DescField="Description"
                        ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem"
                        CssClass="inputRequired" MustMatch="true" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemDesc%>">
                <ItemTemplate>
                    <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("Description") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:Language,UOM%>">
                <ItemTemplate>
                    <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Uom.Code") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.GridView.Action}">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnAdd" runat="server" Text="${Common.Button.New}" OnClick="lbtnAdd_Click"
                        Visible="false">
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtnDelete" runat="server" Text="${Common.Button.Delete}" OnClick="lbtnDelete_Click"
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>' Visible="false">
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</fieldset>
