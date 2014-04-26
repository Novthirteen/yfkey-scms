<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DetailList.ascx.cs" Inherits="EDI_FordPlan_DetailList" %>
<%@ Register Assembly="com.Sconit.Control" Namespace="com.Sconit.Control" TagPrefix="cc1" %>
<style type="text/css">
    .Listtable
    {
        border: 0.1px solid #6F6F6F;
        text-align: center;
        margin: 0;
        border-collapse: collapse;
        border-spacing: 0;
        width:100%;
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
    });

</script>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
            当前版本号:
            </td>
            <td class="td02" style="font-weight: bold;">
            <%=control_num %>
            </td>
            <td />
            <td class="td02">
                <div class="buttons">
                    <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
            CssClass="button2" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
<fieldset>
    <div class="GridView">
        <% if (allList != null && allList.Count > 0)
           { %>
        <table class="Listtable" id="listTable">
            <thead class="Listthead" id="listThead">
                <tr class="Listtr">
                   <%-- <td>
                        <%="${EDI.EDIFordPlanBase.Control_Num}"%>
                    </td>--%>
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
                    <%
               if (forecastDateList!=null && forecastDateList.Count > 0)
               {
                   foreach (var forecastDate in forecastDateList)
                   {%>
                    <td>
                        <%=forecastDate.ToLongDateString() %>
                    </td>
                 <%  }
               }
                         %>
                    <td colspan="15">
                    </td>
                </tr>
            </thead>
            <tbody id="listTbody">
                <%
               int i = 1;
               foreach (var fordPlan in allList)
               {
                %>
                <tr class="<%=i%2==0?"Listtr":"Listtr02" %>">
                  <%--  <td>
                        <%=fordPlan.Control_Num%>
                    </td>--%>
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
                   foreach (var d in fordPlan.PlanQtyArr)
                   {%>
                    <td >
                        <%=d[0] %>
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
                    </td>
                    <% 
                   foreach (var d in fordPlan.PlanQtyArr)
                   {%>
                    <td >
                        <%=d[1] %>
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
                <td colspan="20" style="border: 1px solid #CFCFC9">总共【<span id="totalItem"><%=totalItem %></span>】项
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
