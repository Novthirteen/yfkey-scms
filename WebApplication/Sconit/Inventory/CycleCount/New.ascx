<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Inventory_CycleCount_New" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<div id="divFV" runat="server">
    <fieldset>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblRegion" runat="server" Text="${Common.Business.Region}:" />
                </td>
                <td class="td02">
                    <uc3:textbox id="tbRegion" runat="server" visible="true" width="280" descfield="Name"
                        valuefield="Code" mustmatch="true" servicepath="RegionMgr.service" servicemethod="GetRegion"
                        cssclass="inputRequired" />
                    <asp:RequiredFieldValidator ID="rfvRegion" runat="server" ControlToValidate="tbRegion"
                        ErrorMessage="${Common.Business.Error.RegionInvalid}" Display="Dynamic" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblLocation" runat="server" Text="${Common.Business.Location}:" />
                </td>
                <td class="td02">
                    <uc3:textbox id="tbLocation" runat="server" visible="true" descfield="Name" width="280"
                        valuefield="Code" servicepath="LocationMgr.service" servicemethod="GetLocation"
                        serviceparameter="string:#tbRegion" cssclass="inputRequired" />
                    <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="tbLocation"
                        ErrorMessage="${Common.Business.Error.LocationInvalid}" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblEffDate" runat="server" Text="${Common.Business.EffDate}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbEffDate" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd'})" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblType" runat="server" Text="${Common.Business.Type}:" />
                </td>
                <td class="td02">
                    <cc1:codemstrdropdownlist id="ddlType" runat="server" code="PhysicalCountType" />
                </td>
            </tr>
            <tr>
                <td colspan="3" />
                <td class="td02">
                    <asp:Button ID="btnSave" runat="server" Text="${Common.Button.Save}" OnClick="btnSave_Click"
                        Width="59px" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                        Width="59px" />
                </td>
            </tr>
        </table>
    </fieldset>
</div>
