using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Web;
using com.Sconit.Entity;
using NPOI.HSSF.Util;
using System.Collections;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;
using com.Sconit.Service;
using com.Sconit.Service.MasterData;
using NPOI.HPSF;
using NPOI.SS.UserModel;


namespace com.Sconit.Service.Report
{
    public abstract class ReportBaseMgr : SessionBase, IReportBaseMgr
    {
        protected string reportTemplateFolder;
        protected string barCodeFontName;
        protected short barCodeFontSize;

        protected HSSFWorkbook workbook;

        protected Sheet sheet;


        /*
        protected Row row;
        protected CellStyle style;

        protected Font font;
        */
        protected int rowCount = 0;

        // 设置cell编码解决中文高位字节截断
        //private static short XLS_ENCODING = HSSFWorkbook.ENCODING_UTF_16;

        //private static readonly String BARCODEFONT_CODE39 = "C39HrP24DhTt";
        //private static readonly String BARCODEFONT_3OF9 = "3 of 9 Barcode";

        public ReportBaseMgr()
        {
            this.workbook = new HSSFWorkbook();
            this.sheet = workbook.CreateSheet(Guid.NewGuid().ToString("N"));

            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "YFGM Company";

            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "Report";
            si.Author = "tiansu";

            workbook.DocumentSummaryInformation = dsi;
            workbook.SummaryInformation = si;

            this.sheet.ForceFormulaRecalculation = true;

        }


        /**
        * 
        * Param templateFileName 模版文件名
        * 
        */
        protected void init(String templateFileName)
        {
            this.reportTemplateFolder = this.reportTemplateFolder.Replace("\r\n\t\t", "");
            string templateFile = reportTemplateFolder + templateFileName;
            FileStream file = new FileStream(templateFile, FileMode.Open, FileAccess.Read);
            this.workbook = new HSSFWorkbook(file);
            this.sheet = workbook.GetSheetAt(0);

            this.sheet.ForceFormulaRecalculation = true;
        }

        /**
        * 
        * Param templateFileName 模版文件名
        * Param rowCount 行数
        */
        protected void init(String templateFileName, int rowCount)
        {
            this.rowCount = rowCount;
            this.reportTemplateFolder = this.reportTemplateFolder.Replace("\r\n\t\t", "");
            string templateFile = reportTemplateFolder + templateFileName;
            FileStream file = new FileStream(templateFile, FileMode.Open, FileAccess.Read);
            this.workbook = new HSSFWorkbook(file);
            this.sheet = workbook.GetSheetAt(0);

            this.sheet.ForceFormulaRecalculation = true;
        }




        protected Sheet GetSheet()
        {
            return workbook.GetSheetAt(0);
        }

        protected Sheet GetSheet(int index)
        {
            return workbook.GetSheetAt(index);
        }

        /**
         * 页处理
         *      pageCount==1,隐藏模版中的第二页
         *      pageCount==2,不做处理
         *      pageCount>2,填充页
         * 
         * Param pageCount 页数
         * Param columnCount 列数
         */
        public int CopyPage(int pageCount, int columnCount)
        {
            return this.CopyPage(pageCount, columnCount, 2);
        }


        /**
         * 页处理
         *      pageCount==1,隐藏模版中的第二页
         *      pageCount==pageNum,不做处理
         *      pageCount>pageNum,填充页
         * 
         * Param pageCount 页数
         * Param columnCount 列数
         * Param pageNum 参照页
         */
        protected int CopyPage(int pageCount, int columnCount, int pageNum)
        {
            if (pageCount == 1 && pageNum == 2)
            {
                //删除行
                for (int i = this.rowCount; i < (this.rowCount * 2); i++)
                {
                    Row rowT = this.GetRow(i);
                    //this.sheet.RemoveRow(rowT);
                    rowT.ZeroHeight = true;
                }
            }
            if (pageCount > pageNum)
            {

                for (int pageIndex = pageNum + 1; pageIndex <= pageCount; pageIndex++)
                {
                    //此方法之类实现
                    this.CopyPageValues(pageIndex);
                    for (int rowNum = 0; rowNum < rowCount; rowNum++)
                    {
                        this.CopyRowStyle(this.GetRowIndexAbsolute(1, rowNum), this.GetRowIndexAbsolute(pageIndex, rowNum), columnCount);
                    }
                }
            }
            return pageCount;
        }

        /*
         * 合并单元格
         * 
         * Param pageIndex 页码
         * Param row1 起始行号(相对值)
         * Param column1 起始列号
         * Param row2 终止行号(相对值)
         * Param colunm2 终止列号
         */
        protected virtual void SetMergedRegion(int pageIndex, int row1, int column1, int row2, int colunm2)
        {
            this.sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(this.GetRowIndexAbsolute(pageIndex, row1), this.GetRowIndexAbsolute(pageIndex, row2), column1, colunm2));
        }

        /*
         * 合并单元格
         *
         * Param row1 起始行号
         * Param column1 起始列号
         * Param row2 终止行号
         * Param colunm2 终止列号
         */
        protected void SetMergedRegion(int row1, int column1, int row2, int colunm2)
        {
            this.sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(row1, row2, column1, colunm2));
        }

        /*
         * 拷贝单元格
         *
         * Param pageIndex 页码
         * Param rowIndexRelative 行号(相对值)
         * Param cellIndex 列号
         * Param formula 公式
         */
        protected virtual void CopyCell(int pageIndex, int rowIndexRelative, int cellIndex, string formula)
        {
            this.SetRowCellFormula(this.GetRowIndexAbsolute(pageIndex, rowIndexRelative), cellIndex, formula);
        }


        protected void SetRowCellBarCode(int pageIndex, int rowIndexRelative, int cellIndex)
        {
            this.SetRowCellStyle(pageIndex, rowIndexRelative, cellIndex, barCodeFontName, barCodeFontSize);
        }


        /*
        * 设置单元格样式
        *
        * Param pageIndex 页码
        * Param rowIndexRelative 相对行号
        * Param cellIndex 列号
        * Param fontName 字体名称
        * Param fontSize 字体大小
        * 
        * Return 生成文件的URL
        */
        protected void SetRowCellStyle(int pageIndex, int rowIndexRelative, int cellIndex, String fontName, short fontSize)
        {
            int rowIndexAbsolute = this.GetRowIndexAbsolute(pageIndex, rowIndexRelative);
            Row row = this.GetRow(rowIndexAbsolute);
            Cell cell = row.GetCell((short)cellIndex);
            if (cell == null) cell = row.CreateCell((short)cellIndex);
            CellStyle style = workbook.CreateCellStyle();
            if (cell.CellStyle != null)
            {
                style.CloneStyleFrom(cell.CellStyle);
            }
            Font font = workbook.CreateFont();
            font.FontName = fontName;
            font.FontHeightInPoints = fontSize;
            style.SetFont(font);
            cell.CellStyle = workbook.CreateCellStyle();
            cell.CellStyle.CloneStyleFrom(style);
        }



        /*
         * 设置单元格值
         *
         * Param pageIndex 页码
         * Param rowIndexRelative 相对行号
         * Param cellIndex 列号
         * Param value 目标值
         * 
         * Return 生成文件的URL
         */
        protected virtual void SetRowCell(int pageIndex, int rowIndexRelative, int cellIndex, String value)
        {
            int rowIndexAbsolute = this.GetRowIndexAbsolute(pageIndex, rowIndexRelative);
            Row row = this.GetRow(rowIndexAbsolute);
            Cell cell = row.GetCell((short)cellIndex);
            if (cell == null) cell = row.CreateCell((short)cellIndex);
            cell.SetCellValue(value);
        }

        /*
        * 设置单元格值
        *
        * Param pageIndex 页码
        * Param rowIndexRelative 相对行号
        * Param cellIndex 列号
        * Param value 目标值
        * 
        * Return 生成文件的URL
        */
        protected virtual void SetRowCell(int pageIndex, int rowIndexRelative, int cellIndex, double value)
        {
            int rowIndexAbsolute = this.GetRowIndexAbsolute(pageIndex, rowIndexRelative);
            Row row = this.GetRow(rowIndexAbsolute);
            Cell cell = row.GetCell((short)cellIndex);
            if (cell == null) cell = row.CreateCell((short)cellIndex);
            cell.SetCellValue(value);
        }

        /*
         * 根据相对位置行号获得绝对位置行号
         *
         * Param pageIndex 页码
         * Param rowIndexRelative 相对行号
         * 
         * Return 绝对位置行号
         */
        protected virtual int GetRowIndexAbsolute(int pageIndex, int rowIndexRelative)
        {
            int rowIndexAbsolute = this.rowCount * (pageIndex - 1) + rowIndexRelative;
            return rowIndexAbsolute;
        }



        /*
         * 设置单元格
         *
         * Param rowIndex 行号
         * Param cellIndex 列号
         * Param value 值
         * 
         * Return 绝对位置行号
         */
        protected void SetRowCell(int rowIndex, int cellIndex, String value)
        {
            Row row = this.GetRow(rowIndex);
            Cell cell = row.GetCell((short)cellIndex);
            if (cell == null) cell = row.CreateCell((short)cellIndex);
            cell.SetCellValue(value);
        }

        //public void AddPicture(int rowIndex, int cellIndex, byte[] bytes)
        //{
        //    int pictureIdx = this.workbook.AddPicture(bytes, HSSFWorkbook.PICTURE_TYPE_PNG);

        //    // Create the drawing patriarch.  This is the top level container for all shapes. 
        //    HSSFPatriarch patriarch = this.sheet.CreateDrawingPatriarch();

        //    //add a picture
        //    HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 1023, 0, rowIndex, cellIndex, rowIndex, cellIndex);
        //    HSSFPicture pict = patriarch.CreatePicture(anchor, pictureIdx);
        //    //pict.Resize();
        //}

        /**
         * 
         * 设置单元格公式
         * 
         * Param rowIndex 行号
         * Param cellIndex 列号
         * Param formula 公式
         * 
         */
        protected void SetRowCellFormula(int rowIndex, int cellIndex, string formula)
        {
            Row row = this.GetRow(rowIndex);
            Cell cell = row.GetCell(cellIndex);
            if (cell == null) cell = row.CreateCell((short)cellIndex);
            cell.CellFormula = formula;
        }


        /**
         * 
         * 获得行对象
         * 
         * Param rowIndex 行号
         * Return 行对象
         */
        protected Row GetRow(int rowIndex)
        {
            Row row = null;
            row = this.sheet.GetRow(rowIndex);
            if (row == null) row = this.sheet.CreateRow(rowIndex);
            return row;
        }

        /**
         * 
         * 拷贝行样式
         * 
         * Param rowIndexFrom 源行号
         * Param rowIndexTo 目标行号
         * Param columnCount 列数
         * 
         * Return 行对象
         */
        private void CopyRowStyle(int rowIndexFrom, int rowIndexTo, int columnCount)
        {
            this.CopyRowStyle(this.GetRow(rowIndexFrom), this.GetRow(rowIndexTo), columnCount);
        }

        /**
        * 拷贝行
        * 
        * Param cellFrom 源单元格
        * Param cellTo 目标单元格
        * 
        */
        private void CopyRowStyle(Row rowFrom, Row rowTo, int columnCount)
        {
            //rowTo.HeightInPoints = rowFrom.HeightInPoints;
            rowTo.Height = rowFrom.Height;
            for (int i = 0; i < columnCount; i++)
            {
                this.CopyCellStyle(this.GetCell(rowFrom.RowNum, i), this.GetCell(rowTo.RowNum, i));
            }
        }

        /**
         * 拷贝单元格样式
         * 
         * Param cellFrom 源单元格
         * Param cellTo 目标单元格
         * 
         */
        protected void CopyCellStyle(Cell cellFrom, Cell cellTo)
        {
            //cellTo.CellStyle = workbook.CreateCellStyle();
            //cellTo.CellStyle.CloneStyleFrom(cellFrom.CellStyle);
            cellTo.CellStyle = cellFrom.CellStyle;
        }

        /**
         * 获得单元格对象
         * 
         * Param rowIndex 行号
         * Param cellIndex 列号
         * 
         * Return 单元格对象
         */
        protected Cell GetCell(int rowIndex, int cellIndex)
        {
            Row row = this.GetRow(rowIndex);
            Cell cell = row.GetCell(cellIndex);
            if (cell == null) cell = row.CreateCell(cellIndex);
            return cell;
        }

        public HSSFWorkbook GetWorkbook()
        {
            return workbook;
        }


        protected string GetBarcodeFontName(int rowIndex, int cellIndex)
        {
            return this.GetCell(rowIndex, cellIndex).CellStyle.GetFont(this.workbook).FontName;
        }


        /**
         * 填充报表值
         * 
         * Param list 参数
         * 
         * Return 成功
         */
        public abstract bool FillValues(String templateFileName, IList<object> list);
        public bool FillValues(String templateFileName, string orderNo)
        {
            return FillValues(templateFileName, GetDataList(orderNo));
        }

        /**
         * 拷贝单元格
         * 
         * Param pageIndex 页码
         */
        public abstract void CopyPageValues(int pageIndex);

        public virtual IList<object> GetDataList(string code)
        {
            throw new NotImplementedException();
        }

    }
}
