<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Transportation_TransportPriceList_Edit" %>

<div id="divFV" runat="server">
    <asp:FormView ID="FV_TransportPriceList" runat="server" DataSourceID="ODS_TransportPriceList" DefaultMode="Edit"
        Width="100%" DataKeyNames="Code" OnDataBound="FV_TransportPriceList_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${Transportation.TransportPriceList.UpdateTransportPriceList}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Transportation.TransportPriceList.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbCode" runat="server" Text='<%# Bind("Code") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblParty" runat="server" Text="${Transportation.TransportPriceList.Party}:" />
                        </td>
                        <td class="td02">
                            <asp:Literal ID="tbParty" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${Transportation.TransportPriceList.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
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
                                <asp:Button ID="Button1" runat="server" CommandName="Update" Text="${Common.Button.Save}" CssClass="apply"
                                    ValidationGroup="vgSave" />
                                <asp:Button ID="Button2" runat="server" CommandName="Delete" Text="${Common.Button.Delete}" CssClass="delete"
                                    OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                                <asp:Button ID="Button3" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="back" />
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_TransportPriceList" runat="server" TypeName="com.Sconit.Web.TransportPriceListMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.Transportation.TransportPriceList" UpdateMethod="UpdateTransportPriceList"
    OnUpdated="ODS_TransportPriceList_Updated" OnUpdating="ODS_TransportPriceList_Updating" DeleteMethod="DeleteTransportPriceList"
    OnDeleted="ODS_TransportPriceList_Deleted" SelectMethod="LoadTransportPriceList">
    <SelectParameters>
        <asp:Parameter Name="code" Type="String" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="code" Type="String" />
    </DeleteParameters>
</asp:ObjectDataSource>
