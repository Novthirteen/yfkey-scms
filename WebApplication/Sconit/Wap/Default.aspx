<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Wap_Default"  Culture="auto" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sconit Wap Login Page</title>
    <style type="text/css">
        @import url("Images/Wap.css");
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <img border="0" src="Images/Logo.gif" alt="Logo" />
    </div>
    <div>
        <asp:Literal ID="ltlUsername" runat="server" Text="<%$Resources:Language,LoginAccount%>" />
        <br />
        <input id="inputUsername" runat="server" />
        <br />
        <asp:Literal ID="ltlPassword" Text="<%$Resources:Language,LoginPassword%>" runat="server" />
        <br />
        <input id="inputPassword" runat="server" type="password" />
        <br />
        <asp:Button ID="btnLogin" runat="server" Text="<%$Resources:Language,Login%>" OnClick="Login_Click" />
        <br />
        <asp:Label ID="lblError" runat="server" Height="10" Font-Size="Smaller" ForeColor="#ff3300"></asp:Label>
    </div>
    </form>
</body>
</html>
