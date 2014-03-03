<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MasterData_Location_StorageArea_Main" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="../StorageBin/Main.ascx" TagName="Bin" TagPrefix="uc2" %>
<%@ Register Src="New.ascx" TagName="New" TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc2" %>
<div id="divAreaList" runat="server">
    <fieldset>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblLocationCode" runat="server" Text="${MasterData.Location.Code}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbLocationCode" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td class="td01">
                    <asp:Literal ID="lblAreaCode" runat="server" Text="${MasterData.Location.Area.Code}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbAreaCode" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                        CssClass="button2" />
                    <asp:Button ID="btnNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click"
                        CssClass="button2" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        CssClass="button2" />
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset runat="server" id="fldsgv" visible="false">
        <div class="GridView">
            <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Code"
                ShowSeqNo="true" AllowSorting="true" OnRowDataBound="GV_List_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="${Common.Business.Code}" SortExpression="Code">
                        <ItemTemplate>
                            <asp:Literal ID="ltlCode" runat="server" Text='<%# Eval("Code") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Description" HeaderText="${Common.Business.Description}"
                        SortExpression="Desc1" />
                    <asp:CheckBoxField DataField="IsActive" HeaderText="${MasterData.Item.IsActive}"
                        SortExpression="IsActive" />
                    <asp:TemplateField HeaderText="${Common.GridView.Action}" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>'
                                Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click" />
                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>'
                                Text="${Common.Button.Delete}" OnClick="lbtnDelete_Click" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            
        <asp:Literal ID="ltlMessage" runat="server" Text="${Common.GridView.NoRecordFound}" Visible="false"/>
        </div>
    </fieldset>
</div>
<uc2:New ID="ucNew" runat="server" Visible="false" />
<uc2:Edit ID="ucEdit" runat="server" Visible="false" />
