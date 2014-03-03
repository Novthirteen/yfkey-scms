<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlowDetailList.ascx.cs"
    Inherits="Inventory_PrintHu_FlowDetailList" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/MRP/ShiftPlan/Manual/Shift.ascx" TagName="Shift" TagPrefix="uc" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<script language="javascript" type="text/javascript">
    function GenerateFlowDetail(obj) {
        var objId = $(obj).attr("id");
        var parentId = objId.substring(0, objId.length - "tbItemCode_suggest".length);
        if ($(obj).val() != "") {
            Sys.Net.WebServiceProxy.invoke('Webservice/FlowMgrWS.asmx', 'GenerateFlowDetailProxy', false,
                { "flowCode": "<%=FlowCode%>", "itemCode": $(obj).val(), "partyFromCode": "<%=PartyFromCode%>", "partyToCode": "<%=PartyToCode%>",
                    "moduleType": "<%=ModuleType%>", "changeRef": true, "startTime": "2000-1-1"
                },
            function OnSucceeded(result, eventArgs) {
                $('#' + parentId + 'tbItemDescription').attr('value', result.ItemDescription);
                $('#' + parentId + 'tbRefItemCode_suggest').attr('value', result.ItemReferenceCode);
                $('#' + parentId + 'tbUom_suggest').attr('value', result.UomCode);
                $('#' + parentId + 'tbUnitCount').attr('value', result.UnitCount);
            },
            function OnFailed(error) {
                alert(error.get_message());
            }
           );
        }
    }
    function GenerateFlowDetailProxyByReferenceItem(obj) {
        var objId = $(obj).attr("id");
        var parentId = objId.substring(0, objId.length - "tbRefItemCode_suggest".length);
        if ($(obj).val() != "") {
            Sys.Net.WebServiceProxy.invoke('Webservice/FlowMgrWS.asmx', 'GenerateFlowDetailProxyByReferenceItem', false,
                { "flowCode": "<%=FlowCode%>", "refItemCode": $(obj).val(), "partyFromCode": "<%=PartyFromCode%>", "partyToCode": "<%=PartyToCode%>",
                    "moduleType": "<%=ModuleType%>", "changeRef": false, "startTime": "2000-1-1"
                },
            function OnSucceeded(result, eventArgs) {
                $('#' + parentId + 'tbItemCode_suggest').attr('value', result.ItemCode);
                $('#' + parentId + 'tbItemDescription').attr('value', result.ItemDescription);
                $('#' + parentId + 'tbUom_suggest').attr('value', result.UomCode);
                $('#' + parentId + 'tbUnitCount').attr('value', result.UnitCount);
            },
            function OnFailed(error) {
                alert(error.get_message());
            }
           );
        }
    }
</script>

<fieldset>
    <table id="TabProd" class="mtable" runat="server" visible="false">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblDate" runat="server" Text="${Inventory.PrintHu.FlowDetail.ProdTime}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbWinTime" runat="server" CssClass="inputRequired" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})"
                    AutoPostBack="true" />
                <asp:RequiredFieldValidator ID="rfvDate" runat="server" ErrorMessage="${Inventory.PrintHu.FlowDetail.ProdTime.Required}"
                    Display="Dynamic" ControlToValidate="tbWinTime" ValidationGroup="vgPrint" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlShift" runat="server" Text="${Inventory.PrintHu.FlowDetail.Shift}:" />
            </td>
            <td class="td02">
                <uc:Shift ID="ucShift" runat="server" />
            </td>
        </tr>
    </table>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.FlowDetail.Sequence}">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfId" runat="server" Value='<%# Bind("Id") %>' />
                        <asp:HiddenField ID="hfIsBlankDetail" runat="server" Value='<%# Bind("IsBlankDetail") %>' />
                        <asp:Label ID="lblSeq" runat="server" Text='<%# Bind("Sequence") %>' onmouseup="if(!readOnly)select();" />
                        <asp:TextBox ID="tbSeq" runat="server" onmouseup="if(!readOnly)select();" Visible="false"
                            Width="30" Text='<%# Bind("Sequence") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.FlowDetail.Item.Code}">
                    <ItemTemplate>
                        <asp:TextBox ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' ReadOnly="true"
                            Width="100" />
                        <uc3:textbox ID="tbItemCode" runat="server" Visible="false" Width="250" DescField="Description"
                            ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem"
                            CssClass="inputRequired" InputWidth="80" MustMatch="true" />
                        <asp:RequiredFieldValidator ID="rfvItemCode" runat="server" ControlToValidate="tbItemCode"
                            Display="Dynamic" ErrorMessage="${Inventory.PrintHu.FlowDetail.ItemCode.Required}"
                            ValidationGroup="vgAdd" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.FlowDetail.Item.Description}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("Item.Description") %>' />
                        <asp:TextBox ID="tbItemDescription" runat="server" Visible="false" Width="150" ReadOnly="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.FlowDetail.ReferenceItem}">
                    <ItemTemplate>
                        <asp:Label ID="lblReferenceItemCode" runat="server" Text='<%# Bind("ReferenceItemCode") %>' />
                        <uc3:textbox ID="tbRefItemCode" runat="server" Visible="false" Width="200" DescField="ReferenceCode"
                            ValueField="ReferenceCode" ServicePath="ItemReferenceMgr.service" ServiceMethod="GetItemReferenceByParty"
                            InputWidth="80" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.FlowDetail.Uom}">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Bind("Uom.Code") %>' />
                        <uc3:textbox ID="tbUom" runat="server" Visible="false" Width="200" DescField="Description"
                            ServiceParameter="string:#tbItemCode" ValueField="Code" ServicePath="UomMgr.service"
                            InputWidth="50" ServiceMethod="GetItemUom" CssClass="inputRequired" />
                        <asp:RequiredFieldValidator ID="rfvUom" runat="server" ControlToValidate="tbUom"
                            Display="Dynamic" ErrorMessage="${Inventory.PrintHu.FlowDetail.Uom.Required}"
                            ValidationGroup="vgAdd" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.FlowDetail.UnitCount}">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitCount" runat="server" Text='<%# Bind("UnitCount","{0:0.########}") %>' />
                        <asp:TextBox ID="tbUnitCount" runat="server" CssClass="inputRequired" Visible="false"
                            Width="50" />
                        <asp:RequiredFieldValidator ID="rfvUC" runat="server" ErrorMessage="${Inventory.PrintHu.FlowDetail.UnitCount.Required}"
                            Display="Dynamic" ControlToValidate="tbUnitCount" ValidationGroup="vgAdd" Enabled="false" />
                        <asp:RangeValidator ID="rvUC" ControlToValidate="tbUnitCount" runat="server" Display="Dynamic"
                            ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="999999999" MinimumValue="0.00000001"
                            Type="Double" ValidationGroup="vgAdd" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.FlowDetail.HuLotSize}">
                    <ItemTemplate>
                        <asp:Label ID="lblHuLotSize" runat="server" Text='<%# Bind("HuLotSize","{0:0.########}") %>' />
                        <asp:TextBox ID="tbHuLotSize" runat="server" CssClass="inputRequired" Visible="false"
                            Width="50" />
                        <asp:RangeValidator ID="rvHuLotSize" ControlToValidate="tbHuLotSize" runat="server"
                            Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="999999999"
                            MinimumValue="0.00000001" Type="Double" ValidationGroup="vgAdd" Enabled="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.FlowDetail.PackageType}">
                    <ItemTemplate>
                        <asp:Label ID="lblPackageType" runat="server" Text='<%#Bind("PackageType") %>' />
                        <cc1:CodeMstrDropDownList ID="ddlPackageType" runat="server" Code="PackageType" IncludeBlankOption="true"
                            Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.FlowDetail.LotNo}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbLotNo" runat="server" onmouseup="if(!readOnly)select();" Text='<%# Bind("HuLotNo") %>'
                            Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Inventory.PrintHu.FlowDetail.OrderQty}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbOrderQty" runat="server" onmouseup="if(!readOnly)select();" Text='<%# Bind("OrderedQty","{0:0.########}") %>'
                            Width="50"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.GridView.Action}" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnAdd" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>'
                            Text="${Common.Button.New}" OnClick="lbtnAdd_Click" Visible="false" ValidationGroup="vgAdd">
                        </asp:LinkButton>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>'
                            Text="${Common.Button.Delete}" OnClick="lbtnDelete_Click" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')"
                            Visible="false">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="tablefooter">
        <asp:Button ID="btnPrint" runat="server" Text="${Common.Button.Print}" OnClick="btnPrint_Click"
            CssClass="button2" ValidationGroup="vgPrint" />
    </div>
</fieldset>
