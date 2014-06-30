<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_Region_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc2" %>

<div id="divFV" runat="server">
    <asp:FormView ID="FV_Region" runat="server" DataSourceID="ODS_Region" DefaultMode="Insert"
        DataKeyNames="Code">
        <InsertItemTemplate>
            <fieldset>
                <legend>${MasterData.Region.AddRegion}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Region.Code}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Code") %>' CssClass="inputRequired"
                                Width="250"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="${MasterData.Region.Code.Empty}"
                                Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvInsert" runat="server" ControlToValidate="tbCode" ErrorMessage="${MasterData.Region.Code.Exists}"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="checkRegionExists" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblName" runat="server" Text="${MasterData.Region.Name}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbName" runat="server" Text='<%# Bind("Name") %>' Width="250"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblIsActive" runat="server" Text="${MasterData.Region.IsActive}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsActive" runat="server" Checked='<%#Bind("IsActive") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblType" runat="server" Text="${MasterData.Region.Type}:" />
                        </td>
                        <td class="td02">
                            <cc2:CodeMstrDropDownList ID="ddlRegionType" Code="RegionType" runat="server" >
                            </cc2:CodeMstrDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblPlant" runat="server" Text="${MasterData.Region.Plant}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbPlant" runat="server" Text='<%# Bind("Plant") %>' Width="250"></asp:TextBox>
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                            <div class="buttons">
                                <asp:Button ID="btnInsert" runat="server" OnClick="btnInsert_Click" Text="${Common.Button.Save}"
                                    CssClass="add" ValidationGroup="vgSave" />
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
<asp:ObjectDataSource ID="ODS_Region" runat="server" TypeName="com.Sconit.Web.RegionMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Region" InsertMethod="CreateRegion">
</asp:ObjectDataSource>
