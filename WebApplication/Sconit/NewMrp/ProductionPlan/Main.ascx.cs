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

public partial class NewMrp_ProductionPlan_Main : MainModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.tbStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.tbEndDate.Text = DateTime.Today.AddDays(14).ToString("yyyy-MM-dd");
        }
    }

    #region   明细查询

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        var searchSql = @" select m.Id,m.ReleaseNo,det.Id,det.Item,i.Desc1,det.RefItemCode,det.OrgQty,det.Qty,det.Uom,det.StartTime,det.WindowTime from  dbo.MRP_ProductionPlanDet as det inner join MRP_ProductionPlanMstr as m on det.ProductionPlanId=m.Id
inner join Item as i on i.Code=det.Item  where 1=1 ";

        DateTime startTime = DateTime.Today;
        if (!string.IsNullOrEmpty(this.tbStartDate.Text.Trim()))
        {
            DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
            searchSql += string.Format(" and det.WindowTime>='{0}' ", startTime.Date);
        }
        else
        {
            this.list.InnerHtml = "开始日期不能为空。";
            this.ltlPlanVersion.Text = string.Empty;
            return;
        }
        if (!string.IsNullOrEmpty(this.tbEndDate.Text.Trim()))
        {
            DateTime endTime = DateTime.Today;
            DateTime.TryParse(this.tbEndDate.Text.Trim(), out endTime);
            searchSql += string.Format(" and det.WindowTime<='{0}' ", endTime.Date);
        }
        else
        {
            DateTime endTime = startTime.AddDays(14);
            searchSql += string.Format(" and det.WindowTime<='{0}' ", endTime.Date);
        }

        if (!string.IsNullOrEmpty(this.tbItemCode.Text.Trim()))
        {
            searchSql += string.Format(" and det.Item ='{0}' ", this.tbItemCode.Text.Trim());
        }

        if (!string.IsNullOrEmpty(this.tbReleaseNo.Text.Trim()))
        {
            searchSql += string.Format(" and m.ReleaseNo ='{0}' ", this.tbReleaseNo.Text.Trim());
        }
        else
        {
            searchSql += "and m.ReleaseNo = (select max(ReleaseNo) from MRP_ProductionPlanMstr)";

        }

        var allResult = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
        var prodPlanDetList = new List<ProductionPlanDet>();
        foreach (System.Data.DataRow row in allResult.Rows)
        {
            //m.Id,m.ReleaseNo,det.Id,det.Item,det.ItemDesc,det.RefItemCode,det.OrgQty,det.Qty,
            //det.Uom,det.StartTime,det.WindowTime
            prodPlanDetList.Add(new ProductionPlanDet
            {
                ProductionPlanId = Int32.Parse(row[0].ToString()),
                ReleaseNo = Int32.Parse(row[1].ToString()),
                Id = Int32.Parse(row[2].ToString()),
                Item = row[3].ToString(),
                ItemDesc = row[4].ToString(),
                RefItemCode = row[5].ToString(),
                OrgQty = Convert.ToDecimal(row[6]),
                Qty = Convert.ToDecimal(row[7]),
                Uom = row[8].ToString(),
                StartTime = Convert.ToDateTime(row[9]),
                WindowTime = Convert.ToDateTime(row[10]),
            });
        }

        ListTable(prodPlanDetList);
    }

    private void ListTable(IList<ProductionPlanDet> prodPlanDetList)
    {
        if (prodPlanDetList == null || prodPlanDetList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            this.ltlPlanVersion.Text = string.Empty;
            return;
        }


        var planByDateIndexs = prodPlanDetList.GroupBy(p => p.WindowTime).OrderBy(p => p.Key);
        var planByFlowItems = prodPlanDetList.OrderBy(p => p.Item).GroupBy(p => p.Item);

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
        string headStr = CopyString();
        str.Append("<thead><tr class='GVHeader'><th >行数</th><th >生产计划版本</th><th>物料号</th><th>物料描述</th><th >客户零件号</th><th >单位</th>");
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
         if (ii > 14)
        {
            widths = "240%";
        }
        else if (ii > 10)
        {
            widths = "200%";
        }
        else if (ii > 6)
        {
            widths = "150%";
        }
      
        headStr += string.Format("<table id='tt' runat='server' border='1' class='GV' style='width:{0};border-collapse:collapse;'>", widths);
        #endregion
        int l = 0;
        foreach (var planByFlowItem in planByFlowItems)
        {
            var firstPlan = planByFlowItem.First();
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
            str.Append(firstPlan.ReleaseNo);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.Item);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.ItemDesc);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.RefItemCode);
            str.Append("</td>");
            str.Append("<td>");
            str.Append(firstPlan.Uom);
            str.Append("</td>");
           
            foreach (var planByDateIndex in planByDateIndexs)
            {
                var curenPlan = planByFlowItem.Where(p => p.WindowTime == planByDateIndex.Key);
                str.Append("<td>");
                str.Append(curenPlan.Sum(s=>s.Qty).ToString("0.##"));
                str.Append("</td>");
            }
            str.Append("</tr>");
        }
        str.Append("</tbody></table>");
        this.list.InnerHtml = headStr+str.ToString();
    }

    #endregion

    #region    头查询
    protected void btnMstrSearch_Click(object sender, EventArgs e)
    {
        var searchSql = @"select m.Releaseno,m.BatchNo,m.CreateDate,m.CreateUser,l.Item,l.Bom,l.EffDate,l.Msg from MRP_ProductionPlanMstr as m left join MRP_RunProductionPlanLog as l on m.BatchNo=l.BatchNo where 1=1  ";
       

        if (!string.IsNullOrEmpty(this.tbMstrReleaseNo.Text.Trim()))
        {
            searchSql += string.Format(" and m.ReleaseNo ='{0}' ", this.tbMstrReleaseNo.Text.Trim());
        }

        var allResult = TheGenericMgr.GetDatasetBySql(searchSql+" order by m.ReleaseNo desc ").Tables[0];
        var prodPlanMstrList = new List<ProductionPlanMstr>();
        foreach (System.Data.DataRow row in allResult.Rows)
        {
            //m.Releaseno,m.BatchNo,m.CreateDate,m.CreateUser,l.Item,l.Bom,l.EffDate,l.Msg
            prodPlanMstrList.Add(new ProductionPlanMstr
            {
                ReleaseNo =int.Parse( row[0].ToString()),
                BatchNo = int.Parse(row[1].ToString()),
                CreateDate = Convert.ToDateTime(row[2].ToString()),
                CreateUser =row[3].ToString(),
                Item = row[4].ToString(),
                Bom = row[5].ToString(),
                EffDate =!string.IsNullOrEmpty(row[6].ToString())? (DateTime?)Convert.ToDateTime(row[6]):null,
                Msg =row[7].ToString(),
            });
        }
        ListTable(prodPlanMstrList);
    }

    private void ListTable(IList<ProductionPlanMstr> prodPlanMstrList)
    {
        if (prodPlanMstrList == null || prodPlanMstrList.Count == 0)
        {
            this.list.InnerHtml = "没有查到符合条件的记录";
            this.ltlPlanVersion.Text = string.Empty;
            return;
        }

        var planByReleaseNo = prodPlanMstrList.GroupBy(p => p.ReleaseNo);

        StringBuilder str = new StringBuilder();
        //str.Append(CopyString());
        //head
        string headStr = CopyString();
        str.Append("<thead><tr class='GVHeader'><th>行数</th><th>生产计划版本</th><th>创建用户</th><th>创建时间</th><th>物料号</th><th>Bom</th><th>错误消息</th>");
        str.Append("</tr></thead>");
        str.Append("<tbody>");

        #region  通过长度控制table的宽度
        string widths = "100%";
        headStr += string.Format("<table border='1' class='GV' style='width:{0};border-collapse:collapse;'>", widths);
        #endregion
        int l = 0;
        foreach (var g in planByReleaseNo)
        {
            var firstPlan = g.First();
            int s = 0;
            foreach (var r in g)
            {
                l++;
                if (l % 2 == 0)
                {
                    str.Append("<tr class='GVAlternatingRow'>");
                }
                else
                {
                    str.Append("<tr class='GVRow'>");
                }
                if (s == 0)
                {
                    str.Append("<td>");
                    str.Append(l);
                    str.Append("</td>");
                    str.Append("<td>");
                    str.Append(firstPlan.ReleaseNo);
                    str.Append("</td>");
                    str.Append("<td>");
                    str.Append(firstPlan.CreateUser);
                    str.Append("</td>");
                    str.Append("<td>");
                    str.Append(firstPlan.CreateDate);
                    str.Append("</td>");
                }
                else
                {
                    str.Append("<td></td><td></td><td></td><td></td>");
                }
              
                str.Append("<td>");
                str.Append(r.Item);
                str.Append("</td>");
                str.Append("<td>");
                str.Append(r.Bom);
                str.Append("</td>");
                str.Append("<td>");
                str.Append(r.Msg);
                str.Append("</td>");
                s++;
            }
        }
        str.Append("</tbody></table>");
        this.mstrList.InnerHtml = headStr + str.ToString();
    }
    #endregion

    #region   导出
    protected void btnExport_Click(object sender, EventArgs e)
    {
        var searchSql = @" select m.Id,m.ReleaseNo,det.Id,det.Item,i.Desc1,det.RefItemCode,det.OrgQty,det.Qty,det.Uom,det.StartTime,det.WindowTime from  dbo.MRP_ProductionPlanDet as det inner join MRP_ProductionPlanMstr as m on det.ProductionPlanId=m.Id
inner join Item as i on i.Code=det.Item  where 1=1 ";

        DateTime startTime = DateTime.Today;
        if (!string.IsNullOrEmpty(this.tbStartDate.Text.Trim()))
        {
            DateTime.TryParse(this.tbStartDate.Text.Trim(), out startTime);
            searchSql += string.Format(" and det.WindowTime>='{0}' ", startTime.Date);
        }
        else
        {
            this.list.InnerHtml = "开始日期不能为空。";
            this.ltlPlanVersion.Text = string.Empty;
            return;
        }
        if (!string.IsNullOrEmpty(this.tbEndDate.Text.Trim()))
        {
            DateTime endTime = DateTime.Today;
            DateTime.TryParse(this.tbEndDate.Text.Trim(), out endTime);
            searchSql += string.Format(" and det.WindowTime<='{0}' ", endTime.Date);
        }
        else
        {
            DateTime endTime = startTime.AddDays(14);
            searchSql += string.Format(" and det.WindowTime<='{0}' ", endTime.Date);
        }

        if (!string.IsNullOrEmpty(this.tbItemCode.Text.Trim()))
        {
            searchSql += string.Format(" and det.Item ='{0}' ", this.tbItemCode.Text.Trim());
        }

        if (!string.IsNullOrEmpty(this.tbReleaseNo.Text.Trim()))
        {
            searchSql += string.Format(" and m.ReleaseNo ='{0}' ", this.tbReleaseNo.Text.Trim());
        }
        else
        {
            searchSql += "and m.ReleaseNo = (select max(ReleaseNo) from MRP_ProductionPlanMstr)";

        }

        var allResult = TheGenericMgr.GetDatasetBySql(searchSql).Tables[0];
        var prodPlanDetList = new List<ProductionPlanDet>();
        foreach (System.Data.DataRow row in allResult.Rows)
        {
            //m.Id,m.ReleaseNo,det.Id,det.Item,det.ItemDesc,det.RefItemCode,det.OrgQty,det.Qty,
            //det.Uom,det.StartTime,det.WindowTime
            prodPlanDetList.Add(new ProductionPlanDet
            {
                ProductionPlanId = Int32.Parse(row[0].ToString()),
                ReleaseNo = Int32.Parse(row[1].ToString()),
                Id = Int32.Parse(row[2].ToString()),
                Item = row[3].ToString(),
                ItemDesc = row[4].ToString(),
                RefItemCode = row[5].ToString(),
                OrgQty = Convert.ToDecimal(row[6]),
                Qty = Convert.ToDecimal(row[7]),
                Uom = row[8].ToString(),
                StartTime = Convert.ToDateTime(row[9]),
                WindowTime = Convert.ToDateTime(row[10]),
            });
        }
        ExportExcel(prodPlanDetList);

    }

    private void ExportExcel(IList<ProductionPlanDet> exportList)
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        //Sheet sheet1 = hssfworkbook.CreateSheet("Sheet1");
        MemoryStream output = new MemoryStream();

        if (exportList != null && exportList.Count > 0)
        {
            var groups = (from tak in exportList
                          group tak by tak.ReleaseNo into result
                          select new
                          {
                              ReleaseNo = result.Key,
                              List = result.ToList()
                          }).ToList();
            for (int ti = 0; ti < groups.Count(); ti++)
            {
                var g = groups[ti];
                Sheet sheet1 = hssfworkbook.CreateSheet(g.ReleaseNo.ToString());
                IList<DateTime> dateListDic = g.List.OrderBy(l => l.WindowTime).Select(l => l.WindowTime).Distinct().ToList();
                #region 写入字段
                Row rowHeader = sheet1.CreateRow(0);
                //生产计划版本</th>物料号</th><th>物料描述</th><th >客户零件号</th><th >单位</th>
                for (int i = 0; i < 3 + dateListDic.Count; i++)
                {
                    if (i == 0) //物料号
                    {
                        rowHeader.CreateCell(i).SetCellValue("物料号");
                    }
                    else if (i == 1)  //物料描述
                    {
                        rowHeader.CreateCell(i).SetCellValue("物料描述");
                    }
                    else if (i == 2) //客户零件号
                    {
                        rowHeader.CreateCell(i).SetCellValue("客户零件号");
                    }
                    else if (i == 3)    //单位
                    {
                        rowHeader.CreateCell(i).SetCellValue("单位");
                    }
                    else
                    {
                        foreach (var date in dateListDic)
                        {
                            rowHeader.CreateCell(i++).SetCellValue(date.ToShortDateString());
                        }
                    }
                }
                #endregion

                #region 写入数值
                //Caption.Visible = true;
                var groupByItem = (from tak in g.List
                                      group tak by tak.Item into result
                                      select new
                                      {
                                          Item = result.Key,
                                          List = result.ToList()
                                      }).ToList();

                int j = 1;
                foreach (var d in groupByItem)
                {
                    var first=d.List.First();
                    Row rowDetail = sheet1.CreateRow(j);
                    rowDetail.CreateCell(0).SetCellValue(first.Item);
                    rowDetail.CreateCell(1).SetCellValue(first.ItemDesc);
                    rowDetail.CreateCell(2).SetCellValue(first.RefItemCode);
                    rowDetail.CreateCell(3).SetCellValue(first.Uom);
                    int cell = 0;
                    foreach (var date in dateListDic)
                    {

                        cell++;
                        //rowDetail.CreateCell(5 + cell).
                        var createCell = rowDetail.CreateCell(3 + cell);
                        createCell.SetCellType(CellType.NUMERIC);
                        createCell.SetCellValue(Convert.ToDouble(d.List.Where(dd=>dd.WindowTime==date).Sum(q=>q.Qty)));
                    }
                    j++;
                }
                #endregion
            }
            hssfworkbook.Write(output);

            string filename = "ProductionPlan.xls";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();

            Response.BinaryWrite(output.GetBuffer());
            Response.End();
            //return File(output, contentType, exportName + "." + fileSuffiex);
        }
    }
    #endregion

    protected void rblAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.rblAction.SelectedIndex == 0)
        {
            this.tblImport.Visible = false;
            this.mstrList.Visible = false;
            this.tblSearch.Visible = true;
            this.list.Visible = true;
        }
        else
        {
            this.tblImport.Visible = true;
            this.mstrList.Visible = true;
            this.tblSearch.Visible = false;
            this.list.Visible = false;
        }
    }

    private string CopyString()
    {
                 //底色黄色重新导入无改动,橙色重新导入并有改动

        return @"<a type='text/html' onclick='copyHtml()' href='#' id='copy'>复制</a>
                <script type='text/javascript'>
                    function copyHtml()
                    {
                        window.clipboardData.setData('Text', $('#ctl01_list')[0].innerHTML);
                    }
                </script>";
    }

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

}