<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Hu_List" %>
<%@ Register Src="~/Controls/UpdateProgress.ascx" TagName="UpdateProgress" TagPrefix="uc2" %>
<div id="floatdiv">
    <fieldset>
        <legend>${MasterData.Hu.List}</legend>
        <asp:UpdatePanel ID="UP_GV_List" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="No." ItemStyle-Width="30">
                            <ItemTemplate>
                                <asp:Literal ID="ltlNo" runat="server" Text='<%# (Container as GridViewRow).RowIndex+1 %> ' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataHuid%>" ItemStyle-Width="200px">
                            <ItemTemplate>
                                <asp:Label ID="lblHuId" runat="server" Text='<%# Eval("HuId") %>' />
                                <asp:TextBox ID="tbHuId" runat="server" Text='<%# Bind("HuId") %>' Visible="false"
                                    AutoPostBack="true" OnTextChanged="tbHuId_TextChanged" onclick="this.select();" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemCode%>">
                            <ItemTemplate>
                                <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("Item.Code") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$Resources:Language,MasterDataItemDesc%>">
                            <ItemTemplate>
                                <asp:Label ID="lblItemDesc" runat="server" Text='<%# Bind("Item.Description") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LotNo" HeaderText="<%$Resources:Language,MasterDataLotNo%>" />
                        <asp:BoundField DataField="Qty" DataFormatString="{0:0.########}" HeaderText="<%$Resources:Language,MasterDataQty%>" />
                        <asp:TemplateField HeaderText="<%$Resources:Language,ButtonDelete%>" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDeleteHu" runat="server" Text="<%$Resources:Language,ButtonDelete%>"
                                    OnClick="lbtnDelete_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <uc2:UpdateProgress ID="ucUpdateProgress" runat="server" />
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btn_Hu_Back" />
                <asp:PostBackTrigger ControlID="btn_Hu_Create" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="tablefooter">
            <asp:Button ID="btn_Hu_Create" runat="server" Text="${MasterData.Hu.Save}" OnClick="btn_Hu_Create_Click" />
            <asp:Button ID="btn_Hu_Back" runat="server" Text="Back" OnClick="btn_Hu_Back_Click" />
        </div>
    </fieldset>
</div>
