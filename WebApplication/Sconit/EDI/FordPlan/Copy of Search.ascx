<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Copy of Search.ascx.cs" Inherits="EDI_FordPlan_Search" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="ASTreeView" Namespace="Geekees.Common.Controls" TagPrefix="ct" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>
<script type="text/javascript">
    $(function () {

        if ($("input[type=radio]:checked").val() == "D") {
            $("#controlNumDaily").show();
            $("#controlNumWeekly").hide();
        } else {
            $("#controlNumDaily").hide();
            $("#controlNumWeekly").show();
        }
        $("input[type=radio]").click(function () {
            if ($(this).val() == "D") {
                $("#controlNumDaily").show();
                $("#controlNumWeekly").hide();
            } else {
                $("#controlNumDaily").hide();
                $("#controlNumWeekly").show();
            }
        });

    });
   
</script>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblControl_Num" runat="server" Text="${EDI.EDIFordPlanBase.Control_Num}:" />
            </td>
            <td class="td02">

            <div id="controlNumDaily">
             <ct:ASDropDownTreeView ID="controlNumD" runat="server" BasePath="~/Js/astreeview/"
                    DataTableRootNodeValue="0" EnableRoot="false" EnableNodeSelection="false" EnableCheckbox="true"
                    EnableDragDrop="false" EnableTreeLines="false" EnableNodeIcon="false" EnableCustomizedNodeIcon="false"
                    EnableDebugMode="false" EnableRequiredValidator="false" InitialDropdownText=""
                    Width="170" EnableCloseOnOutsideClick="true" EnableHalfCheckedAsChecked="true"
                    DropdownIconDown="~/Js/astreeview/images/windropdown.gif" EnableContextMenuAdd="false"
                    MaxDropdownHeight="200" />
                    </div>
            <div id="controlNumWeekly">
                <ct:ASDropDownTreeView ID="controlNumM" runat="server" BasePath="~/Js/astreeview/"
                    DataTableRootNodeValue="0" EnableRoot="false" EnableNodeSelection="false" EnableCheckbox="true"
                    EnableDragDrop="false" EnableTreeLines="false" EnableNodeIcon="false" EnableCustomizedNodeIcon="false"
                    EnableDebugMode="false" EnableRequiredValidator="false" InitialDropdownText=""
                    Width="170" EnableCloseOnOutsideClick="true" EnableHalfCheckedAsChecked="true"
                    DropdownIconDown="~/Js/astreeview/images/windropdown.gif" EnableContextMenuAdd="false"
                    MaxDropdownHeight="200" />
                    </div>

                
                <%--<asp:TextBox ID="Control_Num" runat="server" Visible="true" />--%>
            </td>
             <td class="td01">
                <asp:Literal ID="ltlItem" runat="server" Text="${EDI.EDIFordPlanBase.Item}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItem" runat="server" Visible="true" DescField="Description" ImageUrlField="ImageUrl"
                    Width="280" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />
            </td>
        </tr>
        <%--<tr>
            <td class="td01">
                <asp:Literal ID="lblStartDate" runat="server" Text="${Common.Business.StartDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})"
                    Width="100" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlEndDate" runat="server" Text="${Common.Business.EndDate}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEndDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd',isShowWeek:true})"
                    Width="100" />
            </td>
        </tr>--%>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlType" runat="server" Text="${EDI.EDIFordPlanBase.Type}:" />
            </td>
            <td class="td02">
               <%--<input type="radio" name="types" checked="checked" value="D" /> 天计划 &nbsp
               <input type="radio" name="types" value="W','M"  /> 周计划--%>
               <asp:RadioButtonList ID="rblListFormat" runat="server" RepeatDirection="Horizontal"   >
                    <asp:ListItem Text="天计划" Value="D" Selected="True" />
                    <asp:ListItem Text="周计划" Value="W','M" />
                </asp:RadioButtonList>
            </td>
            <td />
            <td class="td02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
