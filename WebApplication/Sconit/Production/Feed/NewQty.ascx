<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewQty.ascx.cs" Inherits="Production_Feed_NewQty" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlProductLine" runat="server" Text="${MasterData.Production.Feed.ProductLine}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbProductLine" runat="server" DescField="Description" ValueField="Code"
                    ServicePath="FlowMgr.service" ServiceMethod="GetProductionFlow" Width="250" CssClass="inputRequired"
                    OnTextChanged="tbProductLine_TextChanged" AutoPostBack="true" />
                <asp:RequiredFieldValidator ID="rfvProductLine" runat="server" ControlToValidate="tbProductLine"
                    ErrorMessage="${MasterData.Production.Feed.ProductLine.Required}" Display="Dynamic"
                    ValidationGroup="vgSaveGroup" />
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
    <legend>${MasterData.Production.Feeded.Detail}</legend>
    <div class="GridView">
        <asp:GridView ID="GV_List_Feeded" runat="server" AllowSorting="True" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.Operation}">
                    <ItemTemplate>
                        <asp:Label ID="lblOperation" runat="server" Text='<%# Bind("Operation") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.ItemCode}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.ItemDescription}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("Item.Description") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.UomCode}">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Item.Uom.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.UnitCount}">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("Item.UnitCount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.Location}">
                    <ItemTemplate>
                        <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("LocationFrom.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.HuId}">
                    <ItemTemplate>
                        <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("HuId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.LotNo}">
                    <ItemTemplate>
                        <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LotNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.Qty}">
                    <ItemTemplate>
                        <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' Width="50" />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="${Common.GridView.Action}" >
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.Delete}" OnClick="lbtnDelete_Click" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
<fieldset>
    <legend>${MasterData.Production.Feed.Detail}</legend>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.Operation}">
                    <ItemTemplate>
                        <asp:Label ID="lblOperation" runat="server" Text='<%# Bind("Operation") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.ItemCode}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("RawMaterial.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.ItemDescription}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("RawMaterial.Description") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.UomCode}">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("RawMaterial.Uom.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.UnitCount}">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("RawMaterial.UnitCount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.Location}">
                    <ItemTemplate>
                        <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("Location.Code","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Production.Feed.Qty}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbQty" runat="server" onmouseup="if(!readOnly)select();" Text='<%# Bind("Qty","{0:0.########}") %>'
                            Width="50"></asp:TextBox>
                        <asp:RangeValidator ID="rvQty" runat="server" ControlToValidate="tbQty" ErrorMessage="${Common.Validator.Valid.Number}"
                            Display="Dynamic" Type="Double" MinimumValue="0" MaximumValue="100000" ValidationGroup="vgSaveGroup" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
