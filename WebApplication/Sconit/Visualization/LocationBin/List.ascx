<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Visualization_LocationBin_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<%@ Register Src="~/Visualization/LocationBin/ItemView.ascx" TagName="ItemView" TagPrefix="uc" %>
<%@ Register Src="~/Visualization/LocationBin/HuView.ascx" TagName="HuView" TagPrefix="uc" %>
<fieldset>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" OnRowDataBound="GV_List_RowDataBound"
            OnDataBound="GV_List_DataBound">
            <Columns>
                <asp:BoundField DataField="Location" HeaderText="${Common.Business.Location}" SortExpression="Location" />
                <asp:BoundField DataField="LocationName" HeaderText="${MasterData.Location.Name}"
                    SortExpression="LocationName" />
                <asp:BoundField DataField="Area" HeaderText="${MasterData.Location.Area}" SortExpression="Area" />
                <%--<asp:BoundField DataField="AreaDescription" HeaderText="${MasterData.Location.Area.Description}"
                    SortExpression="AreaDescription" />--%>
                <asp:BoundField DataField="Bin" HeaderText="${MasterData.Location.Bin}" SortExpression="Bin" />
                <%-- <asp:BoundField DataField="BinDescription" HeaderText="${MasterData.Location.Bin.Description}"
                    SortExpression="BinDescription" />--%>
                <asp:TemplateField HeaderText="${Visualization.LocationBin.ItemCount}" SortExpression="ItemCount">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbItemView" runat="server" Text='<%# Eval("ItemCount")%>' CommandArgument='<%# Eval("Location")+","+Eval("Bin")%>'
                            OnClick="lbItemView_Click"></asp:LinkButton>
                        <asp:UpdatePanel ID="upItemView" runat="server">
                            <ContentTemplate>
                                <uc:ItemView ID="ucItemView" runat="server" Visible="false" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lbItemView" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Visualization.LocationBin.HuCount}" SortExpression="HuCount">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbHuView" runat="server" Text='<%# Eval("HuCount")%>' CommandArgument='<%# Eval("Location")+","+Eval("Bin")%>'
                            OnClick="lbHuView_Click"></asp:LinkButton>
                        <asp:UpdatePanel ID="upHuView" runat="server">
                            <ContentTemplate>
                                <uc:HuView ID="ucHuView" runat="server" Visible="false" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lbHuView" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <%-- <sc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </sc1:GridPager>--%>
    </div>
</fieldset>
