<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Transportation_Vehicle_Edit" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Vehicle" runat="server" DataSourceID="ODS_Vehicle" DefaultMode="Edit"
        Width="100%" DataKeyNames="Code" OnDataBound="FV_Vehicle_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${Transportation.Vehicle.UpdateVehicle}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Transportation.Vehicle.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbCode" runat="server" Text='<%# Bind("Code") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="Literal1" runat="server" Text="${Transportation.Vehicle.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCarrier" runat="server" Text="${Transportation.Vehicle.Carrier}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbCarrier" runat="server" Width="250" DescField="Name" ValueField="Code"
                                CssClass="inputRequired" MustMatch="true" ServiceMethod="GetCarrier" ServicePath="CarrierMgr.service"/>
                            <asp:RequiredFieldValidator ID="rfvCarrier" runat="server" ErrorMessage="${Transportation.Vehicle.Carrier.Empty}"
                                Display="Dynamic" ControlToValidate="tbCarrier" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvCarrier" runat="server" ControlToValidate="tbCarrier" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblType" runat="server" Text="${Transportation.Vehicle.Type}:" />
                        </td>
                        <td class="td02">
                            <cc1:CodeMstrDropDownList ID="ddlType" Code="VehicleType" runat="server" IncludeBlankOption="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblDriver" runat="server" Text="${Transportation.Vehicle.Driver}:" />
                        </td>
                        <td class="td02">
                             <asp:TextBox ID="tbDriver" runat="server" Text='<%# Bind("Driver") %>' Width="250"></asp:TextBox>
                        </td>
                         <td class="td01">
                            <asp:Literal ID="lblMobilePhone" runat="server" Text="${Transportation.Vehicle.MobilePhone}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMobilePhone" runat="server" Text='<%# Bind("MobilePhone") %>' Width="250"></asp:TextBox>
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
                                <asp:Button ID="Button1" runat="server" CommandName="Update" Text="${Common.Button.Save}" CssClass="apply"
                                    ValidationGroup="vgSave" />
                                <asp:Button ID="Button2" runat="server" CommandName="Delete" Text="${Common.Button.Delete}" CssClass="delete"
                                    OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                                <asp:Button ID="Button3" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="back" />
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Vehicle" runat="server" TypeName="com.Sconit.Web.VehicleMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.Transportation.Vehicle" UpdateMethod="UpdateVehicle"
    OnUpdated="ODS_Vehicle_Updated" OnUpdating="ODS_Vehicle_Updating" DeleteMethod="DeleteVehicle"
    OnDeleted="ODS_Vehicle_Deleted" SelectMethod="LoadVehicle">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="code" Type="String" />
    </DeleteParameters>
</asp:ObjectDataSource>
