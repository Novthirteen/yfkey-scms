<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Security_Main" %>
<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="Theme.ascx" TagName="Theme" TagPrefix="uc2" %>
<%@ Register Src="NamedQuery.ascx" TagName="NamedQuery" TagPrefix="uc2" %>
<%@ Register Src="../User/Edit.ascx" TagName="Edit" TagPrefix="uc2" %>

<uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
<div class="ajax__tab_body">
    <table class="mtable">
        <tr>
            <td>
                <uc2:Theme ID="ucTheme" runat="server" Visible="false" />
                <uc2:NamedQuery ID="ucNamedQuery" runat="server" Visible="false" />
                <uc2:Edit ID="ucEdit" runat="server" Visible="true" />
            </td>
        </tr>
    </table>
</div>
</div> 