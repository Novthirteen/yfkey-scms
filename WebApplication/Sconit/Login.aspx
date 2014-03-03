<%@ Page Language="c#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login"  Culture="auto" UICulture="auto" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <style type="text/css">
        body
        {
            font-size: 13px;
            font-family: Arial,宋体, Tahoma;
            margin: 0px;
            background-color: #B2BDC4;
        }
        a
        {
            color: White;
        }
        .login_top_stripe
        {
            width: auto;
            height: 36px;
            background-color: #98a5ac;
            background: url(  'Images/Login/Login_top_stripe.png' ) repeat-x;
            text-align: right;
            padding-top: 5px;
            padding-right: 10px;
            color: White;
            word-spacing: 10px;
        }
        .login_main_area
        {
            position: absolute;
            top: 50%;
            width: 100%;
            height: 340px;
            overflow: hidden;
            margin-top: -170px;
            z-index: 2;
        }
        .login_internalFormArea
        {
            width: 400px;
        }
        .login_fields_captions
        {
            font-size: 13px;
            color: #333331;
            text-align: right;
            height: 30px;
            width: 100px;
            padding-right: 20px;
        }
        .login_text_input
        {
            border: 1px solid #FFFFFE;
            width: 175px;
            height: 20px;
            margin-right: 10px;
        }
        .login_button
        {
            margin-top: 2px;
            cursor: pointer;
            margin-left: 10px;
            margin-right: 40px;
            width: 60px;
        }
        .login_copyrightText
        {
            font-size: 12px;
            text-align: center;
            height: 30px;
        }
        .login_copyrightText a
        {
            color: #333333;
            text-decoration: underline;
        }
        .login_bg
        {
            position: absolute;
            top: 50%;
            width: 100%;
            z-index: 0;
            height: 514px;
            margin-top: -257px; /* negative half of the height */
        }
        .login_box_top
        {
            background-image: url(Images/Login/Login_box_top.png);
            background-repeat: no-repeat;
            height: 60px;
            width: 400px;
            text-align: center;
            vertical-align: bottom;
        }
        .login_box_bottom
        {
            background-image: url(Images/Login/Login_box_bottom.png);
            background-repeat: no-repeat;
            background-position: bottom;
            padding: 0px 0px 15px 0px;
            z-index: 2;
        }
        .login_title
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 16px;
            color: #333336;
            padding: 18px 0px 0px 18px;
        }
        .login_content
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: .85em;
            color: #000000;
            text-align: center;
            vertical-align: bottom;
            padding: 10px 0px 0px 0px;
        }
        .login_bg_td
        {
            border: 3px solid #AAB9C1;
            width: 958px;
            height: 512px;
            background-repeat: no-repeat;
            background-color: #909CA3;
        }
        .login_bg_div
        {
            width: 958px;
            height: 512px;
            background-image: url(  "<%=LoginImagePath%>" );
            background-repeat: no-repeat;
        }
        .divThemeSelect
        {
            float: left;
            margin: 5px;
            z-index: 2;
            color: Silver;
            font-size: 12px;
            cursor: pointer;
            display: none;
            text-decoration: underline;
            white-space: nowrap;
        }
    </style>
    <!--[if lt IE 7.]>
    <script defer type="text/javascript" src="Js/pngfix.js"></script>
    <![endif]-->
    <%--<link href="App_Themes/ImportCSS/Button.css" type="text/css" rel="stylesheet" />--%>
    <script type="text/javascript" src="Js/jquery.js"></script>

    <script language="javascript" type="text/javascript">
        function onPageLoad()
        {
            if(getCookie("PicDate")==null)
            {
                $("#divAddTheme").show();
                $("#divAddTheme").fadeOut("slow");
                $("#divAddTheme").fadeIn("slow");
                $("#divAddTheme").fadeOut("slow");
                $("#divRandomTheme").hide();
            }
            else
            {
                $("#divRandomTheme").show();
                $("#divRandomTheme").fadeOut("slow");
                $("#divRandomTheme").fadeIn("slow");
                $("#divRandomTheme").fadeOut("slow");
                $("#divAddTheme").hide();
            }
            //如果是本地调试,则取Default_Bg.jpg
            var host = document.location.host;
            if(host.indexOf("127.0.0.1") > -1)
            {
                $(".login_bg_div").css("background-image","url('Images/Default_Bg.jpg')");
            }

            if (top.location !== self.location)
            {
                top.location = self.location;
            }
            //alert(getCookie("RandomPicDate"));                  
        }
        
        function setCookie(value)
         {
            var Days = 365;   
            var exp = new Date();
            exp.setTime(exp.getTime() + Days*24*60*60*1000);   
            document.cookie = "PicDate" + "="+  escape(value)  + ";expires=" + exp.toGMTString();
            //alert(getCookie("PicDate"));
            showDiv();
        }
        
        function delCookie()
        //删除Cookie
        {
            //alert(getCookie("PicDate"));
            var exp =  new Date();
            exp.setTime (exp.getTime() - 1);
            var cval = getCookie("PicDate");
            document.cookie = "PicDate =" + cval + ";expires=" + exp.toGMTString();
            //alert(getCookie("PicDate"));
            showDiv();
        }
        
        function getCookie(name)//取cookies函数        
        {
            var arr = document.cookie.match(new RegExp("(^| )"+name+"=([^;]*)(;|$)"));
            if(arr != null) return unescape(arr[2]); 
            return null;
        }
        
        function showDiv()
        {
            if(getCookie("PicDate")==null)
            {
                $("#divAddTheme").show();
                $("#divRandomTheme").hide();
            }
            else
            {
                $("#divRandomTheme").show();
                $("#divAddTheme").hide();
            } 
        }
        function hideDiv()
        {
            $("#divRandomTheme").hide();
            $("#divAddTheme").hide();
        }
    </script>

</head>
<body scroll="no" onload="onPageLoad()">

    <form id="Login_form" method="post" runat="server">
    <div class="login_top_stripe">
        <asp:HyperLink ID="Wiki" runat="server" Text="<%$Resources:Language,LoginWiki%>" Target="_blank" />
        |
        <asp:HyperLink ID="Forum" runat="server" Text="<%$Resources:Language,LoginForum%>" Target="_blank" />
        |
        <asp:HyperLink ID="Documentation" runat="server" Text="<%$Resources:Language,LoginDocument%>" Target="_blank" />

    </div>
    <div class="login_bg">
        <table cellspacing="0" cellpadding="0" border="0" align="center">
            <tr>
                <td class="login_bg_td">
                    <div class="login_bg_div">
                        <div class="divThemeSelect" style="display: block; width: 130px; height: 25px" onmouseover="showDiv()"
                            onmouseout="hideDiv()">
                            <div onclick="setCookie('<%=PicDate%>')" id="divAddTheme" class="divThemeSelect">
                                <asp:Literal ID="literalAddTheme" runat="server" Text="<%$Resources:Language,LoginFixTheme%>" />
                            </div>
                            <div onclick="delCookie()" id="divRandomTheme" class="divThemeSelect">
                                <asp:Literal ID="literalRandomTheme" runat="server" Text="<%$Resources:Language,LoginRandomTheme%>" />
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="login_copyrightText">
                    Copyright &copy; 2010 Sconit 
                </td>
            </tr>
        </table>
    </div>
    <div class="login_main_area">
        <table cellspacing="0" cellpadding="0" border="0" align="center">
            <tr>
                <td class="login_box_top">
                    <img border="0" src="Images/Logo.png" alt="logo" />
                </td>
            </tr>
            <tr>
                <td class="login_box_bottom">
                    <table cellspacing="0" cellpadding="0" border="0" width="100%">
                        <tr>
                            <td class="login_content">
                                <table class="login_internalFormArea" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="login_fields_captions">
                                            <asp:Literal ID="Username" runat="server" Text="<%$Resources:Language,LoginAccount%>" />
                                        </td>
                                        <td width="162px">
                                            <input id="txtUsername" runat="server" class="login_text_input" size="20" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="login_fields_captions">
                                            <asp:Literal ID="Password" Text="<%$Resources:Language,LoginPassword%>" runat="server" />
                                        </td>
                                        <td>
                                            <input id="txtPassword" runat="server" class="login_text_input" type="password" size="20" />
                                        </td>
                                        <td>
                                        <div class="buttons">
                                            <asp:Button ID="IbLogin" runat="server" CssClass="login_button" Text="<%$Resources:Language,Login%>" OnClick="Login_Click" />
                                        </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" width="100%" style="text-align: center">
                                            <asp:Label ID="errorLabel" runat="server" Height="10" Font-Size="Smaller" ForeColor="#ff3300"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
