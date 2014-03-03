<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PickListDetailList.ascx.cs" Inherits="Distribution_PickList_PickListDetailList" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Hu/List.ascx" TagName="HuList" TagPrefix="uc2" %>
<%@ Register Src="~/Hu/HuInput.ascx" TagName="HuInput" TagPrefix="uc2" %>
<fieldset>
    <legend>${MasterData.PickList.PickListDetail}</legend>
    <asp:UpdatePanel ID="UP_GV_List" runat="server">
        <ContentTemplate>
            <div>
                <div id="divMessage" runat="server" >
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%;">
                            </td>
                            <td style="margin-right: 5px; width: 50%; text-align: right">
                                <asp:Literal ID="ltlHuScan" runat="server" Text="<%$Resources:Language,QuickScanHu%>" />
                                <asp:TextBox ID="tbHuScan" runat="server" OnTextChanged="tbHuScan_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="GridView">
                    <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        OnRowDataBound="GV_List_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataOrderNo%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("OrderLocationTransaction.OrderDetail.OrderHead.OrderNo") %>' />
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
                                    <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Uom.Code") %>' />
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
                            <%--<asp:TemplateField HeaderText="<%$Resources:Language,MasterDataStorageArea%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblStorageArea" runat="server" Text='<%# Bind("StorageArea.Code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataStorageBin%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblStorageBin" runat="server" Text='<%# Bind("StorageBin.Code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLotNo%>" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LotNo") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuId%>" >
                                <ItemTemplate>
                                    <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("HuId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataOrderedQty%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrderQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataCurrentShipQty%>">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbShipQty" runat="server" onmouseup="if(!readOnly)select();" onfocus="this.blur();"
                                        Width="50"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuId%>" Visible="true">
                                <ItemTemplate>
                                    <uc2:HuInput ID="ucHuInput" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc2:HuList ID="ucHuList" runat="server" Visible="false" />
    <div class="tablefooter">
        <asp:Button ID="btnScanHu" runat="server" OnClick="btnScanHu_Click" Text="${Common.Button.BatchScanHu}"
            CssClass="button2" />
    </div>
</fieldset>
