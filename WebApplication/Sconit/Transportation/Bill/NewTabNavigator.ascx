﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewTabNavigator.ascx.cs" Inherits="Transportation_Bill_NewTabNavigator" %>

<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header">
        <span class='ajax__tab_active' id='tab_single' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbSingle" Text="${Transportation.TransportationBill.TabName.Single}" runat="server" OnClick="lbSingle_Click" /></span></span></span></span><span id='tab_batch' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbBatch" Text="${Transportation.TransportationBill.TabName.Batch}" runat="server" OnClick="lbBatch_Click" /></span></span></span></span><span id='tab_recalculate' runat="server"><span 
        class='ajax__tab_outer'><span class='ajax__tab_inner'><span class='ajax__tab_tab'><asp:LinkButton ID="lbRecalculate" Text="${Transportation.TransportationBill.TabName.Recalculate}" runat="server" OnClick="lbRecalculate_Click" /></span></span></span></span>
    </div>