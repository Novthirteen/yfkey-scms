<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Visualization_GoodsTraceability_Traceability_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<fieldset>
    <legend>${MasterData.Flow.Basic.Info}</legend>
    <asp:FormView ID="FV_Edit" runat="server" DataSourceID="ODS_FV" DefaultMode="ReadOnly"
        DataKeyNames="HuId" OnDataBound="FV_DataBound">
        <ItemTemplate>
            <table class="mtable">
                <tr>
                    <td class="td01">
                        <asp:Literal ID="ltlHuId" runat="server" Text="${Common.Business.HuId}:" />
                    </td>
                    <td class="td02">
                        <asp:Label ID="tbHuId" runat="server" Text='<%# Bind("HuId") %>' />
                    </td>
                    <td colspan="2" />
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="ltlItemCode" runat="server" Text="${Common.Business.ItemCode}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbItemCode" runat="server" ReadOnly="true" Text='<%# Bind("Item.Code") %>' />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="ltlItemDescription" runat="server" Text="${Common.Business.ItemDescription}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbItemDescription" runat="server" ReadOnly="true" Text='<%# Bind("Item.Description") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="ltlUom" runat="server" Text="${Common.Business.Uom}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbUom" runat="server" ReadOnly="true" Text='<%# Bind("Uom.Code") %>' />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="ltlUnitCount" runat="server" Text="${Common.Business.UnitCount}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbUnitCount" runat="server" ReadOnly="true" Text='<%# Bind("UnitCount","{0:0.########}") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="ltlManufactureDate" runat="server" Text="${Common.Business.ManufactureDate}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="lblManufactureDate" runat="server" ReadOnly="true" Text='<%# Eval("ManufactureDate","{0:yyyy-MM-dd}")%>' />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="ltlManufactureParty" runat="server" Text="${Common.Business.ManufactureParty}:" />
                    </td>
                    <td class="td02">
                        <sc1:ReadonlyTextBox ID="tbManufactureParty" runat="server" CodeField="ManufactureParty.Code"
                            DescField="ManufactureParty.Name" />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="ltlLotNo" runat="server" Text="${Common.Business.LotNo}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbLotNo" runat="server" ReadOnly="true" Text='<%# Bind("LotNo") %>' />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="ltlBin" runat="server" Text="${MasterData.Location.Bin}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbBin" runat="server" ReadOnly="true" />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="ltlLocation" runat="server" Text="${Common.Business.Location}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox  ID="tbLocation" runat="server" ReadOnly="true" Text='<%# Bind("Location") %>' />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="ltlQty" runat="server" Text="${Reports.CurrentInv}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbQty" runat="server" ReadOnly="true" Text='<%# Bind("Qty","{0:0.########}") %>' />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>
    <asp:ObjectDataSource ID="ODS_FV" runat="server" TypeName="com.Sconit.Web.HuMgrProxy"
        DataObjectTypeName="com.Sconit.Entity.MasterData.Hu" SelectMethod="LoadHu">
        <SelectParameters>
            <asp:Parameter Name="code" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</fieldset>
