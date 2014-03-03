<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Visualization_NoSynchronizedData_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <legend>${MasterData.Inventory.InspectOrder.Detail}</legend>
    <div>
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" DefaultSortExpression="Id" DefaultSortDirection="Descending">
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.NoSynchronizedData.Id}">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.NoSynchronizedData.ExtObjectCode}">
                    <ItemTemplate>
                        <asp:Label ID="lblObjectCode" runat="server" Text='<%# Bind("DssOutboundControl.ExternalObjectCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.NoSynchronizedData.KeyCode}">
                    <ItemTemplate>
                        <asp:Label ID="lblKeyCode" runat="server" Text='<%# Bind("KeyCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.NoSynchronizedData.OrderNo}">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("OrderNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.NoSynchronizedData.RecNo}">
                    <ItemTemplate>
                        <asp:Label ID="lblRecNo" runat="server" Text='<%# Bind("ReceiptNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.NoSynchronizedData.PartyFrom}">
                    <ItemTemplate>
                        <asp:Label ID="lblPartyFrom" runat="server" Text='<%# Bind("PartyFrom") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.NoSynchronizedData.PartyTo}">
                    <ItemTemplate>
                        <asp:Label ID="lblPartyTo" runat="server" Text='<%# Bind("PartyTo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="${MasterData.NoSynchronizedData.Location}">
                    <ItemTemplate>
                        <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("Location") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.NoSynchronizedData.ReferenceLocation}">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceLocation" runat="server" Text='<%# Bind("ReferenceLocation") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="${MasterData.NoSynchronizedData.EffectiveDate}">
                    <ItemTemplate>
                        <asp:Label ID="lblEffectiveDate" runat="server" Text='<%# Bind("EffectiveDate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.NoSynchronizedData.Qty}">
                    <ItemTemplate>
                        <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="${MasterData.NoSynchronizedData.CreateDate}">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# Bind("CreateDate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.NoSynchronizedData.TransNo}">
                    <ItemTemplate>
                        <asp:Label ID="lblTransNo" runat="server" Text='<%# Bind("TransNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
