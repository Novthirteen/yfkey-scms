<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Result.ascx.cs" Inherits="Hu_Inbound_Result" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AllowPaging="False" DataKeyNames="Id" AllowSorting="False"
            AutoGenerateColumns="False" >
            <Columns>                
                <asp:TemplateField HeaderText=" ${Hu.Inbound.HuId}" SortExpression="Hu.HuId">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Hu.HuId")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="LotNo" HeaderText="${Hu.Inbound.LotNo}" SortExpression="LotNo" />
                <asp:TemplateField HeaderText=" ${Hu.Inbound.Item}" SortExpression="Item.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Item.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" ${Hu.Inbound.Uom}" SortExpression="Uom.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Uom.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Qty" DataFormatString="{0:0.########}" HeaderText="${Hu.Inbound.Qty}" SortExpression="Qty" />
                <asp:BoundField DataField="OrderNo" HeaderText="${Hu.Inbound.OrderNo}" SortExpression="OrderNo" />
                <asp:BoundField DataField="ManufactureDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="${Hu.Inbound.ManufactureDate}" SortExpression="ManufactureDate" />
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
