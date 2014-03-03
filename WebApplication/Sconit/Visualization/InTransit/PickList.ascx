<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PickList.ascx.cs" Inherits="Visualization_InTransit_PickList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<div id="floatdiv">
    <div id='floatdivtitle'>
        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" CssClass="btnClose" />
    </div>
    <fieldset>
        <legend>${Reports.ViewDetail}</legend>
        <div class="GridView">
            <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" OnRowDataBound="GV_List_RowDataBound">
                <Columns>
                    <asp:TemplateField  HeaderText="${MasterData.Distribution.PickList}" >
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "PickListDetail.PickList.PickListNo")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="${Common.Business.OrderNo}" >
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "PickListDetail.OrderLocationTransaction.OrderDetail.OrderHead.OrderNo")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${MasterData.Distribution.PickList.CreateUser}">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "PickListDetail.PickList.CreateUser.Name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${MasterData.Distribution.PickList.CreateDate}">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "PickListDetail.PickList.CreateDate")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.HuId}" >
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "LocationLotDetail.Hu.HuId")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="${Common.Business.Qty}" >
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Qty", "{0:0.###}")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </fieldset>
</div>
<div id='divHide' />
