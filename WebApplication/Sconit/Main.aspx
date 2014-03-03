<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Main"
    UICulture="zh-CN" Culture="zh-CN" Title="Sconit" %>

<%@ Register Src="~/Controls/Message.ascx" TagName="Message" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControlToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="~/App_Themes/Base.css" type="text/css" rel="stylesheet" />
</head>
<body onload="OnMainPageLoad()">
    <form id="form1" runat="server">
    <div id="divHidden" style="border-top: 0px solid #FFFFFF;">
        <ajaxControlToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"
            EnableScriptGlobalization="true" ScriptMode="Release">
            <CompositeScript>
                <Scripts>
                    <asp:ScriptReference Path="~/Js/script.js" />
                    <asp:ScriptReference Path="~/Js/jquery.js" />
                    <asp:ScriptReference Path="~/Js/jquery.autocomplete.js" />
                    <asp:ScriptReference Path="~/Js/jquery.easydrag.js" />
                    <asp:ScriptReference Path="~/Js/astreeview_packed.js" />
                    <asp:ScriptReference Path="~/Js/contextmenu_packed.js" />
                    <asp:ScriptReference Path="~/Js/boxover.js" />
                    <%--
                    <asp:ScriptReference Path="~/Js/ui.core-min.js" />
                    <asp:ScriptReference Path="~/Js/ui.dropdownchecklist-min.js" />
                    <asp:ScriptReference Path="~/Js/boxover.js" />
                    <asp:ScriptReference Path="~/Js/jquery.colorbox-min.js" />
                    <asp:ScriptReference Path="~/Js/dhtmlgoodies_calendar.js" />
                    <asp:ScriptReference Path="~/Js/jquery.watermark.min.js" />
                    --%>
                </Scripts>
            </CompositeScript>
        </ajaxControlToolkit:ToolkitScriptManager>
        <input type="hidden" name="id_hideUser" id="id_hideUser" runat="server" />
    </div>
    <div runat="server" id="divsmp" style="margin-left: 5px; margin-right: 0px;">
        <table id="smptable" class="GVAlternatingRow">
            <tr>
                <td>
                    <div style="float: left;">
                        <asp:SiteMapPath ID="SiteMapPath1" runat="server" ShowToolTips="True" RenderCurrentNodeAsLink="true">
                            <CurrentNodeStyle ForeColor="Blue" />
                            <NodeStyle Font-Bold="False" ForeColor="Black" />
                            <RootNodeTemplate>
                                <asp:HyperLink ID="HomeHL" ToolTip="Sconit" Text="Home" runat="server" ForeColor="Black" />
                            </RootNodeTemplate>
                        </asp:SiteMapPath>
                    </div>
                    <div style="float: left; padding-left: 5px" id="divFavorite" runat="server">
                        <a href="#" onclick="AddToFavorite()">
                            <asp:Image ID="ImgAddtoFavorite" runat="server" ImageUrl="~/Images/Frame/AddtoFavorite.png"
                                ToolTip="Add To Favorites" /></a>
                    </div>
                    <div id="FavoriteResultId" style="float: left; padding-left: 5px; color: Red">
                    </div>
                </td>
                <td align="left">
                </td>
                <td align="right">
                    <asp:HyperLink ID="hlFeedBack" Text="<% $Resources:Language,Feedback%>" runat="server"
                        NavigateUrl="~/Main.aspx?mid=MainPage.FeedBack" Font-Underline="true" />
                </td>
            </tr>
        </table>
    </div>
    <div style="height: 8px">
    </div>
    <div id="divucMessage" style="margin-left: 5px; margin-right: 10px; z-index: 16;
        top: 0pt; left: 0pt; position: fixed; width: 100%;" title="Double Click to Hide/双击隐藏"
        ondblclick="$('#divucMessage').fadeOut('slow');">
        <uc2:Message ID="ucMessage" runat="server" Visible="true" />
    </div>
    <div runat="server" id="divphModule" style="margin-left: 10px; margin-right: 10px">
        <asp:PlaceHolder ID="phModule" runat="server"></asp:PlaceHolder>
    </div>
    <%--    <div id="crossdiv" style="background-color: #99ccff; position: absolute; display: none;">
        <table style="border: #66ccff 1px solid;width: 250px; ">
            <tr style="height: 20px;">
                <td style="width: 72px; background-color: #99ccff">
                    <span style="color: #000000;" >帮助</span>
                </td>
                <td align="right" style="background-color: #99ccff">                    
                    <img src="Images/Icon/close.png" onclick="crossdiv.style.display='none';" alt=""/>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="background-color: #fff;">
                    <div id="crosscontent" style="padding: 5; margin: 0 5">
                    </div>
                </td>
            </tr>
        </table>
    </div>--%>
    </form>
</body>
</html>
