<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Mes_ByMaterial_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="divFV" runat="server">
    <fieldset>
        <legend>${Mes.ByMaterial.AddByMaterial}</legend>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblOrderNo" runat="server" Text="${Mes.ByMaterial.OrderNo}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbOrderNo" runat="server" Text='<% #Bind("OrderNo") %>' CssClass="inputRequired" />
                    <asp:RequiredFieldValidator ID="rfvOrderNo" runat="server" ErrorMessage="${Mes.ByMaterial.OrderNo}${Common.String.Empty}"
                        Display="Dynamic" ControlToValidate="tbOrderNo" ValidationGroup="vgSave" />
                </td>
                <td class="ttd01">
                    <asp:Literal ID="lblTagNo" runat="server" Text="${Mes.ByMaterial.TagNo}:" />
                </td>
                <td class="ttd02">
                    <asp:TextBox ID="tbTagNo" runat="server" Text='<% #Bind("TagNo") %>' CssClass="inputRequired" />
                    <asp:RequiredFieldValidator ID="rfvTagNo" runat="server" ErrorMessage="${Mes.ByMaterial.TagNo}${Common.String.Empty}"
                        Display="Dynamic" ControlToValidate="tbTagNo" ValidationGroup="vgSave" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblItem" runat="server" Text="${Mes.ByMaterial.Item}:" />
                </td>
                <td class="td02">
                    <uc3:textbox ID="tbItem" runat="server" DescField="Description" ValueField="Code"
                        Width="250" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" MustMatch="true"
                        CssClass="inputRequired" />
                    <asp:RequiredFieldValidator ID="rfvItem" runat="server" ErrorMessage="${MasterData.Item.Code.Empty}"
                        Display="Dynamic" ControlToValidate="tbItem" ValidationGroup="vgSave" />
                </td>
                <td class="td01">
                </td>
                <td class="td02">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                </td>
                <td>
                    <div class="buttons">
                        <asp:Button ID="btnInsert" runat="server" CommandName="Insert" Text="${Common.Button.Save}"
                            CssClass="apply" ValidationGroup="vgSave" OnClick="btnCreate_Click"/>
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                            CssClass="back" />
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
