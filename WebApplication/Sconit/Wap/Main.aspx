<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Wap_Main" %>

<%@ Register Src="~/Controls/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sconit Wap Page</title>
    <style type="text/css">
        @import url("Images/Wap.css");
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="nav">
        <a href="Main.aspx?mid=Order.Online">上线</a> | <a href="Main.aspx?mid=Order.Receive">
            收货</a> | <a href="Main.aspx?mid=Order.Transfer">移库</a> | <a href="Logoff.aspx">注销</a>
    </div>
    <div id="divucMessage" style="background: Red; width: 100%">
        <uc2:message id="ucMessage" runat="server" visible="true" />
    </div>
    <div runat="server" id="divphModule">
        <asp:PlaceHolder ID="phModule" runat="server"></asp:PlaceHolder>
    </div>
    </form>
</body>
</html>
