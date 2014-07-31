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
using NPOI.SS.UserModel;
using System.IO;

public partial class NewMrp_Shift_DetailList : MainModuleBase
{
    public static string currentRelesNo = string.Empty;
    public event EventHandler BackEvent;
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    #region   明细查询
    public void GetView(string relesNo)
    {
        this.tbFlow.Value = string.Empty;
        this.list.InnerHtml = "";
        currentRelesNo = relesNo;
       
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.btQtyHidden.Value = string.Empty;
        this.btSeqHidden.Value = string.Empty;
        var searchSql = @"select m.ReleaseNo,m.Status,s.ProdLine,s.Id,s.Item,s.Itemdesc,s.Qty,s.Uom,s.UC,s.PlanDate,s.Shift,isnull(t1.Qty,0) OrderQty  from MRP_ShiftPlanDet s inner join MRP_ShiftPlanMstr m on m.Id=s.MstrId
 left join (select d.Item,sum((d.OrderQty-d.RecQty)) as Qty,m.StartTime,m.Flow,m.Shift from  OrderDet d inner join OrderMstr m on m.OrderNo=d.OrderNo where m.Type='Production' and m.Status='Submit' and d.OrderQty>d.RecQty group by d.Item,m.StartTime,m.Flow,m.Shift ) t1 on s.ProdLine=t1.Flow and s.Item=t1.Item and s.Shift=t1.Shift and CONVERT(varchar(10), s.PlanDate, 121)=CONVERT(varchar(10), t1.StartTime, 121) where 1=1 ";

        if (!string.IsNullOrEmpty(this.tbItemCode.Text.Trim()))
        {
            searchSql += string.Format(" and s.Item ='{0}' ", this.tbItemCode.Text.Trim());
        }

        if (!string.IsNullOrEmpty(this.tbFlow.Value.Trim()))
        {
            searchSql += string.Format(" and s.ProdLine in ('{0}') ", this.tbFlow.Value);
        }

        if (!string.IsNullOrEmpty(currentRelesNo))
        {
            searchSql += string.Format(" and m.ReleaseNo ='{0}' ", currentRelesNo);
        }

        searchSql += " order by s.Item asc ";

        var allResult = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
        var planDetList = new List<ShiftPlanDet>();
        foreach (System.Data.DataRow row in allResult.Rows)
        {
            //m.ReleaseNo,m.Status,s.ProdLine,s.Id,s.Item,s.Itemdesc,s.Qty,s.Uom,s.UC,s.PlanDate,s.Shift,isnull(t1.Qty,0) OrderQty
            planDetList.Add(new ShiftPlanDet
            {
                ReleaseNo = Int32.Parse(row[0].ToString()),
                Status = row[1].ToString(),
                ProdLine = row[2].ToString(),
                Id = Int32.Parse(row[3].ToString()),
                Item = row[4].ToString(),
                ItemDesc = row[5].ToString(),
                Qty = Convert.ToDecimal(row[6]),
                Uom = row[7].ToString(),
                UnitCount = Convert.ToDecimal(row[8]),
                PlanDate = Convert.ToDateTime(row[9]),
                Shift = row[10].ToString(),
                OrderQty = Convert.ToDecimal(row[11]),
            });
        }
        ListTable(planDetList);
    }

    private void ListTable(IList<ShiftPlanDet> planDetList)
    {
        if (planDetList == null || planDetList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            return;
        }

        var planByDateIndexs = planDetList.GroupBy(p => p.PlanDate).OrderBy(p => p.Key);
        var planByItems = planDetList.GroupBy(p => new { p.Item ,p.ProdLine});

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
        string headStr = string.Empty;
        str.Append("<thead><tr class='GVHeader'><th rowspan='3'>序号</th><th rowspan='3'>生产线</th><th rowspan='3'>物料号</th><th rowspan='3'>物料描述</th><th rowspan='3'>包装量</th><th rowspan='3'>单位</th>");
        int ii = 0;
        foreach (var planByDateIndex in planByDateIndexs)
        {
            ii++;
            str.Append("<th colspan='6'>");
            str.Append(planByDateIndex.Key.ToString("yyyy-MM-dd"));
            str.Append("</th>");
        }
        str.Append("</tr><tr class='GVHeader'>");
        foreach (var planByDateIndex in planByDateIndexs)
        {
           
            str.Append("<th colspan='2'  backgroundcolor='#A290AD' >");
            if (planDetList.First().Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT && planByDateIndex.Key.Date == System.DateTime.Now.Date)
            {
                str.Append("<input type='checkbox' id='CheckAllA' name='CheckAllA'  onclick='doCheckAllClickA()' />");
            }
            str.Append("早班");
            str.Append("</th>");
            str.Append("<th colspan='2' backgroundcolor='#F1A94B' >");
            if (planDetList.First().Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT && planByDateIndex.Key.Date == System.DateTime.Now.Date)
            {
                str.Append("<input type='checkbox' id='CheckAllB' name='CheckAllB'  onclick='doCheckAllClickB()' />");
            }
            str.Append("中班");
            str.Append("</th>");
            str.Append("<th colspan='2' backgroundcolor='#4CF0BA'>");
            if (planDetList.First().Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT && planByDateIndex.Key.Date == System.DateTime.Now.Date)
            {
                str.Append("<input type='checkbox' id='CheckAllC' name='CheckAllC'  onclick='doCheckAllClickC()' />");
            }
            str.Append("晚班");
            str.Append("</th>");

        }
        str.Append("</tr><tr class='GVHeader'>");
        foreach (var planByDateIndex in planByDateIndexs)
        {
            str.Append("<th backgroundcolor='#A290AD' >订单数</th><th backgroundcolor='#A290AD' >计划数</th><th backgroundcolor='#F1A94B'>订单数</th><th backgroundcolor='#F1A94B'>计划数</th><th  backgroundcolor='#4CF0BA'>订单数</th><th  backgroundcolor='#4CF0BA'>计划数</th>");
        }
        str.Append("</tr></thead>");
        str.Append("<tbody>");

        #region  通过长度控制table的宽度
        string widths = "100%";
        if (ii > 14)
        {
            widths = "360%";
        }
        else if (ii > 10)
        {
            widths = "280%";
        }
        else if (ii > 6)
        {
            widths = "200%";
        }
        else if (ii > 4)
        {
            widths = "120%";
        }

        headStr += string.Format("<table id='tt' runat='server' border='1' class='GV' style='width:{0};border-collapse:collapse;'>", widths);
        #endregion
        int l = 0;
        int seq = 0;
        foreach (var planByFlowItem in planByItems)
        {
            var firstPlan = planByFlowItem.First();
            var planDic = planByFlowItem.GroupBy(d => d.PlanDate).ToDictionary(d => d.Key, d => d.Sum(q => q.Qty));
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
            str.Append(firstPlan.ProdLine);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.Item);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ItemDesc);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.UnitCount.ToString("0.##"));
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.Uom);
            str.Append("</td>");

            foreach (var planByDateIndex in planByDateIndexs)
            {
                #region
                var shiftplansByDate = planByFlowItem.Where(s => s.PlanDate == planByDateIndex.Key);
                shiftplansByDate = shiftplansByDate.Count() > 0 ? shiftplansByDate : new List<ShiftPlanDet>();

                var aShift = shiftplansByDate.Where(s => s.Shift.ToUpper() == "A");
                var bShift = shiftplansByDate.Where(s => s.Shift.ToUpper() == "B");
                var cShift = shiftplansByDate.Where(s => s.Shift.ToUpper() == "C");
                var shiftPA = aShift.Count() > 0 ? aShift.First() : new ShiftPlanDet { Shift="A"};
                var shiftPB = bShift.Count() > 0 ? bShift.First() : new ShiftPlanDet { Shift="B"};
                var shiftPC = cShift.Count() > 0 ? cShift.First() : new ShiftPlanDet { Shift="C"};

                if (firstPlan.Status == BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE)
                {
                    seq++;
                    str.Append("<td >");
                    str.Append(shiftPA.OrderQty.ToString("0.##"));
                    str.Append("</td>");
                    str.Append("<td width='30px'>");
                    str.Append("<input  type='text' flow='" + firstPlan.ProdLine + "'  item='" + firstPlan.Item + "'  name='UpQty' id='" + shiftPA.Id + "'value='" + shiftPA.Qty.ToString("0.##") + "' releaseNo='" + firstPlan.ReleaseNo + "'  dateFrom='" + planByDateIndex.Key + "'  style='width:70px' onblur='doFocusClick(this)' seq='" + seq + "' shift='" + shiftPA.Shift + "' />");
                    str.Append("</td>");

                    seq++;
                    str.Append("<td >");
                    str.Append(shiftPB.OrderQty.ToString("0.##"));
                    str.Append("</td>");
                    str.Append("<td width='30px'>");
                    str.Append("<input  type='text' flow='" + firstPlan.ProdLine + "'  item='" + firstPlan.Item + "'  name='UpQty' id='" + shiftPB.Id + "'value='" + shiftPB.Qty.ToString("0.##") + "' releaseNo='" + firstPlan.ReleaseNo + "'  dateFrom='" + planByDateIndex.Key + "'  style='width:70px' onblur='doFocusClick(this)' seq='" + seq + "'shift='" + shiftPB.Shift + "' />");
                    str.Append("</td>");

                    seq++;
                    str.Append("<td >");
                    str.Append(shiftPC.OrderQty.ToString("0.##"));
                    str.Append("</td>");
                    str.Append("<td width='30px'>");
                    str.Append("<input  type='text' flow='" + firstPlan.ProdLine + "'  item='" + firstPlan.Item + "'  name='UpQty' id='" + shiftPC.Id + "'value='" + shiftPC.Qty.ToString("0.##") + "' releaseNo='" + firstPlan.ReleaseNo + "'  dateFrom='" + planByDateIndex.Key + "'  style='width:70px' onblur='doFocusClick(this)' seq='" + seq + "' shift='" + shiftPC.Shift + "' />");
                    str.Append("</td>");
                }
                else
                {
                    str.Append("<td >");
                    str.Append(shiftPA.OrderQty.ToString("0.##"));
                    str.Append("</td>");
                    str.Append("<td>");
                    if (planByDateIndex.Key.Date == System.DateTime.Now.Date)
                    {
                        str.Append("<input type='checkbox' id='CheckBoxGroupA' name='CheckBoxGroupA' value='" + shiftPA.Id + "' runat='' onclick='doCheckClickA()' />");
                    }
                    str.Append(shiftPA.Qty.ToString("0.##"));
                    str.Append("</td>");

                    str.Append("<td >");
                    str.Append(shiftPB.OrderQty.ToString("0.##"));
                    str.Append("</td>");
                    str.Append("<td>");
                    if (planByDateIndex.Key.Date == System.DateTime.Now.Date)
                    {
                        str.Append("<input type='checkbox' id='CheckBoxGroupB' name='CheckBoxGroupB' value='" + shiftPB.Id + "' runat='' onclick='doCheckClickB()' />");
                    }
                    str.Append(shiftPB.Qty.ToString("0.##"));
                    str.Append("</td>");

                    str.Append("<td >");
                    str.Append(shiftPC.OrderQty.ToString("0.##"));
                    str.Append("</td>");
                    str.Append("<td>");
                    if (planByDateIndex.Key.Date == System.DateTime.Now.Date)
                    {
                        str.Append("<input type='checkbox' id='CheckBoxGroupC' name='CheckBoxGroupC' value='" + shiftPC.Id + "' runat='' onclick='doCheckClickC()' />");
                    }
                    str.Append(shiftPC.Qty.ToString("0.##"));
                    str.Append("</td>");
                }

                #endregion
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.list.InnerHtml = headStr + str.ToString();
    }

    #endregion

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(sender, e);
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.btQtyHidden.Value = string.Empty;
        this.btSeqHidden.Value = string.Empty;
        var searchSql = @"  select s.ProdLine,s.Id,s.Item,s.Itemdesc,s.Qty,s.Uom,s.UC,s.PlanDate,s.Shift,isnull(t1.Qty,0) OrderQty  from MRP_ShiftPlanDet s left join (select d.Item,sum((d.OrderQty-d.RecQty)) as Qty,m.StartTime,m.Flow,m.Shift from  OrderDet d inner join OrderMstr m on m.OrderNo=d.OrderNo where m.Type='Production' and m.Status='Submit' and d.OrderQty>d.RecQty group by d.Item,m.StartTime,m.Flow,m.Shift ) t1 on s.ProdLine=t1.Flow and s.Item=t1.Item and s.Shift=t1.Shift and CONVERT(varchar(10), s.PlanDate, 121)=CONVERT(varchar(10), t1.StartTime, 121) ";

        if (!string.IsNullOrEmpty(this.tbItemCode.Text.Trim()))
        {
            searchSql += string.Format(" and s.Item ='{0}' ", this.tbItemCode.Text.Trim());
        }

        if (!string.IsNullOrEmpty(this.tbFlow.Value.Trim()))
        {
            searchSql += string.Format(" and s.ProdLine in ('{0}') ", this.tbFlow.Value);
        }

        if (!string.IsNullOrEmpty(currentRelesNo))
        {
            searchSql += string.Format(" and m.ReleaseNo ='{0}' ", currentRelesNo);
        }

        searchSql += " order by s.Item asc ";

        var allResult = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
        var planDetList = new List<ShiftPlanDet>();
        foreach (System.Data.DataRow row in allResult.Rows)
        {
            //m.ReleaseNo,m.Status,s.ProdLine,s.Id,s.Item,s.Itemdesc,s.Qty,s.Uom,s.UC,s.PlanDate,s.Shift,isnull(t1.Qty,0) OrderQty
            planDetList.Add(new ShiftPlanDet
            {
                ReleaseNo = Int32.Parse(row[0].ToString()),
                Status = row[1].ToString(),
                ProdLine = row[2].ToString(),
                Id = Int32.Parse(row[3].ToString()),
                Item = row[4].ToString(),
                ItemDesc = row[5].ToString(),
                Qty = Convert.ToDecimal(row[6]),
                Uom = row[7].ToString(),
                UnitCount = Convert.ToDecimal(row[8]),
                PlanDate = Convert.ToDateTime(row[9]),
                Shift = row[10].ToString(),
                OrderQty = Convert.ToDecimal(row[11]),
            });
        }
        //if (this.rbType.SelectedValue == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY)
        //if(1==1)
        //{
        //    var minStartTime = productionPlanDetList.Min(s => s.StartTime).AddDays(13);
        //    productionPlanDetList = productionPlanDetList.Where(s => s.StartTime <= minStartTime).ToList();
        //    IList<object> data = new List<object>();
        //    data.Add(productionPlanDetList);
        //    TheReportMgr.WriteToClient("ProductionPlanDaily.xls", data, "ProductionPlanDaily.xls");
        //}
        //else
        //{
        //    ExportWeeklyExcel(productionPlanDetList);
        //}

    }

    private void ExportWeeklyExcel(IList<ProductionPlanDet> exportList)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        Sheet sheet1 = hssfworkbook.CreateSheet("sheet1");
        MemoryStream output = new MemoryStream();

        if (exportList != null && exportList.Count > 0)
        {
            var planByDateIndexs = exportList.GroupBy(p => p.StartTime).OrderBy(p => p.Key);
            var planByItems = exportList.GroupBy(p => p.Item);
            #region 写入字段
            Row rowHeader = sheet1.CreateRow(0);
            Row rowHeader2 = sheet1.CreateRow(1);
            //<th rowspan='2'>经济批量</th><th rowspan='2'>安全库存</th><th rowspan='2'>最大库存</th>");
            for (int i = 0; i < 8 + planByDateIndexs.Count() * 3; i++)
            {
                if (i == 0) //序号
                {
                    rowHeader.CreateCell(i).SetCellValue("序号");
                }
                else if (i == 1)    //物料号
                {
                    rowHeader.CreateCell(i).SetCellValue("物料号");
                }
                else if (i == 2)    //物料描述
                {
                    rowHeader.CreateCell(i).SetCellValue("物料描述");
                }
                else if (i == 3)      //客户零件号
                {
                    rowHeader.CreateCell(i).SetCellValue("客户零件号");
                }
                else if (i == 4)      //包装量
                {
                    rowHeader.CreateCell(i).SetCellValue("包装量");
                }
                else if (i == 5)      //经济批量
                {
                    rowHeader.CreateCell(i).SetCellValue("经济批量");
                }
                else if (i == 6)      //安全库存
                {
                    rowHeader.CreateCell(i).SetCellValue("安全库存");
                }
                else if (i == 7)      //最大库存
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
                        rowHeader2.CreateCell(i2++).SetCellValue("计划数");
                        i += 3;
                    }
                }
            }
            #endregion

            #region 写入数值
            int l = 0;
            int rowIndex = 2;
            foreach (var planByItem in planByItems)
            {
                var firstPlan = planByItem.First();
                var planDic = planByItem.GroupBy(d => d.StartTime).ToDictionary(d => d.Key, d => d.Sum(q => q.Qty));
                l++;
                Row rowDetail = sheet1.CreateRow(rowIndex);
                int cell = 0;
                rowDetail.CreateCell(cell++).SetCellValue(l);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.Item);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.ItemDesc);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.RefItemCode);
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.UnitCount.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.MinLotSize.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.SafeStock.ToString("0.##"));
                rowDetail.CreateCell(cell++).SetCellValue(firstPlan.MaxStock.ToString("0.##"));
                foreach (var planByDateIndex in planByDateIndexs)
                {
                    var curenPlan = planByItem.Where(p => p.StartTime == planByDateIndex.Key);
                    var pdPlan = curenPlan.Count() > 0 ? curenPlan.First() : new ProductionPlanDet();
                    var createCell = rowDetail.CreateCell(cell++);
                    createCell.SetCellType(CellType.NUMERIC);
                    createCell.SetCellValue(Convert.ToDouble(pdPlan.ReqQty));

                    var createCell2 = rowDetail.CreateCell(cell++);
                    createCell2.SetCellType(CellType.NUMERIC);
                    createCell2.SetCellValue(Convert.ToDouble(pdPlan.OrderQty));

                    var createShip = rowDetail.CreateCell(cell++);
                    createShip.SetCellType(CellType.NUMERIC);
                    createShip.SetCellValue(Convert.ToDouble(pdPlan.Qty));
                }

                rowIndex++;
            }
            #endregion

            hssfworkbook.Write(output);

            string filename = "ProductionPlanWeekly.xls";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();
            Response.BinaryWrite(output.GetBuffer());
            Response.End();
            //return File(output, contentType, exportName + "." + fileSuffiex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            var allSeqArr = string.IsNullOrEmpty(this.btSeqHidden.Value) ? new string[0] : this.btSeqHidden.Value.Split(',');
            var allShipQty = string.IsNullOrEmpty(this.btQtyHidden.Value) ? new string[0] : this.btQtyHidden.Value.Split(',');
            string allHtml = this.list.InnerHtml;
            IList<string> itemList = new List<string>();
            IList<string> qtyList = new List<string>();
            IList<string> idList = new List<string>();
            IList<string> releaseNoList = new List<string>();
            IList<string> dateFromList = new List<string>();
            IList<string> shiftCodeList = new List<string>();
            IList<string> flowList = new List<string>();

            string shiftCode = string.Empty;
            string flow = string.Empty;
            string item = string.Empty;
            string qty = string.Empty;
            string id = string.Empty;
            string releaseNo = string.Empty;
            string dateFrom = string.Empty;
            string seq = string.Empty;
            while (allHtml.Length > 0)
            {
                int startIndex = allHtml.IndexOf("flow='");
                if (startIndex == -1) { allHtml = string.Empty; break; }
                allHtml = allHtml.Substring(startIndex + 6);
                int endIndex = allHtml.IndexOf("'");
                flow = allHtml.Substring(0, endIndex);

                 startIndex = allHtml.IndexOf("item='");
                if (startIndex == -1) { allHtml = string.Empty; break; }
                allHtml = allHtml.Substring(startIndex + 6);
                 endIndex = allHtml.IndexOf("'");
                item = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("id='");
                allHtml = allHtml.Substring(startIndex + 4);
                endIndex = allHtml.IndexOf("'");
                id = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("value='");
                allHtml = allHtml.Substring(startIndex + 7);
                endIndex = allHtml.IndexOf("'");
                qty = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("releaseNo='");
                allHtml = allHtml.Substring(startIndex + 11);
                endIndex = allHtml.IndexOf("'");
                releaseNo = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("dateFrom='");
                allHtml = allHtml.Substring(startIndex + 10);
                endIndex = allHtml.IndexOf("'");
                dateFrom = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("seq='");
                allHtml = allHtml.Substring(startIndex + 5);
                endIndex = allHtml.IndexOf("'");
                seq = allHtml.Substring(0, endIndex);

                startIndex = allHtml.IndexOf("shift='");
                allHtml = allHtml.Substring(startIndex + 7);
                endIndex = allHtml.IndexOf("'");
                shiftCode = allHtml.Substring(0, endIndex);

                if (allSeqArr.Contains(seq))
                {
                    int i = 0;
                    foreach (var s in allSeqArr)
                    {
                        i++;
                        if (s == seq) break;
                    }
                    if (allShipQty[i - 1] == qty)
                    { }
                    else
                    {
                        flowList.Add(flow);
                        itemList.Add(item);
                        idList.Add(id);
                        qtyList.Add(allShipQty[i - 1]);
                        releaseNoList.Add(releaseNo);
                        dateFromList.Add(dateFrom);
                        shiftCodeList.Add(shiftCode);
                    }
                }


            }
            IList<decimal> shipQtyList = new List<decimal>();
            foreach (var q in qtyList)
            {
                try
                {
                    shipQtyList.Add(Convert.ToDecimal(q));
                }
                catch (Exception exc)
                {
                    ShowErrorMessage("数量" + q + "填写错误");
                }
            }
            if (itemList.Count == 0)
            {
                ShowErrorMessage("没有要修改的计划。");
            }
            TheMrpMgr.UpdateShiftPlanPlanQty( flowList,itemList, idList, shipQtyList, releaseNoList, dateFromList, this.CurrentUser, shiftCodeList);
            ShowSuccessMessage("修改成功。");
            this.btnSearch_Click(null, null);
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
        this.btQtyHidden.Value = string.Empty;
        this.btSeqHidden.Value = string.Empty;
    }
}