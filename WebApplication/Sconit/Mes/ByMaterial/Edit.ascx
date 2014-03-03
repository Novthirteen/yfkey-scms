<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Mes_ByMaterial_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="divFV" runat="server">
   
            <fieldset>
                <legend>${Mes.ByMaterial.UpdateByMaterial}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblOrderNo" runat="server" Text="${Mes.ByMaterial.OrderNo}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbOrderNo" runat="server"  ReadOnly="true" />
                        </td>
                        <td class="ttd01">
                            <asp:Literal ID="lblTagNo" runat="server" Text="${Mes.ByMaterial.TagNo}:" />
                        </td>
                        <td class="ttd02">
                            <asp:TextBox ID="tbTagNo" runat="server" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblItem" runat="server" Text="${Mes.ByMaterial.Item}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbItem" runat="server" ReadOnly="true" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCreateDate" runat="server" Text="${Mes.ByMaterial.CreateDate}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCreateDate" runat="server" ReadOnly="true"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblCreateUser" runat="server" Text="${Mes.ByMaterial.CreateUser}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCreateUser" runat="server" ReadOnly="true" />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                    </tr>
                </table>
                <div class="tablefooter">
                    <div class="buttons">
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                            CssClass="back" />
                    </div>
                </div>
            </fieldset>
</div>

