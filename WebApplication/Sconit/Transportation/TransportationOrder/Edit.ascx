<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Transportation_TransportationOrder_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="InProcessLocationList.ascx" TagName="List" TagPrefix="uc2" %>
<fieldset>
    <asp:FormView ID="FV_Order" runat="server" DataSourceID="ODS_Order" DefaultMode="Edit"
        OnDataBound="FV_Order_DataBound" DataKeyNames="OrderNo">
        <EditItemTemplate>
            <table class="mtable">
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblOrderNo" runat="server" Text="${Transportation.TransportationOrder.OrderNo}:" />
                    </td>
                    <td class="td02">
                        <asp:Label ID="tbOrderNo" runat="server" Text='<%#Bind("OrderNo") %>' />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="lblRoute" runat="server" Text="${Transportation.TransportationOrder.Route}:" />
                    </td>
                    <td class="td02">
                        <asp:Label ID="tbRoute" runat="server" Text='<%#Bind("TransportationRoute.Code") %>' />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblShipFrom" runat="server" Text="${Transportation.TransportationOrder.ShipFrom}:" />
                    </td>
                    <td class="td02">
                        <asp:Label ID="tbShipFrom" runat="server" />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="lblShipTo" runat="server" Text="${Transportation.TransportationOrder.ShipTo}:" />
                    </td>
                    <td class="td02">
                        <asp:Label ID="tbShipTo" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="ttd01">
                        <asp:Literal ID="lblVehicle" runat="server" Text="${Transportation.TransportationOrder.Vehicle}:" />
                    </td>
                    <td class="ttd02">
                        <uc3:textbox ID="tbVehicle" runat="server" Width="250" DescField="Code" ValueField="Code"
                            ServiceMethod="GetAllVehicle" ServicePath="VehicleMgr.service" AutoPostBack="true"
                            MustMatch="false" OnTextChanged="tbVehicle_TextChanged" />
                        <cc1:ReadonlyTextBox ID="tbVehicle1" runat="server" CodeField="Vehicle" Visible="false" />
                    </td>
                    <td class="ttd01">
                        <asp:Literal ID="lblExpense" runat="server" Text="${Transportation.TransportationOrder.Expense}:" />
                    </td>
                    <td class="ttd02">
                        <uc3:textbox ID="tbExpense" runat="server" Width="250" DescField="Code" ValueField="Code"
                            AutoPostBack="true" MustMatch="true" ServiceMethod="GetAllExpense" ServicePath="ExpenseMgr.service"
                            OnTextChanged="tbExpense_TextChanged" />
                        <cc1:ReadonlyTextBox ID="tbExpense1" runat="server" CodeField="Expense.Code" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblCarrier" runat="server" Text="${Transportation.TransportationOrder.Carrier}:" />
                    </td>
                    <td class="td02">
                        <uc3:textbox ID="tbCarrier" runat="server" Width="250" DescField="Name" ValueField="Code"
                            CssClass="inputRequired" MustMatch="true" ServiceMethod="GetCarrier" ServicePath="CarrierMgr.service" />
                        <asp:TextBox ID="tbCarrier1" runat="server" Text='<%#Bind("Carrier.Code") %>' ReadOnly="true"
                            Visible="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCarrier" runat="server" ControlToValidate="tbCarrier"
                            Display="Dynamic" ErrorMessage="${Transportation.TransportationOrder.Carrier}${Common.String.Empty}"
                            ValidationGroup="vgSaveGroup" />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="lblCarrierBillAddress" runat="server" Text="${Transportation.TransportationOrder.CarrierBillAddress}:" />
                    </td>
                    <td class="td02">
                        <uc3:textbox ID="tbCarrierBillAddress" runat="server" Visible="true" DescField="Address"
                            ValueField="Code" ServicePath="AddressMgr.service" ServiceMethod="GetBillAddress"
                            CssClass="inputRequired" Width="250" ServiceParameter="string:#tbCarrier" />
                        <asp:TextBox ID="tbCarrierBillAddress1" runat="server" Text='<%#Bind("CarrierBillAddress.Code") %>'
                            ReadOnly="true" Visible="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCarrierBillAddress" runat="server" ControlToValidate="tbCarrierBillAddress"
                            Display="Dynamic" ErrorMessage="${Transportation.TransportationOrder.CarrierBillAddress}${Common.String.Empty}"
                            ValidationGroup="vgSaveGroup" />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblDriver" runat="server" Text="${Transportation.TransportationOrder.VehicleDriver}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbDriver" runat="server" Text='<%#Bind("VehicleDriver") %>'></asp:TextBox>
                    </td>
                    <td class="td01">
                        <asp:Literal ID="lblPallentCount" runat="server" Text="${Transportation.TransportationOrder.PallentCount}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbPallentCount" runat="server" Text='<%#Bind("PallentCount") %>'></asp:TextBox>
                        <asp:RangeValidator ID="rvPallentCount" ControlToValidate="tbPallentCount" runat="server"
                            Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="999999999"
                            MinimumValue="0" Type="Integer" ValidationGroup="vgSaveGroup" />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblPricingMethod" runat="server" Text="${Transportation.TransportationOrder.PricingMethod}:" />
                    </td>
                    <td class="td02">
                        <cc1:CodeMstrDropDownList ID="ddlPricingMethod" Code="PricingMethod" runat="server"
                            IncludeBlankOption="true" AutoPostBack="true" OnSelectedIndexChanged="ddlPricingMethod_SelectedIndexChanged" />
                        <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlPricingMethod"
                            Display="Dynamic" ErrorMessage="${Transportation.TransportationOrder.PricingMethod}${Common.String.Empty}"
                            ValidationGroup="vgSaveGroup" />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="lblVehicleType" runat="server" Text="${Transportation.TransportationOrder.VehicleType}:" />
                    </td>
                    <td class="td02">
                        <asp:DropDownList ID="ddlType" runat="server" DataTextField="Description" DataValueField="Value" />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblCreateDate" runat="server" Text="${Transportation.TransportationOrder.CreateDate}:" />
                    </td>
                    <td class="td02">
                        <cc1:ReadonlyTextBox ID="tbCreateDate" runat="server" CodeField="CreateDate" />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="lblCreateUser" runat="server" Text="${Transportation.TransportationOrder.CreateUser}:" />
                    </td>
                    <td class="td02">
                        <cc1:ReadonlyTextBox ID="tbCreateUser" runat="server" CodeField="CreateUser.Name" />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblStartDate" runat="server" Text="${Transportation.TransportationOrder.StartDate}:" />
                    </td>
                    <td class="td02">
                        <cc1:ReadonlyTextBox ID="tbStartDate" runat="server" CodeField="StartDate" />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="lblStartUser" runat="server" Text="${Transportation.TransportationOrder.StartUser}:" />
                    </td>
                    <td class="td02">
                        <cc1:ReadonlyTextBox ID="tbStartUser" runat="server" CodeField="StartUser.Name" />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblCompleteDate" runat="server" Text="${Transportation.TransportationOrder.CompleteDate}:" />
                    </td>
                    <td class="td02">
                        <cc1:ReadonlyTextBox ID="tbCompleteDate" runat="server" CodeField="CompleteDate" />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="lblCompleteUser" runat="server" Text="${Transportation.TransportationOrder.CompleteUser}:" />
                    </td>
                    <td class="td02">
                        <cc1:ReadonlyTextBox ID="tbCompleteUser" runat="server" CodeField="CompleteUser.Name" />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblCloseDate" runat="server" Text="${Transportation.TransportationOrder.CloseDate}:" />
                    </td>
                    <td class="td02">
                        <cc1:ReadonlyTextBox ID="tbCloseDate" runat="server" CodeField="CloseDate" />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="lblCloseUser" runat="server" Text="${Transportation.TransportationOrder.CloseUser}:" />
                    </td>
                    <td class="td02">
                        <cc1:ReadonlyTextBox ID="tbCloseUser" runat="server" CodeField="CloseUser.Name" />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblCancelDate" runat="server" Text="${Transportation.TransportationOrder.CancelDate}:" />
                    </td>
                    <td class="td02">
                        <cc1:ReadonlyTextBox ID="tbCancelDate" runat="server" CodeField="CancelDate" />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="lblCancelUser" runat="server" Text="${Transportation.TransportationOrder.CancelUser}:" />
                    </td>
                    <td class="td02">
                        <cc1:ReadonlyTextBox ID="tbCancelUser" runat="server" CodeField="CancelUser.Name" />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblRemark" runat="server" Text="${Transportation.TransportationOrder.Remark}:" />
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbRemark" runat="server" CodeField="Remark" Text='<%#Bind("Remark") %>' />
                    </td>

                    <td class="td01">
                    </td>
                    <td class="td02">
                        <asp:CheckBox ID="IsExcess" runat="server" Font-Size="9pt" Text="${Transportation.TransportationOrder.ReferencePallentCount}" />
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
                        <cc1:Button ID="btnSave" runat="server" Text="${Common.Button.Save}" OnClick="btnSave_Click"
                            CssClass="button2" ValidationGroup="vgSaveGroup" FunctionId="btnIPSave" />
                        <cc1:Button ID="btnStart" runat="server" Text="${Common.Button.Start}" OnClick="btnStart_Click"
                            CssClass="button2" ValidationGroup="vgSaveGroup" FunctionId="btnIPStart" />
                        <cc1:Button ID="btnCheck" runat="server" Text="检查" OnClick="btnCheck_Click" CssClass="button2"
                            ValidationGroup="vgSaveGroup" FunctionId="btnIPCheck" />
                        <cc1:Button ID="btnCancel" runat="server" Text="${Common.Button.Cancel}" OnClick="btnCancel_Click"
                            CssClass="button2" FunctionId="btnIPCancel" />
                        <cc1:Button ID="btnRestore" runat="server" Text="状态恢复" OnClick="btnRestore_Click"
                            CssClass="button2" FunctionId="btnIPRestore" />
                        <asp:Button ID="btnPrint" runat="server" Text="${Common.Button.Print}" OnClick="btnPrint_Click"
                            CssClass="button2" />
                        <asp:Button ID="btnComplete" runat="server" Text="${Common.Button.Complete}" OnClick="btnComplete_Click"
                            CssClass="button2" />
                        <cc1:Button ID="btnValuate" runat="server" Text="${Common.Button.Valuate}" OnClick="btnValuate_Click"
                            CssClass="button2" FunctionId="btnIPVal" />
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                            CssClass="button2" />
                    </td>
                </tr>
            </table>
        </EditItemTemplate>
    </asp:FormView>
</fieldset>
<asp:ObjectDataSource ID="ODS_Order" runat="server" TypeName="com.Sconit.Web.TransportationOrderMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.Transportation.TransportationOrder" SelectMethod="LoadTransportationOrder">
    <SelectParameters>
        <asp:Parameter Name="orderNo" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<uc2:List ID="ucList" runat="server" Visible="false" />
