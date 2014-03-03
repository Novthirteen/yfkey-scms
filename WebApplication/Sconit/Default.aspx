<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" >
    <script language="javascript" type="text/javascript">
        if (top.location !== self.location)
        {
              top.location=self.location;
        }
    </script>
</head>

<FRAMESET frameSpacing="0" rows="50,*" frameBorder="NO">
	<FRAME name="topFrame" src="Top.aspx" scrolling=no>
		<FRAMESET id="mainframeset" name="mainframeset" frameSpacing="0" frameBorder="NO" cols="220,9,*" scrolling="no">
			<FRAMESET id="leftAll" name="leftAll" frameSpacing="0" rows="33,*,<% = leftdownHeight %>" frameBorder="NO">
			<FRAME id="leftup" name="leftup" src="Lefttop.aspx" scrolling="no" target="right">
			<FRAME id="leftFrame" name="leftFrame" src="Nav.aspx" scrolling="auto" target="right">
			<FRAME id="leftdown" name="leftdown" src="PrintMonitor.aspx" scrolling="no" target="right">
			</FRAMESET>
		<FRAME name="control" src="Switch.aspx" frameBorder="no" noResize scrolling="no">
		<FRAME name="right" src="<% = url %>" scrolling="auto">
		</FRAMESET>
</FRAMESET>

</html>

