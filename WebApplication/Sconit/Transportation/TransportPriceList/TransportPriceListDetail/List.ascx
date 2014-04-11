<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Transportation_TransportPriceList_TransportPriceListDetail_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="${Transportation.TransportPriceListDetail.ShipFrom}"
                    SortExpression="ShipFrom">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ShipFrom.FullAddress")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Transportation.TransportPriceListDetail.ShipTo}"
                    SortExpression="ShipTo">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ShipTo.FullAddress")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Transportation.TransportPriceListDetail.PricingMethod}"
                    SortExpression="PricingMethod">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblPricingMethod" runat="server" Code="PricingMethod" Value='<%# Bind("PricingMethod") %>' />
                        <asp:Label ID="lbPricingMethod" runat="server" Text='<%# Eval("PricingMethod")%>'
                            CssClass="hidden" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Transportation.TransportPriceListDetail.VehicleType}"
                    SortExpression="VehicleType">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblVehicleType" runat="server" Code="VehicleType" Value='<%# Bind("VehicleType") %>' />
                        <asp:Label ID="lbVehicleType" runat="server" Text='<%# Eval("VehicleType")%>' CssClass="hidden" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UnitPrice" HeaderText="${Transportation.TransportPriceListDetail.UnitPrice}" SortExpression="UnitPrice"
                    DataFormatString="{0:0.########}" />
               <asp:BoundField DataField="StartQty" HeaderText="${Transportation.TransportPriceListDetail.StartQty}" SortExpression="StartQty"
                    DataFormatString="{0:0.########}" />
               <asp:BoundField DataField="EndQty" HeaderText="${Transportation.TransportPriceListDetail.EndQty}" SortExpression="EndQty"
                    DataFormatString="{0:0.########}" />
                <asp:CheckBoxField DataField="IsProvisionalEstimate" HeaderText="${Transportation.TransportPriceListDetail.IsProvisionalEstimate}"
                    SortExpression="IsProvisionalEstimate" />
                <asp:BoundField DataField="StartDate" HeaderText="${Common.Business.StartDate}" SortExpression="StartDate"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="EndDate" HeaderText="${Common.Business.EndDate}" SortExpression="EndDate"
                    DataFormatString="{0:yyyy-MM-dd}" />
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click">
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
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
