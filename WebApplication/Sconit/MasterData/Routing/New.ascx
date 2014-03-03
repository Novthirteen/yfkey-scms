<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_Routing_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Routing" runat="server" DataSourceID="ODS_Routing" DefaultMode="Insert"
        Width="100%" DataKeyNames="Code">
        <InsertItemTemplate>
            <fieldset>
                <legend>${MasterData.Routing.AddRouting}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Common.Business.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<% #Bind("Code") %>' CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="${MasterData.Routing.Code.Empty}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvCode" runat="server" ControlToValidate="tbCode" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlDescription" runat="server" Text="${Common.Business.Description}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbDescription" runat="server" Text='<% #Bind("Description") %>'
                                CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="${MasterData.Routing.Description.Empty}"
                                Display="Dynamic" ControlToValidate="tbDescription" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlRegion" runat="server" Text="${MasterData.Region.Code}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbRegion" runat="server" Width="250" DescField="Name" ValueField="Code"
                                ServicePath="RegionMgr.service" ServiceMethod="GetRegion" MustMatch="true" />
                            <asp:CustomValidator ID="cvRegion" runat="server" ControlToValidate="tbRegion" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" Text="*" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlRoutingType" runat="server" Text="${MasterData.Routing.Type}:" />
                        </td>
                        <td>
                            <cc1:CodeMstrDropDownList ID="ddlRoutingType" Code="RoutingType" runat="server">
                            </cc1:CodeMstrDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${Common.Business.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        </td>
                        <td>
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
<asp:ObjectDataSource ID="ODS_Routing" runat="server" TypeName="com.Sconit.Web.RoutingMgrProxy"
    InsertMethod="CreateRouting" DataObjectTypeName="com.Sconit.Entity.MasterData.Routing"
    OnInserted="ODS_Routing_Inserted" OnInserting="ODS_Routing_Inserting"></asp:ObjectDataSource>
