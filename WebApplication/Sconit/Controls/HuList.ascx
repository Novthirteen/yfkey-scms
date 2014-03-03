<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HuList.ascx.cs" Inherits="Controls_HuList" %>
<%@ Register Src="~/Controls/UpdateProgress.ascx" TagName="UpdateProgress" TagPrefix="uc2" %>
<div id="floatdiv">
    <fieldset>
        <legend>${MasterData.Hu.List}</legend>
        <asp:UpdatePanel ID="UP_GV_List" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="No." ItemStyle-Width="30">
                            <ItemTemplate>
                                <asp:Literal ID="ltlSeq" runat="server" Text='<%# (Container as GridViewRow).RowIndex+1 %> ' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuid%>" ItemStyle-Width="200px">
                            <ItemTemplate>
                                <asp:Label ID="lblHuId" runat="server" Text='<%# Eval("HuId") %>' />
                                <asp:TextBox ID="tbHuId" runat="server" Text='<%# Bind("HuId") %>' Visible="false"
                                    AutoPostBack="true" OnTextChanged="tbHuId_TextChanged" onclick="this.select();" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LotNo" HeaderText="<%$Resources:Language,MasterDataLotNo%>" />
                        <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                            <ItemTemplate>
                                <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemDesc%>">
                            <ItemTemplate>
                                <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("ItemDescription") %>' />
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
                        <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataQty%>">
                            <ItemTemplate>
                                <asp:Label ID="lblCurrentQty" runat="server" Text='<%# Bind("CurrentQty","{0:0.########}") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataStorageBin%>">
                            <ItemTemplate>
                                <asp:Label ID="lblStorageBinCode" runat="server" Text='<%# Bind("StorageBinCode") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="<%$Resources:Language,ButtonDelete%>" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDeleteHu" runat="server" Text="<%$Resources:Language,ButtonDelete%>"
                                    OnClick="lbtnDelete_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
                <uc2:UpdateProgress ID="ucUpdateProgress" runat="server" />
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnBack" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="tablefooter">
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
        </div>
    </fieldset>
</div>
