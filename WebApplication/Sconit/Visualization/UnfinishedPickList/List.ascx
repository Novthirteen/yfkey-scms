<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Visualization_UnfinishedPickList_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <legend>${MasterData.Inventory.InspectOrder.Detail}</legend>
    <div>
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="PickListNo"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" DefaultSortExpression="PickListNo" DefaultSortDirection="Descending">
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.UnfinishedPickList.PickListNo}">
                    <ItemTemplate>
                        <asp:Label ID="lblPickListNo" runat="server" Text='<%# Bind("PickListNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.UnfinishedPickList.PartyTo}">
                    <ItemTemplate>
                        <asp:Label ID="lblPartyTo" runat="server" Text='<%# Bind("PartyTo.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.UnfinishedPickList.CreateDate}">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# Bind("CreateDate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.UnfinishedPickList.StartUser}">
                    <ItemTemplate>
                        <asp:Label ID="lblStartUser" runat="server" Text='<%# Bind("StartUser.Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.UnfinishedPickList.StartDate}">
                    <ItemTemplate>
                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("StartDate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
