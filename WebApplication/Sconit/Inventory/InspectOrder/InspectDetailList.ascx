<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InspectDetailList.ascx.cs"
    Inherits="Inventory_InspectOrder_InspectDetailList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
    function GVCheckClick() {
        if ($(".GVHeader input:checkbox").attr("checked") == true) {
            $(".GVRow input:checkbox").attr("checked", true);
            $(".GVAlternatingRow input:checkbox").attr("checked", true);
        }
        else {
            $(".GVRow input:checkbox").attr("checked", false);
            $(".GVAlternatingRow input:checkbox").attr("checked", false);
        }
    }
</script>

<fieldset>
    <legend>${MasterData.Inventory.InspectOrder.Detail}</legend>
    <asp:UpdatePanel ID="UP_GV_List" runat="server">
        <ContentTemplate>
            <div>
                <div class="GridView">
                    <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        OnRowDataBound="GV_List_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <div onclick="GVCheckClick()">
                                        <asp:CheckBox ID="CheckAll" runat="server" />
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBoxGroup" name="CheckBoxGroup" runat="server" />
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
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuId%>" Visible="false">
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
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataCurrentQualifiedQty%>"
                                Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbCurrentQualifiedQty" runat="server" Text='<%# Bind("CurrentQualifiedQty","{0:0.########}") %>'
                                        onmouseup="if(!readOnly)select();" Width="50" ReadOnly="true"></asp:TextBox>
                                    <asp:RangeValidator ID="rvCurrentQty" ControlToValidate="tbCurrentQualifiedQty" runat="server"
                                        Display="Dynamic" ErrorMessage="*" MaximumValue="999999999" MinimumValue="0"
                                        Type="Double" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataCurrentRejectedQty%>"
                                Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbCurrentRejectedQty" runat="server" Text='<%# Bind("CurrentRejectedQty","{0:0.########}") %>'
                                        onmouseup="if(!readOnly)select();" Width="50" ReadOnly="true"></asp:TextBox>
                                    <asp:RangeValidator ID="rvRejectQty" ControlToValidate="tbCurrentRejectedQty" runat="server"
                                        Display="Dynamic" ErrorMessage="*" MaximumValue="999999999" MinimumValue="0"
                                        Type="Double" />
                                </ItemTemplate>
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="${MasterData.InspectDetail.DefectClassification}">  
                                <ItemTemplate>
                                    <cc1:CodeMstrLabel ID="lblDefectClassification" runat="server" Code="DefectClassification" Value='<%# Bind("DefectClassification") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="${MasterData.InspectDetail.DefectFactor}">  
                                <ItemTemplate>
                                    <cc1:CodeMstrLabel ID="lblDefectFactor" runat="server" Code="DefectFactor" Value='<%# Bind("DefectFactor") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataDisposition%>">  
                                <ItemTemplate>
                                    <cc1:CodeMstrDropDownList ID="ddlDisposition" runat="server" Code="Disposition"  IncludeBlankOption="true"/>
                                    <cc1:CodeMstrLabel ID="lblDisposition" runat="server" Code="Disposition" Value='<%# Bind("Disposition") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>
