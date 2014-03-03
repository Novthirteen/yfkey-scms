<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_Routing_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Routing" runat="server" DataSourceID="ODS_Routing" DefaultMode="Edit"
        Width="100%" DataKeyNames="Code" OnDataBound="FV_Routing_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.Routing.UpdateRouting}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Common.Business.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<% #Bind("Code") %>' ReadOnly="true" />
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
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
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
                                <asp:Button ID="Button1" runat="server" CommandName="Update" Text="${Common.Button.Save}"
                                    CssClass="apply" ValidationGroup="vgSave" />
                                <asp:Button ID="Button2" runat="server" CommandName="Delete" Text="${Common.Button.Delete}"
                                    CssClass="delete" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                                <asp:Button ID="Button3" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                                    CssClass="back" />
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Routing" runat="server" TypeName="com.Sconit.Web.RoutingMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Routing" UpdateMethod="UpdateRouting"
    DeleteMethod="DeleteRouting" SelectMethod="LoadRouting" OnUpdated="ODS_Routing_Updated"
    OnUpdating="ODS_Routing_Updating" OnDeleted="ODS_Routing_Deleted">
    <SelectParameters>
        <asp:Parameter Name="Code" Type="String" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="Code" Type="String" />
    </DeleteParameters>
</asp:ObjectDataSource>
