<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UomConv.ascx.cs" Inherits="MasterData_UomConversion_Main" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script language="javascript" type="text/javascript">
    function checkUom(source, args) {
        var baseUom = $('#<%= ((Controls_TextBox)(this.FV_UomConversion.FindControl("tbBaseUom"))).ClientID + "_suggest" %>').val();
        var altUom = $('#<%= ((Controls_TextBox)(this.FV_UomConversion.FindControl("tbAltUom"))).ClientID + "_suggest" %>').val();
        alert(baseUom + " | "+altUom);
        
        if (baseUom == altUom) {
            args.IsValid = false;
        }
    }
</script>

<fieldset id="fldSearch" runat="server">
    <table class="mtable">
        <tr>
            <td class="ttd01">
                <asp:Literal ID="ltlItemCode" runat="server" Text="${MasterData.UomConversion.ItemCode}:" />
            </td>
            <td class="ttd02">
                <asp:TextBox ID="tbItemCode" runat="server"></asp:TextBox>
            </td>
            <td class="ttd01">
            </td>
            <td class="ttd02">
            </td>
        </tr>
    </table>
    <div class="tablefooter">
        <asp:Button ID="SearchBtn" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
            CssClass="button2" />
        <asp:Button ID="btnInsert" runat="server" Text="${Common.Button.New}" OnClick="btnInsert_Click"
            CssClass="button2" />
    </div>
</fieldset>
<div id="floatdiv">
    <fieldset id="fldInsert" runat="server" visible="false">
        <legend>${MasterData.UomConversion.NewUomConversion}</legend>
        <asp:FormView ID="FV_UomConversion" runat="server" DataSourceID="ODS_FV_UomConversion"
            DefaultMode="Insert">
            <InsertItemTemplate>
                <table class="mtable">
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlItemCode" runat="server" Text="${MasterData.UomConversion.ItemCode}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbItemCode" runat="server" Visible="true" DescField="Description"
                                MustMatch="true" ValueField="Code" ServicePath="ItemMgr.service" ServiceMethod="GetCacheAllItem"
                                Width="250" />
                        </td>
                        <td class="td01">
                        </td>
                        <td class="td02">
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlBaseUom" runat="server" Text="${MasterData.UomConversion.BaseUom}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbBaseUom" runat="server" Visible="true" DescField="Description"
                                MustMatch="true" CssClass="inputRequired" ValueField="Code" ServicePath="UomMgr.service"
                                ServiceMethod="GetAllUom" Width="250" />
                            <asp:RequiredFieldValidator ID="rfvBaseUom" runat="server" ErrorMessage="${MasterData.UomConversion.BaseUom.Empty}"
                                Display="Dynamic" ControlToValidate="tbBaseUom" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlAltUom" runat="server" Text="${MasterData.UomConversion.AltUom}:" />
                        </td>
                        <td class="td02">
                            <uc3:textbox ID="tbAltUom" runat="server" Visible="true" DescField="Description"
                                MustMatch="true" CssClass="inputRequired" ValueField="Code" ServicePath="UomMgr.service"
                                ServiceMethod="GetAllUom" Width="250" />
                            <asp:RequiredFieldValidator ID="rfvAltUom" runat="server" ErrorMessage="${MasterData.UomConversion.AltUom.Empty}"
                                Display="Dynamic" ControlToValidate="tbAltUom" ValidationGroup="vgSave" />
                            <asp:CustomValidator ID="cvUom" runat="server" ErrorMessage="${MasterData.UomConversion.Same.Uom}"
                                ValidationGroup="vgSave" ClientValidationFunction="checkUom" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td01">
                            <asp:Literal ID="ltlBaseQty" runat="server" Text="${MasterData.UomConversion.BaseQty}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbBaseQty" runat="server" Text='<%# Bind("BaseQty") %>' CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvBaseQty" runat="server" ErrorMessage="${MasterData.UomConversion.BaseQty.Empty}"
                                Display="Dynamic" ControlToValidate="tbBaseQty" ValidationGroup="vgSave" />
                            <asp:RegularExpressionValidator ID="revBaseQty" ControlToValidate="tbBaseQty" runat="server"
                                ErrorMessage="${Common.Validator.Valid.Number}" ValidationExpression="^[0-9]+(.[0-9]{1,8})?$"
                                Display="Dynamic" ValidationGroup="vgSave" />
                        </td>
                        <td class="td01">
                            <asp:Literal ID="ltlAltQty" runat="server" Text="${MasterData.UomConversion.AlterQty}:" />
                        </td>
                        <td class="td02">
                            <asp:TextBox ID="tbAltQty" runat="server" Text='<%# Bind("AlterQty") %>' CssClass="inputRequired" />
                            <asp:RequiredFieldValidator ID="rfvAltQty" runat="server" ErrorMessage="${MasterData.UomConversion.AltQty.Empty}"
                                Display="Dynamic" ControlToValidate="tbAltQty" ValidationGroup="vgSave" />
                            <asp:RegularExpressionValidator ID="revAltQty" ControlToValidate="tbAltQty" runat="server"
                                ErrorMessage="${Common.Validator.Valid.Number}" ValidationExpression="^[0-9]+(.[0-9]{1,8})?$"
                                Display="Dynamic" ValidationGroup="vgSave" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="ttd01">
                        </td>
                        <td>
                            <asp:Button ID="btnNew" runat="server" CommandName="Insert" Text="${Common.Button.Save}"
                                CssClass="button2" ValidationGroup="vgSave" />
                            <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                                CssClass="button2" />
                        </td>
                    </tr>
                </table>
            </InsertItemTemplate>
        </asp:FormView>
    </fieldset>
</div>
<fieldset id="fldGV" runat="server" visible="false">
    <asp:GridView ID="GV_UomConversion" runat="server" AutoGenerateColumns="False" DataSourceID="ODS_GV_UomConversion"
        OnDataBound="GV_UomConversion_OnDataBind" DataKeyNames="Id" AllowPaging="True"
        PageSize="10" OnRowUpdating="GV_UomConversion_RowUpdating">
        <Columns>
            <asp:TemplateField HeaderText="${MasterData.UomConversion.ItemCode}">
                <ItemTemplate>
                    <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("Id") %>' />
                    <asp:Literal ID="lblItemCode" runat="server" Text='<%# Eval("Item.Code") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.Item.Description}">
                <ItemTemplate>
                    <asp:Literal ID="lblItemDesc" runat="server" Text='<%# Eval("Item.Description") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.UomConversion.BaseUom}">
                <ItemTemplate>
                    <asp:Literal ID="lblBaseUom" runat="server" Text='<%# Eval("BaseUom.Code") %>' />
                    [<asp:Literal ID="lblBaseUomDesc" runat="server" Text='<%# Eval("BaseUom.Description") %>' />]
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.UomConversion.BaseQty}">
                <ItemTemplate>
                    <asp:Literal ID="lblBaseQty" runat="server" Text='<%# Eval("BaseQty","{0:0.########}")  %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="lblBaseQty" runat="server" Text='<%# Bind("BaseQty","{0:0.########}")  %>'
                        Width="50" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.UomConversion.AltUom}">
                <ItemTemplate>
                    <asp:Literal ID="lblAltUom" runat="server" Text='<%# Eval("AlterUom.Code") %>' />
                    [<asp:Literal ID="lblAltUomDesc" runat="server" Text='<%# Eval("AlterUom.Description") %>' />]
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${MasterData.UomConversion.AlterQty}">
                <ItemTemplate>
                    <asp:Literal ID="lblAlterQty" runat="server" Text='<%# Eval("AlterQty","{0:0.########}")  %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="lblAlterQty" runat="server" Text='<%# Bind("AlterQty","{0:0.########}")  %>'
                        Width="50" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="true" ItemStyle-Width="100px"
                HeaderText="Action" DeleteText="&lt;span onclick=&quot;JavaScript:return confirm('${Common.Delete.Confirm}?')&quot;&gt;${Common.Button.Delete}&lt;/span&gt;">
            </asp:CommandField>
        </Columns>
    </asp:GridView>
    <asp:Label runat="server" ID="lblResult" Text="${Common.GridView.NoRecordFound}"
        Visible="false" />
</fieldset>
<asp:ObjectDataSource ID="ODS_GV_UomConversion" runat="server" TypeName="com.Sconit.Web.UomConversionMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.UomConversion" SelectMethod="LoadUomConversion"
    UpdateMethod="UpdateUomConversion" OnUpdating="ODS_GV_UomConversion_OnUpdating"
    DeleteMethod="DeleteUomConversion" OnDeleting="ODS_GV_UomConversion_OnDeleting"
    OnDeleted="ODS_GV_UomConversion_OnDeleted">
    <SelectParameters>
        <asp:ControlParameter ControlID="tbItemCode" Name="itemCode" PropertyName="Text"
            Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ODS_FV_UomConversion" runat="server" TypeName="com.Sconit.Web.UomConversionMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.UomConversion" InsertMethod="CreateUomConversion"
    OnInserted="ODS_UomConversion_Inserted" OnInserting="ODS_UomConversion_Inserting">
</asp:ObjectDataSource>
