<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductLineUser.ascx.cs"
    Inherits="Mes_ProductLineUser_ProductLineUser" %>
    
<%@ Register Src="~/Controls/TextBox.ascx" TagName="textbox" TagPrefix="uc3" %>

<script type="text/javascript" language="javascript">
function idNotInProductLineClick()
{
    if($("#idNotInProductLine input:checkbox").attr("checked")==true)
    {
        $("#idNotInProductLineList input:checkbox").each(function(index,domEle){ 
                  if(this.type=="checkbox")
                       this.checked=true;
        });
     }
    else
    {
        $("#idNotInProductLineList input:checkbox").each(function(index,domEle){ 
                  if(this.type=="checkbox")
                       this.checked=false;
        });
    }
}

function idInProductLineClick()
{
    if($("#idInProductLine input:checkbox").attr("checked")==true)
    {
        $("#idInProductLineList input:checkbox").each(function(index,domEle){ 
                  if(this.type=="checkbox")
                       this.checked=true;
        });
     }
    else
    {
        $("#idInProductLineList input:checkbox").each(function(index,domEle){ 
                  if(this.type=="checkbox")
                       this.checked=false;
        });
    }
}
</script>

<fieldset>
   
    <table width="100%">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblUser" runat="server" Text="${Common.Business.User}:" />
            </td>
            <td class="td02">
                <uc3:textbox ID="tbUser" runat="server" Visible="true" Width="250" DescField="Name"
                    ValueField="Code" ServicePath="UserMgr.service" ServiceMethod="GetAllUser"
                    CssClass="inputRequired" />
                <asp:RequiredFieldValidator ID="rfvUser" runat="server" ErrorMessage="${}"
                    Display="Dynamic" ControlToValidate="tbUser" ValidationGroup="vgSearch" />
            </td>
            
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                    CssClass="button2" ValidationGroup="vgSearch" />
                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click" CssClass="button2" />
            </td>
        </tr>
    </table>
</fieldset>
<fieldset>
    <table width="100%">
        <tr>
            <td style="width: 10%">
                ${Mes.ProductLineUser.NotInProductLine}:
            </td>
            <td style="width: 30%" id="idNotInProductLine" onclick="idNotInProductLineClick()">
                <asp:CheckBox ID="cb_NotInProductLine" runat="server" Text="${Common.Select.All}" />
            </td>
            <td style="width: 10%">
            </td>
            <td style="width: 10%">
                ${Mes.ProductLineUser.InProductLine}:
            </td>
            <td style="width: 30%" id="idInProductLine" onclick="idInProductLineClick()">
                <asp:CheckBox ID="cb_InProductLine" runat="server" Text="${Common.Select.All}" />
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
            </td>
            <td style="width: 30%" valign="top">
                <div class="scrolly" id="idNotInProductLineList">
                    <asp:CheckBoxList ID="CBL_NotInProductLine" runat="server" DataSourceID="ODS_ProductLinesNotInUser"
                        DataTextField="Description" DataValueField="Code">
                    </asp:CheckBoxList>
                    <asp:ObjectDataSource ID="ODS_ProductLinesNotInUser" runat="server" SelectMethod="GetProductLinesNotInUser"
                        TypeName="com.Sconit.Web.FlowMgrProxy" DataObjectTypeName="com.Sconit.Entity.MasterData.Flow">
                        <SelectParameters>
                            <asp:Parameter Name="code" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </td>
            <td valign="middle" align="center" colspan="2">
                <asp:Button ID="ToInBT" runat="server" Text=">>>" OnClick="ToInBT_Click" CssClass="button2" />
                <br />
                <br />
                <asp:Button ID="ToOutBT" runat="server" Text="<<<" OnClick="ToOutBT_Click" CssClass="button2" />
            </td>
            <td style="width: 30%" valign="top">
                <div class="scrolly" id="idInProductLineList">
                    <asp:CheckBoxList ID="CBL_InProductLine" runat="server" DataSourceID="ODS_ProductLinesInUser"
                        DataTextField="Description" DataValueField="Code">
                    </asp:CheckBoxList>
                    <asp:ObjectDataSource ID="ODS_ProductLinesInUser" runat="server" SelectMethod="GetProductLinesInUser"
                        TypeName="com.Sconit.Web.FlowMgrProxy" DataObjectTypeName="com.Sconit.Entity.MasterData.Flow">
                        <SelectParameters>
                            <asp:Parameter Name="code" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </td>
        </tr>
    </table>
</fieldset>
