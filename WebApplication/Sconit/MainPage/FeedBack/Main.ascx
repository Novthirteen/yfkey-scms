<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MainPage_FeedBack_Main" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>
<%--<div runat="server" id="divsmp">
    <table id="smptable">
        <tr>
            <td>
                <span style="display: inline-block;"><span style="color: Black;" title="SCONIT">Home
                </span><span style="color: Black;">&gt;</span> <span>
                    <asp:HyperLink ID="hlFeedBack" Text="<% $Resources:Language,Feedback%>" runat="server"
                        NavigateUrl="~/Main.aspx?mid=MainPage.FeedBack" Font-Underline="false" /></span>
                </span>
            </td>
            <td align="right">
            </td>
        </tr>
    </table>
</div>--%>
<div style="height: 8px">
</div>
<div style="width: 100%; text-align: center">
    <center>
        <table style="width: 700px;">
            <tr>
                <td style="text-align: right; width: 50px">
                    ${MasterData.FeedBack.Topic}:
                </td>
                <td style="width: 410px">
                    <asp:TextBox ID="tbsubject" runat="server" Width="400px" CssClass="inputRequired" />
                    <asp:CustomValidator ID="cvSubject" runat="server" ControlToValidate="tbsubject"
                        Display="Dynamic" OnServerValidate="CV_ServerValidate" />
                </td>
                <td style="text-align: right; width: 70px">
                    ${MasterData.FeedBack.Type}:
                </td>
                <td style="width: 170px">
                    <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="${MasterData.FeedBack.Question}" Value="Question" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="${MasterData.FeedBack.Error}" Value="Error"></asp:ListItem>
                        <asp:ListItem Text="${MasterData.FeedBack.Require}" Value="Require"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="3">
                    <cc1:Editor ID="Content" runat="server" Height="400px" Width="650px" />
                    <asp:CustomValidator ID="cvContent" runat="server" ControlToValidate="Content" Display="Dynamic"
                        OnServerValidate="CV_ServerValidate" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <div class="tablefooter">
                        <asp:Button ID="Submit" runat="server" OnClick="Submit_Click" Text="${Common.Button.Submit}" />
                        <asp:Button ID="btBack" runat="server" OnClick="btnBack_Click" Text="${Common.Button.Back}" /></div>
                </td>
            </tr>
        </table>
    </center>
</div>
<asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick" Enabled="false" />
