<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TextBox.ascx.cs" Inherits="Controls_TextBox" %>
<asp:PlaceHolder ID="phLocaldata" runat="server"></asp:PlaceHolder>
<div ID="divSuggest" runat="server" class="suggestInput">
    <asp:TextBox ID="suggest" runat="server" EnableViewState="true" CssClass="suggestTextBox"/>
    <div id="suggestDiv" class="suggestButton" title="选择" runat="server"/>
</div>