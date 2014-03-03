<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_PriceList_PriceList_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_PriceList" runat="server" DataSourceID="ODS_PriceList" DefaultMode="Edit"
        Width="100%" DataKeyNames="Code" OnDataBound="FV_PriceList_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.PriceList.UpdatePriceList}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Common.Business.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:Label ID="lbCode" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlPartyCode" runat="server" Text="${MasterData.Party.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbPartyCode" runat="server" Text='<%# Eval("Party.Code") %>' ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlPartyName" runat="server" Text="${MasterData.Party.Name}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbPartyName" runat="server" Text='<%# Eval("Party.Name") %>' Width="250"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${Common.Business.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%# Bind("IsActive") %>' />
                        </td>
                    </tr>
                </table>
                <div class="tablefooter">
                    <div class="buttons">
                        <asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="${Common.Button.Save}"
                            CssClass="apply" ValidationGroup="vgSave" />
                        <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}"
                            CssClass="delete" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                            CssClass="back" />
                    </div>
                </div>
            </fieldset>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_PriceList" runat="server" TypeName="com.Sconit.Web.PriceListMgrProxy"
    OnUpdated="ODS_PriceList_Updated" OnUpdating="ODS_PriceList_Updating" OnDeleted="ODS_PriceList_Deleted">
    <SelectParameters>
        <asp:Parameter Name="Code" Type="String" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="Code" Type="String" />
    </DeleteParameters>
</asp:ObjectDataSource>
