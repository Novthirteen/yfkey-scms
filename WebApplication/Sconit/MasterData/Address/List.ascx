<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MasterData_Address_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Code"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Code" HeaderText="${MasterData.Address.Code}" SortExpression="Code" />
                <asp:BoundField DataField="Sequence" HeaderText="${MasterData.Address.Sequence}"
                    SortExpression="Sequence" />
                <asp:BoundField DataField="Address" HeaderText="${MasterData.Address.Address}" SortExpression="Name" />
                <asp:CheckBoxField DataField="IsPrimary" HeaderText="${MasterData.Address.IsPrimary}"
                    SortExpression="IsPrimary" />
                <asp:BoundField DataField="PostalCode" HeaderText="${MasterData.Address.PostalCode}"
                    SortExpression="PostalCode" />
                <asp:BoundField DataField="TelephoneNumber" HeaderText="${MasterData.Address.TelephoneNumber}"
                    SortExpression="TelephoneNumber" />
                <asp:BoundField DataField="ContactPersonName" HeaderText="${MasterData.Address.ContactPersonName}"
                    SortExpression="ContactPersonName" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="${MasterData.Address.IsActive}"
                    SortExpression="IsActive" />
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>'
                            Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click">
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>'
                           Text="${Common.Button.Delete}" OnClick="lbtnDelete_Click" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
