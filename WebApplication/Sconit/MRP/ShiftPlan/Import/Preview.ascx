<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Preview.ascx.cs" Inherits="MRP_ShiftPlan_Import_Preview" %>
<%@ Register Src="PreviewDetail.ascx" TagName="Detail" TagPrefix="uc" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <legend>${Import.PSModel.OrderPreview}</legend>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="${Common.Business.Flow}">
                    <ItemTemplate>
                        <asp:Label ID="lblFlow" runat="server" Text='<%# Eval("Flow")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
              <%--  <asp:TemplateField HeaderText="${Common.Business.Description}">
                    <ItemTemplate>
                        <asp:Label ID="lblFlowDescription" runat="server" Text='<%# Eval("Flow.Description")%>' />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <%-- <asp:TemplateField HeaderText="${MasterData.WorkCalendar.Shift}">
                    <ItemTemplate>
                        <asp:Label ID="lblShift" runat="server" Text='<%# Eval("Shift.ShiftName")%>' />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.StartTime}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbStartTime" runat="server" Text='<%# Eval("StartTime","{0:yyyy-MM-dd HH:mm}")%>'
                            onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm',isShowWeek:true})" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.WindowTime}">
                    <ItemTemplate>
                        <asp:TextBox ID="tbWindowTime" runat="server" Text='<%# Eval("WindowTime","{0:yyyy-MM-dd HH:mm}")%>'
                            onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm',isShowWeek:true})" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <uc:Detail ID="ucDetail" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="tablefooter buttons">
        <asp:Button ID="btnCreate" runat="server" Text="${Common.Button.Create}" OnClick="btnCreate_Click"
            CssClass="add" />
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="back" />
    </div>
</fieldset>
