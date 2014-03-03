<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Warehouse_BatchShelf_Edit" %>
<%@ Register Src="~/Warehouse/PutAway/List.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/UpdateProgress.ascx" TagName="UpdateProgress" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset>
    <legend>${Common.Business.BasicInfo}</legend>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblRegion" runat="server" Text="${Common.Business.Region}:" />
            </td>
            <td class="td02">
                <asp:Label ID="tvMiscOrderRegion" runat="server" Visible="false" />
                <uc3:textbox ID="tbMiscOrderRegion" runat="server" Visible="true" Width="250" DescField="Name"
                    ValueField="Code" MustMatch="true" ServicePath="RegionMgr.service" ServiceMethod="GetRegion"
                    CssClass="inputRequired" />
                <asp:RequiredFieldValidator ID="rfvRegion" runat="server" ErrorMessage="${MasterData.MiscOrder.WarningMessage.RegionEmpty}"
                    Display="Dynamic" ControlToValidate="tbMiscOrderRegion" ValidationGroup="vgCreate" />
            </td>
            <td class="td01">
                <asp:Literal ID="lblLocation" runat="server" Text="${Common.Business.Location}:" />
            </td>
            <td class="td02">
                <asp:Label ID="tvMiscOrderLocation" runat="server" Visible="false" />
                <uc3:textbox ID="tbMiscOrderLocation" runat="server" Visible="true" DescField="Name"
                     ValueField="Code" Width="250" ServicePath="LocationMgr.service"
                    ServiceMethod="GetLocation" ServiceParameter="string:#tbMiscOrderRegion" CssClass="inputRequired" />
                <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ErrorMessage="${MasterData.MiscOrder.WarningMessage.LocationEmpty}"
                    Display="Dynamic" ControlToValidate="tbMiscOrderLocation" ValidationGroup="vgCreate" />
            </td>
        </tr>
        <tr>
            <td class="td01">
                <asp:Literal ID="Literal1" runat="server" Text="库区:" />
            </td>
            <td class="td02">
             <asp:Label ID="Label1" runat="server" Visible="false" />
                <uc3:textbox ID="tbStorageBin" runat="server" Visible="true" DescField="Code"
                     OnTextChanged="LoadItem" AutoPostBack="true" ValueField="Code" Width="250" ServicePath="StorageAreaMgr.service"
                    ServiceMethod="GetStorageArea" ServiceParameter="string:#tbMiscOrderLocation" CssClass="inputRequired" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="${MasterData.MiscOrder.WarningMessage.LocationEmpty}"
                    Display="Dynamic" ControlToValidate="tbStorageBin" ValidationGroup="vgCreate" />
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <div class="tablefooter">
        <asp:Button ID="btnPickup" runat="server" Text="<%$Resources:Language,Pickup%>" OnClick="btnPickup_Click"
            CssClass="button2" />
    </div>
</fieldset>
<fieldset><legend>下架明细</legend>
    <div class="GridView">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="条码">
                    <ItemTemplate>
                        <%# Eval("Hu.HuId")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="批号">
                    <ItemTemplate>
                        <%# Eval("LotNo")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="物料号">
                    <ItemTemplate>
                        <%# Eval("Item.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="物料描述">
                    <ItemTemplate>
                        <%# Eval("Item.Desc1")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="单位">
                    <ItemTemplate>
                        <%# Eval("Hu.Uom.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="数量">
                    <ItemTemplate>
                        <%# Eval("Qty")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="库格号">
                    <ItemTemplate>
                        <%# Eval("StorageBin.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <asp:UpdatePanel ID="UP_GV_List" runat="server">
        <ContentTemplate>
            <uc2:List ID="ucList" runat="server" />
            <uc2:UpdateProgress ID="ucUpdateProgress" runat="server" />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <%--    <table class="mtable">
                <tr>
                    <td class="td01">
                        <asp:Literal ID="ltlScanBarcode" runat="server" Text="<%$Resources:Language,ScanBarcode%>" />:
                    </td>
                    <td class="td02">
                        <asp:TextBox ID="tbScanBarcode" runat="server" AutoPostBack="true" OnTextChanged="tbScanBarcode_TextChanged" />
                    </td>
                    <td />
                </tr>
            </table>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>
