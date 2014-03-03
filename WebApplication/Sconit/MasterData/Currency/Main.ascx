<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MasterData_Currency_Main" %>
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>
<fieldset id="fldSearch" runat="server">
    <table class="mtable">
        <tr>
            <td class="ttd01">
                <asp:Literal ID="ltlCode" runat="server" Text="${MasterData.Currency.Code}:" />
            </td>
            <td class="ttd02">
                <asp:TextBox ID="tbCode" runat="server"></asp:TextBox>
            </td>
            <td class="ttd01">
                <asp:Literal ID="ltlName" runat="server" Text="${MasterData.Currency.Name}:" />
            </td>
            <td class="ttd02">
                <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <td colspan="3">
        </td>
        <td class="ttd02">
            <asp:Button ID="SearchBtn" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                CssClass="button2" />
            <asp:Button ID="btnInsert" runat="server" Text="${Common.Button.New}" OnClick="btnInsert_Click"
                CssClass="button2" />
        </td>
        <tr>
        </tr>
    </table>
</fieldset>
<fieldset id="fldInsert" runat="server" visible="false">
    <legend>${MasterData.Currency.NewCurrency}</legend>
    <asp:FormView ID="FV_Currency" runat="server" DataSourceID="ODS_FV_Currency" DefaultMode="Insert">
        <InsertItemTemplate>
            <table class="mtable">
                <tr>
                    <td class="ttd01">
                        <asp:Literal ID="ltlCode" runat="server" Text="${MasterData.Currency.Code}:" />
                    </td>
                    <td class="ttd02">
                        <asp:TextBox ID="tbCode" runat="server" Text='<%# Bind("Code") %>'></asp:TextBox>
                    </td>
                    <td class="ttd01">
                        <asp:Literal ID="ltlName" runat="server" Text="${MasterData.Currency.Name}:" />
                    </td>
                    <td class="ttd02">
                        <asp:TextBox ID="tbName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
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
<fieldset id="fldGV" runat="server" visible="false">
    <asp:GridView ID="GV_Currency" runat="server" AutoGenerateColumns="False" DataSourceID="ODS_GV_Currency"
        OnDataBound="GV_Currency_OnDataBind" DataKeyNames="Code" AllowPaging="True" PageSize="10">
        <Columns>
            <asp:BoundField ReadOnly="true" DataField="Code" HeaderText="${MasterData.Currency.Code}"
                ItemStyle-Width="40%" />
            <asp:BoundField DataField="Name" HeaderText="${MasterData.Currency.Name}" ItemStyle-Width="40%" />
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="true" ItemStyle-Width="20%"
                HeaderText="${Common.GridView.Action}" DeleteText="&lt;span onclick=&quot;JavaScript:return confirm('${Common.Delete.Confirm}?')&quot;&gt;${Common.Button.Delete}&lt;/span&gt;">
            </asp:CommandField>
        </Columns>
    </asp:GridView>
    <asp:Label runat="server" ID="lblResult" Text="${Common.GridView.NoRecordFound}"
        Visible="false" />
</fieldset>
<asp:ObjectDataSource ID="ODS_GV_Currency" runat="server" TypeName="com.Sconit.Web.CurrencyMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Currency" SelectMethod="LoadCurrency"
    UpdateMethod="UpdateCurrency" OnUpdating="ODS_GV_Currency_OnUpdating" DeleteMethod="DeleteCurrency"
    OnDeleting="ODS_GV_Currency_OnDeleting" OnDeleted="ODS_GV_Currency_OnDeleted">
    <SelectParameters>
        <asp:ControlParameter ControlID="tbCode" Name="Code" PropertyName="Text" Type="String" />
        <asp:ControlParameter ControlID="tbName" Name="Name" PropertyName="Text" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ODS_FV_Currency" runat="server" TypeName="com.Sconit.Web.CurrencyMgrProxy"
    DataObjectTypeName="com.Sconit.Entity.MasterData.Currency" InsertMethod="CreateCurrency"
    OnInserted="ODS_Currency_Inserted" OnInserting="ODS_Currency_Inserting"></asp:ObjectDataSource>
