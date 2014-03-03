<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="MasterData_Location_TabNavigator" %>
<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
    
        <span class='ajax__tab_active' id='tab_Location' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbLocation" Text="${MasterData.Location}" runat="server" OnClick="lbLocation_Click" /></span></span></span></span><span 
        id='tab_LocationAdv' runat="server" visible="false"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbLocationAdv" Text="${MasterData.Location.Area}" runat="server" OnClick="lbLocationAdv_Click" /></span></span></span></span><span 
        id='tab_LocationBin' runat="server" visible="false"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lblBin" Text="${MasterData.Location.Bin}" runat="server" OnClick="lbLocationBin_Click" /></span></span></span></span>
    </div>
<div class="ajax__tab_body">