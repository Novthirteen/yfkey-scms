<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MasterData_Employee_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Code"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="true" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="${Common.Business.Code}" SortExpression="Code" />
                <asp:BoundField DataField="Name" HeaderText="${Common.Business.Name}" SortExpression="Name" />
                <asp:TemplateField HeaderText="${Security.User.Gender}" SortExpression="Gender">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblGender" runat="server" Code="Gender" Value='<%# Bind("Gender") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Department" HeaderText="${MasterData.Employee.Department}" SortExpression="Department" />
                <asp:BoundField DataField="WorkGroup" HeaderText="${MasterData.Employee.WorkGroup}" SortExpression="WorkGroup" />
                <asp:BoundField DataField="Post" HeaderText="${MasterData.Employee.Post}" SortExpression="Post" />
                <asp:BoundField DataField="Memo" HeaderText="${MasterData.Employee.Memo}" SortExpression="Memo" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="${Common.Business.IsActive}"
                    SortExpression="IsActive" />
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>'
                            Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click" />
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>'
                            Text="${Common.Button.Delete}" OnClick="lbtnDelete_Click" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
