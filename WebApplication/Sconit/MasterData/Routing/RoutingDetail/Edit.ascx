<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_Routing_RoutingDetail_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div id="floatdiv">
    <asp:FormView ID="FV_RoutingDetail" runat="server" DataSourceID="ODS_RoutingDetail"
        DefaultMode="Edit" Width="100%" DataKeyNames="Id" OnDataBound="FV_RoutingDetail_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.RoutingDetail.UpdateRoutingDetail}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblRouting" runat="server" Text="${MasterData.Routing.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbRouting" runat="server" ReadOnly="true" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblActivity" runat="server" Text="${MasterData.RoutingDetail.Activity}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbActivity" runat="server" Text='<%#Bind("Activity") %>' CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvActivity" runat="server" ErrorMessage="${MasterData.RoutingDetail.Activity.Empty}"
                                Display="Dynamic" ControlToValidate="tbActivity" ValidationGroup="vgSave" />
                            <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Id") %>' ReadOnly="true"
                                Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlOperation" runat="server" Text="${MasterData.RoutingDetail.Operation}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbOperation" runat="server" Text='<%# Bind("Operation") %>' ReadOnly="true" />
                            <asp:RegularExpressionValidator ID="revOperation" ControlToValidate="tbOperation"
                                runat="server" ValidationGroup="vgSave" ErrorMessage="${Common.Validator.Int.Error}"
                                ValidationExpression="^[1-9][0-9]*$" Display="Dynamic" />
                            <asp:RequiredFieldValidator ID="rfvOperation" runat="server" ErrorMessage="${MasterData.RoutingDetail.Operation.Empty}"
                                Display="Dynamic" ControlToValidate="tbOperation" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvOperation" runat="server" ControlToValidate="tbOperation"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlReference" runat="server" Text="${MasterData.RoutingDetail.Reference}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbReference" runat="server" Text='<%# Bind("Reference") %>' ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblStartDate" runat="server" Text="${Common.Business.StartDate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbStartDate" runat="server" Text='<%# Bind("StartDate") %>' ReadOnly="true" />
                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="${MasterData.RoutingDetail.StartDate.Empty}"
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
                                runat="server" />
                            <asp:CustomValidator ID="cvEndDate" runat="server" ControlToValidate="tbEndDate"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlParty" runat="server" Text="${Common.Business.Party}:" />
                        </td>
                        <td class="td02">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <uc3:textbox ID="tbParty" runat="server" DescField="Name" ValueField="Code" ServicePath="PartyMgr.service"
                                            ServiceMethod="GetAllParty" MustMatch="true" CssClass="inputRequired" Width="250"
                                            InputWidth="100" />
                                    </td>
                                    <td>
                                        <asp:Literal ID="ltlWorkCenter" runat="server" Text="${Common.Business.WorkCenter}:" />
                                    </td>
                                    <td>
                                        <uc3:textbox ID="tbWorkCenter" runat="server" DescField="Name" ValueField="Code"
                                            ServicePath="WorkCenterMgr.service" ServiceMethod="GetWorkCenter" MustMatch="true"
                                            CssClass="inputRequired" Width="250" InputWidth="100" ServiceParameter="string:#tbParty" />
                                        <asp:CustomValidator ID="cvWorkCenter" runat="server" ControlToValidate="tbWorkCenter"
                                            Display="Dynamic" ErrorMessage="*" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                                        <asp:RequiredFieldValidator ID="rfvWorkCenter" runat="server" ErrorMessage="${MasterData.RoutingDetail.WorkCenter.Empty}"
                                            Display="Dynamic" ControlToValidate="tbWorkCenter" ValidationGroup="vgSave" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblLocation" runat="server" Text="${MasterData.Location.Code}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbLocation" runat="server" DescField="Name" ValueField="Code" ServicePath="LocationMgr.service"
                                ServiceMethod="GetAllLocation" MustMatch="true" Width="250" />
                            <asp:CustomValidator ID="cvLocation" runat="server" ControlToValidate="tbLocation"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblSetupTime" runat="server" Text="${MasterData.RoutingDetail.SetupTime}(min):" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbSetupTime" runat="server" Text='<%#Bind("SetupTime","{0:0.########}") %>'
                                CssClass="inputRequired" />
                            <asp:CustomValidator ID="cvSetupTime" runat="server" ControlToValidate="tbSetupTime"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                            <asp:RequiredFieldValidator ID="rfvSetupTime" runat="server" ErrorMessage="${MasterData.RoutingDetail.SetupTime.Empty}"
                                Display="Dynamic" ControlToValidate="tbSetupTime" ValidationGroup="vgSave" />
                            <asp:RegularExpressionValidator ID="revSetupTime" ControlToValidate="tbSetupTime"
                                runat="server" ValidationGroup="vgSave" ErrorMessage="${Common.Validator.Valid.Number}"
                                ValidationExpression="^[0-9]+(.[0-9]{1,8})?$" Display="Dynamic" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlRunTime" runat="server" Text="${MasterData.RoutingDetail.RunTime}(min):" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbRunTime" runat="server" Text='<%#Bind("RunTime","{0:0.########}") %>'
                                CssClass="inputRequired" />
                            <asp:CustomValidator ID="cvRunTime" runat="server" ControlToValidate="tbRunTime"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                            <asp:RequiredFieldValidator ID="rfvRunTime" runat="server" ErrorMessage="${MasterData.RoutingDetail.RunTime.Empty}"
                                Display="Dynamic" ControlToValidate="tbRunTime" ValidationGroup="vgSave" />
                            <asp:RegularExpressionValidator ID="revRunTime" ControlToValidate="tbRunTime" runat="server"
                                ValidationGroup="vgSave" ErrorMessage="${Common.Validator.Valid.Number}" ValidationExpression="^[0-9]+(.[0-9]{1,8})?$"
                                Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlMoveTime" runat="server" Text="${MasterData.RoutingDetail.MoveTime}(min):" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbMoveTime" runat="server" Text='<%#Bind("MoveTime","{0:0.########}") %>'
                                CssClass="inputRequired" />
                            <asp:CustomValidator ID="cvMoveTime" runat="server" ControlToValidate="tbMoveTime"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                            <asp:RequiredFieldValidator ID="rfvMoveTime" runat="server" ErrorMessage="${MasterData.RoutingDetail.MoveTime.Empty}"
                                Display="Dynamic" ControlToValidate="tbMoveTime" ValidationGroup="vgSave" />
                            <asp:RegularExpressionValidator ID="revMoveTime" ControlToValidate="tbMoveTime" runat="server"
                                ValidationGroup="vgSave" ErrorMessage="${Common.Validator.Valid.Number}" ValidationExpression="^[0-9]+(.[0-9]{1,8})?$"
                                Display="Dynamic" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblTactTime" runat="server" Text="${MasterData.RoutingDetail.TactTime}(min):" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbTactTime" runat="server" Text='<%#Bind("TactTime","{0:0.########}") %>'
                                CssClass="inputRequired" />
                            <asp:CustomValidator ID="cvTactTime" runat="server" ControlToValidate="tbTactTime"
                                ErrorMessage="*" Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                            <asp:RequiredFieldValidator ID="rfvTactTime" runat="server" ErrorMessage="${MasterData.RoutingDetail.TactTime.Empty}"
                                Display="Dynamic" ControlToValidate="tbTactTime" ValidationGroup="vgSave" />
                            <asp:RegularExpressionValidator ID="revTactTime" ControlToValidate="tbTactTime" runat="server"
                                ValidationGroup="vgSave" ErrorMessage="${Common.Validator.Valid.Number}" ValidationExpression="^[0-9]+(.[0-9]{1,8})?$"
                                Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblTagNo" runat="server" Text="${MasterData.RoutingDetail.TagNo}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbTagNo" runat="server" Text='<%#Bind("TagNo") %>' />
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
                                <asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="${Common.Button.Save}"
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
<asp:ObjectDataSource ID="ODS_RoutingDetail" runat="server" TypeName="com.Sconit.Web.RoutingDetailMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.RoutingDetail" UpdateMethod="UpdateRoutingDetail"
    OnUpdated="ODS_RoutingDetail_Updated" OnUpdating="ODS_RoutingDetail_Updating"
    DeleteMethod="DeleteRoutingDetail" OnDeleted="ODS_RoutingDetail_Deleted" SelectMethod="LoadRoutingDetail">
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
