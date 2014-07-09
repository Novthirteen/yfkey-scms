<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="NewMrp_CustomerSchedule_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="ReferenceScheduleNo" HeaderText="版本号" SortExpression="ReferenceScheduleNo" />
                <asp:BoundField DataField="Flow" HeaderText="销售路线" SortExpression="Flow" />
                <asp:BoundField DataField="ShipFlow" HeaderText="发运路线" SortExpression="ShipFlow" />
                <asp:BoundField DataField="Status" HeaderText="状态" SortExpression="Status" />
                <asp:BoundField DataField="Type" HeaderText="类型" SortExpression="Type" />
                <asp:BoundField DataField="CreateDate" HeaderText="创建用户" SortExpression="CreateDate" />
                <asp:BoundField DataField="CreateUser" HeaderText="创建时间" SortExpression="CreateUser" />
                <asp:BoundField DataField="ReleaseUser" HeaderText="释放用户" SortExpression="LastModifyUser" />
                <asp:BoundField DataField="ReleaseDate" HeaderText="释放时间" SortExpression="LastModifyDate" />
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ReferenceScheduleNo") %>'
                            Text="查看明细" OnClick="lbtnEdit_Click">
                        </asp:LinkButton>
                    </ItemTemplate>

                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="20">
        </cc1:GridPager>
    </div>
</fieldset>
