<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Order_GoodsReceipt_AsnReceipt_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="IpNo"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound" DefaultSortExpression="CreateDate"
            DefaultSortDirection="Descending">
            <Columns>
                <asp:BoundField DataField="IpNo" HeaderText="${InProcessLocation.IpNo}" SortExpression="IpNo" />
                <asp:TemplateField HeaderText="${InProcessLocation.Type}">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblType" runat="server" Code="IpType" Value='<%# Bind("Type") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${InProcessLocation.PartyFrom}" SortExpression="PartyFrom">
                    <ItemTemplate>
                        <asp:Label ID="PartyFromName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PartyFrom.Name")%>'
                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "ShipFrom.Address")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="${InProcessLocation.ShipFrom.Address}" SortExpression="ShipFrom.Address">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ShipFrom.Address")%>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="${InProcessLocation.PartyToCode}"  >
                    <ItemTemplate>
                        <asp:Label ID="PartyToCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PartyTo.Code")%>'
                             />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${InProcessLocation.PartyTo}" SortExpression="PartyTo">
                    <ItemTemplate>
                        <asp:Label ID="PartyToName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PartyTo.Name")%>'
                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "ShipTo.Address")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="${InProcessLocation.ShipTo.Address}" SortExpression="ShipTo.Address">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ShipTo.Address")%>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="DockDescription" HeaderText="${InProcessLocation.DockDescription}"
                    SortExpression="DockDescription" />
                      <asp:BoundField DataField="CreateDate" HeaderText="${InProcessLocation.CreateDate}"
                    SortExpression="CreateDate" />
                <asp:TemplateField HeaderText="${InProcessLocation.Status}">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblStatus" runat="server" Code="Status" Value='<%# Bind("Status") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
              
                <asp:TemplateField HeaderText="${Common.Business.CreateUser}" SortExpression="CreateUser.FirstName">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "CreateUser.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <cc1:LinkButton ID="lbtnView" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IpNo") %>'
                            Text="${Common.Button.View}" OnClick="lbtnView_Click" FunctionId="ViewAsn">
                        </cc1:LinkButton>
                        <cc1:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IpNo") %>'
                            Text="${MasterData.Order.Button.Receive}" OnClick="lbtnEdit_Click" FunctionId="ReceiveAsn">
                        </cc1:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
    <div class="GridView">
        <cc1:GridView ID="GV_List_Detail" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp_Detail" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_Detail_RowDataBound" DefaultSortExpression="Id"
            DefaultSortDirection="Descending">
            <Columns>
                <asp:TemplateField HeaderText="${InProcessLocation.IpNo}" SortExpression="InProcessLocation">
                    <ItemTemplate>
                        <asp:Label ID="lblIpNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InProcessLocation.IpNo")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${InProcessLocation.InProcessLocationDetail.OrderNo}"
                    SortExpression="OrderLocationTransaction.OrderDetail.OrderHead">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.OrderHead.OrderNo")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${InProcessLocation.InProcessLocationDetail.Item}"
                    SortExpression="OrderLocationTransaction.Item">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.Item.Code")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ItemDescription}" SortExpression="OrderLocationTransaction.Item">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.Item.Description")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.ItemRef}" SortExpression="OrderLocationTransaction.OrderDetail.ReferenceItemCode">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.ReferenceItemCode")%>' />
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="${Common.Business.Uom}" SortExpression="OrderLocationTransaction.Uom.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.Item.Uom.Code")%>' 
                        ToolTip='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.Item.Uom.Description")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.UnitCount}" SortExpression="OrderLocationTransaction.OrderDetail.UnitCount">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.UnitCount", "{0:0.########}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.LocFrom}" SortExpression="OrderLocationTransaction.OrderDetail.LocationFrom.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationFrom" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.DefaultLocationFrom.Code")%>' 
                         ToolTip='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.DefaultLocationFrom.Name")%>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                 
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.LocTo}" SortExpression="OrderLocationTransaction.OrderDetail.LocationTo.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationTo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.LocationTo.Code")%>'
                        ToolTip='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.DefaultLocationTo.Name")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PartyTo" Visible="false" SortExpression="OrderLocationTransaction.OrderDetail.OrderHead.PartyTo.Code">
                    <ItemTemplate>
                        <asp:Label ID="lblPartyTo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.OrderHead.PartyTo.Code")%>'
                        />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="PartyTo描述" Visible="false" SortExpression="OrderLocationTransaction.OrderHead.PartyTo.Name">
                    <ItemTemplate>
                        <asp:Label ID="lblPartyToCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDetail.OrderHead.PartyTo.Name")%>'
                        />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Qty" HeaderText="${Common.Business.Qty}" SortExpression="Qty"  DataFormatString="{0:0.########}"/>
                 
                 <asp:TemplateField HeaderText="${Common.Business.CreateDate}" SortExpression="InProcessLocation.CreateDate">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InProcessLocation.CreateDate")%>'   />
                    </ItemTemplate>
                </asp:TemplateField>
                
                     <asp:TemplateField HeaderText="创建日期" Visible="false" SortExpression="InProcessLocation.CreateDate">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# String.Format("{0:yyyy-MM-dd} ", DataBinder.Eval(Container.DataItem, "InProcessLocation.CreateDate"))%>'   />
                    </ItemTemplate>
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="创建时间" Visible="false" SortExpression="InProcessLocation.CreateDate">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# String.Format("{0:HH:mm:ss} ", DataBinder.Eval(Container.DataItem, "InProcessLocation.CreateDate"))%>'   />
                    </ItemTemplate>
                </asp:TemplateField>
                          
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp_Detail" runat="server" GridViewID="GV_List_Detail" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
