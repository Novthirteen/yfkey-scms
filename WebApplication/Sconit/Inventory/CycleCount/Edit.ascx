<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Inventory_CycleCount_Edit" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<tr>
    <td class="td01">
        <asp:Literal ID="lblOrderNo" runat="server" Text="${Common.Business.OrderNo}:" />
    </td>
    <td class="td02">
        <asp:TextBox ID="tbOrderNo" runat="server" ReadOnly="true" />
    </td>
    <td class="td01">
        <asp:Literal ID="lblStatus" runat="server" Text="${Common.CodeMaster.Status}:" />
    </td>
    <td class="td02">
        <asp:TextBox ID="tbStatus" runat="server" ReadOnly="true" />
    </td>
</tr>
<tr>
    <td class="td01">
        <asp:Literal ID="lblRegion" runat="server" Text="${Common.Business.Region}:" />
    </td>
    <td class="td02">
        <asp:TextBox ID="tbRegion" runat="server" ReadOnly="true" />
    </td>
    <td class="td01">
        <asp:Literal ID="lblLocation" runat="server" Text="${Common.Business.Location}:" />
    </td>
    <td class="td02">
        <asp:TextBox ID="tbLocation" runat="server" ReadOnly="true" />
    </td>
</tr>
<tr>
    <td class="td01">
        <asp:Literal ID="lblEffDate" runat="server" Text="${Common.Business.EffDate}:" />
    </td>
    <td class="td02">
        <asp:TextBox ID="tbEffDate" runat="server" ReadOnly="true" Text='<%# Bind("EffDate","{0:yyyy-MM-dd}") %>' />
    </td>
    <td class="td01">
        <asp:Literal ID="lblType" runat="server" Text="${Common.Business.Type}:" />
    </td>
    <td class="td02">
        <cc1:CodeMstrLabel ID="ddlType" Code="PhysicalCountType" runat="server" Value='<%#Bind("Type") %>' />
    </td>
</tr>
<tr>
    <td class="td01">
        <asp:Literal ID="lblLastModifyDate" runat="server" Text="${Common.Business.LastModifyDate}:" />
    </td>
    <td class="td02">
        <asp:TextBox ID="tbLastModifyDate" runat="server" ReadOnly="true" Text='<%# Bind("LastModifyDate","{0:yyyy-MM-dd HH:mm}") %>' />
    </td>
    <td class="td01">
        <asp:Literal ID="lblLastModifyUser" runat="server" Text="${Common.Business.LastModifyUser}:" />
    </td>
    <td class="td02">
        <asp:TextBox ID="tbLastModifyUser" runat="server" ReadOnly="true" />
    </td>
</tr>
