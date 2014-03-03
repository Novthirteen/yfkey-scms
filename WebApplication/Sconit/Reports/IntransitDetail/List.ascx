<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Reports_IntransitDetail_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeys="ItemCode, Uom, UnitCount">
            <Columns>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.Supplier}">
                    <ItemTemplate>
                        <asp:Label ID="lblSupplier" runat="server" Text='<%# Eval("PartyFrom")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.Customer}">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("PartyTo")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.ItemCode}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ItemCode")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.ItemDescription}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("ItemName")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.ReferenceItemCode}">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceItemCode" runat="server" Text='<%# Eval("ReferenceItem")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.Uom}">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Eval("Uom")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.UnitCount}">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Eval("UnitCount", "{0:0.###}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Reports.IntransitDetail.DefaultActivity}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtDefaultActivity" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$0"%>'
                            Text='<%# Eval("DefaultActivity", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity1" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$1"%>'
                            Text='<%# Eval("Activity1", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity2" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$2"%>'
                            Text='<%# Eval("Activity2", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity3" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$3"%>'
                            Text='<%# Eval("Activity3", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity4" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$4"%>'
                            Text='<%# Eval("Activity4", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity5" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$5"%>'
                            Text='<%# Eval("Activity5", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity6" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$6"%>'
                            Text='<%# Eval("Activity6", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity7" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$7"%>'
                            Text='<%# Eval("Activity7", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity8" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$8"%>'
                            Text='<%# Eval("Activity8", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity9" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$9"%>'
                            Text='<%# Eval("Activity9", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity10" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$10"%>'
                            Text='<%# Eval("Activity10", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity11" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$11"%>'
                            Text='<%# Eval("Activity11", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity12" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$12"%>'
                            Text='<%# Eval("Activity12", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity13" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$13"%>'
                            Text='<%# Eval("Activity13", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity14" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$14"%>'
                            Text='<%# Eval("Activity14", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity15" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$15"%>'
                            Text='<%# Eval("Activity15", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity16" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$16"%>'
                            Text='<%# Eval("Activity16", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity17" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$17"%>'
                            Text='<%# Eval("Activity17", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity18" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$18"%>'
                            Text='<%# Eval("Activity18", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity19" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$19"%>'
                            Text='<%# Eval("Activity19", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtActivity20" runat="server" CommandArgument='<%# Eval("ItemCode") + "$" + Eval("Uom") + "$" + Eval("UnitCount") + "$20"%>'
                            Text='<%# Eval("Activity20", "{0:0.###}")%>' OnClick="lbtnDetail_Click">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Literal ID="lblNoRecordFound" runat="server" Text="${Common.GridView.NoRecordFound}"
            Visible="false" />
    </div>
</fieldset>
