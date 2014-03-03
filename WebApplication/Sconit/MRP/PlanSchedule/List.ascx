<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="MRP_PlanSchedule_List" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript" src="../../Js/boxover.js"></script>

<fieldset>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" DataKeyNames="ItemFlowPlanId" AutoGenerateColumns="false"
            OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:BoundField DataField="StaCol_0" HeaderText="Item">
                    <HeaderStyle Wrap="false" />
                    <ItemStyle Wrap="false" />
                </asp:BoundField>
                <asp:BoundField DataField="StaCol_1" HeaderText="ItemDesc">
                    <HeaderStyle Wrap="false" />
                    <ItemStyle Wrap="false" />
                </asp:BoundField>
                <asp:BoundField DataField="StaCol_2" HeaderText="Total">
                    <HeaderStyle Wrap="false" />
                    <ItemStyle Wrap="false" />
                </asp:BoundField>
                <asp:BoundField DataField="StaCol_3" HeaderText="Date">
                    <HeaderStyle Wrap="false" />
                    <ItemStyle Wrap="false" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
</fieldset>
