<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LocTransList.ascx.cs"
    Inherits="Order_OrderView_LocTransList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script language="javascript" type="text/javascript">
    function GetItemUom(obj) {
        var objId = $(obj).attr("id");
        var parentId = objId.substring(0, objId.length - "tbItem_suggest".length);
        if ($(obj).val() != "") {
            Sys.Net.WebServiceProxy.invoke('Webservice/ItemMgrWS.asmx', 'GenerateItemProxy', false,
                { "itemCode": $(obj).val() },
            function OnSucceeded(result, eventArgs) {
                $('#' + parentId + 'tbUom').attr('value', result.Uom.Code);
            },
            function OnFailed(error) {
                alert(error.get_message());
            }
           );
        }
    }

    function UnitQtyChanged(obj) {
        var objId = $(obj).attr("id");
        var parentId = objId.substring(0, objId.length - "tbUnitQty".length);
        var qtyId = '#' + parentId + 'tbOrderdQty';
        var rate = '#' + parentId + 'hfRate';
        if ($(rate).val() != 0 && !isNaN($(obj).val())) {
            var qty = $(obj).val() * $(rate).val();
            $(qtyId).attr('value', qty.toFixed(2));
        }

    }
</script>

<div class="GridView">
    <div id="divMessage" runat="server" visible="false">
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
    <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
        AutoLoadStyle="false" AllowSorting="false" AllowPaging="false" Width="100%" OnRowDataBound="GV_List_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.Operation}" SortExpression="Operation">
                <ItemTemplate>
                    <asp:Label ID="lbOperation" runat="server" Text='<%# Bind("Operation") %>' />
                    <asp:HiddenField ID="hfId" runat="server" Value='<%# Bind("Id") %>' />
                    <asp:TextBox ID="tbOperation" runat="server" Visible="false" Width="50" />
                    <asp:RequiredFieldValidator ID="rfvOperation" runat="server" ControlToValidate="tbOperation"
                        Display="Dynamic" ErrorMessage="${MasterData.Order.OrderDetail.Operation.Required}"
                        ValidationGroup="vgAdd" Enabled="false" />
                    <asp:RangeValidator ID="rvOperation" ControlToValidate="tbOperation" runat="server"
                        Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="999999999"
                        MinimumValue="1" Type="Integer" Enabled="false" ValidationGroup="vgAdd" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.KitItem}">
                <ItemTemplate>
                    <asp:Label ID="lbKitItem" runat="server" Text='<%# Bind("OrderDetail.Item.Code") %>' />
                    <asp:DropDownList ID="tbKitItem" runat="server" Visible="false" DataValueField="Value"
                        DataTextField="Key" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.ItemCode}" SortExpression="Item.Code">
                <ItemTemplate>
                    <asp:Label ID="lbItem" runat="server" Text='<%# Bind("Item.Code") %>' />
                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>'
                        Text='<%# Bind("Item.Code") %>' OnClick="lbtnEdit_Click" Visible="false" />
                    <uc3:textbox ID="tbItem" runat="server" Visible="false" Width="250" DescField="Description"
                        ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem"
                        CssClass="inputRequired" InputWidth="80" MustMatch="true" />
                    <asp:RequiredFieldValidator ID="rfvItem" runat="server" ControlToValidate="tbItem"
                        Display="Dynamic" ErrorMessage="${MasterData.Order.OrderDetail.ItemCode.Required}"
                        ValidationGroup="vgAdd" Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.ItemDescription}" SortExpression="Item.Desc1">
                <ItemTemplate>
                    <asp:Label ID="lbItemDesc1" runat="server" Text='<%# Bind("Item.Desc1") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.ItemVersion}" SortExpression="ItemVersion">
                <ItemTemplate>
                    <asp:Label ID="lbItemVersion" runat="server" Text='<%# Bind("ItemVersion") %>' />
                    <asp:TextBox ID="tbItemVersion" runat="server" Text='<%# Bind("ItemVersion") %>'
                        Visible="false" Width="50" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.IsAssemble}" SortExpression="IsAssemble"
                Visible="false">
                <ItemTemplate>
                    <asp:CheckBox ID="cbIsAssemble" runat="server" Checked='<%# Bind("IsAssemble") %>'
                        Visible="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.Uom}" SortExpression="Uom.Code">
                <ItemTemplate>
                    <asp:TextBox ID="tbUom" runat="server" Text='<%# Bind("Uom.Code") %>' Width="50"
                        onfocus="this.blur();" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.UnitQty}" SortExpression="Uom.Code">
                <ItemTemplate>
                    <asp:HiddenField ID="hfRate" runat="server" Value='<%# Bind("OrderDetail.OrderedQty") %>' />
                    <asp:Label ID="lbUnitQty" runat="server" Text='<%# Bind("UnitQty","{0:0.########}") %>' />
                    <asp:TextBox ID="tbUnitQty" runat="server" Text='<%# Bind("UnitQty","{0:0.########}") %>'
                        Width="50" Visible="false" CssClass="inputRequired" />
                    <asp:RequiredFieldValidator ID="rfvUnitQty" runat="server" ControlToValidate="tbUnitQty"
                        Display="Dynamic" ErrorMessage="${MasterData.Order.OrderDetail.UnitQty.Required}"
                        ValidationGroup="vgAdd" Enabled="false" />
                    <asp:RangeValidator ID="rvUnitQty" ControlToValidate="tbUnitQty" runat="server" Display="Dynamic"
                        ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="999999999" MinimumValue="0"
                        Type="Double" Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.OrderdQty}">
                <ItemTemplate>
                    <asp:Label ID="lbOrderdQty" runat="server" Text='<%# Bind("OrderedQty","{0:0.########}") %>' />
                    <asp:TextBox ID="tbOrderdQty" runat="server" Text='<%# Bind("OrderedQty","{0:0.########}") %>'
                        Visible="false" Width="50" CssClass="inputRequired" />
                    <asp:RequiredFieldValidator ID="rfvOrderdQty" runat="server" ControlToValidate="tbOrderdQty"
                        Display="Dynamic" ErrorMessage="${MasterData.Order.OrderDetail.OrderdQty.Required}"
                        Enabled="false" />
                    <asp:RangeValidator ID="rvOrderdQty" ControlToValidate="tbOrderdQty" runat="server"
                        Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="999999999"
                        MinimumValue="-999999999" Type="Double" Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="${MasterData.Order.LocTrans.AccumulateQty}" DataField="AccumulateQty"
                DataFormatString="{0:0.########}" />
            <asp:BoundField HeaderText="${MasterData.Order.LocTrans.AccumulateRejectQty}" DataField="AccumulateRejectQty"
                DataFormatString="{0:0.########}" Visible="false" />
            <asp:BoundField HeaderText="${MasterData.Order.LocTrans.AccumulateScrapQty}" DataField="AccumulateScrapQty"
                DataFormatString="{0:0.########}" Visible="false" />
            <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.Location}" SortExpression="Location.Code">
                <ItemTemplate>
                    <asp:Label ID="lbLocation" runat="server" Text='<%# Bind("Location.Code") %>' />
                    <uc3:textbox ID="tbLocation" runat="server" DescField="Name" ValueField="Code" ServicePath="LocationMgr.service"
                        ServiceMethod="GetLocation" Visible="false" InputWidth="80" Width="250" MustMatch="true" CssClass="inputRequired" />
                    <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="tbLocation"
                        Display="Dynamic" ErrorMessage="${MasterData.InspectOrder.Location.Required}"
                        ValidationGroup="vgAdd" Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.RejectLocation}" SortExpression="RejectLocation.Code"
                Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lbRejectLocation" runat="server" Text='<%# Bind("RejectLocation.Code") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.NeedPrint}">
                <ItemTemplate>
                    <asp:CheckBox ID="cbNeedPrint" runat="server" Checked='<%# Bind("NeedPrint") %>'
                        Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.GridView.Action}" Visible="false">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnAdd" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>'
                        Text="${Common.Button.New}" OnClick="lbtnAdd_Click" Visible="false" ValidationGroup="vgAdd">
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>'
                        Text="${Common.Button.Delete}" OnClick="lbtnDelete_Click" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')">
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <fieldset visible="false" id="fdHuList" runat="server">
        <legend id="lTitle" runat="server" visible="false">${MasterData.Production.Hu.List}</legend>
        <asp:GridView ID="GV_HuList" runat="server" AutoGenerateColumns="False" DataKeyNames="HuId"
            AutoLoadStyle="false" AllowSorting="false" AllowPaging="false" Width="100%">
            <Columns>
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
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuId%>">
                    <ItemTemplate>
                        <asp:Label ID="lblHuId" runat="server" Text='<%# Bind("HuId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataOrderedQty%>">
                    <ItemTemplate>
                        <asp:Label ID="tbQty" runat="server" Text='<%# Bind("Qty","{0:0.########}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </fieldset>
</div>
