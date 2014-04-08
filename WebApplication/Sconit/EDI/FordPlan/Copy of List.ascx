<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Copy of List.ascx.cs" Inherits="EDI_FordPlan_List" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<style type="text/css">
    .Listtable
    {
        border: 0.1px solid #6F6F6F;
        text-align: center;
        margin: 0;
        border-collapse: collapse;
        border-spacing: 0;
    }
    .Listthead
    {
        background-color: #666666;
        border: 0.1px solid #6F6F6F;
    }
    
    .Listtd
    {
        border: 0.2px solid #CFCFC9;
    }
    
    
    .Listtr
    {
        height: 30px;
    }
    .Listtr02
    {
        height: 30px;
        background-color: #CFCFC9;
    }
    
    .footSpan
    {
        color:#6600FF;
        }
</style>
<script type="text/javascript">
    $(function () {
        //        $("#listTable td").css("padding", "0");
        $("#listTable td").css("padding-left", "3");
        $("#listTable td").css("padding-right", "3");
        $("#listThead td").css("border", "1px solid #D7D7D2");
        $("#listThead td").css("color", "#FFFFFF");
        $("#listThead td").css("font-weight", "bold");
        $("#listTbody td").css("border-left", "1px solid #B8B8AD");
        $("#listTbody td").css("border-right", "1px solid #B8B8AD");
        //        $("#btPrev").attr("disabled",<%=isMinPage %>);
        //        $("#btNext).attr("disabled",<%=isMaxPage %>);
    });

    function pageClick(e) {
//    debugger
//        var data = {
//            
//        }
//        $.ajax({
//            type: "Get",
//            dataType: "Json",
//            data: {"clickType":e},
//            url: "EDI/FordPlan/List.ascx/GetView2",
//            cache: false,
//            success: function (data, textStatus) {

//            },
//            error: function (XMLHttpRequest, textStatus, errorThrown) {
//            debugger
//                alert(errorThrown);
//            }
        //        });
    }
</script>
<fieldset>
    <div class="GridView">
        <% if (returnList != null && returnList.Count > 0)
           { %>
        <table class="Listtable" id="listTable">
            <thead class="Listthead" id="listThead">
                <tr class="Listtr">
                    <td>
                        <%="${EDI.EDIFordPlanBase.Control_Num}"%>
                    </td>
                    <td>
                        <%="${EDI.EDIFordPlanBase.ReleaseIssueDate}"%>
                    </td>
                    <%--<td>
                        <%="${EDI.EDIFordPlanBase.Type}"%>
                    </td>--%>
                    <td>
                        <%="${EDI.EDIFordPlanBase.Item}"%>
                    </td>
                    <td>
                        <%="${EDI.EDIFordPlanBase.ItemDesc}"%>
                    </td>
                    <td>
                        <%="${EDI.EDIFordPlanBase.RefItem}"%>
                    </td>
                    <td>
                        <%="${EDI.EDIFordPlanBase.Uom}"%>
                    </td>
                    <%--  <td>
                        <%="${EDI.EDIFordPlanBase.LastShippedQuantity}"%>
                    </td>
                    <td>
                        <%="${EDI.EDIFordPlanBase.LastShippedCumulative}"%>
                    </td>
                    <td>
                        <%="${EDI.EDIFordPlanBase.LastShippedDate}"%>
                    </td>--%>
                    <td colspan="15">
                    </td>
                </tr>
            </thead>
            <tbody id="listTbody">
                <%
               int i = 1;
               foreach (var fordPlan in returnList)
               {
                %>
                <tr class="<%=i%2==0?"Listtr":"Listtr02" %>">
                    <td>
                        <%=fordPlan.Control_Num%>
                    </td>
                    <td>
                        <%=fordPlan.ReleaseIssueDate.ToShortDateString()%>
                    </td>
                    <%-- <td>
                        <%=fordPlan.Type%>
                    </td>--%>
                    <td>
                        <%=fordPlan.Item%>
                    </td>
                    <td>
                        <%=fordPlan.ItemDesc%>
                    </td>
                    <td>
                        <%=fordPlan.RefItem%>
                    </td>
                    <td>
                        <%=fordPlan.Uom%>
                    </td>
                    <%--                    <td>
                        <%=fordPlan.LastShippedQuantity%>
                    </td>
                    <td>
                        <%=fordPlan.LastShippedCumulative%>
                    </td>
                    <td>
                        <%=fordPlan.LastShippedDate%>
                    </td>--%>
                    <% 
                   foreach (var Dic in fordPlan.PlanDateDic)
                   {%>
                    <td style="font-weight: bold; padding-left: 5px; padding-right: 5px">
                        <%=Dic.Key.ToShortDateString()%>
                    </td>
                    <% }
                   i++;
                       
                    %>
                </tr>
                <tr class="<%=i%2==0?"Listtr":"Listtr02" %>">
                    <%-- <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>--%>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        需求净值
                    </td>
                    <td>
                    </td>
                    <% 
                   foreach (var Dic in fordPlan.PlanDateDic)
                   {%>
                    <td>
                        <%=Dic.Value[0]%>
                    </td>
                    <% }
                   i++;
                       
                    %>
                </tr>
                <tr class="<%=i%2==0?"Listtr":"Listtr02" %>">
                    <%-- <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>--%>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                        累计需求量
                    </td>
                    <td>
                    </td>
                    <% 
                   foreach (var Dic in fordPlan.PlanDateDic)
                   {%>
                    <td>
                        <%=Dic.Value[1]%>
                    </td>
                    <% }
                   i++;
                       
                    %>
                </tr>
                <%    }
                %>
            </tbody>
            <tfoot >
            <tr style="height:30px; text-align:left;border: 1px solid #CFCFC9">
                
                <%--<td colspan="20">总共【<span id="Span1"><%=totalItem %></span>】项,当前显示【<span id="Span2"><%=currentItem01%></span>～<span id="Span3"><%=currentItem02%></span>】项，<span class="footSpan" onclick="pageClick(1)">首页</span>|<span class="footSpan" onclick="pageClick(2)">上一页</span>|<span class="footSpan" onclick="pageClick(3)">下一页</span>|<span class="footSpan" onclick="pageClick(4)">尾页</span></td>--%>
                <td colspan="20" style="border: 1px solid #CFCFC9">总共【<span id="totalItem"><%=totalItem %></span>】项,当前显示【<span id="currentItem01"><%=currentItem01%></span>～<span id="currentItem02"><%=currentItem02%></span>】项，
                <asp:Button ID="btFistr" runat="server" Text="首页" OnClick="btnFirst_Click"  />
                <asp:Button ID="btPrev" runat="server" Text="上一页" OnClick="btnPrev_Click"  />
                <asp:Button ID="btNext" runat="server" Text="下一页" OnClick="btnNext_Click"  />
                <asp:Button ID="btLast" runat="server" Text="尾页" OnClick="btnLast_Click" />
                </td>
            </tr>
            </tfoot>
        </table>
        <%}
           else { 
           %>
           没有符合条件的记录。
          <% } %>
    </div>
</fieldset>
