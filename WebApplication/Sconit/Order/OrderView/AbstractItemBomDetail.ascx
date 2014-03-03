<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AbstractItemBomDetail.ascx.cs"
    Inherits="Order_OrderView_AbstractItemBomDetail" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">

function radioButtonClick(obj)
{
    $("#locTransList input:radio").each(function(index,domEle){ 
        if(this.type=="radio")
        this.checked=false;
    });  
    $(obj).attr("checked",true);
}

</script>

<div id="floatdiv">
    <fieldset>
        <legend>明细</legend>
        <div class="GridView" id="detailView">
            <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                AllowMultiColumnSorting="false" AutoLoadStyle="false" ShowSeqNo="true" AllowSorting="false"
                AllowPaging="True" Width="100%" OnRowDataBound="GV_List_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <ItemTemplate>
                            <div id="locTransList">
                                <asp:RadioButton ID="rbSelect" runat="server" onclick="radioButtonClick(this);"  />
                                <asp:HiddenField ID="hfRowIndex" runat="server" Value='<%# DataBinder.Eval(Container,"RowIndex") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.Operation}" SortExpression="Operation">
                        <ItemTemplate>
                            <asp:Label ID="lbOperation" runat="server" Text='<%# Bind("Operation") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.ItemCode}" SortExpression="Item.Code">
                        <ItemTemplate>
                            <asp:Label ID="lbItem" runat="server" Text='<%# Bind("Item.Code") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.IsAssemble}" SortExpression="IsAssemble">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbIsAssemble" runat="server" Checked='<%# Bind("IsAssemble") %>'
                                Visible="false" AutoPostBack="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.Uom}" SortExpression="Uom.Code">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Uom.Code")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="${MasterData.Order.LocTrans.OrderdQty}" DataField="OrderedQty"
                        DataFormatString="{0:0.########}" />
                    <asp:BoundField HeaderText="${MasterData.Order.LocTrans.AccumulateQty}" DataField="AccumulateQty"
                        DataFormatString="{0:0.########}" />
                    <asp:BoundField HeaderText="${MasterData.Order.LocTrans.AccumulateRejectQty}" DataField="AccumulateRejectQty"
                        DataFormatString="{0:0.########}" />
                    <asp:BoundField HeaderText="${MasterData.Order.LocTrans.AccumulateScrapQty}" DataField="AccumulateScrapQty"
                        DataFormatString="{0:0.########}" />
                    <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.Location}" SortExpression="Location.Code">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Location.Code")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.RejectLocation}" SortExpression="RejectLocation.Code">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "RejectLocation.Code")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="${MasterData.Order.LocTrans.TransactionType}" DataField="TransactionType" />
                    <asp:TemplateField HeaderText="${MasterData.Order.LocTrans.HuOption}">
                        <ItemTemplate>
                            <cc1:CodeMstrLabel ID="lblHuOption" runat="server" Code="HuOption" Value='<%# Bind("HuOption") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CheckBoxField HeaderText="${MasterData.Order.LocTrans.NeedPrint}" DataField="NeedPrint" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="tablefooter">
            <asp:Button ID="Button1" runat="server" Text="${Common.Button.Save}" OnClick="btnSave_Click"
                CssClass="button2" />
            <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                CssClass="button2" />
        </div>
    </fieldset>
</div>
