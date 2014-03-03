<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Inventory_PendingInspectOrder_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
    function CheckAll() {
        var GV_ListId = document.getElementById("<%=GV_List.ClientID %>");
        var allselect = GV_ListId.rows[0].cells[0].getElementsByTagName("INPUT")[0].checked;
        for (i = 1; i < GV_ListId.rows.length; i++) {
            GV_ListId.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = allselect;
        }
    }

</script>

<fieldset>
    <legend>${MasterData.Inventory.InspectOrder.Detail}</legend>
    <div>
        <div class="GridView">
            <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                OnRowDataBound="GV_List_RowDataBound">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <div onclick="CheckAll()">
                                <asp:CheckBox ID="CheckAll" runat="server" />
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBoxGroup" name="CheckBoxGroup" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${MasterData.Inventory.InspectOrder.InspectNo}">
                        <ItemTemplate>
                            <asp:Label ID="lblInspectOrder" runat="server" Text='<%# Bind("InspectOrder.InspectNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="${MasterData.Inventory.InspectOrder.IpNo}">
                        <ItemTemplate>
                            <asp:Label ID="lblAsn" runat="server" Text='<%# Bind("InspectOrder.IpNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="${MasterData.Inventory.InspectOrder.ReceiptNo}">
                        <ItemTemplate>
                            <asp:Label ID="lblReceiptNo" runat="server" Text='<%# Bind("InspectOrder.ReceiptNo") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfId" runat="server" Value='<%# Bind("Id") %>' />
                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("LocationLotDetail.Item.Code") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemDesc%>">
                        <ItemTemplate>
                            <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("LocationLotDetail.Item.Description") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUom%>">
                        <ItemTemplate>
                            <asp:Label ID="lblUom" runat="server" Text='<%# Bind("LocationLotDetail.Item.Uom.Code") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataUnitCount%>">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("LocationLotDetail.Item.UnitCount","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLocationFrom%>">
                        <ItemTemplate>
                            <asp:Label ID="lblLocationFrom" runat="server" Text='<%# Bind("LocationFrom.Code") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataLocationTo%>">
                        <ItemTemplate>
                            <asp:Label ID="lblLocationTo" runat="server" Text='<%# Bind("LocationTo.Code") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuId%>" >
                        <ItemTemplate>
                            <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("LocationLotDetail.Hu.HuId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataInspectQty%>">
                        <ItemTemplate>
                            <asp:Label ID="lblInspectQty" runat="server" Text='<%# Bind("InspectQty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataQualifiedQty%>">
                        <ItemTemplate>
                            <asp:Label ID="lblQualifiedQty" runat="server" Text='<%# Bind("QualifiedQty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataRejectedQty%>">
                        <ItemTemplate>
                            <asp:Label ID="lblRejectedQty" runat="server" Text='<%# Bind("RejectedQty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataPendingQualifiedQty%>">
                        <ItemTemplate>
                            <asp:Label ID="lblPendingQualifiedQty" runat="server" Text='<%# Bind("PendingQualifiedQty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataPendingRejectedQty%>">
                        <ItemTemplate>
                            <asp:Label ID="lblPendingRejectedQty" runat="server" Text='<%# Bind("PendingRejectedQty","{0:0.########}") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataCurrentQualifiedQty%>">
                        <ItemTemplate>
                            <asp:TextBox ID="tbCurrentQualifiedQty" runat="server" Text='<%# Bind("CurrentQualifiedQty","{0:0.########}") %>'
                                onmouseup="if(!readOnly)select();" Width="50"></asp:TextBox>
                            <asp:RangeValidator ID="rvCurrentQty" ControlToValidate="tbCurrentQualifiedQty" runat="server"
                                Display="Dynamic" ErrorMessage="*" MaximumValue="999999999" MinimumValue="0"
                                Type="Double" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataCurrentRejectedQty%>">
                        <ItemTemplate>
                            <asp:TextBox ID="tbCurrentRejectedQty" runat="server" Text='<%# Bind("CurrentRejectedQty","{0:0.########}") %>'
                                onmouseup="if(!readOnly)select();" Width="50"></asp:TextBox>
                            <asp:RangeValidator ID="rvRejectQty" ControlToValidate="tbCurrentRejectedQty" runat="server"
                                Display="Dynamic" ErrorMessage="*" MaximumValue="999999999" MinimumValue="0"
                                Type="Double" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblMessage" runat="server" Visible="false" />
        </div>
    </div>
</fieldset>
<div class="tablefooter">
    <asp:Button ID="btnConfirm" runat="server" Text="${Common.Button.Confirm}" CssClass="button2"
        OnClick="btnConfirm_Click" />
</div>
