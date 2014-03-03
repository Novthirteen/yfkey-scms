using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using Castle.Services.Transaction;
using com.Sconit.Utility;
using com.Sconit.Service.MasterData;

namespace com.Sconit.Service.Report.Yfk.Impl
{

    /*
     * 报验单
     * 
     */
    [Transactional]
    public class RepInspectOrderMgr : RepTemplate1
    {
        private IInspectOrderMgr inspectOrderMgr;

        public RepInspectOrderMgr(String reportTemplateFolder, IInspectOrderMgr inspectOrderMgr)
        {
            this.reportTemplateFolder = reportTemplateFolder;
            this.inspectOrderMgr = inspectOrderMgr;

            //明细部分的行数
            this.pageDetailRowCount = 40;
            //列数   1起始
            this.columnCount = 9;
            //报表头的行数  1起始
            this.headRowCount = 8;
            //报表尾的行数  1起始
            this.bottomRowCount = 1;
        }

        /**
         * 填充报表
         * 
         * Param list [0]InspectOrder
         *            
         */
        [Transaction(TransactionMode.Requires)]
        protected override bool FillValuesImpl(String templateFileName, IList<object> list)
        {
            try
            {

                InspectOrder inspectOrder = (InspectOrder)list[0];

                if (inspectOrder == null)
                {
                    return false;
                }
                IList<InspectOrderDetail> inspectOrderDetailsTemp = null;
                if (list.Count == 2)
                {
                    inspectOrderDetailsTemp = (IList<InspectOrderDetail>)list[1];
                }
                else
                {
                    inspectOrderDetailsTemp = inspectOrder.InspectOrderDetails;
                }

                if (inspectOrderDetailsTemp == null || inspectOrderDetailsTemp.Count == 0)
                {
                    return false;
                }

                IList<InspectOrderDetail> inspectOrderDetails = new List<InspectOrderDetail>();
                for (int i = 0; i < inspectOrderDetailsTemp.Count; i++)
                {
                    InspectOrderDetail inspectOrderDetail = new InspectOrderDetail();
                    CloneHelper.CopyProperty(inspectOrderDetailsTemp[i], inspectOrderDetail);
                    if (i == 0)
                    {
                        inspectOrderDetails.Add(inspectOrderDetail);
                    }
                    else
                    {
                        InspectOrderDetail inspectOrderDetailPrev = inspectOrderDetails[inspectOrderDetails.Count - 1];
                        if (inspectOrderDetail.LocationLotDetail.Item.Code == inspectOrderDetailPrev.LocationLotDetail.Item.Code
                            && inspectOrderDetail.LocationFrom.Code == inspectOrderDetailPrev.LocationFrom.Code
                            && inspectOrderDetail.LocationTo.Code == inspectOrderDetailPrev.LocationTo.Code
                            && (
                                (
                                   inspectOrderDetail.LocationLotDetail.Hu != null
                                && inspectOrderDetail.LocationLotDetail.Hu.Uom.Code == inspectOrderDetailPrev.LocationLotDetail.Hu.Uom.Code
                                && inspectOrderDetail.LocationLotDetail.Hu.UnitCount == inspectOrderDetailPrev.LocationLotDetail.Hu.UnitCount)
                                ||
                                (
                                 inspectOrderDetail.LocationLotDetail.Hu == null
                                && inspectOrderDetail.LocationLotDetail.Item.Uom.Code == inspectOrderDetailPrev.LocationLotDetail.Item.Uom.Code
                                && inspectOrderDetail.LocationLotDetail.Item.UnitCount == inspectOrderDetailPrev.LocationLotDetail.Item.UnitCount
                                )
                                )
                        )
                        {
                            //待检数
                            if (inspectOrderDetailPrev.InspectQty != null)
                            {
                                inspectOrderDetailPrev.InspectQty += inspectOrderDetail.InspectQty;
                            }
                            //合格数
                            if (inspectOrderDetailPrev.QualifiedQty != null)
                            {
                                inspectOrderDetailPrev.QualifiedQty += inspectOrderDetail.QualifiedQty;
                            }
                            //不合格数
                            if (inspectOrderDetailPrev.RejectedQty != null)
                            {
                                inspectOrderDetailPrev.RejectedQty += inspectOrderDetail.RejectedQty;
                            }
                        }
                        else
                        {
                            inspectOrderDetails.Add(inspectOrderDetail);
                        }
                    }
                }

                this.barCodeFontName = this.GetBarcodeFontName(2, 6);
                //this.SetRowCellBarCode(0, 2, 6);

                this.CopyPage(inspectOrderDetails.Count);

                this.FillHead(inspectOrder);


                int pageIndex = 1;
                int rowIndex = 0;
                int rowTotal = 0;
                foreach (InspectOrderDetail inspectOrderDetail in inspectOrderDetails)
                {

                    //物料号
                    this.SetRowCell(pageIndex, rowIndex, 0, inspectOrderDetail.LocationLotDetail.Item.Code);
                    //物料描述
                    this.SetRowCell(pageIndex, rowIndex, 1, inspectOrderDetail.LocationLotDetail.Item.Description);
                    //"单位Unit"
                    this.SetRowCell(pageIndex, rowIndex, 2, inspectOrderDetail.LocationLotDetail.Item.Uom.Code);
                    //"单包装UC"
                    this.SetRowCell(pageIndex, rowIndex, 3, inspectOrderDetail.LocationLotDetail.Item.UnitCount.ToString("0.########"));
                    //来源库位
                    this.SetRowCell(pageIndex, rowIndex, 4, inspectOrderDetail.LocationFrom.Code);
                    //目的库位
                    this.SetRowCell(pageIndex, rowIndex, 5, inspectOrderDetail.LocationTo.Code);
                    //待检数
                    this.SetRowCell(pageIndex, rowIndex, 6, inspectOrderDetail.InspectQty == null ? string.Empty : inspectOrderDetail.InspectQty.ToString("0.########"));
                    //合格数
                    this.SetRowCell(pageIndex, rowIndex, 7, inspectOrderDetail.QualifiedQty == null ? string.Empty : inspectOrderDetail.QualifiedQty.Value.ToString("0.########"));
                    //不合格数
                    this.SetRowCell(pageIndex, rowIndex, 8, inspectOrderDetail.RejectedQty == null ? string.Empty : inspectOrderDetail.RejectedQty.Value.ToString("0.########"));

                    if (this.isPageBottom(rowIndex, rowTotal))//页的最后一行
                    {
                        pageIndex++;
                        rowIndex = 0;
                    }
                    else
                    {
                        rowIndex++;
                    }
                    rowTotal++;
                }

                this.sheet.DisplayGridlines = false;
                this.sheet.IsPrintGridlines = false;

                if (inspectOrder.IsPrinted == null || inspectOrder.IsPrinted == false)
                {
                    inspectOrder.IsPrinted = true;
                    inspectOrderMgr.UpdateInspectOrder(inspectOrder);
                }

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }



        /*
         * 填充报表头
         * 
         * Param repack 报验单头对象
         */
        private void FillHead(InspectOrder inspectOrder)
        {
            string inspectCode = Utility.BarcodeHelper.GetBarcodeStr(inspectOrder.InspectNo, this.barCodeFontName);
            //报验单号
            this.SetRowCell(2, 6, inspectCode);
            //报验单号
            this.SetRowCell(3, 6, inspectOrder.InspectNo);
            //报验人 Create User:
            this.SetRowCell(5, 1, inspectOrder.CreateUser.Name);
            //报验时间
            this.SetRowCell(5, 6, inspectOrder.CreateDate.ToString("yyyy-MM-dd HH:mm"));
        }

        /**
           * 需要拷贝的数据与合并单元格操作
           * 
           * Param pageIndex 页号
           */
        public override void CopyPageValues(int pageIndex)
        {
            //* 我已阅读延锋杰华的安全告知！
            this.CopyCell(pageIndex, 48 - this.headRowCount, 0, "A49");
        }

        public override IList<object> GetDataList(string code)
        {
            IList<object> list = new List<object>();
            InspectOrder inspectOrderList = inspectOrderMgr.LoadInspectOrder(code, true, true);
            if (inspectOrderList != null)
            {
                list.Add(inspectOrderList);
            }
            return list;
        }

    }
}
