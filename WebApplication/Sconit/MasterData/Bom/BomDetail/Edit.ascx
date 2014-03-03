<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_Bom_BomDetail_Edit" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc2" %>

<script language="javascript" type="text/javascript" src="Js/DatePicker/WdatePicker.js"></script>

<div id="divFV" runat="server">
    <asp:FormView ID="FV_BomDetail" runat="server" DataSourceID="ODS_BomDetail" DefaultMode="Edit"
        Width="100%" DataKeyNames="Id" OnDataBound="FV_BomDetail_DataBound">
        <EditItemTemplate>
            <fieldset>
                <legend>${MasterData.BomDetail.UpdateBomDetail}</legend>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblParCode" runat="server" Text="${MasterData.Bom.ParCode}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbParCode" runat="server" ReadOnly="true" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblCompCode" runat="server" Text="${MasterData.Bom.CompCode}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbCompCode" runat="server" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblOp" runat="server" Text="${Common.Business.Op}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbOp" runat="server" Text='<%# Bind("Operation") %>' ReadOnly="true" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblRef" runat="server" Text="${MasterData.Bom.Reference}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbReference" runat="server" Text='<%#Bind("Reference") %>' ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblStartTime" runat="server" Text="${Common.Business.StartTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbStartTime" runat="server" Text='<%# Bind("StartDate") %>' ReadOnly="true" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblEndTime" runat="server" Text="${Common.Business.EndTime}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbEndTime" runat="server" onClick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" />
                            <asp:CustomValidator ID="cvEndTime" runat="server" ControlToValidate="tbEndTime"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblRateQty" runat="server" Text="${MasterData.Bom.RateQty}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbRateQty" runat="server" Text='<%#Bind("RateQty","{0:0.########}") %>'
                                CssClass="inputRequired" />
                            <asp:CustomValidator ID="cvRateQty" runat="server" ControlToValidate="tbRateQty"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                            <asp:RequiredFieldValidator ID="rfvRateQty" runat="server" ErrorMessage="${MasterData.BomDetail.WarningMessage.RateQtyError}"
                                Display="Dynamic" ControlToValidate="tbRateQty" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblUom" runat="server" Text="${Common.Business.Uom}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbUom" runat="server" DescField="Name" ValueField="Code" ServicePath="UomMgr.service"
                                ServiceMethod="GetAllUom" />
                            <asp:CustomValidator ID="cvUom" runat="server" ControlToValidate="tbUom" Display="Dynamic"
                                ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblBackFlushMethod" runat="server" Text="${MasterData.BomDetail.BackFlushMethod}:" />
                        </td>
                        <td class="td02">
                            <cc2:CodeMstrDropDownList ID="ddlBackFlushMethod" Code="BackFlushMethod" runat="server">
                            </cc2:CodeMstrDropDownList>
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblStruType" runat="server" Text="${MasterData.Bom.StructureType}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbStruType" runat="server" Text='<%#Bind("StructureType") %>' DescField="Description"
                                ValueField="Value" ServicePath="CodeMasterMgr.service" ServiceMethod="GetCachedCodeMaster"
                                ServiceParameter="string:BomDetType" />
                            <asp:CustomValidator ID="cvStruType" runat="server" ControlToValidate="tbStruType"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblScrapPercentage" runat="server" Text="${MasterData.Bom.ScrapPercentage}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbScrapPercentage" runat="server" Text='<%#Bind("ScrapPercentage") %>' />%
                            <asp:CustomValidator ID="cvScrapPercentage" runat="server" ControlToValidate="tbScrapPercentage"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblNeedPrint" runat="server" Text="${MasterData.Bom.NeedPrint}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbNeedPrint" runat="server" Checked='<%#Bind("NeedPrint") %>' />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblIsShipScan" runat="server" Text="${MasterData.Flow.IsShipScanHu}:" />
                        </td>
                        <td class="td02">
                            <asp:CheckBox ID="cbIsShipScan" runat="server" Checked='<%#Bind("IsShipScanHu") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="lblLocation" runat="server" Text="${Common.Business.Location}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbLocation" runat="server" DescField="Name" ValueField="Code" ServicePath="LocationMgr.service"
                                ServiceMethod="GetAllLocation" />
                            <asp:CustomValidator ID="cvLocation" runat="server" ControlToValidate="tbLocation"
                                Display="Dynamic" ValidationGroup="vgSave" OnServerValidate="CV_ServerValidate" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="lblPriority" runat="server" Text="${MasterData.Bom.Priority}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbPriority" runat="server" Text='<%#Bind("Priority") %>' />
                            <asp:RequiredFieldValidator ID="rfvPriority" runat="server" ErrorMessage="*" Display="Dynamic"
                                ControlToValidate="tbPriority" ValidationGroup="vgSave" />
                            <asp:RangeValidator ID="rvPriority" ControlToValidate="tbPriority" runat="server"
                                Display="Dynamic" ErrorMessage="*" MinimumValue="0" MaximumValue="999999999"
                                Type="Integer"></asp:RangeValidator>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <div class="tablefooter">
                <div class="buttons">
                    <asp:Button ID="btnInsert" runat="server" CommandName="Update" Text="${Common.Button.Save}" CssClass="apply"
                        ValidationGroup="vgSave" />
                    <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}" CssClass="delete"
                        OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="back" />
                </div>
            </div>
        </EditItemTemplate>
    </asp:FormView>
</div>
<asp:ObjectDataSource ID="ODS_BomDetail" runat="server" TypeName="com.Sconit.Web.BomDetailMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.BomDetail" UpdateMethod="UpdateBomDetail"
    OnUpdated="ODS_BomDetail_Updated" OnUpdating="ODS_BomDetail_Updating" DeleteMethod="DeleteBomDetail"
    OnDeleted="ODS_BomDetail_Deleted" SelectMethod="LoadBomDetail">
    <SelectParameters>
        <asp:Parameter Name="ID" Type="Int32" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="ID" Type="Int32" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="EndDate" Type="DateTime" ConvertEmptyStringToNull="true" />
    </UpdateParameters>
</asp:ObjectDataSource>
