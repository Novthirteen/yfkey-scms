<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_Item_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/FileUpload.ascx" TagName="FileUpload" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc2" %>
<div id="divFV">
    <asp:FormView ID="FV_Item" runat="server" DataSourceID="ODS_Item" DefaultMode="Insert"
        Width="100%" DataKeyNames="Code">
        <InsertItemTemplate>
            <fieldset>
                <legend>${MasterData.Item.NewItem}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Item.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Code") %>' CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rtvCode" runat="server" ErrorMessage="${MasterData.Item.Code.Empty}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvInsert" runat="server" ControlToValidate="tbCode" ErrorMessage="${MasterData.Item.CodeExist1}"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="checkItemExists" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlItemImage" runat="server" Text="${MasterData.Item.Image}:" />
                        </td>
                        <td class="td02">
                            <asp:FileUpload ID="fileUpload" ContentEditable="false" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlDesc1" runat="server" Text="${MasterData.Item.Description}1:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbDesc1" runat="server" Text='<%# Bind("Desc1") %>' CssClass="inputRequired"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDesc1" runat="server" ErrorMessage="${MasterData.Item.Desc1.Empty}"
                                Display="Dynamic" ControlToValidate="tbDesc1" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlDesc2" runat="server" Text="${MasterData.Item.Description}2:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbDesc2" runat="server" Text='<%# Bind("Desc2") %>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlUom" runat="server" Text="${MasterData.Item.Uom}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbUom" runat="server" Visible="true" DescField="Description" ValueField="Code"
                                ServicePath="UomMgr.service" ServiceMethod="GetAllUom" MustMatch="true" CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvUom" runat="server" ErrorMessage="${MasterData.Item.Uom.Empty}"
                                Display="Dynamic" ControlToValidate="tbUom" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlType" runat="server" Text="${MasterData.Item.Type}:" />
                        </td>
                        <td class="td02">
                            <asp:DropDownList ID="ddlType" runat="server" DataTextField="Description" DataValueField="Value"
                                Text='<%# Bind("Type") %>' DataSourceID="ODS_ddlType">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlCount" runat="server" Text="${MasterData.Item.Uc}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCount" runat="server" Text='<%# Bind("UnitCount") %>' CssClass="inputRequired"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUC" runat="server" ErrorMessage="${MasterData.Item.UC.Empty}"
                                Display="Dynamic" ControlToValidate="tbCount" ValidationGroup="vgSave" />
                            <asp:RegularExpressionValidator ID="revCount" ControlToValidate="tbCount" runat="server"
                                ValidationGroup="vgSave" ErrorMessage="${MasterData.Item.UC.Format}" ValidationExpression="^[0-9]+(.[0-9]{1,8})?$"
                                Display="Dynamic" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlLocation" runat="server" Text="${MasterData.Item.Location}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbLocation" runat="server" Visible="true" DescField="Name" ValueField="Code"
                                ServicePath="LocationMgr.service" ServiceMethod="GetLocation" MustMatch="true"
                                Width="250" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlBom" runat="server" Text="${MasterData.Item.Bom}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbBom" runat="server" Visible="true" DescField="Description" ValueField="Code"
                                ServicePath="BomMgr.service" ServiceMethod="GetAllBom" MustMatch="true" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlRouting" runat="server" Text="${MasterData.Item.Routing}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbRouting" runat="server" Visible="true" DescField="Description"
                                ValueField="Code" ServicePath="RoutingMgr.service" ServiceMethod="GetAllRouting"
                                MustMatch="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblItemCategory" runat="server" Text="${MasterData.Item.Category}:" />
                        </td>
                        <td class="td02">
                            <cc2:CodeMstrDropDownList ID="ddlItemCategory" Code="ItemCategory" runat="server"
                                IncludeBlankOption="true">
                            </cc2:CodeMstrDropDownList>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${MasterData.Item.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="tbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlMemo" runat="server" Text="${MasterData.Item.Memo}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMemo" runat="server" Text='<%# Bind("Memo") %>'></asp:TextBox>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlIsMes" runat="server" Text="${MasterData.Item.IsMes}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsMes" runat="server" Checked='<%#Bind("IsMes") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="Literal1" runat="server" Text="安全库存" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbSafeStock" runat="server" Text='<%# Bind("SafeStock") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvSafeStock" ControlToValidate="tbSafeStock" runat="server"
                                Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="1000000"
                                MinimumValue="0" Type="Double" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlMaxStock" runat="server" Text="最大库存" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMaxStock" runat="server" Text='<%# Bind("MaxStock") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvMaxStock" ControlToValidate="tbMaxStock" runat="server"
                                Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="1000000"
                                MinimumValue="0" Type="Double" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlLeadTime" runat="server" Text="${MasterData.Item.LeadTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbLeadTime" runat="server" Text='<%# Bind("LeadTime") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvtbLeadTime" ControlToValidate="tbLeadTime" runat="server"
                                Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="1000000"
                                MinimumValue="0" Type="Double" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlIsMRP" runat="server" Text="是否参与MRP运算" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsMRP" runat="server" Checked='<%#Bind("IsMRP") %>' />
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
<asp:ObjectDataSource ID="ODS_Item" runat="server" TypeName="com.Sconit.Web.ItemMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Item" InsertMethod="CreateItem"
    OnInserted="ODS_Item_Inserted" OnInserting="ODS_Item_Inserting"></asp:ObjectDataSource>
<asp:ObjectDataSource ID="ODS_ddlType" runat="server" TypeName="com.Sconit.Web.CodeMasterMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.CodeMaster" SelectMethod="GetCachedCodeMaster">
    <SelectParameters>
        <asp:QueryStringParameter Name="code" DefaultValue="ItemType" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ODS_ddlUom" runat="server" TypeName="com.Sconit.Web.UomMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Uom" SelectMethod="GetAllUom">
</asp:ObjectDataSource>
