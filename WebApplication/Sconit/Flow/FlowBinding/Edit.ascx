<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_FlowBinding_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<asp:FormView ID="FV_FlowBinding" runat="server" DataSourceID="ODS_FlowBinding" DefaultMode="Edit"
    DataKeyNames="Id" OnDataBound="FV_FlowBinding_DataBound">
    <EditItemTemplate>
        <fieldset>
            <table class="mtable">
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblMasterFlowType" runat="server" Text="${MasterData.Flow.Binding.MasterFlow.Type}:" />
                    </td>
                    <td class="td02">
                        <asp:Label ID="lblMasterFlowTypeValue" runat="server" />
                    </td>
                    <td class="td01">
                    </td>
                    <td class="td02">
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblMasterFlow" runat="server" Text="${MasterData.Flow.Binding.MasterFlow.Code}:" />
                    </td>
                    <td class="td02">
                        <asp:Label ID="lblMasterFlowValue" runat="server" />
                    </td>
                    <td class="td01">
                        <asp:Literal ID="lblSlaveFlow" runat="server" Text="${MasterData.Flow.Binding.SlaveFlow.Code}:" />
                    </td>
                    <td class="td02">
                        <uc3:textbox ID="tbSlaveFlow" runat="server" Visible="true" Width="250" DescField="Description"
                            ValueField="Code" ServicePath="FlowMgr.service" CssClass="inputRequired"  ServiceMethod="GetAllFlow"/>
                        <asp:RequiredFieldValidator ID="rfvSlaveFlow" runat="server" ErrorMessage="${MasterData.Flow.Binding.SlaveFlow.Required}"
                            Display="Dynamic" ControlToValidate="tbSlaveFlow" ValidationGroup="vgSaveGroup" />
                        <asp:CustomValidator ID="cvSlaveFlowCheck" runat="server" ControlToValidate="tbSlaveFlow"
                            ErrorMessage="${MasterData.Flow.Binding.SlaveFlow.Exists}" Display="Dynamic"
                            ValidationGroup="vgSaveGroup" OnServerValidate="CheckSlaveFlow" />
                    </td>
                </tr>
                <tr>
                    <td class="td01">
                        <asp:Literal ID="lblBindingType" runat="server" Text="${MasterData.Flow.Binding.BindingType}:" />
                    </td>
                    <td class="td02">
                        <cc1:CodeMstrDropDownList ID="ddlBindingType" Code="BindingType" runat="server">
                        </cc1:CodeMstrDropDownList>
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
                        <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="${Common.Button.Save}"
                            CssClass="button2" ValidationGroup="vgSaveGroup" />
                        <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}"
                            CssClass="button2" />
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                            CssClass="button2" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </EditItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ODS_FlowBinding" runat="server" TypeName="com.Sconit.Web.FlowBindingMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.FlowBinding" OnUpdating="ODS_FlowBinding_Updating"
    OnUpdated="ODS_FlowBinding_Updated" UpdateMethod="UpdateFlowBinding" DeleteMethod="DeleteFlowBinding"
    OnDeleted="ODS_FlowBinding_Deleted" SelectMethod="LoadFlowBinding">
    <SelectParameters>
        <asp:Parameter Name="id" Type="Int32" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="id" Type="Int32" />
    </DeleteParameters>
</asp:ObjectDataSource>
