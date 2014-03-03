<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Visualization_Traceability_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="sc1" %>
<%@ Register Src="View.ascx" TagName="View" TagPrefix="uc2" %>
<fieldset>
    <legend>${Visualization.GoodsTraceability.TransList}</legend>
    <div class="GridView">
        <sc1:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            SkinID="GV" AllowMultiColumnSorting="false" AutoLoadStyle="false" SeqNo="0" seqtext="No."
            ShowSeqNo="true" AllowSorting="True" AllowPaging="True" PagerID="gp" Width="100%"
            CellMaxLength="10" TypeName="com.Sconit.Web.CriteriaMgrProxy" SelectMethod="FindAll"
            SelectCountMethod="FindCount" OnRowDataBound="GV_List_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="${Visualization.GoodsTraceability.TransType}" SortExpression="TransType">
                    <ItemTemplate>
                        <asp:Label ID="lblTransType" runat="server" Text='<%# Eval("TransactionType")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.ReferenceOrderNo}">
                    <ItemTemplate>
                        <uc2:View ID="ucView" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Uom}" SortExpression="Uom">
                    <ItemTemplate>
                        <asp:Label ID="lblUom" runat="server" Text='<%# Eval("Uom")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Qty}" SortExpression="Qty">
                    <ItemTemplate>
                        <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty","{0:0.########}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="${Common.Business.Supplier}" SortExpression="PartyFromName">
                    <ItemTemplate>
                        <asp:Label ID="lblSupplier" runat="server" Text='<%# Eval("PartyFromName")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.Customer}" SortExpression="PartyToName">
                    <ItemTemplate>
                        <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("PartyToName")%>' />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="${Common.Business.Location}" SortExpression="Location">
                    <ItemTemplate>
                        <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Location.Name}" SortExpression="Location.Name">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationName" runat="server" Text='<%# Eval("LocationName")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${MasterData.Location.Bin.Code}" SortExpression="StorageBin">
                    <ItemTemplate>
                        <asp:Label ID="lblStorageBin" runat="server" Text='<%# Eval("StorageBin")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.EffDate}" SortExpression="EffectiveDate">
                    <ItemTemplate>
                        <asp:Label ID="lblEffectiveDate" runat="server" Text='<%# Eval("EffectiveDate","{0:yyyy-MM-dd}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.CreateUser}" SortExpression="CreateUser">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateUser" runat="server" Text='<%# Eval("CreateUser")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="${Common.Business.CreateDate}" SortExpression="CreateDate">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# Eval("CreateDate","{0:yyyy-MM-dd HH:mm}")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </sc1:GridView>
        <sc1:GridPager ID="gp" runat="server" GridViewID="GV_List" PageSize="10">
        </sc1:GridPager>
    </div>
</fieldset>
