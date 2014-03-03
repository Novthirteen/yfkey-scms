<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InTransitList.ascx.cs"
    Inherits="Visualization_InTransit_InTransitList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="floatdiv">
    <div id='floatdivtitle'>
        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" CssClass="btnClose" />
    </div>
    <fieldset>
        <legend>${InProcessLocation.InProcessLocationDetail}</legend>
        <div class="GridView">
            <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
                ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
                CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
                SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="${InProcessLocation.IpNo}">
                        <ItemTemplate>
                            <asp:Label ID="lblIpNo" runat="server" Text='<%# Bind("InProcessLocation.IpNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.OrderNo}">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.OrderHead.OrderNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="${Common.From}">
                        <ItemTemplate>
                            <asp:Label ID="lblPartyFrom" runat="server" Text='<%# Bind("InProcessLocation.PartyFrom.Code") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.ItemCode}">
                        <ItemTemplate>
                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.Item.Code") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.ItemDescription}">
                        <ItemTemplate>
                            <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.Item.Description") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.ReferenceItemCode}">
                        <ItemTemplate>
                            <asp:Label ID="lblReferenceItemCode" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.ReferenceItemCode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.Uom}">
                        <ItemTemplate>
                            <asp:Label ID="lblUom" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.Uom.Code") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.UnitCount}">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.UnitCount","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.HuId}">
                        <ItemTemplate>
                            <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("HuId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.LotNo}">
                        <ItemTemplate>
                            <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LotNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.Qty}">
                        <ItemTemplate>
                            <asp:Label ID="lblShippedQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </cc1:GridView>
            <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
            </cc1:GridPager>
        </div>
    </fieldset>
</div>
<div id='divHide' />
