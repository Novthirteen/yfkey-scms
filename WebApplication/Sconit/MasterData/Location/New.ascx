<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_Location_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc2" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Location" runat="server" DataSourceID="ODS_Location" DefaultMode="Insert"
        Width="100%" DataKeyNames="Code" OnDataBinding="FV_Location_OnDataBinding">
        <InsertItemTemplate>
            <fieldset>
                <legend>${MasterData.Location.NewLocation}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Location.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Code") %>' CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rtvCode" runat="server" ErrorMessage="${MasterData.Location.Code.Empty}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvInsert" runat="server" ControlToValidate="tbCode" ErrorMessage="${Common.Code.Exist}"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="checkLocationExists" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlRegion" runat="server" Text="${MasterData.Location.Region}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbRegion" runat="server" DescField="Name" ValueField="Code" Width="200"
                                ServicePath="RegionMgr.service" ServiceMethod="GetRegion" MustMatch="true" CssClass="inputRequired" />
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
                            <asp:TextBox ID="tbVolume" runat="server" Text='<%# Bind("Volume") %>'></asp:TextBox>
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
                        </td>
                        <td class="td02">
                            <div class="buttons">
                                <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="${Common.Button.Save}"
                                    CssClass="apply" ValidationGroup="vgSave" />
                                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                                    CssClass="back" />
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </InsertItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Location" runat="server" TypeName="com.Sconit.Web.LocationMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Location" InsertMethod="CreateLocation"
    OnInserted="ODS_Location_Inserted" OnInserting="ODS_Location_Inserting">
    <InsertParameters>
        <asp:Parameter Name="Volume" Type="Decimal" ConvertEmptyStringToNull="true" />
    </InsertParameters>
</asp:ObjectDataSource>
