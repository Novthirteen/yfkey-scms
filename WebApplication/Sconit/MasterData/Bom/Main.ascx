<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MasterData_Bom_Main" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="./BomView/Main.ascx" TagName="BomViewMain" TagPrefix="uc2" %>
<%@ Register Src="./Bom/Main.ascx" TagName="BomMain" TagPrefix="uc2" %>
<%@ Register Src="./BomDetail/Main.ascx" TagName="BomDetailMain" TagPrefix="uc2" %>
<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
<div class="ajax__tab_body">
    <table class="mtable">
        <tr>
            <td>
                <uc2:BomViewMain ID="ucBomViewMain" runat="server" Visible="false" />
                <uc2:BomMain ID="ucBomMain" runat="server" Visible="false" />
                <uc2:BomDetailMain ID="ucBomDetailMain" runat="server" Visible="false" />
            </td>
        </tr>
    </table>
</div>
</div>