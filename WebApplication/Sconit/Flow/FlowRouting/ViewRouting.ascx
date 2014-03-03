<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewRouting.ascx.cs" Inherits="MasterData_Flow_ViewRouting" %>
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
                            <asp:Literal ID="lRouting" runat="server" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblDescription" runat="server" Text="${MasterData.Flow.Routing.Description}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lDescription" runat="server" />
                        </td>
                    </tr>
                </table>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Routing" runat="server" TypeName="com.Sconit.Web.FlowMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Flow" SelectMethod="LoadFlow">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
