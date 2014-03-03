<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Top.aspx.cs" Inherits="Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Top Page</title>

    <script type="text/javascript" src="Js/jquery.js"></script>

    <script type="text/javascript">
        function refresh() {
            parent.right.location.reload();
            //parent.left.location = "";
            //window.location.reload();
        }

        window.onresize = doResize;

        function doResize() {
            var LogoTranswidth = parseFloat(document.body.clientWidth) - 750;
            if (LogoTranswidth < 400) {
                LogoTranswidth = 400;
            }
            $("#IdLogotrans").attr("width", LogoTranswidth);
        }

        function onPageLoad() {
            doResize();

            if (top.location == self.location) {
                top.location = "Default.aspx";
            }

            var LoginImagePath = getCookie("RandomPicDate");
            var themeFrame = getCookie("ThemeFrame");

            //alert(themeFrame+" "+PicDate);
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
                        //alert(LoginImagePath);
                    }
                }
            }
            //alert(getCookie("RandomPicDate"));
        }

        function getCookie(name)//取cookies函数        
        {
            var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
            if (arr != null) return unescape(arr[2]);
            return null;
        }

        function setColor(color) {
            alert(color);
        }
        
    </script>

    <link rel="stylesheet" type="text/css" href="App_Themes/BaseFrame.css" />
    <!--[if lt IE 7.]>
    <script defer type="text/javascript" src="Js/pngfix.js"></script>
    <![endif]-->
</head>
<body class="topbody" onload="onPageLoad()">
    <form id="form1" runat="server">
    <asp:HiddenField ID="data" runat="server" />
    <div style="float: left; position: absolute; top: 0px; left: 0px; z-index: -1; width: 600px;
        height: 50px;">
        <img src="Images/Frame/Logo_trans.png" width="600px" height="50px" alt="bg" id="IdLogotrans" />
    </div>
    <div style="width: 100%">
        <table class="toptable" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td class="toptdlogo">
                    <asp:Image ID="LeftImage" ImageAlign="Middle" NavigateUrl="~/Default.aspx" ImageUrl="~/Images/Logo.png"
                        runat="server" />
                </td>
                <td width="17px" align="Middle">
                    <asp:Image ID="Line" ImageAlign="Middle" ImageUrl="~/Images/line.gif" runat="server" />
                </td>
                <td class="toptdmid">
                    <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td align="left" valign="middle" rowspan="2">
                                &nbsp;<asp:Label ID="Info" valign="top" runat="server" Font-Names="Arial" Font-Size="13px"
                                    Font-Bold="true"></asp:Label>
                            </td>
                            <td align="right">
                                <div style="background: url(Images/Banner_bg.png) no-repeat left; height: 30px; margin-bottom: 20px;
                                    max-width: 220px;">
                                    <div style="padding-top: 5px; padding-left: 30px; text-align: center; word-spacing: 5px;">
                                        <%--     <asp:HyperLink ID="helpHL" Text="<% $Resources:Language,TopHelp%>" runat="server" NavigateUrl="#" CssClass="HyperLink"
                                    Target="right" />
                                |--%>
                                        <asp:HyperLink ID="Refresh" runat="server" Text="<% $Resources:Language,TopRefresh%>"
                                            CssClass="HyperLink" NavigateUrl="javascript:refresh();" />
                                        |
                                        <asp:HyperLink ID="HomeHL" Text="<% $Resources:Language,TopHome%>" runat="server"
                                            CssClass="HyperLink" NavigateUrl="~/Main.aspx" Target="right" />
                                        |
                                        <asp:HyperLink ID="logoffHL" Text="<% $Resources:Language,TopLogoff%>" runat="server"
                                            CssClass="HyperLink" Target="right" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
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
