<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_PriceList_PriceListDetail_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div id="floatdiv">
    <asp:FormView ID="FV_PriceListDetail" runat="server" DataSourceID="ODS_PriceListDetail"
        DefaultMode="Edit" Width="100%" DataKeyNames="Id" OnDataBound="FV_PriceListDetail_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.PriceListDetail.UpdatePriceListDetail}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblPriceList" runat="server" Text="${MasterData.PriceList.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbPriceList" runat="server" ReadOnly="true" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblIsProvEst" runat="server" Text="${MasterData.PriceListDetail.IsProvEst}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsProvEst" runat="server" Checked='<%#Bind("IsProvisionalEstimate") %>' />
                            <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Id") %>' ReadOnly="true"
                                Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblStartDate" runat="server" Text="${Common.Business.StartDate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbStartDate" runat="server" Text='<%# Bind("StartDate") %>' ReadOnly="true" />
                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="${MasterData.PriceListDetail.StartDate.Empty}"
                                Display="Dynamic" ControlToValidate="tbStartDate" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvStartDate" runat="server" ControlToValidate="tbStartDate"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblEndDate" runat="server" Text="${Common.Business.EndDate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbEndDate" runat="server" Text='<%# Bind("EndDate") %>' />
                            <cc1:CalendarExtender ID="ceEndDate" TargetControlID="tbEndDate" Format="yyyy-MM-dd"
                                runat="server">
                            </cc1:CalendarExtender>
                            <asp:CustomValidator ID="cvEndDate" runat="server" ControlToValidate="tbEndDate"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblItem" runat="server" Text="${MasterData.Item.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbItem" runat="server" ReadOnly="true" />
                            <asp:CustomValidator ID="cvItem" runat="server" ControlToValidate="tbItem" Display="Dynamic"
                                ErrorMessage="*" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCurrency" runat="server" Text="${MasterData.Currency.Code}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbCurrency" runat="server" DescField="Name" ValueField="Code" ServicePath="CurrencyMgr.service"
                                ServiceMethod="GetAllCurrency" MustMatch="true" CssClass="inputRequired" />
                            <asp:CustomValidator ID="cvCurrency" runat="server" ControlToValidate="tbCurrency"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                            <asp:RequiredFieldValidator ID="rfvCurrency" runat="server" ErrorMessage="${MasterData.Currency.Code.Empty}"
                                Display="Dynamic" ControlToValidate="tbCurrency" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblUnitPrice" runat="server" Text="${MasterData.PriceListDetail.UnitPrice}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbUnitPrice" runat="server" Text='<%#Bind("UnitPrice","{0:0.########}") %>'
                                CssClass="inputRequired" />
                            <asp:CustomValidator ID="cvUnitPrice" runat="server" ControlToValidate="tbUnitPrice"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                            <asp:RequiredFieldValidator ID="rfvUnitPrice" runat="server" ErrorMessage="${MasterData.PriceListDetail.UnitPrice.Empty}"
                                Display="Dynamic" ControlToValidate="tbUnitPrice" ValidationGroup="vgSave" />
                            <asp:RegularExpressionValidator ID="revCount" ControlToValidate="tbUnitPrice" runat="server"
                                ValidationGroup="vgSave" ErrorMessage="${MasterData.Item.UC.Format}" ValidationExpression="^[0-9]+(.[0-9]{1,8})?$"
                                Display="Dynamic" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblUom" runat="server" Text="${MasterData.Uom.Code}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbUom" runat="server" Visible="true" Width="250" DescField="Description"
                                ServiceParameter="string:#tbItem" ValueField="Code" ServicePath="UomMgr.service"
                                ServiceMethod="GetItemUom" CssClass="inputRequired" />
                            <asp:CustomValidator ID="cvUom" runat="server" ControlToValidate="tbUom" Display="Dynamic"
                                ErrorMessage="*" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblTaxCode" runat="server" Text="${MasterData.PriceListDetail.TaxCode}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbTaxCode" runat="server" Text='<%#Bind("TaxCode") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblIsIncludeTax" runat="server" Text="${MasterData.PriceListDetail.IsIncludeTax}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsIncludeTax" runat="server" Checked='<%#Bind("IsIncludeTax") %>' />
                        </td>
                    </tr>
                </table>
                <div class="tablefooter">
                    <div class="buttons">
                        <asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="${Common.Button.Save}"
                            CssClass="apply" ValidationGroup="vgSave" />
                        <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}"
                            CssClass="delete" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="back"/>
                    </div>
                </div>
            </fieldset>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_PriceListDetail" runat="server" TypeName="com.Sconit.Web.PriceListDetailMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.PriceListDetail" UpdateMethod="UpdatePriceListDetail"
    OnUpdated="ODS_PriceListDetail_Updated" OnUpdating="ODS_PriceListDetail_Updating"
    DeleteMethod="DeletePriceListDetail" OnDeleted="ODS_PriceListDetail_Deleted"
    SelectMethod="LoadPriceListDetail">
    <SelectParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="EndDate" Type="DateTime" ConvertEmptyStringToNull="true" />
    </UpdateParameters>
</asp:ObjectDataSource>
