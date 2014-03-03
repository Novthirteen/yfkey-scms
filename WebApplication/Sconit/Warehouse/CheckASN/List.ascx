<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Warehouse_CheckASN_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset><legend>明细</legend>
    <div class="GridView">
        <cc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False"  
            AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" SeqText="No."
            ShowSeqNo="true"  AllowPaging="True" PagerID="gp" Width="100%" OnRowDataBound="GV_List_RowDataBound"
            CellMaxLength="20" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount"  DefaultSortExpression="CreateDate"
            DefaultSortDirection="Descending">
            <Columns>
                <asp:BoundField DataField="IpNo" HeaderText="${InProcessLocation.IpNo}" SortExpression="IpNo" />
            
               
                <asp:TemplateField HeaderText="${InProcessLocation.PartyTo}" SortExpression="PartyTo">
                    <ItemTemplate>
                        <asp:Label ID="PartyToName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PartyTo.Name")%>'
                            ToolTip='<%# DataBinder.Eval(Container.DataItem, "ShipTo.Address")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
                 
                <asp:TemplateField HeaderText="${InProcessLocation.Status}">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblStatus" runat="server" Code="Status" Value='<%# Bind("Status") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreateDate" HeaderText="${InProcessLocation.CreateDate}"
                    SortExpression="CreateDate" />
                <asp:TemplateField HeaderText="${Common.Business.CreateUser}" SortExpression="CreateUser.FirstName">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "CreateUser.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.GridView.Action}">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IpNo") %>'
                            Text="确认" OnClick="lbtnView_Click" >
                        </asp:LinkButton>
                         <asp:Label Text="已确认" runat="server" ID="ShowConFirm" Visible="false" /> 
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:GridView>
        <cc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </cc1:GridPager>
    </div>
  
</fieldset>
