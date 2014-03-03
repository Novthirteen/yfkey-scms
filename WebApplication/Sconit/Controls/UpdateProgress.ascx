<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UpdateProgress.ascx.cs"
    Inherits="Controls_UpdateProgress" %>
<asp:UpdateProgress ID="upLoading" runat="server">
    <ProgressTemplate>
        <div style="margin-top:5px">
            <asp:Image ID="imLoading" runat="server" ImageUrl="~/Images/dt_loading.gif" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
