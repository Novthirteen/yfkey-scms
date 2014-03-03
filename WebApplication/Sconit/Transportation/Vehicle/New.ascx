<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Transportation_Vehicle_New" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Vehicle" runat="server" DataSourceID="ODS_Vehicle" DefaultMode="Insert"
        DataKeyNames="Code">
        <InsertItemTemplate>
            <fieldset>
                <legend>${Transportation.Vehicle.AddVehicle}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Transportation.Vehicle.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Code") %>' CssClass="inputRequired"
                                Width="250"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="${Transportation.Vehicle.Code.Empty}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvCode" runat="server" ControlToValidate="tbCode" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${Transportation.Vehicle.IsActive}:" />
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
<asp:ObjectDataSource ID="ODS_Vehicle" runat="server" TypeName="com.Sconit.Web.VehicleMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.Transportation.Vehicle" InsertMethod="CreateVehicle"
    OnInserted="ODS_Vehicle_Inserted" OnInserting="ODS_Vehicle_Inserting">
</asp:ObjectDataSource>
