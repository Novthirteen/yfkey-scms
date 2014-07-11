<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="NewMrp_MrpParameterSetup_Main" %>
<fieldset>
<table class="mtable" runat="server" id="tblImport" >
        <tr>
            <td class="td01">
                导入参数类型:
            </td>
            <td class="td02">
            
                            <asp:RadioButtonList ID="rblDateType" runat="server" RepeatDirection="Horizontal"
                                CssClass="floatright">
                                <asp:ListItem Text="发运计划" Value="ShipPlan" Selected="True" />
                                <asp:ListItem Text="生产计划" Value="ProductionPlan" />
                                <asp:ListItem Text="物料需求计划" Value="PurchasePlan" />
                            </asp:RadioButtonList>
                       
                            <asp:HyperLink ID="hlTemplate1" runat="server" Text="发运计划" NavigateUrl="~/Reports/Templates/MRP/发运计划参数导入模板.xls" />
                            <asp:HyperLink ID="hlTemplate2" runat="server" Text="生产计划" NavigateUrl="~/Reports/Templates/MRP/生产计划参数导入模板.xls" />
                            <asp:HyperLink ID="hlTemplate3" runat="server" Text="物料需求计划" NavigateUrl="~/Reports/Templates/MRP/物料需求计划参数导入模板.xls" />
                   
            </td>
            <td class="td01">
                请选择文件:
            </td>
            <td class="td02">
                <asp:FileUpload ID="fileUpload" ContentEditable="false" runat="server" />
                <%--<cc1:Button ID="btnImport" runat="server" Text="导入" OnClick="btnImport_Click" />--%>
                <asp:Button ID="btnImport" runat="server" Text="${Common.Button.Import}" OnClick="btnImport_Click"
                    CssClass="apply" />
            </td>
        </tr>
    </table>
</fieldset>

