<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Reports_Inv" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="ttd01">
                <asp:Label ID="lblTRoute" runat="server" Text="库位"></asp:Label>
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbLocation" runat="server" Visible="true" DescField="Name" Width="280"
                    ValueField="Code" ServicePath="LocationMgr.service" ServiceMethod="GetLocationByUserCode"
                    MustMatch="false" />
            </td>
            <td class="ttd01">
                <asp:Label ID="lblIpNo" runat="server" Text="Item"></asp:Label>
            </td>
            <td class="ttd02">
                <uc3:textbox ID="tbItem" runat="server" Visible="true" DescField="Description" ImageUrlField="ImageUrl"
                    Width="280" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem"
                    MustMatch="false" />
                <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
<%--        <tr>
            <td class="ttd01">
                <asp:Label ID="lblCreateDate" runat="server" Text="发货日期"></asp:Label>
            </td>
            <td class="ttd02">
                <asp:TextBox ID="txtCreateDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
            </td>
            <td class="ttd01">
                <asp:Label ID="lblOrderNo" runat="server" Text="运单号"></asp:Label>
            </td>
            <td class="ttd02">
                <asp:TextBox ID="txtOrderNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Label ID="lblStatus" runat="server" Text="状态"></asp:Label>
            </td>
            <td class="td02">
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="all">全部</asp:ListItem>
                    <asp:ListItem Value="empty">未生成运单</asp:ListItem>
                    <asp:ListItem>Create</asp:ListItem>
                    <asp:ListItem>In-Process</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="td01">
                <asp:Label ID="lblFlow" runat="server" Text="发货路线"></asp:Label>
            </td>
            <td class="td02">
                <asp:TextBox ID="txtFlow" runat="server"></asp:TextBox>
            </td>
        </tr>--%>
        <tr>
            <td colspan="3" />
            <td class="ttd02">
                <div class="buttons">
                    <asp:Button ID="btnTransportationOrderQuery" runat="server" OnClick="btn1_Click"
                        Text="查询" />
                    <asp:Button ID="btnClear" runat="server" Text="清除" OnClick="btnClear_Click" />
                    <asp:Button ID="btnExport" runat="server" Text="导出" OnClick="btnExport_Click" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
<fieldset>
    <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" CellMaxLength="20"
        EmptyDataText="无记录！" AllowPaging="True" Width="100%" 
        OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20">
        <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" HeaderStyle-HorizontalAlign="Center"
                ItemStyle-Width="30">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1%>
                </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
            </asp:TemplateField>
            <asp:BoundField DataField="site" HeaderText="Site" />
            <asp:BoundField DataField="item" HeaderText="Item" />
            <asp:BoundField DataField="desc1" HeaderText="Desc1" />
            <asp:BoundField DataField="Uom" HeaderText="Uom" />
            <asp:BoundField DataField="Location" HeaderText="Location" />
            <asp:BoundField DataField="Bin" HeaderText="Bin" />
            <asp:BoundField DataField="LotNo" HeaderText="LotNo" />
            <asp:BoundField DataField="Qty" HeaderText="QTY" />
        </Columns>
    </asp:GridView>
</fieldset>
