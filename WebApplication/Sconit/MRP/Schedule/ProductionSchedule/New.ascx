<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MRP_Schedule_ProductionSchedule_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblFlow" runat="server" Text="${Common.Business.Flow}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbFlow" runat="server" DescField="Description" ValueField="Code"
                    ServicePath="FlowMgr.service" MustMatch="true" Width="250"
                    ServiceMethod="GetMRPFlow" />
                <asp:TextBox ID="tbFlow1" runat="server" Visible="false" ReadOnly="true" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblScheduleNo" runat="server" Text="${MRP.Schedule.ProductionSchedule}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbScheduleNo" runat="server" CssClass="inputRequired" />
                <asp:RequiredFieldValidator ID="rfvScheduleNo" runat="server" ErrorMessage="${MRP.Schedule.ProductionSchedule}${Common.Business.Error.Empty}"
                    ControlToValidate="tbScheduleNo" Display="Dynamic" ValidationGroup="vgSave" />
            </td>
        </tr>
        
        <tr id="trTemplate" runat="server">
            <td class="td01">
                <asp:Literal ID="ltlSelect" runat="server" Text="${Common.FileUpload.PleaseSelect}:"></asp:Literal>
            </td>
            <td class="td02">
                <asp:FileUpload ID="fileUpload" ContentEditable="false" runat="server" />
                <asp:Button ID="btnImport" runat="server" Text="${Common.Button.Import}" OnClick="btnImport_Click"
                    ValidationGroup="vgImport" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlTemplate" runat="server" Text="${Common.Business.Template}:" />
            </td>
            <td class="td02">
                <asp:HyperLink ID="hlTemplate" runat="server" Text="${Common.Business.ClickToDownload}" 
                    NavigateUrl="~/Reports/Templates/ImportTemplates/ProductionSchedule.xls"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
            <td class="td01">
            </td>
            <td class="td02">
                <div class="buttons">
                    <asp:Button ID="btnSave" runat="server" Text="${Common.Button.Save}" OnClick="btnSave_Click"
                        Visible="false" ValidationGroup="vgSave" />
                    <asp:Button ID="btnRelease" runat="server" Text="${Common.Button.Submit}" OnClick="btnRelease_Click"
                        Visible="false" />
                    <asp:Button ID="btnDelete" runat="server" Text="${Common.Button.Delete}" OnClick="btnDelete_Click"
                        Visible="false" />
                    <asp:Button ID="btnCancel" runat="server" Text="${Common.Button.Cancel}" OnClick="btnCancel_Click"
                        Visible="false" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
<div id="div_List_Detail" runat="server">
    <fieldset>
        <legend>
            <asp:Literal ID="ltllegend" runat="server" />
        </legend>
        <asp:GridView ID="GV_List_Detail" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_List_Detail_RowDataBound"
            CellPadding="0">
            <Columns>
                <asp:TemplateField HeaderText="${MRP.Schedule.Seq}">
                    <ItemTemplate>
                        <asp:Label ID="ltlSequence" runat="server" Text='<%# (Container as GridViewRow).RowIndex+1 %> ' />
                        <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("Id")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Type}">
                    <ItemTemplate>
                        <asp:Literal ID="ltlType" runat="server" Text='<%# Eval("Type")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MRP.Schedule.ShipDate}">
                    <ItemTemplate>
                        <asp:Literal ID="ltlStartTime" runat="server" Text='<%# Eval("StartTime","{0:yyyy-MM-dd}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="${MRP.Schedule.WinDate}" DataField="DateFrom" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:TemplateField HeaderText="${MRP.Schedule.Item}">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item")%>' ToolTip='<%# Eval("ItemDescription")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="${MRP.Schedule.ItemDescription}" DataField="ItemDescription" />
                <asp:BoundField HeaderText="${Common.Business.RefCode}" DataField="ItemReference" />
                <asp:TemplateField HeaderText="${Common.Business.Uom}">
                    <ItemTemplate>
                        <asp:Literal ID="ltlUom" runat="server" Text='<%# Eval("Uom")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MRP.Schedule.UnitCount}">
                    <ItemTemplate>
                        <asp:Literal ID="ltlUnitCount" runat="server" Text='<%# Eval("UnitCount","{0:0.##}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MRP.Schedule.Location}">
                    <ItemTemplate>
                        <asp:Literal ID="ltlLocation" runat="server" Text='<%# Eval("Location")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Qty}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbQty" runat="server" Width="50" Text='<%# Bind("Qty","{0:0.##}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </fieldset>
</div>
