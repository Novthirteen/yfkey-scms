<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DetailList.ascx.cs" Inherits="Reports_LocAging_DetailList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<div id="floatdiv" class="GridView">
    <fieldset>
        <legend>${Reports.InvAging.Detail}</legend>
        <div class="GridView">
            <sc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" SkinID="GV"
                AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" seqtext="No."
                ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
                CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
                SelectCountMethod="FindCount">
                <Columns>
                    <asp:TemplateField HeaderText="${Common.Business.Region}" SortExpression="Region">
                        <ItemTemplate>
                            <asp:Label ID="lblRegion" runat="server" Text='<%# Eval("Location.Region.Name")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.Location}" SortExpression="Location">
                        <ItemTemplate>
                            <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location.Code")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${MasterData.Location.Name}" SortExpression="Location">
                        <ItemTemplate>
                            <asp:Label ID="lblLocationName" runat="server" Text='<%# Eval("Location.Name")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.ItemCode}" SortExpression="Item">
                        <ItemTemplate>
                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item.Code")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.ItemDescription}" SortExpression="Item">
                        <ItemTemplate>
                            <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("Item.Description")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.Uom}" SortExpression="Item">
                        <ItemTemplate>
                            <asp:Label ID="lblUom" runat="server" Text='<%# Eval("Item.Uom.Code")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.Qty}">
                        <ItemTemplate>
                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty","{0:0.########}")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Reports.InvAging.CreateDate}" SortExpression="CreateDate">
                        <ItemTemplate>
                            <asp:Label ID="lblCreateDate" runat="server" Text='<%# Eval("CreateDate","{0:yyyy-MM-dd}")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </sc1:GridView>
            <sc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
            </sc1:GridPager>
        </div>
        <div class="tablefooter">
            <asp:Button ID="btnClose" runat="server" Text="${Common.Button.Close}" CssClass="button2"
                OnClick="btnClose_Click" />
        </div>
    </fieldset>
</div>
