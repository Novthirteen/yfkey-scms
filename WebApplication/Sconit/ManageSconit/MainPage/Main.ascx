<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MainPage_Main" %>

<div class="AjaxClass  ajax__tab_default">
    <div class="ajax__tab_header" runat="server" id="div_ajax__tab">
        <span class='ajax__tab_active' id='tab_POMonitoring' runat="server"><span class='ajax__tab_outer'>
            <span class='ajax__tab_inner'><span class='ajax__tab_tab'>
                <asp:LinkButton ID="lbPOMonitoring" Text="<% $Resources:Language,POMonitoring%>" runat="server" OnClick="lbPOMonitoring_Click" /></span></span></span></span><span
                    id='tab_WOMonitoring' runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span
                        class='ajax__tab_tab'><asp:LinkButton ID="lbWOMonitoring" Text="<% $Resources:Language,WOMonitoring%>"
                            runat="server" OnClick="lbWOMonitoring_Click" /></span></span></span></span><span id='tab_DOMonitoring'
                                runat="server"><span class='ajax__tab_outer'><span class='ajax__tab_inner'><span
                                    class='ajax__tab_tab'><asp:LinkButton ID="lbDOMonitoring" Text="<% $Resources:Language,DOMonitoring%>"
                                        runat="server" OnClick="lbDOMonitoring_Click" /></span></span></span></span>
    </div>
    <div class="ajax__tab_body" style="width: 96.6%; height: 91%; position: absolute;">
    
        <iframe src="Main.aspx?mid=Order.OrderHead.Procurement__mp--ModuleType-Procurement_ModuleSubType-Nml_StatusGroupId-7__act--ListAction__smp--none" name="indexFrame" style="width: 98%; margin: 0;
            height: 97%; position: absolute; border: 0;" frameborder="0" runat="server" id="indexFrame"></iframe>
    </div>
</div>
