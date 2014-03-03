<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logoff.aspx.cs" Inherits="Logoff"
    Title="Logoff" %>

<script language="javascript" type="text/javascript">
function logoffpageload()
{
    if (top.location !== self.location) 
    {
        top.location = self.location;
    }
}
</script>
<body onload="logoffpageload()"></body>
