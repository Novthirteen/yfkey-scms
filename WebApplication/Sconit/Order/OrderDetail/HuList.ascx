<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HuList.ascx.cs" Inherits="Order_OrderIssue_HuList" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Hu/List.ascx" TagName="HuList" TagPrefix="uc2" %>
<fieldset>
    <legend>${MasterData.Order.OrderDetail}</legend>
    <div>
        <div class="GridView">
            <asp:UpdatePanel ID="UP_GV_List" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="divMessage" runat="server">
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
                    <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        OnRowDataBound="GV_List_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataSequence%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblSeq" runat="server" Text='<%# Bind("Sequence") %>' onmouseup="if(!readOnly)select();" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemDesc%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("Item.Description") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataReferenceItemCode%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblReferenceItemCode" runat="server" Text='<%# Bind("ReferenceItemCode") %>' />
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
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataPackageType%>" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblPackageType" runat="server" Text='<%#Bind("PackageType") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLocationFrom%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocFrom" runat="server" Text='<%# Bind("DefaultLocationFrom.Code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLocationTo%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocTo" runat="server" Text='<%# Bind("DefaultLocationTo.Code") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuId%>">
                                <ItemTemplate>
                                    <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("HuId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataOrderedQty%>">
                                <ItemTemplate>
                                    <asp:Label ID="tbQty" runat="server" Text='<%# Bind("OrderedQty","{0:0.########}") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="tbHuScan" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <div style="text-align:right;margin:5px">
                <asp:Button ID="btnScanHu" runat="server" OnClick="btnScanHu_Click" Text="${Common.Button.BatchScanHu}" Visible="false"
                    CssClass="button2" />
            </div>
        </div>
</fieldset>
<uc2:hulist id="ucHuList" runat="server" visible="false" />
