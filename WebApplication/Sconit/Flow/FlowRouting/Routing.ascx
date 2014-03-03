<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Routing.ascx.cs" Inherits="MasterData_Flow_Routing" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Routing" runat="server" DataSourceID="ODS_Routing" DefaultMode="Edit"
        DataKeyNames="Code" OnDataBound="FV_Routing_DataBound">
        <EditItemTemplate>
            
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblRouting" runat="server" Text="${MasterData.Flow.Routing.Code}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbRouting" runat="server" Visible="true" Width="250" DescField="Description"
                                ValueField="Code" ServicePath="RoutingMgr.service" ServiceMethod="GetRouting"/>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblDescription" runat="server" Text="${MasterData.Flow.Routing.Description}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lDescription" runat="server" />
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
                             <asp:Button ID="btnEdit" runat="server" CommandName="Update" Text="${Common.Button.Save}" CssClass="button2"
                        ValidationGroup="vgSaveGroup" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="button2" />
                        </td>
                    </tr>
                </table>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Routing" runat="server" TypeName="com.Sconit.Web.FlowMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Flow" UpdateMethod="UpdateFlow"
    OnUpdated="ODS_Routing_Updated" OnUpdating="ODS_Routing_Updating" SelectMethod="LoadFlow">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
