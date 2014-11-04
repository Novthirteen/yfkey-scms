<%@ Page Language="c#" AutoEventWireup="true" CodeFile="UpdatePassword.aspx.cs" Inherits="UpdatePassword"
    Culture="auto" UICulture="auto" %>

<html>
<head id="Head1" runat="server">
    <%--    <link href="App_Themes/Base.css" type="text/css" rel="stylesheet" />
    <link href="App_Themes/DeepBlue/StyleSheet.css" type="text/css" rel="stylesheet" />--%>
    <style type="text/css">
        body
        {
            font-size: 13px;
            font-family: Arial,宋体, Tahoma;
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
        
        .updatePassword_form
        {
            position: absolute;
            top: 35%;
            z-index: 2;
            width: 484px;
            left: 30%;
            height: 190px;
            background: url("Images/OEM/bg_form.png") no-repeat;
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
            color: #849E59;
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
        function logoffpageload() {
            if (top.location !== self.location) {
                top.location = self.location;
            }
        }
    </script>
</head>
<body style="margin: 0px" bgcolor="#A6BA87" scroll="no" onload="logoffpageload()">
    <div class="login_bottom_ie" style="overflow: hidden; width: 100%; position: relative;">
        <div style="overflow: hidden; position: relative">
            <div class="login_main_area" style="overflow: hidden; position: relative">
                <table class="login_plugins_table" style="left: 10px; position: absolute; top: 30px"
                    cellspacing="4" cellpadding="0" id="table1">
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
                <div class="updatePassword_form" style="overflow: hidden; position: relative">
                    <form id="Login" method="post" runat="server">
                    <%--<div class="login_titleText">
                        <asp:Literal ID="Literal1" runat="server" /></div>--%>
                    <div class="login_titleText">
                        <asp:Literal ID="Literal2" runat="server" />
                    </div>
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
                                                <asp:Literal ID="lblPassword" runat="server" Text="密码:" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbPassword" TextMode="Password" runat="server" Text='<%#Bind("Password") %>'
                                                EnableViewState="false" Width="150" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="login_fields_captions">
                                                <asp:Literal ID="lblConfirmPassword" runat="server" Text="确认密码:" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbConfirmPassword" TextMode="Password" runat="server" Text='<%#Bind("Password") %>'
                                                Width="150" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="btnUpdatePassword" runat="server" OnClick="Update_Click" Text="保存"
                                                CssClass="apply" ValidationGroup="vgPassword" />
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
                <table class="login_plugins_table" style="left: 30%; position: absolute; top: 70%"
                    cellspacing="4" cellpadding="0" id="table4">
                    <tr>
                        <td>强密码要求：</td>
                        <td>
                            <asp:Label ID="Label1" >(1)密码长度大于等于8位。</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="Label2" >(2)英大写(A)、小写(a)、数(12)、特殊字符(&)，至少包含3种类型如（Aa123@#$）。</asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</body>
</html>
