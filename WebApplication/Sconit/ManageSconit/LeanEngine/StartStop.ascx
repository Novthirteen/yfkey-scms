<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StartStop.ascx.cs" Inherits="ManageSconit_LeanEngine_StartStop" %>
<fieldset>
    <legend>${LeanEngine.ServiceManager}</legend>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Label ID="lbCurrentStatus" runat="server" Text="${LeanEngine.CurrentStatus}: "></asp:Label>
            </td>
            <td class="td02">
                <asp:Label ID="lbStatus" runat="server" Text="${LeanEngine.Status.NotRun}"></asp:Label>
            </td>
            <td class="td01">
                <asp:Label ID="lbStartStop" runat="server" Text="${LeanEngine.StartStopService}: "></asp:Label>
            </td>
            <td class="td02">
                <asp:ImageButton ID="ibStart" runat="server" ImageUrl="~/Images/start.png" 
                    ToolTip="${LeanEngine.Start}" onclick="ibStart_Click" />
                <asp:ImageButton ID="ibStop" runat="server" ImageUrl="~/Images/stop.png" ToolTip="${LeanEngine.Stop}"
                    Visible="false" onclick="ibStop_Click" />
            </td>
        </tr>
    </table>
</fieldset>
