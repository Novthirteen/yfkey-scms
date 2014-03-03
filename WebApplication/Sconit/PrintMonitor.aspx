<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintMonitor.aspx.cs" Inherits="PrintMonitor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PrintMonitor</title>

    <script language="javascript" type="text/javascript">      
        var url;
        function Print(urls) {
            var urlArray = urls.split("|");
            for (url in urlArray) {
                var xlApp = null;
                try {
                    xlApp = new ActiveXObject("Excel.Application");
                } catch (e) {
                    alert("${Common.Warning.Please.Send.The.Site.To.Join.Trust.Site}");
                    return;
                    break;
                }
                var xlBook = xlApp.WorkBooks.open(urlArray[url]);
                var xlsheet = xlBook.Worksheets(1);
                xlsheet.PrintOut(); //打印工作表
                xlBook.Close(false); //关闭文档
                xlApp.Quit();   //结束excel对象
                xlApp = null;   //释放excel对象
            }
        
        }
    </script>

    <link href="App_Themes/Base.css" type="text/css" rel="stylesheet" />
</head>
<body style="margin: 0 0 0 6">
    <form id="form1" runat="server">
    <div style="margin: 0px">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Timer ID="Timer1" runat="server" Interval="60000" OnTick="Timer1_Tick" />       
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    </div>
    </form>
</body>
</html>
