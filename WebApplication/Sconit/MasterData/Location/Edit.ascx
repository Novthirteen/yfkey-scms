<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_Location_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc2" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Location" runat="server" DataSourceID="ODS_Location" DefaultMode="Edit"
        Width="100%" DataKeyNames="Code" OnDataBound="FV_Location_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.Location.UpdateLocation}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Location.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbCode" runat="server" Text='<%# Bind("Code") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlRegion" runat="server" Text="${MasterData.Location.Region}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbRegion" runat="server" Text='<%# Eval("Region.Code") %>' ReadOnly="true" />
                            <asp:RequiredFieldValidator ID="rfvRegion" runat="server" ErrorMessage="${MasterData.Location.Region.Empty}"
                                Display="Dynamic" ControlToValidate="tbRegion" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlName" runat="server" Text="${MasterData.Location.Name}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbName" runat="server" Text='<%# Bind("Name") %>' CssClass="inputRequired"
                                Width="250" />
                            <asp:RequiredFieldValidator ID="rfvName" runat="server" ErrorMessage="${MasterData.Location.Name.Empty}"
                                Display="Dynamic" ControlToValidate="tbName" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlVolume" runat="server" Text="${MasterData.Location.Volume}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbVolume" runat="server" Text='<%# Bind("Volume","{0:0.########}") %>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revVolume" ControlToValidate="tbVolume" runat="server"
                                ValidationGroup="vgSave" ErrorMessage="${MasterData.Item.UC.Format}" ValidationExpression="^[0-9]+(.[0-9]{1,8})?$"
                                Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblType" runat="server" Text="${MasterData.Location.Type}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbType" runat="server" Text='<%#Bind("Type") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlAllowNegativeInventory" runat="server" Text="${MasterData.Location.AllowNegativeInventory}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="tbAllowNegativeInventory" runat="server" Checked='<%# Bind("AllowNegativeInventory") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${MasterData.Location.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="tbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlIsSettleConsignment" runat="server" Text="${MasterData.Location.IsSettleConsignment}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsSettleConsignment" runat="server" Checked='<%#Bind("IsSettleConsignment") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlEnableAdv" runat="server" Text="${MasterData.Location.EnableAdvWM}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbEnableAdv" runat="server" Checked='<%#Bind("EnableAdvWM") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblIsMRP" runat="server" Text="${MasterData.Location.IsMRP}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="tbIsMRP" runat="server" Checked='<%#Bind("IsMRP") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblIsAutoConfirm" runat="server" Text="${MasterData.Location.IsAutoConfirm}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsAutoConfirm" runat="server" Checked='<%#Bind("IsAutoConfirm") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="IsFG" runat="server" Text="是否成品库存" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsFG" runat="server" Checked='<%#Bind("IsFG") %>' />
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
                            <div class="buttons">
                                <asp:Button ID="btnSave" runat="server" CommandName="Update" Text="${Common.Button.Save}"
                                    CssClass="apply" ValidationGroup="vgSave" />
                                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}"
                                    CssClass="delete" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                                    CssClass="back" />
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Location" runat="server" TypeName="com.Sconit.Web.LocationMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Location" UpdateMethod="UpdateLocation"
    OnUpdated="ODS_Location_Updated" SelectMethod="LoadLocation" OnUpdating="ODS_Location_Updating"
    DeleteMethod="DeleteLocation" OnDeleted="ODS_Location_Deleted">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="Volume" Type="Decimal" ConvertEmptyStringToNull="true" />
    </UpdateParameters>
</asp:ObjectDataSource>
