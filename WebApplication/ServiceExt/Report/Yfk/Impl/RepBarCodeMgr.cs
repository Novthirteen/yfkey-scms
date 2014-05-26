using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using Castle.Services.Transaction;
using NPOI.HSSF.UserModel;
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
    public class RepBarCodeMgr : ReportBaseMgr
    {

        private static readonly int ROW_COUNT = 11;
        //列数   1起始
        private static readonly int COLUMN_COUNT = 6;

        private IHuMgr huMgr;
        private IItemReferenceMgr itemReferenceMgr;
        public IEntityPreferenceMgr entityPreferenceMgr { get; set; }

        

        public RepBarCodeMgr(String reportTemplateFolder, IHuMgr huMgr, IItemReferenceMgr itemReferenceMgr)
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.huMgr = huMgr;
            this.itemReferenceMgr = itemReferenceMgr;
        }


        /**
         * 需要拷贝的数据与合并单元格操作
         * 
         * Param pageIndex 页号
         */
        public override void CopyPageValues(int pageIndex)
        {
            this.SetMergedRegion(pageIndex, 0, 0, 0, 4);
            this.SetMergedRegion(pageIndex, 1, 0, 1, 2);
            this.SetMergedRegion(pageIndex, 3, 0, 3, 3);
            this.SetMergedRegion(pageIndex, 4, 0, 4, 1);
            this.SetMergedRegion(pageIndex, 5, 0, 5, 1);
            this.SetMergedRegion(pageIndex, 6, 0, 6, 1);
            this.SetMergedRegion(pageIndex, 7, 0, 7, 2);
            this.SetMergedRegion(pageIndex, 8, 0, 8, 1);
            this.SetMergedRegion(pageIndex, 9, 0, 9, 3);

            this.CopyCell(pageIndex, 2, 0, "A3");
            this.CopyCell(pageIndex, 4, 0, "A5");
            this.CopyCell(pageIndex, 10, 0, "A11");
            this.CopyCell(pageIndex, 10, 2, "C11");
        }

        /**
         * 填充报表
         * 
         * Param list [0]huDetailList
         */
        [Transaction(TransactionMode.Requires)]
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

                    this.sheet.DisplayGridlines = false;
                    this.sheet.IsPrintGridlines = false;

                    //this.sheet.DisplayGuts = false;

                    int count = 0;
                    foreach (Hu hu in huList)
                    {
                        if (hu.Item.Type.Equals("M") //成品
                                || hu.Item.Type.Equals("P")) //原材料
                        {
                            count++;
                        }
                    }

                    if (count == 0) return false;

                    this.barCodeFontName = this.GetBarcodeFontName(0, 0);

                    //加页删页
                    this.CopyPage(count, COLUMN_COUNT, 1);

                    int pageIndex = 1;

                    string companyCode = entityPreferenceMgr.LoadEntityPreference(BusinessConstants.ENTITY_PREFERENCE_CODE_COMPANY_CODE).Value;
                    if (companyCode == null) companyCode = string.Empty;

                    foreach (Hu hu in huList)
                    {
                        if (hu.Item.Type.Equals("M")) //成品
                        {

                            if (hu.PrintCount > 1)
                            {
                                this.SetRowCell(pageIndex, 1, 3, "(R)");
                            }
                            hu.PrintCount += 1;

                            //YFKSS
                            int rowIndexAbsolute = this.GetRowIndexAbsolute(pageIndex, 1);
                            this.SetMergedRegion(pageIndex, 1, 4, 9, 4);
                            this.SetRowCell(pageIndex, 1, 4, companyCode);
                            Cell cell = this.GetCell(rowIndexAbsolute, 4);
                            CellStyle cellStyle = workbook.CreateCellStyle();
                            Font font = workbook.CreateFont();
                            font.FontName = "宋体";
                            font.FontHeightInPoints = 24;
                            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.BOLD;
                            cellStyle.SetFont(font);
                            cellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                            cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.TOP;
                            cellStyle.Rotation = (short)-90;
                            cell.CellStyle = workbook.CreateCellStyle();
                            cell.CellStyle.CloneStyleFrom(cellStyle);

                            //hu id内容
                            string barCode = Utility.BarcodeHelper.GetBarcodeStr(hu.HuId, this.barCodeFontName);
                            this.SetRowCell(pageIndex, 0, 0, barCode);

                            //hu id内容
                            this.SetRowCell(pageIndex, 1, 0, hu.HuId);

                            //PART NO.内容
                            this.SetRowCell(pageIndex, 3, 0, hu.Item.Code);

                            //SHIFT
                            this.SetRowCell(pageIndex, 4, 2, "SHIFT");

                            //QUANTITY.
                            this.SetRowCell(pageIndex, 4, 3, "QUANTITY.");

                            //批号LotNo
                            this.SetRowCell(pageIndex, 5, 0, hu.LotNo);


                            //SHIFT内容
                            this.SetRowCell(pageIndex, 5, 2, Utility.BarcodeHelper.GetShiftCode(hu.HuId));

                            //QUANTITY内容
                            this.SetRowCell(pageIndex, 5, 3, hu.Qty.ToString("0.########"));

                            //CUSTPART
                            this.SetRowCell(pageIndex, 6, 0, "CUSTPART");

                            //WO DATE
                            this.SetRowCell(pageIndex, 6, 3, "WO DATE");

                            //CUSTPART内容
                            IList<ItemReference> itemReferenceList = itemReferenceMgr.GetItemReference(hu.Item.Code);
                            if (itemReferenceList != null && itemReferenceList.Count > 0)
                            {
                                this.SetRowCell(pageIndex, 7, 0, itemReferenceList[0].ReferenceCode);
                            }
                            //WO DATE内容
                            this.SetRowCell(pageIndex, 7, 3, hu.ManufactureDate.ToString("MM/dd/yy"));

                            //DESCRIPTION.
                            this.SetRowCell(pageIndex, 8, 0, "DESCRIPTION.");

                            //DESCRIPTION内容
                            this.SetRowCell(pageIndex, 9, 0, hu.Item.Description);

                            //PRINTED DATE:内容
                            this.SetRowCell(pageIndex, 10, 1, DateTime.Now.ToString("MM/dd/yy"));

                            //print name 内容
                            this.SetRowCell(pageIndex, 10, 3, userName);


                        

                            this.sheet.SetRowBreak(this.GetRowIndexAbsolute(pageIndex, ROW_COUNT - 1));
                            pageIndex++;
                        }
                        else if (hu.Item.Type.Equals("P")) //原材料
                        {
                            if (hu.PrintCount > 1)
                            {
                                this.SetRowCell(pageIndex, 1, 3, "(R)");
                            }
                            hu.PrintCount += 1;

                            //hu id内容
                            string barCode = Utility.BarcodeHelper.GetBarcodeStr(hu.HuId, this.barCodeFontName);
                            this.SetRowCell(pageIndex, 0, 0, barCode);


                            //hu id内容
                            this.SetRowCell(pageIndex, 1, 0, hu.HuId);


                            //画方框
                            int rowIndexAbsolute = this.GetRowIndexAbsolute(pageIndex, 2);
                            Cell cell1 = this.GetCell(rowIndexAbsolute, 4);
                            CellStyle cellStyle1 = workbook.CreateCellStyle();
                            cellStyle1.BorderBottom = NPOI.SS.UserModel.CellBorderType.NONE;

                            cellStyle1.BorderLeft = NPOI.SS.UserModel.CellBorderType.THIN;
                            cellStyle1.BorderRight = NPOI.SS.UserModel.CellBorderType.THIN;
                            cellStyle1.BorderTop = NPOI.SS.UserModel.CellBorderType.THIN;
                            cell1.CellStyle = workbook.CreateCellStyle();
                            cell1.CellStyle.CloneStyleFrom(cellStyle1);


                            //PART NO.内容
                            this.SetRowCell(pageIndex, 3, 0, hu.Item.Code);

                            //画方框
                            rowIndexAbsolute = this.GetRowIndexAbsolute(pageIndex, 3);
                            CellStyle cellStyle2 = workbook.CreateCellStyle();
                            Cell cell2 = this.GetCell(rowIndexAbsolute, 4);
                            cellStyle2.BorderLeft = NPOI.SS.UserModel.CellBorderType.THIN;
                            cellStyle2.BorderRight = NPOI.SS.UserModel.CellBorderType.THIN;
                            cellStyle2.BorderBottom = NPOI.SS.UserModel.CellBorderType.THIN;
                            cellStyle2.BorderTop = NPOI.SS.UserModel.CellBorderType.NONE;
                            cell2.CellStyle = workbook.CreateCellStyle();
                            cell2.CellStyle.CloneStyleFrom(cellStyle2);


                            //QUANTITY.
                            this.SetRowCell(pageIndex, 4, 2, "QUANTITY.");

                            //批号LotNo
                            this.SetRowCell(pageIndex, 5, 0, hu.LotNo);

                            //QUANTITY.
                            this.SetRowCell(pageIndex, 5, 2, hu.Qty.ToString("0.########"));


                            //DESCRIPTION.
                            this.SetRowCell(pageIndex, 6, 0, "DESCRIPTION.");

                            //DESCRIPTION内容
                            this.SetRowCell(pageIndex, 7, 0, hu.Item.Description);

                            //SUPPLIER.	
                            this.SetRowCell(pageIndex, 8, 0, "SUPPLIER.");

                            //SUPPLIER内容
                            this.SetRowCell(pageIndex, 9, 0, hu.ManufactureParty == null ? string.Empty : hu.ManufactureParty.Name);

                            //PRINTED DATE:内容
                            this.SetRowCell(pageIndex, 10, 1, DateTime.Now.ToString("MM/dd/yy"));

                            //print name 内容
                            this.SetRowCell(pageIndex, 10, 3, userName);

                            this.sheet.SetRowBreak(this.GetRowIndexAbsolute(pageIndex, ROW_COUNT - 1));
                            pageIndex++;
                        }

                    }

                    foreach (Hu hu in huList)
                    {
                        if (hu.Item.Type.Equals("M") || hu.Item.Type.Equals("P")) //成品  //原材料
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

    }

}
