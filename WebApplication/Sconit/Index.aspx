<%@ Page Language="c#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index"
    Culture="auto" UICulture="auto" %>

<html>
<head runat="server">
<%--    <link href="App_Themes/Base.css" type="text/css" rel="stylesheet" />
    <link href="App_Themes/DeepBlue/StyleSheet.css" type="text/css" rel="stylesheet" />--%>
    <style type="text/css" >
        body
        {
	        font-size: 13px;
	        font-family: Arial,ËÎÌå, Tahoma ;
	    }
        .login_bottom_ie
        {
            height: 100%;
            width: 100%;
            background-color: #A6BA87;
        }
        .login_top_stripe
        {
            width: 100%;
            height: 30px;
            background-color: #DEEBC6;
            text-align: right;
            padding-top: 5px;
            padding-right: 10px;
            color: Green;
            word-spacing: 10px;
        }
        .login_main_area
        {
            width: 100%;
            height: 640px;
            background: url( 'Images/OEM/bg_main.png' ) repeat-x;
        }
        .login_welcome_text
        {
            position: absolute;
            top: 183px;
            left: 37%;
            width: 368px;
            height: 28px;
            font-weight: bold;
            font-size: 18px;
            color: #EE9F4E;
            width: 448px;
            height: 25px;
            padding-left: 24px;
            padding-top: 10px;
        }
        .login_form
        {
            position: absolute;
            top: 230px;
            left: 30%;
            z-index: 2;
            width: 484px;
            height: 190px;
            background: url("<% = formBg %>") no-repeat;
        }
        .login_titleText
        {
            font-weight: bold;
            font-size: 13px;
            color: #ffffff;
            width: 448px;
            height: 25px;
            padding-left: 24px;
            padding-top: 10px;
        }
        .login_internalFormArea
        {
            width: 448px;
            height: 114px;
            padding-top: 29px;
            padding-left: 24px;
        }
        .login_fields_captions
        {
            font-weight: bold;
            font-size: 11px;
            color: #849E59;
        }
        .login_text_input
        {
            font-size: 11px;
            border: 1px solid #A9BD85;
            width: 152px;
            height: 18px;
            padding: 1px;
        }
        .login_button
        {
            margin-top: 2px;
            cursor: pointer;
        }
        .login_copyrightText
        {
            color: Black;
            font-size: 11px;
            position: absolute;
            top: 425px;
            left: 55%;
            text-align: right;
            color:#849E59;
        }
        .login_copyrightText a
        {
            color: #A3AE88;
            text-decoration: underline;
        }
        .login_plugins_table
        {
            background-color: transparent;
        }
    </style>
<script language="javascript" type="text/javascript">
function logoffpageload()
{
    if (top.location !== self.location) 
    {
        top.location = self.location;
    }
}
</script>
</head>
<body style="margin: 0px"  bgcolor="#A6BA87" scroll="no" onload="logoffpageload()">
    <div class="login_bottom_ie" style="overflow: hidden; width: 100%; position: relative;">
        <div style="overflow: hidden; position: relative">
            <div class="login_top_stripe" style="overflow: hidden; position: relative">
                <asp:HyperLink ID="Wiki" runat="server" Text="<%$Resources:Language,LoginWiki%>" ForeColor="Green"
                    Target="_blank" />
                |
                <asp:HyperLink ID="Forum" runat="server" Text="<%$Resources:Language,LoginForum%>" ForeColor="Green"
                    Target="_blank" />
                |
                <asp:HyperLink ID="Documentation" runat="server" Text="<%$Resources:Language,LoginDocument%>" ForeColor="Green"
                    Target="_blank" />
                    ¡¡¡¡
            </div>
            <div class="login_main_area" style="overflow: hidden; position: relative">
                <table class="login_plugins_table" style="left: 10px; position: absolute; top: 30px"
                    cellspacing="4" cellpadding="0" id="table1">
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
                <div style="position: absolute; left: 29%; top: 90px; width: 484px; height: 26px">
                    <table width="500px">
                        <tr>
                            <td>
                                <asp:Image ID="imgLogo" runat="server" />
                            </td>
                            <td align="right" valign="bottom">
                            </td>
                        </tr>
                    </table>
                </div>
                <%--<div class="login_welcome_text" style="position: absolute; left: 28%; top: 185px;
                    width: 500px; height: 40px">
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<img src="Images/OEM/welcome2lpp.gif" />
                </div>--%>
                <div class="login_form" style="overflow: hidden; position: relative">
                    <form id="Login" method="post" runat="server">
                    <div class="login_titleText">
                        <asp:Literal ID="Literal1" runat="server" /></div>
                    <table class="login_internalFormArea" cellspacing="0" cellpadding="0" id="table2">
                        <tr>
                            <td style="vertical-align: top" align="left" width="264">
                                <table cellpadding="5" id="table3">
                                    <colgroup>
                                        <col />
                                    </colgroup>
                                    <tr>
                                        <td>
                                            <div class="login_fields_captions">
                                                <asp:Literal ID="Username" runat="server" Text="<%$Resources:Language,LoginAccount%>" /></div>
                                        </td>
                                        <td>
                                            <input id="txtUsername" runat="server" class="login_text_input" size="20"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="login_fields_captions">
                                                <asp:Literal ID="Password" runat="server"  Text="<%$Resources:Language,LoginPassword%>" /></div>
                                        </td>
                                        <td>
                                            <input id="txtPassword" runat="server" class="login_text_input" type="password" size="20" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="right">
                                            <asp:ImageButton ID="IbLogin" runat="server" CssClass="login_button" OnClick="Login_Click"
                                                ImageUrl="~/Images/OEM/button_login.gif" Width="104" meta:resourcekey="IbLogin" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" width="100%">
                                            <asp:Label ID="errorLabel" runat="server" Height="10" Font-Size="Smaller" ForeColor="#ff3300"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top" align="left">
                            </td>
                        </tr>
                    </table>
                    </form>
                </div>
                <div class="login_copyrightText">
                    Copyright &copy; 2010 Sconit <br />
                    </div>
            </div>
        </div>
    </div>
</body>
</html>
