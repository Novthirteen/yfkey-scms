<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewLocTransList.ascx.cs"
    Inherits="MasterData_Flow_ViewLocTransList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="${MasterData.Flow.LocTrans.ItemCode}" SortExpression="Item.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Item.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.LocTrans.Uom}" SortExpression="Uom.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Uom.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="${MasterData.Flow.LocTrans.Operation}" DataField="Operation" />
                <asp:BoundField HeaderText="${MasterData.Flow.LocTrans.TransactionType}" DataField="TransactionType" />
                <asp:BoundField HeaderText="${MasterData.Flow.LocTrans.UnitQty}" DataField="UnitQty"  DataFormatString="{0:0.########}" />
                <asp:TemplateField HeaderText="${MasterData.Flow.LocTrans.Location}" SortExpression="Location.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Location.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.LocTrans.RejectLocation}" SortExpression="RejectLocation.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "RejectLocation.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="${MasterData.Flow.LocTrans.HuLotSize}" DataField="HuLotSize" />
                <asp:CheckBoxField HeaderText="${MasterData.Flow.LocTrans.NeedPrint}" DataField="NeedPrint" />
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
