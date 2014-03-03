<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Transportation_TransportationOrderQuery_Main" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="ttd01">
                <asp:Label ID="lblTRoute" runat="server" Text="运输路线"></asp:Label>
            </td>
            <td class="ttd02">
                <uc3:textbox ID="txtTRoute" runat="server" Width="250" DescField="Description" ValueField="Code"
                    MustMatch="true" ServiceMethod="GetAllTransportationRoute" ServicePath="TransportationRouteMgr.service" />
            </td>
            <td class="ttd01">
                <asp:Label ID="lblIpNo" runat="server" Text="ASN"></asp:Label>
            </td>
            <td class="ttd02">
                <asp:TextBox ID="txtIpNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
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
        </tr>
        <tr>
            <td colspan="3" />
            <td class="ttd02">
                <div class="buttons">
                    <asp:Button ID="btnTransportationOrderQuery" runat="server" OnClick="btn1_Click" Text="查询" />
                    <asp:Button ID="btnClear" runat="server" Text="清除" OnClick="btnClear_Click" />
                    <asp:Button ID="btnExport" runat="server" Text="导出" 
                        onclick="btnExport_Click"  />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
<fieldset>
<asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" CellMaxLength="10" EmptyDataText="无记录！"
    AllowPaging="True" Width="100%" OnPageIndexChanging="GridView1_PageIndexChanging">
    <Columns>
        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="30">
        <ItemTemplate >
        <%# Container.DataItemIndex + 1%>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="troute" HeaderText="运输路线" />
        <asp:BoundField DataField="ipno" HeaderText="ASN" />
        <asp:BoundField DataField="createdate" HeaderText="发货日期" />
        <asp:BoundField DataField="orderno" HeaderText="运单号" />
        <asp:BoundField DataField="status" HeaderText="状态" />
        <asp:BoundField DataField="flow" HeaderText="发货路线" />
    </Columns>
</asp:GridView>
</fieldset>