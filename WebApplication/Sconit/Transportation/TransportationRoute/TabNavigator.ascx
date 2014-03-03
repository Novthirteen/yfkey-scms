<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabNavigator.ascx.cs" Inherits="Transportation_TransportationRoute_TabNavigator" %>

<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
    
        <span class='ajax__tab_active' id='tab_transportationRoute' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbTransportationRoute" Text="${Transportation.TransportationRoute.TransportationRoute}" runat="server" 
        OnClick="lbTransportationRoute_Click" /></span></span></span></span><span 
        id='tab_transportationRouteDetail' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span 
        class='ajax__tab_tab'><asp:LinkButton ID="lbTransportationRouteDetail" Text="${Transportation.TransportationRoute.TransportationRouteDetail}" runat="server" 
        OnClick="lbTransportationRouteDetail_Click" /></span></span></span></span>
    </div>
