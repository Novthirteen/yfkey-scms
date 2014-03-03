<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Inventory_InspectOrder_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="InspectNo"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll" SelectCountMethod="FindCount"
            OnRowDataBound="GV_List_RowDataBound" DefaultSortExpression="CreateDate" DefaultSortDirection="Descending">
            <Columns>
                <asp:BoundField DataField="InspectNo" HeaderText="${MasterData.Inventory.InspectOrder.InspectNo}"
                    SortExpression="InspectNo" />
                <asp:BoundField DataField="IpNo" HeaderText="${Common.Business.IpNo}" SortExpression="IpNo" />
                <asp:BoundField DataField="ReceiptNo" HeaderText="${Common.Business.ReceiptNo}" SortExpression="ReceiptNo" />
                <asp:TemplateField HeaderText="${MasterData.Distribution.PickList.CreateDate}" SortExpression="CreateDate">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "CreateDate")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Distribution.PickList.Status}" SortExpression="Status">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Status")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${OrderDetail.IsDetailHasHu}" SortExpression="IsDetailHasHu">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "IsDetailHasHu")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.IsPrinted}" SortExpression="IsPrinted">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "IsPrinted")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Distribution.PickList.CreateUser}" SortExpression="CreateUser">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "CreateUser.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Distribution.PickList.CreateDate}" SortExpression="CreateDate">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "CreateDate")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "InspectNo") %>'
                            Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click">
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "InspectNo") %>'
                            Text="${Common.Button.Delete}" OnClick="lbtnDelete_Click" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')"
                            Visible="false">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
    <div>
        <cc1:GridView ID="GV_List_Detail" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp_Detail" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" DefaultSortExpression="Id" DefaultSortDirection="Descending"
            OnRowDataBound="GV_List_Detail_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.Inventory.InspectOrder.InspectNo}">
                    <ItemTemplate>
                        <asp:Label ID="lblInspectNo" runat="server" Text='<%# Bind("InspectOrder.InspectNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.IpNo}">
                    <ItemTemplate>
                        <asp:Label ID="lblIpNo" runat="server" Text='<%# Bind("InspectOrder.IpNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ReceiptNo}">
                    <ItemTemplate>
                        <asp:Label ID="lblReceiptNo" runat="server" Text='<%# Bind("InspectOrder.ReceiptNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("LocationLotDetail.Item.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemDesc%>">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("LocationLotDetail.Item.Description") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUom%>">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("LocationLotDetail.Item.Uom.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUnitCount%>">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("LocationLotDetail.Item.UnitCount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLocationFrom%>">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationFrom" runat="server" Text='<%# Bind("LocationFrom.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLocationTo%>">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationTo" runat="server" Text='<%# Bind("LocationTo.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuId%>">
                    <ItemTemplate>
                        <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("LocationLotDetail.Hu.HuId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataInspectQty%>">
                    <ItemTemplate>
                        <asp:Label ID="lblInspectQty" runat="server" Text='<%# Bind("InspectQty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataQualifiedQty%>">
                    <ItemTemplate>
                        <asp:Label ID="lblQualifiedQty" runat="server" Text='<%# Bind("QualifiedQty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataRejectedQty%>">
                    <ItemTemplate>
                        <asp:Label ID="lblRejectedQty" runat="server" Text='<%# Bind("RejectedQty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.InspectDetail.DefectClassification}">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblDefectClassification" runat="server" Code="DefectClassification"
                            Value='<%# Bind("DefectClassification") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="${MasterData.InspectDetail.DefectFactor}">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblDefectFactor" runat="server" Code="DefectFactor"
                            Value='<%# Bind("DefectFactor") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataDisposition%>">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblDisposition" runat="server" Code="Disposition" Value='<%# Bind("Disposition") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Distribution.PickList.CreateDate}">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# Bind("InspectOrder.CreateDate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp_Detail" runat="server" GridViewID="GV_List_Detail" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
