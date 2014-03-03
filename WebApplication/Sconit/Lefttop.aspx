<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lefttop.aspx.cs" Inherits="Lefttop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LeftTop Page</title>
    <script language="javascript" type ="text/javascript">
	function setBg(bgpng)
	{
	    var leftFrame = window.parent.frames["leftFrame"].document;
	    
	    switch(bgpng)
	    {
	        case "History":
	            document.getElementById('idHistory').style.backgroundImage="url(Images/Frame/History_Active.png)";
	            document.getElementById('idFavorite').style.backgroundImage="url(Images/Frame/Favorite_Inactive.png)"; 
	            document.getElementById('idNav').style.backgroundImage="url(Images/Frame/Nav_Inactive.png)"; 
	            leftFrame.getElementById("DivHistory").style.display='block';
	            leftFrame.getElementById("DivFavorites").style.display='none';
	            leftFrame.getElementById("DivTreeView").style.display='none';
	            window.top.leftFrame.LoadHistory();
	            break;
	        case "Favorite":
	            document.getElementById('idHistory').style.backgroundImage="url(Images/Frame/History_Inactive.png)"; 
	            document.getElementById('idFavorite').style.backgroundImage="url(Images/Frame/Favorite_Active.png)"; 
	            document.getElementById('idNav').style.backgroundImage="url(Images/Frame/Nav_Inactive.png)";
	            leftFrame.getElementById("DivHistory").style.display='none';
	            leftFrame.getElementById("DivFavorites").style.display='block';
	            leftFrame.getElementById("DivTreeView").style.display='none'; 	            
	            window.top.leftFrame.LoadFavorite();
	            break;
	        case "Nav":
	            document.getElementById('idHistory').style.backgroundImage="url(Images/Frame/History_Inactive.png)"; 
	            document.getElementById('idFavorite').style.backgroundImage="url(Images/Frame/Favorite_Inactive.png)"; 
	            document.getElementById('idNav').style.backgroundImage="url(Images/Frame/Nav_Active.png)"; 
	            leftFrame.getElementById("DivHistory").style.display='none';
	            leftFrame.getElementById("DivFavorites").style.display='none';
	            leftFrame.getElementById("DivTreeView").style.display='block';
	            break; 	    
	    }
	}
    if (top.location == self.location)
    {
        top.location="Default.aspx";
    }
    </script>       
    <link rel="stylesheet" type="text/css" href="App_Themes/BaseFrame.css" /> 
</head>
<body class="lefttop" >
    <div style="height:30px;">
        <div style="position: absolute; float: left; left: 5px;height: 30px; margin-top:6px;">
            <asp:HyperLink ID="SUser" runat="server" Target="right" NavigateUrl="~/Main.aspx?mid=Security.UserPreference"
                Font-Size="13px" />
        </div>
        <div id="idHistory" style="background-image: url('Images/Frame/History_Inactive.png'); width: 24px; height: 24px; margin-top:3px;
            cursor: pointer; position: absolute; float: right; right: 5px;" onclick="javascript:setBg('History');" title="History">
        </div>
        <div id="idFavorite" style="background-image: url('Images/Frame/Favorite_Inactive.png'); width: 24px; height: 24px;margin-top:3px;
            cursor: pointer; position: absolute; float: right; right: 35px;" onclick="javascript:setBg('Favorite');" title="Favorite">
        </div>
        <div id="idNav" style="background-image: url('Images/Frame/Nav_Active.png'); width: 24px; height: 24px;margin-top:3px;
            cursor: pointer; position: absolute; float: right; right: 65px;" onclick="javascript:setBg('Nav');" title="Navigation">
        </div>
    </div>
</body>
</html>
