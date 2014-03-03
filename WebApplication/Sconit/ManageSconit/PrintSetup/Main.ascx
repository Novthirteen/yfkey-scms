<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="ManageSconit_PrintSetup_Main" %>
<fieldset>
    <legend>${Print.Setup.Management}</legend>
    <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_List_OnRowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="${Print.Setup.Name}">
                <ItemTemplate>
                    <asp:Literal ID="ltlName" runat="server" Text='<%# Bind("Description") %>' />
                    <asp:HiddenField ID="hfCode" runat="server" Value='<%# Bind("Code") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Print.Setup.CurrentStatus}">
                <ItemTemplate>
                    <asp:Literal ID="ltlCurrentStatus" runat="server" Text='<%# Bind("Status") %>' Visible="false"/>
                    <asp:Literal ID="ltlStatus" runat="server"  />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="${Common.GridView.Action}">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnStart" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>'
                        Text="${Common.Button.Start}" OnClick="lbnControl_Click">
                    </asp:LinkButton>                    
                    <asp:LinkButton ID="lbtnStop" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Code") %>'
                        Text="${Common.Button.Stop}" OnClick="lbnControl_Click">
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</fieldset>
