
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Services.Transaction;


namespace com.Sconit.Service.Report
{
    /*
     * 用于带重复头的excel报表
     * 
     * 
     */
    public abstract class RepTemplate1 : ReportBaseMgr
    {

        //明细部分的行数
        protected int pageDetailRowCount = 0;
        //列数  1起始
        protected int columnCount = 0;
        //报表头的行数  1起始
        protected int headRowCount = 0;
        //报表尾的行数  1起始
        protected int bottomRowCount = 0;

        //记录数
        protected int resultCount = 0;

        public override bool FillValues(String templateFileName, IList<object> list)
        {
            if (list == null || list.Count < 1 || templateFileName == null || templateFileName.Length < 0) return false;
            this.rowCount = pageDetailRowCount + bottomRowCount;
            this.init(templateFileName);
            bool result = this.FillValuesImpl(templateFileName, list);
            /*
			if (result == false)
            {
                this.workbook = new HSSFWorkbook();
            }
			*/
            return result;
        }


        protected bool isPageBottom(int rowIndex, int rowTotal)
        {
            return (rowIndex == (this.resultCount - 1) || ((rowIndex + 1) % this.pageDetailRowCount == 0) || this.isLastPage(rowTotal));
        }

        protected bool isLastPage(int rowTotal)
        {
            return rowTotal == (this.resultCount - 1);
        }

        /*
         * 计算页数
         * 
         * Param resultCount 记录明细数
         * 
         */
        private int PageCount(int resultCount)
        {

            return (int)Math.Ceiling(resultCount / (pageDetailRowCount * 1.0)); ;
        }

        protected int CopyPage(int resultCount)
        {
            this.resultCount = resultCount;
            int pageCount = PageCount(resultCount);
            return this.CopyPage(pageCount, this.columnCount, 1);
        }

        /*
         * 拷贝单元格
         *
         * Param pageIndex 页码
         * Param rowIndexRelative 行号(相对值)
         * Param cellIndex 列号
         * Param formula 公式
         */
        protected override void CopyCell(int pageIndex, int rowIndexRelative, int cellIndex, string formula)
        {
            if (pageIndex < 2)
            {
                base.CopyCell(pageIndex, rowIndexRelative - this.headRowCount, cellIndex, formula);
            }
            else
            {
                base.CopyCell(pageIndex, rowIndexRelative, cellIndex, formula);
            }
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
        protected override void SetMergedRegion(int pageIndex, int row1, int column1, int row2, int colunm2)
        {
            if (pageIndex < 2)
            {
                base.SetMergedRegion(pageIndex, row1 - this.headRowCount, column1, row2 - this.headRowCount, colunm2);
            }
            else
            {
                base.SetMergedRegion(pageIndex, row1, column1, row2, colunm2);
            }
        }



        /*
        * 根据相对位置行号获得绝对位置行号
        *
        * Param pageIndex 页码
        * Param rowIndexRelative 相对行号
        * 
        * Return 绝对位置行号
        */
        protected override int GetRowIndexAbsolute(int pageIndex, int rowIndexRelative)
        {
            int rowIndexAbsolute = rowIndexRelative;

            if (pageIndex == 1)
            {
                rowIndexAbsolute += this.headRowCount;
            }

            if (pageIndex > 1)
            {
                rowIndexAbsolute = this.headRowCount + this.pageDetailRowCount + this.bottomRowCount + (this.pageDetailRowCount + this.bottomRowCount) * (pageIndex - 2) + rowIndexRelative;
                //(头行数 + 列头 + 明细 +尾巴) (页数-1)
                //rowIndexAbsolute = (headRowCount + this.rowCount + bottomRowCount) * (pageIndex - 1) + rowIndexRelative;

            }
            return rowIndexAbsolute;
        }


        protected abstract bool FillValuesImpl(String templateFileName, IList<object> list);
    }
}

