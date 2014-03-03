<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewQty.ascx.cs" Inherits="Inventory_InspectOrder_NewQty" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlLocation" runat="server" Text="${MasterData.InspectOrder.Location}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbLocation" runat="server" DescField="Name" ValueField="Code" ServicePath="LocationMgr.service"
                    ServiceMethod="GetLocationByUserCode" Width="250" CssClass="inputRequired" />
                <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="tbLocation"
                    ErrorMessage="${MasterData.InspectOrder.Location.Required}" Display="Dynamic" ValidationGroup="vgSaveGroup" />
            </td>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
        </tr>
        <tr>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
            <td class="td01">
            </td>
            <td class="td02">
                <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" Text="${Common.Button.Confirm}"
                    CssClass="button2" ValidationGroup="vgSaveGroup" />
                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                    CssClass="button2" />
            </td>
        </tr>
    </table>
</fieldset>
<fieldset>
    <legend>${MasterData.Inventory.InspectOrder.Detail}</legend>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' />
                        <uc3:textbox ID="tbItemCode" runat="server" Visible="false" Width="250" DescField="Description"
                            ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem"
                            CssClass="inputRequired"  MustMatch="true" OnTextChanged="tbItemCode_TextChanged"
                            AutoPostBack="true"  />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemDesc%>">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("Item.Description") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUom%>">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Item.Uom.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUnitCount%>">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("Item.UnitCount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataInspectQty%>">
                    <ItemTemplate>
                        <asp:TextBox ID="tbInspectQty" runat="server" onmouseup="if(!readOnly)select();"
                            Text='<%# Bind("InspectQty","{0:0.########}") %>' Width="50"></asp:TextBox>
                        <asp:RangeValidator ID="rvInspectQty" runat="server" ControlToValidate="tbInspectQty"
                            ErrorMessage="${Common.Validator.Valid.Number}" Display="Dynamic" Type="Double"
                            MinimumValue="0" MaximumValue="100000" ValidationGroup="vgSaveGroup" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
