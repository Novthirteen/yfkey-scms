<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StorageBinList.ascx.cs"
    Inherits="Inventory_Stocktaking_StorageBinList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <legend>${MasterData.Inventory.Stocktaking.StorageBin}</legend>
    <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_List_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="No." ItemStyle-Width="30">
                <ItemTemplate>
                    <asp:Literal ID="ltlSeq" runat="server" Text='<%# (Container as GridViewRow).RowIndex+1 %> ' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.Business.Bin}">
                <ItemTemplate>
                    <asp:Label ID="lblBinCode" runat="server" Text='<%# Bind("Code") %>' />
                    <uc3:textbox ID="tbBinCodeFrom" runat="server" Visible="false" Width="250" DescField="Description"
                        ValueField="Code" ServicePath="StorageBinMgr.service" ServiceMethod="GetStorageBinByLocation"
                        CssClass="inputRequired" MustMatch="true" />
                     <asp:Label ID="lbTo" Text="${Common.Business.To}" runat="server" Visible="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.Business.Bin}">
                <ItemTemplate>
                    <uc3:textbox ID="tbBinCodeTo" runat="server" Visible="false" Width="250" DescField="Description"
                        ValueField="Code" ServicePath="StorageBinMgr.service" ServiceMethod="GetStorageBinByLocation"
                        MustMatch="true" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Location.Bin.Description}">
                <ItemTemplate>
                    <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("Description") %>' />
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
