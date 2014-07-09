using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.Sconit.Web;
using com.Sconit.Entity;
using Geekees.Common.Controls;
using com.Sconit.Entity.MRP;
using NHibernate.Expression;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public partial class NewMrp_ShipPlan_Search : SearchModuleBase
{



    public event EventHandler SearchEvent;
    //private List<string> StatusList
    //{
    //    get { return this.astvMyTree.GetCheckedNodes().Select(a => a.NodeValue).ToList(); }
    //}


    protected void Page_Load(object sender, EventArgs e)
    {
        //this.tbFlow.ServiceParameter = "string:" + this.CurrentUser.Code;

        if (!IsPostBack)
        {
            this.tbCreateStartDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            this.tbCreateEndDate.Text = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd");
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.DoSearch();
    }

    protected override void DoSearch()
    {

        DateTime? startDate = System.DateTime.Now;
        DateTime? endDate = null;
        if (!string.IsNullOrEmpty(this.tbCreateStartDate.Text))
        {
            startDate = DateTime.Parse(this.tbCreateStartDate.Text);
        }
        if (!string.IsNullOrEmpty(this.tbCreateEndDate.Text))
        {
            endDate = DateTime.Parse(this.tbCreateEndDate.Text);
        }
        string releaseNo = this.tbReleaseNo.Text.Trim();

        if (SearchEvent != null)
        {
            #region DetachedCriteria

                DetachedCriteria selectCriteria = DetachedCriteria.For(typeof(ShipPlanMstr));
                DetachedCriteria selectCountCriteria = DetachedCriteria.For(typeof(ShipPlanMstr))
                     .SetProjection(Projections.Count("Id"));
                if (releaseNo != string.Empty)
                {
                    selectCriteria.Add(Expression.Eq("RelaseNo", releaseNo));
                    selectCountCriteria.Add(Expression.Eq("RelaseNo", releaseNo));
                }
                if (startDate != null)
                {
                    selectCriteria.Add(Expression.Ge("CreateDate", startDate));
                    selectCountCriteria.Add(Expression.Ge("CreateDate", startDate));
                }
                if (endDate != null)
                {
                    selectCriteria.Add(Expression.Le("CreateDate", endDate));
                    selectCountCriteria.Add(Expression.Le("CreateDate", endDate));
                }
                SearchEvent((new object[] { selectCriteria, selectCountCriteria }), null);
            #endregion
        }
    }

    protected override void InitPageParameter(IDictionary<string, string> actionParameter)
    {
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        //flowDetail.PackagingCode = ((System.Web.UI.HtmlControls.HtmlSelect)this.FV_FlowDetail.FindControl("tbPackagingCode")).Value;
        //string controlNums = this.btControl_Num.Value;
        //if (!string.IsNullOrEmpty(controlNums))
        //{
        //    string sql = string.Format(" select e from EDIFordPlan as e  where Control_Num in ('{0}') ", string.Join("','", controlNums.Split(',')));
        //    IList<EDIFordPlan> exportList = TheGenericMgr.FindAllWithCustomQuery<EDIFordPlan>(sql);
        //    if (exportList != null && exportList.Count > 0)
        //    {
        //        ExportExcel(exportList);
        //    }
        //}

    }

    //protected void ExportExcel(IList<EDIFordPlan> exportList)
    //{
    //    HSSFWorkbook hssfworkbook = new HSSFWorkbook();
    //    //Sheet sheet1 = hssfworkbook.CreateSheet("Sheet1");
    //    MemoryStream output = new MemoryStream();

    //    if (exportList != null && exportList.Count > 0)
    //    {
    //        var groups = (from tak in exportList
    //                      group tak by tak.Control_Num into result
    //                      select new
    //                      {
    //                          Control_Num = result.Key,
    //                          List = result.ToList()
    //                      }).ToList();
    //        for (int ti = 0; ti < groups.Count(); ti++)
    //        {
    //            var g = groups[ti];
    //            Sheet sheet1 = hssfworkbook.CreateSheet(g.Control_Num);
    //            IList<DateTime> dateListDic = g.List.OrderBy(l => l.ForecastDate).Select(l => l.ForecastDate).Distinct().ToList();
    //            #region 写入字段
    //            Row rowHeader = sheet1.CreateRow(0);
    //            for (int i = 0; i < 6 + dateListDic.Count; i++)
    //            {
    //                if (i == 0) //版本号
    //                {
    //                    rowHeader.CreateCell(i).SetCellValue("版本号");
    //                }
    //                else if (i == 1)  //文件发布日期
    //                {
    //                    rowHeader.CreateCell(i).SetCellValue("文件发布日期");
    //                }
    //                else if (i == 2) //物料号
    //                {
    //                    rowHeader.CreateCell(i).SetCellValue("物料号");
    //                }
    //                else if (i == 3)    //物料描述
    //                {
    //                    rowHeader.CreateCell(i).SetCellValue("物料描述");
    //                }
    //                else if (i == 4)    //福特物料号
    //                {
    //                    rowHeader.CreateCell(i).SetCellValue("福特物料号");
    //                }
    //                else if (i == 5)      //单位
    //                {
    //                    rowHeader.CreateCell(i).SetCellValue("单位");
    //                }
    //                else
    //                {
    //                    foreach (var date in dateListDic)
    //                    {
    //                        rowHeader.CreateCell(i++).SetCellValue(date.ToShortDateString());
    //                    }
    //                }
    //            }
    //            #endregion

    //            #region 写入数值
    //            //Caption.Visible = true;
    //            var groupByRefItem = (from tak in g.List
    //                                  group tak by tak.RefItem into result
    //                                  select new
    //                                  {
    //                                      RefItem = result.Key,
    //                                      List = result.ToList()
    //                                  }).ToList();
    //            IList<FlowDetail> flowdets = TheGenericMgr.FindAllWithCustomQuery<FlowDetail>(string.Format(" select d from FlowDetail as d where  d.ReferenceItemCode in('{0}') ", string.Join("','", g.List.Select(w => w.RefItem).Distinct().ToArray())));
    //            int j = 1;
    //            foreach (var d in groupByRefItem)
    //            {
    //                Row rowDetail = sheet1.CreateRow(j);
    //                Row rowDetail2 = sheet1.CreateRow(j + 1);
    //                EDIFordPlan newPlan = d.List.First();
    //                if (flowdets != null && flowdets.Count > 0)
    //                {
    //                    var flowDet = flowdets.Where(f => f.ReferenceItemCode == newPlan.RefItem);
    //                    if (flowDet != null && flowDet.Count() > 0)
    //                    {
    //                        newPlan.Item = flowDet.First().Item.Code;
    //                        newPlan.ItemDesc = flowDet.First().Item.Description;
    //                    }
    //                }
    //                rowDetail.CreateCell(0).SetCellValue(newPlan.Control_Num);
    //                rowDetail.CreateCell(1).SetCellValue(newPlan.ReleaseIssueDate.ToShortDateString());
    //                rowDetail.CreateCell(2).SetCellValue(newPlan.Item);
    //                rowDetail.CreateCell(3).SetCellValue(newPlan.ItemDesc);
    //                rowDetail.CreateCell(4).SetCellValue(newPlan.RefItem);
    //                rowDetail.CreateCell(5).SetCellValue(newPlan.Uom);
    //                int cell = 0;
    //                foreach (var f in d.List.OrderBy(o => o.ForecastDate))
    //                {
    //                    cell++;
    //                    //rowDetail.CreateCell(5 + cell).
    //                    var createCell = rowDetail.CreateCell(5 + cell);
    //                    createCell.SetCellType(CellType.NUMERIC);
    //                    createCell.SetCellValue(Convert.ToDouble(f.ForecastQty > 0 ? f.ForecastQty : 0));

    //                    var createCell2 = rowDetail2.CreateCell(5 + cell);
    //                    createCell2.SetCellType(CellType.NUMERIC);
    //                    createCell2.SetCellValue(Convert.ToDouble(f.ForecastCumQty > 0 ? f.ForecastCumQty : 0));
    //                }
    //                j += 2;
    //            }
    //            #endregion
    //        }
    //        hssfworkbook.Write(output);

    //        string filename = "FordEdiPlan.xls";
    //        Response.ContentType = "application/vnd.ms-excel";
    //        Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
    //        Response.Clear();

    //        Response.BinaryWrite(output.GetBuffer());
    //        Response.End();
    //        //return File(output, contentType, exportName + "." + fileSuffiex);
    //    }
    //}

    //public void ExportExcel(IList<EDIFordPlan> exportList)
    //{
    //    if (exportList == null || exportList.Count == 0)
    //    {
    //        return;
    //    }
    //    KillProcess("Excel");
    //    string saveFileName = @"C:\Users\Administrator\Desktop\" + System.DateTime.Now.ToString("yyMMddHHmmss") + ".xlsx";
    //    Application xlApp = new Application();
    //    object missing = System.Reflection.Missing.Value;

    //    if (xlApp == null)
    //    {
    //        //MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
    //        return;
    //    }

    //    Workbooks workbooks = xlApp.Workbooks;
    //    Workbook workbook = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
    //    Sheets sheets = workbook.Worksheets;

    //    Worksheet worksheet = null;
    //    var groups = (from tak in exportList
    //                  group tak by tak.Control_Num into result
    //                  select new
    //                  {
    //                      Control_Num = result.Key,
    //                      List = result.ToList()
    //                  }).ToList();
    //    for (int ti = 0; ti < groups.Count(); ti++)
    //    {
    //        var g = groups[ti];
    //        if (worksheet == null)
    //        {
    //            worksheet = (Worksheet)workbook.Worksheets[1];
    //        }
    //        else
    //        {
    //            worksheet = (Worksheet)workbook.Worksheets.Add(Type.Missing, worksheet, 1, Type.Missing);
    //        }
    //        worksheet.Name = g.Control_Num;

    //        Range range;

    //        IList<DateTime> dateListDic = g.List.OrderBy(l => l.ForecastDate).Select(l => l.ForecastDate).Distinct().ToList();
    //        #region 写入字段
    //        for (int i = 0; i < 6 + dateListDic.Count; i++)
    //        {
    //            if (i == 0) //版本号
    //            {
    //                worksheet.Cells[1, i + 1] = "版本号";
    //            }
    //            else if (i == 1)  //文件发布日期
    //            {
    //                worksheet.Cells[1, i + 1] = "文件发布日期";
    //            }
    //            else if (i == 2) //物料号
    //            {
    //                worksheet.Cells[1, i + 1] = "物料号";
    //            }
    //            else if (i == 3)    //物料描述
    //            {
    //                worksheet.Cells[1, i + 1] = "物料描述";
    //            }
    //            else if (i == 4)    //福特物料号
    //            {
    //                worksheet.Cells[1, i + 1] = "福特物料号";
    //            }
    //            else if (i == 5)      //单位
    //            {
    //                worksheet.Cells[1, i + 1] = "单位";
    //            }
    //            else
    //            {
    //                foreach (var date in dateListDic)
    //                {
    //                    worksheet.Cells[1, i + 1] = date.ToShortDateString();
    //                }
    //            }
    //            //worksheet.Cells[1, i + 1] = ds.Tables[ti].Columns[i].ColumnName;
    //            range = (Range)worksheet.Cells[1, i + 1];
    //            range.Interior.ColorIndex = 15;
    //            range.Font.Bold = true;
    //        }
    //        #endregion

    //        #region 写入数值
    //        //Caption.Visible = true;
    //        var groupByRefItem = (from tak in g.List
    //                              group tak by tak.RefItem into result
    //                              select new
    //                              {
    //                                  RefItem = result.Key,
    //                                  List = result.ToList()
    //                              }).ToList();
    //        IList<FlowDetail> flowdets = TheGenericMgr.FindAllWithCustomQuery<FlowDetail>(string.Format(" select d from FlowDetail as d where  d.ReferenceItemCode in('{0}') ", string.Join("','", g.List.Select(w => w.RefItem).Distinct().ToArray())));
    //        int ii = 0;
    //        foreach (var d in groupByRefItem)
    //        {
    //            EDIFordPlan newPlan = g.List.First();
    //            var flowDet = flowdets.Where(f => f.ReferenceItemCode == newPlan.RefItem);
    //            if (flowDet != null || flowDet.Count() > 0)
    //            {
    //                newPlan.Item = flowDet.First().Item.Code;
    //                newPlan.ItemDesc = flowDet.First().Item.Description;
    //            }
    //            worksheet.Cells[ii + 2, 1] = newPlan.Control_Num;
    //            worksheet.Cells[ii + 2, 2] = newPlan.ReleaseIssueDate;
    //            worksheet.Cells[ii + 2, 3] = newPlan.Item;
    //            worksheet.Cells[ii + 2, 4] = newPlan.ItemDesc;
    //            worksheet.Cells[ii + 2, 5] = newPlan.RefItem;
    //            worksheet.Cells[ii + 2, 6] = newPlan.Uom;

    //            int cell = 0;
    //            foreach (var f in d.List)
    //            {
    //                cell++;
    //                worksheet.Cells[ii + 2, 6 + cell] = newPlan.ForecastQty;
    //                worksheet.Cells[ii + 3, 6 + cell] = newPlan.ForecastCumQty;
    //            }
    //            ii += 2;
    //        }
    //        #endregion
    //    }
    //    worksheet.SaveAs(saveFileName, missing, missing, missing, missing, missing, missing, missing, missing, missing);
    //    workbook.Close(missing, missing, missing);

    //    xlApp.Quit();
    //}

    //private void KillProcess(string processName)
    //{
    //    System.Diagnostics.Process myproc = new System.Diagnostics.Process();
    //    //得到所有打开的进程
    //    try
    //    {
    //        foreach (System.Diagnostics.Process thisproc in System.Diagnostics.Process.GetProcessesByName(processName))
    //        {
    //            if (!thisproc.CloseMainWindow())
    //            {
    //                thisproc.Kill();
    //            }
    //        }
    //    }
    //    catch (Exception Exc)
    //    {
    //        throw new Exception("", Exc);
    //    }
    //}

    //#endregion


}
