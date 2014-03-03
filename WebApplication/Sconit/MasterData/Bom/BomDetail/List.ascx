<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MasterData_Bom_BomDetail_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<fieldset>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Operation" HeaderText="${Common.Business.Op}" SortExpression="Operation" />
                <asp:BoundField DataField="Reference" HeaderText="${MasterData.Bom.Reference}" SortExpression="Reference" />
                <asp:TemplateField HeaderText="${MasterData.Bom.ParCode}" SortExpression="Bom.Code">
                    <ItemTemplate>
                        <asp:Label ID="GV_lbParCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Bom.Code")%>'
                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "Bom.Description")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Uom}" SortExpression="Bom.Uom.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Bom.Uom.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Bom.CompCode}" SortExpression="Item.Code">
                    <ItemTemplate>
                        <asp:Label ID="GV_lbCompCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Item.Code")%>'
                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "Item.Description")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Uom}" SortExpression="Uom.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Uom.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="StructureType" HeaderText="${MasterData.Bom.StructureType}"
                    SortExpression="StructureType" />
                <asp:BoundField DataField="StartDate" HeaderText="${Common.Business.StartTime}" SortExpression="StartDate"
                    DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:BoundField DataField="EndDate" HeaderText="${Common.Business.EndTime}" SortExpression="EndDate"
                    DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                <asp:BoundField DataField="RateQty" HeaderText="${MasterData.Bom.RateQty}" SortExpression="RateQty"
                    DataFormatString="{0:0.########}" />
                <asp:BoundField DataField="ScrapPercentage" HeaderText="${MasterData.Bom.ScrapPercentage}"
                    SortExpression="ScrapPercentage" DataFormatString="{0:0.########}" />
                <asp:TemplateField HeaderText="${MasterData.BomDetail.BackFlushMethod}" SortExpression="BackFlushMethod">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblBackFlushMethod" runat="server" Code="BackFlushMethod"
                            Value='<%# Bind("BackFlushMethod") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Flow.IsShipScanHu}" SortExpression="IsShipScanHu">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblIsShipScanHu" runat="server" Code="TrueOrFalse" Value='<%# Bind("IsShipScanHu") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CheckBoxField DataField="NeedPrint" HeaderText="${MasterData.Bom.NeedPrint}"
                    SortExpression="NeedPrint" />
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                       <cc1:LinkButton  ID="lbtnEdit" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.Edit}" OnClick="lbtnEdit_Click" FunctionId="EditBomDetail" CssClass="button2"/>
                        <cc1:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                            Text="${Common.Button.Delete}" OnClick="lbtnDelete_Click" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')"
                            FunctionId="EditBomDetail" CssClass="button2"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
</fieldset>
