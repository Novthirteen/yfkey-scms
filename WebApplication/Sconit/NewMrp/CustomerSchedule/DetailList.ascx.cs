using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.MRP;
using com.Sconit.Web;
using com.Sconit.Utility;
using com.Sconit.Entity.Procurement;
using System.Data.SqlClient;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;

public partial class NewMrp_ShipPlan_DetailList : MainModuleBase
{
    public static string currentRefScheduleNo = string.Empty;
    public event EventHandler BackEvent;
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.tbFlow.ServiceParameter = "string:" + this.Cur
        //this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code + ",bool:false,bool:false,bool:true,bool:false,bool:false,bool:false,string:" + BusinessConstants.PARTY_AUTHRIZE_OPTION_BOTH;

        if (!IsPostBack)
        {
            //this.tbFlow.Text = string.Empty;
        }

    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
       
    }

    #region   明细查询
    public void GetView(string refScheduleNo)
    {
        //this.tbFlow.Text = string.Empty;
        this.list.InnerHtml = "";
        currentRefScheduleNo = refScheduleNo;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        var hql = "select c from CustomerScheduleDetail as c where 1=1 ";
        var paramList = new List<object>();
        hql += " and c.ReferenceScheduleNo=? ";
        paramList.Add(currentRefScheduleNo);
        //if (!string.IsNullOrEmpty(this.tbFlow.Text.Trim()))
        //{
        //    hql += " and c.Flow =? ";
        //    paramList.Add(this.tbFlow.Text.Trim());
        //}
       
        if (!string.IsNullOrEmpty(this.tbItemCode.Text.Trim()))
        {
            hql += " and c.Item =? ";
            paramList.Add(this.tbItemCode.Text.Trim());
        }

        var customerPlanList = this.TheGenericMgr.FindAllWithCustomQuery<CustomerScheduleDetail>(hql, paramList.ToArray()) ?? new List<CustomerScheduleDetail>();
       
        ListTable(customerPlanList);
    }

    private void ListTable(IList<CustomerScheduleDetail> customerPlanList)
    {
        if (customerPlanList == null || customerPlanList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            this.ltlPlanVersion.Text = string.Empty;
            return;
        }

        var planByDateIndexs = customerPlanList.GroupBy(p => p.DateFrom).OrderBy(p => p.Key);
        var planByFlowItems = customerPlanList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item });

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
        //var flowCode = this.tbFlow.Text.Trim();
        string headStr = string.Empty;
        //CopyString();
        str.Append("<thead><tr class='GVHeader'><th>序号</th><th>路线</th><th>版本号</th><th>物料号</th><th>物料描述</th><th>客户零件号</th>");
        int ii = 0;
        foreach (var planByDateIndex in planByDateIndexs)
        {
            ii++;
            str.Append("<th>");
            str.Append(planByDateIndex.Key.ToString("yyyy-MM-dd"));
            str.Append("</th>");
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");

        #region  通过长度控制table的宽度
        string widths = "100%";
        if (ii > 25)
        {
            widths = "300%";
        }
        else if (ii > 20)
        {
            widths = "240%";
        }
        else if (ii > 15)
        {
            widths = "190%";
        }
        else if (ii > 10)
        {
            widths = "150%";
        }
        else if (ii > 6)
        {
            widths = "120%";
        }
        //else if (ii > 4)
        //{
        //    widths = "170%";
        //}
        //else if (ii > 2)
        //{
        //    widths = "130%";
        //}
        headStr += string.Format("<table border='1' class='GV' style='width:{0};border-collapse:collapse;'>", widths);
        #endregion
        int l = 0;
        foreach (var planByFlowItem in planByFlowItems)
        {
            var firstPlan = planByFlowItem.First();
            var planDic = planByFlowItem.GroupBy(d => d.DateFrom).ToDictionary(d => d.Key, d => d.Sum(q => q.Qty));
            l++;
            if (l % 2 == 0)
            {
                str.Append("<tr class='GVAlternatingRow'>");
            }
            else
            {
                str.Append("<tr class='GVRow'>");
            }
            str.Append("<td>");
            str.Append(l);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.Flow);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ReferenceScheduleNo);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(planByFlowItem.Key.Item);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ItemDescription);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ItemReference);
            str.Append("</td>");
            foreach (var planByDateIndex in planByDateIndexs)
            {
                var qty = planDic.Keys.Contains(planByDateIndex.Key) ? planDic[planByDateIndex.Key] : 0;

                str.Append("<td>");
                str.Append(qty.ToString("0.##"));
                str.Append("</td>");
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.list.InnerHtml = headStr + str.ToString();
    }

    #endregion


    protected void btnRunProdPlan_Click(object sender, EventArgs e)
    {
        try
        {
            TheMrpMgr.RunProductionPlan(this.CurrentUser);
            ShowSuccessMessage("生成成功。");
        }
        catch (SqlException ex)
        {
            ShowErrorMessage(ex.Message);
        }
        catch (Exception ee)
        {
            ShowErrorMessage(ee.Message);
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(sender, e);
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        var hql = "select c from CustomerScheduleDetail as c where 1=1 ";
        var paramList = new List<object>();
        hql += " and c.ReferenceScheduleNo=? ";
        paramList.Add(currentRefScheduleNo);

        if (!string.IsNullOrEmpty(this.tbItemCode.Text.Trim()))
        {
            hql += " and c.Item =? ";
            paramList.Add(this.tbItemCode.Text.Trim());
        }

        var customerPlanList = this.TheGenericMgr.FindAllWithCustomQuery<CustomerScheduleDetail>(hql, paramList.ToArray()) ?? new List<CustomerScheduleDetail>();

        ExportDailyExcel(customerPlanList);
    }

    private void ExportDailyExcel(IList<CustomerScheduleDetail> exportList)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        Sheet sheet1 = hssfworkbook.CreateSheet("sheet1");
        MemoryStream output = new MemoryStream();

        if (exportList != null && exportList.Count > 0)
        {
            var planByDateIndexs = exportList.GroupBy(p => p.DateFrom).OrderBy(p => p.Key);
            var planByFlowItems = exportList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item });

            #region 写入字段
            Row rowHeader = sheet1.CreateRow(0);
           // <th>路线</th><th>版本号</th><th>物料号</th><th>物料描述</th><th>客户零件号</th>");
            for (int i = 0; i < 6 + planByDateIndexs.Count(); i++)
            {
                if (i == 0) //序号
                {
                    rowHeader.CreateCell(i).SetCellValue("序号");
                }
                else if (i == 1)  //路线
                {
                    rowHeader.CreateCell(i).SetCellValue("路线");
                }
                else if (i == 2) //提前期
                {
                    rowHeader.CreateCell(i).SetCellValue("版本号");
                }
                else if (i == 3)    //物料号
                {
                    rowHeader.CreateCell(i).SetCellValue("物料号");
                }
                else if (i == 4)    //物料描述
                {
                    rowHeader.CreateCell(i).SetCellValue("物料描述");
                }
                else if (i == 5)      //客户零件号
                {
                    rowHeader.CreateCell(i).SetCellValue("客户零件号");
                }
                else
                {
                    foreach (var date in planByDateIndexs)
                    {
                        rowHeader.CreateCell(i++).SetCellValue(date.Key.ToShortDateString());
                    }
                }
            }
            #endregion

            #region 写入数值
            int l = 0;
            int rowIndex = 1;
            int seq = 0;
            foreach (var planByFlowItem in planByFlowItems)
            {
                var firstPlan = planByFlowItem.First();
                var planDic = planByFlowItem.GroupBy(d => d.DateFrom).ToDictionary(d => d.Key, d => d.Sum(q => q.Qty));
                l++;
                Row rowDetail = sheet1.CreateRow(rowIndex);
                int cell = 0;
                // <th>路线</th><th>版本号</th><th>物料号</th><th>物料描述</th><th>客户零件号</th>");
                rowDetail.CreateCell(cell++).SetCellValue(l);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.Flow);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.ReferenceScheduleNo);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.Item);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.ItemDescription);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.ItemReference);
                foreach (var planByDateIndex in planByDateIndexs)
                {
                    var qty = planDic.Keys.Contains(planByDateIndex.Key) ? planDic[planByDateIndex.Key] : 0;
                    var createShip = rowDetail.CreateCell(cell++);
                    createShip.SetCellType(CellType.NUMERIC);
                    createShip.SetCellValue(Convert.ToDouble(qty));
                }

                rowIndex++;
            }
            #endregion

            hssfworkbook.Write(output);

            string filename = "CustomerDetail.xls";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();
            Response.BinaryWrite(output.GetBuffer());
            Response.End();
            //return File(output, contentType, exportName + "." + fileSuffiex);
        }
    }

    private void ExportWeeklyExcel(IList<ShipPlanDet> exportList)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        Sheet sheet1 = hssfworkbook.CreateSheet("sheet1");
        MemoryStream output = new MemoryStream();

        if (exportList != null && exportList.Count > 0)
        {
            var planByDateIndexs = exportList.GroupBy(p => p.StartTime).OrderBy(p => p.Key);
            var planByFlowItems = exportList.OrderBy(p => p.Flow).GroupBy(p => new { p.Flow, p.Item, p.LocFrom, p.LocTo });
            #region 写入字段
            Row rowHeader = sheet1.CreateRow(0);
            Row rowHeader2 = sheet1.CreateRow(1);
            // str.Append("<th rowspan='2'>包装量</th><th rowspan='2'>安全库存</th><th rowspan='2'>最大库存</th>");
            for (int i = 0; i < 9 + planByDateIndexs.Count() * 3; i++)
            {
                if (i == 0) //序号
                {
                    rowHeader.CreateCell(i).SetCellValue("序号");
                }
                else if (i == 1)  //路线
                {
                    rowHeader.CreateCell(i).SetCellValue("路线");
                }
                else if (i == 2) //提前期
                {
                    rowHeader.CreateCell(i).SetCellValue("提前期");
                }
                else if (i == 3)    //物料号
                {
                    rowHeader.CreateCell(i).SetCellValue("物料号");
                }
                else if (i == 4)    //物料描述
                {
                    rowHeader.CreateCell(i).SetCellValue("物料描述");
                }
                else if (i == 5)      //客户零件号
                {
                    rowHeader.CreateCell(i).SetCellValue("客户零件号");
                }
                else if (i == 6)      //包装量
                {
                    rowHeader.CreateCell(i).SetCellValue("包装量");
                }
                else if (i == 7)      //安全库存
                {
                    rowHeader.CreateCell(i).SetCellValue("安全库存");
                }
                else if (i == 8)      //最大库存
                {
                    rowHeader.CreateCell(i).SetCellValue("最大库存");
                }
                else
                {
                    foreach (var date in planByDateIndexs)
                    {
                        rowHeader.CreateCell(i).SetCellValue(date.Key.ToShortDateString());
                        int i2 = i;
                        rowHeader2.CreateCell(i2++).SetCellValue("需求数");
                        rowHeader2.CreateCell(i2++).SetCellValue("订单数");
                        rowHeader2.CreateCell(i2++).SetCellValue("发货数");
                        i += 3;
                    }
                }
            }
            #endregion

            #region 写入数值
            int l = 0;
            int rowIndex = 2;
            foreach (var planByFlowItem in planByFlowItems)
            {
                var firstPlan = planByFlowItem.First();
                var planDic = planByFlowItem.GroupBy(d => d.StartTime).ToDictionary(d => d.Key, d => d.Sum(q => q.ShipQty));
                l++;
                Row rowDetail = sheet1.CreateRow(rowIndex);
                int cell = 0;
                rowDetail.CreateCell(cell++).SetCellValue(l);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.Flow);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.MrpLeadTime.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.Item);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.ItemDesc);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.RefItemCode);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.UnitCount.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.SafeStock.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.MaxStock.ToString("0.##"));
                foreach (var planByDateIndex in planByDateIndexs)
                {
                    var curenPlan = planByFlowItem.Where(p => p.StartTime == planByDateIndex.Key);
                    var shipPlanDet = curenPlan.Count() > 0 ? curenPlan.First() : new ShipPlanDet();
                    var createCell = rowDetail.CreateCell(cell++);
                    createCell.SetCellType(CellType.NUMERIC);
                    createCell.SetCellValue(Convert.ToDouble(shipPlanDet.ReqQty));

                    var createCell2 = rowDetail.CreateCell(cell++);
                    createCell2.SetCellType(CellType.NUMERIC);
                    createCell2.SetCellValue(Convert.ToDouble(shipPlanDet.OrderQty));

                    var createShip = rowDetail.CreateCell(cell++);
                    createShip.SetCellType(CellType.NUMERIC);
                    createShip.SetCellValue(Convert.ToDouble(shipPlanDet.ShipQty));
                }

                rowIndex++;
            }
            #endregion

            hssfworkbook.Write(output);

            string filename = "ShipPlanWeekly.xls";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();
            Response.BinaryWrite(output.GetBuffer());
            Response.End();
            //return File(output, contentType, exportName + "." + fileSuffiex);
        }
    }
}