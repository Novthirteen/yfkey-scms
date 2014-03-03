<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Nav.aspx.cs" Inherits="Nav" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControlToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Nav Page</title>

    <script language="javascript" type="text/javascript">
        function LoadFavorite() {
            Sys.Net.WebServiceProxy.invoke('Webservice/userFavoritesMgrWS.asmx', 'ListUserFavorites', false,
                { "userCode": document.getElementById("id_hideUser").value, "type": "Favorite" },
            function OnSucceeded(result, eventArgs) {
                var RsltElem =
               document.getElementById("DivFavorites");
                RsltElem.innerHTML = result;
                //$("li").hover( function(){$("span").show();},function () {$("span").hide();});
                //alert("Success");
            },
            function OnFailed(error) {
                // alert(error.get_message());
            }
           );
            //alert(document.getElementById("id_hideUser").value);
        }

        function DeleteFavorite(id) {
            //alert("test");
            Sys.Net.WebServiceProxy.invoke('Webservice/userFavoritesMgrWS.asmx', 'DeleteUserFavorite', false,
           { "id": id },
            function OnSucceeded(result, eventArgs) {
                if (document.getElementById("DivFavorites").style.display == 'none')
                    LoadHistory();
                else
                    LoadFavorite();
                //alert("Delete Success");
            },
            function OnFailed(error) {
                //alert(error.get_message());
            }
           );
        }
        //History
        function LoadHistory() {
            Sys.Net.WebServiceProxy.invoke('Webservice/userFavoritesMgrWS.asmx', 'ListUserFavorites', false,
                { "userCode": document.getElementById("id_hideUser").value, "type": "History" },
            function OnSucceeded(result, eventArgs) {
                var RsltElem =
               document.getElementById("DivHistory");
                RsltElem.innerHTML = result;
                //alert("Success");
            },
            function OnFailed(error) {
                //alert(error.get_message());
            }
           );
        }

        function OnPageLoad() {
            try {
                //alert("run");
                if (top.location == self.location) {
                    top.location = "Default.aspx";
                }

                var leftupFrame = window.parent.frames["leftup"].document;

                if (leftupFrame.getElementById('idHistory') != null && leftupFrame.getElementById('idHistory').style.backgroundImage == 'url(Images/Frame/History_Active.png)') {
                    LoadHistory();
                    document.getElementById("DivHistory").style.display = 'block';
                    document.getElementById("DivFavorites").style.display = 'none';
                    document.getElementById("DivTreeView").style.display = 'none';
                    //alert("facorites");
                }

                if (leftupFrame.getElementById('idFavorite').style.backgroundImage == 'url(Images/Frame/Favorite_Active.png)') {
                    document.getElementById("DivHistory").style.display = 'none';
                    document.getElementById("DivFavorites").style.display = 'block';
                    document.getElementById("DivTreeView").style.display = 'none';
                    LoadFavorite();
                    //alert("History");
                }
            }
            catch (err){ }
        }

        window.onerror = function() { return true; } //屏蔽错误
    </script>

    <link href="App_Themes/Base.css" type="text/css" rel="stylesheet" />
    <link href="App_Themes/BaseFrame.css" type="text/css" rel="stylesheet" />
</head>
<body class="leftbody" onload="OnPageLoad()">
    <form id="form1" runat="server">
    <ajaxControlToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"
        EnableScriptGlobalization="true" ScriptMode="Release">
        <CompositeScript>
            <Scripts>
                <asp:ScriptReference Path="~/Js/jquery.js" />
            </Scripts>
        </CompositeScript>
    </ajaxControlToolkit:ToolkitScriptManager>
    <input type="hidden" name="id_hideUser" id="id_hideUser" runat="server" />
    <div style="position: absolute; top: 3px; left: 3px" id="DivTreeView">
        <asp:TreeView ID="TreeView1" runat="server" DataSourceID="SiteMapDataSource1" ShowLines="true"
            ExpandDepth="0" MaxDataBindDepth="2" Target="right" ShowExpandCollapse="true"
            OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
        </asp:TreeView>
        <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" ShowStartingNode="false" />
    </div>
    <div style="display: none; position: absolute; top: 3px; left: 0px;" id="DivFavorites"
        class="listfav" runat="server">
    </div>
    <div style="display: none; position: absolute; top: 3px; left: 0px;" id="DivHistory"
        class="listfav" runat="server">
    </div>
    </form>
</body>
</html>
