using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.Sconit.Web;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service.MasterData;
using NHibernate.Expression;
using com.Sconit.Entity;
using com.Sconit.Entity.View;
using com.Sconit.Utility;
using System.Collections.Generic;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;


public partial class MasterData_Reports_Inventory_List : ReportModuleBase
{

    private static string StaticSql { get; set; }
    private static int CurrentPageIndex {get;set;}
    private static string CurrentSortParam { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //UpdateView();
        }
    }

    public override void UpdateView()
    {
        //this.GV_List.Execute();
        com.Sconit.Control.GridPager pager = this.gp;
        pager.CurrentPageIndex = CurrentPageIndex;
        string resultSql = "select * from ( select *,RowId=row_number()over(order by "+CurrentSortParam+" asc) from (  " + StaticSql + ") as T1 ) as T2 where RowId between " + (pager.CurrentPageIndex - 1) * pager.PageSize + " and " + pager.CurrentPageIndex * pager.PageSize + "";
        var result = TheGenericMgr.GetDatasetBySql(resultSql).Tables[0];
        var counts = TheGenericMgr.GetDatasetBySql("select count(*) from ("+StaticSql+") as T1").Tables[0].Rows[0][0];
        var list = new List<LocationLotDetailView>();
        foreach (System.Data.DataRow row in result.Rows)
        {
            //i.Code,i.Desc1,i.Uom,loc.Code,loc.Name,l.Bin,l.LotNo,l.Qty
            list.Add(new LocationLotDetailView
            {
                Id = int.Parse(row[0].ToString()),
                ItemCode = row[1].ToString(),
                ItemDesc = row[2].ToString(),
                Uom = row[3].ToString(),
                LocCode = row[4].ToString(),
                LocName = row[5].ToString(),
                BinCode = row[6].ToString(),
                LotNo = row[7].ToString(),
                Qty = Convert.ToDecimal(row[8].ToString()),
            });
        }

        list = list == null || list.Count == 0 ? new List<LocationLotDetailView>() : list;
        pager.RecordCount = int.Parse(counts.ToString());
        this.GV_List.DataSource = list;
        this.GV_List.DataBind();

    }

    protected void GV_List_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            e.Row.Visible = false;
        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    CurrentFooterCount++;
        //    if (CurrentFooterCount > footerCount)
        //    {
        //        e.Row.Visible = false;
        //    }
        //}
    }

    public override void InitPageParameter(object sender)
    {
        this._criteriaParam = (CriteriaParam)sender;
        //this.SetCriteria();
        //this.UpdateView();
        string searchSql = @"select l.Id,i.Code as ItemCode,i.Desc1,i.Uom,loc.Code as LocCode,loc.Name as LocName,l.Bin,l.LotNo,Isnull(l.Qty,0) as Qty
from LocLotDetView	 as l with(nolock)	
inner join Item as i on i.Code=l.Item
inner join Location as loc on loc.Code=l.Location where 1=1 ";

        string locCodes = this._criteriaParam.LocCodes;
        string itemCodes = this._criteriaParam.ItemCodes;
        string itemCode = this._criteriaParam.Item;
        string lotNo = this._criteriaParam.LotNo;
        if (!string.IsNullOrEmpty(locCodes))
        {
            locCodes = locCodes.Replace("\r\n", ",");
            locCodes = locCodes.Replace("\n", ",");
            locCodes = locCodes.Replace(",", "','");
            searchSql += string.Format(" and l.Location in ('{0}') ", locCodes);
        }


        if (!string.IsNullOrEmpty(itemCodes))
        {
            itemCodes = itemCodes.Replace("\r\n", ",");
            itemCodes = itemCodes.Replace("\n", ",");
            itemCodes = itemCodes.Replace(",", "','");
            if (!string.IsNullOrEmpty(itemCode))
            {
                itemCodes += " ','" + itemCode;
            }
            searchSql += string.Format(" and l.Item in ('{0}') ", itemCodes);
        }
        else
        {
            if (!string.IsNullOrEmpty(itemCode))
            {
                searchSql += string.Format(" and l.Item ='{0}' ", itemCode);
            }
        }

        if (!string.IsNullOrEmpty(lotNo))
        {
            searchSql += string.Format(" and l.LotNo ='{0}' ", lotNo);
        }
        StaticSql = searchSql;
        CurrentPageIndex = this._criteriaParam.Page;
        CurrentSortParam = this._criteriaParam.SortParam;
        this.UpdateView();
    }

    public void Export()
    {
        //var result = TheGenericMgr.GetDatasetBySql(StaticSql).Tables[0];
        //var list = new List<LocationLotDetailView>();
        //foreach (System.Data.DataRow row in result.Rows)
        //{
        //    //i.Code,i.Desc1,i.Uom,loc.Code,loc.Name,l.Bin,l.LotNo,l.Qty
        //    list.Add(new LocationLotDetailView
        //    {
        //        Id = int.Parse(row[0].ToString()),
        //        ItemCode = row[1].ToString(),
        //        ItemDesc = row[2].ToString(),
        //        Uom = row[3].ToString(),
        //        LocCode = row[4].ToString(),
        //        LocName = row[5].ToString(),
        //        BinCode = row[6].ToString(),
        //        LotNo = row[7].ToString(),
        //        Qty = Convert.ToDecimal(row[8].ToString()),
        //    });
        //}

        //com.Sconit.Control.GridPager pager = this.gp;
        //list = list == null || list.Count == 0 ? new List<LocationLotDetailView>() : list;
        //pager.RecordCount = list.Count;
        //this.GV_List.DataSource = list;
        //this.GV_List.DataBind();
        //this.ExportXLS(GV_List);
        ExportExcel();
    }

    private void ExportExcel()
    {
        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        //Sheet sheet1 = hssfworkbook.CreateSheet("Sheet1");
        MemoryStream output = new MemoryStream();
        var result = TheGenericMgr.GetDatasetBySql(StaticSql).Tables[0];
        var exportList = new List<LocationLotDetailView>();
        foreach (System.Data.DataRow row in result.Rows)
        {
            exportList.Add(new LocationLotDetailView
            {
                Id = int.Parse(row[0].ToString()),
                ItemCode = row[1].ToString(),
                ItemDesc = row[2].ToString(),
                Uom = row[3].ToString(),
                LocCode = row[4].ToString(),
                LocName = row[5].ToString(),
                BinCode = row[6].ToString(),
                LotNo = row[7].ToString(),
                Qty = Convert.ToDecimal(row[8].ToString()),
            });
        }
        exportList = exportList == null || exportList.Count == 0 ? new List<LocationLotDetailView>() : exportList;
        if (exportList != null && exportList.Count > 0)
        {
            Sheet sheet1 = hssfworkbook.CreateSheet("sheet1");

            #region 写入字段
            int i = 0;
            Row rowHeader = sheet1.CreateRow(i++);
            //No.	物料代码	物料描述	单位	库位	库位名称	库格	批号	数量
            rowHeader.CreateCell(0).SetCellValue("No.");
            rowHeader.CreateCell(1).SetCellValue("物料代码");
            rowHeader.CreateCell(2).SetCellValue("物料描述");
            rowHeader.CreateCell(3).SetCellValue("单位");
            rowHeader.CreateCell(4).SetCellValue("库位");
            rowHeader.CreateCell(5).SetCellValue("库位名称");
            rowHeader.CreateCell(6).SetCellValue("库格");
            rowHeader.CreateCell(7).SetCellValue("批号");
            rowHeader.CreateCell(8).SetCellValue("数量");
            #endregion

            #region 写入数值
            foreach (var d in exportList)
            {
                Row rowDetail = sheet1.CreateRow(i++);
                rowDetail.CreateCell(0).SetCellValue(i - 1);
                rowDetail.CreateCell(1).SetCellValue(d.ItemCode);
                rowDetail.CreateCell(2).SetCellValue(d.ItemDesc);
                rowDetail.CreateCell(3).SetCellValue(d.Uom);
                rowDetail.CreateCell(4).SetCellValue(d.LocCode);
                rowDetail.CreateCell(5).SetCellValue(d.LocName);
                rowDetail.CreateCell(6).SetCellValue(d.BinCode);
                rowDetail.CreateCell(7).SetCellValue(d.LotNo);
                rowDetail.CreateCell(8).SetCellValue(d.Qty.Value.ToString("0.##"));
            }
            #endregion
        }
        hssfworkbook.Write(output);

        string filename = "export.xls";
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
        Response.Clear();

        Response.BinaryWrite(output.GetBuffer());
        Response.End();
        //return File(output, contentType, exportName + "." + fileSuffiex);

    }

    protected override void SetCriteria()
    {
        DetachedCriteria criteria = DetachedCriteria.For(typeof(LocationLotDetailView));
        criteria.CreateAlias("Location", "l");

        #region Customize
        SecurityHelper.SetRegionSearchCriteria(criteria, "l.Region.Code", this.CurrentUser.Code); //区域权限
        #endregion

        #region Select Parameters
        CriteriaHelper.SetLocationCriteria(criteria, "l.Code", this._criteriaParam);
        CriteriaHelper.SetItemCriteria(criteria, "Item.Code", this._criteriaParam);
        CriteriaHelper.SetLotNoCriteria(criteria, "LotNo", this._criteriaParam);

        #endregion

        DetachedCriteria selectCountCriteria = CloneHelper.DeepClone<DetachedCriteria>(criteria);
        selectCountCriteria.SetProjection(Projections.Count("Id"));
        SetSearchCriteria(criteria, selectCountCriteria);
    }
}
