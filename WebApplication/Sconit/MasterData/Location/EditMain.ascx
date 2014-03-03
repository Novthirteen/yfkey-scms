<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditMain.ascx.cs" Inherits="MasterData_Location_EditMain" %>

<%@ Register Src="TabNavigator.ascx" TagName="TabNavigator" TagPrefix="uc2" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc2" %>
<%@ Register Src="StorageArea/Main.ascx" TagName="Area" TagPrefix="uc2" %>
<%@ Register Src="StorageBin/Main.ascx" TagName="Bin" TagPrefix="uc2" %>

    <uc2:TabNavigator ID="ucTabNavigator" runat="server" Visible="true" />
    <uc2:Edit ID="ucEdit" runat="server" Visible="true" />
    <uc2:Area ID="ucArea" runat="server" Visible="false" />
    <uc2:Bin ID="ucBin" runat="server" Visible="false" />
    </div>
    </div>