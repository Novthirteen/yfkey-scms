<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_Item_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Item" runat="server" DataSourceID="ODS_Item" DefaultMode="Edit"
        Width="100%" DataKeyNames="Code" OnDataBound="FV_Item_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.Item.UpdateItem}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td02" rowspan="8" style="width: 150px">
                            <asp:Image ID="imgUpload" ImageUrl='<%# Eval("ImageUrl") %>' runat="server" Width="150px" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Item.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbCode" runat="server" Text='<%# Bind("Code") %>' />
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
                            <asp:TextBox ID="tbDesc1" runat="server" Text='<%# Bind("Desc1") %>' CssClass="inputRequired"
                                Width="200" />
                            <asp:RequiredFieldValidator ID="rfvDesc1" runat="server" ErrorMessage="${MasterData.Item.Desc1.Empty}"
                                Display="Dynamic" ControlToValidate="tbDesc1" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlDesc2" runat="server" Text="${MasterData.Item.Description}2:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbDesc2" runat="server" Text='<%# Bind("Desc2") %>' Width="200" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlUom" runat="server" Text="${MasterData.Item.Uom}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbUom" runat="server" ReadOnly="true" />
                            <%--                            <uc3:textbox ID="tbUom" runat="server" Visible="true" DescField="Description" CssClass="inputRequired"
                                ValueField="Code" ServicePath="UomMgr.service" ServiceMethod="GetAllUom" MustMatch="true" />
                            <asp:RequiredFieldValidator ID="rfvtbUom" runat="server" ErrorMessage="${MasterData.Item.Uom.Empty}"
                                Display="Dynamic" ControlToValidate="tbUom" ValidationGroup="vgSave" />--%>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlType" runat="server" Text="${MasterData.Item.Type}:" />
                        </td>
                        <td class="td02">
                            <cc1:CodeMstrDropDownList ID="ddlType" Code="ItemType" runat="server" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlCount" runat="server" Text="${MasterData.Item.Uc}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCount" runat="server" Text='<%# Bind("UnitCount","{0:0.########}") %>'
                                CssClass="inputRequired" />
                            <asp:RegularExpressionValidator ID="revCount" ControlToValidate="tbCount" runat="server"
                                ValidationGroup="vgSave" ErrorMessage="${MasterData.Item.UC.Format}" ValidationExpression="^[0-9]+(.[0-9]{1,8})?$"
                                Display="Dynamic" />
                            <asp:RequiredFieldValidator ID="rfvUC" runat="server" ErrorMessage="${MasterData.Item.UC.Empty}"
                                Display="Dynamic" ControlToValidate="tbCount" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlLocation" runat="server" Text="${MasterData.Item.Location}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbLocation" runat="server" Visible="true" DescField="Name" Width="250"
                                ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetAllLocation"
                                MustMatch="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlBom" runat="server" Text="${MasterData.Item.Bom}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbBom" runat="server" Visible="true" DescField="Description" ValueField="Code"
                                ServicePath="BomMgr.service" ServiceMethod="GetAllBom" MustMatch="true" Width="250" />
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
                            <cc1:CodeMstrDropDownList ID="ddlItemCategory" Code="ItemCategory" runat="server"
                                IncludeBlankOption="true">
                            </cc1:CodeMstrDropDownList>
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
                    <td class="td01" style="width: 150px">
                            &nbsp;
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblPlant" runat="server" Text="${MasterData.Item.Plant}:" />
                        </td>
                        <td class="td02">
                            <cc1:CodeMstrDropDownList ID="ddlPlantType" Code="PlantType" runat="server" IncludeBlankOption="true"
                                Enabled="false">
                            </cc1:CodeMstrDropDownList>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlIsMes" runat="server" Text="${MasterData.Item.IsMes}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsMes" runat="server" Checked='<%#Bind("IsMes") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01" style="width: 150px; text-align: center">
                            <asp:Literal ID="ltlDeleteImage" runat="server" Text="${MasterData.Item.DeleteImage}:" />
                            <asp:CheckBox ID="cbDeleteImage" runat="server" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlMemo" runat="server" Text="${MasterData.Item.Memo}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMemo" runat="server" Text='<%# Bind("Memo") %>'></asp:TextBox>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlMinLotSize" runat="server" Text="${MasterData.Item.MinLotSize}:" />
                        </td>
                        <td class="td02">
                        <asp:TextBox ID="tbMinLotSize" runat="server" Text='<%# Bind("MinLotSize") %>'></asp:TextBox>
                            <asp:RangeValidator ID="rvtbMinLotSize" ControlToValidate="tbMinLotSize" runat="server"
                                Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="1000000"
                                MinimumValue="0" Type="Double" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01" style="width: 150px">
                            &nbsp;
                        </td>
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
<asp:ObjectDataSource ID="ODS_Item" runat="server" TypeName="com.Sconit.Web.ItemMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Item" UpdateMethod="UpdateItem"
    OnUpdated="ODS_Item_Updated" SelectMethod="LoadItem" OnUpdating="ODS_Item_Updating"
    DeleteMethod="DeleteItem" OnDeleted="ODS_Item_Deleted" OnDeleting="ODS_Item_Deleting">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
