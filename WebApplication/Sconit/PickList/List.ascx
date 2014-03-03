<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Distribution_PickList_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="PickListNo"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll" SelectCountMethod="FindCount"
            OnRowDataBound="GV_List_RowDataBound" DefaultSortExpression="CreateDate" DefaultSortDirection="Descending">
            <Columns>
                <asp:BoundField DataField="PickListNo" HeaderText="${MasterData.Distribution.PickList}"
                    SortExpression="PickListNo" />
                <asp:TemplateField HeaderText="${MasterData.Distribution.PickList.OrderType}" SortExpression="OrderType">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "OrderType")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Flow}" SortExpression="Flow">
                    <ItemTemplate>
                        <asp:Label ID="lblFlow" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Flow")%>'
                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "Flow")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.From}" SortExpression="PartyFrom">
                    <ItemTemplate>
                        <asp:Label ID="lblFrom" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PartyFrom.Name")%>'
                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "ShipFrom.Address")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.To}" SortExpression="PartyTo">
                    <ItemTemplate>
                        <asp:Label ID="lblTo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PartyTo.Name")%>'
                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "ShipTo.Address")%>' />
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
                <asp:TemplateField HeaderText="${MasterData.Distribution.PickList.Status}" SortExpression="Status">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Status")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PickListNo") %>'
                            Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click">
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PickListNo") %>'
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
        <cc1:GridView ID="GV_Detail" runat="server"  AutoGenerateColumns="False" DataKeyNames="Id"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp_Detail" Width="100%"
            TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll" SelectCountMethod="FindCount"
            OnRowDataBound="GV_Detail_RowDataBound" DefaultSortExpression="Id" DefaultSortDirection="Descending">
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.Distribution.PickList}">
                    <ItemTemplate>
                        <asp:Label ID="lblPickListNo" runat="server" Text='<%# Bind("PickList.PickListNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataOrderNo%>">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.OrderHead.OrderNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Distribution.PickList.Status}">
                    <ItemTemplate>
                        <asp:Label ID="lblPickListStatus" runat="server" Text='<%# Bind("PickList.Status") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfId" runat="server" Value='<%# Bind("Id") %>' />
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemDesc%>">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("Item.Description") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUom%>">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.Uom.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUnitCount%>">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("UnitCount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLocation%>">
                    <ItemTemplate>
                        <asp:Label ID="lblLoc" runat="server" Text='<%# Bind("Location.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataStorageBin%>">
                    <ItemTemplate>
                        <asp:Label ID="lblStorageBin" runat="server" Text='<%# Bind("StorageBin.Code") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataOrderedQty%>">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLotNo%>">
                    <ItemTemplate>
                        <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LotNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataMemo%>">
                    <ItemTemplate>
                        <asp:Label ID="lblMemo" runat="server" Text='<%# Bind("Memo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Common.CreateDate}">
                    <ItemTemplate>
                        <asp:Label ID="lblCereateDate" runat="server" Text='<%# Bind("PickList.CreateDate") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp_Detail" runat="server" GridViewID="GV_Detail" PageSize="10">
        </cc1:GridPager>
    </div>
    <div>
        <cc1:GridView ID="GV_Result" runat="server"  AutoGenerateColumns="False" DataKeyNames="Id"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp_Result" Width="100%"
            TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll" SelectCountMethod="FindCount"
            OnRowDataBound="GV_Result_RowDataBound" DefaultSortExpression="PickListNo" DefaultSortDirection="Descending">
            <Columns>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataPicklist%>">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("PickListNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataOrderNo%>">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("OrderNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemDesc%>">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("ItemDescription") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUom%>">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("UomCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUnitCount%>">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("UnitCount","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLocation%>">
                    <ItemTemplate>
                        <asp:Label ID="lblLoc" runat="server" Text='<%# Bind("LocationCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataStorageBin%>">
                    <ItemTemplate>
                        <asp:Label ID="lblStorageBin" runat="server" Text='<%# Bind("StorageBinCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLotNo%>">
                    <ItemTemplate>
                        <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LotNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuId%>">
                    <ItemTemplate>
                        <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("HuId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataQty%>">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>        
        <cc1:GridPager ID="gp_Result" runat="server" GridViewID="GV_Result" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
