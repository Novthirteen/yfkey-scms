<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="NewMrp_PlanVersionContrast_Main" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <table class="mtable" runat="server" id="tblImport">
        <tr>
            <td class="td01">
                选择类型:
            </td>
            <td class="td02">
            <%--<asp:RadioButtonList ID="rblAction" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                    CssClass="floatright" OnSelectedIndexChanged="rblAction_SelectedIndexChanged">
                    <asp:ListItem Text="查询" Value="Search" Selected="True" />
                    <asp:ListItem Text="导入" Value="Import" />
                </asp:RadioButtonList>--%>
               <asp:RadioButtonList ID="rblPlanType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                OnSelectedIndexChanged="rblAction_SelectedIndexChanged">
                    <asp:ListItem Text="发运计划" Value="ShipPlan" Selected="True" />
                    <asp:ListItem Text="生产计划" Value="ProductionPlan" />
                    <asp:ListItem Text="采购计划" Value="PurchasePlan" />
                    <asp:ListItem Text="采购发货计划" Value="PurchasePlan2" />
                </asp:RadioButtonList>
            </td>
            <td  class="td01"></td>
            <td class="td02"></td>
        </tr>
         <tr>
            <td class="td01">
                版本1
            </td>
            <td class="td02">
               <%--<asp:TextBox ID="tbVersion1" runat="server" Width="100" />--%>
               <select id="versionSelect1" runat="server" >
                                <option selected="selected" value=""></option>
                            </select>
            </td>
            <td class="td01">
                版本2
            </td>
            <td class="td02">
               <%--<asp:TextBox ID="tbVersion2" runat="server" Width="100" />--%>
               <select id="versionSelect2" runat="server" >
                                <option selected="selected" value=""></option>
                            </select>
            </td>
        </tr> 
        <tr>
            <td class="td01">
                路线:
            </td>
            <td class="td02">
               <textarea id="tbFlow" rows="2" runat="server"  style="width:200" ></textarea>
            </td>
            <td class="td01">
                物料代码
            </td>
            <td class="td02">
                <uc3:textbox ID="tbItemCode" runat="server" Visible="true" Width="250" MustMatch="true"
                    DescField="Description" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem" />
            </td>
        </tr>
        <tr>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
            <td class="td01">
               
            </td>
            <td class="td02">
              <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click" />
            </td>
        </tr>
    </table>
    <div id="Resultlist" runat="server">
    </div>
</fieldset>
