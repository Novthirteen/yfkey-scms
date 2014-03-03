<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Warehouse_CheckASN_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <legend>批量确认 </legend>
    <table class="mtable">
        <tr>
            <td class="td01">
                ASN:
            </td>
            <td class="td02">
                <asp:TextBox ID="IpNo" runat="server"  ></asp:TextBox>
            </td>
            <td colspan="2" class="td01"></td>
        </tr>
        <tr><td></td>
            <td class="td02">
                <asp:Button ID="btnConfirm" runat="server"    Text="添加" OnClick="btnConfirm_Click"  CssClass="button2" />
                <asp:Button ID="btnCon" runat="server" Text="确认" OnClick="btnBatchCon_Click" />
                <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_Click" />
            </td>
        </tr>
    </table>
</fieldset>
<fieldset>
    <legend>明细</legend>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" AllowMultiColumnSorting="false"
            AutoLoadStyle="false" SeqNo="0" SeqText="No." ShowSeqNo="true" Width="100%" CellMaxLength="20"
            TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll" SelectCountMethod="FindCount"
            DefaultSortExpression="CreateDate" DefaultSortDirection="Descending">
            <Columns>
                <asp:BoundField DataField="IpNo" HeaderText="${InProcessLocation.IpNo}" SortExpression="IpNo" />
                <asp:TemplateField HeaderText="${InProcessLocation.PartyTo}" SortExpression="PartyTo">
                    <ItemTemplate>
                        <asp:Label ID="PartyToName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PartyTo.Name")%>'
                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "ShipTo.Address")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${InProcessLocation.Status}">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblStatus" runat="server" Code="Status" Value='<%# Bind("Status") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreateDate" HeaderText="${InProcessLocation.CreateDate}"
                    SortExpression="CreateDate" />
                <asp:TemplateField HeaderText="${Common.Business.CreateUser}" SortExpression="CreateUser.FirstName">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "CreateUser.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDel" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IpNo") %>'
                            Text="删除" OnClick="lbtnDel_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" Visible="false" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
