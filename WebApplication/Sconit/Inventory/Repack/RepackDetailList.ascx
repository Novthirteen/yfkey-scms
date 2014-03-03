<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RepackDetailList.ascx.cs"
    Inherits="Inventory_Repack_RepackDetailList" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="RepackDetailInfo.ascx" TagName="RepackDetailInfo" TagPrefix="uc2" %>
<fieldset>
    <legend>${MasterData.Inventory.RepackDetail.In}</legend>
    <asp:UpdatePanel ID="UP_GV_List_In" runat="server">
        <ContentTemplate>
            <div>
                <div runat="server" visible="false" id="divLoc">
                    <table class="mtable">
                        <tr>
                            <td class="td01">
                                <asp:Literal ID="lblLocation" runat="server" Text="<%$Resources:Language,MasterDataLocationFrom%>" />
                            </td>
                            <td class="td02">
                                <uc3:textbox ID="tbLocation" runat="server" Visible="true" DescField="Name" Width="280"
                                    ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocationByUserCode" />
                            </td>
                            <td class="td01">
                            </td>
                            <td class="td02">
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="InDiv" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td style="margin-right: 5px; width: 50%; text-align: right">
                                <asp:Literal ID="ltlInHuScan" runat="server" Text="<%$Resources:Language,RepackInHu%>" />
                                <asp:TextBox ID="tbInHuScan" runat="server" OnTextChanged="tbInHuScan_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                                <asp:Label ID="lblInMessage" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="GridView">
                    <asp:GridView ID="GV_InList" runat="server" AllowSorting="True" AutoGenerateColumns="False">
                        <Columns>
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
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="tablefooter">
        <asp:Button ID="btnDevanning" runat="server" Text="${Common.Button.CreateHu}" OnClick="btnDevanning_Click"
            CssClass="button2" Visible="false" />
    </div>
</fieldset>
<fieldset>
    <legend>${MasterData.Inventory.RepackDetail.Out}</legend>
    <asp:UpdatePanel ID="UP_GV_List_Out" runat="server">
        <ContentTemplate>
            <div>
                <div id="OutDiv" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td style="margin-right: 5px; width: 50%; text-align: right">
                                <asp:Literal ID="ltlOutHuScan" runat="server" Text="<%$Resources:Language,RepackOutHu%>" />
                                <asp:TextBox ID="tbOutHuScan" runat="server" OnTextChanged="tbOutHuScan_TextChanged"
                                    AutoPostBack="true"></asp:TextBox>
                                <asp:Label ID="lblOutMessage" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="GridView">
                    <asp:GridView ID="GV_OutList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        OnRowDataBound="GV_OutList_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfId" runat="server" Value='<%# Bind("Id") %>' />
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
                                    <asp:TextBox ID="tbOrderQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>'
                                        Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>
<div class="tablefooter">
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="${Common.Button.AddDetail}"
        CssClass="button2" Visible="false" />
    <asp:Button ID="btnRepack" runat="server" OnClick="btnRepack_Click" Text="${Common.Button.Confirm}"
        CssClass="button2" />
    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
        CssClass="button2" />
</div>
<uc2:RepackDetailInfo ID="ucRepackDetailInfo" runat="server" Visible="false" />
