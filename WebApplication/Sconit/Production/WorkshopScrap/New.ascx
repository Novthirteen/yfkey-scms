<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Inventory_InspectOrder_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblFlow" runat="server" Text="${MasterData.Flow.Flow.Production}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbFlow" runat="server" Visible="true" DescField="Description" ValueField="Code"
                    ServicePath="FlowMgr.service" MustMatch="true" Width="250" CssClass="inputRequired"
                    ServiceMethod="GetProductionFlow" OnTextChanged="tbFlow_Changed" AutoPostBack="true" />
                <asp:RequiredFieldValidator ID="rfvFlow" runat="server" ErrorMessage="${MasterData.Order.OrderHead.Flow.Required}"
                    Display="Dynamic" ControlToValidate="tbFlow" ValidationGroup="vgSaveGroup" />
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
                        <asp:HiddenField ID="hfFgCode" runat="server" Value='<%# Bind("FinishGoods.Code") %>' />
                        <uc3:textbox ID="tbItemCode" runat="server" Visible="false" Width="250" DescField="Description"
                            ValueField="Code" ServicePath="FlowDetailMgr.service" ServiceMethod="GetAllFlowDetailItem"
                            CssClass="inputRequired" MustMatch="true" />
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
                            MinimumValue="0" MaximumValue="99999999999" ValidationGroup="vgSaveGroup" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.InspectDetail.DefectClassification}">
                    <ItemTemplate>
                        <cc1:CodeMstrDropDownList ID="tbDefectClassification" runat="server" Code="DefectClassification"
                            IncludeBlankOption="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="${MasterData.InspectDetail.DefectFactor}">
                    <ItemTemplate>
                        <cc1:CodeMstrDropDownList ID="tbDefectFactor" runat="server" Code="DefectFactor"
                            IncludeBlankOption="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnAdd" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Item.Code") %>'
                            Text="${Common.Button.New}" OnClick="lbtnAdd_Click" ValidationGroup="vgAdd" Visible="false">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
