<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="NewMrp_ShipPlan_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="ShowErrorMsg.ascx"   TagName="ShowErrorMsg"   TagPrefix="uc2" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="ReleaseNo" HeaderText="版本号" SortExpression="ReleaseNo" />
                <asp:BoundField DataField="EffDate" HeaderText="生效日期" SortExpression="EffDate" />
                <asp:BoundField DataField="Status" HeaderText="状态" SortExpression="Status" />
                <asp:BoundField DataField="CreateDate" HeaderText="创建用户" SortExpression="CreateDate" />
                <asp:BoundField DataField="CreateUser" HeaderText="创建时间" SortExpression="CreateUser" />
                <asp:BoundField DataField="LastModifyUser" HeaderText="最后修改用户" SortExpression="LastModifyUser" />
                <asp:BoundField DataField="LastModifyDate" HeaderText="最后修改时间" SortExpression="LastModifyDate" />
                <asp:BoundField DataField="ReleaseUser" HeaderText="释放用户" SortExpression="LastModifyUser" />
                <asp:BoundField DataField="ReleaseDate" HeaderText="释放时间" SortExpression="LastModifyDate" />
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ReleaseNo") %>'
                            Text="查看明细" OnClick="lbtnEdit_Click">
                        </asp:LinkButton>
                        &nbsp&nbsp
                        <asp:LinkButton ID="lbtnShowErrorMsg" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BatchNo") %>'
                            Text="错误日志" OnClick="lbtnShowErrorMsg_Click">
                       </asp:LinkButton>
                       &nbsp&nbsp
                       <asp:LinkButton ID="lbtSubmit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ReleaseNo") %>'
                            Text="释放" OnClick="btnSubmit_Click" OnClientClick="return confirm('确定要释放？')">
                       </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>

<div id="floatdiv">
    <uc2:ShowErrorMsg ID="ucShowErrorMsg" runat="server" Visible="false"  />
</div>
