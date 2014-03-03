using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using Castle.Services.Transaction;
using NPOI.HSSF.UserModel;
/*
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
*/
using com.Sconit.Service.MasterData;
using com.Sconit.Entity;
using NPOI.SS.UserModel;

namespace com.Sconit.Service.Report.Yfk.Impl
{
    /**
     * 
     * 原材料条码
     * 
     */
    [Transactional]
    public class RepBarCodeA4Mgr : ReportBaseMgr
    {

        private static readonly int PAGE_DETAIL_ROW_COUNT = 9;

        private static readonly int ROW_COUNT = 35;
        //列数   1起始
        private static readonly int COLUMN_COUNT = 20;

        private IHuMgr huMgr;

        private CellStyle cellStyle1;//成品

        private CellStyle cellStyle2;//原材料画方框

        private CellStyle cellStyle3;//原材料画方框

        public IEntityPreferenceMgr entityPreferenceMgr { get; set; }

        public RepBarCodeA4Mgr(String reportTemplateFolder, IHuMgr huMgr)
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.huMgr = huMgr;
        }


        /**
         * 需要拷贝的数据与合并单元格操作
         * 
         * Param pageIndex 页号
         */
        [Transaction(TransactionMode.Requires)]
        public override void CopyPageValues(int pageIndex)
        {

            //三行 三个
            for (int rowNum = 0; rowNum < 25; rowNum += 12)
            {
                //第一个
                //hu.id
                this.SetMergedRegion(pageIndex, rowNum, 0, rowNum, 4);
                //YFKSS
                //this.SetMergedRegion(pageIndex, rowNum + 1, 4, rowNum + 9, 4);
                //hu.id
                this.SetMergedRegion(pageIndex, rowNum + 1, 0, rowNum + 1, 2);
                //PART NO.
                this.SetMergedRegion(pageIndex, rowNum + 3, 0, rowNum + 3, 3);
                //LOT/SERUAL	
                this.SetMergedRegion(pageIndex, rowNum + 4, 0, rowNum + 4, 1);
                this.SetMergedRegion(pageIndex, rowNum + 5, 0, rowNum + 5, 1);
                this.SetMergedRegion(pageIndex, rowNum + 6, 0, rowNum + 6, 1);
                this.SetMergedRegion(pageIndex, rowNum + 7, 0, rowNum + 7, 1);
                this.SetMergedRegion(pageIndex, rowNum + 8, 0, rowNum + 8, 1);
                this.SetMergedRegion(pageIndex, rowNum + 9, 0, rowNum + 9, 3);

                //PART NO.
                this.CopyCell(pageIndex, rowNum + 2, 0, "A" + (rowNum + 3));
                //YFKSS
                //this.CopyCell(pageIndex, rowNum , 4, "E" + (rowNum+1) );
                //LOT/SERIAL NO.	
                this.CopyCell(pageIndex, rowNum + 4, 0, "A" + (rowNum + 5));
                //PRINTED DATE:
                this.CopyCell(pageIndex, rowNum + 10, 0, "A" + (rowNum + 11));
                //PRINTER USER:
                this.CopyCell(pageIndex, rowNum + 10, 2, "C" + (rowNum + 11));


                // 第二个
                //hu.id
                this.SetMergedRegion(pageIndex, rowNum, 7, rowNum, 11);
                //YFKSS
                //this.SetMergedRegion(pageIndex, rowNum+1, 11, rowNum+9, 11);
                //hu.id
                this.SetMergedRegion(pageIndex, rowNum + 1, 7, rowNum + 1, 9);
                //PART NO.
                this.SetMergedRegion(pageIndex, rowNum + 3, 7, rowNum + 3, 10);
                //LOT/SERUAL	
                this.SetMergedRegion(pageIndex, rowNum + 4, 7, rowNum + 4, 8);
                this.SetMergedRegion(pageIndex, rowNum + 5, 7, rowNum + 5, 8);
                this.SetMergedRegion(pageIndex, rowNum + 6, 7, rowNum + 6, 8);
                this.SetMergedRegion(pageIndex, rowNum + 7, 7, rowNum + 7, 8);
                this.SetMergedRegion(pageIndex, rowNum + 8, 7, rowNum + 8, 8);
                this.SetMergedRegion(pageIndex, rowNum + 9, 7, rowNum + 9, 10);

                //PART NO.
                this.CopyCell(pageIndex, rowNum + 2, 7, "H" + (rowNum + 3));
                //YFKSS
                //this.CopyCell(pageIndex, rowNum, 10, "K" + (rowNum + 1));
                //LOT/SERIAL NO.	
                this.CopyCell(pageIndex, rowNum + 4, 7, "H" + (rowNum + 5));
                //PRINTED DATE:
                this.CopyCell(pageIndex, rowNum + 10, 7, "H" + (rowNum + 11));
                this.CopyCell(pageIndex, rowNum + 10, 9, "J" + (rowNum + 11));


                // 第三个
                //hu.id
                this.SetMergedRegion(pageIndex, rowNum, 14, rowNum, 18);
                //YFKSS
                //this.SetMergedRegion(pageIndex, rowNum+1, 11, rowNum+9, 11);
                //hu.id
                this.SetMergedRegion(pageIndex, rowNum + 1, 14, rowNum + 1, 16);
                //PART NO.
                this.SetMergedRegion(pageIndex, rowNum + 3, 14, rowNum + 3, 17);
                //LOT/SERUAL	
                this.SetMergedRegion(pageIndex, rowNum + 4, 14, rowNum + 4, 15);
                this.SetMergedRegion(pageIndex, rowNum + 5, 14, rowNum + 5, 15);
                this.SetMergedRegion(pageIndex, rowNum + 6, 14, rowNum + 6, 15);
                this.SetMergedRegion(pageIndex, rowNum + 7, 14, rowNum + 7, 15);
                this.SetMergedRegion(pageIndex, rowNum + 8, 14, rowNum + 8, 15);
                this.SetMergedRegion(pageIndex, rowNum + 9, 14, rowNum + 9, 17);

                //PART NO.
                this.CopyCell(pageIndex, rowNum + 2, 14, "O" + (rowNum + 3));
                //YFKSS
                //this.CopyCell(pageIndex, rowNum, 10, "K" + (rowNum + 1));
                //LOT/SERIAL NO.	
                this.CopyCell(pageIndex, rowNum + 4, 14, "O" + (rowNum + 5));
                //PRINTED DATE:
                this.CopyCell(pageIndex, rowNum + 10, 14, "O" + (rowNum + 11));
                this.CopyCell(pageIndex, rowNum + 10, 16, "Q" + (rowNum + 11));

            }
        }

        /**
         * 填充报表
         * 
         * Param list [0]huDetailList
         */
        public override bool FillValues(String templateFileName, IList<object> list)
        {
            try
            {

                this.init(templateFileName, ROW_COUNT);

                if (list == null || list.Count == 0) return false;

                IList<Hu> huList = (IList<Hu>)list[0];
                string userName = "";
                if (list.Count == 2)
                {
                    userName = (string)list[1];
                }

                int count = 0;
                foreach (Hu hu in huList)
                {
                    if (hu.Item.Type.Equals("M") || hu.Item.Type.Equals("P"))
                    {
                        count++;
                    }
                }
                if (count == 0) return false;

                this.barCodeFontName = this.GetBarcodeFontName(0, 0);

                int pageCount = (int)Math.Ceiling(count / (PAGE_DETAIL_ROW_COUNT * 1.0));
                /*
                for (int rowNum = 0; rowNum < 25; rowNum += 12)
                {
                    this.SetRowCellBarCode(1, rowNum, 0);
                    this.SetRowCellBarCode(1, rowNum, 7);
                    this.SetRowCellBarCode(1, rowNum, 14);
                }
                 */
                this.CopyPage(pageCount, COLUMN_COUNT, 1);


                this.sheet.DisplayGridlines = false;
                this.sheet.IsPrintGridlines = false;

                /*
                CellStyle cellStyleT = workbook.CreateCellStyle();
                Font fontT = workbook.CreateFont();
                fontT.FontHeightInPoints = (short)9;
                fontT.FontName = "宋体";
                fontT.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.BOLD;
                cellStyleT.SetFont(fontT);
                */

                int pageIndex = 1;
                int num = 1;

                string companyCode = entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_COMPANY_CODE).Value;
                if (companyCode == null) companyCode = string.Empty;

                //成品
                cellStyle1 = workbook.CreateCellStyle();
                Font font = workbook.CreateFont();
                font.FontName = "宋体";
                font.FontHeightInPoints = 24;
                font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.BOLD;
                cellStyle1.SetFont(font);
                cellStyle1.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                cellStyle1.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.TOP;
                cellStyle1.Rotation = (short)-90;

                //原材料画方框
                cellStyle2 = workbook.CreateCellStyle();
                cellStyle2.BorderBottom = NPOI.SS.UserModel.CellBorderType.NONE;
                cellStyle2.BorderLeft = NPOI.SS.UserModel.CellBorderType.THIN;
                cellStyle2.BorderRight = NPOI.SS.UserModel.CellBorderType.THIN;
                cellStyle2.BorderTop = NPOI.SS.UserModel.CellBorderType.THIN;

                //原材料画方框
                cellStyle3 = workbook.CreateCellStyle();
                cellStyle3.BorderLeft = NPOI.SS.UserModel.CellBorderType.THIN;
                cellStyle3.BorderRight = NPOI.SS.UserModel.CellBorderType.THIN;
                cellStyle3.BorderBottom = NPOI.SS.UserModel.CellBorderType.THIN;
                cellStyle3.BorderTop = NPOI.SS.UserModel.CellBorderType.NONE;

                foreach (Hu hu in huList)
                {

                    //3行每行3个
                    if (hu.Item.Type.Equals("M") || hu.Item.Type.Equals("P"))
                    {

                        this.writeContent(companyCode, userName, pageIndex, num, hu);

                        if (num == count || num % PAGE_DETAIL_ROW_COUNT == 0)
                        {
                            pageIndex++;
                        }
                        num++;
                    }

                    /*
                    if ( num == huList.Count + 1)
                    {
                        for (int i = 1; i <= (PAGE_DETAIL_ROW_COUNT - (num % PAGE_DETAIL_ROW_COUNT)); i++)
                        {
                            //YFKSS
                            //this.SetRowCell(pageIndex - 1, 1, 4, string.Empty, i);
                            //PART NO.
                            this.CopyCell(pageIndex - 1, this.getRowIndex(2, num - 1 + i), 0, "");
                            this.SetRowCell(pageIndex - 1, 2, 0, string.Empty, num - 1 + i);
                            //LOT/SERIAL NO.
                            this.CopyCell(pageIndex - 1, this.getRowIndex(4, num - 1 + i), 0, "");
                            this.SetRowCell(pageIndex - 1, 4, 0, string.Empty, num - 1 + i);
                            //PRINTED DATE:
                            this.CopyCell(pageIndex - 1, this.getRowIndex(10, num - 1 + i), 0, "");
                            this.SetRowCell(pageIndex - 1, 10, 0, string.Empty, num - 1 + i);
                            //PRINTER USER:
                            this.CopyCell(pageIndex - 1, this.getRowIndex(10, num - 1 + i), 2, "");
                            this.SetRowCell(pageIndex - 1, 10, 2, string.Empty, num - 1 + i);
                        }

                    }
                    */
                }


                foreach (Hu hu in huList)
                {
                    if (hu.Item.Type.Equals("P") || hu.Item.Type.Equals("M")) //原材料
                    {
                        huMgr.UpdateHu(hu);
                    }
                }

            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        private void writeContent(string companyCode, string userName, int pageIndex, int num, Hu hu)
        {
            if (hu.PrintCount > 1)
            {
                this.SetRowCell(pageIndex, 1, 3, "(R)", num);
            }
            hu.PrintCount += 1;

            if (hu.Item.Type.Equals("M")) //成品
            {

                //YFKSS
                this.SetMergedRegion(pageIndex, 1, 4, 9, 4, num);
                Cell cell = this.GetCell(this.GetRowIndexAbsolute(pageIndex, getRowIndex(1, num)), getColumnIndex(4, num));

                //cell.CellStyle = workbook.CreateCellStyle();
                //cell.CellStyle.CloneStyleFrom(cellStyle);
                cell.CellStyle = this.cellStyle1;
                this.SetRowCell(pageIndex, 1, 4, companyCode, num);

                /*
                Bitmap bitmap = Barcode_Writer.Code128.Instance.Generate("12314325346457567");
                MemoryStream ms = new MemoryStream();
                bitmap.Save(ms, ImageFormat.Jpeg);
                ms.Flush();
                byte[] bData = ms.GetBuffer();
                ms.Close();
 
                int pictureIdx = this.workbook.AddPicture(bData, HSSFWorkbook.PICTURE_TYPE_JPEG);

                HSSFPatriarch patriarch = this.sheet.CreateDrawingPatriarch();
                //add a picture
                HSSFClientAnchor anchor = new HSSFClientAnchor(0, 0, 70, 100, this.getColumnIndex(0, num), this.getRowIndex(0, num), this.getColumnIndex(0, num), this.getRowIndex(0, num));
                HSSFPicture pict = patriarch.CreatePicture(anchor, pictureIdx);
                */
                //hu id内容
                string barCode = Utility.BarcodeHelper.GetBarcodeStr(hu.HuId, this.barCodeFontName);
                this.SetRowCell(pageIndex, 0, 0, barCode, num);
                //hu id内容
                this.SetRowCell(pageIndex, 1, 0, hu.HuId, num);
                //PART NO.内容
                this.SetRowCell(pageIndex, 3, 0, hu.Item.Code, num);
                //批号LotNo
                this.SetRowCell(pageIndex, 5, 0, hu.LotNo, num);
                //SHIFT
                this.SetRowCell(pageIndex, 4, 2, "SHIFT", num);
                //SHIFT内容
                this.SetRowCell(pageIndex, 5, 2, Utility.BarcodeHelper.GetShiftCode(hu.HuId), num);
                //QUANTITY.
                this.SetRowCell(pageIndex, 4, 3, "QUANTITY.", num);
                //QUANTITY内容
                this.SetRowCell(pageIndex, 5, 3, hu.Qty.ToString("0.########"), num);
                //CUSTPART
                this.SetRowCell(pageIndex, 6, 0, "CUSTPART", num);
                //CUSTPART内容
                this.SetRowCell(pageIndex, 7, 0, hu.CustomerItemCode, num);
                //WO DATE
                this.SetRowCell(pageIndex, 6, 3, "WO DATE", num);
                //WO DATE内容
                this.SetRowCell(pageIndex, 7, 3, hu.ManufactureDate.ToString("MM/dd/yy"), num);
                //DESCRIPTION
                this.SetRowCell(pageIndex, 8, 0, "DESCRIPTION.", num);
                //DESCRIPTION内容
                this.SetRowCell(pageIndex, 9, 0, hu.Item.Description, num);
                //PRINTED DATE:内容
                this.SetRowCell(pageIndex, 10, 1, DateTime.Now.ToString("MM/dd/yy"), num);
                //print name 内容
                this.SetRowCell(pageIndex, 10, 3, userName, num);

            }
            else if (hu.Item.Type.Equals("P")) //原材料
            {
                //画方框
                Cell cell1 = this.GetCell(this.GetRowIndexAbsolute(pageIndex, getRowIndex(2, num)), getColumnIndex(4, num));

                //cell1.CellStyle = workbook.CreateCellStyle();
                //cell1.CellStyle.CloneStyleFrom(cellStyle1);
                cell1.CellStyle = this.cellStyle2;

                Cell cell2 = this.GetCell(this.GetRowIndexAbsolute(pageIndex, getRowIndex(3, num)), getColumnIndex(4, num));
                
                //cell2.CellStyle = workbook.CreateCellStyle();
                //cell2.CellStyle.CloneStyleFrom(cellStyle2);
                cell2.CellStyle = this.cellStyle3;

                //hu id内容
                string barCode = Utility.BarcodeHelper.GetBarcodeStr(hu.HuId, this.barCodeFontName);
                this.SetRowCell(pageIndex, 0, 0, barCode, num);
                //hu id内容
                this.SetRowCell(pageIndex, 1, 0, hu.HuId, num);
                //PART NO.内容
                this.SetRowCell(pageIndex, 3, 0, hu.Item.Code, num);
                //批号LotNo
                this.SetRowCell(pageIndex, 5, 0, hu.LotNo, num);
                //QUANTITY.
                this.SetRowCell(pageIndex, 4, 2, "QUANTITY.", num);
                //QUANTITY.
                this.SetRowCell(pageIndex, 5, 2, hu.Qty.ToString("0.########"), num);
                //DESCRIPTION	
                this.SetRowCell(pageIndex, 6, 0, "DESCRIPTION.", num);
                //DESCRIPTION内容
                this.SetRowCell(pageIndex, 7, 0, hu.Item.Description, num);
                //SUPPLIER
                this.SetRowCell(pageIndex, 8, 0, "SUPPLIER.", num);
                //SUPPLIER内容
                this.SetRowCell(pageIndex, 9, 0, hu.ManufactureParty == null ? string.Empty : hu.ManufactureParty.Name, num);
                //PRINTED DATE:内容
                this.SetRowCell(pageIndex, 10, 1, DateTime.Now.ToString("MM/dd/yy"), num);
                //print name 内容
                this.SetRowCell(pageIndex, 10, 3, userName, num);

            }
        }


        protected void SetMergedRegion(int pageIndex, int row1, int column1, int row2, int colunm2, int num)
        {
            this.SetMergedRegion(pageIndex, getRowIndex(row1, num), getColumnIndex(column1, num), getRowIndex(row2, num), getColumnIndex(colunm2, num));
        }

        private int getRowIndex(int rowIndexRelative, int num)
        {
            //return ((int)(Math.Ceiling(num / 3.0)) - 1) * 12 + rowIndexRelative;
            return (num - 1) / 3 % 3 * 12 + rowIndexRelative;
        }

        private int getColumnIndex(int cellIndex, int num)
        {
            return cellIndex + ((num - 1) % 3) * 7;
        }

        public void SetRowCell(int pageIndex, int rowIndexRelative, int cellIndex, String value, int num)
        {
            this.SetRowCell(pageIndex, getRowIndex(rowIndexRelative, num), getColumnIndex(cellIndex, num), value);
        }

    }

}
