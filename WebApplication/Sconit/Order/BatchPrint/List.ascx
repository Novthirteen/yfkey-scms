<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Order_BatchPrint_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
    function CheckAll() {
        var GV_ListId = document.getElementById("<%=GV_List.ClientID %>");
        var allselect = GV_ListId.rows[0].cells[0].getElementsByTagName("INPUT")[0].checked;
        for (i = 1; i < GV_ListId.rows.length; i++) {
            GV_ListId.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = allselect;
        }
    }

    var url;
    function Print(urls) {
        var urlArray = urls.split("|");
        for (url in urlArray) {
            var xlApp = null;
            try {
                xlApp = new ActiveXObject("Excel.Application");
            } catch (e) {
                alert("${Common.Warning.Please.Send.The.Site.To.Join.Trust.Site}");
                return;
                break;
            }
            var xlBook = xlApp.WorkBooks.open(urlArray[url]);
            var xlsheet = xlBook.Worksheets(1);
            xlsheet.PrintOut(); //打印工作表
            xlBook.Close(false); //关闭文档
            xlApp.Quit();   //结束excel对象
            xlApp = null;   //释放excel对象
        }

    }

</script>

<fieldset>
    <legend>${MasterData.Order.OrderHead.AvailableOrder}</legend>
    <div class="GridView">
        <asp:GridView ID="GV_List" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            DataKeyNames="OrderNo">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <div onclick="CheckAll()">
                            <asp:CheckBox ID="CheckAll" runat="server" />
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbOrderNo" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="OrderNo" HeaderText="${MasterData.Order.OrderHead.OrderNo.Production}"
                    SortExpression="OrderNo" />
                <asp:TemplateField HeaderText="${MasterData.Order.PrintOrder.Region}" SortExpression="PartyFrom.Code">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "PartyFrom.Code")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="StartTime" HeaderText="${MasterData.Order.OrderHead.StartTime}"
                    SortExpression="StartTime" />
                <asp:BoundField DataField="WindowTime" HeaderText="${MasterData.Order.OrderHead.WindowTime}"
                    SortExpression="WindowTime" />
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.Priority}">
                    <ItemTemplate>
                        <cc1:CodeMstrLabel ID="lblPriority" runat="server" Code="OrderPriority" Value='<%# Bind("Priority") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Status" HeaderText="${MasterData.Order.OrderHead.Status}"
                    SortExpression="Status" />
                <asp:TemplateField HeaderText="${MasterData.Order.OrderHead.CreateUser}" SortExpression="CreateUser.Name">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "CreateUser.Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblMessage" runat="server" Visible ="false" />
    </div>
</fieldset>
<div class="tablefooter">
    <cc1:Button ID="btnPrint" runat="server" Text="${Common.Button.Print}" CssClass="button2"
        OnClick="btnPrint_Click" FunctionId="PrintOrder" />
</div>
