<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Transportation_TransportPriceList_New" %>

<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<div id="divFV" runat="server">
    <asp:FormView ID="FV_TransportPriceList" runat="server" DataSourceID="ODS_TransportPriceList" DefaultMode="Insert"
        DataKeyNames="Code">
        <InsertItemTemplate>
            <fieldset>
                <legend>${Transportation.TransportPriceList.AddTransportPriceList}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${Transportation.TransportPriceList.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Code") %>' CssClass="inputRequired"
                                Width="250"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="${Transportation.TransportPriceList.Code.Empty}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvCode" runat="server" ControlToValidate="tbCode" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblParty" runat="server" Text="${Transportation.TransportPriceList.Party}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbParty" runat="server" Width="250" DescField="Name" ValueField="Code"
                                CssClass="inputRequired" MustMatch="true" ServiceMethod="GetTransportationParty" ServicePath="PartyMgr.service"/>
                            <asp:RequiredFieldValidator ID="rfvParty" runat="server" ErrorMessage="${Transportation.TransportPriceList.Party.Empty}"
                                Display="Dynamic" ControlToValidate="tbParty" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvParty" runat="server" ControlToValidate="tbParty" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
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
<asp:ObjectDataSource ID="ODS_TransportPriceList" runat="server" TypeName="com.Sconit.Web.TransportPriceListMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.Transportation.TransportPriceList" InsertMethod="CreateTransportPriceList"
    OnInserted="ODS_TransportPriceList_Inserted" OnInserting="ODS_TransportPriceList_Inserting">
</asp:ObjectDataSource>
