<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Reports_CycCntDiff_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<fieldset>
    <div class="GridView">
        <sc1:gridview id="GV_List" runat="server" autogeneratecolumns="False" datakeynames="Id"
            skinid="GV" allowmulticolumnsorting="false" autoloadstyle="false" seqno="0" seqtext="No."
            showseqno="true" allowsorting="True" allowpaging="True" pagerid="gp" width="100%"
            cellmaxlength="10" typename="com.Sconit.Web.CriteriaMgrProxy" selectmethod="FindAll"
            selectcountmethod="FindCount" onrowdatabound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="No." ItemStyle-Width="30">
                    <ItemTemplate>
                        <asp:Literal ID="ltlSeq" runat="server" Text='<%# (Container as GridViewRow).RowIndex+1 %> ' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.OrderNo}">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("CycleCount.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Location}">
                    <ItemTemplate>
                        <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("Location.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Bin}">
                    <ItemTemplate>
                        <asp:Label ID="lblStorageBin" runat="server" Text='<%# Bind("StorageBin") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemCode}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' />&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemDescription}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("Item.Description") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Uom}">
                    <ItemTemplate>
                        <asp:Label ID="lblUOM" runat="server" Text='<%# Bind("Item.Uom.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.UnitCount}">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("Item.UnitCount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.HuId}">
                    <ItemTemplate>
                        <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("HuId") %>' />&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.LotNo}">
                    <ItemTemplate>
                        <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LotNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Visualization.LocationBin.HuCount}">
                    <ItemTemplate>
                        <asp:Label ID="lblCartons" runat="server" Text='<%# Bind("Cartons") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${CycCnt.PhysicalCount}">
                    <ItemTemplate>
                        <asp:Label ID="lblQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${CycCnt.InvQty}">
                    <ItemTemplate>
                        <asp:Label ID="lblInvQty" runat="server" Text='<%# Bind("InvQty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${CycCnt.DiffQty}">
                    <ItemTemplate>
                        <asp:Label ID="lblDiffQty" runat="server" Text='<%# Bind("DiffQty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${CycCnt.ReferenceLocation}">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceLocation" runat="server" Text='<%# Bind("ReferenceLocation.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${CycCnt.DiffReason}">
                    <ItemTemplate>
                        <asp:Label ID="lblDiffReason" runat="server" Text='<%# Bind("DiffReason") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </sc1:gridview>
        <sc1:gridpager id="gp" runat="server" gridviewid="GV_List" pagesize="10">
        </sc1:gridpager>
    </div>
</fieldset>
