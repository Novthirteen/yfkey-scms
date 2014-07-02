<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Inventory_UnWHQuery_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ac1" %>
<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblOrderNo" runat="server" Text="${Menu.Location}:" />
            </td>
            <td class="td02">
               <%-- <asp:TextBox ID="tbLocation" runat="server" />--%>
                <asp:DropDownList ID="tbLocation" runat="server">
                <asp:ListItem Value="" Text="ALL" ></asp:ListItem>
                <asp:ListItem Value="M111S"> </asp:ListItem>
                 <asp:ListItem Value="M222S"> </asp:ListItem>
                  <asp:ListItem Value="M333S"> </asp:ListItem>
                   <asp:ListItem Value="KQF01"> </asp:ListItem> 
                   <asp:ListItem Value="M301S"> </asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="td01">
                <asp:Literal ID="Literal1" runat="server" Text="${Reports.ActBill.ItemCode}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItemCode" runat="server" Visible="true" DescField="Description" ImageUrlField="ImageUrl"
                    Width="280" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblParty" runat="server" Text="条形码" />
            </td>
            <td class="td02">
               <asp:TextBox ID="tbHuId" runat="server" /> 
            </td>   
            <td class="td01">
                <asp:Literal ID="lblItem" runat="server" Text="${MasterData.Receipt.LotNo}:" />
            </td>
            <td class="td02">
                  <asp:TextBox ID="tbLotNo" runat="server" /> 
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="lblEffectiveDateFrom" runat="server" Text="${MasterData.Flow.FlowDetail.StartDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEffectiveDateFrom" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
               <%-- <ac1:CalendarExtender ID="CalendarExtender2" TargetControlID="tbEffectiveDateFrom" Format="yyyy-MM-dd hh:mm:ss"
                    runat="server">
                </ac1:CalendarExtender>--%>
            </td>
            <td class="td01">
                <asp:Literal ID="lblEffectiveDateTo" runat="server" Text="${MasterData.Flow.FlowDetail.EndDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEffectiveDateTo" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" />
               <%-- <ac1:CalendarExtender ID="CalendarExtender1" TargetControlID="tbEffectiveDateTo" Format="yyyy-MM-dd hh:mm:ss"
                    runat="server">
                </ac1:CalendarExtender>--%>
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="t02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" CssClass="button2"
                    OnClick="btnSearch_Click" />
                <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" CssClass="button2"
                    OnClick="btnExport_Click" />
             <asp:Button ID="btnClear" runat="server" Text="清除" 
                    CssClass="button2" onclick="btnClear_Click"
                     />
            </td>
        </tr>
        
    </table>
</fieldset>