<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Portal_Login.aspx.cs" Inherits="Portal_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .PortalMenu
        {
            border-bottom: 1px dashed #CCCCCC;
            line-height: 20px;
            list-style-position: outside;
            list-style-type: none;
            margin-left: 5px;
            text-align: left;
            font-size: 13px;
            text-decoration: none;
        }
        a
        {
            color: Black;
        }
    </style>

    <script type="text/javascript">

        function tdMenuHide() {
            var tdMenu = document.getElementById('tdMenu');
            //alert(tdMenu);
            if (tdMenu.style.display == "block") {
                tdMenu.style.display = "none";
            }
            else if (tdMenu.style.display == "none") {
                tdMenu.style.display = "block";
            }
        }
    </script>

    <link href="App_Themes/BaseFrame.css" type="text/css" rel="stylesheet" />
</head>
<body style="margin: 0px; background-color: #F8F8F8">
    <form id="form1" runat="server">
    <div>
        <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
        <table style="width: 100%">
            <tr>
                <td style="vertical-align: top; display: block" id="tdMenu" width="130px">
                    <%--<asp:PlaceHolder ID="phMenu" runat="server"></asp:PlaceHolder>--%>
                    <asp:Literal ID="message" runat="server" />
                    <br />
                    <asp:TreeView ID="TreeView1" runat="server" DataSourceID="SiteMapDataSource1" ShowLines="true"
                        ExpandDepth="1" MaxDataBindDepth="2" Target="right" ShowExpandCollapse="true">
                    </asp:TreeView>
                    <br /><br />
                    <a href="Reports/Templates/ExcelTemplates/code128.ttf"> <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:Language,MasterDataDownloadCode128%>" /></a>
                    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" ShowStartingNode="false" />
                </td>
                <td width="8px" style="background: #999999" onclick="tdMenuHide()">
                </td>
                <td style="width: auto">
                    <iframe id="right" name="right" width="100%" height="500px" frameborder="0" src="Main.aspx?mid=Order.OrderIssue__mp--ModuleType-Distribution_IsSupplier-true">
                    </iframe>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
