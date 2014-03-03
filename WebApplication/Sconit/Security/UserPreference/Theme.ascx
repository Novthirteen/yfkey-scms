<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Theme.ascx.cs" Inherits="Security_Theme" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="divRefresh" runat="server" visible="false">

    <script type="text/javascript">
        function refresh() {
            //alert("aaa");
            parent.topFrame.location = "Top.aspx";
            parent.leftFrame.location = "Nav.aspx";
            parent.control.location = "Switch.aspx";
            parent.leftup.location = "Lefttop.aspx";
            window.top.topFrame.onPageLoad();
            window.top.control.onPageLoad();
        }
        setTimeout("refresh()", 5);
    </script>

</div>
<fieldset>
    <table width="100%">
        <tr>
            <td class="td01">
                <asp:Literal ID="FrameThemes" runat="server" Text="${Security.UserPreference.ThemeFrame}:" />
            </td>
            <td class="td02">
                <asp:DropDownList ID="DDL_ThemeFrame" runat="server" AutoPostBack="false" Width="120px"
                    DataTextField="Description" DataValueField="Value" OnSelectedIndexChanged="DDL_ThemeFrame_SelectedIndexChanged" />
            </td>
            <td class="td01">
                <asp:Literal ID="Themes" runat="server" Text="${Security.UserPreference.ThemePage}:" />
            </td>
            <td class="td02">
                <asp:DropDownList ID="DDL_ThemePage" runat="server" AutoPostBack="false" Width="120px"
                    DataTextField="Description" DataValueField="Value" OnSelectedIndexChanged="DDL_ThemePage_SelectedIndexChanged" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="ltlLanguage" runat="server" Text="${Security.UserPreference.Language}:" />
            </td>
            <td class="td02">
                <cc1:CodeMstrDropDownList ID="ddlLanguage" Code="Language" runat="server" OnSelectedIndexChanged="DDL_Language_SelectedIndexChanged" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlCSModule" runat="server" Text="${Security.UserPreference.CSModule}:" />
            </td>
            <td class="td02">
                <asp:DropDownList ID="ddlCSModule" runat="server" AutoPostBack="false" Width="120px"
                    DataTextField="Description" DataValueField="Code" AppendDataBoundItems="true"
                    OnSelectedIndexChanged="DDL_CSModule_SelectedIndexChanged">
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
            <td>
            </td>
            <td class="td02">
                <div class="buttons">
                    <asp:Button ID="SaveBt" runat="server" Text="${Common.Button.Save}" OnClick="SaveBt_Click"
                        CssClass="apply" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
