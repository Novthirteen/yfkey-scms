<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MRP_ShiftPlan_Manual_List" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc" %>
<fieldset>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="FlowCode" HeaderText="${Common.Business.Flow}" />
                <asp:BoundField DataField="FlowDesc" HeaderText="${Common.Business.Description}" />
                <asp:BoundField DataField="ItemCode" HeaderText="${Common.Business.ItemCode}" />
                <asp:BoundField DataField="ItemDesc" HeaderText="${Common.Business.ItemDescription}" />
                <asp:BoundField DataField="Uom" HeaderText="${Common.Business.Uom}" />
                <asp:BoundField DataField="TotalPlanQty" HeaderText="${MRP.PlanViewType.TotalPlan}"
                    DataFormatString="{0:0.########}" />
                <asp:TemplateField HeaderText="DynCol_0" Visible="true">
                    <ItemTemplate>
                        <uc:Edit ID="ucEdit_0" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DynCol_1" Visible="true">
                    <ItemTemplate>
                        <uc:Edit ID="ucEdit_1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DynCol_2" Visible="true">
                    <ItemTemplate>
                        <uc:Edit ID="ucEdit_2" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DynCol_3" Visible="true">
                    <ItemTemplate>
                        <uc:Edit ID="ucEdit_3" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DynCol_4" Visible="true">
                    <ItemTemplate>
                        <uc:Edit ID="ucEdit_4" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
