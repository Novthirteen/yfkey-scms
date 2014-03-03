<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Switch.aspx.cs" Inherits="Switch" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="MSHTML 6.00.5730.13" name="GENERATOR" />
    <title>Switch Page</title>

    <script type="text/javascript" src="Js/jquery.js"></script>

    <script language="javascript" type="text/javascript">
        var flag = false;
        function shift_status() {
            var mainFrame = top.document.getElementsByName("mainframeset")[0];
            if (flag) {
                if (screen.width > 1024) {
                    mainFrame.cols = "220,9,*";
                } else if (screen.width > 800) {
                    mainFrame.cols = "220,9,*";
                } else {
                    mainFrame.cols = "200,9,*";
                }
                //document.getElementById("menuSwitch1").src='Images/Nav/Spacer.gif';
                //document.getElementById("menuSwitch1").title='隐藏';
            }
            else {
                //parent.main.cols = "0,9,*";
                mainFrame.cols = "0,9,*";
                //document.getElementById("menuSwitch1").src='Images/Nav/Spacer.gif';
                //document.getElementById("menuSwitch1").title='显示';
            }
            flag = !flag;
        }

        function onSwitchPageLoad() {
            if (top.location == self.location) {
                top.location = "Default.aspx";
            }
            var LoginPage = getCookie("LoginPage");

            if (LoginPage == "Index") {
                return;
            }
            var LoginImagePath = getCookie("RandomPicDate");
            var themeFrame = getCookie("ThemeFrame");

            if ($.browser.msie) {
                if (themeFrame != null) {
                    document.body.style.backgroundColor = themeFrame;
                    document.body.style.backgroundImage = "";
                    //alert(themeFrame);
                }
                else {
                    if (LoginImagePath != null) {
                        //如果是本地调试,则取Default_Bg.jpg
                        var host = document.location.host;
                        if (host.indexOf("127.0.0.1") > -1) {
                            document.body.style.backgroundImage = "url('Images/Default_Bg.jpg')";
                            document.body.style.backgroundColor = "";
                        }
                        else {
                            document.body.style.backgroundImage = "url('" + LoginImagePath + "')";
                            document.body.style.backgroundColor = "";
                        }
                    }
                }
            }
            else {
                if (themeFrame != "") {
                    document.body.style.backgroundColor = "#" + themeFrame;
                    document.body.style.backgroundImage = "";
                    //alert(themeFrame);
                }
                else {
                    if (LoginImagePath != "") {
                        //如果是本地调试,则取Default_Bg.jpg
                        var host = document.location.host;
                        if (host.indexOf("127.0.0.1") > -1) {
                            document.body.style.backgroundImage = "url('Images/Default_Bg.jpg')";
                            document.body.style.backgroundColor = "";
                        }
                        else {
                            document.body.style.backgroundImage = "url('" + LoginImagePath + "')";
                            document.body.style.backgroundColor = "";
                        }
                    }
                }
            }
            //  alert(document.body.style.backgroundImage);     
        }

        function getCookie(name)//取cookies函数        
        {
            var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
            if (arr != null) return unescape(arr[2]);
            return null;
        }
    </script>

    <link rel="stylesheet" type="text/css" href="App_Themes/BaseFrame.css" />
</head>
<body onclick="shift_status()" onload="onSwitchPageLoad()" style="margin: 0px; background: url('Images/OEM/Switch_bg.jpg') right"
    class="switch">
    <table cellspacing="0" cellpadding="0" border="0" style="height: 100%;" id="tableSwitch"
        runat="server">
        <tr>
            <td width="100">
            </td>
        </tr>
    </table>
</body>
</html>
