<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="NewMrp_PurchasePlan_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="ASTreeView" Namespace="Geekees.Common.Controls" TagPrefix="ct" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>


<fieldset>
    <table class="mtable">
         <tr>
            <td class="td01">
                版本号
            </td>
            <td class="td02">
                <asp:TextBox ID="tbReleaseNo" runat="server" Width="100" />
            </td>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
        </tr> 
         <tr>
            <td class="td01">
                创建日期 从
            </td>
            <td class="td02">
                <asp:TextBox ID="tbCreateStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})"
                    Width="100" />
            </td>
            <td class="td01">
                至
            </td>
            <td class="td02">
                <asp:TextBox ID="tbCreateEndDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})"
                    Width="100" />
            </td>
        </tr>
        <tr>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
            <td />
            <td class="td02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnExport" runat="server" Text="${Common.Button.Export}" style="display:none" OnClick="btnExport_Click" />
                    <%--<asp:Button ID="btReplace" Text="" runat="server"   OnClick="btnReplace_Click"   Style="display: none" />--%>
                <%--<asp:Button ID="btmSubmit" runat="server" Text="释放" OnClick="btnSubmit_Click" />--%>
                    <input type="hidden" id="btControl_Num" runat="server" Style="display: none"  />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
