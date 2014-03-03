<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_FlowBinding_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<asp:FormView ID="FV_FlowBinding" runat="server" DataSourceID="ODS_FlowBinding" DefaultMode="Insert">
    <InsertItemTemplate>
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
                        <asp:HiddenField ID="hfBindingType" Value='<%# Bind("BindingType") %>' runat="server" />
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
                        <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="${Common.Button.Save}"
                            CssClass="button2" ValidationGroup="vgSaveGroup" />
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                            CssClass="button2" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </InsertItemTemplate>
</asp:FormView>
<asp:ObjectDataSource ID="ODS_FlowBinding" runat="server" TypeName="com.Sconit.Web.FlowBindingMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.FlowBinding" InsertMethod="CreateFlowBinding"
    OnInserted="ODS_FlowBinding_Inserted" OnInserting="ODS_FlowBinding_Inserting">
</asp:ObjectDataSource>
