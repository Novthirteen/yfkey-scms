<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PickListInfo.ascx.cs" Inherits="Distribution_PickList_PickListInfo" %>
<%@ Register Src="PickListDetailList.ascx" TagName="DetailList" TagPrefix="uc2" %>
<%@ Register Src="PickListResultList.ascx" TagName="ResultList" TagPrefix="uc2" %>

<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblPickListNo" runat="server" Text="${MasterData.Distribution.PickList}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbPickListNo" runat="server" OnTextChanged="tbPickList_TextChanged" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblStatus" runat="server" Text="${MasterData.Distribution.PickList.Status}:"
                    Visible="false" />
            </td>
            <td class="td02">
                <asp:Label ID="lbStatus" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
            <td class="td01">
            </td>
            <td class="td02">
               <asp:Button ID="btnStart" runat="server" Text="${MasterData.Order.Button.Start}" OnClick="btnStart_Click"
                    CssClass="button2" Visible="false" />
                <asp:Button ID="btnConfirm" runat="server" Text="${Common.Button.Confirm}" OnClick="btnConfirm_Click"
                    CssClass="button2" Visible="false" />
                <asp:Button ID="btnCancel" runat="server" Text="${Common.Button.Cancel}" OnClick="btnCancel_Click"
                    CssClass="button2" Visible="false" />
                <asp:Button ID="btnClose" runat="server" Text="${Common.Button.Close}" OnClick="btnClose_Click"
                    CssClass="button2" Visible="false" />
                <asp:Button ID="btnPrint" runat="server" Text="${Common.Button.Print}" OnClick="btnPrint_Click"
                    CssClass="button2" Visible="false" />
                <asp:Button ID="btnShip" runat="server" Text="${Common.Button.Ship}" OnClick="btnShip_Click"
                    CssClass="button2" Visible="false" />
                 <asp:Button ID="btnDelete" runat="server" Text="${Common.Button.Delete}" CssClass="button2" OnClick="btnDelete_Click"
                    Visible="false" />     
                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" CssClass="button2" OnClick="btnBack_Click"
                    Visible="false" />    
            </td>
        </tr>
    </table>
</fieldset>
<uc2:DetailList id="ucDetailList" runat="server" visible="false" />
<uc2:ResultList id="ucResultList" runat="server" visible="false" />
