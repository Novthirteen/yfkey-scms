<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewStrategy.ascx.cs"
    Inherits="MasterData_Flow_ViewStrategy" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc2" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_Strategy" runat="server" DataSourceID="ODS_Strategy" DefaultMode="Edit"
        DataKeyNames="Code" OnDataBound="FV_Strategy_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend id="lStrategy" runat="server"></legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblFlowStrategy" runat="server" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lFlowStrategy" runat="server" Text='<%# Bind("FlowStrategy") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblLotGroup" runat="server" Text="${MasterData.Flow.Strategy.LotGroup}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lLotGroup" runat="server" Text='<%# Bind("LotGroup") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblStartLatency" runat="server" Text="${MasterData.Flow.Strategy.StartLatency}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lStartLatency" runat="server" Text='<%# Bind("StartLatency","{0:0.########}") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCompleteLatency" runat="server" Text="${MasterData.Flow.Strategy.CompleteLatency}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbCompleteLatency" runat="server" Text='<%# Bind("CompleteLatency","{0:0.########}") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblLeadTime" runat="server" Text="${MasterData.Flow.Strategy.LeadTime}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lLeadTime" runat="server" Text='<%# Bind("LeadTime","{0:0.########}") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblWeekInterval" runat="server" Text="${MasterData.Flow.Strategy.WeekInterval}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lWeekInterval" runat="server" Text='<%# Bind("WeekInterval") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblNextOrderTime" runat="server" Text="${MasterData.Flow.Strategy.NextOrderTime}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lNextOrderTime" runat="server" Text='<%# Bind("NextOrderTime") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblNextWinTime" runat="server" Text="${MasterData.Flow.Strategy.NextWinTime}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="lNextWinTime" runat="server" Text='<%# Bind("NextWinTime") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime1" runat="server" Text="${MasterData.Flow.Strategy.WinTime1}:" />
                        </td>
                        <td class="td02">
                            <asp:Label ID="lWinTime1" runat="server" Text='<%# Bind("WinTime1") %>' CssClass="wordbreak" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime2" runat="server" Text="${MasterData.Flow.Strategy.WinTime2}:" />
                        </td>
                        <td class="td02">
                            <asp:Label ID="lWinTime2" runat="server" Text='<%# Bind("WinTime2") %>' CssClass="wordbreak" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime3" runat="server" Text="${MasterData.Flow.Strategy.WinTime3}:" />
                        </td>
                        <td class="td02">
                            <asp:Label ID="lWinTime3" runat="server" Text='<%# Bind("WinTime3") %>' CssClass="wordbreak" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime4" runat="server" Text="${MasterData.Flow.Strategy.WinTime4}:" />
                        </td>
                        <td class="td02">
                            <asp:Label ID="lWinTime4" runat="server" Text='<%# Bind("WinTime4") %>' CssClass="wordbreak" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime5" runat="server" Text="${MasterData.Flow.Strategy.WinTime5}:" />
                        </td>
                        <td class="td02">
                            <asp:Label ID="tbWinTime5" runat="server" Text='<%# Bind("WinTime5") %>' CssClass="wordbreak" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime6" runat="server" Text="${MasterData.Flow.Strategy.WinTime6}:" />
                        </td>
                        <td class="td02">
                            <asp:Label ID="lWinTime6" runat="server" Text='<%# Bind("WinTime6") %>' CssClass="wordbreak" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblWinTime7" runat="server" Text="${MasterData.Flow.Strategy.WinTime7}:" />
                        </td>
                        <td class="td02" colspan="3">
                            <asp:Label ID="tbWinTime7" runat="server" Text='<%# Bind("WinTime7") %>' />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_Strategy" runat="server" TypeName="com.Sconit.Web.FlowMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Flow" SelectMethod="LoadFlow">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
