<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EntityOpt.ascx.cs" Inherits="MasterData_GeneralCode_EntityOpt" %>
<asp:GridView ID="GV_EntityOpt" runat="server" AutoGenerateColumns="False" DataSourceID="ODS_GV_EntityOpt"
    AllowSorting="false" DataKeyNames="Code" OnRowUpdating="GV_EntityOpt_RowUpdating">
    <Columns>
        <asp:TemplateField HeaderText="${MasterData.GeneralCode.Seq}" SortExpression="Seq">
            <EditItemTemplate>
                <asp:TextBox ID="tbSeq" runat="server" Text='<%# Bind("Seq") %>' Width="50" MaxLength="50" />
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="lblSeq" runat="server" Text='<%# Eval("Seq") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Code" HeaderText="${MasterData.GeneralCode.Code}" SortExpression="Code"
            ReadOnly="true" />
        <asp:BoundField DataField="Description" HeaderText="${MasterData.GeneralCode.Description}"
            SortExpression="Description" ReadOnly="true" ControlStyle-Width="200px" />
        <asp:TemplateField HeaderText="${MasterData.GeneralCode.Value}" SortExpression="PreValue">
            <EditItemTemplate>
                <asp:TextBox ID="tbValue" runat="server" Text='<%# Bind("Value") %>' Width="95%"
                    MaxLength="250" />
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:CommandField ShowEditButton="True" ItemStyle-Width="80px" HeaderText="${Common.GridView.Action}">
        </asp:CommandField>
    </Columns>
</asp:GridView>
<asp:ObjectDataSource ID="ODS_GV_EntityOpt" runat="server" TypeName="com.Sconit.Web.EntityPreferenceMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.EntityPreference" UpdateMethod="UpdateEntityPreference"
    SelectMethod="LoadEntityPreference" OnUpdating="ODS_GV_EntityOpt_OnUpdating">
</asp:ObjectDataSource>
